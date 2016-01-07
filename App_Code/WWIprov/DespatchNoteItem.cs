using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace DAL.Logistics
{
	/// <summary>
	/// Strongly-typed collection for the DespatchNoteItem class.
	/// </summary>
    [Serializable]
	public partial class DespatchNoteItemCollection : ActiveList<DespatchNoteItem, DespatchNoteItemCollection>
	{	   
		public DespatchNoteItemCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>DespatchNoteItemCollection</returns>
		public DespatchNoteItemCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                DespatchNoteItem o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the despatch_note_items table.
	/// </summary>
	[Serializable]
	public partial class DespatchNoteItem : ActiveRecord<DespatchNoteItem>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public DespatchNoteItem()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public DespatchNoteItem(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public DespatchNoteItem(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public DespatchNoteItem(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("despatch_note_items", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarItemId = new TableSchema.TableColumn(schema);
				colvarItemId.ColumnName = "item_id";
				colvarItemId.DataType = DbType.Int32;
				colvarItemId.MaxLength = 0;
				colvarItemId.AutoIncrement = true;
				colvarItemId.IsNullable = false;
				colvarItemId.IsPrimaryKey = true;
				colvarItemId.IsForeignKey = false;
				colvarItemId.IsReadOnly = false;
				colvarItemId.DefaultSetting = @"";
				colvarItemId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarItemId);
				
				TableSchema.TableColumn colvarDespatchNoteId = new TableSchema.TableColumn(schema);
				colvarDespatchNoteId.ColumnName = "despatch_note_id";
				colvarDespatchNoteId.DataType = DbType.Int32;
				colvarDespatchNoteId.MaxLength = 0;
				colvarDespatchNoteId.AutoIncrement = false;
				colvarDespatchNoteId.IsNullable = false;
				colvarDespatchNoteId.IsPrimaryKey = false;
				colvarDespatchNoteId.IsForeignKey = false;
				colvarDespatchNoteId.IsReadOnly = false;
				
						colvarDespatchNoteId.DefaultSetting = @"((0))";
				colvarDespatchNoteId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDespatchNoteId);
				
				TableSchema.TableColumn colvarBuyersOrderNumber = new TableSchema.TableColumn(schema);
				colvarBuyersOrderNumber.ColumnName = "buyers_order_number";
				colvarBuyersOrderNumber.DataType = DbType.String;
				colvarBuyersOrderNumber.MaxLength = 50;
				colvarBuyersOrderNumber.AutoIncrement = false;
				colvarBuyersOrderNumber.IsNullable = true;
				colvarBuyersOrderNumber.IsPrimaryKey = false;
				colvarBuyersOrderNumber.IsForeignKey = false;
				colvarBuyersOrderNumber.IsReadOnly = false;
				colvarBuyersOrderNumber.DefaultSetting = @"";
				colvarBuyersOrderNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBuyersOrderNumber);
				
				TableSchema.TableColumn colvarPrintersJobNumber = new TableSchema.TableColumn(schema);
				colvarPrintersJobNumber.ColumnName = "printers_job_number";
				colvarPrintersJobNumber.DataType = DbType.String;
				colvarPrintersJobNumber.MaxLength = 50;
				colvarPrintersJobNumber.AutoIncrement = false;
				colvarPrintersJobNumber.IsNullable = true;
				colvarPrintersJobNumber.IsPrimaryKey = false;
				colvarPrintersJobNumber.IsForeignKey = false;
				colvarPrintersJobNumber.IsReadOnly = false;
				colvarPrintersJobNumber.DefaultSetting = @"";
				colvarPrintersJobNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPrintersJobNumber);
				
				TableSchema.TableColumn colvarPublishipRef = new TableSchema.TableColumn(schema);
				colvarPublishipRef.ColumnName = "publiship_ref";
				colvarPublishipRef.DataType = DbType.String;
				colvarPublishipRef.MaxLength = 20;
				colvarPublishipRef.AutoIncrement = false;
				colvarPublishipRef.IsNullable = true;
				colvarPublishipRef.IsPrimaryKey = false;
				colvarPublishipRef.IsForeignKey = false;
				colvarPublishipRef.IsReadOnly = false;
				colvarPublishipRef.DefaultSetting = @"";
				colvarPublishipRef.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPublishipRef);
				
				TableSchema.TableColumn colvarIsbn = new TableSchema.TableColumn(schema);
				colvarIsbn.ColumnName = "isbn";
				colvarIsbn.DataType = DbType.String;
				colvarIsbn.MaxLength = 20;
				colvarIsbn.AutoIncrement = false;
				colvarIsbn.IsNullable = true;
				colvarIsbn.IsPrimaryKey = false;
				colvarIsbn.IsForeignKey = false;
				colvarIsbn.IsReadOnly = false;
				colvarIsbn.DefaultSetting = @"";
				colvarIsbn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsbn);
				
				TableSchema.TableColumn colvarTitle = new TableSchema.TableColumn(schema);
				colvarTitle.ColumnName = "title";
				colvarTitle.DataType = DbType.String;
				colvarTitle.MaxLength = 100;
				colvarTitle.AutoIncrement = false;
				colvarTitle.IsNullable = true;
				colvarTitle.IsPrimaryKey = false;
				colvarTitle.IsForeignKey = false;
				colvarTitle.IsReadOnly = false;
				colvarTitle.DefaultSetting = @"";
				colvarTitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTitle);
				
				TableSchema.TableColumn colvarImpression = new TableSchema.TableColumn(schema);
				colvarImpression.ColumnName = "impression";
				colvarImpression.DataType = DbType.String;
				colvarImpression.MaxLength = 10;
				colvarImpression.AutoIncrement = false;
				colvarImpression.IsNullable = true;
				colvarImpression.IsPrimaryKey = false;
				colvarImpression.IsForeignKey = false;
				colvarImpression.IsReadOnly = false;
				colvarImpression.DefaultSetting = @"";
				colvarImpression.ForeignKeyTableName = "";
				schema.Columns.Add(colvarImpression);
				
				TableSchema.TableColumn colvarTotalQty = new TableSchema.TableColumn(schema);
				colvarTotalQty.ColumnName = "total_qty";
				colvarTotalQty.DataType = DbType.Int32;
				colvarTotalQty.MaxLength = 0;
				colvarTotalQty.AutoIncrement = false;
				colvarTotalQty.IsNullable = true;
				colvarTotalQty.IsPrimaryKey = false;
				colvarTotalQty.IsForeignKey = false;
				colvarTotalQty.IsReadOnly = false;
				
						colvarTotalQty.DefaultSetting = @"((0))";
				colvarTotalQty.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTotalQty);
				
				TableSchema.TableColumn colvarFullPallets = new TableSchema.TableColumn(schema);
				colvarFullPallets.ColumnName = "full_pallets";
				colvarFullPallets.DataType = DbType.Int32;
				colvarFullPallets.MaxLength = 0;
				colvarFullPallets.AutoIncrement = false;
				colvarFullPallets.IsNullable = true;
				colvarFullPallets.IsPrimaryKey = false;
				colvarFullPallets.IsForeignKey = false;
				colvarFullPallets.IsReadOnly = false;
				
						colvarFullPallets.DefaultSetting = @"((0))";
				colvarFullPallets.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFullPallets);
				
				TableSchema.TableColumn colvarUnitsFull = new TableSchema.TableColumn(schema);
				colvarUnitsFull.ColumnName = "units_full";
				colvarUnitsFull.DataType = DbType.Int32;
				colvarUnitsFull.MaxLength = 0;
				colvarUnitsFull.AutoIncrement = false;
				colvarUnitsFull.IsNullable = true;
				colvarUnitsFull.IsPrimaryKey = false;
				colvarUnitsFull.IsForeignKey = false;
				colvarUnitsFull.IsReadOnly = false;
				
						colvarUnitsFull.DefaultSetting = @"((0))";
				colvarUnitsFull.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUnitsFull);
				
				TableSchema.TableColumn colvarPartPallets = new TableSchema.TableColumn(schema);
				colvarPartPallets.ColumnName = "part_pallets";
				colvarPartPallets.DataType = DbType.Int32;
				colvarPartPallets.MaxLength = 0;
				colvarPartPallets.AutoIncrement = false;
				colvarPartPallets.IsNullable = true;
				colvarPartPallets.IsPrimaryKey = false;
				colvarPartPallets.IsForeignKey = false;
				colvarPartPallets.IsReadOnly = false;
				
						colvarPartPallets.DefaultSetting = @"((0))";
				colvarPartPallets.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPartPallets);
				
				TableSchema.TableColumn colvarUnitsPart = new TableSchema.TableColumn(schema);
				colvarUnitsPart.ColumnName = "units_part";
				colvarUnitsPart.DataType = DbType.Int32;
				colvarUnitsPart.MaxLength = 0;
				colvarUnitsPart.AutoIncrement = false;
				colvarUnitsPart.IsNullable = true;
				colvarUnitsPart.IsPrimaryKey = false;
				colvarUnitsPart.IsForeignKey = false;
				colvarUnitsPart.IsReadOnly = false;
				
						colvarUnitsPart.DefaultSetting = @"((0))";
				colvarUnitsPart.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUnitsPart);
				
				TableSchema.TableColumn colvarParcelCount = new TableSchema.TableColumn(schema);
				colvarParcelCount.ColumnName = "parcel_count";
				colvarParcelCount.DataType = DbType.Int32;
				colvarParcelCount.MaxLength = 0;
				colvarParcelCount.AutoIncrement = false;
				colvarParcelCount.IsNullable = true;
				colvarParcelCount.IsPrimaryKey = false;
				colvarParcelCount.IsForeignKey = false;
				colvarParcelCount.IsReadOnly = false;
				
						colvarParcelCount.DefaultSetting = @"((0))";
				colvarParcelCount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarParcelCount);
				
				TableSchema.TableColumn colvarUnitsPerParcel = new TableSchema.TableColumn(schema);
				colvarUnitsPerParcel.ColumnName = "units_per_parcel";
				colvarUnitsPerParcel.DataType = DbType.Int32;
				colvarUnitsPerParcel.MaxLength = 0;
				colvarUnitsPerParcel.AutoIncrement = false;
				colvarUnitsPerParcel.IsNullable = true;
				colvarUnitsPerParcel.IsPrimaryKey = false;
				colvarUnitsPerParcel.IsForeignKey = false;
				colvarUnitsPerParcel.IsReadOnly = false;
				
						colvarUnitsPerParcel.DefaultSetting = @"((0))";
				colvarUnitsPerParcel.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUnitsPerParcel);
				
				TableSchema.TableColumn colvarParcelsPerLayer = new TableSchema.TableColumn(schema);
				colvarParcelsPerLayer.ColumnName = "parcels_per_layer";
				colvarParcelsPerLayer.DataType = DbType.Int32;
				colvarParcelsPerLayer.MaxLength = 0;
				colvarParcelsPerLayer.AutoIncrement = false;
				colvarParcelsPerLayer.IsNullable = true;
				colvarParcelsPerLayer.IsPrimaryKey = false;
				colvarParcelsPerLayer.IsForeignKey = false;
				colvarParcelsPerLayer.IsReadOnly = false;
				
						colvarParcelsPerLayer.DefaultSetting = @"((0))";
				colvarParcelsPerLayer.ForeignKeyTableName = "";
				schema.Columns.Add(colvarParcelsPerLayer);
				
				TableSchema.TableColumn colvarOddsCount = new TableSchema.TableColumn(schema);
				colvarOddsCount.ColumnName = "odds_count";
				colvarOddsCount.DataType = DbType.Int32;
				colvarOddsCount.MaxLength = 0;
				colvarOddsCount.AutoIncrement = false;
				colvarOddsCount.IsNullable = true;
				colvarOddsCount.IsPrimaryKey = false;
				colvarOddsCount.IsForeignKey = false;
				colvarOddsCount.IsReadOnly = false;
				
						colvarOddsCount.DefaultSetting = @"((0))";
				colvarOddsCount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOddsCount);
				
				TableSchema.TableColumn colvarHeight = new TableSchema.TableColumn(schema);
				colvarHeight.ColumnName = "height";
				colvarHeight.DataType = DbType.Decimal;
				colvarHeight.MaxLength = 0;
				colvarHeight.AutoIncrement = false;
				colvarHeight.IsNullable = true;
				colvarHeight.IsPrimaryKey = false;
				colvarHeight.IsForeignKey = false;
				colvarHeight.IsReadOnly = false;
				
						colvarHeight.DefaultSetting = @"((0))";
				colvarHeight.ForeignKeyTableName = "";
				schema.Columns.Add(colvarHeight);
				
				TableSchema.TableColumn colvarWidth = new TableSchema.TableColumn(schema);
				colvarWidth.ColumnName = "width";
				colvarWidth.DataType = DbType.Decimal;
				colvarWidth.MaxLength = 0;
				colvarWidth.AutoIncrement = false;
				colvarWidth.IsNullable = true;
				colvarWidth.IsPrimaryKey = false;
				colvarWidth.IsForeignKey = false;
				colvarWidth.IsReadOnly = false;
				
						colvarWidth.DefaultSetting = @"((0))";
				colvarWidth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWidth);
				
				TableSchema.TableColumn colvarDepth = new TableSchema.TableColumn(schema);
				colvarDepth.ColumnName = "depth";
				colvarDepth.DataType = DbType.Decimal;
				colvarDepth.MaxLength = 0;
				colvarDepth.AutoIncrement = false;
				colvarDepth.IsNullable = true;
				colvarDepth.IsPrimaryKey = false;
				colvarDepth.IsForeignKey = false;
				colvarDepth.IsReadOnly = false;
				
						colvarDepth.DefaultSetting = @"((0))";
				colvarDepth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDepth);
				
				TableSchema.TableColumn colvarUnitNetWeight = new TableSchema.TableColumn(schema);
				colvarUnitNetWeight.ColumnName = "unit_net_weight";
				colvarUnitNetWeight.DataType = DbType.Decimal;
				colvarUnitNetWeight.MaxLength = 0;
				colvarUnitNetWeight.AutoIncrement = false;
				colvarUnitNetWeight.IsNullable = true;
				colvarUnitNetWeight.IsPrimaryKey = false;
				colvarUnitNetWeight.IsForeignKey = false;
				colvarUnitNetWeight.IsReadOnly = false;
				
						colvarUnitNetWeight.DefaultSetting = @"((0))";
				colvarUnitNetWeight.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUnitNetWeight);
				
				TableSchema.TableColumn colvarParcelHeight = new TableSchema.TableColumn(schema);
				colvarParcelHeight.ColumnName = "parcel_height";
				colvarParcelHeight.DataType = DbType.Decimal;
				colvarParcelHeight.MaxLength = 0;
				colvarParcelHeight.AutoIncrement = false;
				colvarParcelHeight.IsNullable = true;
				colvarParcelHeight.IsPrimaryKey = false;
				colvarParcelHeight.IsForeignKey = false;
				colvarParcelHeight.IsReadOnly = false;
				
						colvarParcelHeight.DefaultSetting = @"((0))";
				colvarParcelHeight.ForeignKeyTableName = "";
				schema.Columns.Add(colvarParcelHeight);
				
				TableSchema.TableColumn colvarParcelWidth = new TableSchema.TableColumn(schema);
				colvarParcelWidth.ColumnName = "parcel_width";
				colvarParcelWidth.DataType = DbType.Decimal;
				colvarParcelWidth.MaxLength = 0;
				colvarParcelWidth.AutoIncrement = false;
				colvarParcelWidth.IsNullable = true;
				colvarParcelWidth.IsPrimaryKey = false;
				colvarParcelWidth.IsForeignKey = false;
				colvarParcelWidth.IsReadOnly = false;
				
						colvarParcelWidth.DefaultSetting = @"((0))";
				colvarParcelWidth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarParcelWidth);
				
				TableSchema.TableColumn colvarParcelDepth = new TableSchema.TableColumn(schema);
				colvarParcelDepth.ColumnName = "parcel_depth";
				colvarParcelDepth.DataType = DbType.Decimal;
				colvarParcelDepth.MaxLength = 0;
				colvarParcelDepth.AutoIncrement = false;
				colvarParcelDepth.IsNullable = true;
				colvarParcelDepth.IsPrimaryKey = false;
				colvarParcelDepth.IsForeignKey = false;
				colvarParcelDepth.IsReadOnly = false;
				
						colvarParcelDepth.DefaultSetting = @"((0))";
				colvarParcelDepth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarParcelDepth);
				
				TableSchema.TableColumn colvarParcelUnitgrossweight = new TableSchema.TableColumn(schema);
				colvarParcelUnitgrossweight.ColumnName = "parcel_unitgrossweight";
				colvarParcelUnitgrossweight.DataType = DbType.Decimal;
				colvarParcelUnitgrossweight.MaxLength = 0;
				colvarParcelUnitgrossweight.AutoIncrement = false;
				colvarParcelUnitgrossweight.IsNullable = true;
				colvarParcelUnitgrossweight.IsPrimaryKey = false;
				colvarParcelUnitgrossweight.IsForeignKey = false;
				colvarParcelUnitgrossweight.IsReadOnly = false;
				
						colvarParcelUnitgrossweight.DefaultSetting = @"((0))";
				colvarParcelUnitgrossweight.ForeignKeyTableName = "";
				schema.Columns.Add(colvarParcelUnitgrossweight);
				
				TableSchema.TableColumn colvarDespatchItemTs = new TableSchema.TableColumn(schema);
				colvarDespatchItemTs.ColumnName = "despatch_item_ts";
				colvarDespatchItemTs.DataType = DbType.Binary;
				colvarDespatchItemTs.MaxLength = 0;
				colvarDespatchItemTs.AutoIncrement = false;
				colvarDespatchItemTs.IsNullable = true;
				colvarDespatchItemTs.IsPrimaryKey = false;
				colvarDespatchItemTs.IsForeignKey = false;
				colvarDespatchItemTs.IsReadOnly = true;
				colvarDespatchItemTs.DefaultSetting = @"";
				colvarDespatchItemTs.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDespatchItemTs);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("despatch_note_items",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("ItemId")]
		[Bindable(true)]
		public int ItemId 
		{
			get { return GetColumnValue<int>(Columns.ItemId); }
			set { SetColumnValue(Columns.ItemId, value); }
		}
		  
		[XmlAttribute("DespatchNoteId")]
		[Bindable(true)]
		public int DespatchNoteId 
		{
			get { return GetColumnValue<int>(Columns.DespatchNoteId); }
			set { SetColumnValue(Columns.DespatchNoteId, value); }
		}
		  
		[XmlAttribute("BuyersOrderNumber")]
		[Bindable(true)]
		public string BuyersOrderNumber 
		{
			get { return GetColumnValue<string>(Columns.BuyersOrderNumber); }
			set { SetColumnValue(Columns.BuyersOrderNumber, value); }
		}
		  
		[XmlAttribute("PrintersJobNumber")]
		[Bindable(true)]
		public string PrintersJobNumber 
		{
			get { return GetColumnValue<string>(Columns.PrintersJobNumber); }
			set { SetColumnValue(Columns.PrintersJobNumber, value); }
		}
		  
		[XmlAttribute("PublishipRef")]
		[Bindable(true)]
		public string PublishipRef 
		{
			get { return GetColumnValue<string>(Columns.PublishipRef); }
			set { SetColumnValue(Columns.PublishipRef, value); }
		}
		  
		[XmlAttribute("Isbn")]
		[Bindable(true)]
		public string Isbn 
		{
			get { return GetColumnValue<string>(Columns.Isbn); }
			set { SetColumnValue(Columns.Isbn, value); }
		}
		  
		[XmlAttribute("Title")]
		[Bindable(true)]
		public string Title 
		{
			get { return GetColumnValue<string>(Columns.Title); }
			set { SetColumnValue(Columns.Title, value); }
		}
		  
		[XmlAttribute("Impression")]
		[Bindable(true)]
		public string Impression 
		{
			get { return GetColumnValue<string>(Columns.Impression); }
			set { SetColumnValue(Columns.Impression, value); }
		}
		  
		[XmlAttribute("TotalQty")]
		[Bindable(true)]
		public int? TotalQty 
		{
			get { return GetColumnValue<int?>(Columns.TotalQty); }
			set { SetColumnValue(Columns.TotalQty, value); }
		}
		  
		[XmlAttribute("FullPallets")]
		[Bindable(true)]
		public int? FullPallets 
		{
			get { return GetColumnValue<int?>(Columns.FullPallets); }
			set { SetColumnValue(Columns.FullPallets, value); }
		}
		  
		[XmlAttribute("UnitsFull")]
		[Bindable(true)]
		public int? UnitsFull 
		{
			get { return GetColumnValue<int?>(Columns.UnitsFull); }
			set { SetColumnValue(Columns.UnitsFull, value); }
		}
		  
		[XmlAttribute("PartPallets")]
		[Bindable(true)]
		public int? PartPallets 
		{
			get { return GetColumnValue<int?>(Columns.PartPallets); }
			set { SetColumnValue(Columns.PartPallets, value); }
		}
		  
		[XmlAttribute("UnitsPart")]
		[Bindable(true)]
		public int? UnitsPart 
		{
			get { return GetColumnValue<int?>(Columns.UnitsPart); }
			set { SetColumnValue(Columns.UnitsPart, value); }
		}
		  
		[XmlAttribute("ParcelCount")]
		[Bindable(true)]
		public int? ParcelCount 
		{
			get { return GetColumnValue<int?>(Columns.ParcelCount); }
			set { SetColumnValue(Columns.ParcelCount, value); }
		}
		  
		[XmlAttribute("UnitsPerParcel")]
		[Bindable(true)]
		public int? UnitsPerParcel 
		{
			get { return GetColumnValue<int?>(Columns.UnitsPerParcel); }
			set { SetColumnValue(Columns.UnitsPerParcel, value); }
		}
		  
		[XmlAttribute("ParcelsPerLayer")]
		[Bindable(true)]
		public int? ParcelsPerLayer 
		{
			get { return GetColumnValue<int?>(Columns.ParcelsPerLayer); }
			set { SetColumnValue(Columns.ParcelsPerLayer, value); }
		}
		  
		[XmlAttribute("OddsCount")]
		[Bindable(true)]
		public int? OddsCount 
		{
			get { return GetColumnValue<int?>(Columns.OddsCount); }
			set { SetColumnValue(Columns.OddsCount, value); }
		}
		  
		[XmlAttribute("Height")]
		[Bindable(true)]
		public decimal? Height 
		{
			get { return GetColumnValue<decimal?>(Columns.Height); }
			set { SetColumnValue(Columns.Height, value); }
		}
		  
		[XmlAttribute("Width")]
		[Bindable(true)]
		public decimal? Width 
		{
			get { return GetColumnValue<decimal?>(Columns.Width); }
			set { SetColumnValue(Columns.Width, value); }
		}
		  
		[XmlAttribute("Depth")]
		[Bindable(true)]
		public decimal? Depth 
		{
			get { return GetColumnValue<decimal?>(Columns.Depth); }
			set { SetColumnValue(Columns.Depth, value); }
		}
		  
		[XmlAttribute("UnitNetWeight")]
		[Bindable(true)]
		public decimal? UnitNetWeight 
		{
			get { return GetColumnValue<decimal?>(Columns.UnitNetWeight); }
			set { SetColumnValue(Columns.UnitNetWeight, value); }
		}
		  
		[XmlAttribute("ParcelHeight")]
		[Bindable(true)]
		public decimal? ParcelHeight 
		{
			get { return GetColumnValue<decimal?>(Columns.ParcelHeight); }
			set { SetColumnValue(Columns.ParcelHeight, value); }
		}
		  
		[XmlAttribute("ParcelWidth")]
		[Bindable(true)]
		public decimal? ParcelWidth 
		{
			get { return GetColumnValue<decimal?>(Columns.ParcelWidth); }
			set { SetColumnValue(Columns.ParcelWidth, value); }
		}
		  
		[XmlAttribute("ParcelDepth")]
		[Bindable(true)]
		public decimal? ParcelDepth 
		{
			get { return GetColumnValue<decimal?>(Columns.ParcelDepth); }
			set { SetColumnValue(Columns.ParcelDepth, value); }
		}
		  
		[XmlAttribute("ParcelUnitgrossweight")]
		[Bindable(true)]
		public decimal? ParcelUnitgrossweight 
		{
			get { return GetColumnValue<decimal?>(Columns.ParcelUnitgrossweight); }
			set { SetColumnValue(Columns.ParcelUnitgrossweight, value); }
		}
		  
		[XmlAttribute("DespatchItemTs")]
		[Bindable(true)]
		public byte[] DespatchItemTs 
		{
			get { return GetColumnValue<byte[]>(Columns.DespatchItemTs); }
			set { SetColumnValue(Columns.DespatchItemTs, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varDespatchNoteId,string varBuyersOrderNumber,string varPrintersJobNumber,string varPublishipRef,string varIsbn,string varTitle,string varImpression,int? varTotalQty,int? varFullPallets,int? varUnitsFull,int? varPartPallets,int? varUnitsPart,int? varParcelCount,int? varUnitsPerParcel,int? varParcelsPerLayer,int? varOddsCount,decimal? varHeight,decimal? varWidth,decimal? varDepth,decimal? varUnitNetWeight,decimal? varParcelHeight,decimal? varParcelWidth,decimal? varParcelDepth,decimal? varParcelUnitgrossweight,byte[] varDespatchItemTs)
		{
			DespatchNoteItem item = new DespatchNoteItem();
			
			item.DespatchNoteId = varDespatchNoteId;
			
			item.BuyersOrderNumber = varBuyersOrderNumber;
			
			item.PrintersJobNumber = varPrintersJobNumber;
			
			item.PublishipRef = varPublishipRef;
			
			item.Isbn = varIsbn;
			
			item.Title = varTitle;
			
			item.Impression = varImpression;
			
			item.TotalQty = varTotalQty;
			
			item.FullPallets = varFullPallets;
			
			item.UnitsFull = varUnitsFull;
			
			item.PartPallets = varPartPallets;
			
			item.UnitsPart = varUnitsPart;
			
			item.ParcelCount = varParcelCount;
			
			item.UnitsPerParcel = varUnitsPerParcel;
			
			item.ParcelsPerLayer = varParcelsPerLayer;
			
			item.OddsCount = varOddsCount;
			
			item.Height = varHeight;
			
			item.Width = varWidth;
			
			item.Depth = varDepth;
			
			item.UnitNetWeight = varUnitNetWeight;
			
			item.ParcelHeight = varParcelHeight;
			
			item.ParcelWidth = varParcelWidth;
			
			item.ParcelDepth = varParcelDepth;
			
			item.ParcelUnitgrossweight = varParcelUnitgrossweight;
			
			item.DespatchItemTs = varDespatchItemTs;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varItemId,int varDespatchNoteId,string varBuyersOrderNumber,string varPrintersJobNumber,string varPublishipRef,string varIsbn,string varTitle,string varImpression,int? varTotalQty,int? varFullPallets,int? varUnitsFull,int? varPartPallets,int? varUnitsPart,int? varParcelCount,int? varUnitsPerParcel,int? varParcelsPerLayer,int? varOddsCount,decimal? varHeight,decimal? varWidth,decimal? varDepth,decimal? varUnitNetWeight,decimal? varParcelHeight,decimal? varParcelWidth,decimal? varParcelDepth,decimal? varParcelUnitgrossweight,byte[] varDespatchItemTs)
		{
			DespatchNoteItem item = new DespatchNoteItem();
			
				item.ItemId = varItemId;
			
				item.DespatchNoteId = varDespatchNoteId;
			
				item.BuyersOrderNumber = varBuyersOrderNumber;
			
				item.PrintersJobNumber = varPrintersJobNumber;
			
				item.PublishipRef = varPublishipRef;
			
				item.Isbn = varIsbn;
			
				item.Title = varTitle;
			
				item.Impression = varImpression;
			
				item.TotalQty = varTotalQty;
			
				item.FullPallets = varFullPallets;
			
				item.UnitsFull = varUnitsFull;
			
				item.PartPallets = varPartPallets;
			
				item.UnitsPart = varUnitsPart;
			
				item.ParcelCount = varParcelCount;
			
				item.UnitsPerParcel = varUnitsPerParcel;
			
				item.ParcelsPerLayer = varParcelsPerLayer;
			
				item.OddsCount = varOddsCount;
			
				item.Height = varHeight;
			
				item.Width = varWidth;
			
				item.Depth = varDepth;
			
				item.UnitNetWeight = varUnitNetWeight;
			
				item.ParcelHeight = varParcelHeight;
			
				item.ParcelWidth = varParcelWidth;
			
				item.ParcelDepth = varParcelDepth;
			
				item.ParcelUnitgrossweight = varParcelUnitgrossweight;
			
				item.DespatchItemTs = varDespatchItemTs;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn ItemIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn DespatchNoteIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn BuyersOrderNumberColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn PrintersJobNumberColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn PublishipRefColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn IsbnColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn TitleColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn ImpressionColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn TotalQtyColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn FullPalletsColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn UnitsFullColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn PartPalletsColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn UnitsPartColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn ParcelCountColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn UnitsPerParcelColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn ParcelsPerLayerColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn OddsCountColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn HeightColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn WidthColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn DepthColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn UnitNetWeightColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn ParcelHeightColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn ParcelWidthColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn ParcelDepthColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn ParcelUnitgrossweightColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        public static TableSchema.TableColumn DespatchItemTsColumn
        {
            get { return Schema.Columns[25]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string ItemId = @"item_id";
			 public static string DespatchNoteId = @"despatch_note_id";
			 public static string BuyersOrderNumber = @"buyers_order_number";
			 public static string PrintersJobNumber = @"printers_job_number";
			 public static string PublishipRef = @"publiship_ref";
			 public static string Isbn = @"isbn";
			 public static string Title = @"title";
			 public static string Impression = @"impression";
			 public static string TotalQty = @"total_qty";
			 public static string FullPallets = @"full_pallets";
			 public static string UnitsFull = @"units_full";
			 public static string PartPallets = @"part_pallets";
			 public static string UnitsPart = @"units_part";
			 public static string ParcelCount = @"parcel_count";
			 public static string UnitsPerParcel = @"units_per_parcel";
			 public static string ParcelsPerLayer = @"parcels_per_layer";
			 public static string OddsCount = @"odds_count";
			 public static string Height = @"height";
			 public static string Width = @"width";
			 public static string Depth = @"depth";
			 public static string UnitNetWeight = @"unit_net_weight";
			 public static string ParcelHeight = @"parcel_height";
			 public static string ParcelWidth = @"parcel_width";
			 public static string ParcelDepth = @"parcel_depth";
			 public static string ParcelUnitgrossweight = @"parcel_unitgrossweight";
			 public static string DespatchItemTs = @"despatch_item_ts";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
