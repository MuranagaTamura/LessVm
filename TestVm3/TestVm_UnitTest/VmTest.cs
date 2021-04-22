using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestVm.Core.Vm;
using TestVm.Core.Collections;
using System.Collections.Generic;

namespace TestVm_UnitTest
{
  [TestClass]
  public class VmTest
  {
    [TestMethod]
    public void TestLoad()
    {
      #region byte
      byte val8 = 0x12;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.LOAD_B);
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8H));
      code.Add(0xFF);
      code.Add(0x0F);
      BytecodeStream stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      Assert.IsTrue(vm.TryPushUi8(val8), "Pushに失敗しました");
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          val8,
          (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8H)),
          "レジスタ8Hに格納できていません");
      val8 = 0x12;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.LOAD_B);
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      code.Add(0xFF);
      code.Add(0x0F);
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      Assert.IsTrue(vm.TryPushUi8(val8), "Pushに失敗しました");
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          val8,
          (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8L)),
          "レジスタ8Lに格納できていません");
      #endregion
      #region ushort
      ushort val16 = 0x1234;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.LOAD_W);
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_16));
      code.Add(0xFD);
      code.Add(0x0F);
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      Assert.IsTrue(vm.TryPushUi16(val16), "Pushに失敗しました");
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          val16,
          (ushort)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_16)),
          "レジスタ16に格納できていません");
      #endregion
      #region uint
      uint val32 = 0x12345678;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.LOAD_D);
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_32));
      code.Add(0xFB);
      code.Add(0x0F);
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      Assert.IsTrue(vm.TryPushUi32(val32), "Pushに失敗しました");
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          val32,
          vm.GetReg(vm.RegType(3, Vm.REG_TYPE_32)),
          "レジスタ32に格納できていません");
      #endregion
    }

    [TestMethod]
    public void TestSub()
    {
      #region byte
      byte a8 = 0x12;
      byte b8 = 0x07;
      byte c8 = 0x0B;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.SUB_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8H));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8H));
      BytecodeStream stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi8(vm.RegType(2, Vm.REG_TYPE_8H), a8);
      vm.TrySetRegUi8(vm.RegType(3, Vm.REG_TYPE_8H), b8);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          b8,
          (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8H)),
          "レジスタ8Hに格納できていません");
      Assert.AreEqual(
          c8,
          (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8H)),
          "レジスタ8Hに格納できていません");

      a8 = 0x12;
      b8 = 0x07;
      c8 = 0x0B;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.SUB_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi8(vm.RegType(2, Vm.REG_TYPE_8L), a8);
      vm.TrySetRegUi8(vm.RegType(3, Vm.REG_TYPE_8L), b8);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          b8,
          (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8L)),
          "レジスタ8Lに格納できていません");
      Assert.AreEqual(
          c8,
          (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8L)),
          "レジスタ8Lに格納できていません");
      #endregion
      #region ushort
      ushort a16 = 0x1234;
      ushort b16 = 0x0765;
      ushort c16 = 0x0ACF;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.SUB_W);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_16));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_16));
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi16(vm.RegType(2, Vm.REG_TYPE_16), a16);
      vm.TrySetRegUi16(vm.RegType(3, Vm.REG_TYPE_16), b16);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          b16,
          (ushort)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_16)),
          "レジスタ16に格納できていません");
      Assert.AreEqual(
          c16,
          (ushort)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_16)),
          "レジスタ16に格納できていません");
      #endregion
      #region uint
      uint a32 = 0x12345678;
      uint b32 = 0x00000765;
      uint c32 = 0x12344F13;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.SUB_D);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_32));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_32));
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi32(vm.RegType(2, Vm.REG_TYPE_32), a32);
      vm.TrySetRegUi32(vm.RegType(3, Vm.REG_TYPE_32), b32);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          b32,
          vm.GetReg(vm.RegType(3, Vm.REG_TYPE_32)),
          "レジスタ32に格納できていません");
      Assert.AreEqual(
          c32,
          vm.GetReg(vm.RegType(2, Vm.REG_TYPE_32)),
          "レジスタ32に格納できていません");
      #endregion
    }

    [TestMethod]
    public void TestCmp()
    {
      #region byte
      byte a8 = 0x12;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.CMP_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8H));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8H));
      BytecodeStream stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi8(vm.RegType(2, Vm.REG_TYPE_8H), a8);
      vm.TrySetRegUi8(vm.RegType(3, Vm.REG_TYPE_8H), a8);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a8,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8H)),
        "レジスタ8Hに格納できていません");
      Assert.AreEqual(
        a8,
        (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8H)),
        "レジスタ8Hに格納できていません");
      Assert.IsTrue(
        vm.IsZero,
        "うまく比較できていません");
      Assert.IsFalse(
        vm.IsSign,
        "うまく比較できていません");

      a8 = 0x12;
      byte b8 = 0x07;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.CMP_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi8(vm.RegType(2, Vm.REG_TYPE_8L), a8);
      vm.TrySetRegUi8(vm.RegType(3, Vm.REG_TYPE_8L), b8);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a8,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8L)),
        "レジスタ8Lに格納できていません");
      Assert.AreEqual(
        b8,
        (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8L)),
        "レジスタ8Lに格納できていません");
      Assert.IsFalse(
        vm.IsZero,
        "うまく比較できていません");
      Assert.IsFalse(
        vm.IsSign,
        "うまく比較できていません");
      #endregion
      #region ushort
      ushort a16 = 0x1234;
      ushort b16 = 0x8765;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.CMP_W);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_16));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_16));
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi16(vm.RegType(2, Vm.REG_TYPE_16), a16);
      vm.TrySetRegUi16(vm.RegType(3, Vm.REG_TYPE_16), b16);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a16,
        (ushort)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_16)),
        "レジスタ16に格納できていません");
      Assert.AreEqual(
        b16,
        (ushort)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_16)),
        "レジスタ16に格納できていません");
      Assert.IsFalse(
        vm.IsZero,
        "うまく比較できていません");
      Assert.IsTrue(
        vm.IsSign,
        "うまく比較できていません");
      #endregion
      #region uint
      uint a32 = 0x12345678;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.CMP_D);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_32));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_32));
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi32(vm.RegType(2, Vm.REG_TYPE_32), a32);
      vm.TrySetRegUi32(vm.RegType(3, Vm.REG_TYPE_32), a32);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a32,
        vm.GetReg(vm.RegType(2, Vm.REG_TYPE_32)),
        "レジスタ16に格納できていません");
      Assert.AreEqual(
        a32,
        vm.GetReg(vm.RegType(3, Vm.REG_TYPE_32)),
        "レジスタ16に格納できていません");
      Assert.IsTrue(
        vm.IsZero,
        "うまく比較できていません");
      Assert.IsFalse(
        vm.IsSign,
        "うまく比較できていません");
      #endregion
    }
  }

  public static class VmEx
  {
    public static uint GetReg(this Vm vm, int id)
    {
      uint ret;
      if (!vm.TryGetReg(id, out ret))
        Assert.Fail("レジスタの値取得に失敗");
      return ret;
    }
  }
}
