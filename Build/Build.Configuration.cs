sealed partial class Build
{
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / "output";
    readonly AbsolutePath ChangeLogPath = RootDirectory / "Changelog.md";

    [Parameter] string ReleaseVersion;
    
    protected override void OnBuildInitialized()
    {
        ReleaseVersion = GitRepository.Tags.SingleOrDefault();
        
        Configurations =
        [
            "Release*"
        ];

        PackageVersionsMap = new()
        {
            { "Release R20", "2020.3.1-preview.6.0" },
            { "Release R21", "2021.3.1-preview.6.0" },
            { "Release R22", "2022.3.1-preview.6.0" },
            { "Release R23", "2023.3.1-preview.6.0" },
            { "Release R24", "2024.1.1-preview.6.0" },
            { "Release R25", "2025.0.1-preview.6.0" },
        };
    }
}