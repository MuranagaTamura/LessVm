using TestVm.Core.Collections;

namespace TestVm.Core.Vm
{
  public class VmStatus
  {
    public const int FINISH = 0;
    public const int CONTINUE = 1;
    public const int HALT = 2;

    public bool isEnd = false;
    public bool isZero = false;
    public bool isSign = false;

    public MemoryObj memory = default;
    public Register[] registers = default;
  }
}
