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
        "ui32�t�B�[���h�̒l������������܂���");

      Assert.AreEqual(
        (ushort)0x5678,
        ctx.ui16,
        "ui16�t�B�[���h�̒l������������܂���");

      Assert.AreEqual(
        (byte)0x56,
        ctx.ui8h,
        "ui8h�t�B�[���h�̒l������������܂���");

      Assert.AreEqual(
        (byte)0x78,
        ctx.ui8l,
        "ui8l�t�B�[���h�̒l������������܂���");
    }

    [TestMethod]
    public void RegisterTest1()
    {
      Register reg = new Register();
      reg[0] = 0x12345678;

      Assert.AreEqual(
        (uint)0x12345678,
        reg[0],
        "reg[0]�̒l������������܂���");

      Assert.AreEqual(
        (uint)0x5678,
        reg[1],
        "reg[1]�̒l������������܂���");

      Assert.AreEqual(
        (uint)0x56,
        reg[2],
        "reg[2]�̒l������������܂���");

      Assert.AreEqual(
        (uint)0x78,
        reg[3],
        "reg[3]�̒l������������܂���");
    }
  }
}
