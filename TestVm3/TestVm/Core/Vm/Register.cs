using System;
using System.Runtime.InteropServices;

namespace TestVm.Core.Vm
{
  [StructLayout(LayoutKind.Explicit)]
  public struct RegisterContext
  {
    [FieldOffset(0)]
    public uint ui32;
    [FieldOffset(0)]
    public ushort ui16;
    [FieldOffset(1)]
    public byte ui8h;
    [FieldOffset(0)]
    public byte ui8l;
  }

  public class Register
  {
    const int REG_MAX_OFFSET = 4;
    const int REG_UI32 = 0;
    const int REG_UI16 = 1;
    const int REG_UI8H = 2;
    const int REG_UI8L = 3;

    RegisterContext _context = new RegisterContext();

    public uint this[int idx]
    {
      get
      {
        switch(idx % REG_MAX_OFFSET)
        {
          case REG_UI32: return _context.ui32;
          case REG_UI16: return _context.ui16;
          case REG_UI8H: return _context.ui8h;
          case REG_UI8L: return _context.ui8l;
          default: throw new ArgumentOutOfRangeException();
        }
      }
      set
      {
        switch (idx % REG_MAX_OFFSET)
        {
          case REG_UI32:
            _context.ui32 = value; 
            break;
          case REG_UI16: 
            _context.ui16 = (ushort)value;
            break;
          case REG_UI8H: 
            _context.ui8h = (byte)value;
            break;
          case REG_UI8L: 
            _context.ui8l = (byte)value;
            break;
          default: 
            throw new ArgumentOutOfRangeException();
        }
      }
    }
  }
}
