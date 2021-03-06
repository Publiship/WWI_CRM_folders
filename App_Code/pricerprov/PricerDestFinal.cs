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
	/// Strongly-typed collection for the PricerDestFinal class.
	/// </summary>
    [Serializable]
	public partial class PricerDestFinalCollection : ActiveList<PricerDestFinal, PricerDestFinalCollection>
	{	   
		public PricerDestFinalCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>PricerDestFinalCollection</returns>
		public PricerDestFinalCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                PricerDestFinal o = this[i];
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
	/// This is an ActiveRecord class which wraps the pricer_dest_final table.
	/// </summary>
	[Serializable]
	public partial class PricerDestFinal : ActiveRecord<PricerDestFinal>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public PricerDestFinal()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public PricerDestFinal(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public PricerDestFinal(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public PricerDestFinal(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("pricer_dest_final", TableType.Table, DataService.GetInstance("pricerprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarDestFinalId = new TableSchema.TableColumn(schema);
				colvarDestFinalId.ColumnName = "dest_final_ID";
				colvarDestFinalId.DataType = DbType.Int32;
				colvarDestFinalId.MaxLength = 0;
				colvarDestFinalId.AutoIncrement = true;
				colvarDestFinalId.IsNullable = false;
				colvarDestFinalId.IsPrimaryKey = true;
				colvarDestFinalId.IsForeignKey = false;
				colvarDestFinalId.IsReadOnly = false;
				colvarDestFinalId.DefaultSetting = @"";
				colvarDestFinalId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDestFinalId);
				
				TableSchema.TableColumn colvarDestCountryId = new TableSchema.TableColumn(schema);
				colvarDestCountryId.ColumnName = "dest_country_ID";
				colvarDestCountryId.DataType = DbType.Int32;
				colvarDestCountryId.MaxLength = 0;
				colvarDestCountryId.AutoIncrement = false;
				colvarDestCountryId.IsNullable = false;
				colvarDestCountryId.IsPrimaryKey = false;
				colvarDestCountryId.IsForeignKey = false;
				colvarDestCountryId.IsReadOnly = false;
				
						colvarDestCountryId.DefaultSetting = @"((0))";
				colvarDestCountryId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDestCountryId);
				
				TableSchema.TableColumn colvarDestFinal = new TableSchema.TableColumn(schema);
				colvarDestFinal.ColumnName = "dest_final";
				colvarDestFinal.DataType = DbType.AnsiString;
				colvarDestFinal.MaxLength = 100;
				colvarDestFinal.AutoIncrement = false;
				colvarDestFinal.IsNullable = true;
				colvarDestFinal.IsPrimaryKey = false;
				colvarDestFinal.IsForeignKey = false;
				colvarDestFinal.IsReadOnly = false;
				colvarDestFinal.DefaultSetting = @"";
				colvarDestFinal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDestFinal);
				
				TableSchema.TableColumn colvarCompanyGroup = new TableSchema.TableColumn(schema);
				colvarCompanyGroup.ColumnName = "company_group";
				colvarCompanyGroup.DataType = DbType.Int32;
				colvarCompanyGroup.MaxLength = 0;
				colvarCompanyGroup.AutoIncrement = false;
				colvarCompanyGroup.IsNullable = true;
				colvarCompanyGroup.IsPrimaryKey = false;
				colvarCompanyGroup.IsForeignKey = false;
				colvarCompanyGroup.IsReadOnly = false;
				
						colvarCompanyGroup.DefaultSetting = @"((0))";
				colvarCompanyGroup.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCompanyGroup);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["pricerprov"].AddSchema("pricer_dest_final",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("DestFinalId")]
		[Bindable(true)]
		public int DestFinalId 
		{
			get { return GetColumnValue<int>(Columns.DestFinalId); }
			set { SetColumnValue(Columns.DestFinalId, value); }
		}
		  
		[XmlAttribute("DestCountryId")]
		[Bindable(true)]
		public int DestCountryId 
		{
			get { return GetColumnValue<int>(Columns.DestCountryId); }
			set { SetColumnValue(Columns.DestCountryId, value); }
		}
		  
		[XmlAttribute("DestFinal")]
		[Bindable(true)]
		public string DestFinal 
		{
			get { return GetColumnValue<string>(Columns.DestFinal); }
			set { SetColumnValue(Columns.DestFinal, value); }
		}
		  
		[XmlAttribute("CompanyGroup")]
		[Bindable(true)]
		public int? CompanyGroup 
		{
			get { return GetColumnValue<int?>(Columns.CompanyGroup); }
			set { SetColumnValue(Columns.CompanyGroup, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varDestCountryId,string varDestFinal,int? varCompanyGroup)
		{
			PricerDestFinal item = new PricerDestFinal();
			
			item.DestCountryId = varDestCountryId;
			
			item.DestFinal = varDestFinal;
			
			item.CompanyGroup = varCompanyGroup;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varDestFinalId,int varDestCountryId,string varDestFinal,int? varCompanyGroup)
		{
			PricerDestFinal item = new PricerDestFinal();
			
				item.DestFinalId = varDestFinalId;
			
				item.DestCountryId = varDestCountryId;
			
				item.DestFinal = varDestFinal;
			
				item.CompanyGroup = varCompanyGroup;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn DestFinalIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn DestCountryIdColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn DestFinalColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn CompanyGroupColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string DestFinalId = @"dest_final_ID";
			 public static string DestCountryId = @"dest_country_ID";
			 public static string DestFinal = @"dest_final";
			 public static string CompanyGroup = @"company_group";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
