using System;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace UniGitUtils
{
	/// <summary>
	/// Git に関するメニューを管理するクラス
	/// </summary>
	public static class GitUtilsMenu
	{
		//================================================================================
		// 定数
		//================================================================================
		public const string ITEM_NAME_ROOT = "Assets/UniGitUtils/"; // Unity メニューのルート

		//================================================================================
		// イベント（static）
		//================================================================================
		/// <summary>
		/// <para>Unity メニューからスクリプトを生成する時に呼び出される関数</para>
		/// <para>Unity メニューからスクリプトを生成する処理を外部から登録できます</para>
		/// </summary>
		public static event Action OnForceGenerate;

		//================================================================================
		// 関数（static）
		//================================================================================
		/// <summary>
		/// Unity メニューからスクリプトを生成するための関数
		/// </summary>
		[MenuItem( ITEM_NAME_ROOT + "Git の情報を管理するスクリプトを生成" )]
		private static void ForceGenerate()
		{
			if ( OnForceGenerate == null )
			{
				Debug.LogWarning( "[UniGitUtils] Git の情報を管理するスクリプトを生成する処理が登録されていません" );
				return;
			}

			OnForceGenerate();
		}

		/// <summary>
		/// Unity メニューからブランチ名をログ出力して確認するための関数
		/// </summary>
		[MenuItem( ITEM_NAME_ROOT + "ブランチ名をログ出力" )]
		private static void LogBranchName()
		{
			Debug.Log( $"[UniGitUtils] {GitUtils.LoadBranchName()}" );
		}

		/// <summary>
		/// Unity メニューからコミットハッシュをログ出力して確認するための関数
		/// </summary>
		[MenuItem( ITEM_NAME_ROOT + "コミットハッシュをログ出力" )]
		private static void LogCommitHash()
		{
			Debug.Log( $"[UniGitUtils] {GitUtils.LoadCommitHash()}" );
		}

		/// <summary>
		/// Unity メニューからコミットハッシュ（短縮版）をログ出力して確認するための関数
		/// </summary>
		[MenuItem( ITEM_NAME_ROOT + "コミットハッシュ（短縮版）をログ出力" )]
		private static void LogShortCommitHash()
		{
			Debug.Log( $"[UniGitUtils] {GitUtils.LoadShortCommitHash()}" );
		}

		/// <summary>
		/// Unity メニューからコミットログをログ出力して確認するための関数
		/// </summary>
		[MenuItem( ITEM_NAME_ROOT + "コミットログをログ出力" )]
		private static void LogCommitLog()
		{
			var option = new CommitLogOption
			(
				count: 10,
				isNoMerges: false,
				format: "%h %cd %cn %s"
			);

			Debug.Log( $"[UniGitUtils]\n{GitUtils.LoadCommitLog( option )}" );
		}
	}
}