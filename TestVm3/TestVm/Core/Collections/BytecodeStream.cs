using System;
using System.IO;

namespace TestVm.Core.Collections
{
  public class BytecodeStream
  {
    private byte[] _data = null;
    private int _position = -1;

    public int Position
    {
      get => _position;
      set
      {
        if (value > Size) throw new IndexOutOfRangeException();
        _position = value;
      }
    }

    public long Size { get; private set; } = -1;

    public bool IsEnd
    {
      get
      {
        return Size <= Position;
      }
    }

    public BytecodeStream(string filepath)
    {
      using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
      {
        _data = new byte[fs.Length];
        Size = fs.Length;
        _position = 0;

        fs.Read(_data, _position, _data.Length);
      }
    }

    public BytecodeStream(byte[] rawCode)
    {
      _data = new byte[rawCode.Length];
      Size = rawCode.Length;
      _position = 0;

      rawCode.CopyTo(_data, 0);
    }

    public byte ReadByte()
    {
      if (_position >= _data.Length) return 0;
      return _data[_position++];
    }

    public int ReadInt()
    {
      byte[] intBuf = new byte[4];
      Read(intBuf, Position, 4);
      Position += 4;
      return BitConverter.ToInt32(intBuf, 0);
    }

    public char ReadChar()
    {
      byte[] ushartBuf = new byte[2];
      Read(ushartBuf, Position, 2);
      Position += 2;
      return BitConverter.ToChar(ushartBuf, 0);
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      if (offset < 0) throw new ArgumentOutOfRangeException("offsetが負の値です");
      int size = count;
      if(offset + count >= _data.Length)
      {
        size = _data.Length - offset;
      }
      for(int i = 0; i < size; ++i)
      {
        buffer[i] = _data[offset + i];
      }
      return size;
    }

    public bool TrySetPosition(int position)
    {
      if (position > Size || position < 0) return false;
      Position = position;
      return true;
    }
  } // class
} // namespace