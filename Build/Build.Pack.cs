using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
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