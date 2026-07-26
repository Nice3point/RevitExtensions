using System.Diagnostics.CodeAnalysis;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Visual;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the Revit API collections holding elements of a single type.
/// </summary>
/// <remarks>
///     A collection stops its contract at the non-generic <see cref="global::System.Collections.IEnumerable"/>.
///     A <c>foreach</c> over it yields <see cref="object"/>, and every LINQ query opens with a cast naming the element type.
///     The members below carry the element type into the sequence.
/// </remarks>
[PublicAPI]
[SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
public static class CollectionExtensions
{
    /// <param name="assetSet">The source collection.</param>
    extension(AssetSet assetSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Autodesk.Revit.DB.Visual.Asset"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<Asset> EnumerateValues()
        {
            foreach (Asset asset in assetSet)
            {
                yield return asset;
            }
        }
    }

    /// <param name="categorySet">The source collection.</param>
    extension(CategorySet categorySet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Autodesk.Revit.DB.Category"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<Category> EnumerateValues()
        {
            foreach (Category category in categorySet)
            {
                yield return category;
            }
        }
    }

    /// <param name="citySet">The source collection.</param>
    extension(CitySet citySet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="City"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<City> EnumerateValues()
        {
            foreach (City city in citySet)
            {
                yield return city;
            }
        }
    }

    /// <param name="combinableElementArray">The source collection.</param>
    extension(CombinableElementArray combinableElementArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="CombinableElement"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<CombinableElement> EnumerateValues()
        {
            var count = combinableElementArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return combinableElementArray.get_Item(index);
            }
        }
    }

    /// <param name="connectorSet">The source collection.</param>
    extension(ConnectorSet connectorSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Connector"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<Connector> EnumerateValues()
        {
            foreach (Connector connector in connectorSet)
            {
                yield return connector;
            }
        }
    }

    /// <param name="curtainGridSet">The source collection.</param>
    extension(CurtainGridSet curtainGridSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="CurtainGrid"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<CurtainGrid> EnumerateValues()
        {
            foreach (CurtainGrid curtainGrid in curtainGridSet)
            {
                yield return curtainGrid;
            }
        }
    }

    /// <param name="curveArrArray">The source collection.</param>
    extension(CurveArrArray curveArrArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="CurveArray"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<CurveArray> EnumerateValues()
        {
            var count = curveArrArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return curveArrArray.get_Item(index);
            }
        }
    }

    /// <param name="curveArray">The source collection.</param>
    extension(CurveArray curveArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Curve"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<Curve> EnumerateValues()
        {
            var count = curveArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return curveArray.get_Item(index);
            }
        }
    }

    /// <param name="curveByPointsArray">The source collection.</param>
    extension(CurveByPointsArray curveByPointsArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="CurveByPoints"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<CurveByPoints> EnumerateValues()
        {
            var count = curveByPointsArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return curveByPointsArray.get_Item(index);
            }
        }
    }

    /// <param name="detailCurveArray">The source collection.</param>
    extension(DetailCurveArray detailCurveArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="DetailCurve"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<DetailCurve> EnumerateValues()
        {
            var count = detailCurveArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return detailCurveArray.get_Item(index);
            }
        }
    }

    /// <param name="dimensionSegmentArray">The source collection.</param>
    extension(DimensionSegmentArray dimensionSegmentArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="DimensionSegment"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<DimensionSegment> EnumerateValues()
        {
            var count = dimensionSegmentArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return dimensionSegmentArray.get_Item(index);
            }
        }
    }

    /// <param name="distributionSysTypeSet">The source collection.</param>
    extension(DistributionSysTypeSet distributionSysTypeSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="DistributionSysType"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<DistributionSysType> EnumerateValues()
        {
            foreach (DistributionSysType distributionSysType in distributionSysTypeSet)
            {
                yield return distributionSysType;
            }
        }
    }

    /// <param name="documentSet">The source collection.</param>
    extension(DocumentSet documentSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Document"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<Document> EnumerateValues()
        {
            foreach (Document document in documentSet)
            {
                yield return document;
            }
        }
    }

    /// <param name="doubleArray">The source collection.</param>
    extension(DoubleArray doubleArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="global::System.Double"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<double> EnumerateValues()
        {
            var count = doubleArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return doubleArray.get_Item(index);
            }
        }
    }

    /// <param name="edgeArray">The source collection.</param>
    extension(EdgeArray edgeArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Edge"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<Edge> EnumerateValues()
        {
            var count = edgeArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return edgeArray.get_Item(index);
            }
        }
    }

    /// <param name="edgeArrayArray">The source collection.</param>
    extension(EdgeArrayArray edgeArrayArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="EdgeArray"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<EdgeArray> EnumerateValues()
        {
            var count = edgeArrayArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return edgeArrayArray.get_Item(index);
            }
        }
    }

    /// <param name="elementArray">The source collection.</param>
    extension(ElementArray elementArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Autodesk.Revit.DB.Element"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<Element> EnumerateValues()
        {
            var count = elementArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return elementArray.get_Item(index);
            }
        }
    }

    /// <param name="elementSet">The source collection.</param>
    extension(ElementSet elementSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Autodesk.Revit.DB.Element"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<Element> EnumerateValues()
        {
            foreach (Element element in elementSet)
            {
                yield return element;
            }
        }
    }

    /// <param name="faceArray">The source collection.</param>
    extension(FaceArray faceArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Face"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<Face> EnumerateValues()
        {
            var count = faceArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return faceArray.get_Item(index);
            }
        }
    }

    /// <param name="familyParameterSet">The source collection.</param>
    extension(FamilyParameterSet familyParameterSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="FamilyParameter"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<FamilyParameter> EnumerateValues()
        {
            foreach (FamilyParameter familyParameter in familyParameterSet)
            {
                yield return familyParameter;
            }
        }
    }

    /// <param name="familyTypeSet">The source collection.</param>
    extension(FamilyTypeSet familyTypeSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="FamilyType"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<FamilyType> EnumerateValues()
        {
            foreach (FamilyType familyType in familyTypeSet)
            {
                yield return familyType;
            }
        }
    }

    /// <param name="formArray">The source collection.</param>
    extension(FormArray formArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Form"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<Form> EnumerateValues()
        {
            var count = formArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return formArray.get_Item(index);
            }
        }
    }

    /// <param name="geomCombinationSet">The source collection.</param>
    extension(GeomCombinationSet geomCombinationSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="GeomCombination"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<GeomCombination> EnumerateValues()
        {
            foreach (GeomCombination geomCombination in geomCombinationSet)
            {
                yield return geomCombination;
            }
        }
    }

    /// <param name="groupSet">The source collection.</param>
    extension(GroupSet groupSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Group"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<Group> EnumerateValues()
        {
            foreach (Group group in groupSet)
            {
                yield return group;
            }
        }
    }

    /// <param name="intersectionResultArray">The source collection.</param>
    extension(IntersectionResultArray intersectionResultArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="IntersectionResult"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<IntersectionResult> EnumerateValues()
        {
            var count = intersectionResultArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return intersectionResultArray.get_Item(index);
            }
        }
    }

    /// <param name="leaderArray">The source collection.</param>
    extension(LeaderArray leaderArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Leader"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<Leader> EnumerateValues()
        {
            var count = leaderArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return leaderArray.get_Item(index);
            }
        }
    }

    /// <param name="mepBuildingConstructionSet">The source collection.</param>
    extension(MEPBuildingConstructionSet mepBuildingConstructionSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="MEPBuildingConstruction"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<MEPBuildingConstruction> EnumerateValues()
        {
            foreach (MEPBuildingConstruction mepBuildingConstruction in mepBuildingConstructionSet)
            {
                yield return mepBuildingConstruction;
            }
        }
    }

    /// <param name="modelCurveArrArray">The source collection.</param>
    extension(ModelCurveArrArray modelCurveArrArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="ModelCurveArray"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<ModelCurveArray> EnumerateValues()
        {
            var count = modelCurveArrArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return modelCurveArrArray.get_Item(index);
            }
        }
    }

    /// <param name="modelCurveArray">The source collection.</param>
    extension(ModelCurveArray modelCurveArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="ModelCurve"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<ModelCurve> EnumerateValues()
        {
            var count = modelCurveArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return modelCurveArray.get_Item(index);
            }
        }
    }

    /// <param name="mullionTypeSet">The source collection.</param>
    extension(MullionTypeSet mullionTypeSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="MullionType"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<MullionType> EnumerateValues()
        {
            foreach (MullionType mullionType in mullionTypeSet)
            {
                yield return mullionType;
            }
        }
    }

    /// <param name="panelTypeSet">The source collection.</param>
    extension(PanelTypeSet panelTypeSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="PanelType"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<PanelType> EnumerateValues()
        {
            foreach (PanelType panelType in panelTypeSet)
            {
                yield return panelType;
            }
        }
    }

    /// <param name="paperSizeSet">The source collection.</param>
    extension(PaperSizeSet paperSizeSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="PaperSize"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<PaperSize> EnumerateValues()
        {
            foreach (PaperSize paperSize in paperSizeSet)
            {
                yield return paperSize;
            }
        }
    }

    /// <param name="paperSourceSet">The source collection.</param>
    extension(PaperSourceSet paperSourceSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="PaperSource"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<PaperSource> EnumerateValues()
        {
            foreach (PaperSource paperSource in paperSourceSet)
            {
                yield return paperSource;
            }
        }
    }

    /// <param name="parameterSet">The source collection.</param>
    extension(ParameterSet parameterSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Parameter"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<Parameter> EnumerateValues()
        {
            foreach (Parameter parameter in parameterSet)
            {
                yield return parameter;
            }
        }
    }

    /// <param name="phaseArray">The source collection.</param>
    extension(PhaseArray phaseArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Phase"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<Phase> EnumerateValues()
        {
            var count = phaseArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return phaseArray.get_Item(index);
            }
        }
    }

    /// <param name="planCircuitSet">The source collection.</param>
    extension(PlanCircuitSet planCircuitSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="PlanCircuit"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<PlanCircuit> EnumerateValues()
        {
            foreach (PlanCircuit planCircuit in planCircuitSet)
            {
                yield return planCircuit;
            }
        }
    }

    /// <param name="planTopologySet">The source collection.</param>
    extension(PlanTopologySet planTopologySet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="PlanTopology"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<PlanTopology> EnumerateValues()
        {
            foreach (PlanTopology planTopology in planTopologySet)
            {
                yield return planTopology;
            }
        }
    }

    /// <param name="projectLocationSet">The source collection.</param>
    extension(ProjectLocationSet projectLocationSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="ProjectLocation"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<ProjectLocation> EnumerateValues()
        {
            foreach (ProjectLocation projectLocation in projectLocationSet)
            {
                yield return projectLocation;
            }
        }
    }

    /// <param name="referenceArray">The source collection.</param>
    extension(ReferenceArray referenceArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Reference"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<Reference> EnumerateValues()
        {
            var count = referenceArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return referenceArray.get_Item(index);
            }
        }
    }

    /// <param name="referenceArrayArray">The source collection.</param>
    extension(ReferenceArrayArray referenceArrayArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="ReferenceArray"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<ReferenceArray> EnumerateValues()
        {
            var count = referenceArrayArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return referenceArrayArray.get_Item(index);
            }
        }
    }

    /// <param name="referencePointArray">The source collection.</param>
    extension(ReferencePointArray referencePointArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="ReferencePoint"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<ReferencePoint> EnumerateValues()
        {
            var count = referencePointArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return referencePointArray.get_Item(index);
            }
        }
    }

    /// <param name="slabShapeCreaseArray">The source collection.</param>
    extension(SlabShapeCreaseArray slabShapeCreaseArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="SlabShapeCrease"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<SlabShapeCrease> EnumerateValues()
        {
            var count = slabShapeCreaseArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return slabShapeCreaseArray.get_Item(index);
            }
        }
    }

    /// <param name="slabShapeVertexArray">The source collection.</param>
    extension(SlabShapeVertexArray slabShapeVertexArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="SlabShapeVertex"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<SlabShapeVertex> EnumerateValues()
        {
            var count = slabShapeVertexArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return slabShapeVertexArray.get_Item(index);
            }
        }
    }

    /// <param name="spaceSet">The source collection.</param>
    extension(SpaceSet spaceSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Space"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<Space> EnumerateValues()
        {
            foreach (Space space in spaceSet)
            {
                yield return space;
            }
        }
    }

    /// <param name="symbolicCurveArray">The source collection.</param>
    extension(SymbolicCurveArray symbolicCurveArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="SymbolicCurve"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<SymbolicCurve> EnumerateValues()
        {
            var count = symbolicCurveArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return symbolicCurveArray.get_Item(index);
            }
        }
    }

    /// <param name="vertexIndexPairArray">The source collection.</param>
    extension(VertexIndexPairArray vertexIndexPairArray)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="VertexIndexPair"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        /// <remarks>
        ///     Each enumeration reads the size of the collection once and reads every element by its index.
        /// </remarks>
        [Pure]
        public IEnumerable<VertexIndexPair> EnumerateValues()
        {
            var count = vertexIndexPairArray.Size;

            for (var index = 0; index < count; index++)
            {
                yield return vertexIndexPairArray.get_Item(index);
            }
        }
    }

    /// <param name="viewSet">The source collection.</param>
    extension(ViewSet viewSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="View"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<View> EnumerateValues()
        {
            foreach (View view in viewSet)
            {
                yield return view;
            }
        }
    }

    /// <param name="voltageTypeSet">The source collection.</param>
    extension(VoltageTypeSet voltageTypeSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="VoltageType"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<VoltageType> EnumerateValues()
        {
            foreach (VoltageType voltageType in voltageTypeSet)
            {
                yield return voltageType;
            }
        }
    }

    /// <param name="wireConduitTypeSet">The source collection.</param>
    extension(WireConduitTypeSet wireConduitTypeSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="WireConduitType"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<WireConduitType> EnumerateValues()
        {
            foreach (WireConduitType wireConduitType in wireConduitTypeSet)
            {
                yield return wireConduitType;
            }
        }
    }

    /// <param name="wireSet">The source collection.</param>
    extension(WireSet wireSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="Wire"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<Wire> EnumerateValues()
        {
            foreach (Wire wire in wireSet)
            {
                yield return wire;
            }
        }
    }

    /// <param name="wireTypeSet">The source collection.</param>
    extension(WireTypeSet wireTypeSet)
    {
        /// <summary>
        ///     Returns an enumeration of <see cref="WireType"/> from this collection.
        /// </summary>
        /// <returns>A sequence that walks the collection in the order the collection holds its elements.</returns>
        [Pure]
        public IEnumerable<WireType> EnumerateValues()
        {
            foreach (WireType wireType in wireTypeSet)
            {
                yield return wireType;
            }
        }
    }
}
