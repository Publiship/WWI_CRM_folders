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
	/// Strongly-typed collection for the ItemTable class.
	/// </summary>
    [Serializable]
	public partial class ItemTableCollection : ActiveList<ItemTable, ItemTableCollection>
	{	   
		public ItemTableCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>ItemTableCollection</returns>
		public ItemTableCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                ItemTable o = this[i];
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
	/// This is an ActiveRecord class which wraps the ItemTable table.
	/// </summary>
	[Serializable]
	public partial class ItemTable : ActiveRecord<ItemTable>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public ItemTable()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public ItemTable(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public ItemTable(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public ItemTable(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("ItemTable", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarTitleID = new TableSchema.TableColumn(schema);
				colvarTitleID.ColumnName = "TitleID";
				colvarTitleID.DataType = DbType.Int32;
				colvarTitleID.MaxLength = 0;
				colvarTitleID.AutoIncrement = true;
				colvarTitleID.IsNullable = false;
				colvarTitleID.IsPrimaryKey = true;
				colvarTitleID.IsForeignKey = false;
				colvarTitleID.IsReadOnly = false;
				colvarTitleID.DefaultSetting = @"";
				colvarTitleID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTitleID);
				
				TableSchema.TableColumn colvarOrderNumber = new TableSchema.TableColumn(schema);
				colvarOrderNumber.ColumnName = "OrderNumber";
				colvarOrderNumber.DataType = DbType.Int32;
				colvarOrderNumber.MaxLength = 0;
				colvarOrderNumber.AutoIncrement = false;
				colvarOrderNumber.IsNullable = true;
				colvarOrderNumber.IsPrimaryKey = false;
				colvarOrderNumber.IsForeignKey = true;
				colvarOrderNumber.IsReadOnly = false;
				colvarOrderNumber.DefaultSetting = @"";
				
					colvarOrderNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOrderNumber);
				
				TableSchema.TableColumn colvarTitle = new TableSchema.TableColumn(schema);
				colvarTitle.ColumnName = "Title";
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
				
				TableSchema.TableColumn colvarIsbn = new TableSchema.TableColumn(schema);
				colvarIsbn.ColumnName = "ISBN";
				colvarIsbn.DataType = DbType.String;
				colvarIsbn.MaxLength = 50;
				colvarIsbn.AutoIncrement = false;
				colvarIsbn.IsNullable = true;
				colvarIsbn.IsPrimaryKey = false;
				colvarIsbn.IsForeignKey = false;
				colvarIsbn.IsReadOnly = false;
				colvarIsbn.DefaultSetting = @"";
				colvarIsbn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsbn);
				
				TableSchema.TableColumn colvarSSRNo = new TableSchema.TableColumn(schema);
				colvarSSRNo.ColumnName = "SSRNo";
				colvarSSRNo.DataType = DbType.String;
				colvarSSRNo.MaxLength = 50;
				colvarSSRNo.AutoIncrement = false;
				colvarSSRNo.IsNullable = true;
				colvarSSRNo.IsPrimaryKey = false;
				colvarSSRNo.IsForeignKey = false;
				colvarSSRNo.IsReadOnly = false;
				colvarSSRNo.DefaultSetting = @"";
				colvarSSRNo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSSRNo);
				
				TableSchema.TableColumn colvarSSRDate = new TableSchema.TableColumn(schema);
				colvarSSRDate.ColumnName = "SSRDate";
				colvarSSRDate.DataType = DbType.DateTime;
				colvarSSRDate.MaxLength = 0;
				colvarSSRDate.AutoIncrement = false;
				colvarSSRDate.IsNullable = true;
				colvarSSRDate.IsPrimaryKey = false;
				colvarSSRDate.IsForeignKey = false;
				colvarSSRDate.IsReadOnly = false;
				colvarSSRDate.DefaultSetting = @"";
				colvarSSRDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSSRDate);
				
				TableSchema.TableColumn colvarImpression = new TableSchema.TableColumn(schema);
				colvarImpression.ColumnName = "Impression";
				colvarImpression.DataType = DbType.String;
				colvarImpression.MaxLength = 50;
				colvarImpression.AutoIncrement = false;
				colvarImpression.IsNullable = true;
				colvarImpression.IsPrimaryKey = false;
				colvarImpression.IsForeignKey = false;
				colvarImpression.IsReadOnly = false;
				colvarImpression.DefaultSetting = @"";
				colvarImpression.ForeignKeyTableName = "";
				schema.Columns.Add(colvarImpression);
				
				TableSchema.TableColumn colvarPONumber = new TableSchema.TableColumn(schema);
				colvarPONumber.ColumnName = "PONumber";
				colvarPONumber.DataType = DbType.String;
				colvarPONumber.MaxLength = 50;
				colvarPONumber.AutoIncrement = false;
				colvarPONumber.IsNullable = true;
				colvarPONumber.IsPrimaryKey = false;
				colvarPONumber.IsForeignKey = false;
				colvarPONumber.IsReadOnly = false;
				colvarPONumber.DefaultSetting = @"";
				colvarPONumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPONumber);
				
				TableSchema.TableColumn colvarOtherRef = new TableSchema.TableColumn(schema);
				colvarOtherRef.ColumnName = "OtherRef";
				colvarOtherRef.DataType = DbType.String;
				colvarOtherRef.MaxLength = 50;
				colvarOtherRef.AutoIncrement = false;
				colvarOtherRef.IsNullable = true;
				colvarOtherRef.IsPrimaryKey = false;
				colvarOtherRef.IsForeignKey = false;
				colvarOtherRef.IsReadOnly = false;
				colvarOtherRef.DefaultSetting = @"";
				colvarOtherRef.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOtherRef);
				
				TableSchema.TableColumn colvarCopies = new TableSchema.TableColumn(schema);
				colvarCopies.ColumnName = "Copies";
				colvarCopies.DataType = DbType.Int32;
				colvarCopies.MaxLength = 0;
				colvarCopies.AutoIncrement = false;
				colvarCopies.IsNullable = true;
				colvarCopies.IsPrimaryKey = false;
				colvarCopies.IsForeignKey = false;
				colvarCopies.IsReadOnly = false;
				colvarCopies.DefaultSetting = @"";
				colvarCopies.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCopies);
				
				TableSchema.TableColumn colvarCopiesLastUpdated = new TableSchema.TableColumn(schema);
				colvarCopiesLastUpdated.ColumnName = "CopiesLastUpdated";
				colvarCopiesLastUpdated.DataType = DbType.DateTime;
				colvarCopiesLastUpdated.MaxLength = 0;
				colvarCopiesLastUpdated.AutoIncrement = false;
				colvarCopiesLastUpdated.IsNullable = true;
				colvarCopiesLastUpdated.IsPrimaryKey = false;
				colvarCopiesLastUpdated.IsForeignKey = false;
				colvarCopiesLastUpdated.IsReadOnly = false;
				colvarCopiesLastUpdated.DefaultSetting = @"";
				colvarCopiesLastUpdated.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCopiesLastUpdated);
				
				TableSchema.TableColumn colvarItemDesc = new TableSchema.TableColumn(schema);
				colvarItemDesc.ColumnName = "ItemDesc";
				colvarItemDesc.DataType = DbType.String;
				colvarItemDesc.MaxLength = 50;
				colvarItemDesc.AutoIncrement = false;
				colvarItemDesc.IsNullable = true;
				colvarItemDesc.IsPrimaryKey = false;
				colvarItemDesc.IsForeignKey = false;
				colvarItemDesc.IsReadOnly = false;
				colvarItemDesc.DefaultSetting = @"";
				colvarItemDesc.ForeignKeyTableName = "";
				schema.Columns.Add(colvarItemDesc);
				
				TableSchema.TableColumn colvarPackages = new TableSchema.TableColumn(schema);
				colvarPackages.ColumnName = "Packages";
				colvarPackages.DataType = DbType.Int32;
				colvarPackages.MaxLength = 0;
				colvarPackages.AutoIncrement = false;
				colvarPackages.IsNullable = true;
				colvarPackages.IsPrimaryKey = false;
				colvarPackages.IsForeignKey = false;
				colvarPackages.IsReadOnly = false;
				colvarPackages.DefaultSetting = @"";
				colvarPackages.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPackages);
				
				TableSchema.TableColumn colvarPackageID = new TableSchema.TableColumn(schema);
				colvarPackageID.ColumnName = "PackageID";
				colvarPackageID.DataType = DbType.Int32;
				colvarPackageID.MaxLength = 0;
				colvarPackageID.AutoIncrement = false;
				colvarPackageID.IsNullable = true;
				colvarPackageID.IsPrimaryKey = false;
				colvarPackageID.IsForeignKey = false;
				colvarPackageID.IsReadOnly = false;
				colvarPackageID.DefaultSetting = @"";
				colvarPackageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPackageID);
				
				TableSchema.TableColumn colvarWeight = new TableSchema.TableColumn(schema);
				colvarWeight.ColumnName = "Weight";
				colvarWeight.DataType = DbType.Int32;
				colvarWeight.MaxLength = 0;
				colvarWeight.AutoIncrement = false;
				colvarWeight.IsNullable = true;
				colvarWeight.IsPrimaryKey = false;
				colvarWeight.IsForeignKey = false;
				colvarWeight.IsReadOnly = false;
				colvarWeight.DefaultSetting = @"";
				colvarWeight.ForeignKeyTableName = "";
				schema.Columns.Add(colvarWeight);
				
				TableSchema.TableColumn colvarVolume = new TableSchema.TableColumn(schema);
				colvarVolume.ColumnName = "Volume";
				colvarVolume.DataType = DbType.Int32;
				colvarVolume.MaxLength = 0;
				colvarVolume.AutoIncrement = false;
				colvarVolume.IsNullable = true;
				colvarVolume.IsPrimaryKey = false;
				colvarVolume.IsForeignKey = false;
				colvarVolume.IsReadOnly = false;
				colvarVolume.DefaultSetting = @"";
				colvarVolume.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVolume);
				
				TableSchema.TableColumn colvarPerCopy = new TableSchema.TableColumn(schema);
				colvarPerCopy.ColumnName = "PerCopy";
				colvarPerCopy.DataType = DbType.Double;
				colvarPerCopy.MaxLength = 0;
				colvarPerCopy.AutoIncrement = false;
				colvarPerCopy.IsNullable = true;
				colvarPerCopy.IsPrimaryKey = false;
				colvarPerCopy.IsForeignKey = false;
				colvarPerCopy.IsReadOnly = false;
				colvarPerCopy.DefaultSetting = @"";
				colvarPerCopy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPerCopy);
				
				TableSchema.TableColumn colvarTotalValue = new TableSchema.TableColumn(schema);
				colvarTotalValue.ColumnName = "TotalValue";
				colvarTotalValue.DataType = DbType.Decimal;
				colvarTotalValue.MaxLength = 0;
				colvarTotalValue.AutoIncrement = false;
				colvarTotalValue.IsNullable = true;
				colvarTotalValue.IsPrimaryKey = false;
				colvarTotalValue.IsForeignKey = false;
				colvarTotalValue.IsReadOnly = false;
				colvarTotalValue.DefaultSetting = @"";
				colvarTotalValue.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTotalValue);
				
				TableSchema.TableColumn colvarTs = new TableSchema.TableColumn(schema);
				colvarTs.ColumnName = "TS";
				colvarTs.DataType = DbType.Binary;
				colvarTs.MaxLength = 0;
				colvarTs.AutoIncrement = false;
				colvarTs.IsNullable = true;
				colvarTs.IsPrimaryKey = false;
				colvarTs.IsForeignKey = false;
				colvarTs.IsReadOnly = true;
				colvarTs.DefaultSetting = @"";
				colvarTs.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTs);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("ItemTable",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("TitleID")]
		[Bindable(true)]
		public int TitleID 
		{
			get { return GetColumnValue<int>(Columns.TitleID); }
			set { SetColumnValue(Columns.TitleID, value); }
		}
		  
		[XmlAttribute("OrderNumber")]
		[Bindable(true)]
		public int? OrderNumber 
		{
			get { return GetColumnValue<int?>(Columns.OrderNumber); }
			set { SetColumnValue(Columns.OrderNumber, value); }
		}
		  
		[XmlAttribute("Title")]
		[Bindable(true)]
		public string Title 
		{
			get { return GetColumnValue<string>(Columns.Title); }
			set { SetColumnValue(Columns.Title, value); }
		}
		  
		[XmlAttribute("Isbn")]
		[Bindable(true)]
		public string Isbn 
		{
			get { return GetColumnValue<string>(Columns.Isbn); }
			set { SetColumnValue(Columns.Isbn, value); }
		}
		  
		[XmlAttribute("SSRNo")]
		[Bindable(true)]
		public string SSRNo 
		{
			get { return GetColumnValue<string>(Columns.SSRNo); }
			set { SetColumnValue(Columns.SSRNo, value); }
		}
		  
		[XmlAttribute("SSRDate")]
		[Bindable(true)]
		public DateTime? SSRDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.SSRDate); }
			set { SetColumnValue(Columns.SSRDate, value); }
		}
		  
		[XmlAttribute("Impression")]
		[Bindable(true)]
		public string Impression 
		{
			get { return GetColumnValue<string>(Columns.Impression); }
			set { SetColumnValue(Columns.Impression, value); }
		}
		  
		[XmlAttribute("PONumber")]
		[Bindable(true)]
		public string PONumber 
		{
			get { return GetColumnValue<string>(Columns.PONumber); }
			set { SetColumnValue(Columns.PONumber, value); }
		}
		  
		[XmlAttribute("OtherRef")]
		[Bindable(true)]
		public string OtherRef 
		{
			get { return GetColumnValue<string>(Columns.OtherRef); }
			set { SetColumnValue(Columns.OtherRef, value); }
		}
		  
		[XmlAttribute("Copies")]
		[Bindable(true)]
		public int? Copies 
		{
			get { return GetColumnValue<int?>(Columns.Copies); }
			set { SetColumnValue(Columns.Copies, value); }
		}
		  
		[XmlAttribute("CopiesLastUpdated")]
		[Bindable(true)]
		public DateTime? CopiesLastUpdated 
		{
			get { return GetColumnValue<DateTime?>(Columns.CopiesLastUpdated); }
			set { SetColumnValue(Columns.CopiesLastUpdated, value); }
		}
		  
		[XmlAttribute("ItemDesc")]
		[Bindable(true)]
		public string ItemDesc 
		{
			get { return GetColumnValue<string>(Columns.ItemDesc); }
			set { SetColumnValue(Columns.ItemDesc, value); }
		}
		  
		[XmlAttribute("Packages")]
		[Bindable(true)]
		public int? Packages 
		{
			get { return GetColumnValue<int?>(Columns.Packages); }
			set { SetColumnValue(Columns.Packages, value); }
		}
		  
		[XmlAttribute("PackageID")]
		[Bindable(true)]
		public int? PackageID 
		{
			get { return GetColumnValue<int?>(Columns.PackageID); }
			set { SetColumnValue(Columns.PackageID, value); }
		}
		  
		[XmlAttribute("Weight")]
		[Bindable(true)]
		public int? Weight 
		{
			get { return GetColumnValue<int?>(Columns.Weight); }
			set { SetColumnValue(Columns.Weight, value); }
		}
		  
		[XmlAttribute("Volume")]
		[Bindable(true)]
		public int? Volume 
		{
			get { return GetColumnValue<int?>(Columns.Volume); }
			set { SetColumnValue(Columns.Volume, value); }
		}
		  
		[XmlAttribute("PerCopy")]
		[Bindable(true)]
		public double? PerCopy 
		{
			get { return GetColumnValue<double?>(Columns.PerCopy); }
			set { SetColumnValue(Columns.PerCopy, value); }
		}
		  
		[XmlAttribute("TotalValue")]
		[Bindable(true)]
		public decimal? TotalValue 
		{
			get { return GetColumnValue<decimal?>(Columns.TotalValue); }
			set { SetColumnValue(Columns.TotalValue, value); }
		}
		  
		[XmlAttribute("Ts")]
		[Bindable(true)]
		public byte[] Ts 
		{
			get { return GetColumnValue<byte[]>(Columns.Ts); }
			set { SetColumnValue(Columns.Ts, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int? varOrderNumber,string varTitle,string varIsbn,string varSSRNo,DateTime? varSSRDate,string varImpression,string varPONumber,string varOtherRef,int? varCopies,DateTime? varCopiesLastUpdated,string varItemDesc,int? varPackages,int? varPackageID,int? varWeight,int? varVolume,double? varPerCopy,decimal? varTotalValue,byte[] varTs)
		{
			ItemTable item = new ItemTable();
			
			item.OrderNumber = varOrderNumber;
			
			item.Title = varTitle;
			
			item.Isbn = varIsbn;
			
			item.SSRNo = varSSRNo;
			
			item.SSRDate = varSSRDate;
			
			item.Impression = varImpression;
			
			item.PONumber = varPONumber;
			
			item.OtherRef = varOtherRef;
			
			item.Copies = varCopies;
			
			item.CopiesLastUpdated = varCopiesLastUpdated;
			
			item.ItemDesc = varItemDesc;
			
			item.Packages = varPackages;
			
			item.PackageID = varPackageID;
			
			item.Weight = varWeight;
			
			item.Volume = varVolume;
			
			item.PerCopy = varPerCopy;
			
			item.TotalValue = varTotalValue;
			
			item.Ts = varTs;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varTitleID,int? varOrderNumber,string varTitle,string varIsbn,string varSSRNo,DateTime? varSSRDate,string varImpression,string varPONumber,string varOtherRef,int? varCopies,DateTime? varCopiesLastUpdated,string varItemDesc,int? varPackages,int? varPackageID,int? varWeight,int? varVolume,double? varPerCopy,decimal? varTotalValue,byte[] varTs)
		{
			ItemTable item = new ItemTable();
			
				item.TitleID = varTitleID;
			
				item.OrderNumber = varOrderNumber;
			
				item.Title = varTitle;
			
				item.Isbn = varIsbn;
			
				item.SSRNo = varSSRNo;
			
				item.SSRDate = varSSRDate;
			
				item.Impression = varImpression;
			
				item.PONumber = varPONumber;
			
				item.OtherRef = varOtherRef;
			
				item.Copies = varCopies;
			
				item.CopiesLastUpdated = varCopiesLastUpdated;
			
				item.ItemDesc = varItemDesc;
			
				item.Packages = varPackages;
			
				item.PackageID = varPackageID;
			
				item.Weight = varWeight;
			
				item.Volume = varVolume;
			
				item.PerCopy = varPerCopy;
			
				item.TotalValue = varTotalValue;
			
				item.Ts = varTs;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn TitleIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn OrderNumberColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn TitleColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn IsbnColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SSRNoColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SSRDateColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn ImpressionColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn PONumberColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn OtherRefColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn CopiesColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn CopiesLastUpdatedColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn ItemDescColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn PackagesColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn PackageIDColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn WeightColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn VolumeColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn PerCopyColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn TotalValueColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn TsColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string TitleID = @"TitleID";
			 public static string OrderNumber = @"OrderNumber";
			 public static string Title = @"Title";
			 public static string Isbn = @"ISBN";
			 public static string SSRNo = @"SSRNo";
			 public static string SSRDate = @"SSRDate";
			 public static string Impression = @"Impression";
			 public static string PONumber = @"PONumber";
			 public static string OtherRef = @"OtherRef";
			 public static string Copies = @"Copies";
			 public static string CopiesLastUpdated = @"CopiesLastUpdated";
			 public static string ItemDesc = @"ItemDesc";
			 public static string Packages = @"Packages";
			 public static string PackageID = @"PackageID";
			 public static string Weight = @"Weight";
			 public static string Volume = @"Volume";
			 public static string PerCopy = @"PerCopy";
			 public static string TotalValue = @"TotalValue";
			 public static string Ts = @"TS";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
