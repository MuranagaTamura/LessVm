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

      // ��O���N�������s�ł��邩
      Assert.IsFalse(memory.TrySetUi32(10, 0), "�ݒ�ł��Ȃ��̂ɁC�ݒ�ł��Ă܂�");
      Assert.IsFalse(memory.TryGetUi32(10, out uint _), "�擾�ł��Ȃ��̂ɁC�擾�ł��Ă܂�");

      // �������邩
      Assert.IsTrue(memory.TrySetUi32(0, 0), "�ݒ�ł���̂ɁC�ݒ�ł��Ă܂���");
      Assert.IsTrue(memory.TryGetUi32(0, out uint _), "�擾�ł���̂ɁC�擾�ł��Ă܂���");

      // �ݒ�C�擾�ł��邩
      memory = new MemoryObj(8);
      uint result;
      if (!memory.TrySetUi32(0, test))
        Assert.Fail("�ݒ�Ɏ��s���܂���");
      if (!memory.TryGetUi32(0, out result))
        Assert.Fail("�擾�Ɏ��s���܂���");
      Assert.AreEqual(
          test,
          result,
          "�擾�͂ł������C�擾���e�ɍ��ق�����܂�");
    }

    [TestMethod]
    public void TryUi16Test1()
    {
      ushort test = 0x1234;
      MemoryObj memory = new MemoryObj(8);

      // ��O���N�������s�ł��邩
      Assert.IsFalse(memory.TrySetUi16(10, 0), "�ݒ�ł��Ȃ��̂ɁC�ݒ�ł��Ă܂�");
      Assert.IsFalse(memory.TryGetUi16(10, out ushort _), "�擾�ł��Ȃ��̂ɁC�擾�ł��Ă܂�");

      // �������邩
      Assert.IsTrue(memory.TrySetUi16(0, 0), "�ݒ�ł���̂ɁC�ݒ�ł��Ă܂���");
      Assert.IsTrue(memory.TryGetUi16(0, out ushort _), "�擾�ł���̂ɁC�擾�ł��Ă܂���");

      // �ݒ�C�擾�ł��邩
      memory = new MemoryObj(8);
      ushort result;
      if (!memory.TrySetUi16(0, test))
        Assert.Fail("�ݒ�Ɏ��s���܂���");
      if (!memory.TryGetUi16(0, out result))
        Assert.Fail("�擾�Ɏ��s���܂���");
      Assert.AreEqual(
          test,
          result,
          "�擾�͂ł������C�擾���e�ɍ��ق�����܂�");
    }

    [TestMethod]
    public void TryUi8Test1()
    {
      byte test = 0x12;
      MemoryObj memory = new MemoryObj(8);

      // ��O���N�������s�ł��邩
      Assert.IsFalse(memory.TrySetUi8(10, 0), "�ݒ�ł��Ȃ��̂ɁC�ݒ�ł��Ă܂�");
      Assert.IsFalse(memory.TryGetUi8(10, out byte _), "�擾�ł��Ȃ��̂ɁC�擾�ł��Ă܂�");

      // �������邩
      Assert.IsTrue(memory.TrySetUi8(0, 0), "�ݒ�ł���̂ɁC�ݒ�ł��Ă܂���");
      Assert.IsTrue(memory.TryGetUi8(0, out byte _), "�擾�ł���̂ɁC�擾�ł��Ă܂���");

      // �ݒ�C�擾�ł��邩
      memory = new MemoryObj(8);
      byte result;
      if (!memory.TrySetUi8(0, test))
        Assert.Fail("�ݒ�Ɏ��s���܂���");
      if (!memory.TryGetUi8(0, out result))
        Assert.Fail("�擾�Ɏ��s���܂���");
      Assert.AreEqual(
          test,
          result,
          "�擾�͂ł������C�擾���e�ɍ��ق�����܂�");
    }
  }
}
