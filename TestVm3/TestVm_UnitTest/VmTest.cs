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
      Assert.IsTrue(vm.TryPushUi8(val8), "Push�Ɏ��s���܂���");
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VM�ŃG���[���������܂���\n{vm.Errors}");
      Assert.AreEqual(
          val8,
          (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8H)),
          "���W�X�^8H�Ɋi�[�ł��Ă��܂���");
      val8 = 0x12;
      vm = new Vm();
      code.Clear();
      code.Add((byte)OPCODE.LOAD_B);
      code.Add((byte)vm.RegType(3, Vm.REG_TYPE_8L));
      code.Add(0xFF);
      code.Add(0x0F);
      stream = new BytecodeStream(code.ToArray());
      vm.Init(stream);
      Assert.IsTrue(vm.TryPushUi8(val8), "Push�Ɏ��s���܂���");
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VM�ŃG���[���������܂���\n{vm.Errors}");
      Assert.AreEqual(
          val8,
          (byte)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_8L)),
          "���W�X�^8L�Ɋi�[�ł��Ă��܂���");
      #endregion
      // ushort
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
      Assert.IsTrue(vm.TryPushUi16(val16), "Push�Ɏ��s���܂���");
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VM�ŃG���[���������܂���\n{vm.Errors}");
      Assert.AreEqual(
          val16,
          (ushort)vm.GetReg(vm.RegType(3, Vm.REG_TYPE_16)),
          "���W�X�^16�Ɋi�[�ł��Ă��܂���");
      #endregion
      // uint
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
      Assert.IsTrue(vm.TryPushUi32(val32), "Push�Ɏ��s���܂���");
      vm.Run();
      Assert.IsFalse(vm.IsError, $"VM�ŃG���[���������܂���\n{vm.Errors}");
      Assert.AreEqual(
          val32,
          vm.GetReg(vm.RegType(3, Vm.REG_TYPE_32)),
          "���W�X�^32�Ɋi�[�ł��Ă��܂���");
      #endregion
    }
  }
}
