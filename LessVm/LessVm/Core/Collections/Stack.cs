using System.Text;

namespace LessVm.Core.Collections
{
  public class Stack<T>
  {
    private T[] _context = default;
    private int _ptr = 0;
    private int _size = 0;

    public T this[int idx]
    {
      get => _context[idx];
      set => _context[idx] = value;
    }

    public Stack(int size)
    {
      _context = new T[size];
      _ptr = size;
      _size = size;
    }

    public void Push(T value)
    {
      _context[--_ptr] = value;
    }

    public T Pop()
    {
      return _context[_ptr++];
    }

    public T Peek()
    {
      return _context[_ptr];
    }

    public T[] Pop(int size, bool reverse = false)
    {
      if (_ptr + size > _size) return null;
      T[] ret = new T[size];
      for(int i = 0; i < size; ++i)
      {
        int idx;
        if (reverse) idx = size - i - 1;
        else idx = i;
        ret[idx] = Pop();
      }
      return ret;
    }

    public bool IsEmpty() => _ptr == _size;

    public override string ToString()
    {
      StringBuilder builder = new StringBuilder();
      for(int i = _size - 1; i >= _ptr; --i)
      {
        builder.AppendLine($"{_context[i]}(0x{_context[i]:X8})");
      }
      return builder.ToString();
    }
  } // class
} // namespace
