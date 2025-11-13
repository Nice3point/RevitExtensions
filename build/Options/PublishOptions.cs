using System.ComponentModel.DataAnnotations;

namespace Build.Options;

[Serializable]
public sealed record PublishOptions
{
    [Required] public string Version { get; init; } = null!;
}