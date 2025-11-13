using System.ComponentModel.DataAnnotations;

namespace Build.Options;

[Serializable]
public sealed record BuildOptions
{
    [Required] public string ConfigurationFilter { get; init; } = null!;
    [Required] public Dictionary<string, string> Versions { get; init; } = null!;
}