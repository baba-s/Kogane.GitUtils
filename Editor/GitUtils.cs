using System.Diagnostics;
using System.Text;

namespace Kogane
{
    /// <summary>
    /// Git の情報を読み込むためのクラス
    /// </summary>
    public static class GitUtils
    {
        //================================================================================
        // 関数（static）
        //================================================================================
        /// <summary>
        /// Git のブランチ名を読み込んで返します
        /// </summary>
        public static string LoadBranchName()
        {
            return LoadImpl( "symbolic-ref --short HEAD" );
        }

        /// <summary>
        /// Git のコミットハッシュを読み込んで返します
        /// </summary>
        public static string LoadCommitHash()
        {
            return LoadImpl( "rev-parse HEAD" );
        }

        /// <summary>
        /// Git のコミットハッシュ（短縮版）を読み込んで返します
        /// </summary>
        public static string LoadShortCommitHash()
        {
            return LoadImpl( "rev-parse --short HEAD" );
        }

        /// <summary>
        /// Git のコミットログを読み込んで返します
        /// </summary>
        public static string LoadCommitLog( CommitLogOption option )
        {
            var count        = option.Count;
            var isNoMerges   = option.IsNoMerges;
            var noMergesText = isNoMerges ? "--no-merges " : string.Empty;
            var format       = option.Format;

            return LoadImpl( $@"log -n {count} --date=short {noMergesText} --pretty=format:""{format}""" );
        }

        /// <summary>
        /// Git のコミットハッシュを読み込むための関数
        /// </summary>
        private static string LoadImpl( string arguments )
        {
            var startInfo = new ProcessStartInfo
            {
                FileName               = "git",
                Arguments              = arguments,
                CreateNoWindow         = true,
                RedirectStandardError  = true,
                RedirectStandardOutput = true,
                StandardErrorEncoding  = Encoding.UTF8,
                StandardOutputEncoding = Encoding.UTF8,
                UseShellExecute        = false,
            };

            using ( var process = Process.Start( startInfo ) )
            {
                // git コマンドが終了するまで待機します
                process.WaitForExit();

                // Git の情報を読み込んだ際に末尾に改行コードが付与されているため
                // 改行コードを取り除いた文字列を返すようにしています
                var hash = process.StandardOutput.ReadToEnd().Trim();
                return hash;
            }
        }
    }
}