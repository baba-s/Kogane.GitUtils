using System.IO;
using UnityEditor;

namespace UniGitUtils
{
	/// <summary>
	/// Git の情報を管理するスクリプトを生成するためのクラス
	/// </summary>
	public static class GitCodeGenerator
	{
		//================================================================================
		// 定数
		//================================================================================
		public const string BRANCH_NAME_TAG       = "#BRANCH_NAME#";       // ブランチ名置換対象のタグ
		public const string COMMIT_HASH_TAG       = "#COMMIT_HASH#";       // コミットハッシュ置換対象のタグ
		public const string SHORT_COMMIT_HASH_TAG = "#SHORT_COMMIT_HASH#"; // コミットハッシュ（短縮版）置換対象のタグ
		public const string COMMIT_LOG_TAG        = "#COMMIT_LOG#";        // コミットログ置換対象のタグ

		//================================================================================
		// 関数（static）
		//================================================================================
		/// <summary>
		/// Git の情報を管理するスクリプトを生成します
		/// </summary>
		/// <example>
		/// <code>
		/// var outputPath = "Assets/Scripts/GitInfo.cs";
		///
		/// var option = new CommitLogOption
		///	(
		///		count: 10,
		///		isNoMerges: false,
		///		format: "%h %cd %cn %s"
		/// );
		/// 
		/// var template = $@"public static class GitInfo
		/// {{
		///     public const string BRANCH_NAME       = ""{GitCodeGenerator.BRANCH_NAME_TAG}"";
		///     public const string COMMIT_HASH       = ""{GitCodeGenerator.COMMIT_HASH_TAG}"";
		///     public const string SHORT_COMMIT_HASH = ""{GitCodeGenerator.SHORT_COMMIT_HASH_TAG}"";
		///     public const string COMMIT_LOG_TAG    = @""{GitCodeGenerator.COMMIT_LOG_TAG}"";
		/// }}";
		///
		/// GitCommitHashCodeGenerator.Generate( outputPath, template, option );
		/// </code>
		/// </example>
		public static void Generate
		(
			string          outputPath,
			string          template,
			CommitLogOption commitLogOption
		)
		{
			// 出力先のフォルダが存在しない場合は作成します
			var dir = Path.GetDirectoryName( outputPath );

			if ( !Directory.Exists( dir ) )
			{
				Directory.CreateDirectory( dir );
			}

			// Git の情報を読み込みます
			var branchName      = GitUtils.LoadBranchName();
			var commitHash      = GitUtils.LoadCommitHash();
			var shortCommitHash = GitUtils.LoadShortCommitHash();
			var commitLog       = GitUtils.LoadCommitLog( commitLogOption );

			// Git の情報を埋め込んだスクリプトの文字列を作成します
			var code = template;

			code = code.Replace( BRANCH_NAME_TAG, branchName );
			code = code.Replace( COMMIT_HASH_TAG, commitHash );
			code = code.Replace( SHORT_COMMIT_HASH_TAG, shortCommitHash );
			code = code.Replace( COMMIT_LOG_TAG, commitLog );

			// 作成した文字列をスクリプトとして保存します
			File.WriteAllText( outputPath, code );
			AssetDatabase.Refresh();
		}

		public static void Generate
		(
			string outputPath,
			string template
		)
		{
			Generate( outputPath, template, default );
		}
	}
}