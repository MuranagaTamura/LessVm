namespace TestVm.Core.Collections
{
  public class MemoryObj
  {
    private byte[] _data = null;
    private int _size = default;

    public MemoryObj(int size)
    {
      _data = new byte[size];
      _size = size;
    }

    public bool TryGetUi32(int ptr, out uint result)
    {
      result = 0;
      if (ptr < 0 | _size <= ptr) return false;
      if (_size <= ptr + 4) return false;
      for (int i = 0; i < 4; ++i)
        result |= (uint)_data[ptr + i] << (i << 3);
      return true;
    }

    public bool TrySetUi32(int ptr, uint value)
    {
      if (ptr < 0 | _size <= ptr) return false;
      if (_size <= ptr + 4) return false;
      for (int i = 0; i < 4; ++i)
        _data[ptr + i] = GetByte(value, i);
      return true;
    }

    public bool TryGetUi16(int ptr, out ushort result)
    {
      result = 0;
      if (ptr < 0 | _size <= ptr) return false;
      if (_size <= ptr + 2) return false;
      for (int i = 0; i < 2; ++i)
        result |= (ushort)(_data[ptr + i] << (i << 3));
      return true;
    }

    public bool TrySetUi16(int ptr, ushort value)
    {
      if (ptr < 0 | _size <= ptr) return false;
      if (_size <= ptr + 2) return false;
      for (int i = 0; i < 2; ++i)
        _data[ptr + i] = GetByte(value, i);
      return true;
    }

    public bool TryGetUi8(int ptr, out byte result)
    {
      result = 0;
      if (ptr < 0 | _size <= ptr) return false;
      result |= _data[ptr];
      return true;
    }

    public bool TrySetUi8(int ptr, byte value)
    {
      if (ptr < 0 | _size <= ptr) return false;
      _data[ptr] = value;
      return true;
    }

    private byte GetByte(uint value, int offset)
      => (byte)((value & 0xFF << (offset << 3)) >> (offset << 3));
  }
}
