using System.Diagnostics.CodeAnalysis;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the Revit API maps that pair a key with a value.
/// </summary>
/// <remarks>
///     A map hands out its entries through an iterator factory and keeps the key of the current entry on the concrete iterator.
///     Its contract stops at the non-generic <see cref="global::System.Collections.IEnumerable"/>.
///     The members below pair each key with its value and carry the element type into the sequence.
/// </remarks>
[PublicAPI]
public static class MapExtensions
{
    /// <param name="map">The source map.</param>
    extension(DefinitionBindingMap map)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Definition"/> and <see cref="Binding"/> pairs from this map.
        /// </summary>
        /// <returns>A sequence pairing each definition in the map with the binding stored under it.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        /// </remarks>
        [Pure]
        public IEnumerable<(Definition Definition, Binding Binding)> EnumerateEntries()
        {
            using var iterator = map.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return (iterator.Key, (Binding)iterator.Current!);
            }
        }

        /// <summary>
        ///     Returns an enumeration of <see cref="Definition"/> from this map.
        /// </summary>
        /// <returns>A sequence of the definitions the map binds.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        ///     The enumeration reads the key of each entry and leaves the binding untouched.
        /// </remarks>
        [Pure]
        public IEnumerable<Definition> EnumerateKeys()
        {
            using var iterator = map.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return iterator.Key;
            }
        }

        /// <summary>
        ///     Returns an enumeration of <see cref="Binding"/> from this map.
        /// </summary>
        /// <returns>A sequence of the bindings the map holds.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        ///     The enumeration reads the binding of each entry and leaves the key untouched.
        /// </remarks>
        [Pure]
        public IEnumerable<Binding> EnumerateValues()
        {
            using var iterator = map.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return (Binding)iterator.Current!;
            }
        }

        /// <summary>
        ///     Gets the <see cref="Binding"/> associated with the specified <see cref="Definition"/>.
        /// </summary>
        /// <param name="definition">The definition of the binding to get.</param>
        /// <param name="binding">
        ///     When this method returns, contains the binding associated with the specified definition, if the definition is
        ///     found; otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the map contains a binding for the specified definition; otherwise,
        ///     <see langword="false"/>.
        /// </returns>
        /// <remarks>This method combines the functionality of <c>Contains</c> and the indexer.</remarks>
        [Pure]
        public bool TryGetValue(Definition definition, [NotNullWhen(true)] out Binding? binding)
        {
            if (!map.Contains(definition))
            {
                binding = null;

                return false;
            }

            binding = map.get_Item(definition);

            return true;
        }
    }

    /// <param name="map">The source map.</param>
    extension(ParameterMap map)
    {
        /// <summary>
        ///     Returns an enumeration of name and <see cref="Parameter"/> pairs from this map.
        /// </summary>
        /// <returns>A sequence pairing each parameter name in the map with the parameter stored under it.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        /// </remarks>
        [Pure]
        public IEnumerable<(string Name, Parameter Parameter)> EnumerateEntries()
        {
            using var iterator = map.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return (iterator.Key, (Parameter)iterator.Current!);
            }
        }

        /// <summary>
        ///     Returns an enumeration of the parameter names in this map.
        /// </summary>
        /// <returns>A sequence of the names the map stores its parameters under.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        ///     The enumeration reads the key of each entry and leaves the parameter untouched.
        /// </remarks>
        [Pure]
        public IEnumerable<string> EnumerateKeys()
        {
            using var iterator = map.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return iterator.Key;
            }
        }

        /// <summary>
        ///     Returns an enumeration of <see cref="Parameter"/> from this map.
        /// </summary>
        /// <returns>A sequence of the parameters the map holds.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        ///     The enumeration reads the parameter of each entry and leaves the key untouched.
        /// </remarks>
        [Pure]
        public IEnumerable<Parameter> EnumerateValues()
        {
            using var iterator = map.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return (Parameter)iterator.Current!;
            }
        }

        /// <summary>
        ///     Gets the <see cref="Parameter"/> associated with the specified name.
        /// </summary>
        /// <param name="name">The name of the parameter to get.</param>
        /// <param name="parameter">
        ///     When this method returns, contains the parameter associated with the specified name, if the name is found;
        ///     otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the map contains a parameter for the specified name; otherwise,
        ///     <see langword="false"/>.
        /// </returns>
        /// <remarks>This method combines the functionality of <c>Contains</c> and the indexer.</remarks>
        [Pure]
        public bool TryGetValue(string name, [NotNullWhen(true)] out Parameter? parameter)
        {
            if (!map.Contains(name))
            {
                parameter = null;

                return false;
            }

            parameter = map.get_Item(name);

            return true;
        }
    }

    /// <param name="map">The source map.</param>
    extension(CategoryNameMap map)
    {
        /// <summary>
        ///     Returns an enumeration of name and <see cref="Autodesk.Revit.DB.Category"/> pairs from this map.
        /// </summary>
        /// <returns>A sequence pairing each category name in the map with the category stored under it.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        /// </remarks>
        [Pure]
        public IEnumerable<(string Name, Category Category)> EnumerateEntries()
        {
            using var iterator = map.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return (iterator.Key, (Category)iterator.Current!);
            }
        }

        /// <summary>
        ///     Returns an enumeration of the category names in this map.
        /// </summary>
        /// <returns>A sequence of the names the map stores its categories under.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        ///     The enumeration reads the key of each entry and leaves the category untouched.
        /// </remarks>
        [Pure]
        public IEnumerable<string> EnumerateKeys()
        {
            using var iterator = map.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return iterator.Key;
            }
        }

        /// <summary>
        ///     Returns an enumeration of <see cref="Autodesk.Revit.DB.Category"/> from this map.
        /// </summary>
        /// <returns>A sequence of the categories the map holds.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        ///     The enumeration reads the category of each entry and leaves the key untouched.
        /// </remarks>
        [Pure]
        public IEnumerable<Category> EnumerateValues()
        {
            using var iterator = map.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return (Category)iterator.Current!;
            }
        }

        /// <summary>
        ///     Gets the <see cref="Autodesk.Revit.DB.Category"/> associated with the specified name.
        /// </summary>
        /// <param name="name">The name of the category to get.</param>
        /// <param name="category">
        ///     When this method returns, contains the category associated with the specified name, if the name is found;
        ///     otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the map contains a category for the specified name; otherwise,
        ///     <see langword="false"/>.
        /// </returns>
        /// <remarks>This method combines the functionality of <c>Contains</c> and the indexer.</remarks>
        [Pure]
        public bool TryGetValue(string name, [NotNullWhen(true)] out Category? category)
        {
            if (!map.Contains(name))
            {
                category = null;

                return false;
            }

            category = map.get_Item(name);

            return true;
        }
    }

    /// <param name="categories">The source map.</param>
    extension(Categories categories)
    {
        /// <summary>
        ///     Returns an enumeration of name and <see cref="Autodesk.Revit.DB.Category"/> pairs from this map.
        /// </summary>
        /// <returns>A sequence pairing each category name in the map with the category stored under it.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        /// </remarks>
        [Pure]
        public IEnumerable<(string Name, Category Category)> EnumerateEntries()
        {
            using var iterator = categories.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return (iterator.Key, (Category)iterator.Current!);
            }
        }

        /// <summary>
        ///     Returns an enumeration of the category names in this map.
        /// </summary>
        /// <returns>A sequence of the names the map stores its categories under.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        ///     The enumeration reads the key of each entry and leaves the category untouched.
        /// </remarks>
        [Pure]
        public IEnumerable<string> EnumerateKeys()
        {
            using var iterator = categories.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return iterator.Key;
            }
        }

        /// <summary>
        ///     Returns an enumeration of <see cref="Autodesk.Revit.DB.Category"/> from this map.
        /// </summary>
        /// <returns>A sequence of the categories the map holds.</returns>
        /// <remarks>
        ///     Each enumeration opens a native iterator over the map and disposes it when the enumeration ends.
        ///     The enumeration reads the category of each entry and leaves the key untouched.
        /// </remarks>
        [Pure]
        public IEnumerable<Category> EnumerateValues()
        {
            using var iterator = categories.ForwardIterator();

            while (iterator.MoveNext())
            {
                yield return (Category)iterator.Current!;
            }
        }

        /// <summary>
        ///     Gets the <see cref="Autodesk.Revit.DB.Category"/> associated with the specified name.
        /// </summary>
        /// <param name="name">The name of the category to get.</param>
        /// <param name="category">
        ///     When this method returns, contains the category associated with the specified name, if the name is found;
        ///     otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the map contains a category for the specified name; otherwise,
        ///     <see langword="false"/>.
        /// </returns>
        /// <remarks>This method combines the functionality of <c>Contains</c> and the indexer.</remarks>
        [Pure]
        public bool TryGetValue(string name, [NotNullWhen(true)] out Category? category)
        {
            if (!categories.Contains(name))
            {
                category = null;

                return false;
            }

            category = categories.get_Item(name);

            return true;
        }
    }
}
