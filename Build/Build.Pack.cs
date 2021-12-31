using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    readonly Dictionary<string, string> VersionMap = new()
    {
        {"Release R19", "2019.0.1"},
        {"Release R20", "2020.0.1"},
        {"Release R21", "2021.0.1"},
        {"Release R22", "2022.0.1"}
    };

    Target Pack => _ => _
        .TriggeredBy(Test)
        .OnlyWhenStatic(() => IsLocalBuild)
        .Executes(() =>
        {
            var configurations = GetConfigurations(BuildConfiguration);
            configurations.ForEach(configuration =>
            {
                DotNetPack(settings => settings
                    .SetProcessToolPath(MsBuildPath.Value)
                    .SetConfiguration(configuration)
                    .SetVersion(GetPackVersion(configuration))
                    .SetOutputDirectory(ArtifactsDirectory)
                    .SetVerbosity(DotNetVerbosity.Minimal));
            });
        });

    string GetPackVersion(string configuration)
    {
        if (VersionMap.ContainsKey(configuration)) return VersionMap[configuration];
        throw new Exception($"Can't find pack version for configuration: {configuration}");
    }
}