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
	/// Strongly-typed collection for the PricerOriginPoint class.
	/// </summary>
    [Serializable]
	public partial class PricerOriginPointCollection : ActiveList<PricerOriginPoint, PricerOriginPointCollection>
	{	   
		public PricerOriginPointCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>PricerOriginPointCollection</returns>
		public PricerOriginPointCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                PricerOriginPoint o = this[i];
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
	/// This is an ActiveRecord class which wraps the pricer_origin_point table.
	/// </summary>
	[Serializable]
	public partial class PricerOriginPoint : ActiveRecord<PricerOriginPoint>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public PricerOriginPoint()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public PricerOriginPoint(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public PricerOriginPoint(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public PricerOriginPoint(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("pricer_origin_point", TableType.Table, DataService.GetInstance("pricerprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarOriginPointId = new TableSchema.TableColumn(schema);
				colvarOriginPointId.ColumnName = "origin_point_ID";
				colvarOriginPointId.DataType = DbType.Int32;
				colvarOriginPointId.MaxLength = 0;
				colvarOriginPointId.AutoIncrement = true;
				colvarOriginPointId.IsNullable = false;
				colvarOriginPointId.IsPrimaryKey = true;
				colvarOriginPointId.IsForeignKey = false;
				colvarOriginPointId.IsReadOnly = false;
				colvarOriginPointId.DefaultSetting = @"";
				colvarOriginPointId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOriginPointId);
				
				TableSchema.TableColumn colvarOriginPoint = new TableSchema.TableColumn(schema);
				colvarOriginPoint.ColumnName = "origin_point";
				colvarOriginPoint.DataType = DbType.AnsiString;
				colvarOriginPoint.MaxLength = 50;
				colvarOriginPoint.AutoIncrement = false;
				colvarOriginPoint.IsNullable = true;
				colvarOriginPoint.IsPrimaryKey = false;
				colvarOriginPoint.IsForeignKey = false;
				colvarOriginPoint.IsReadOnly = false;
				colvarOriginPoint.DefaultSetting = @"";
				colvarOriginPoint.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOriginPoint);
				
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
				DataService.Providers["pricerprov"].AddSchema("pricer_origin_point",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("OriginPointId")]
		[Bindable(true)]
		public int OriginPointId 
		{
			get { return GetColumnValue<int>(Columns.OriginPointId); }
			set { SetColumnValue(Columns.OriginPointId, value); }
		}
		  
		[XmlAttribute("OriginPoint")]
		[Bindable(true)]
		public string OriginPoint 
		{
			get { return GetColumnValue<string>(Columns.OriginPoint); }
			set { SetColumnValue(Columns.OriginPoint, value); }
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
		public static void Insert(string varOriginPoint,int? varCompanyGroup)
		{
			PricerOriginPoint item = new PricerOriginPoint();
			
			item.OriginPoint = varOriginPoint;
			
			item.CompanyGroup = varCompanyGroup;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varOriginPointId,string varOriginPoint,int? varCompanyGroup)
		{
			PricerOriginPoint item = new PricerOriginPoint();
			
				item.OriginPointId = varOriginPointId;
			
				item.OriginPoint = varOriginPoint;
			
				item.CompanyGroup = varCompanyGroup;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn OriginPointIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn OriginPointColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn CompanyGroupColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string OriginPointId = @"origin_point_ID";
			 public static string OriginPoint = @"origin_point";
			 public static string CompanyGroup = @"company_group";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
