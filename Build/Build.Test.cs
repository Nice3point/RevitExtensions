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
            var msBuildPath = GetMsBuildPath();
            TestProject(msBuildPath);
        });

    void TestProject(string toolPath) =>
        DotNetTest(settings => settings
            .SetProcessToolPath(toolPath)
            .SetConfiguration(TestConfiguration)
            .SetVerbosity(DotNetVerbosity.Minimal));
}