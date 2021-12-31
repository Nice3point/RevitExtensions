using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    Target Test => _ => _
        .TriggeredBy(Compile, Cleaning)
        .Executes(() =>
        {
            DotNetTest(settings => settings
                .SetProcessToolPath(MsBuildPath.Value)
                .SetConfiguration(TestConfiguration)
                .SetVerbosity(DotNetVerbosity.Minimal));
        });
}