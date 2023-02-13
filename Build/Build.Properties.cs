partial class Build
{
    readonly Dictionary<string, string> VersionMap = new()
    {
        {"Release R20", "2020.2.0"},
        {"Release R21", "2021.2.0"},
        {"Release R22", "2022.2.0"},
        {"Release R23", "2023.2.0"},
        {"Release R24", "2023.2.0"},
    };

    const string BuildConfiguration = "Release";
    const string ArtifactsFolder = "output";
}