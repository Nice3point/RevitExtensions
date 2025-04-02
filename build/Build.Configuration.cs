sealed partial class Build
{
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / "output";
    readonly AbsolutePath ChangeLogPath = RootDirectory / "Changelog.md";

    [Parameter] string ReleaseVersion;
    
    protected override void OnBuildInitialized()
    {
        ReleaseVersion ??= GitRepository.Tags.SingleOrDefault();
        
        Configurations =
        [
            "Release*"
        ];

        PackageVersionsMap = new()
        {
            { "Release R20", "2020.4.1" },
            { "Release R21", "2021.4.1" },
            { "Release R22", "2022.4.1" },
            { "Release R23", "2023.4.1" },
            { "Release R24", "2024.2.1" },
            { "Release R25", "2025.1.1" },
            { "Release R26", "2026.0.1" }
        };
    }
}