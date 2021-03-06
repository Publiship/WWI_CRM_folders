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
	/// Strongly-typed collection for the PricerCompanyRate class.
	/// </summary>
    [Serializable]
	public partial class PricerCompanyRateCollection : ActiveList<PricerCompanyRate, PricerCompanyRateCollection>
	{	   
		public PricerCompanyRateCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>PricerCompanyRateCollection</returns>
		public PricerCompanyRateCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                PricerCompanyRate o = this[i];
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
	/// This is an ActiveRecord class which wraps the pricer_company_rates table.
	/// </summary>
	[Serializable]
	public partial class PricerCompanyRate : ActiveRecord<PricerCompanyRate>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public PricerCompanyRate()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public PricerCompanyRate(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public PricerCompanyRate(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public PricerCompanyRate(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("pricer_company_rates", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarPricerRateId = new TableSchema.TableColumn(schema);
				colvarPricerRateId.ColumnName = "pricer_rate_Id";
				colvarPricerRateId.DataType = DbType.Int32;
				colvarPricerRateId.MaxLength = 0;
				colvarPricerRateId.AutoIncrement = true;
				colvarPricerRateId.IsNullable = false;
				colvarPricerRateId.IsPrimaryKey = true;
				colvarPricerRateId.IsForeignKey = false;
				colvarPricerRateId.IsReadOnly = false;
				colvarPricerRateId.DefaultSetting = @"";
				colvarPricerRateId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPricerRateId);
				
				TableSchema.TableColumn colvarPricerGroupId = new TableSchema.TableColumn(schema);
				colvarPricerGroupId.ColumnName = "pricer_group_Id";
				colvarPricerGroupId.DataType = DbType.Int32;
				colvarPricerGroupId.MaxLength = 0;
				colvarPricerGroupId.AutoIncrement = false;
				colvarPricerGroupId.IsNullable = false;
				colvarPricerGroupId.IsPrimaryKey = false;
				colvarPricerGroupId.IsForeignKey = false;
				colvarPricerGroupId.IsReadOnly = false;
				
						colvarPricerGroupId.DefaultSetting = @"((0))";
				colvarPricerGroupId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPricerGroupId);
				
				TableSchema.TableColumn colvarZoneName = new TableSchema.TableColumn(schema);
				colvarZoneName.ColumnName = "zone_name";
				colvarZoneName.DataType = DbType.String;
				colvarZoneName.MaxLength = 50;
				colvarZoneName.AutoIncrement = false;
				colvarZoneName.IsNullable = false;
				colvarZoneName.IsPrimaryKey = false;
				colvarZoneName.IsForeignKey = false;
				colvarZoneName.IsReadOnly = false;
				colvarZoneName.DefaultSetting = @"";
				colvarZoneName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarZoneName);
				
				TableSchema.TableColumn colvarZoneRate = new TableSchema.TableColumn(schema);
				colvarZoneRate.ColumnName = "zone_rate";
				colvarZoneRate.DataType = DbType.Double;
				colvarZoneRate.MaxLength = 0;
				colvarZoneRate.AutoIncrement = false;
				colvarZoneRate.IsNullable = false;
				colvarZoneRate.IsPrimaryKey = false;
				colvarZoneRate.IsForeignKey = false;
				colvarZoneRate.IsReadOnly = false;
				
						colvarZoneRate.DefaultSetting = @"((0))";
				colvarZoneRate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarZoneRate);
				
				TableSchema.TableColumn colvarRateTs = new TableSchema.TableColumn(schema);
				colvarRateTs.ColumnName = "rate_ts";
				colvarRateTs.DataType = DbType.Binary;
				colvarRateTs.MaxLength = 0;
				colvarRateTs.AutoIncrement = false;
				colvarRateTs.IsNullable = false;
				colvarRateTs.IsPrimaryKey = false;
				colvarRateTs.IsForeignKey = false;
				colvarRateTs.IsReadOnly = true;
				colvarRateTs.DefaultSetting = @"";
				colvarRateTs.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRateTs);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("pricer_company_rates",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("PricerRateId")]
		[Bindable(true)]
		public int PricerRateId 
		{
			get { return GetColumnValue<int>(Columns.PricerRateId); }
			set { SetColumnValue(Columns.PricerRateId, value); }
		}
		  
		[XmlAttribute("PricerGroupId")]
		[Bindable(true)]
		public int PricerGroupId 
		{
			get { return GetColumnValue<int>(Columns.PricerGroupId); }
			set { SetColumnValue(Columns.PricerGroupId, value); }
		}
		  
		[XmlAttribute("ZoneName")]
		[Bindable(true)]
		public string ZoneName 
		{
			get { return GetColumnValue<string>(Columns.ZoneName); }
			set { SetColumnValue(Columns.ZoneName, value); }
		}
		  
		[XmlAttribute("ZoneRate")]
		[Bindable(true)]
		public double ZoneRate 
		{
			get { return GetColumnValue<double>(Columns.ZoneRate); }
			set { SetColumnValue(Columns.ZoneRate, value); }
		}
		  
		[XmlAttribute("RateTs")]
		[Bindable(true)]
		public byte[] RateTs 
		{
			get { return GetColumnValue<byte[]>(Columns.RateTs); }
			set { SetColumnValue(Columns.RateTs, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varPricerGroupId,string varZoneName,double varZoneRate,byte[] varRateTs)
		{
			PricerCompanyRate item = new PricerCompanyRate();
			
			item.PricerGroupId = varPricerGroupId;
			
			item.ZoneName = varZoneName;
			
			item.ZoneRate = varZoneRate;
			
			item.RateTs = varRateTs;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varPricerRateId,int varPricerGroupId,string varZoneName,double varZoneRate,byte[] varRateTs)
		{
			PricerCompanyRate item = new PricerCompanyRate();
			
				item.PricerRateId = varPricerRateId;
			
				item.PricerGroupId = varPricerGroupId;
			
				item.ZoneName = varZoneName;
			
				item.ZoneRate = varZoneRate;
			
				item.RateTs = varRateTs;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn PricerRateIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn PricerGroupIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ZoneNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn ZoneRateColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn RateTsColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string PricerRateId = @"pricer_rate_Id";
			 public static string PricerGroupId = @"pricer_group_Id";
			 public static string ZoneName = @"zone_name";
			 public static string ZoneRate = @"zone_rate";
			 public static string RateTs = @"rate_ts";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
