using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    const string NugetApiUrl = "https://api.nuget.org/v3/index.json";

    Target NuGetPush => _ => _
        .OnlyWhenStatic(() => IsLocalBuild)
        .Executes(() =>
        {
            ArtifactsDirectory.GlobFiles("*.nupkg")
                .ForEach(x =>
                {
                    DotNetNuGetPush(settings => settings
                        .SetProcessToolPath(MsBuildPath.Value)
                        .SetSource(NugetApiUrl)
                        .SetApiKey(NugetApiKey));
                });
        });
}