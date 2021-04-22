namespace TestVm.Core.Vm
{
  public enum OPCODE : byte
  {
    // メモリアクセス
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
    #region STORE
    #endregion

    // レジスタ間アクセス
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
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（byte）
    /// フォーマット:
    ///   MOVE_B(u8) R(u8) R(u8)
    /// </summary>
    MOVE_B,
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（ushort）
    /// フォーマット:
    ///   MOVE_W(u8) R(u8) R(u8)
    /// </summary>
    MOVE_W,
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（uint）
    /// フォーマット:
    ///   MOVE_D(u8) R(u8) R(u8)
    /// </summary>
    MOVE_D,
    #endregion

    // 算術
    #region ADD
    #endregion
    #region SUB
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（byte）
    /// フォーマット:
    ///   SUB_B(u8) R(u8) R(u8)
    /// </summary>
    SUB_B,
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（ushort）
    /// フォーマット:
    ///   SUB_W(u8) R(u8) R(u8)
    /// </summary>
    SUB_W,
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（uint）
    /// フォーマット:
    ///   SUB_D(u8) R(u8) R(u8)
    /// </summary>
    SUB_D,
    #endregion
    #region MUL
    #endregion
    #region DIV
    #endregion
    #region REM
    #endregion
    #region SLL
    #endregion
    #region SRL
    #endregion
    #region AND
    #endregion
    #region OR
    #endregion
    #region XOR
    #endregion

    // フラグレジスタ設定
    #region CMP
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（byte）
    /// フォーマット:
    ///   CMP_B(u8) R(u8) R(u8)
    /// </summary>
    CMP_B,
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（ushort）
    /// フォーマット:
    ///   CMP_W(u8) R(u8) R(u8)
    /// </summary>
    CMP_W,
    /// <summary>
    /// 内容:
    ///   即値からレジスタに格納（uint）
    /// フォーマット:
    ///   CMP_D(u8) R(u8) R(u8)
    /// </summary>
    CMP_D,
    #endregion

    // 実行位置変更
    #region JMP
    #endregion
    #region JEQ
    #endregion
    #region JNE
    #endregion
    #region JLT
    #endregion

    // システムコール
    #region SYSCALL
    #endregion

  } // enum
} // namespace
