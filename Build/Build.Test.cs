using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    Target Test => _ => _
        .TriggeredBy(Compile, Cleaning)
        .Executes(() =>
        {
            DotNetTest(settings => settings
                .SetConfiguration(TestConfiguration)
                .SetVerbosity(DotNetVerbosity.Minimal));
        });
}