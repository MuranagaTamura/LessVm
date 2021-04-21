using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestVm.Core.Collections;

namespace TestVm_UnitTest
{
  [TestClass]
  public class TestMemory
  {
    [TestMethod]
    public void TryUi32Test1()
    {
      uint test = 0x12345678;
      MemoryObj memory = new MemoryObj(8);

      // 例外が起きず失敗できるか
      Assert.IsFalse(memory.TrySetUi32(10, 0), "設定できないのに，設定できてます");
      Assert.IsFalse(memory.TryGetUi32(10, out uint _), "取得できないのに，取得できてます");

      // 成功するか
      Assert.IsTrue(memory.TrySetUi32(0, 0), "設定できるのに，設定できてません");
      Assert.IsTrue(memory.TryGetUi32(0, out uint _), "取得できるのに，取得できてません");

      // 設定，取得できるか
      memory = new MemoryObj(8);
      uint result;
      if (!memory.TrySetUi32(0, test))
        Assert.Fail("設定に失敗しました");
      if (!memory.TryGetUi32(0, out result))
        Assert.Fail("取得に失敗しました");
      Assert.AreEqual(
          test,
          result,
          "取得はできたが，取得内容に差異があります");
    }

    [TestMethod]
    public void TryUi16Test1()
    {
      ushort test = 0x1234;
      MemoryObj memory = new MemoryObj(8);

      // 例外が起きず失敗できるか
      Assert.IsFalse(memory.TrySetUi16(10, 0), "設定できないのに，設定できてます");
      Assert.IsFalse(memory.TryGetUi16(10, out ushort _), "取得できないのに，取得できてます");

      // 成功するか
      Assert.IsTrue(memory.TrySetUi16(0, 0), "設定できるのに，設定できてません");
      Assert.IsTrue(memory.TryGetUi16(0, out ushort _), "取得できるのに，取得できてません");

      // 設定，取得できるか
      memory = new MemoryObj(8);
      ushort result;
      if (!memory.TrySetUi16(0, test))
        Assert.Fail("設定に失敗しました");
      if (!memory.TryGetUi16(0, out result))
        Assert.Fail("取得に失敗しました");
      Assert.AreEqual(
          test,
          result,
          "取得はできたが，取得内容に差異があります");
    }

    [TestMethod]
    public void TryUi8Test1()
    {
      byte test = 0x12;
      MemoryObj memory = new MemoryObj(8);

      // 例外が起きず失敗できるか
      Assert.IsFalse(memory.TrySetUi8(10, 0), "設定できないのに，設定できてます");
      Assert.IsFalse(memory.TryGetUi8(10, out byte _), "取得できないのに，取得できてます");

      // 成功するか
      Assert.IsTrue(memory.TrySetUi8(0, 0), "設定できるのに，設定できてません");
      Assert.IsTrue(memory.TryGetUi8(0, out byte _), "取得できるのに，取得できてません");

      // 設定，取得できるか
      memory = new MemoryObj(8);
      byte result;
      if (!memory.TrySetUi8(0, test))
        Assert.Fail("設定に失敗しました");
      if (!memory.TryGetUi8(0, out result))
        Assert.Fail("取得に失敗しました");
      Assert.AreEqual(
          test,
          result,
          "取得はできたが，取得内容に差異があります");
    }
  }
}
