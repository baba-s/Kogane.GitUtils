# Uni Git Utils

Git のブランチ名やコミットハッシュ、コミットログを管理するスクリプトを生成するエディタ拡張

## 使用例

```cs
using UniGitUtils;
using UnityEditor;
using UnityEngine;

public static class Example
{
    [MenuItem( "Tools/Log" )]
    private static void Log()
    {
        var option = new CommitLogOption
        (
            count: 10,
            isNoMerges: false,
            format: "%h %cd %cn %s"
        );

        Debug.Log( GitUtils.LoadBranchName() );
        Debug.Log( GitUtils.LoadCommitHash() );
        Debug.Log( GitUtils.LoadShortCommitHash() );
        Debug.Log( GitUtils.LoadCommitLog( option ) );
    }

    [MenuItem( "Tools/Generate" )]
    private static void Generate()
    {
        var outputPath = "Assets/Scripts/GitInfo.cs";

        var option = new CommitLogOption
        (
            count: 10,
            isNoMerges: false,
            format: "%h %cd %cn %s"
        );

        var template = $@"public static class GitInfo
{{
    public const string BRANCH_NAME       = ""{GitCodeGenerator.BRANCH_NAME_TAG}"";
    public const string COMMIT_HASH       = ""{GitCodeGenerator.COMMIT_HASH_TAG}"";
    public const string SHORT_COMMIT_HASH = ""{GitCodeGenerator.SHORT_COMMIT_HASH_TAG}"";
    public const string COMMIT_LOG_TAG    = @""{GitCodeGenerator.COMMIT_LOG_TAG}"";
}}";

        GitCodeGenerator.Generate( outputPath, template, option );
    }
}
```