namespace UniGitUtils
{
	/// <summary>
	/// <para>Git のコミットログを取得するオプションを管理する構造体</para>
	/// <para>Format には下記のフォーマットを指定できる</para>
	/// <para>・%h：コミットハッシュ（短縮版）</para>
	/// <para>・%cd：日付</para>
	/// <para>・%cn：コミットユーザー</para>
	/// <para>・%s：コミットコメント</para>
	/// </summary>
	public struct CommitLogOption
	{
		//================================================================================
		// プロパティ
		//================================================================================
		public int    Count      { get; } // 取得件数
		public bool   IsNoMerges { get; } // マージのログを除外する場合 true
		public string Format     { get; } // フォーマット

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CommitLogOption
		(
			int    count,
			bool   isNoMerges,
			string format
		)
		{
			Count      = count;
			IsNoMerges = isNoMerges;
			Format     = format;
		}
	}
}