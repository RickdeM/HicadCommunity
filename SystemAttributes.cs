/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

namespace RDM.HicadCommunity
{
	/// <summary>
	/// List of System Atrributes
	/// </summary>
	public static class SystemAttributes
	{
		/// <summary>
		/// Accessory set
		/// </summary>
		public const string AccessorySet = "ACCE";

		/// <summary>
		/// Additional tolerance
		/// </summary>
		public const string AdditionalTolerance = "$ETOL";

		/// <summary>
		/// Angle
		/// </summary>
		public const string Angle = "ANGLE";

		/// <summary>
		/// Angle 1 of section schema
		/// </summary>
		public const string Angle1OfSectionSchema = "§14";

		/// <summary>
		/// Angle 2 of section schema
		/// </summary>
		public const string Angle2OfSectionSchema = "§15";

		/// <summary>
		/// Angle bottom/left - YZ
		/// </summary>
		public const string AngleBottomLeftYz = "§06";

		/// <summary>
		/// Angle bottom/left - XZ
		/// </summary>
		public const string AngleBottomLleftXz = "§05";

		/// <summary>
		/// Angle bottom/right - XZ
		/// </summary>
		public const string AngleBottomRightXz = "§07";

		/// <summary>
		/// Angle bottom/right - YZ
		/// </summary>
		public const string AngleBottomRightYz = "§08";

		/// <summary>
		/// Aperture angle
		/// </summary>
		public const string ApertureAngle = "P_BW";

		/// <summary>
		/// Arbitrarily divisible
		/// </summary>
		public const string ArbitrarilyDivisible = "DIVD";

		/// <summary>
		/// Article master
		/// </summary>
		public const string ArticleMaster = "$BK";

		/// <summary>
		/// Article number
		/// </summary>
		public const string ArticleNumber = "$BB";

		/// <summary>
		/// Assembling ID for bolts+screws
		/// </summary>
		public const string AssemblingIdForBoltsscrews = "%08";

		/// <summary>
		/// Automatic
		/// </summary>
		public const string Automatic = "#AD";

		/// <summary>
		/// Auxiliary body ID
		/// </summary>
		public const string AuxiliaryBodyId = "%07";

		/// <summary>
		/// Beam rotation for NCX export
		/// </summary>
		public const string BeamRotationForNcxExport = "%NCROT";

		/// <summary>
		/// Bend
		/// </summary>
		public const string Bend = "BEND";

		/// <summary>
		/// BOM-relevant
		/// </summary>
		public const string Bomrelevant = "#SR";

		/// <summary>
		/// Catalogue designation
		/// </summary>
		public const string CatalogueDesignation = "BZ";

		/// <summary>
		/// Coated surface area
		/// </summary>
		public const string CoatedSurfaceArea = "§CoatedSurfaceArea";

		/// <summary>
		/// Coating
		/// </summary>
		public const string Coating = "$15";

		/// <summary>
		/// Coating, external
		/// </summary>
		public const string CoatingExternal = "$18";

		/// <summary>
		/// Coating, internal
		/// </summary>
		public const string CoatingInternal = "$17";

		/// <summary>
		/// Coating type
		/// </summary>
		public const string CoatingType = "$BART";

		/// <summary>
		/// Comment
		/// </summary>
		public const string Comment = "$03";

		/// <summary>
		/// Commercial weight
		/// </summary>
		public const string CommercialWeight = "§18";

		/// <summary>
		/// Commercial weight per length
		/// </summary>
		public const string CommercialWeightPerLength = "§19";

		/// <summary>
		/// Connection number of attached part
		/// </summary>
		public const string ConnectionNumberOfAttachedPart = "%ATTACHED_CON_NUM";

		/// <summary>
		/// Connection type
		/// </summary>
		public const string ConnectionType = "CNT1";

		/// <summary>
		/// Connection type 2
		/// </summary>
		public const string ConnectionType2 = "CNT2";

		/// <summary>
		/// Connection type 3
		/// </summary>
		public const string ConnectionType3 = "CNT3";

		/// <summary>
		/// Connection type 4
		/// </summary>
		public const string ConnectionType4 = "CNT4";

		/// <summary>
		/// Construction section
		/// </summary>
		public const string ConstructionSection = "$CSEC";

		/// <summary>
		/// Copy of part name for HELiCON without HELiOS
		/// </summary>
		public const string CopyOfPartNameForHeliconWithoutHelios = "$TN_COPY";

		/// <summary>
		/// Cross-section equality for sheet metal
		/// </summary>
		public const string CrosssectionEqualityForSheetMetal = "%SU";

		/// <summary>
		/// Cross-section surface (cm**2)
		/// </summary>
		public const string CrosssectionSurface = "§CSA";

		/// <summary>
		/// Curve radius about y
		/// </summary>
		public const string CurveRadiusAboutY = "P_BRY";

		/// <summary>
		/// Curve radius about z
		/// </summary>
		public const string CurveRadiusAboutZ = "P_BRZ";

		/// <summary>
		/// Density RHO from material tables
		/// </summary>
		public const string DensityRhoFromMaterialTables = "§21";

		/// <summary>
		/// Designation 1
		/// </summary>
		public const string Designation1 = "$01";

		/// <summary>
		/// Designation 2
		/// </summary>
		public const string Designation2 = "$02";

		/// <summary>
		/// Deviating model
		/// </summary>
		public const string DeviatingModel = "%DVMD";

		/// <summary>
		/// Dimension 1
		/// </summary>
		public const string Dimension1 = "§09";

		/// <summary>
		/// Dimension 2
		/// </summary>
		public const string Dimension2 = "§M2";

		/// <summary>
		/// Dispatch item number
		/// </summary>
		public const string DispatchItemNumber = "%PI";

		/// <summary>
		/// Distance tolerance for calculation of actual surface area
		/// </summary>
		public const string DistanceToleranceForCalculationOfActualSurfaceArea = "§CoatedSurfaceAreaDist";

		/// <summary>
		/// Document master (Part to Part geometry)
		/// </summary>
		public const string DocumentMaster = "$DK";

		/// <summary>
		/// Document type Workshop drawing
		/// </summary>
		public const string DocumentTypeWorkshopDrawing = "%WSD";

		/// <summary>
		/// Drawing number
		/// </summary>
		public const string DrawingNumber = "$ZNR";

		/// <summary>
		/// DSTV Part designation
		/// </summary>
		public const string DstvPartDesignation = "$DSTV_N";

		/// <summary>
		/// DSTV part type
		/// </summary>
		public const string DstvPartType = "$DSTV_T";

		/// <summary>
		/// Editing status
		/// </summary>
		public const string EditingStatus = "%BIMP";

		/// <summary>
		/// Execution class,  weld seams
		/// </summary>
		public const string ExecutionClassWeldSeams = "$EXC23";

		/// <summary>
		/// Height
		/// </summary>
		public const string Height = "§04";

		/// <summary>
		/// HELiCON Configuration ID
		/// </summary>
		public const string HeliconConfigurationId = "CONFIGURATIONID";

		/// <summary>
		/// HELiCON Instance ID
		/// </summary>
		public const string HeliconInstanceId = "%INSTANCE_ID";

		/// <summary>
		/// HELiCON IsChanged flag
		/// </summary>
		public const string HeliconIschangedFlag = "%ISCHANGED";

		/// <summary>
		/// HELiCON IsSelected flag
		/// </summary>
		public const string HeliconIsselectedFlag = "%ISSELECTED";

		/// <summary>
		/// HELiCON Purchased part, marking
		/// </summary>
		public const string HeliconPurchasedPartMarking = "STATIC_PART";

		/// <summary>
		/// HELiCON Transfer info
		/// </summary>
		public const string HeliconTransferInfo = "TRANS_INFO";

		/// <summary>
		/// IFC element type
		/// </summary>
		public const string IfcElementType = "IfcType";

		/// <summary>
		/// IFC Global ID
		/// </summary>
		public const string IfcGlobalId = "IfcGuid";

		/// <summary>
		/// IFC Type object
		/// </summary>
		public const string IfcTypeObject = "IfcTypeObject";

		/// <summary>
		/// Ignore for dimensions
		/// </summary>
		public const string IgnoreForDimensions = "#NDR";

		/// <summary>
		/// Installation surface
		/// </summary>
		public const string InstallationSurface = "DWF_AREA";

		/// <summary>
		/// Internal key of inspection category
		/// </summary>
		public const string InternalKeyOfInspectionCategory = "%WCKEY";

		/// <summary>
		/// Item index
		/// </summary>
		public const string ItemIndex = "%PIDX";

		/// <summary>
		/// Itemisation project
		/// </summary>
		public const string ItemisationProject = "$PK";

		/// <summary>
		/// Itemised source model (BIM-PDM)
		/// </summary>
		public const string ItemisedSourceModel = "_ZNR";

		/// <summary>
		/// Item No.
		/// </summary>
		public const string ItemNo = "%02";

		/// <summary>
		/// Item number of 1st welded part
		/// </summary>
		public const string ItemNumberOf1stWeldedPart = "%ITNR_1";

		/// <summary>
		/// Item number of 2nd welded part
		/// </summary>
		public const string ItemNumberOf2ndWeldedPart = "%ITNR_2";

		/// <summary>
		/// Item text
		/// </summary>
		public const string ItemText = "$PTXT";

		/// <summary>
		/// Language-independent key of a Standard Parts catalogue
		/// </summary>
		public const string LanguageindependentKeyOfAStandardPartsCatalogue = "$PTK";

		/// <summary>
		/// Last HELiCON Instance ID
		/// </summary>
		public const string LastHeliconInstanceId = "%LAST_INSTANCE_ID";

		/// <summary>
		/// Length
		/// </summary>
		public const string Length = "§03";

		/// <summary>
		/// Length 1
		/// </summary>
		public const string Length1 = "LNG1";

		/// <summary>
		/// Length 2
		/// </summary>
		public const string Length2 = "LNG2";

		/// <summary>
		/// Length 3
		/// </summary>
		public const string Length3 = "LNG3";

		/// <summary>
		/// Length of 2-D development
		/// </summary>
		public const string LengthOf2dDevelopment = "§L2D";

		/// <summary>
		/// LogiKal facade
		/// </summary>
		public const string LogikalFacade = "%LFA";

		/// <summary>
		/// LogiKal item name
		/// </summary>
		public const string LogikalItemName = "$LPN";

		/// <summary>
		/// Main dimension
		/// </summary>
		public const string MainDimension = "$MM";

		/// <summary>
		/// Material designation
		/// </summary>
		public const string MaterialDesignation = "$07";

		/// <summary>
		/// Material length
		/// </summary>
		public const string MaterialLength = "§23";

		/// <summary>
		/// Material number
		/// </summary>
		public const string MaterialNumber = "$08";

		/// <summary>
		/// Modified
		/// </summary>
		public const string Modified = "#IP";

		/// <summary>
		/// Moment of inertia (cm**4)
		/// </summary>
		public const string MomentOfInertia = "§MOI";

		/// <summary>
		/// Moment of inertia IY
		/// </summary>
		public const string MomentOfInertiaIy = "IY";

		/// <summary>
		/// Moment of inertia IZ
		/// </summary>
		public const string MomentOfInertiaIz = "IZ";

		/// <summary>
		/// Name of attached part
		/// </summary>
		public const string NameOfAttachedPart = "%ATTACHED_COMP_NAME";

		/// <summary>
		/// Name of part
		/// </summary>
		public const string NameOfPart = "$TN";

		/// <summary>
		/// Nominal diameter
		/// </summary>
		public const string NominalDiameter = "DN1";

		/// <summary>
		/// Nominal diameter 2
		/// </summary>
		public const string NominalDiameter2 = "DN2";

		/// <summary>
		/// Nominal diameter 3
		/// </summary>
		public const string NominalDiameter3 = "DN3";

		/// <summary>
		/// Nominal diameter 4
		/// </summary>
		public const string NominalDiameter4 = "DN4";

		/// <summary>
		/// Nominal pressure
		/// </summary>
		public const string NominalPressure = "PRES";

		/// <summary>
		/// NPS inch
		/// </summary>
		public const string NpsInch = "DNI1";

		/// <summary>
		/// NPS inch 2
		/// </summary>
		public const string NpsInch2 = "DNI2";

		/// <summary>
		/// NPS inch 3
		/// </summary>
		public const string NpsInch3 = "DNI3";

		/// <summary>
		/// NPS inch 4
		/// </summary>
		public const string NpsInch4 = "DNI4";

		/// <summary>
		/// Number of section schema
		/// </summary>
		public const string NumberOfSectionSchema = "$06";

		/// <summary>
		/// Number/Quantity
		/// </summary>
		public const string NumberQuantity = "%PART_QUANT";

		/// <summary>
		/// One-sided coating for sheet metal parts
		/// </summary>
		public const string OnesidedCoatingForSheetMetalParts = "%12";

		/// <summary>
		/// Order note
		/// </summary>
		public const string OrderNote = "$BV";

		/// <summary>
		/// Order number
		/// </summary>
		public const string OrderNumber = "$ON";

		/// <summary>
		/// Outer diameter
		/// </summary>
		public const string OuterDiameter = "DA1";

		/// <summary>
		/// Outer diameter 2
		/// </summary>
		public const string OuterDiameter2 = "DA2";

		/// <summary>
		/// Outer diameter 3
		/// </summary>
		public const string OuterDiameter3 = "DA3";

		/// <summary>
		/// Outer diameter 4
		/// </summary>
		public const string OuterDiameter4 = "DA4";

		/// <summary>
		/// Overall width (mm)
		/// </summary>
		public const string OverallWidth = "DWF_COVER_WIDTH";

		/// <summary>
		/// Part-Item-ID (Model ID)
		/// </summary>
		public const string Partitemid = "%05";

		/// <summary>
		/// Part type
		/// </summary>
		public const string PartType1 = "$05";

		/// <summary>
		/// Part type
		/// </summary>
		public const string PartType2 = "%10";

		/// <summary>
		/// Part type
		/// </summary>
		public const string PartType3 = "PTYP";

		/// <summary>
		/// Part type ID
		/// </summary>
		public const string PartTypeId = "PKEY";

		/// <summary>
		/// P+ID symbol assignment
		/// </summary>
		public const string PIDSymbolAssignment = "$PID_ASGN";

		/// <summary>
		/// Placeholder part
		/// </summary>
		public const string PlaceholderPart = "PLANT_DUMMY_PART";

		/// <summary>
		/// Position realative to glass surface
		/// </summary>
		public const string PositionRealativeToGlassSurface = "%MGE";

		/// <summary>
		/// Preferred type
		/// </summary>
		public const string PreferredType = "PRTY";

		/// <summary>
		/// Pre-item number, fixed
		/// </summary>
		public const string PreitemNumberFixed = "%PIFIX";

		/// <summary>
		/// Pre-item number text
		/// </summary>
		public const string PreitemNumberText = "$PITXT";

		/// <summary>
		/// Processing note
		/// </summary>
		public const string ProcessingNote = "$BHW";

		/// <summary>
		/// Qty. in assembly
		/// </summary>
		public const string QtyInAssembly = "%13";

		/// <summary>
		/// Qty. per part
		/// </summary>
		public const string QtyPerPart = "%01";

		/// <summary>
		/// Quantity 1
		/// </summary>
		public const string Quantity1 = "§11";

		/// <summary>
		/// Quantity 2
		/// </summary>
		public const string Quantity2 = "§12";

		/// <summary>
		/// Quantity 3
		/// </summary>
		public const string Quantity3 = "§13";

		/// <summary>
		/// Repetion ID in DV
		/// </summary>
		public const string RepetionIdInDv = "_DV_RP";

		/// <summary>
		/// Ridder Stuklijst nummer
		/// </summary>
		public const string RidderStuklijstNummer = "RIQ_CODE";

		/// <summary>
		/// Schedule
		/// </summary>
		public const string Schedule = "SCHD";

		/// <summary>
		/// Seal
		/// </summary>
		public const string Seal = "SEAL";

		/// <summary>
		/// Section modulus (cm**3)
		/// </summary>
		public const string SectionModulus = "§MOR";

		/// <summary>
		/// Section modulus WY
		/// </summary>
		public const string SectionModulusWy = "WY";

		/// <summary>
		/// Section modulus WZ
		/// </summary>
		public const string SectionModulusWz = "WZ";

		/// <summary>
		/// Semi-finished product article master
		/// </summary>
		public const string SemifinishedProductArticleMaster = "$RBK";

		/// <summary>
		/// Semi-finished product revision
		/// </summary>
		public const string SemifinishedProductRevision = "#RBR";

		/// <summary>
		/// Semi-finished products
		/// </summary>
		public const string SemifinishedProducts = "#BT";

		/// <summary>
		/// Set ID for boltings
		/// </summary>
		public const string SetIdForBoltings = "%09";

		/// <summary>
		/// Sheet thickness
		/// </summary>
		public const string SheetThickness = "§T2D";

		/// <summary>
		/// Shipped length
		/// </summary>
		public const string ShippedLength = "§25";

		/// <summary>
		/// Shipping quantity
		/// </summary>
		public const string ShippingQuantity = "%PIQ";

		/// <summary>
		/// Standard
		/// </summary>
		public const string Standard = "NRM";

		/// <summary>
		/// Start value
		/// </summary>
		public const string StartValue = "ITEMNR_INITIALVALUE";

		/// <summary>
		/// Style
		/// </summary>
		public const string Style = "MDL";

		/// <summary>
		/// Supplied length
		/// </summary>
		public const string SuppliedLength = "SLNG";

		/// <summary>
		/// Surface
		/// </summary>
		public const string Surface = "§10";

		/// <summary>
		/// Surface per length
		/// </summary>
		public const string SurfacePerLength = "§17";

		/// <summary>
		/// Symbols for front views of blanks
		/// </summary>
		public const string SymbolsForFrontViewsOfBlanks = "$10";

		/// <summary>
		/// Symbols for top views of blanks
		/// </summary>
		public const string SymbolsForTopViewsOfBlanks = "$09";

		/// <summary>
		/// System notes
		/// </summary>
		public const string SystemNotes = "$04";

		/// <summary>
		/// Teknr Klant
		/// </summary>
		public const string TeknrKlant = "TEKKLNT";

		/// <summary>
		/// Thickness
		/// </summary>
		public const string Thickness = "THK";

		/// <summary>
		/// Total length
		/// </summary>
		public const string TotalLength = "§22";

		/// <summary>
		/// Total quantity
		/// </summary>
		public const string TotalQuantity = "%06";

		/// <summary>
		/// Trimmed length
		/// </summary>
		public const string TrimmedLength = "§24";

		/// <summary>
		/// Type designation of variant
		/// </summary>
		public const string TypeDesignationOfVariant = "$TY";

		/// <summary>
		/// Unambiguous DV ID
		/// </summary>
		public const string UnambiguousDvId = "_DV_KEY";

		/// <summary>
		/// Unit of quantity
		/// </summary>
		public const string UnitOfQuantity = "$16";

		/// <summary>
		/// Usage
		/// </summary>
		public const string Usage = "P_TYPE";

		/// <summary>
		/// Usage, ID 1
		/// </summary>
		public const string UsageId1 = "USETABLEID";

		/// <summary>
		/// Usage, ID 2
		/// </summary>
		public const string UsageId2 = "USEITEMID";

		/// <summary>
		/// User-defined attribute 1
		/// </summary>
		public const string UserdefinedAttribute1 = "CUSTOM1";

		/// <summary>
		/// User-defined attribute 2
		/// </summary>
		public const string UserdefinedAttribute2 = "CUSTOM2";

		/// <summary>
		/// User-defined Plant Engineering attribute 1
		/// </summary>
		public const string UserdefinedPlantEngineeringAttribute1 = "PLTATTR01";

		/// <summary>
		/// User-defined Plant Engineering attribute 10
		/// </summary>
		public const string UserdefinedPlantEngineeringAttribute10 = "PLTATTR10";

		/// <summary>
		/// User-defined Plant Engineering attribute 2
		/// </summary>
		public const string UserdefinedPlantEngineeringAttribute2 = "PLTATTR02";

		/// <summary>
		/// User-defined Plant Engineering attribute 3
		/// </summary>
		public const string UserdefinedPlantEngineeringAttribute3 = "PLTATTR03";

		/// <summary>
		/// User-defined Plant Engineering attribute 4
		/// </summary>
		public const string UserdefinedPlantEngineeringAttribute4 = "PLTATTR04";

		/// <summary>
		/// User-defined Plant Engineering attribute 5
		/// </summary>
		public const string UserdefinedPlantEngineeringAttribute5 = "PLTATTR05";

		/// <summary>
		/// User-defined Plant Engineering attribute 6
		/// </summary>
		public const string UserdefinedPlantEngineeringAttribute6 = "PLTATTR06";

		/// <summary>
		/// User-defined Plant Engineering attribute 7
		/// </summary>
		public const string UserdefinedPlantEngineeringAttribute7 = "PLTATTR07";

		/// <summary>
		/// User-defined Plant Engineering attribute 8
		/// </summary>
		public const string UserdefinedPlantEngineeringAttribute8 = "PLTATTR08";

		/// <summary>
		/// User-defined Plant Engineering attribute 9
		/// </summary>
		public const string UserdefinedPlantEngineeringAttribute9 = "PLTATTR09";

		/// <summary>
		/// U-value
		/// </summary>
		public const string Uvalue = "§26";

		/// <summary>
		/// Volume
		/// </summary>
		public const string Volume = "§20";

		/// <summary>
		/// Wall thickness
		/// </summary>
		public const string WallThickness = "WTH1";

		/// <summary>
		/// Wall thickness 2
		/// </summary>
		public const string WallThickness2 = "WTH2";

		/// <summary>
		/// Wall thickness 3
		/// </summary>
		public const string WallThickness3 = "WTH3";

		/// <summary>
		/// Wall thickness 4
		/// </summary>
		public const string WallThickness4 = "WTH4";

		/// <summary>
		/// Wall thickness series
		/// </summary>
		public const string WallThicknessSeries = "WLTS";

		/// <summary>
		/// Web A
		/// </summary>
		public const string WebA = "ASTEG";

		/// <summary>
		/// Weight
		/// </summary>
		public const string Weight = "§01";

		/// <summary>
		/// Weight fixed
		/// </summary>
		public const string WeightFixed = "%WFIX";

		/// <summary>
		/// Weight per length
		/// </summary>
		public const string WeightPerLength = "§16";

		/// <summary>
		/// Weld inspection category
		/// </summary>
		public const string WeldInspectionCategory = "$WCAT";

		/// <summary>
		/// Weld side
		/// </summary>
		public const string WeldSide = "%WSIDE";

		/// <summary>
		/// Width
		/// </summary>
		public const string Width = "§02";

		/// <summary>
		/// Width of 2-D development
		/// </summary>
		public const string WidthOf2dDevelopment = "§B2D";

		#region Szene

		/// <summary>
		/// Assembly drawing belonging to detail drawing (BIM-PDM)
		/// </summary>
		public const string SceneAssemblyDrawingBelongingToDetailDrawing = "_SZNATTRRAS";

		/// <summary>
		/// Assembly item number belonging to detail drawing (BIM-PDM)
		/// </summary>
		public const string SceneAssemblyItemNumberBelongingToDetailDrawing = "_SZNATTRRIN";

		/// <summary>
		/// Auxiliary text 1
		/// </summary>
		public const string SceneAuxiliaryText1 = "_SZNATTRS10";

		/// <summary>
		/// Auxiliary text 2
		/// </summary>
		public const string SceneAuxiliaryText2 = "_SZNATTRS11";

		/// <summary>
		/// Auxiliary text 3
		/// </summary>
		public const string SceneAuxiliaryText3 = "_SZNATTRS12";

		/// <summary>
		/// Auxiliary text 4
		/// </summary>
		public const string SceneAuxiliaryText4 = "_SZNATTRS13";

		/// <summary>
		/// Auxiliary text 5
		/// </summary>
		public const string SceneAuxiliaryText5 = "_SZNATTRS14";

		/// <summary>
		/// Back from authoriz.
		/// </summary>
		public const string SceneBackFromAuthoriz = "_SZNDSTV_04";

		/// <summary>
		/// Commercial weight of model
		/// </summary>
		public const string SceneCommercialWeightOfModel = "_SZNATTRD02";

		/// <summary>
		/// Created by
		/// </summary>
		public const string SceneCreatedBy = "_SZNATTRS08";

		/// <summary>
		/// Created on
		/// </summary>
		public const string SceneCreatedOn = "_SZNATTRS09";

		/// <summary>
		/// Customer
		/// </summary>
		public const string SceneCustomer = "_SZNATTRS03";

		/// <summary>
		/// Designation 1
		/// </summary>
		public const string SceneDesignation1 = "_SZNATTRS06";

		/// <summary>
		/// Designation 2
		/// </summary>
		public const string SceneDesignation2 = "_SZNATTRS07";

		/// <summary>
		/// Drawing No.
		/// </summary>
		public const string SceneDrawingNo = "_SZNATTRS04";

		/// <summary>
		/// Execution class, weld seams
		/// </summary>
		public const string SceneExecutionClassWeldSeams = "_SZNATTRSEXC";

		/// <summary>
		/// File name
		/// </summary>
		public const string SceneFileName = "_SZNATTRS00";

		/// <summary>
		/// Index
		/// </summary>
		public const string SceneIndex = "_SZNATTRS15";

		/// <summary>
		/// Job text
		/// </summary>
		public const string SceneJobText = "_SZNDSTV_07";

		/// <summary>
		/// List of item numbers of a production drawing (for BIM-PDM)
		/// </summary>
		public const string SceneListOfItemNumbersOfAProductionDrawing = "_SZNATTRITN";

		/// <summary>
		/// Order No.
		/// </summary>
		public const string SceneOrderNo = "_SZNATTRS01";

		/// <summary>
		/// Order text
		/// </summary>
		public const string SceneOrderText = "_SZNATTRS02";

		/// <summary>
		/// Part number
		/// </summary>
		public const string ScenePartNumber = "_SZNDSTV_01";

		/// <summary>
		/// Release
		/// </summary>
		public const string SceneRelease = "_SZNDSTV_06";

		/// <summary>
		/// Sheet No.
		/// </summary>
		public const string SceneSheetNo = "_SZNATTRS05";

		/// <summary>
		/// Status of auto-generated drawing (BIM-PDM)
		/// </summary>
		public const string SceneStatusOfAutogeneratedDrawing = "_SZNATTRSTA";

		/// <summary>
		/// To authoriz.
		/// </summary>
		public const string SceneToAuthoriz = "_SZNDSTV_03";

		/// <summary>
		/// To Test Engeneer
		/// </summary>
		public const string SceneToTestEngeneer = "_SZNDSTV_05";

		/// <summary>
		/// Weight Addition
		/// </summary>
		public const string SceneWeightAddition = "_SZNDSTV_02";

		/// <summary>
		/// Weight of drawing
		/// </summary>
		public const string SceneWeightOfDrawing = "_SZNATTRD01";

		#endregion Szene

		#region DSTV

		/// <summary>
		/// Acceptance index
		/// </summary>
		public const string DstvAcceptanceIndex = "DSTV_21";

		/// <summary>
		/// Advance Order No.
		/// </summary>
		public const string DstvAdvanceOrderNo = "DSTV_13";

		/// <summary>
		/// Article number of the assembly of the DSTV-part "H"
		/// </summary>
		public const string DstvArticleNumberOfTheAssemblyOfTheDstvpart = "DSTV_31";

		/// <summary>
		/// Characteristic
		/// </summary>
		public const string DstvCharacteristic = "DSTV_14";

		/// <summary>
		/// Custom-coated surface
		/// </summary>
		public const string DstvCustomcoatedSurface = "DSTV_27";

		/// <summary>
		/// Designation of the assembly of the DSTV-part"H"
		/// </summary>
		public const string DstvDesignationOfTheAssemblyOfTheDstvpart = "DSTV_32";

		/// <summary>
		/// Dispatch B [m]
		/// </summary>
		public const string DstvDispatchB = "DSTV_19";

		/// <summary>
		/// Dispatch H
		/// </summary>
		public const string DstvDispatchH = "DSTV_20";

		/// <summary>
		/// Dispatch L [m]
		/// </summary>
		public const string DstvDispatchL = "DSTV_18";

		/// <summary>
		/// Grid square
		/// </summary>
		public const string DstvGridSquare = "DSTV_16";

		/// <summary>
		/// Ident. Custom-coated surface
		/// </summary>
		public const string DstvIdentCustomcoatedSurface = "DSTV_25";

		/// <summary>
		/// Ident. Special weight
		/// </summary>
		public const string DstvIdentSpecialWeight = "DSTV_24";

		/// <summary>
		/// Item number of the assembly of the DSTV-part "H"
		/// </summary>
		public const string DstvItemNumberOfTheAssemblyOfTheDstvpart = "DSTV_28";

		/// <summary>
		/// LV Item 1
		/// </summary>
		public const string DstvLvItem1 = "DSTV_07";

		/// <summary>
		/// LV Item 2
		/// </summary>
		public const string DstvLvItem2 = "DSTV_08";

		/// <summary>
		/// LV Item 3
		/// </summary>
		public const string DstvLvItem3 = "DSTV_09";

		/// <summary>
		/// LV Item 4
		/// </summary>
		public const string DstvLvItem4 = "DSTV_10";

		/// <summary>
		/// LV Item 5
		/// </summary>
		public const string DstvLvItem5 = "DSTV_11";

		/// <summary>
		/// Main Part No.
		/// </summary>
		public const string DstvMainPartNo = "DSTV_22";

		/// <summary>
		/// Number of bores
		/// </summary>
		public const string DstvNumberOfBores = "DSTV_15";

		/// <summary>
		/// Production line
		/// </summary>
		public const string DstvProductionLine = "DSTV_12";

		/// <summary>
		/// Special Weight
		/// </summary>
		public const string DstvSpecialWeight = "DSTV_26";

		/// <summary>
		/// Store
		/// </summary>
		public const string DstvStore = "DSTV_17";

		/// <summary>
		/// Surface of the assembly of the DSTV-part "H"
		/// </summary>
		public const string DstvSurfaceOfTheAssemblyOfTheDstvpart = "DSTV_30";

		/// <summary>
		/// temp. Dummy
		/// </summary>
		public const string DstvTempDummy = "DSTV_23";

		/// <summary>
		/// Weight of the assembly of the DSTV-part "H"
		/// </summary>
		public const string DstvWeightOfTheAssemblyOfTheDstvpart = "DSTV_29";

		#endregion DSTV

		#region Piping

		/// <summary>
		/// Connection location
		/// </summary>
		public const string PipeConnectionLocation = "%PIPE_JOINT_LOCATION";

		/// <summary>
		/// Content of created annotation tag
		/// </summary>
		public const string PipeContentOfCreatedAnnotationTag = "%PIPE_JOINT_TEXT";

		/// <summary>
		/// Curve angle
		/// </summary>
		public const string PipeCurveAngle = "%PIPE_ANGLE";

		/// <summary>
		/// Curve radius
		/// </summary>
		public const string PipeCurveRadius = "%PIPE_ARCRADIUS";

		/// <summary>
		/// Down-grade in degrees
		/// </summary>
		public const string PipeDowngradeInDegrees = "%PIPE_SLOPE_ANGLE";

		/// <summary>
		/// Down-grade in percent
		/// </summary>
		public const string PipeDowngradeInPercent = "%PIPE_SLOPE_PERCENT";

		/// <summary>
		/// Down-grade in per mille
		/// </summary>
		public const string PipeDowngradeInPerMille = "%PIPE_SLOPE_PERMILLE";

		/// <summary>
		/// Joint No.
		/// </summary>
		public const string PipeJointNo = "%PIPE_JOINT_POS";

		/// <summary>
		/// Length item number
		/// </summary>
		public const string PipeLengthItemNumber = "%PIPE_POS_2";

		/// <summary>
		/// Local connection number in pipeline
		/// </summary>
		public const string PipeLocalConnectionNumberInPipeline = "%LOCAL_CON_NUM";

		/// <summary>
		/// Number of pipe lengths
		/// </summary>
		public const string PipeNumberOfPipeLengths = "%LENGTH_COUNT";

		/// <summary>
		/// Part name
		/// </summary>
		public const string PipePartName = "%PIPE_NAME";

		/// <summary>
		/// Part selection ID
		/// </summary>
		public const string PipePartSelectionId = "%PIPELINE_SELECTION_HASH";

		/// <summary>
		/// Pipe class designation
		/// </summary>
		public const string PipePipeClassDesignation = "PCLS";

		/// <summary>
		/// Pipe length
		/// </summary>
		public const string PipePipeLength = "%PIPE_LENGTH";

		/// <summary>
		/// Pipeline name
		/// </summary>
		public const string PipePipelineName = "%PIPE_LINE_NAME";

		/// <summary>
		/// Pipe part accessory set
		/// </summary>
		public const string PipePipePartAccessorySet = "%PIPE_ACCESSORIES";

		/// <summary>
		/// Pipe part item number
		/// </summary>
		public const string PipePipePartItemNumber = "%PIPE_POS_1";

		/// <summary>
		/// Split index
		/// </summary>
		public const string PipeSplitIndex = "%PIPE_ISOSPLIT_IDX";

		/// <summary>
		/// Split index counterpiece
		/// </summary>
		public const string PipeSplitIndexCounterpiece = "%PIPE_ISONSPLIT_IDX";

		/// <summary>
		/// X-Coordinate
		/// </summary>
		public const string PipeXcoordinate = "%PIPE_X_COOR";

		/// <summary>
		/// X-Coordinate arc
		/// </summary>
		public const string PipeXcoordinateArc = "%PIPE_BOW_X_COOR";

		/// <summary>
		/// X-Coordinate arc with sign
		/// </summary>
		public const string PipeXcoordinateArcWithSign = "%PIPE_BOW_XCOOR_POSPRESIGN";

		/// <summary>
		/// X-Coordinate with sign
		/// </summary>
		public const string PipeXcoordinateWithSign = "%PIPE_XCOOR_POSPRESIGN";

		/// <summary>
		/// Y-Coordinate
		/// </summary>
		public const string PipeYcoordinate = "%PIPE_Y_COOR";

		/// <summary>
		/// Y-Coordinate arc
		/// </summary>
		public const string PipeYcoordinateArc = "%PIPE_BOW_Y_COOR";

		/// <summary>
		/// Y-Coordinate arc with sign
		/// </summary>
		public const string PipeYcoordinateArcWithSign = "%PIPE_BOW_YCOOR_POSPRESIGN";

		/// <summary>
		/// Y-Coordinate with sign
		/// </summary>
		public const string PipeYcoordinateWithSign = "%PIPE_YCOOR_POSPRESIGN";

		/// <summary>
		/// Z-Coordinate
		/// </summary>
		public const string PipeZcoordinate = "%PIPE_Z_COOR";

		/// <summary>
		/// Z-Coordinate arc
		/// </summary>
		public const string PipeZcoordinateArc = "%PIPE_BOW_Z_COOR";

		/// <summary>
		/// Z-Coordinate arc with sign
		/// </summary>
		public const string PipeZcoordinateArcWithSign = "%PIPE_BOW_ZCOOR_POSPRESIGN";

		/// <summary>
		/// Z-Coordinate with sign
		/// </summary>
		public const string PipeZcoordinateWithSign = "%PIPE_ZCOOR_POSPRESIGN";

		#endregion Piping
	}
}