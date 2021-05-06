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
    /// <summary>
    /// 内容:
    ///   レジスタからメモリに格納（byte）
    /// フォーマット:
    ///   STORE_B(u8) R(u8) Mem(u16)
    /// </summary>
    STORE_B,
    /// <summary>
    /// 内容:
    ///   レジスタからメモリに格納（ushort）
    /// フォーマット:
    ///   STORE_W(u8) R(u8) Mem(u16)
    /// </summary>
    STORE_W,
    /// <summary>
    /// 内容:
    ///   レジスタからメモリに格納（uint）
    /// フォーマット:
    ///   STORE_D(u8) R(u8) Mem(u16)
    /// </summary>
    STORE_D,
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
    ///   レジスタからレジスタに格納（byte）
    /// フォーマット:
    ///   MOVE_B(u8) R(u8) R(u8)
    /// </summary>
    MOVE_B,
    /// <summary>
    /// 内容:
    ///   レジスタからレジスタに格納（ushort）
    /// フォーマット:
    ///   MOVE_W(u8) R(u8) R(u8)
    /// </summary>
    MOVE_W,
    /// <summary>
    /// 内容:
    ///   レジスタからレジスタに格納（uint）
    /// フォーマット:
    ///   MOVE_D(u8) R(u8) R(u8)
    /// </summary>
    MOVE_D,
    #endregion

    // 算術
    #region ADD
    /// <summary>
    /// 内容:
    ///   レジスタ同士の加算結果をレジスタに格納（byte）
    /// フォーマット:
    ///   ADD_B(u8) R(u8) R(u8)
    /// </summary>
    ADD_B,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の加算結果をレジスタに格納（ushort）
    /// フォーマット:
    ///   ADD_W(u8) R(u8) R(u8)
    /// </summary>
    ADD_W,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の加算結果をレジスタに格納（uint）
    /// フォーマット:
    ///   ADD_D(u8) R(u8) R(u8)
    /// </summary>
    ADD_D,
    #endregion
    #region SUB
    /// <summary>
    /// 内容:
    ///   レジスタ同士の減算結果をレジスタに格納（byte）
    /// フォーマット:
    ///   SUB_B(u8) R(u8) R(u8)
    /// </summary>
    SUB_B,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の減算結果をレジスタに格納（ushort）
    /// フォーマット:
    ///   SUB_W(u8) R(u8) R(u8)
    /// </summary>
    SUB_W,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の減算結果をレジスタに格納（uint）
    /// フォーマット:
    ///   SUB_D(u8) R(u8) R(u8)
    /// </summary>
    SUB_D,
    #endregion
    #region MUL
    /// <summary>
    /// 内容:
    ///   レジスタ同士の乗算結果をレジスタに格納（byte）
    /// フォーマット:
    ///   MUL_B(u8) R(u8) R(u8)
    /// </summary>
    MUL_B,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の乗算結果をレジスタに格納（ushort）
    /// フォーマット:
    ///   MUL_W(u8) R(u8) R(u8)
    /// </summary>
    MUL_W,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の乗算結果をレジスタに格納（uint）
    /// フォーマット:
    ///   MUL_D(u8) R(u8) R(u8)
    /// </summary>
    MUL_D,
    #endregion
    #region DIV
    /// <summary>
    /// 内容:
    ///   レジスタ同士の除算結果をレジスタに格納（byte）
    /// フォーマット:
    ///   DIV_B(u8) R(u8) R(u8)
    /// </summary>
    DIV_B,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の除算結果をレジスタに格納（ushort）
    /// フォーマット:
    ///   DIV_W(u8) R(u8) R(u8)
    /// </summary>
    DIV_W,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の除算結果をレジスタに格納（uint）
    /// フォーマット:
    ///   DIV_D(u8) R(u8) R(u8)
    /// </summary>
    DIV_D,
    #endregion
    #region REM
    /// <summary>
    /// 内容:
    ///   レジスタ同士の剰余結果をレジスタに格納（byte）
    /// フォーマット:
    ///   REM_B(u8) R(u8) R(u8)
    /// </summary>
    REM_B,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の剰余結果をレジスタに格納（ushort）
    /// フォーマット:
    ///   REM_W(u8) R(u8) R(u8)
    /// </summary>
    REM_W,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の剰余結果をレジスタに格納（uint）
    /// フォーマット:
    ///   REM_D(u8) R(u8) R(u8)
    /// </summary>
    REM_D,
    #endregion
    #region SLL
    /// <summary>
    /// 内容:
    ///   レジスタの値を定数で左シフト結果をレジスタに格納（byte）
    /// フォーマット:
    ///   SLL_B(u8) R(u8) Imm(u8)
    /// </summary>
    SLL_B,
    /// <summary>
    /// 内容:
    ///   レジスタの値を定数で左シフト結果をレジスタに格納（ushort）
    /// フォーマット:
    ///   SLL_W(u8) R(u8) Imm(u8)
    /// </summary>
    SLL_W,
    /// <summary>
    /// 内容:
    ///   レジスタの値を定数で左シフト結果をレジスタに格納（uint）
    /// フォーマット:
    ///   SLL_D(u8) R(u8) Imm(u16)
    /// </summary>
    SLL_D,
    #endregion
    #region SRL
    /// <summary>
    /// 内容:
    ///   レジスタの値を定数で右シフト結果をレジスタに格納（byte）
    /// フォーマット:
    ///   SRL_B(u8) R(u8) Imm(u8)
    /// </summary>
    SRL_B,
    /// <summary>
    /// 内容:
    ///   レジスタの値を定数で右シフト結果をレジスタに格納（ushort）
    /// フォーマット:
    ///   SRL_W(u8) R(u8) Imm(u8)
    /// </summary>
    SRL_W,
    /// <summary>
    /// 内容:
    ///   レジスタの値を定数で右シフト結果をレジスタに格納（uint）
    /// フォーマット:
    ///   SRL_D(u8) R(u8) Imm(u16)
    /// </summary>
    SRL_D,
    #endregion
    #region AND
    /// <summary>
    /// 内容:
    ///   レジスタ同士の論理積結果をレジスタに格納（byte）
    /// フォーマット:
    ///   AND_B(u8) R(u8) R(u8)
    /// </summary>
    AND_B,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の論理積結果をレジスタに格納（ushort）
    /// フォーマット:
    ///   AND_W(u8) R(u8) R(u8)
    /// </summary>
    AND_W,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の論理積結果をレジスタに格納（uint）
    /// フォーマット:
    ///   AND_D(u8) R(u8) R(u8)
    /// </summary>
    AND_D,
    #endregion
    #region OR
    /// <summary>
    /// 内容:
    ///   レジスタ同士の論理和結果をレジスタに格納（byte）
    /// フォーマット:
    ///   OR_B(u8) R(u8) R(u8)
    /// </summary>
    OR_B,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の論理和結果をレジスタに格納（ushort）
    /// フォーマット:
    ///   OR_W(u8) R(u8) R(u8)
    /// </summary>
    OR_W,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の論理和結果をレジスタに格納（uint）
    /// フォーマット:
    ///   OR_D(u8) R(u8) R(u8)
    /// </summary>
    OR_D,
    #endregion
    #region XOR
    /// <summary>
    /// 内容:
    ///   レジスタ同士の排他的論理和結果をレジスタに格納（byte）
    /// フォーマット:
    ///   XOR_B(u8) R(u8) R(u8)
    /// </summary>
    XOR_B,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の排他的論理和結果をレジスタに格納（ushort）
    /// フォーマット:
    ///   OR_W(u8) R(u8) R(u8)
    /// </summary>
    XOR_W,
    /// <summary>
    /// 内容:
    ///   レジスタ同士の排他的論理和結果をレジスタに格納（uint）
    /// フォーマット:
    ///   OR_D(u8) R(u8) R(u8)
    /// </summary>
    XOR_D,
    #endregion

    // フラグレジスタ設定
    #region CMP
    /// <summary>
    /// 内容:
    ///   レジスタ同士を比較結果をフラグに設定（byte）
    /// フォーマット:
    ///   CMP_B(u8) R(u8) R(u8)
    /// </summary>
    CMP_B,
    /// <summary>
    /// 内容:
    ///   レジスタ同士を比較結果をフラグに設定（ushort）
    /// フォーマット:
    ///   CMP_W(u8) R(u8) R(u8)
    /// </summary>
    CMP_W,
    /// <summary>
    /// 内容:
    ///   レジスタ同士を比較結果をフラグに設定（uint）
    /// フォーマット:
    ///   CMP_D(u8) R(u8) R(u8)
    /// </summary>
    CMP_D,
    #endregion

    // 実行位置変更
    #region JMP
    /// <summary>
    /// 内容:
    ///   即値にジャンプ
    /// フォーマット:
    ///   JMP(u8) Imm(u16)
    /// </summary>
    JMP,
    #endregion
    #region JEQ
    /// <summary>
    /// 内容:
    ///   Zero=1なら即値にジャンプ
    /// フォーマット:
    ///   JEQ(u8) Imm(u16)
    /// </summary>
    JEQ,
    #endregion
    #region JNE
    /// <summary>
    /// 内容:
    ///   Zero=0なら即値にジャンプ
    /// フォーマット:
    ///   JNE(u8) Imm(u16)
    /// </summary>
    JNE,
    #endregion
    #region JLT
    /// <summary>
    /// 内容:
    ///   Sign=1かつZero=0なら即値にジャンプ
    /// フォーマット:
    ///   JLT(u8) Imm(u16)
    /// </summary>
    JLT,
    #endregion

    // システムコール
    #region SYSCALL
    /// <summary>
    /// 内容:
    ///   エンジン側で実装されている関数を実行する
    /// フォーマット:
    ///   SYSCALL(u8) Imm(u16)
    /// </summary>
    SYSCALL,
    #endregion
  } // enum
} // namespace
