namespace TestVm.Core.Vm
{
  public enum OPCODE : byte
  {
    #region LOAD
    /// <summary>
    /// 内容:
    ///   メモリからレジスタに格納（byte）
    /// フォーマット:
    ///   LOAD_B(u8) R(u8) Mem(u16)
    /// </summary>
    LOAD_B = 1,
    /// <summary>
    /// 内容:
    ///   メモリからレジスタに格納（ushort）
    /// フォーマット:
    ///   LOAD_W(u8) R(u8) Mem(u16)
    /// </summary>
    LOAD_W,
    /// <summary>
    /// 内容:
    ///   メモリからレジスタに格納（uint）
    /// フォーマット:
    ///   LOAD_D(u8) R(u8) Mem(u16)
    /// </summary>
    LOAD_D,
    #endregion

    #region MOVE
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（byte）
    /// フォーマット:
    ///   MOVE_I_B(u8) R(u8) Imm(u8)
    /// </summary>
    MOVE_I_B,
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（ushort）
    /// フォーマット:
    ///   MOVE_I_W(u8) R(u8) Imm(u16)
    /// </summary>
    MOVE_I_W,
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（uint）
    /// フォーマット:
    ///   MOVE_I_D(u8) R(u8) Imm(u32)
    /// </summary>
    MOVE_I_D,
    #endregion

  } // enum
} // namespace
