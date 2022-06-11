partial class Build
{
    readonly Dictionary<string, string> VersionMap = new()
    {
        {"Release R19", "2019.1.3"},
        {"Release R20", "2020.1.3"},
        {"Release R21", "2021.1.3"},
        {"Release R22", "2022.1.3"},
        {"Release R23", "2023.1.3"},
    };

    const string BuildConfiguration = "Release";
    const string TestConfiguration = "UnitTests";
    const string ArtifactsFolder = "output";
}