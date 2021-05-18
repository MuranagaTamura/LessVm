using Microsoft.VisualStudio.TestTools.UnitTesting;
using LessVm.Core.Vm;

namespace LessVm_UnitTest
{
  [TestClass]
  public class RegisterTest
  {
    [TestMethod]
    public void UnionTest1()
    {
      RegisterContext ctx = new RegisterContext();
      ctx.ui32 = 0x12345678;

      Assert.AreEqual(
        (uint)0x12345678,
        ctx.ui32,
        "ui32フィールドの値が正しくありません");

      Assert.AreEqual(
        (ushort)0x5678,
        ctx.ui16,
        "ui16フィールドの値が正しくありません");

      Assert.AreEqual(
        (byte)0x56,
        ctx.ui8h,
        "ui8hフィールドの値が正しくありません");

      Assert.AreEqual(
        (byte)0x78,
        ctx.ui8l,
        "ui8lフィールドの値が正しくありません");
    }

    [TestMethod]
    public void RegisterTest1()
    {
      Register reg = new Register();
      reg[0] = 0x12345678;

      Assert.AreEqual(
        (uint)0x12345678,
        reg[0],
        "reg[0]の値が正しくありません");

      Assert.AreEqual(
        (uint)0x5678,
        reg[1],
        "reg[1]の値が正しくありません");

      Assert.AreEqual(
        (uint)0x56,
        reg[2],
        "reg[2]の値が正しくありません");

      Assert.AreEqual(
        (uint)0x78,
        reg[3],
        "reg[3]の値が正しくありません");
    }
  }
}
