﻿using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    Target Pack => _ => _
        .DependsOn(Clean, Test)
        .Requires(() => ReleaseVersion)
        .Executes(() =>
        {
            var readme = CreateNugetReadme();
            try
            {
                var changelog = CreateNugetChangelog();
                var packConfigurations = GlobBuildConfigurations()
                    .Where(configuration => configuration.Contains(" R", StringComparison.OrdinalIgnoreCase));
                
                foreach (var configuration in packConfigurations)
                {
                    DotNetPack(settings => settings
                        .SetConfiguration(configuration)
                        .SetVersion(GetPackVersion(configuration))
                        .SetOutputDirectory(ArtifactsDirectory)
                        .SetVerbosity(DotNetVerbosity.minimal)
                        .SetPackageReleaseNotes(changelog));
                }
            }
            finally
            {
                RestoreReadme(readme);
            }
        });

    string GetPackVersion(string configuration)
    {
        if (PackageVersionsMap.TryGetValue(configuration, out var version)) return version;
        throw new Exception($"Can't find pack version for configuration: {configuration}");
    }

    string CreateNugetReadme()
    {
        var readmePath = Solution.Directory / "Readme.md";
        var readme = File.ReadAllText(readmePath);

        const string startSymbol = "<p";
        const string endSymbol = "</p>\r\n\r\n";

        var logoStartIndex = readme.IndexOf(startSymbol, StringComparison.Ordinal);
        var logoEndIndex = readme.IndexOf(endSymbol, StringComparison.Ordinal);

        var nugetReadme = readme.Remove(logoStartIndex, logoEndIndex - logoStartIndex + endSymbol.Length);
        File.WriteAllText(readmePath, nugetReadme);

        return readme;
    }

    void RestoreReadme(string readme)
    {
        var readmePath = Solution.Directory / "Readme.md";

        File.WriteAllText(readmePath, readme);
    }
}