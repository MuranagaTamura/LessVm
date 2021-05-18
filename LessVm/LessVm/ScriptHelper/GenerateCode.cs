using System.Collections.Generic;

namespace LessVm.ScriptHelper
{
  public enum RegType
  {
    u32,
    u16,
    u8h,
    u8l,
  }

  public static class GenerateCode
  {
    public static byte GenRegId(byte rid, RegType type)
      => (byte)(rid * 4 + type);

    public static void GenLoad(this IList<byte> code, byte r, ushort ptr)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.LOAD_B, r);
      code.Add(r);
      code.Add((byte)(ptr & 0xFF));
      code.Add((byte)(ptr >> 8));
    }

    public static void GenStore(this IList<byte> code, ushort ptr, byte r)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.STORE_B, r);
      code.Add((byte)(ptr & 0xFF));
      code.Add((byte)(ptr >> 8));
      code.Add(r);
    }

    public static void GenMoveI(this IList<byte> code, byte r, uint imm)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.MOVE_I_B, r);
      code.Add(r);
      GenImmByRegId(code, imm, r);
    }

    public static void GenMove(this IList<byte> code, byte r0, byte r1)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.MOVE_B, r0);
      code.Add(r0);
      GenRegIdByRegId(code, r1, r0);
    }

    public static void GenAdd(this IList<byte> code, byte r0, byte r1)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.ADD_B, r0);
      code.Add(r0);
      GenRegIdByRegId(code, r1, r0);
    }

    public static void GenSub(this IList<byte> code, byte r0, byte r1)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.SUB_B, r0);
      code.Add(r0);
      GenRegIdByRegId(code, r1, r0);
    }

    public static void GenMul(this IList<byte> code, byte r0, byte r1)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.MUL_B, r0);
      code.Add(r0);
      GenRegIdByRegId(code, r1, r0);
    }

    public static void GenDiv(this IList<byte> code, byte r0, byte r1)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.DIV_B, r0);
      code.Add(r0);
      GenRegIdByRegId(code, r1, r0);
    }

    public static void GenRem(this IList<byte> code, byte r0, byte r1)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.REM_B, r0);
      code.Add(r0);
      GenRegIdByRegId(code, r1, r0);
    }

    public static void GenSll(this IList<byte> code, byte r, uint imm)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.SRL_B, r);
      code.Add(r);
      GenImmByRegId(code, imm, r);
    }

    public static void GenSrl(this IList<byte> code, byte r, uint imm)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.SRL_B, r);
      code.Add(r);
      GenImmByRegId(code, imm, r);
    }

    public static void GenAnd(this IList<byte> code, byte r0, byte r1)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.AND_B, r0);
      code.Add(r0);
      GenRegIdByRegId(code, r1, r0);
    }

    public static void GenOr(this IList<byte> code, byte r0, byte r1)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.OR_B, r0);
      code.Add(r0);
      GenRegIdByRegId(code, r1, r0);
    }

    public static void GenXor(this IList<byte> code, byte r0, byte r1)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.XOR_B, r0);
      code.Add(r0);
      GenRegIdByRegId(code, r1, r0);
    }

    public static void GenCmp(this IList<byte> code, byte r0, byte r1)
    {
      GenOpcodeByRegId(code, (byte)OPCODE.CMP_B, r0);
      code.Add(r0);
      GenRegIdByRegId(code, r1, r0);
    }

    public static void GenJmp(this IList<byte> code, ushort addr)
    {
      code.Add((byte)OPCODE.JMP);
      code.Add((byte)(addr & 0xFF));
      code.Add((byte)(addr >> 8));
    }

    public static void GenJeq(this IList<byte> code, ushort addr)
    {
      code.Add((byte)OPCODE.JEQ);
      code.Add((byte)(addr & 0xFF));
      code.Add((byte)(addr >> 8));
    }

    public static void GenJne(this IList<byte> code, ushort addr)
    {
      code.Add((byte)OPCODE.JNE);
      code.Add((byte)(addr & 0xFF));
      code.Add((byte)(addr >> 8));
    }

    public static void GenJlt(this IList<byte> code, ushort addr)
    {
      code.Add((byte)OPCODE.JLT);
      code.Add((byte)(addr & 0xFF));
      code.Add((byte)(addr >> 8));
    }

    public static void GenSyscall(this IList<byte> code, ushort addr)
    {
      code.Add((byte)OPCODE.SYSCALL);
      code.Add((byte)(addr & 0xFF));
      code.Add((byte)(addr >> 8));
    }

    private static void GenOpcodeByRegId(IList<byte> code, byte baseOpcode, byte r)
    {
      switch (r % 4)
      {
        case 0:
          code.Add((byte)(baseOpcode + 2));
          return;
        case 1:
          code.Add((byte)(baseOpcode + 1));
          return;
        default:
          code.Add((byte)(baseOpcode + 0));
          return;
      }
    }

    private static void GenImmByRegId(IList<byte> code, uint imm, byte r)
    {
      switch (r % 4)
      {
        case 0:
          code.Add((byte)(imm & 0xFF));
          code.Add((byte)(imm >> 8));
          code.Add((byte)(imm >> 16));
          code.Add((byte)(imm >> 24));
          return;
        case 1:
          code.Add((byte)(imm & 0xFF));
          code.Add((byte)(imm >> 8));
          return;
        default:
          code.Add((byte)(imm & 0xFF));
          return;
      }
    }

    private static void GenRegIdByRegId(IList<byte> code, byte rid, byte r)
    {
      byte baseRid = (byte)(rid >> 2 << 2);
      switch (r % 4)
      {
        case 0:
          code.Add((byte)(baseRid + 0));
          return;
        case 1:
          code.Add((byte)(baseRid + 1));
          return;
        default:
          code.Add((byte)(baseRid + 3));
          return;
      }
    }
  }
}
