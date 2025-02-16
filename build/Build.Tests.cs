using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

sealed partial class Build
{
    Target Test => _ => _
        .DependsOn(Clean)
        .OnlyWhenStatic(() => IsServerBuild)
        .WhenSkipped(DependencyBehavior.Execute)
        .Executes(() =>
        {
            var testConfigurations = GlobBuildConfigurations()
                .Where(configuration => configuration.Contains("test", StringComparison.OrdinalIgnoreCase));

            foreach (var configuration in testConfigurations)
            {
                DotNetTest(settings => settings
                    .SetConfiguration(configuration)
                    .SetVerbosity(DotNetVerbosity.minimal));
            }
        });
}