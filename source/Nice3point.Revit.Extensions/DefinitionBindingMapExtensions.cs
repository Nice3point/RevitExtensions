using JetBrains.Annotations;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit DefinitionBindingMap Extensions
/// </summary>
[PublicAPI]
public static class DefinitionBindingMapExtensions
{
    /// <param name="map">The source map.</param>
    extension(DefinitionBindingMap map)
    {
        /// <summary>
        ///     Enumerates the definition and binding pairs contained in the map.
        /// </summary>
        /// <returns>A sequence of <see cref="Definition"/> and <see cref="ElementBinding"/> pairs.</returns>
        [Pure]
        public IEnumerable<(Definition Definition, ElementBinding Binding)> AsEnumerable()
        {
            var iterator = map.ForwardIterator();
            while (iterator.MoveNext())
            {
                if (iterator is { Key: { } definition, Current: ElementBinding binding })
                {
                    yield return (definition, binding);
                }
            }
        }
    }
}