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
namespace DAL.Pricer
{
	/// <summary>
	/// Strongly-typed collection for the PriceRequestTemp class.
	/// </summary>
    [Serializable]
	public partial class PriceRequestTempCollection : ActiveList<PriceRequestTemp, PriceRequestTempCollection>
	{	   
		public PriceRequestTempCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>PriceRequestTempCollection</returns>
		public PriceRequestTempCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                PriceRequestTemp o = this[i];
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
	/// This is an ActiveRecord class which wraps the price_request_temp table.
	/// </summary>
	[Serializable]
	public partial class PriceRequestTemp : ActiveRecord<PriceRequestTemp>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public PriceRequestTemp()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public PriceRequestTemp(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public PriceRequestTemp(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public PriceRequestTemp(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("price_request_temp", TableType.Table, DataService.GetInstance("pricerprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarRequestId = new TableSchema.TableColumn(schema);
				colvarRequestId.ColumnName = "request_Id";
				colvarRequestId.DataType = DbType.Int32;
				colvarRequestId.MaxLength = 0;
				colvarRequestId.AutoIncrement = true;
				colvarRequestId.IsNullable = false;
				colvarRequestId.IsPrimaryKey = true;
				colvarRequestId.IsForeignKey = false;
				colvarRequestId.IsReadOnly = false;
				colvarRequestId.DefaultSetting = @"";
				colvarRequestId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequestId);
				
				TableSchema.TableColumn colvarRequestDate = new TableSchema.TableColumn(schema);
				colvarRequestDate.ColumnName = "request_date";
				colvarRequestDate.DataType = DbType.DateTime;
				colvarRequestDate.MaxLength = 0;
				colvarRequestDate.AutoIncrement = false;
				colvarRequestDate.IsNullable = true;
				colvarRequestDate.IsPrimaryKey = false;
				colvarRequestDate.IsForeignKey = false;
				colvarRequestDate.IsReadOnly = false;
				colvarRequestDate.DefaultSetting = @"";
				colvarRequestDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequestDate);
				
				TableSchema.TableColumn colvarRequestUserId = new TableSchema.TableColumn(schema);
				colvarRequestUserId.ColumnName = "request_user_id";
				colvarRequestUserId.DataType = DbType.Int32;
				colvarRequestUserId.MaxLength = 0;
				colvarRequestUserId.AutoIncrement = false;
				colvarRequestUserId.IsNullable = true;
				colvarRequestUserId.IsPrimaryKey = false;
				colvarRequestUserId.IsForeignKey = false;
				colvarRequestUserId.IsReadOnly = false;
				colvarRequestUserId.DefaultSetting = @"";
				colvarRequestUserId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequestUserId);
				
				TableSchema.TableColumn colvarRequestCompanyId = new TableSchema.TableColumn(schema);
				colvarRequestCompanyId.ColumnName = "request_company_id";
				colvarRequestCompanyId.DataType = DbType.Int32;
				colvarRequestCompanyId.MaxLength = 0;
				colvarRequestCompanyId.AutoIncrement = false;
				colvarRequestCompanyId.IsNullable = true;
				colvarRequestCompanyId.IsPrimaryKey = false;
				colvarRequestCompanyId.IsForeignKey = false;
				colvarRequestCompanyId.IsReadOnly = false;
				colvarRequestCompanyId.DefaultSetting = @"";
				colvarRequestCompanyId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequestCompanyId);
				
				TableSchema.TableColumn colvarRequestIp = new TableSchema.TableColumn(schema);
				colvarRequestIp.ColumnName = "request_ip";
				colvarRequestIp.DataType = DbType.String;
				colvarRequestIp.MaxLength = 20;
				colvarRequestIp.AutoIncrement = false;
				colvarRequestIp.IsNullable = true;
				colvarRequestIp.IsPrimaryKey = false;
				colvarRequestIp.IsForeignKey = false;
				colvarRequestIp.IsReadOnly = false;
				colvarRequestIp.DefaultSetting = @"";
				colvarRequestIp.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequestIp);
				
				TableSchema.TableColumn colvarBookTitle = new TableSchema.TableColumn(schema);
				colvarBookTitle.ColumnName = "book_title";
				colvarBookTitle.DataType = DbType.String;
				colvarBookTitle.MaxLength = 200;
				colvarBookTitle.AutoIncrement = false;
				colvarBookTitle.IsNullable = true;
				colvarBookTitle.IsPrimaryKey = false;
				colvarBookTitle.IsForeignKey = false;
				colvarBookTitle.IsReadOnly = false;
				colvarBookTitle.DefaultSetting = @"";
				colvarBookTitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBookTitle);
				
				TableSchema.TableColumn colvarInDimensions = new TableSchema.TableColumn(schema);
				colvarInDimensions.ColumnName = "in_dimensions";
				colvarInDimensions.DataType = DbType.Int32;
				colvarInDimensions.MaxLength = 0;
				colvarInDimensions.AutoIncrement = false;
				colvarInDimensions.IsNullable = false;
				colvarInDimensions.IsPrimaryKey = false;
				colvarInDimensions.IsForeignKey = false;
				colvarInDimensions.IsReadOnly = false;
				
						colvarInDimensions.DefaultSetting = @"((0))";
				colvarInDimensions.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInDimensions);
				
				TableSchema.TableColumn colvarInCurrency = new TableSchema.TableColumn(schema);
				colvarInCurrency.ColumnName = "in_currency";
				colvarInCurrency.DataType = DbType.String;
				colvarInCurrency.MaxLength = 50;
				colvarInCurrency.AutoIncrement = false;
				colvarInCurrency.IsNullable = true;
				colvarInCurrency.IsPrimaryKey = false;
				colvarInCurrency.IsForeignKey = false;
				colvarInCurrency.IsReadOnly = false;
				colvarInCurrency.DefaultSetting = @"";
				colvarInCurrency.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInCurrency);
				
				TableSchema.TableColumn colvarInPallet = new TableSchema.TableColumn(schema);
				colvarInPallet.ColumnName = "in_pallet";
				colvarInPallet.DataType = DbType.String;
				colvarInPallet.MaxLength = 25;
				colvarInPallet.AutoIncrement = false;
				colvarInPallet.IsNullable = true;
				colvarInPallet.IsPrimaryKey = false;
				colvarInPallet.IsForeignKey = false;
				colvarInPallet.IsReadOnly = false;
				colvarInPallet.DefaultSetting = @"";
				colvarInPallet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInPallet);
				
				TableSchema.TableColumn colvarInLength = new TableSchema.TableColumn(schema);
				colvarInLength.ColumnName = "in_length";
				colvarInLength.DataType = DbType.Double;
				colvarInLength.MaxLength = 0;
				colvarInLength.AutoIncrement = false;
				colvarInLength.IsNullable = true;
				colvarInLength.IsPrimaryKey = false;
				colvarInLength.IsForeignKey = false;
				colvarInLength.IsReadOnly = false;
				colvarInLength.DefaultSetting = @"";
				colvarInLength.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInLength);
				
				TableSchema.TableColumn colvarInWidth = new TableSchema.TableColumn(schema);
				colvarInWidth.ColumnName = "in_width";
				colvarInWidth.DataType = DbType.Double;
				colvarInWidth.MaxLength = 0;
				colvarInWidth.AutoIncrement = false;
				colvarInWidth.IsNullable = true;
				colvarInWidth.IsPrimaryKey = false;
				colvarInWidth.IsForeignKey = false;
				colvarInWidth.IsReadOnly = false;
				colvarInWidth.DefaultSetting = @"";
				colvarInWidth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInWidth);
				
				TableSchema.TableColumn colvarInDepth = new TableSchema.TableColumn(schema);
				colvarInDepth.ColumnName = "in_depth";
				colvarInDepth.DataType = DbType.Double;
				colvarInDepth.MaxLength = 0;
				colvarInDepth.AutoIncrement = false;
				colvarInDepth.IsNullable = true;
				colvarInDepth.IsPrimaryKey = false;
				colvarInDepth.IsForeignKey = false;
				colvarInDepth.IsReadOnly = false;
				colvarInDepth.DefaultSetting = @"";
				colvarInDepth.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInDepth);
				
				TableSchema.TableColumn colvarInWeight = new TableSchema.TableColumn(schema);
				colvarInWeight.ColumnName = "in_weight";
				colvarInWeight.DataType = DbType.Double;
				colvarInWeight.MaxLength = 0;
				colvarInWeight.AutoIncrement = false;
				colvarInWeight.IsNullable = true;
				colvarInWeight.IsPrimaryKey = false;
				colvarInWeight.IsForeignKey = false;
				colvarInWeight.IsReadOnly = false;
				colvarInWeight.DefaultSetting = @"";
				colvarInWeight.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInWeight);
				
				TableSchema.TableColumn colvarInExtent = new TableSchema.TableColumn(schema);
				colvarInExtent.ColumnName = "in_extent";
				colvarInExtent.DataType = DbType.Double;
				colvarInExtent.MaxLength = 0;
				colvarInExtent.AutoIncrement = false;
				colvarInExtent.IsNullable = true;
				colvarInExtent.IsPrimaryKey = false;
				colvarInExtent.IsForeignKey = false;
				colvarInExtent.IsReadOnly = false;
				colvarInExtent.DefaultSetting = @"";
				colvarInExtent.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInExtent);
				
				TableSchema.TableColumn colvarInPapergsm = new TableSchema.TableColumn(schema);
				colvarInPapergsm.ColumnName = "in_papergsm";
				colvarInPapergsm.DataType = DbType.Double;
				colvarInPapergsm.MaxLength = 0;
				colvarInPapergsm.AutoIncrement = false;
				colvarInPapergsm.IsNullable = true;
				colvarInPapergsm.IsPrimaryKey = false;
				colvarInPapergsm.IsForeignKey = false;
				colvarInPapergsm.IsReadOnly = false;
				colvarInPapergsm.DefaultSetting = @"";
				colvarInPapergsm.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInPapergsm);
				
				TableSchema.TableColumn colvarInHardback = new TableSchema.TableColumn(schema);
				colvarInHardback.ColumnName = "in_hardback";
				colvarInHardback.DataType = DbType.Boolean;
				colvarInHardback.MaxLength = 0;
				colvarInHardback.AutoIncrement = false;
				colvarInHardback.IsNullable = true;
				colvarInHardback.IsPrimaryKey = false;
				colvarInHardback.IsForeignKey = false;
				colvarInHardback.IsReadOnly = false;
				colvarInHardback.DefaultSetting = @"";
				colvarInHardback.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInHardback);
				
				TableSchema.TableColumn colvarCopiesCarton = new TableSchema.TableColumn(schema);
				colvarCopiesCarton.ColumnName = "copies_carton";
				colvarCopiesCarton.DataType = DbType.Int32;
				colvarCopiesCarton.MaxLength = 0;
				colvarCopiesCarton.AutoIncrement = false;
				colvarCopiesCarton.IsNullable = true;
				colvarCopiesCarton.IsPrimaryKey = false;
				colvarCopiesCarton.IsForeignKey = false;
				colvarCopiesCarton.IsReadOnly = false;
				colvarCopiesCarton.DefaultSetting = @"";
				colvarCopiesCarton.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCopiesCarton);
				
				TableSchema.TableColumn colvarOriginName = new TableSchema.TableColumn(schema);
				colvarOriginName.ColumnName = "origin_name";
				colvarOriginName.DataType = DbType.String;
				colvarOriginName.MaxLength = 75;
				colvarOriginName.AutoIncrement = false;
				colvarOriginName.IsNullable = true;
				colvarOriginName.IsPrimaryKey = false;
				colvarOriginName.IsForeignKey = false;
				colvarOriginName.IsReadOnly = false;
				colvarOriginName.DefaultSetting = @"";
				colvarOriginName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOriginName);
				
				TableSchema.TableColumn colvarCountryName = new TableSchema.TableColumn(schema);
				colvarCountryName.ColumnName = "country_name";
				colvarCountryName.DataType = DbType.String;
				colvarCountryName.MaxLength = 75;
				colvarCountryName.AutoIncrement = false;
				colvarCountryName.IsNullable = true;
				colvarCountryName.IsPrimaryKey = false;
				colvarCountryName.IsForeignKey = false;
				colvarCountryName.IsReadOnly = false;
				colvarCountryName.DefaultSetting = @"";
				colvarCountryName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountryName);
				
				TableSchema.TableColumn colvarFinalName = new TableSchema.TableColumn(schema);
				colvarFinalName.ColumnName = "final_name";
				colvarFinalName.DataType = DbType.String;
				colvarFinalName.MaxLength = 75;
				colvarFinalName.AutoIncrement = false;
				colvarFinalName.IsNullable = true;
				colvarFinalName.IsPrimaryKey = false;
				colvarFinalName.IsForeignKey = false;
				colvarFinalName.IsReadOnly = false;
				colvarFinalName.DefaultSetting = @"";
				colvarFinalName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFinalName);
				
				TableSchema.TableColumn colvarTotCopies = new TableSchema.TableColumn(schema);
				colvarTotCopies.ColumnName = "tot_copies";
				colvarTotCopies.DataType = DbType.Int32;
				colvarTotCopies.MaxLength = 0;
				colvarTotCopies.AutoIncrement = false;
				colvarTotCopies.IsNullable = true;
				colvarTotCopies.IsPrimaryKey = false;
				colvarTotCopies.IsForeignKey = false;
				colvarTotCopies.IsReadOnly = false;
				colvarTotCopies.DefaultSetting = @"";
				colvarTotCopies.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTotCopies);
				
				TableSchema.TableColumn colvarPrTimestamp = new TableSchema.TableColumn(schema);
				colvarPrTimestamp.ColumnName = "pr_timestamp";
				colvarPrTimestamp.DataType = DbType.Binary;
				colvarPrTimestamp.MaxLength = 0;
				colvarPrTimestamp.AutoIncrement = false;
				colvarPrTimestamp.IsNullable = false;
				colvarPrTimestamp.IsPrimaryKey = false;
				colvarPrTimestamp.IsForeignKey = false;
				colvarPrTimestamp.IsReadOnly = true;
				colvarPrTimestamp.DefaultSetting = @"";
				colvarPrTimestamp.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPrTimestamp);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["pricerprov"].AddSchema("price_request_temp",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("RequestId")]
		[Bindable(true)]
		public int RequestId 
		{
			get { return GetColumnValue<int>(Columns.RequestId); }
			set { SetColumnValue(Columns.RequestId, value); }
		}
		  
		[XmlAttribute("RequestDate")]
		[Bindable(true)]
		public DateTime? RequestDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.RequestDate); }
			set { SetColumnValue(Columns.RequestDate, value); }
		}
		  
		[XmlAttribute("RequestUserId")]
		[Bindable(true)]
		public int? RequestUserId 
		{
			get { return GetColumnValue<int?>(Columns.RequestUserId); }
			set { SetColumnValue(Columns.RequestUserId, value); }
		}
		  
		[XmlAttribute("RequestCompanyId")]
		[Bindable(true)]
		public int? RequestCompanyId 
		{
			get { return GetColumnValue<int?>(Columns.RequestCompanyId); }
			set { SetColumnValue(Columns.RequestCompanyId, value); }
		}
		  
		[XmlAttribute("RequestIp")]
		[Bindable(true)]
		public string RequestIp 
		{
			get { return GetColumnValue<string>(Columns.RequestIp); }
			set { SetColumnValue(Columns.RequestIp, value); }
		}
		  
		[XmlAttribute("BookTitle")]
		[Bindable(true)]
		public string BookTitle 
		{
			get { return GetColumnValue<string>(Columns.BookTitle); }
			set { SetColumnValue(Columns.BookTitle, value); }
		}
		  
		[XmlAttribute("InDimensions")]
		[Bindable(true)]
		public int InDimensions 
		{
			get { return GetColumnValue<int>(Columns.InDimensions); }
			set { SetColumnValue(Columns.InDimensions, value); }
		}
		  
		[XmlAttribute("InCurrency")]
		[Bindable(true)]
		public string InCurrency 
		{
			get { return GetColumnValue<string>(Columns.InCurrency); }
			set { SetColumnValue(Columns.InCurrency, value); }
		}
		  
		[XmlAttribute("InPallet")]
		[Bindable(true)]
		public string InPallet 
		{
			get { return GetColumnValue<string>(Columns.InPallet); }
			set { SetColumnValue(Columns.InPallet, value); }
		}
		  
		[XmlAttribute("InLength")]
		[Bindable(true)]
		public double? InLength 
		{
			get { return GetColumnValue<double?>(Columns.InLength); }
			set { SetColumnValue(Columns.InLength, value); }
		}
		  
		[XmlAttribute("InWidth")]
		[Bindable(true)]
		public double? InWidth 
		{
			get { return GetColumnValue<double?>(Columns.InWidth); }
			set { SetColumnValue(Columns.InWidth, value); }
		}
		  
		[XmlAttribute("InDepth")]
		[Bindable(true)]
		public double? InDepth 
		{
			get { return GetColumnValue<double?>(Columns.InDepth); }
			set { SetColumnValue(Columns.InDepth, value); }
		}
		  
		[XmlAttribute("InWeight")]
		[Bindable(true)]
		public double? InWeight 
		{
			get { return GetColumnValue<double?>(Columns.InWeight); }
			set { SetColumnValue(Columns.InWeight, value); }
		}
		  
		[XmlAttribute("InExtent")]
		[Bindable(true)]
		public double? InExtent 
		{
			get { return GetColumnValue<double?>(Columns.InExtent); }
			set { SetColumnValue(Columns.InExtent, value); }
		}
		  
		[XmlAttribute("InPapergsm")]
		[Bindable(true)]
		public double? InPapergsm 
		{
			get { return GetColumnValue<double?>(Columns.InPapergsm); }
			set { SetColumnValue(Columns.InPapergsm, value); }
		}
		  
		[XmlAttribute("InHardback")]
		[Bindable(true)]
		public bool? InHardback 
		{
			get { return GetColumnValue<bool?>(Columns.InHardback); }
			set { SetColumnValue(Columns.InHardback, value); }
		}
		  
		[XmlAttribute("CopiesCarton")]
		[Bindable(true)]
		public int? CopiesCarton 
		{
			get { return GetColumnValue<int?>(Columns.CopiesCarton); }
			set { SetColumnValue(Columns.CopiesCarton, value); }
		}
		  
		[XmlAttribute("OriginName")]
		[Bindable(true)]
		public string OriginName 
		{
			get { return GetColumnValue<string>(Columns.OriginName); }
			set { SetColumnValue(Columns.OriginName, value); }
		}
		  
		[XmlAttribute("CountryName")]
		[Bindable(true)]
		public string CountryName 
		{
			get { return GetColumnValue<string>(Columns.CountryName); }
			set { SetColumnValue(Columns.CountryName, value); }
		}
		  
		[XmlAttribute("FinalName")]
		[Bindable(true)]
		public string FinalName 
		{
			get { return GetColumnValue<string>(Columns.FinalName); }
			set { SetColumnValue(Columns.FinalName, value); }
		}
		  
		[XmlAttribute("TotCopies")]
		[Bindable(true)]
		public int? TotCopies 
		{
			get { return GetColumnValue<int?>(Columns.TotCopies); }
			set { SetColumnValue(Columns.TotCopies, value); }
		}
		  
		[XmlAttribute("PrTimestamp")]
		[Bindable(true)]
		public byte[] PrTimestamp 
		{
			get { return GetColumnValue<byte[]>(Columns.PrTimestamp); }
			set { SetColumnValue(Columns.PrTimestamp, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(DateTime? varRequestDate,int? varRequestUserId,int? varRequestCompanyId,string varRequestIp,string varBookTitle,int varInDimensions,string varInCurrency,string varInPallet,double? varInLength,double? varInWidth,double? varInDepth,double? varInWeight,double? varInExtent,double? varInPapergsm,bool? varInHardback,int? varCopiesCarton,string varOriginName,string varCountryName,string varFinalName,int? varTotCopies,byte[] varPrTimestamp)
		{
			PriceRequestTemp item = new PriceRequestTemp();
			
			item.RequestDate = varRequestDate;
			
			item.RequestUserId = varRequestUserId;
			
			item.RequestCompanyId = varRequestCompanyId;
			
			item.RequestIp = varRequestIp;
			
			item.BookTitle = varBookTitle;
			
			item.InDimensions = varInDimensions;
			
			item.InCurrency = varInCurrency;
			
			item.InPallet = varInPallet;
			
			item.InLength = varInLength;
			
			item.InWidth = varInWidth;
			
			item.InDepth = varInDepth;
			
			item.InWeight = varInWeight;
			
			item.InExtent = varInExtent;
			
			item.InPapergsm = varInPapergsm;
			
			item.InHardback = varInHardback;
			
			item.CopiesCarton = varCopiesCarton;
			
			item.OriginName = varOriginName;
			
			item.CountryName = varCountryName;
			
			item.FinalName = varFinalName;
			
			item.TotCopies = varTotCopies;
			
			item.PrTimestamp = varPrTimestamp;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varRequestId,DateTime? varRequestDate,int? varRequestUserId,int? varRequestCompanyId,string varRequestIp,string varBookTitle,int varInDimensions,string varInCurrency,string varInPallet,double? varInLength,double? varInWidth,double? varInDepth,double? varInWeight,double? varInExtent,double? varInPapergsm,bool? varInHardback,int? varCopiesCarton,string varOriginName,string varCountryName,string varFinalName,int? varTotCopies,byte[] varPrTimestamp)
		{
			PriceRequestTemp item = new PriceRequestTemp();
			
				item.RequestId = varRequestId;
			
				item.RequestDate = varRequestDate;
			
				item.RequestUserId = varRequestUserId;
			
				item.RequestCompanyId = varRequestCompanyId;
			
				item.RequestIp = varRequestIp;
			
				item.BookTitle = varBookTitle;
			
				item.InDimensions = varInDimensions;
			
				item.InCurrency = varInCurrency;
			
				item.InPallet = varInPallet;
			
				item.InLength = varInLength;
			
				item.InWidth = varInWidth;
			
				item.InDepth = varInDepth;
			
				item.InWeight = varInWeight;
			
				item.InExtent = varInExtent;
			
				item.InPapergsm = varInPapergsm;
			
				item.InHardback = varInHardback;
			
				item.CopiesCarton = varCopiesCarton;
			
				item.OriginName = varOriginName;
			
				item.CountryName = varCountryName;
			
				item.FinalName = varFinalName;
			
				item.TotCopies = varTotCopies;
			
				item.PrTimestamp = varPrTimestamp;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn RequestIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn RequestDateColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn RequestUserIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn RequestCompanyIdColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn RequestIpColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn BookTitleColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn InDimensionsColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn InCurrencyColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn InPalletColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn InLengthColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn InWidthColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn InDepthColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn InWeightColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn InExtentColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn InPapergsmColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn InHardbackColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn CopiesCartonColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn OriginNameColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn CountryNameColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn FinalNameColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn TotCopiesColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn PrTimestampColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string RequestId = @"request_Id";
			 public static string RequestDate = @"request_date";
			 public static string RequestUserId = @"request_user_id";
			 public static string RequestCompanyId = @"request_company_id";
			 public static string RequestIp = @"request_ip";
			 public static string BookTitle = @"book_title";
			 public static string InDimensions = @"in_dimensions";
			 public static string InCurrency = @"in_currency";
			 public static string InPallet = @"in_pallet";
			 public static string InLength = @"in_length";
			 public static string InWidth = @"in_width";
			 public static string InDepth = @"in_depth";
			 public static string InWeight = @"in_weight";
			 public static string InExtent = @"in_extent";
			 public static string InPapergsm = @"in_papergsm";
			 public static string InHardback = @"in_hardback";
			 public static string CopiesCarton = @"copies_carton";
			 public static string OriginName = @"origin_name";
			 public static string CountryName = @"country_name";
			 public static string FinalName = @"final_name";
			 public static string TotCopies = @"tot_copies";
			 public static string PrTimestamp = @"pr_timestamp";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}