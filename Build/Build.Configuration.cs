using Nuke.Common.CI.GitHubActions;

sealed partial class Build
{
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / "output";
    readonly AbsolutePath ChangeLogPath = RootDirectory / "Changelog.md";

    string ReleaseVersion => ((GitRepository.Branch is null && GitRepository.Tags.Count > 0) ||
                              (GitRepository.Branch is not null && GitRepository.Branch.StartsWith("refs/tags"))) switch
    {
        true when GitHubActions.Instance is not null => GitHubActions.Instance.RefName,
        true => GitRepository.Tags.Single(),
        false => "1.0.0"
    };
    
    protected override void OnBuildInitialized()
    {
        Configurations =
        [
            "Release*"
        ];

        PackageVersionsMap = new()
        {
            { "Release R20", "2020.3.1-preview.5.2" },
            { "Release R21", "2021.3.1-preview.5.2" },
            { "Release R22", "2022.3.1-preview.5.2" },
            { "Release R23", "2023.3.1-preview.5.2" },
            { "Release R24", "2024.1.1-preview.5.2" },
            { "Release R25", "2025.0.1-preview.5.2" },
        };
    }
}