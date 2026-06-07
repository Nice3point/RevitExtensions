# 2027.0.1

- Fixed AddShortcuts memory leak https://github.com/Nice3point/RevitExtensions/issues/74

# 2027.0.0

This update focuses on improved API design through C# 14 extension methods syntax, .NET 10 support, Revit 2027 support, and covers all known Utils classes.

## New Extensions

### Element

- `element.CanBeDeleted`
- `elementId.CanBeDeleted(Document)`

#### Transformation

- `element.Copy(XYZ)`, `Copy(double, double, double)`
- `elementId.Copy(Document, XYZ)`, `Copy(Document, double, double, double)`
- `element.Mirror(Plane)`
- `elementId.Mirror(Document, Plane)`
- `element.Move(XYZ)`, `Move(double, double, double)`
- `elementId.Move(Document, XYZ)`, `Move(Document, double, double, double)`
- `element.Rotate(Line, double)`
- `elementId.Rotate(Document, Line, double)`
- `element.CanBeMirrored`
- `elementId.CanBeMirrored(Document)`
- `elementIds.CanBeMirrored(Document)`
- `elementIds.MirrorElements(Document, Plane, bool)`
- `elementIds.MoveElements(Document, XYZ)`
- `elementIds.RotateElements(Document, Line, double)`
- `elementIds.CopyElements(Document, XYZ)`
- `elementIds.CopyElements(Document, Document)`
- `elementIds.CopyElements(Document, Document, Transform, CopyPasteOptions)`
- `elementIds.CopyElements(View, View)`
- `elementIds.CopyElements(View, View, Transform, CopyPasteOptions)`
- `view.GetTransformFromViewToView(View)`

#### Joins and cuts

- `element.JoinGeometry(Element)`
- `element.UnjoinGeometry(Element)`
- `element.AreElementsJoined(Element)`
- `element.GetJoinedElements()`
- `element.SwitchJoinOrder(Element)`
- `element.IsCuttingElementInJoin(Element)`
- `element.CanBeCutWithVoid`
- `element.GetCuttingVoidInstances()`
- `element.AddInstanceVoidCut(FamilyInstance)`
- `element.RemoveInstanceVoidCut(FamilyInstance)`
- `element.IsInstanceVoidCutExists(FamilyInstance)`
- `element.IsAllowedForSolidCut`
- `element.IsElementFromAppropriateContext`
- `element.GetCuttingSolids()`
- `element.GetSolidsBeingCut()`
- `element.CanElementCutElement(Element, out CutFailureReason)`
- `element.CutExistsBetweenElements(Element, out bool)`
- `element.AddCutBetweenSolids(Element)`
- `element.AddCutBetweenSolids(Element, bool)`
- `element.RemoveCutBetweenSolids(Element)`
- `element.SplitFacesOfCuttingSolid(Element, bool)`

### Application extensions

- `application.AsControlledApplication()`
- `application.IsDgnExportAvailable`
- `application.IsDgnImportLinkAvailable`
- `application.IsDwfExportAvailable`
- `application.IsDwgExportAvailable`
- `application.IsDwgImportLinkAvailable`
- `application.IsDxfExportAvailable`
- `application.IsFbxExportAvailable`
- `application.IsGraphicsAvailable`
- `application.IsIfcAvailable`
- `application.IsNavisworksExporterAvailable`
- `application.IsSatImportLinkAvailable`
- `application.IsShapeImporterAvailable`
- `application.IsSkpImportLinkAvailable`
- `application.Is3DmImportLinkAvailable`
- `application.IsAxmImportLinkAvailable`
- `application.IsObjImportLinkAvailable`
- `application.IsStlImportLinkAvailable`
- `application.IsStepImportLinkAvailable`
- `application.IsMaterialLibraryAvailable`
- `application.GetAllCloudRegions()`

### UIApplication extensions

- `uiApplication.AsControlledApplication()`

#### Ribbon

- `pushButton.TryAddShortcuts(string)`
- `pushButton.TryAddShortcuts(params IEnumerable<string>)`

### Document

- `document.Version`
- `document.IsValidVersionGuid(Guid)`
- `document.CheckAllFamilies(out ISet<ElementId>)`
- `document.CheckAllFamiliesSlow(out ISet<ElementId>)`

#### Global parameters

- `document.AreGlobalParametersAllowed`
- `document.FindGlobalParameter(string)`
- `document.GetAllGlobalParameters()`
- `document.GetGlobalParametersOrdered()`
- `document.SortGlobalParameters(ParametersOrder)`
- `document.IsUniqueGlobalParameterName(string)`
- `globalParameter.MoveUpOrder()`
- `globalParameter.MoveDownOrder()`
- `elementId.IsValidGlobalParameter(Document)`
- `elementId.MoveGlobalParameterUpOrder(Document)`
- `elementId.MoveGlobalParameterDownOrder(Document)`

#### Managers and services

- `document.GetTemporaryGraphicsManager()`
- `document.GetAnalyticalToPhysicalAssociationManager()`
- `document.GetLightGroupManager()`
- `view.CreateSpatialFieldManager(int)`
- `view.GetSpatialFieldManager()`

### Parameters

- `parameter.IsBuiltInParameter`

#### BuiltInParameter

- `builtInParameter.ToParameter(Document)`
- `builtInParameter.ToElementId()`
- `builtInParameter.GetParameterTypeId()`
- `elementId.IsParameter(BuiltInParameter)`

#### Filtering

- `element.IsParameterApplicable(ElementId)`
- `element.IsParameterApplicable(Parameter)`
- `categories.RemoveUnfilterableCategories()`
- `ParameterFilterElement.GetAllFilterableCategories()`
- `ParameterFilterElement.GetFilterableParametersInCommon(Document, ICollection<ElementId>)`
- `ParameterFilterElement.GetInapplicableParameters(Document, ICollection<ElementId>, IList<ElementId>)`

### Category

#### BuiltInCategory

- `builtInCategory.ToCategory(Document)`
- `builtInCategory.ToElementId()`
- `elementId.IsCategory(BuiltInCategory)`

### Geometry

#### Bounding box

- `boundingBox.Contains(XYZ)`
- `boundingBox.Contains(XYZ, bool)`
- `boundingBox.Contains(BoundingBoxXYZ)`
- `boundingBox.Contains(BoundingBoxXYZ, bool)`
- `boundingBox.Overlaps(BoundingBoxXYZ)`

#### Curve

- `curveElement.GetHostFace()`
- `curveElement.GetProjectionType()`
- `curveElement.SetProjectionType(CurveProjectionType)`
- `curveElement.GetSketchOnSurface()`
- `curveElement.SetSketchOnSurface(bool)`
- `reference.GetFaceRegions(Document)`
- `CurveElement.CreateArcThroughPoints(Document, ReferencePoint, ReferencePoint, ReferencePoint)`
- `CurveElement.AddCurvesToFaceRegion(Document, IList<ElementId>)`
- `CurveElement.CreateRectangle(Document, ReferencePoint, ReferencePoint, CurveProjectionType, bool, bool, out IList<ElementId>, out IList<ElementId>)`
- `CurveElement.ValidateForFaceRegions(Document, IList<ElementId>)`
- `CurveLoop.IsValidHorizontalBoundary(IList<CurveLoop>)`
- `CurveLoop.IsValidBoundaryOnSketchPlane(SketchPlane, IList<CurveLoop>)`
- `CurveLoop.IsValidBoundaryOnView(Document, ElementId, IList<CurveLoop>)`

#### Solid

- `geometry.IsNonSolid`
- `geometry.IsSolid`
- `geometry.LacksSubnodes`
- `solid.ComputeIsGeometricallyClosed()`
- `solid.ComputeIsTopologicallyClosed()`
- `solid.CutWithHalfSpace(Plane)`
- `solid.CutWithHalfSpaceModifyingOriginalSolid(Plane)`
- `solid.ExecuteBooleanOperation(Solid, BooleanOperationsType)`
- `solid.ExecuteBooleanOperationModifyingOriginalSolid(Solid, BooleanOperationsType)`
- `Solid.CreateBlendGeometry(CurveLoop, CurveLoop)`
- `Solid.CreateBlendGeometry(CurveLoop, CurveLoop, ICollection<VertexPair>)`
- `Solid.CreateBlendGeometry(CurveLoop, CurveLoop, ICollection<VertexPair>, SolidOptions)`
- `Solid.CreateExtrusionGeometry(IList<CurveLoop>, XYZ, double)`
- `Solid.CreateExtrusionGeometry(IList<CurveLoop>, XYZ, double, SolidOptions)`
- `Solid.CreateRevolvedGeometry(Frame, IList<CurveLoop>, double, double)`
- `Solid.CreateRevolvedGeometry(Frame, IList<CurveLoop>, double, double, SolidOptions)`
- `Solid.CreateSweptGeometry(CurveLoop, int, double, IList<CurveLoop>)`
- `Solid.CreateSweptGeometry(CurveLoop, int, double, IList<CurveLoop>, SolidOptions)`
- `Solid.CreateSweptBlendGeometry(Curve, IList<double>, IList<CurveLoop>, IList<ICollection<VertexPair>>)`
- `Solid.CreateSweptBlendGeometry(Curve, IList<double>, IList<CurveLoop>, IList<ICollection<VertexPair>>, SolidOptions)`
- `Solid.CreateFixedReferenceSweptGeometry(CurveLoop, int, double, IList<CurveLoop>, XYZ)`
- `Solid.CreateFixedReferenceSweptGeometry(CurveLoop, int, double, IList<CurveLoop>, XYZ, SolidOptions)`
- `Solid.CreateLoftGeometry(IList<CurveLoop>, SolidOptions)`

#### Tessellation

- `triangulation.ConvertTrianglesToQuads()`
- `filter.GetFilteredOutline(Outline)`

### View

- `view.GetDrawOrderForDetails(ISet<ElementId>)`
- `element.GetReferencedViewId()`
- `element.ChangeReferencedView(ElementId)`
- `elementId.GetReferencedViewId(Document)`
- `elementId.ChangeReferencedView(Document, ElementId)`

#### SSE point

- `category.GetSsePointVisibility(Document)`
- `category.SetSsePointVisibility(Document, bool)`

### ForgeTypeId

- `typeId.IsBuiltInParameter`
- `typeId.IsBuiltInGroup`
- `typeId.IsSpec`
- `typeId.IsValidDataType`
- `typeId.IsSymbol`
- `typeId.IsUnit`
- `typeId.IsMeasurableSpec`
- `typeId.IsValidUnit(ForgeTypeId)`
- `typeId.GetBuiltInParameter()`
- `typeId.GetDiscipline()`
- `typeId.GetValidUnits()`
- `typeId.GetTypeCatalogStringForSpec()`
- `typeId.GetTypeCatalogStringForUnit()`
- `typeId.GetBuiltInParameterGroupTypeId()`
- `typeId.DownloadCompanyName(Document)`
- `typeId.DownloadCompanyName(Document, string)`
- `typeId.DownloadParameterOptions()`
- `typeId.DownloadParameterOptions(string)`
- `typeId.DownloadParameter(Document, ParameterDownloadOptions)`
- `typeId.DownloadParameter(Document, ParameterDownloadOptions, string)`
- `ForgeTypeId.GetAllBuiltInParameters()`
- `ForgeTypeId.GetAllBuiltInGroups()`
- `ForgeTypeId.GetAllSpecs()`
- `ForgeTypeId.GetAllMeasurableSpecs()`
- `ForgeTypeId.GetAllDisciplines()`
- `ForgeTypeId.GetAllUnits()`

### Label

- `typeId.ToLabel()`
- `typeId.ToSpecLabel()`
- `typeId.ToSymbolLabel()`
- `typeId.ToUnitLabel()`
- `typeId.ToGroupLabel()`
- `typeId.ToParameterLabel()`
- `failureSeverity.ToLabel()`
- `structuralSectionShape.ToLabel()`

### FilteredElementCollector

- `document.CollectElements()`
- `document.CollectElements(ElementId)`
- `document.CollectElements(View)`
- `document.CollectElements(ICollection<ElementId>)`
- `collector.OfClass<T>()`
- `collector.OfClasses(IList<Type>)`
- `collector.OfClasses(params Type[])`
- `collector.ExcludingClass<T>()`
- `collector.ExcludingClasses(IList<Type>)`
- `collector.ExcludingClasses(params Type[])`
- `collector.OfCategories(ICollection<BuiltInCategory>)`
- `collector.OfCategories(params BuiltInCategory[])`
- `collector.ExcludingCategory(BuiltInCategory)`
- `collector.ExcludingCategories(ICollection<BuiltInCategory>)`
- `collector.ExcludingCategories(params BuiltInCategory[])`
- `collector.OfElements(ICollection<ElementId>)`
- `collector.OfCurveElementType(CurveElementType)`
- `collector.ExcludingCurveElementType(CurveElementType)`
- `collector.OfStructuralType(StructuralType)`
- `collector.ExcludingStructuralType(StructuralType)`
- `collector.Types()`
- `collector.Instances()`
- `collector.Rooms()`
- `collector.RoomTags()`
- `collector.Areas()`
- `collector.AreaTags()`
- `collector.Spaces()`
- `collector.SpaceTags()`
- `collector.FamilySymbols(ElementId)`
- `collector.FamilySymbols(Family)`
- `collector.FamilyInstances(Document, ElementId)`
- `collector.FamilyInstances(FamilySymbol)`
- `collector.IsCurveDriven()`
- `collector.IsViewIndependent()`
- `collector.IsPrimaryDesignOptionMember()`
- `collector.IsNotPrimaryDesignOptionMember()`
- `collector.OwnedByView(ElementId)`
- `collector.OwnedByView(View)`
- `collector.NotOwnedByView(ElementId)`
- `collector.NotOwnedByView(View)`
- `collector.VisibleInView(Document, ElementId)`
- `collector.VisibleInView(View)`
- `collector.NotVisibleInView(Document, ElementId)`
- `collector.NotVisibleInView(View)`
- `collector.OnLevel(ElementId)`
- `collector.OnLevel(Level)`
- `collector.NotOnLevel(ElementId)`
- `collector.NotOnLevel(Level)`
- `collector.InDesignOption(ElementId)`
- `collector.InDesignOption(DesignOption)`
- `collector.NotInDesignOption(ElementId)`
- `collector.NotInDesignOption(DesignOption)`
- `collector.InWorkset(WorksetId)`
- `collector.NotInWorkset(WorksetId)`
- `collector.WithStructuralUsage(StructuralInstanceUsage)`
- `collector.WithoutStructuralUsage(StructuralInstanceUsage)`
- `collector.WithStructuralWallUsage(StructuralWallUsage)`
- `collector.WithoutStructuralWallUsage(StructuralWallUsage)`
- `collector.WithStructuralMaterial(StructuralMaterialType)`
- `collector.WithoutStructuralMaterial(StructuralMaterialType)`
- `collector.WithFamilyStructuralMaterial(StructuralMaterialType)`
- `collector.WithoutFamilyStructuralMaterial(StructuralMaterialType)`
- `collector.WithPhaseStatus(ElementId, ICollection<ElementOnPhaseStatus>)`
- `collector.WithPhaseStatus(ElementId, params ElementOnPhaseStatus[])`
- `collector.WithPhaseStatus(Phase, ICollection<ElementOnPhaseStatus>)`
- `collector.WithPhaseStatus(Phase, params ElementOnPhaseStatus[])`
- `collector.WithoutPhaseStatus(ElementId, ICollection<ElementOnPhaseStatus>)`
- `collector.WithoutPhaseStatus(ElementId, params ElementOnPhaseStatus[])`
- `collector.WithoutPhaseStatus(Phase, ICollection<ElementOnPhaseStatus>)`
- `collector.WithoutPhaseStatus(Phase, params ElementOnPhaseStatus[])`
- `collector.WithExtensibleStorage(Guid)`
- `collector.WithExtensibleStorage(Schema)`
- `collector.HasSharedParameter(string)`
- `collector.IntersectingBoundingBox(Outline)`
- `collector.IntersectingBoundingBox(Outline, double)`
- `collector.NotIntersectingBoundingBox(Outline)`
- `collector.NotIntersectingBoundingBox(Outline, double)`
- `collector.InsideBoundingBox(Outline)`
- `collector.InsideBoundingBox(Outline, double)`
- `collector.NotInsideBoundingBox(Outline)`
- `collector.NotInsideBoundingBox(Outline, double)`
- `collector.ContainingPoint(XYZ)`
- `collector.ContainingPoint(XYZ, double)`
- `collector.NotContainingPoint(XYZ)`
- `collector.NotContainingPoint(XYZ, double)`
- `collector.IntersectingElement(Element)`
- `collector.NotIntersectingElement(Element)`
- `collector.IntersectingSolid(Solid)`
- `collector.NotIntersectingSolid(Solid)`
- `collector.SelectableInView(Document, ElementId)`
- `collector.SelectableInView(View)`
- `collector.NotSelectableInView(Document, ElementId)`
- `collector.NotSelectableInView(View)`
- `collector.WhereParameter(BuiltInParameter)`
- `collector.WhereParameter(ElementId)`
- `collector.First()`
- `collector.FirstOrDefault()`
- `collector.Count()`
- `collector.Any()`

#### ParameterFilterRuleFactory

- `collector.WhereParameter(parameter).Equals(string)`
- `collector.WhereParameter(parameter).Equals(int)`
- `collector.WhereParameter(parameter).Equals(double, double)`
- `collector.WhereParameter(parameter).Equals(ElementId)`
- `collector.WhereParameter(parameter).NotEquals(string)`
- `collector.WhereParameter(parameter).NotEquals(int)`
- `collector.WhereParameter(parameter).NotEquals(double, double)`
- `collector.WhereParameter(parameter).NotEquals(ElementId)`
- `collector.WhereParameter(parameter).IsGreaterThan(string)`
- `collector.WhereParameter(parameter).IsGreaterThan(int)`
- `collector.WhereParameter(parameter).IsGreaterThan(double)`
- `collector.WhereParameter(parameter).IsGreaterThan(double, double)`
- `collector.WhereParameter(parameter).IsGreaterThan(ElementId)`
- `collector.WhereParameter(parameter).IsGreaterThanOrEqualTo(string)`
- `collector.WhereParameter(parameter).IsGreaterThanOrEqualTo(int)`
- `collector.WhereParameter(parameter).IsGreaterThanOrEqualTo(double, double)`
- `collector.WhereParameter(parameter).IsGreaterThanOrEqualTo(ElementId)`
- `collector.WhereParameter(parameter).IsLessThan(string)`
- `collector.WhereParameter(parameter).IsLessThan(int)`
- `collector.WhereParameter(parameter).IsLessThan(double, double)`
- `collector.WhereParameter(parameter).IsLessThan(ElementId)`
- `collector.WhereParameter(parameter).IsLessThanOrEqualTo(string)`
- `collector.WhereParameter(parameter).IsLessThanOrEqualTo(int)`
- `collector.WhereParameter(parameter).IsLessThanOrEqualTo(double, double)`
- `collector.WhereParameter(parameter).IsLessThanOrEqualTo(ElementId)`
- `collector.WhereParameter(parameter).Contains(string)`
- `collector.WhereParameter(parameter).NotContains(string)`
- `collector.WhereParameter(parameter).StartsWith(string)`
- `collector.WhereParameter(parameter).NotStartsWith(string)`
- `collector.WhereParameter(parameter).EndsWith(string)`
- `collector.WhereParameter(parameter).NotEndsWith(string)`
- `collector.WhereParameter(parameter).HasValue()`
- `collector.WhereParameter(parameter).HasNoValue()`
- `collector.WhereParameter(parameter).IsAssociatedWithGlobalParameter(ElementId)`
- `collector.WhereParameter(parameter).IsNotAssociatedWithGlobalParameter(ElementId)`

### Families and modeling

#### Family

- `family.CanBeConvertedToFaceHostBased`
- `family.ConvertToFaceHostBased()`
- `family.CheckIntegrity()`
- `elementId.CanBeConvertedToFaceHostBased(Document)`
- `elementId.ConvertToFaceHostBased(Document)`
- `elementId.CheckFamilyIntegrity(Document)`
- `Form.CanBeDissolved(Document, ICollection<ElementId>)`
- `Form.DissolveForms(Document, ICollection<ElementId>)`
- `Form.DissolveForms(Document, ICollection<ElementId>, out ICollection<ElementId>)`

#### FamilySymbol

- `familySymbol.IsAdaptiveFamilySymbol`
- `familySymbol.CreateAdaptiveComponentInstance()`
- `familySymbol.SetStructuralSection(StructuralSection)`
- `FamilySymbol.GetProfileSymbols(Document, ProfileFamilyUsage, bool)`

#### FamilyInstance

- `familyInstance.IsVoidInstanceCuttingElement`
- `familyInstance.GetElementsBeingCut()`
- `familyInstance.GetStructuralSection()`
- `familyInstance.GetStructuralElementDefinitionData(out StructuralElementDefinitionData)`

#### Wall

- `wall.IsJoinAllowedAtEnd(int)`
- `wall.AllowJoinAtEnd(int)`
- `wall.DisallowJoinAtEnd(int)`

#### Adaptive components

- `family.IsAdaptiveComponentFamily`
- `family.GetNumberOfAdaptivePoints()`
- `family.GetNumberOfAdaptivePlacementPoints()`
- `family.GetNumberOfAdaptiveShapeHandlePoints()`
- `familySymbol.IsAdaptiveFamilySymbol`
- `familySymbol.CreateAdaptiveComponentInstance()`
- `familyInstance.HasAdaptiveFamilySymbol`
- `familyInstance.IsAdaptiveComponentInstance`
- `familyInstance.IsAdaptiveInstanceFlipped`
- `familyInstance.SetAdaptiveInstanceFlipped(bool)`
- `familyInstance.MoveAdaptiveComponentInstance(Transform, bool)`
- `familyInstance.GetAdaptivePlacementPointElementRefIds()`
- `familyInstance.GetAdaptivePointElementRefIds()`
- `familyInstance.GetAdaptiveShapeHandlePointElementRefIds()`
- `referencePoint.IsAdaptivePlacementPoint`
- `referencePoint.IsAdaptivePoint`
- `referencePoint.IsAdaptiveShapeHandlePoint`
- `referencePoint.MakeAdaptivePoint(AdaptivePointType)`
- `referencePoint.GetAdaptivePlacementNumber()`
- `referencePoint.GetAdaptivePointConstraintType()`
- `referencePoint.GetAdaptivePointOrientationType()`
- `referencePoint.SetAdaptivePlacementNumber(int)`
- `referencePoint.SetAdaptivePointConstraintType(AdaptivePointConstraintType)`
- `referencePoint.SetAdaptivePointOrientationType(AdaptivePointOrientationType)`

#### Annotation

- `element.IsMultiAlignSupported`
- `element.GetAnnotationOutlineWithoutLeaders()`
- `element.MoveWithAnchoredLeaders(XYZ)`

#### Detail

- `element.IsDetailElement(View)`
- `element.BringForward(View)`
- `element.BringToFront(View)`
- `element.SendBackward(View)`
- `element.SendToBack(View)`
- `elementId.IsDetailElement(Document, View)`
- `elementId.BringForward(Document, View)`
- `elementId.BringToFront(Document, View)`
- `elementId.SendBackward(Document, View)`
- `elementId.SendToBack(Document, View)`
- `elementIds.AreDetailElements(Document, View)`
- `elementIds.BringForward(Document, View)`
- `elementIds.BringToFront(Document, View)`
- `elementIds.SendBackward(Document, View)`
- `elementIds.SendToBack(Document, View)`
- `view.GetDrawOrderForDetails(ISet<ElementId>)`

#### Parts

- `element.HasAssociatedParts`
- `element.GetAssociatedParts(bool, bool)`
- `element.GetAssociatedPartMaker()`
- `elementId.HasAssociatedParts(Document)`
- `elementId.GetAssociatedParts(Document, bool, bool)`
- `elementId.GetAssociatedPartMaker(Document)`
- `elementId.GetSplittingCurves(Document)`
- `elementId.GetSplittingCurves(Document, out Plane)`
- `elementId.GetSplittingElements(Document)`
- `linkElementId.IsValidForCreateParts(Document)`
- `linkElementId.HasAssociatedParts(Document)`
- `linkElementId.GetAssociatedParts(Document, bool, bool)`
- `linkElementId.GetAssociatedPartMaker(Document)`
- `part.IsMergedPart`
- `part.IsPartDerivedFromLink`
- `part.GetChainLengthToOriginal()`
- `part.GetMergedParts()`
- `part.GetSplittingCurves()`
- `part.GetSplittingCurves(out Plane)`
- `part.GetSplittingElements()`
- `partMaker.GetPartMakerMethodToDivideVolumeFw()`
- `elementIds.AreElementsValidForCreateParts(Document)`
- `elementIds.ArePartsValidForDivide(Document)`
- `elementIds.ArePartsValidForMerge(Document)`
- `elementIds.CreateParts(Document)`
- `elementIds.CreateMergedPart(Document)`
- `elementIds.DivideParts(Document, ICollection<ElementId>, IList<Curve>, ElementId)`
- `elementIds.FindMergeableClusters(Document)`
- `hostOrLinkElements.CreateParts(Document)`

#### Assembly

- `assemblyInstance.AcquireViews(AssemblyInstance)`
- `assemblyInstance.Create3DOrthographic()`
- `assemblyInstance.Create3DOrthographic(ElementId, bool)`
- `assemblyInstance.CreateDetailSection(AssemblyDetailViewOrientation)`
- `assemblyInstance.CreateDetailSection(AssemblyDetailViewOrientation, ElementId, bool)`
- `assemblyInstance.CreateMaterialTakeoff()`
- `assemblyInstance.CreateMaterialTakeoff(ElementId, bool)`
- `assemblyInstance.CreatePartList()`
- `assemblyInstance.CreatePartList(ElementId, bool)`
- `assemblyInstance.CreateSheet(ElementId)`
- `assemblyInstance.CreateSingleCategorySchedule(ElementId)`
- `assemblyInstance.CreateSingleCategorySchedule(ElementId, ElementId, bool)`

#### Mass

- `massInstance.IsMassFamilyInstance`
- `massInstance.GetMassGrossFloorArea()`
- `massInstance.GetMassGrossSurfaceArea()`
- `massInstance.GetMassGrossVolume()`
- `massInstance.GetMassLevelDataIds()`
- `massInstance.GetMassJoinedElementIds()`
- `massInstance.GetMassLevelIds()`
- `massInstance.AddMassLevelData(ElementId)`
- `massInstance.RemoveMassLevelData(ElementId)`
- `massInstanceId.IsMassFamilyInstance(Document)`
- `massInstanceId.GetMassGrossFloorArea(Document)`
- `massInstanceId.GetMassGrossSurfaceArea(Document)`
- `massInstanceId.GetMassGrossVolume(Document)`
- `massInstanceId.GetMassLevelDataIds(Document)`
- `massInstanceId.GetMassJoinedElementIds(Document)`
- `massInstanceId.GetMassLevelIds(Document)`
- `massInstanceId.AddMassLevelData(Document, ElementId)`
- `massInstanceId.RemoveMassLevelData(Document, ElementId)`

### Disciplines

#### MEP

##### Pipe

- `pipe.HasOpenConnector`
- `pipe.PlaceCapOnOpenEnds()`
- `pipe.PlaceCapOnOpenEnds(ElementId)`
- `pipe.BreakCurve(XYZ)`
- `connector.ConnectPipePlaceholdersAtElbow(Connector)`
- `connector.ConnectPipePlaceholdersAtTee(Connector, Connector)`
- `connector.ConnectPipePlaceholdersAtCross(Connector, Connector, Connector)`
- `placeholderIds.ConvertPipePlaceholders(Document)`

##### Duct

- `duct.BreakCurve(XYZ)`
- `duct.ConnectAirTerminal(ElementId)`
- `connector.ConnectDuctPlaceholdersAtElbow(Connector)`
- `connector.ConnectDuctPlaceholdersAtTee(Connector, Connector)`
- `connector.ConnectDuctPlaceholdersAtCross(Connector, Connector, Connector)`
- `placeholderIds.ConvertDuctPlaceholders(Document)`

##### Fabrication

- `document.ExportToPcf(string, IList<ElementId>)`
- `connector.ValidateFabricationConnectivity(Connector)`

##### MEP Support

- `document.NewDuctworkStiffener(FamilySymbol, Element, double)`

#### Structure

##### Rebar

- `rebar.CanBeSpliced(RebarSpliceOptions, Line, XYZ)`
- `rebar.CanBeSpliced(RebarSpliceOptions, Line, ElementId)`
- `rebar.CanBeSpliced(RebarSpliceOptions, RebarSpliceGeometry)`
- `rebar.GetLapDirectionForSpliceGeometryAndPosition(RebarSpliceGeometry, RebarSplicePosition)`
- `rebar.GetSpliceChain()`
- `rebar.GetSpliceGeometries(RebarSpliceOptions, RebarSpliceRules)`
- `rebar.Splice(RebarSpliceOptions, Line, XYZ)`
- `rebar.Splice(RebarSpliceOptions, Line, ElementId)`
- `rebar.Splice(RebarSpliceOptions, IList<RebarSpliceGeometry>)`
- `rebar.UnifyRebarsIntoOne(ElementId)`
- `elementId.GetRebarSpliceGeometries(Document, RebarSpliceOptions, RebarSpliceRules)`
- `elementId.SpliceRebar(Document, RebarSpliceOptions, Line, XYZ)`
- `elementId.SpliceRebar(Document, RebarSpliceOptions, Line, ElementId)`
- `elementId.SpliceRebar(Document, RebarSpliceOptions, IList<RebarSpliceGeometry>)`
- `elementId.UnifyRebarsIntoOne(Document, ElementId)`
- `sourceRebars.AlignByFace(Document, Reference, Reference)`
- `sourceRebars.AlignByHost(Document, Element)`
- `rebarShape.GetAllParameters()`
- `externalDefinition.IsValidRebarShapeParameter`
- `externalDefinition.GetRebarShapeParameterElementId(Document)`
- `externalDefinition.GetOrCreateRebarShapeParameterElementId(Document)`
- `definitionFile.SearchExternalDefinition(Document, ElementId)`
- `definitionFile.SearchExternalDefinition(Parameter)`
- `document.NewRebarSpliceType(string)`
- `document.NewRebarCrankType(string)`
- `element.GetRebarSpliceLapLengthMultiplier()`
- `element.GetRebarSpliceShiftOption()`
- `element.GetRebarSpliceStaggerLengthMultiplier()`
- `element.SetRebarSpliceLapLengthMultiplier(double)`
- `element.SetRebarSpliceShiftOption(RebarSpliceShiftOption)`
- `element.SetRebarSpliceStaggerLengthMultiplier(double)`
- `element.GetRebarCrankLengthMultiplier()`
- `element.GetRebarCrankOffsetMultiplier()`
- `element.GetRebarCrankRatio()`
- `element.SetRebarCrankLengthMultiplier(double)`
- `element.SetRebarCrankOffsetMultiplier(double)`
- `element.SetRebarCrankRatio(double)`
- `elementId.GetRebarSpliceLapLengthMultiplier(Document)`
- `elementId.GetRebarSpliceShiftOption(Document)`
- `elementId.GetRebarSpliceStaggerLengthMultiplier(Document)`
- `elementId.SetRebarSpliceLapLengthMultiplier(Document, double)`
- `elementId.SetRebarSpliceShiftOption(Document, RebarSpliceShiftOption)`
- `elementId.SetRebarSpliceStaggerLengthMultiplier(Document, double)`
- `elementId.GetRebarCrankLengthMultiplier(Document)`
- `elementId.GetRebarCrankOffsetMultiplier(Document)`
- `elementId.GetRebarCrankRatio(Document)`
- `elementId.SetRebarCrankLengthMultiplier(Document, double)`
- `elementId.SetRebarCrankOffsetMultiplier(Document, double)`
- `elementId.SetRebarCrankRatio(Document, double)`

##### Structural framing

- `familyInstance.CanFlipFramingEnds`
- `familyInstance.FlipFramingEnds()`
- `familyInstance.IsFramingJoinAllowedAtEnd(int)`
- `familyInstance.AllowFramingJoinAtEnd(int)`
- `familyInstance.DisallowFramingJoinAtEnd(int)`
- `familyInstance.GetFramingEndReference(int)`
- `familyInstance.IsFramingEndReferenceValid(int, Reference)`
- `familyInstance.CanSetFramingEndReference(int)`
- `familyInstance.SetFramingEndReference(int, Reference)`
- `familyInstance.RemoveFramingEndReference(int)`

##### Structural sections

- `familyInstance.GetStructuralSection()`
- `familyInstance.GetStructuralElementDefinitionData(out StructuralElementDefinitionData)`
- `familySymbol.SetStructuralSection(StructuralSection)`
- `elementId.GetStructuralSection(Document)`
- `elementId.SetStructuralSection(Document, StructuralSection)`
- `elementId.GetStructuralElementDefinitionData(Document, out StructuralElementDefinitionData)`

#### Analytical

- `element.IsAnalyticalElement`
- `element.IsPhysicalElement`
- `elementId.IsAnalyticalElement(Document)`
- `elementId.IsPhysicalElement(Document)`
- `document.GetAnalyticalToPhysicalAssociationManager()`

### Model access and interoperability

#### ModelPath

- `modelPath.ConvertToUserVisiblePath()`
- `modelPath.CreateNewLocal(ModelPath)`
- `modelPath.GetUserWorksetInfo()`
- `modelGuid.ConvertToCloudPath(Guid, string)`
- `application.GetAllCloudRegions()`

#### Worksharing

- `element.GetCheckoutStatus()`
- `element.GetCheckoutStatus(out string)`
- `element.GetWorksharingTooltipInfo()`
- `element.GetModelUpdatesStatus()`
- `elementId.GetCheckoutStatus(Document)`
- `elementId.GetCheckoutStatus(Document, out string)`
- `elementId.GetWorksharingTooltipInfo(Document)`
- `elementId.GetModelUpdatesStatus(Document)`
- `document.RelinquishOwnership(RelinquishOptions, TransactWithCentralOptions)`
- `worksets.CheckoutWorksets(Document)`
- `worksets.CheckoutWorksets(Document, TransactWithCentralOptions)`
- `elementIds.CheckoutElements(Document)`
- `elementIds.CheckoutElements(Document, TransactWithCentralOptions)`

#### Coordination model

- `element.IsCoordinationModelInstance`
- `element.IsCoordinationModelType`
- `element.GetCoordinationModelVisibilityOverride(View)`
- `element.SetCoordinationModelVisibilityOverride(View, bool)`
- `element.GetAllPropertiesForReferenceInsideCoordinationModel(Reference)`
- `element.GetCategoryForReferenceInsideCoordinationModel(Reference)`
- `element.GetVisibilityOverrideForReferenceInsideCoordinationModel(View, Reference)`
- `element.SetVisibilityOverrideForReferenceInsideCoordinationModel(View, Reference, bool)`
- `elementType.GetCoordinationModelTypeData()`
- `elementType.GetCoordinationModelColorOverride(View)`
- `elementType.SetCoordinationModelColorOverride(View, Color)`
- `elementType.GetCoordinationModelTransparencyOverride(View)`
- `elementType.SetCoordinationModelTransparencyOverride(View, int)`
- `elementType.ContainsCoordinationModelCategory(string)`
- `elementType.GetCoordinationModelColorOverrideForCategory(View, string)`
- `elementType.SetCoordinationModelColorOverrideForCategory(View, string, Color)`
- `elementType.GetCoordinationModelVisibilityOverrideForCategory(View, string)`
- `elementType.SetCoordinationModelVisibilityOverrideForCategory(View, string, bool)`
- `elementType.ReloadCoordinationModel()`
- `elementType.ReloadAutodeskDocsCoordinationModelFrom(string, string, string, string)`
- `elementType.ReloadLocalCoordinationModelFrom(string)`
- `elementType.UnloadCoordinationModel()`
- `document.GetAllCoordinationModelInstanceIds()`
- `document.GetAllCoordinationModelTypeIds()`
- `document.LinkCoordinationModelFromLocalPath(string, CoordinationModelLinkOptions)`
- `document.Link3DViewFromAutodeskDocs(string, string, string, string, CoordinationModelLinkOptions)`

#### Export

- `element.ExportId`
- `subelement.ExportId`
- `elementId.GetExportId(Document)`
- `document.GbXmlId`
- `surface.GetNurbsSurfaceData()`

#### External files

- `element.IsExternalFileReference`
- `element.GetExternalFileReference()`
- `elementId.IsExternalFileReference(Document)`
- `elementId.GetExternalFileReference(Document)`
- `document.GetAllExternalFileReferences()`

#### External resources

- `document.GetAllExternalResourceReferences()`
- `document.GetAllExternalResourceReferences(ExternalResourceType)`
- `resourceType.GetServers()`
- `externalResourceReference.ServerSupportsAssemblyCodeData`
- `externalResourceReference.ServerSupportsCadLinks`
- `externalResourceReference.ServerSupportsIfcLinks`
- `externalResourceReference.ServerSupportsKeynotes`
- `externalResourceReference.ServerSupportsRevitLinks`
- `ExternalResourceReference.IsValidShortName(Guid, string)`

#### Point clouds

- `filter.GetFilteredOutline(Outline)`

### DirectContext3D

- `category.IsADirectContext3DHandleCategory`
- `category.GetDirectContext3DHandleInstances(Document)`
- `category.GetDirectContext3DHandleTypes(Document)`
- `element.IsADirectContext3DHandleInstance`
- `element.IsADirectContext3DHandleType`

## Breaking Changes

**The entire library has been rewritten using C# 14 extension member syntax.**
All extension methods are now declared inside `extension(T)` blocks, which means they participate in member lookup and appear in IntelliSense alongside native Revit API members. There are no changes to call sites — all
existing code continues to compile without modification.

**Methods converted to properties.**
The following boolean methods have been converted to read-only properties to align with modern C# conventions and Revit API style:

- `element.CanBeDeleted` (was `CanDeleteElement()`)
- `element.CanBeMirrored` (was `CanMirrorElement()`)
- `family.CanBeConvertedToFaceHostBased` (was `CanConvertToFaceHostBased()`)
- `element.IsAnalyticalElement` (was `IsAnalyticalElement()`)
- `element.IsPhysicalElement` (was `IsPhysicalElement()`)
- `document.AreGlobalParametersAllowed` (was `AreGlobalParametersAllowed()`)
- `typeId.IsBuiltInParameter` (was `IsBuiltInParameter()`)
- `parameter.IsBuiltInParameter` (was `IsBuiltInParameter()`)
- `typeId.IsBuiltInGroup` (was `IsBuiltInGroup()`)
- `typeId.IsSpec` (was `IsSpec()`)
- `typeId.IsValidDataType` (was `IsValidDataType()`)
- `typeId.IsSymbol` (was `IsSymbol()`)
- `typeId.IsUnit` (was `IsUnit()`)
- `typeId.IsMeasurableSpec` (was `IsMeasurableSpec()`)
- `pipe.HasOpenConnector` (was `HasOpenConnector()`)
- `element.IsAllowedForSolidCut` (was `IsAllowedForSolidCut()`)
- `element.IsElementFromAppropriateContext` (was `IsElementFromAppropriateContext()`)
- `solid.IsValidForTessellation` (was `IsValidForTessellation()`)
- `familyInstance.CanFlipFramingEnds` (was `CanFlipFramingEnds()`)
- `family.CanBeConvertedToFaceHostBased` (was `CanBeConvertedToFaceHostBased()`)
- `externalDefinition.IsValidRebarShapeParameter` (was `IsValidRebarShapeParameter()`)
- `referencePoint.IsAdaptivePlacementPoint` (was `IsAdaptivePlacementPoint`)
- `referencePoint.IsAdaptivePoint` (was `IsAdaptivePoint`)
- `referencePoint.IsAdaptiveShapeHandlePoint` (was `IsAdaptiveShapeHandlePoint`)

**Renamed methods.**
Old names are marked `[Obsolete]` with `[CodeTemplate]` attributes that provide automatic IDE quick-fixes:

- `CanDeleteElement()` → `CanBeDeleted`
- `CanMirrorElement()` → `CanBeMirrored`
- `CanMirrorElements()` → `CanBeMirrored`
- `CanConvertToFaceHostBased()` → `CanBeConvertedToFaceHostBased`
- `AreEquals(BuiltInCategory)` → `IsCategory(BuiltInCategory)`
- `AreEquals(BuiltInParameter)` → `IsParameter(BuiltInParameter)`

**Namespace changes.**
Several extension classes have been moved to dedicated namespaces to prevent type resolution conflicts in headless environments such as unit tests and Revit Design Automation:

| Class                                              | Old namespace                 | New namespace                              |
|----------------------------------------------------|-------------------------------|--------------------------------------------|
| `RibbonExtensions`                                 | `Nice3point.Revit.Extensions` | `Nice3point.Revit.Extensions.UI`           |
| `ContextMenuExtensions`                            | `Nice3point.Revit.Extensions` | `Nice3point.Revit.Extensions.UI`           |
| `UiApplicationExtensions`                          | `Nice3point.Revit.Extensions` | `Nice3point.Revit.Extensions.UI`           |
| `PresentationFrameworkExtensions`                  | `Nice3point.Revit.Extensions` | `Nice3point.Revit.Extensions.UI`           |
| `SystemExtensions`                                 | `Nice3point.Revit.Extensions` | `Nice3point.Revit.Extensions.Runtime`      |
| `AnalyticalToPhysicalAssociationManagerExtensions` | `Nice3point.Revit.Extensions` | `Nice3point.Revit.Extensions.Structure`    |
| `LightGroupManagerExtensions`                      | `Nice3point.Revit.Extensions` | `Nice3point.Revit.Extensions.Lighting`     |
| `SpatialFieldManagerExtensions`                    | `Nice3point.Revit.Extensions` | `Nice3point.Revit.Extensions.Analysis`     |
| `PlumbingUtilsExtensions`                          | `Nice3point.Revit.Extensions` | `Nice3point.Revit.Extensions.Plumbing`     |
| `MechanicalUtilsExtensions`                        | —                             | `Nice3point.Revit.Extensions.Mechanical`   |
| `CoordinationModelLinkUtilsExtensions`             | —                             | `Nice3point.Revit.Extensions.ExternalData` |

**Removed members.**
The following members have been removed without a replacement or superseded by built-in language and framework features:

- `SystemExtensions.AppendPath(string, params string[])` — use `Path.Combine` directly
- `SystemExtensions.Contains(string, StringComparison)` — use Polyfills package

**ImperialExtensions** are marked `[Obsolete]`. Use the **UnitsNet** package instead.

# 2026.0.1

This update focuses on increased utility class coverage, new extensions for global parameter management, for ForgeTypeId handling, advanced geometry extensions, and many-many more.

## New Extensions

**Ribbon Extensions**

- **AddStackPanel:** Adds a vertical stack panel to the specified Ribbon panel.
- **AddPushButton:** Adds a PushButton to the vertical stack panel.
- **AddPullDownButton:** Adds a PullDownButton to the vertical stack panel.
- **AddSplitButton:** Adds a SplitButton to the vertical stack panel.
- **AddComboBox:** Adds a ComboBox to the vertical stack panel.
- **AddTextBox:** Adds a TextBox to the vertical stack panel.
- **AddLabel:** Adds a text label to the vertical stack panel.

    ![verticalStack](https://github.com/user-attachments/assets/3cef1e86-89a3-4f9c-8a06-b7661c6f428f)

- **RemovePanel:** Removes RibbonPanel from the Revit ribbon.
- **SetBackground:** Sets the RibbonPanel background color.
- **SetTitleBarBackground:** Sets the RibbonPanel title bar background color.
- **SetSlideOutPanelBackground:** Sets the slide-out panel background color for the target RibbonPanel.
- **SetToolTip:** Sets the tooltip text for the PushButton.
- **SetLongDescription:** Sets the extended tooltip description for the RibbonItem.
- **CreatePanel:** Now works with internal panels. Panel can be added to the Modify, View, Manage, and other tabs.
- **SetImage:** Support for Light and Dark UI themes.
- **SetLargeImage:** Support for Light and Dark UI themes.
- **AddShortcuts:** Adds keyboard shortcuts to the RibbonButton.

**ElementId Extensions**

- **ToElements:** Retrieves a collection of Elements associated with the specified ElementIds.
- **ToElements<T>:** Retrieves a collection of Elements associated with the specified ElementIds as the specified type T.
- **ToOrderedElements:** Retrieves the Elements associated with the specified ElementIds in their original order.
- **ToOrderedElements<T>:** Retrieves the Elements associated with the specified ElementIds and casts them to the specified type T in their original order.

**ElementId Transform Extensions**

- **CanMirrorElements:** Verifies whether a set of elements can be mirrored.
- **MirrorElements:** Mirrors elements across a plane, with support for mirrored copies.
- **MoveElements:** Moves elements according to a specified transformation.
- **RotateElements:** Rotates elements around a given axis by a specified angle.
- **CopyElements:** Copies elements between views or within the same document, with options for translation and transformation.

**Geometry Extensions**

- **Contains:** Determines if a point or another bounding box is contained within the bounding box, with strict mode available for more precise containment checks.
- **Overlaps:** Checks whether two bounding boxes overlap.
- **ComputeCentroid:** Calculates the geometric center of a bounding box.
- **ComputeVertices:** Returns the coordinates of the eight vertices of a bounding box.
- **ComputeVolume:** Computes the volume enclosed by the bounding box.
- **ComputeSurfaceArea:** Calculates the total surface area of the bounding box.

**Parameters Extensions**

- **IsBuiltInParameter:** Verifies if a parameter is a built-in parameter.

**Document Global Parameters Extensions**

- **FindGlobalParameter:** Locates a global parameter by name in the document.
- **GetAllGlobalParameters:** Returns all global parameters in the document.
- **GetGlobalParametersOrdered:** Retrieves ordered global parameters.
- **SortGlobalParameters:** Sorts global parameters in the specified order.
- **MoveUpOrder:** Moves a global parameter up in the order.
- **MoveDownOrder:** Moves a global parameter down in the order.
- **IsUniqueGlobalParameterName:** Checks if a global parameter name is unique.
- **IsValidGlobalParameter:** Validates if an ElementId is a global parameter.
- **AreGlobalParametersAllowed:** Checks if global parameters are permitted in the document.

**Element Association Extensions**

- **IsAnalyticalElement:** Determines whether an element is an analytical element.
- **IsPhysicalElement:** Checks if an element is a physical one.

**Element Validation Extensions**

- **CanDeleteElement:** Indicates if an element can be deleted.

**Element Worksharing Extensions**

- **GetCheckoutStatus:** Retrieves the ownership status of an element, with an optional parameter to return the owner's name.
- **GetWorksharingTooltipInfo:** Provides worksharing information about an element for in-canvas tooltips.
- **GetModelUpdatesStatus:** Gets the status of an element in the central model.

**Document Extensions**

- **GetProfileSymbols:** Returns profile family symbols in the document.
- **RelinquishOwnership:** Allows relinquishment of ownership based on specified options.

**Document Managers Extensions**

- **GetTemporaryGraphicsManager:** Fetches a reference to the document’s temporary graphics manager.
- **GetAnalyticalToPhysicalAssociationManager:** Retrieves the manager for analytical-to-physical element associations.
- **GetLightGroupManager:** Creates or retrieves the light group manager for the document.

**ForgeTypeId Extensions**

- **IsSpec:** Validates if a ForgeTypeId identifies a spec.
- **IsBuiltInGroup:** Determines whether a ForgeTypeId identifies a built-in parameter group.
- **IsBuiltInParameter:** Validates if a ForgeTypeId identifies a built-in parameter.
- **IsSymbol:** Checks if a ForgeTypeId identifies a symbol.
- **IsUnit:** Verifies if a ForgeTypeId identifies a unit.
- **IsValidDataType:** Determines if a ForgeTypeId identifies a valid parameter data type.
- **IsValidUnit:** Validates if a unit is valid for a measurable spec.
- **IsMeasurableSpec:** Checks if a ForgeTypeId represents a measurable spec.
- **GetBuiltInParameter:** Retrieves a BuiltInParameter from a ForgeTypeId.
- **GetParameterTypeId:** Fetches the ForgeTypeId corresponding to a BuiltInParameter.
- **GetDiscipline:** Gets the discipline for a measurable spec.
- **GetValidUnits:** Retrieves all valid units for a measurable spec.
- **GetTypeCatalogStringForSpec:** Fetches the type catalog string for a measurable spec.
- **GetTypeCatalogStringForUnit:** Retrieves the type catalog string for a unit.
- **DownloadCompanyName:** Downloads the owning company name for a parameter and records it in the document.
- **DownloadParameterOptions:** Fetches settings related to a parameter from the Parameters Service.
- **DownloadParameter:** Creates a shared parameter element in a document based on a downloaded parameter definition.

**Color Extensions**

- **ToHex:** Converts a color to its hexadecimal representation.
- **ToHexInteger:** Returns the hexadecimal integer representation of a color.
- **ToRgb:** Provides the RGB representation of a color.
- **ToHsl:** Converts a color to its HSL representation.
- **ToHsv:** Converts a color to its HSV representation.
- **ToCmyk:** Retrieves the CMYK representation of a color.
- **ToHsb:** Converts a color to HSB.
- **ToHsi:** Converts a color to HSI format.
- **ToHwb:** Provides the HWB representation of a color.
- **ToNCol:** Converts a color to NCol format.
- **ToCielab:** Retrieves the Cielab representation of a color.
- **ToCieXyz:** Converts a color to CieXyz format.
- **ToFloat:** Returns the float representation of a color.
- **ToDecimal:** Returns the decimal representation of a color.

**Family Extensions**

- **CanConvertToFaceHostBased:** Indicates whether a family can be converted to a face-hosted version.
- **ConvertToFaceHostBased:** Converts a family to be face-host based.

**Plumbing Extensions**

- **ConnectPipePlaceholdersAtElbow:** Connects pipe placeholders that form an elbow connection.
- **ConnectPipePlaceholdersAtTee:** Connects pipe placeholders to form a Tee connection.
- **ConnectPipePlaceholdersAtCross:** Connects pipe placeholders to form a Cross connection.
- **PlaceCapOnOpenEnds:** Places caps on open pipe connectors.
- **HasOpenConnector:** Checks if a pipe curve has an open piping connector.
- **BreakCurve:** Splits a pipe curve at a specified point.

**Element Solid Cut Extensions**

- **GetCuttingSolids:** Retrieves solids that cut a given element.
- **GetSolidsBeingCut:** Returns solids that are cut by the given element.
- **IsAllowedForSolidCut:** Verifies if the element can be involved in a solid-solid cut.
- **IsElementFromAppropriateContext:** Checks if the element belongs to a suitable context for solid cuts.
- **CanElementCutElement:** Determines whether a cutting element can create a solid cut on a target element.
- **CutExistsBetweenElements:** Checks if there is an existing solid-solid cut between two elements.
- **AddCutBetweenSolids:** Adds a solid-solid cut between two elements.
- **RemoveCutBetweenSolids:** Removes an existing solid cut between two elements.
- **SplitFacesOfCuttingSolid:** Splits the faces of a cutting solid element.

**View Extensions**

- **GetTransformFromViewToView:** Returns a transformation to copy elements between two views.

**View Managers Extensions**

- **CreateSpatialFieldManager:** Creates a SpatialField manager for a given view.
- **GetSpatialFieldManager:** Retrieves the SpatialField manager for a specific view.

## Breaking changes

- **ToFraction:** Now uses "8" precision, instead of "32".
- **ToFraction:** Now suppress "0" for inches part.
- **SetAvailabilityController:** Now returns `PushButton` instead of `RibbonButton`.

**Readme** has been updated, you can find a detailed description and code samples in it.

# 2025.0.0

- Revit 2025 support
- New `FindParameter()` overloads with GUID and Definition. This method combines all API methods for getting a parameter, such as `get_Parameter`, `LookupParameter`, `GetParameter`. It also searches for a parameter in the element type if there is no such parameter in the element
- `GetParameter()` method is obsolete. Use `FindParameter()` instead
- New extensions to help you add context menu items without creating additional classes or specifying type names. Revit 2025 and higher

The **ConfigureContextMenu()** method registers an action used to configure a Context menu.

```c#
application.ConfigureContextMenu(menu =>
{
    menu.AddMenuItem<Command>("Menu title");
    menu.AddMenuItem<Command>("Menu title")
        .SetAvailabilityController<Controller>()
        .SetToolTip("Description");
});
```

You can also specify your own context menu title. By default, Revit uses the Application name

```c#
application.ConfigureContextMenu("Title", menu =>
{
    menu.AddMenuItem<Command>("Menu title");
});
```

The **AddMenuItem()** method adds a menu item to the Context Menu.

```c#
menu.AddMenuItem<Command>("Menu title");
```

The **AddSeparator()** method adds a separator to the Context Menu.

```c#
menu.AddSeparator();
```

The **AddSubMenu()** method adds a sub menu to the Context Menu.

```c#
var subMenu = new ContextMenu();
subMenu.AddMenuItem<Command>("Menu title");
subMenu.AddMenuItem<Command>("Menu title");

menu.AddSubMenu("Sub menu title", subMenu);
```

The **SetAvailabilityController()** method specifies the class type that decides the availability of menu item.

```c#
menuItem.SetAvailabilityController<Controller>()
```

# 2024.0.0

Revit 2024 support

# 2023.1.9

ElementExtensions:

- Method chain support

ElementIdExtensions:

- Fixed cast operations. Directly cast used by default (safe cast early)

SchemaExtensions:

- SaveEntity now return bool if the entity save was successful

SystemExtensions:

- AppendPath overload with params

# 2023.1.8

UnitExtensions:

- New FromUnit extension
- New ToUnit extension

# 2023.1.7

RibbonExtensions:

- New SetAvailabilityController() extension

ParameterExtensions:

- New Set(bool) extension
- New Set(color) extension

Nuget symbol server support: https://symbols.nuget.org/download/symbols

# 2023.1.6

CollectorExtensions:

- New GetElements(ElementId viewId) extension
- New GetElements(ICollection<ElementId> elementIds) extension
- New overloads for instances with ViewId

# 2023.1.5

New Parameter extensions

Fixed GetParameter extensions for cases where the parameter value was not initialized

# 2023.1.4

New Application extensions
New Collector extensions

Updated TargetFramework. It now matches the framework that Revit is running on

# 2023.1.3

New Schema extensions

# 2023.1.2

Geometry extensions

- New line.SetCoordinateX extension
- New line.SetCoordinateY extension
- New line.SetCoordinateZ extension
- New arc.SetCoordinateX extension
- New arc.SetCoordinateY extension
- New arc.SetCoordinateZ extension

# 2023.1.1

New JoinGeometryUtils extensions

# 2023.1.0

- Revit 2023 support
- Update attributes

# 2022.0.5

Unit extensions

- New FormatUnit extensions

Solid Extensions

- New Clone extension
- New CreateTransformed extension
- New SplitVolumes extension
- New IsValidForTessellation extension
- New TessellateSolidOrShell extension
- New FindAllEdgeEndPointsAtVertex extension

Ribbon Extensions

- New AddPushButton<TCommand> extensions
- Uri changed to UriKind.RelativeOrAbsolute

# 2022.0.4

Element extensions

- New GetParameter(ForgeTypeId) extension
- New Cast<T>() extension
- Removed CanBeNull attribute for ToElement<T> extension

Host Extensions

- New GetBottomFaces() extension
- New GetTopFaces() extension
- New GetSideFaces() extension

# 2022.0.3

Element extensions

- New Copy extension
- New Mirror extension
- New Move extension
- New Rotate extension
- New CanBeMirrored extension

Label Extensions

- New ToLabel(BuiltInParameter) extension
- New ToLabel(BuiltInParameterGroup) extension
- New ToLabel(BuiltInCategory) extension
- New ToLabel(DisplayUnitType) extension
- New ToLabel(ParameterType) extension
- New ToDisciplineLabel(ForgeTypeId) extension
- New ToGroupLabel(ForgeTypeId) extension
- New ToSpecLabel(ForgeTypeId) extension
- New ToSymbolLabel(ForgeTypeId) extension
- New ToUnitLabel(ForgeTypeId) extension

# 2022.0.2

Ribbon extensions

- New AddSplitButton extension
- New AddRadioButtonGroup extension
- New AddComboBox extension
- New AddTextBox extension

Unit extensions

- New FromMeters extension
- New ToMeters extension
- New FromDegrees extension
- New ToDegrees extension

ElementId extensions

- New AreEquals(BuiltInParameter) extension

# 2022.0.1

- Connected JetBrains annotations

# 2022.0.0

- Initial release