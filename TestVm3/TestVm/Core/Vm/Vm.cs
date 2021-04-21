using System;
using System.Collections.Generic;
using System.Text;
using TestVm.Core.Collections;

namespace TestVm.Core.Vm
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
    public const int REG_SIZE = 128;
    public const int MEM_SIZE = 4096;
    public const int BASE_PTR = (int)GeneralRegType.ESP / 4;
    public const int STACK_PTR = (int)GeneralRegType.EBP / 4;

    public const int REG_TYPE_32 = 0;
    public const int REG_TYPE_16 = 1;
    public const int REG_TYPE_8H = 2;
    public const int REG_TYPE_8L = 3;

    public bool IsEnd { get; private set; } = false;

    private Queue<Func<bool>> _apiQueue = new Queue<Func<bool>>();
    private Dictionary<OPCODE, Func<bool>> _opTable = new Dictionary<OPCODE, Func<bool>>();

    public string Errors => string.Join("\n", _errors);
    public bool IsError => _errors.Count != 0;
    private List<string> _errors = new List<string>();

    BytecodeStream _code = default;
    MemoryObj _memory = default;
    Register[] _registers = default;

    public Vm()
    {
      RegisterOpTable();
    }

    public void Init(BytecodeStream code)
    {
      _code = code;
      _memory = new MemoryObj(MEM_SIZE);
      _apiQueue.Clear();
      _errors.Clear();
      
      _registers = new Register[REG_SIZE];
      for (int i = 0; i < REG_SIZE; ++i)
        _registers[i] = new Register();
      _registers[BASE_PTR][0] = MEM_SIZE;
      _registers[STACK_PTR][0] = MEM_SIZE;
    }

    public void Run()
    {
      while (StepRun()) ;
    }

    public bool StepRun()
    {
      IsEnd = _code.IsEnd;
      if (IsEnd) return false;
      OPCODE op = (OPCODE)_code.ReadByte();
      if (_opTable.TryGetValue(op, out Func<bool> opFunc))
        return opFunc();
      return false;
    }

    #region Opcode Runner
    public bool OpLoadB()
    {
      int regId = _code.ReadByte();
      int memPtr = _code.ReadChar();
      if(_memory.TryGetUi8(memPtr, out byte val8))
      {
        SetRegUi8(regId, val8);
        return !IsError;
      }
      return PushError($"メモリ範囲外を指定しました => ptr: {memPtr}");
    }

    public bool OpLoadW()
    {
      int regId = _code.ReadByte();
      int memPtr = _code.ReadChar();
      if (_memory.TryGetUi16(memPtr, out ushort val16))
      {
        SetRegUi16(regId, val16);
        return true;
      }
      return PushError($"メモリ範囲外を指定しました => ptr: {memPtr}");
    }

    public bool OpLoadD()
    {
      int regId = _code.ReadByte();
      int memPtr = _code.ReadChar();
      if (_memory.TryGetUi32(memPtr, out uint val32))
      {
        SetRegUi32(regId, val32);
        return true;
      }
      return PushError($"メモリ範囲外を指定しました => ptr: {memPtr}");
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

    private bool PushError(string err)
    {
      _errors.Add(err);
      return false;
    }

    private void RegisterOpTable()
    {
      // TODO: _opTableの登録
      _opTable[OPCODE.LOAD_B] = OpLoadB;
      _opTable[OPCODE.LOAD_W] = OpLoadW;
      _opTable[OPCODE.LOAD_D] = OpLoadD;
    }

    #region Register Access Helper
    public int RegType(int idx, int type)
      => idx * 4 + type;

    public uint GetReg(int id)
      => _registers[id / 4][id % 4];

    public void SetRegUi32(int id, uint val)
      => _registers[id / 4][0] = val;

    public void SetRegUi16(int id, ushort val)
      => _registers[id / 4][1] = val;

    public void SetRegUi8(int id, byte val)
    {
      if (id % 4 != 2 && id % 4 != 3)
        PushError($"レジスタの値の設定に失敗しました");
      _registers[id / 4][id % 4] = val;
    }
    #endregion
  }
}
