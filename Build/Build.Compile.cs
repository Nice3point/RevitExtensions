using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.VSWhere;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    Target Compile => _ => _
        .TriggeredBy(Cleaning)
        .OnlyWhenStatic(() => IsServerBuild)
        .Executes(() =>
        {
            var configurations = GetConfigurations(BuildConfiguration);
            configurations.ForEach(configuration =>
            {
                DotNetBuild(settings => settings
                    .SetProcessToolPath(MsBuildPath.Value)
                    .SetConfiguration(configuration)
                    .SetVerbosity(DotNetVerbosity.Minimal));
            });
        });
}