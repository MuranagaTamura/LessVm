using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestVm.ScriptHelper;

namespace TestVm_UnitTest
{
  [TestClass]
  public class ScriptHelperTest
  {
    [TestMethod]
    public void GenerateLoadTest()
    {
      // LOAD_D R1 0x1234
      List<byte> correct = new List<byte>()
      {
        (byte)OPCODE.LOAD_D,
        0x04,
        0x34,
        0x12,
      };
      
      // generate code
      List<byte> gen = new List<byte>();
      gen.GenLoad(0x04, 0x1234);

      // check
      _CheckCode(correct, gen);
    }

    [TestMethod]
    public void GenerateStoreTest()
    {
      // STORE_B 0x1234 R1
      List<byte> correct = new List<byte>()
      {
        (byte)OPCODE.STORE_B,
        0x34,
        0x12,
        0x07,
      };

      // generate code
      List<byte> gen = new List<byte>();
      gen.GenStore(0x1234, 0x07);

      // check
      _CheckCode(correct, gen);
    }

    [TestMethod]
    public void GenerateMoveI()
    {
      // MOVE_I_D R1 0x12345678
      List<byte> correct = new List<byte>()
      {
        (byte)OPCODE.MOVE_I_D,
        0x04,
        0x78,
        0x56,
        0x34,
        0x12,
      };

      // generate code
      List<byte> gen = new List<byte>();
      gen.GenMoveI(0x04, 0x12345678);

      // check
      _CheckCode(correct, gen);
    }

    [TestMethod]
    public void GenerateAdd()
    {
      // ADD_D R0 R1
      List<byte> correct = new List<byte>()
      {
        (byte)OPCODE.ADD_D,
        0x00,
        0x04,
      };

      // generate code
      List<byte> gen = new List<byte>();
      gen.GenAdd(0x00, 0x04);

      // check
      _CheckCode(correct, gen);
    }

    [TestMethod]
    public void GenerateRegId()
    {
      Assert.AreEqual(
        40,
        GenerateCode.GenRegId(10, RegType.u32),
        "RegIdの生成に失敗してます");
      Assert.AreEqual(
        1,
        GenerateCode.GenRegId(0, RegType.u16),
        "RegIdの生成に失敗してます");
      Assert.AreEqual(
        50,
        GenerateCode.GenRegId(12, RegType.u8h),
        "RegIdの生成に失敗してます");
      Assert.AreEqual(
        115,
        GenerateCode.GenRegId(28, RegType.u8l),
        "RegIdの生成に失敗してます");
    }

    private void _CheckCode(List<byte> correct, List<byte> gen)
    {
      Assert.AreEqual(
        correct.Count,
        gen.Count,
        "サイズが違います");
      for (int i = 0; i < correct.Count; ++i)
        Assert.AreEqual(
            correct[i],
            gen[i],
            "生成されたコードが違います");
    }
  }
}
