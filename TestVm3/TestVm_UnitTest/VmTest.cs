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
    public void TestStore()
    {
      #region byte
      byte a8 = 0x12;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.STORE_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8H));
      code.Add(0x00);
      code.Add(0x00);
      BytecodeStream stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi8(vm.RegType(2, Vm.REG_TYPE_8H), a8);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          a8,
          vm.GetMemory8(0x00),
          "レジスタ8Hに格納できていません");

      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.STORE_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add(0x00);
      code.Add(0x00);
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi8(vm.RegType(2, Vm.REG_TYPE_8L), a8);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          a8,
          vm.GetMemory8(0x00),
          "レジスタ8Lに格納できていません");
      #endregion
      #region ushort
      ushort a16 = 0x1234;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.STORE_W);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_16));
      code.Add(0x00);
      code.Add(0x00);
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi16(vm.RegType(2, Vm.REG_TYPE_16), a16);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          a16,
          vm.GetMemory16(0x00),
          "レジスタ16に格納できていません");
      #endregion
      #region uint
      uint a32 = 0x12345678;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.STORE_D);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_32));
      code.Add(0x00);
      code.Add(0x00);
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi32(vm.RegType(2, Vm.REG_TYPE_32), a32);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
          a32,
          vm.GetMemory32(0x00),
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

      byte b8 = 0x13;
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
      Assert.IsTrue(
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
        "レジスタ32に格納できていません");
      Assert.AreEqual(
        a32,
        vm.GetReg(vm.RegType(3, Vm.REG_TYPE_32)),
        "レジスタ32に格納できていません");
      Assert.IsTrue(
        vm.IsZero,
        "うまく比較できていません");
      Assert.IsFalse(
        vm.IsSign,
        "うまく比較できていません");
      #endregion
    }

    [TestMethod]
    public void TestJeq()
    {
      #region Jump Address
      byte a8l = 0x12;
      byte b8l = 0x12;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      // CMP_B R2L, R3L
      code.Add((byte)OPCODE.CMP_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      // JEQ L1(0x09)
      code.Add((byte)OPCODE.JEQ);
      code.Add(0x09);
      code.Add(0x00);
      // SUB_B R2L, R2L
      code.Add((byte)OPCODE.SUB_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      // L1: SUB_B R3L, R3L
      code.Add((byte)OPCODE.SUB_B);
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      // Bytecode化
      BytecodeStream stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      // Register初期化
      vm.TrySetRegUi32(vm.RegType(2, Vm.REG_TYPE_32), a8l);
      vm.TrySetRegUi32(vm.RegType(3, Vm.REG_TYPE_32), b8l);
      // 実行
      vm.Run();
      // 結果の担保
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a8l,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      Assert.AreEqual(
        0x00,
        (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      #endregion
      #region Jump Not Address
      b8l = 0x13;
      vm = new Vm();
      // Bytecode.Positionを0に変更
      stream.Position = 0;
      vm.Init(stream);
      // Register初期化
      vm.TrySetRegUi32(vm.RegType(2, Vm.REG_TYPE_32), a8l);
      vm.TrySetRegUi32(vm.RegType(3, Vm.REG_TYPE_32), b8l);
      // 実行
      vm.Run();
      // 結果の担保
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        0x00,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      Assert.AreEqual(
        0x00,
        (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      #endregion
    }

    [TestMethod]
    public void TestMoveI()
    {
      #region byte
      byte a8 = 0x12;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.MOVE_I_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8H));
      code.Add(a8);
      BytecodeStream stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a8,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8H)),
        "レジスタ8Hに格納できていません");

      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.MOVE_I_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add(a8);
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a8,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8L)),
        "レジスタ8Lに格納できていません");
      #endregion
      #region ushort
      ushort a16 = 0x1234;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.MOVE_I_W);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_16));
      code.Add(0x34);
      code.Add(0x12);
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a16,
        (ushort)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_16)),
        "レジスタ16に格納できていません");
      #endregion
      #region uint
      uint a32 = 0x12345678;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.MOVE_I_D);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_32));
      code.Add(0x78);
      code.Add(0x56);
      code.Add(0x34);
      code.Add(0x12);
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a32,
        vm.GetReg(vm.RegType(2, Vm.REG_TYPE_32)),
        "レジスタ32に格納できていません");
      #endregion
    }

    [TestMethod]
    public void TestMove()
    {
      #region byte
      byte a8 = 0x12;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.MOVE_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8H));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8H));
      BytecodeStream stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi8(vm.RegType(2, Vm.REG_TYPE_8H), a8);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a8,
        (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8H)),
        "レジスタ8Hに格納できていません");

      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.MOVE_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi8(vm.RegType(2, Vm.REG_TYPE_8L), a8);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a8,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8L)),
        "レジスタ8Lに格納できていません");
      #endregion
      #region ushort
      ushort a16 = 0x1234;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.MOVE_W);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_16));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_16));
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi16(vm.RegType(2, Vm.REG_TYPE_16), a16);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a16,
        (ushort)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_16)),
        "レジスタ16に格納できていません");
      #endregion
      #region uint
      uint a32 = 0x12345678;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.MOVE_D);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_32));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_32));
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      vm.TrySetRegUi32(vm.RegType(2, Vm.REG_TYPE_32), a32);
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a32,
        vm.GetReg(vm.RegType(3, Vm.REG_TYPE_32)),
        "レジスタ32に格納できていません");
      #endregion
    }

    [TestMethod]
    public void TestSysCall()
    {
      #region context
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.SYSCALL);
      code.Add(0x00);
      code.Add(0x00);
      BytecodeStream stream = new BytecodeStream(code.ToArray());
      vm.Init(stream, new VmTestMethods());
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        (uint)0x12345678,
        vm.GetReg(vm.RegType(10, Vm.REG_TYPE_32)),
        "レジスタ32に格納できていません");
      #endregion
    }

    [TestMethod]
    public void TestAdd()
    {
      #region byte
      byte a8 = 0x12;
      byte b8 = 0x07;
      byte c8 = 0x19;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.ADD_B);
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
      c8 = 0x19;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.ADD_B);
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
      ushort c16 = 0x1999;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.ADD_W);
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
      uint c32 = 0x12345DDD;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.ADD_D);
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
    public void TestMul()
    {
      #region byte
      byte a8 = 0x12;
      byte b8 = 0x07;
      byte c8 = 0x7E;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.MUL_B);
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

      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.MUL_B);
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
      ushort c16 = 0x9A84;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.MUL_W);
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
      uint c32 = 0x9D036558;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.MUL_D);
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
    public void TestDiv()
    {
      #region byte
      byte a8 = 0x12;
      byte b8 = 0x07;
      byte c8 = 0x02;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.DIV_B);
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

      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.DIV_B);
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
      ushort c16 = 0x0002;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.DIV_W);
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
      uint c32 = 0x0002763D;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.DIV_D);
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
    public void TestRem()
    {
      #region byte
      byte a8 = 0x12;
      byte b8 = 0x07;
      byte c8 = 0x04;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.REM_B);
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

      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.REM_B);
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
      ushort c16 = 0x036A;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.REM_W);
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
      uint c32 = 0x00000567;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.REM_D);
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
    public void TestShift()
    {
      #region Left Shift
      #region byte
      byte a8 = 0x12;
      byte b8 = 0x03;
      byte c8 = 0x90;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.SLL_B);
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

      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.SLL_B);
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
      ushort b16 = 0x0003;
      ushort c16 = 0x91A0;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.SLL_W);
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
      uint b32 = 0x00000005;
      uint c32 = 0x468ACF00;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.SLL_D);
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
      #endregion
      #region Right Shift
      #region byte
      c8 = 0x02;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.SRL_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8H));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8H));
      stream = new BytecodeStream(code.ToArray());
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

      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.SRL_B);
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
      c16 = 0x0246;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.SRL_W);
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
      c32 = 0x0091A2B3;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.SRL_D);
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
      #endregion
    }

    [TestMethod]
    public void TestAnd()
    {
      #region byte
      byte a8 = 0x12;
      byte b8 = 0x07;
      byte c8 = 0x02;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.AND_B);
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

      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.AND_B);
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
      ushort c16 = 0x0224;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.AND_W);
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
      uint c32 = 0x00000660;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.AND_D);
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
    public void TestOr()
    {
      #region byte
      byte a8 = 0x12;
      byte b8 = 0x07;
      byte c8 = 0x17;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.OR_B);
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

      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.OR_B);
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
      ushort c16 = 0x1775;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.OR_W);
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
      uint c32 = 0x1234577D;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.OR_D);
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
    public void TestXor()
    {
      #region byte
      byte a8 = 0x12;
      byte b8 = 0x07;
      byte c8 = 0x15;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      code.Add((byte)OPCODE.XOR_B);
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

      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.XOR_B);
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
      ushort c16 = 0x1551;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.XOR_W);
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
      uint c32 = 0x1234511D;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.XOR_D);
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
    public void TestJmpAll()
    {
      #region JMP
      #region Jump Address
      byte a8l = 0x12;
      byte b8l = 0x12;
      Vm vm = new Vm();
      List<byte> code = new List<byte>();
      // CMP_B R2L, R3L
      code.Add((byte)OPCODE.CMP_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      // JMP L1(0x09)
      code.Add((byte)OPCODE.JMP);
      code.Add(0x09);
      code.Add(0x00);
      // SUB_B R2L, R2L
      code.Add((byte)OPCODE.SUB_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      // L1: SUB_B R3L, R3L
      code.Add((byte)OPCODE.SUB_B);
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      // Bytecode化
      BytecodeStream stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      // Register初期化
      vm.TrySetRegUi32(vm.RegType(2, Vm.REG_TYPE_32), a8l);
      vm.TrySetRegUi32(vm.RegType(3, Vm.REG_TYPE_32), b8l);
      // 実行
      vm.Run();
      // 結果の担保
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a8l,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      Assert.AreEqual(
        0x00,
        (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      #endregion
      #endregion
      #region JNE
      #region Jump Address
      a8l = 0x12;
      b8l = 0x13;
      vm = new Vm();
      code.Clear();
      // CMP_B R2L, R3L
      code.Add((byte)OPCODE.CMP_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      // JNE L1(0x09)
      code.Add((byte)OPCODE.JNE);
      code.Add(0x09);
      code.Add(0x00);
      // SUB_B R2L, R2L
      code.Add((byte)OPCODE.SUB_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      // L1: SUB_B R3L, R3L
      code.Add((byte)OPCODE.SUB_B);
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      // Bytecode化
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      // Register初期化
      vm.TrySetRegUi32(vm.RegType(2, Vm.REG_TYPE_32), a8l);
      vm.TrySetRegUi32(vm.RegType(3, Vm.REG_TYPE_32), b8l);
      // 実行
      vm.Run();
      // 結果の担保
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a8l,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      Assert.AreEqual(
        0x00,
        (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      #endregion
      #region Jump Not Address
      b8l = 0x12;
      vm = new Vm();
      // Bytecode.Positionを0に変更
      stream.Position = 0;
      vm.Init(stream);
      // Register初期化
      vm.TrySetRegUi32(vm.RegType(2, Vm.REG_TYPE_32), a8l);
      vm.TrySetRegUi32(vm.RegType(3, Vm.REG_TYPE_32), b8l);
      // 実行
      vm.Run();
      // 結果の担保
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        0x00,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      Assert.AreEqual(
        0x00,
        (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      #endregion
      #endregion
      #region JLT
      #region Jump Address
      a8l = 0x12;
      b8l = 0x13;
      vm = new Vm();
      code.Clear();
      // CMP_B R2L, R3L
      code.Add((byte)OPCODE.CMP_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      // JLT L1(0x09)
      code.Add((byte)OPCODE.JLT);
      code.Add(0x09);
      code.Add(0x00);
      // SUB_B R2L, R2L
      code.Add((byte)OPCODE.SUB_B);
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(2, Vm.REG_TYPE_8L));
      // L1: SUB_B R3L, R3L
      code.Add((byte)OPCODE.SUB_B);
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      // Bytecode化
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      // Register初期化
      vm.TrySetRegUi32(vm.RegType(2, Vm.REG_TYPE_32), a8l);
      vm.TrySetRegUi32(vm.RegType(3, Vm.REG_TYPE_32), b8l);
      // 実行
      vm.Run();
      // 結果の担保
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        a8l,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      Assert.AreEqual(
        0x00,
        (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      #endregion
      #region Jump Not Address
      b8l = 0x12;
      vm = new Vm();
      // Bytecode.Positionを0に変更
      stream.Position = 0;
      vm.Init(stream);
      // Register初期化
      vm.TrySetRegUi32(vm.RegType(2, Vm.REG_TYPE_32), a8l);
      vm.TrySetRegUi32(vm.RegType(3, Vm.REG_TYPE_32), b8l);
      // 実行
      vm.Run();
      // 結果の担保
      Assert.IsFalse(vm.IsError, $"VMでエラーが発生しました\n{vm.Errors}");
      Assert.AreEqual(
        0x00,
        (byte)vm.GetReg(vm.RegType(2, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      Assert.AreEqual(
        0x00,
        (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8L)),
        "レジスタ8に格納できていません");
      #endregion
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

  class VmTestMethods
  {
    /// <summary>
    /// Reg[10]に0x12345678に代入
    /// </summary>
    public static IEnumerator<int> Test(VmStatus status)
    {
      status.registers[10][0] = 0x12345678;
      yield return VmStatus.FINISH;
    }
  }
}
