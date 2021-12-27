using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.VSWhere;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    readonly Dictionary<string, string> VersionMap = new()
    {
        {"Release R19", "2019.0.0"},
        {"Release R20", "2020.0.0"},
        {"Release R21", "2021.0.0"},
        {"Release R22", "2022.0.0"}
    };

    Target Pack => _ => _
        .TriggeredBy(Cleaning)
        .Executes(() =>
        {
            var msBuildPath = GetMsBuildPath();
            var configurations = GetConfigurations(BuildConfiguration);
            foreach (var configuration in configurations) PackProject(configuration, msBuildPath);
        });

    static string GetMsBuildPath()
    {
        if (IsServerBuild) return null;
        var (_, output) = VSWhereTasks.VSWhere(settings => settings
            .EnableLatest()
            .AddRequires("Microsoft.Component.MSBuild")
            .SetProperty("installationPath")
        );

        if (output.Count > 0) return null;
        if (!File.Exists(CustomMsBuildPath)) throw new Exception($"Missing file: {CustomMsBuildPath}. Change the path to the build platform or install Visual Studio.");
        return CustomMsBuildPath;
    }

    string GetPackVersion(string configuration)
    {
        if (VersionMap.ContainsKey(configuration)) return VersionMap[configuration];
        throw new Exception($"Can't find pack version for configuration: {configuration}");
    }

    void PackProject(string configuration, string toolPath) =>
        DotNetPack(settings => settings
            .SetProcessToolPath(toolPath)
            .SetConfiguration(configuration)
            .SetVersion(GetPackVersion(configuration))
            .SetOutputDirectory(ArtifactsDirectory)
            .SetVerbosity(DotNetVerbosity.Minimal));
}