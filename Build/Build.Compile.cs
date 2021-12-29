using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.VSWhere;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    Target Compile => _ => _
        .TriggeredBy(Cleaning)
        .OnlyWhenStatic(() => IsServerBuild)
        .Executes(() =>
        {
            var msBuildPath = GetMsBuildPath();
            var configurations = GetConfigurations(BuildConfiguration);
            foreach (var configuration in configurations) CompileProject(configuration, msBuildPath);
        });

    void CompileProject(string configuration, string toolPath) =>
        DotNetBuild(settings => settings
            .SetProcessToolPath(toolPath)
            .SetConfiguration(configuration)
            .SetVerbosity(DotNetVerbosity.Minimal));
}