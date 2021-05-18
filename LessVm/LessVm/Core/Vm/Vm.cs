using System;
using System.Collections.Generic;
using System.Reflection;
using LessVm.Core.Collections;
using System.Linq;

namespace LessVm.Core.Vm
{
  public enum GeneralRegType
  {
    ESP,
    SP,
    SH,
    SL,
    EBP,
    BP,
    BH,
    BL,
  }

  public class Vm
  {
    public const int REG_SIZE = 64;
    public const int MEM_SIZE = 4096;
    public const int BASE_PTR = (int)GeneralRegType.ESP / 4;
    public const int STACK_PTR = (int)GeneralRegType.EBP / 4;

    public const int REG_TYPE_32 = 0;
    public const int REG_TYPE_16 = 1;
    public const int REG_TYPE_8H = 2;
    public const int REG_TYPE_8L = 3;

    public bool IsEnd { get; private set; } = false;
    public bool IsZero { get; private set; } = false;
    public bool IsSign { get; private set; } = false;

    private Dictionary<OPCODE, Func<BytecodeStream, bool>> _opTable
      = new Dictionary<OPCODE, Func<BytecodeStream, bool>>();

    public string Errors => string.Join("\n", _errors);
    public bool IsError => _errors.Count != 0;
    private List<string> _errors = new List<string>();

    BytecodeStream _code = default;
    MemoryObj _memory = default;
    Register[] _registers = default;
    IEnumerator<int> _itr = default;
    VmStatus _status = default;
    object _instance = default;
    List<MethodInfo> _methods = new List<MethodInfo>();

    public Vm()
    {
      RegisterOpTable();
    }

    public void Init(BytecodeStream code, object instance = null)
    {
      _code = code;
      _memory = new MemoryObj(MEM_SIZE);
      _errors.Clear();

      _registers = new Register[REG_SIZE];
      for (int i = 0; i < REG_SIZE; ++i)
        _registers[i] = new Register();
      _registers[BASE_PTR][0] = MEM_SIZE;
      _registers[STACK_PTR][0] = MEM_SIZE;

      _instance = instance;
      _methods.Clear();
      RegisterBuildMethods();
    }

    public bool InitMemory(BytecodeStream mem, int start, int end)
    {
      int length = end - start;
      if (length < 0) return PushError("メモリの初期化失敗しました");
      for (int i = 0; i < length && i < mem.Size; ++i)
      {
        if (!_memory.TrySetUi8(start + i, mem.ReadByte()))
          return PushError("メモリの初期化に失敗しました");
      }
      return true;
    }


    public void Run()
    {
      while (StepRun()) ;
    }

    public bool StepRun()
    {
      if (IsEnd) return false;
      if (_itr != null)
      {
        _itr.MoveNext();
        switch (_itr.Current)
        {
          case VmStatus.HALT:
            IsEnd = true;
            return PushError("組み込み関数からHALTを検出しました");
          case VmStatus.FINISH:
            _itr = null;
            SetFromVmStatus(_status);
            return true;
          case VmStatus.CONTINUE:
            return true;
          default:
            IsEnd = true;
            return PushError("組み込み関数で不正な値が検出されました");
        }
      }
      if (_code.IsEnd) return false;
      OPCODE op = (OPCODE)_code.ReadByte();
      if (_opTable.TryGetValue(op, out Func<BytecodeStream, bool> opFunc))
        return opFunc(_code);
      return PushError($"実行できないOPCODEです => {op}");
    }

    #region Opcode Runner
    public bool OpLoadB(BytecodeStream code)
    {
      int regId = code.ReadByte();
      int memPtr = code.ReadChar();
      if (_memory.TryGetUi8(memPtr, out byte val8))
      {
        return TrySetRegUi8(regId, val8);
      }
      return PushError($"メモリ範囲外を指定しました => ptr: {memPtr}");
    }

    public bool OpLoadW(BytecodeStream code)
    {
      int regId = code.ReadByte();
      int memPtr = code.ReadChar();
      if (_memory.TryGetUi16(memPtr, out ushort val16))
      {
        return TrySetRegUi16(regId, val16);
      }
      return PushError($"メモリ範囲外を指定しました => ptr: {memPtr}");
    }

    public bool OpLoadD(BytecodeStream code)
    {
      int regId = code.ReadByte();
      int memPtr = code.ReadChar();
      if (_memory.TryGetUi32(memPtr, out uint val32))
      {
        return TrySetRegUi32(regId, val32);
      }
      return PushError($"メモリ範囲外を指定しました => ptr: {memPtr}");
    }

    public bool OpStoreB(BytecodeStream code)
    {
      int memPtr = code.ReadChar();
      int regId = code.ReadByte();
      if (TryGetReg(regId, out uint val))
      {
        return _memory.TrySetUi8(memPtr, (byte)val);
      }
      return PushRegError(regId);
    }

    public bool OpStoreW(BytecodeStream code)
    {
      int memPtr = code.ReadChar();
      int regId = code.ReadByte();
      if (TryGetReg(regId, out uint val))
      {
        return _memory.TrySetUi16(memPtr, (ushort)val);
      }
      return PushRegError(regId);
    }

    public bool OpStoreD(BytecodeStream code)
    {
      int memPtr = code.ReadChar();
      int regId = code.ReadByte();
      if (TryGetReg(regId, out uint val))
      {
        return _memory.TrySetUi32(memPtr, val);
      }
      return PushRegError(regId);
    }

    public bool OpMoveIB(BytecodeStream code)
    {
      int regId = code.ReadByte();
      byte imm = code.ReadByte();
      if (!TrySetRegUi8(regId, imm))
        return PushRegError(regId);
      return true;
    }

    public bool OpMoveIW(BytecodeStream code)
    {
      int regId = code.ReadByte();
      ushort imm = code.ReadChar();
      if (!TrySetRegUi16(regId, imm))
        return PushRegError(regId);
      return true;
    }

    public bool OpMoveID(BytecodeStream code)
    {
      int regId = code.ReadByte();
      uint imm = (uint)code.ReadInt();
      if (!TrySetRegUi32(regId, imm))
        return PushRegError(regId);
      return true;
    }

    public bool OpMoveB(BytecodeStream code)
    {
      int rsId = code.ReadByte();
      int rtId = code.ReadByte();
      uint a;
      if (!TryGetReg(rsId, out a))
        return PushRegError(rsId);
      if (!TrySetRegUi8(rtId, (byte)a))
        return PushRegError(rtId);
      return true;
    }

    public bool OpMoveW(BytecodeStream code)
    {
      int rsId = code.ReadByte();
      int rtId = code.ReadByte();
      uint a;
      if (!TryGetReg(rsId, out a))
        return PushRegError(rsId);
      if (!TrySetRegUi16(rtId, (ushort)a))
        return PushRegError(rtId);
      return true;
    }

    public bool OpMoveD(BytecodeStream code)
    {
      int rsId = code.ReadByte();
      int rtId = code.ReadByte();
      uint a;
      if (!TryGetReg(rsId, out a))
        return PushRegError(rsId);
      if (!TrySetRegUi32(rtId, a))
        return PushRegError(rtId);
      return true;
    }

    public bool OpAddB(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (byte)(a + b));

    public bool OpAddW(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (ushort)(a + b));

    public bool OpAddD(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (a + b));

    public bool OpSubB(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (byte)(a - b));

    public bool OpSubW(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (ushort)(a - b));

    public bool OpSubD(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (a - b));

    public bool OpMulB(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (byte)(a * b));

    public bool OpMulW(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (ushort)(a * b));

    public bool OpMulD(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (a * b));

    public bool OpDivB(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (byte)(a / b));

    public bool OpDivW(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (ushort)(a / b));

    public bool OpDivD(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (a / b));

    public bool OpRemB(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (byte)(a % b));

    public bool OpRemW(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (ushort)(a % b));

    public bool OpRemD(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (a % b));

    public bool OpSllB(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (byte)(a << (byte)b));
      
    public bool OpSllW(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (ushort)(a << (byte)b));

    public bool OpSllD(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (a << (ushort)b));

    public bool OpSrlB(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (byte)(a >> (byte)b));

    public bool OpSrlW(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (ushort)(a >> (byte)b));

    public bool OpSrlD(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (a >> (ushort)b));

    public bool OpAndB(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (byte)(a & b));

    public bool OpAndW(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (ushort)(a & b));

    public bool OpAndD(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (a & b));

    public bool OpOrB(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (byte)(a | b));

    public bool OpOrW(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (ushort)(a | b));

    public bool OpOrD(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (a | b));

    public bool OpXorB(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (byte)(a ^ b));

    public bool OpXorW(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (ushort)(a ^ b));

    public bool OpXorD(BytecodeStream code)
      => OpCalcHelper(code, (a, b) => (a ^ b));

    public bool OpCmpB(BytecodeStream code)
      => OpCmpHelper(code, (a, b) => ((a - b) >> 7 & 1) == 1);

    public bool OpCmpW(BytecodeStream code)
      => OpCmpHelper(code, (a, b) => ((a - b) >> 15 & 1) == 1);

    public bool OpCmpD(BytecodeStream code)
      => OpCmpHelper(code, (a, b) => ((a - b) >> 31 & 1) == 1);

    public bool OpJmp(BytecodeStream code)
    {
      ushort address = code.ReadChar();
      if (code.TrySetPosition(address))
        return true;

      return PushError("参照不可能な箇所にジャンプしようとしました");
    }

    public bool OpJeq(BytecodeStream code)
    {
      ushort address = code.ReadChar();
      if (!IsZero) return true;

      if (code.TrySetPosition(address))
        return true;

      return PushError("参照不可能な箇所にジャンプしようとしました");
    }

    public bool OpJne(BytecodeStream code)
    {
      ushort address = code.ReadChar();
      if (IsZero) return true;

      if (code.TrySetPosition(address))
        return true;

      return PushError("参照不可能な箇所にジャンプしようとしました");
    }

    public bool OpJlt(BytecodeStream code)
    {
      ushort address = code.ReadChar();
      if (!IsSign || IsZero) return true;

      if (code.TrySetPosition(address))
        return true;

      return PushError("参照不可能な箇所にジャンプしようとしました");
    }

    public bool OpSysCall(BytecodeStream code)
    {
      ushort methodId = code.ReadChar();

      if (methodId < 0 || methodId > _methods.Count)
        return PushError("組み込み関数IDが不正です");

      CreateStatus();
      _itr = _methods[methodId].Invoke(_instance, new object[] { _status }) as IEnumerator<int>;

      return true;
    }
    #endregion

    #region Memory Push Helper
    public bool TryPushUi32(uint value)
    {
      uint sp = _registers[STACK_PTR][0];
      if (!_memory.TrySetUi32((int)sp - 5, value))
        return PushError("スタックバッファオーバーフロー");
      _registers[STACK_PTR][0] = sp - 5;
      return true;
    }

    public bool TryPopUi32()
    {
      uint sp = _registers[STACK_PTR][0];
      if (!_memory.TryGetUi32((int)sp - 5, out uint _))
        return PushError("スタックバッファオーバーフロー");
      _registers[STACK_PTR][0] = sp + 5;
      return true;
    }

    public bool TryPushUi16(ushort value)
    {
      uint sp = _registers[STACK_PTR][0];
      if (!_memory.TrySetUi16((int)sp - 3, value))
        return PushError("スタックバッファオーバーフロー");
      _registers[STACK_PTR][0] = sp - 3;
      return true;
    }

    public bool TryPopUi16()
    {
      uint sp = _registers[STACK_PTR][0];
      if (!_memory.TryGetUi16((int)sp - 3, out ushort _))
        return PushError("スタックバッファオーバーフロー");
      _registers[STACK_PTR][0] = sp + 3;
      return true;
    }

    public bool TryPushUi8(byte value)
    {
      uint sp = _registers[STACK_PTR][0];
      if (!_memory.TrySetUi8((int)sp - 1, value))
        return PushError("スタックバッファオーバーフロー");
      _registers[STACK_PTR][0] = sp - 1;
      return true;
    }

    public bool TryPopUi8()
    {
      uint sp = _registers[STACK_PTR][0];
      if (!_memory.TryGetUi8((int)sp - 1, out byte _))
        return PushError("スタックバッファオーバーフロー");
      _registers[STACK_PTR][0] = sp + 1;
      return true;
    }
    #endregion

    #region Register Access Helper
    public int RegType(int idx, int type)
      => idx * 4 + type;

    public bool TryGetReg(int id, out uint value)
    {
      value = 0;
      if (id / 4 < 0 || _registers.Length <= id / 4)
      {
        PushError($"レジスタの値の設定に失敗しました");
        return false;
      }
      value = _registers[id / 4][id % 4];
      return true;
    }

    public bool TrySetRegUi32(int id, uint val)
    {
      if (id / 4 < 0 || _registers.Length <= id / 4)
      {
        return PushError($"レジスタの値の設定に失敗しました");
      }
      _registers[id / 4][0] = val;
      return true;
    }

    public bool TrySetRegUi16(int id, ushort val)
    {
      if (id / 4 < 0 || _registers.Length <= id / 4)
      {
        return PushError($"レジスタの値の設定に失敗しました");
      }
      _registers[id / 4][1] = val;
      return true;
    }

    public bool TrySetRegUi8(int id, byte val)
    {
      if (id / 4 < 0 || _registers.Length <= id / 4)
      {
        return PushError($"レジスタの値の設定に失敗しました");
      }
      if (id % 4 == 2)
        _registers[id / 4][2] = val;
      else
        _registers[id / 4][3] = val;
      return true;
    }
    #endregion

    #region Memory Access Helper
    public byte GetMemory8(int ptr)
    {
      byte ret;
      _memory.TryGetUi8(ptr, out ret);
      return ret;
    }

    public ushort GetMemory16(int ptr)
    {
      ushort ret;
      _memory.TryGetUi16(ptr, out ret);
      return ret;
    }

    public uint GetMemory32(int ptr)
    {
      uint ret;
      _memory.TryGetUi32(ptr, out ret);
      return ret;
    }
    #endregion

    #region VmStatus Helper
    private void CreateStatus()
    {
      _status = new VmStatus();
      _status.isEnd = IsEnd;
      _status.isZero = IsZero;
      _status.isSign = IsSign;

      _status.memory = new MemoryObj(_memory);
      _status.registers = new Register[_registers.Length];
      Array.Copy(_registers, _status.registers, _registers.Length);
    }

    private void SetFromVmStatus(VmStatus status)
    {
      IsEnd = status.isEnd;
      IsZero = status.isZero;
      IsSign = status.isSign;

      _memory = new MemoryObj(status.memory);
      _registers = new Register[status.registers.Length];
      Array.Copy(status.registers, _registers, status.registers.Length);
    }
    #endregion

    private bool PushError(string err)
    {
      _errors.Add(err);
      return false;
    }

    private bool PushRegError(int rid)
    {
      _errors.Add($"Reg[{rid / 4}].{rid % 4}参照に失敗しました");
      return false;
    }

    private void RegisterBuildMethods()
    {
      if (_instance == null) return;
      _methods = _instance.GetType()
        .GetMethods()
        .Where(itr => itr.ReturnType == typeof(IEnumerator<int>))
        .ToList();
    }

    private void RegisterOpTable()
    {
      _opTable[OPCODE.LOAD_B] = OpLoadB;
      _opTable[OPCODE.LOAD_W] = OpLoadW;
      _opTable[OPCODE.LOAD_D] = OpLoadD;
      _opTable[OPCODE.STORE_B] = OpStoreB;
      _opTable[OPCODE.STORE_W] = OpStoreW;
      _opTable[OPCODE.STORE_D] = OpStoreD;

      _opTable[OPCODE.MOVE_I_B] = OpMoveIB;
      _opTable[OPCODE.MOVE_I_W] = OpMoveIW;
      _opTable[OPCODE.MOVE_I_D] = OpMoveID;
      _opTable[OPCODE.MOVE_B] = OpMoveB;
      _opTable[OPCODE.MOVE_W] = OpMoveW;
      _opTable[OPCODE.MOVE_D] = OpMoveD;

      _opTable[OPCODE.ADD_B] = OpAddB;
      _opTable[OPCODE.ADD_W] = OpAddW;
      _opTable[OPCODE.ADD_D] = OpAddD;
      _opTable[OPCODE.SUB_B] = OpSubB;
      _opTable[OPCODE.SUB_W] = OpSubW;
      _opTable[OPCODE.SUB_D] = OpSubD;
      _opTable[OPCODE.MUL_B] = OpMulB;
      _opTable[OPCODE.MUL_W] = OpMulW;
      _opTable[OPCODE.MUL_D] = OpMulD;
      _opTable[OPCODE.DIV_B] = OpDivB;
      _opTable[OPCODE.DIV_W] = OpDivW;
      _opTable[OPCODE.DIV_D] = OpDivD;
      _opTable[OPCODE.REM_B] = OpRemB;
      _opTable[OPCODE.REM_W] = OpRemW;
      _opTable[OPCODE.REM_D] = OpRemD;
      _opTable[OPCODE.SLL_B] = OpSllB;
      _opTable[OPCODE.SLL_W] = OpSllW;
      _opTable[OPCODE.SLL_D] = OpSllD;
      _opTable[OPCODE.SRL_B] = OpSrlB;
      _opTable[OPCODE.SRL_W] = OpSrlW;
      _opTable[OPCODE.SRL_D] = OpSrlD;
      _opTable[OPCODE.AND_B] = OpAndB;
      _opTable[OPCODE.AND_W] = OpAndW;
      _opTable[OPCODE.AND_D] = OpAndD;
      _opTable[OPCODE.OR_B] = OpOrB;
      _opTable[OPCODE.OR_W] = OpOrW;
      _opTable[OPCODE.OR_D] = OpOrD;
      _opTable[OPCODE.XOR_B] = OpXorB;
      _opTable[OPCODE.XOR_W] = OpXorW;
      _opTable[OPCODE.XOR_D] = OpXorD;

      _opTable[OPCODE.CMP_B] = OpCmpB;
      _opTable[OPCODE.CMP_W] = OpCmpW;
      _opTable[OPCODE.CMP_D] = OpCmpD;

      _opTable[OPCODE.JMP] = OpJmp;
      _opTable[OPCODE.JEQ] = OpJeq;
      _opTable[OPCODE.JNE] = OpJne;
      _opTable[OPCODE.JLT] = OpJlt;

      _opTable[OPCODE.SYSCALL] = OpSysCall;
    }

    #region Opcode Helper
    private bool GetRegVal(
      BytecodeStream code,
      out int rsId,
      out uint a,
      out uint b)
    {
      rsId = code.ReadByte();
      int rtId = code.ReadByte();
      b = 0;
      if (!TryGetReg(rsId, out a))
        return PushRegError(rsId);
      if (!TryGetReg(rtId, out b))
        return PushRegError(rtId);
      return true;
    }

    private bool OpCalcHelper(
      BytecodeStream code,
      Func<uint, uint, byte> calc)
    {
      uint a, b;
      int rsId;
      if (!GetRegVal(code, out rsId, out a, out b))
        return false;

      return TrySetRegUi8(rsId, calc(a, b));
    }

    private bool OpCalcHelper(
      BytecodeStream code,
      Func<uint, uint, ushort> calc)
    {
      uint a, b;
      int rsId;
      if (!GetRegVal(code, out rsId, out a, out b))
        return false;

      return TrySetRegUi16(rsId, calc(a, b));
    }

    private bool OpCalcHelper(
      BytecodeStream code,
      Func<uint, uint, uint> calc)
    {
      uint a, b;
      int rsId;
      if (!GetRegVal(code, out rsId, out a, out b))
        return false;

      return TrySetRegUi32(rsId, calc(a, b));
    }

    private bool OpCmpHelper(
      BytecodeStream code,
      Func<uint, uint, bool> isSign)
    {
      int rsId = code.ReadByte();
      int rtId = code.ReadByte();
      uint a, b;
      if (!TryGetReg(rsId, out a))
        return PushRegError(rsId);
      if (!TryGetReg(rtId, out b))
        return PushRegError(rtId);

      IsZero = (a - b) == 0;
      IsSign = isSign(a, b);

      return true;
    }
    #endregion
  }
}
