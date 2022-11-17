partial class Build
{
    readonly Dictionary<string, string> VersionMap = new()
    {
        {"Release R19", "2019.1.9"},
        {"Release R20", "2020.1.9"},
        {"Release R21", "2021.1.9"},
        {"Release R22", "2022.1.9"},
        {"Release R23", "2023.1.9"},
    };

    const string BuildConfiguration = "Release";
    const string ArtifactsFolder = "output";
}