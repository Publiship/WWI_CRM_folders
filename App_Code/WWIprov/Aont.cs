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
	/// Strongly-typed collection for the Aont class.
	/// </summary>
    [Serializable]
	public partial class AontCollection : ActiveList<Aont, AontCollection>
	{	   
		public AontCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>AontCollection</returns>
		public AontCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Aont o = this[i];
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
	/// This is an ActiveRecord class which wraps the AONT table.
	/// </summary>
	[Serializable]
	public partial class Aont : ActiveRecord<Aont>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Aont()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Aont(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Aont(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Aont(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("AONT", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarOrderNumber = new TableSchema.TableColumn(schema);
				colvarOrderNumber.ColumnName = "OrderNumber";
				colvarOrderNumber.DataType = DbType.Int32;
				colvarOrderNumber.MaxLength = 0;
				colvarOrderNumber.AutoIncrement = true;
				colvarOrderNumber.IsNullable = false;
				colvarOrderNumber.IsPrimaryKey = true;
				colvarOrderNumber.IsForeignKey = false;
				colvarOrderNumber.IsReadOnly = false;
				colvarOrderNumber.DefaultSetting = @"";
				colvarOrderNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOrderNumber);
				
				TableSchema.TableColumn colvarDateCreated = new TableSchema.TableColumn(schema);
				colvarDateCreated.ColumnName = "DateCreated";
				colvarDateCreated.DataType = DbType.DateTime;
				colvarDateCreated.MaxLength = 0;
				colvarDateCreated.AutoIncrement = false;
				colvarDateCreated.IsNullable = false;
				colvarDateCreated.IsPrimaryKey = false;
				colvarDateCreated.IsForeignKey = false;
				colvarDateCreated.IsReadOnly = false;
				colvarDateCreated.DefaultSetting = @"";
				colvarDateCreated.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDateCreated);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("AONT",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("OrderNumber")]
		[Bindable(true)]
		public int OrderNumber 
		{
			get { return GetColumnValue<int>(Columns.OrderNumber); }
			set { SetColumnValue(Columns.OrderNumber, value); }
		}
		  
		[XmlAttribute("DateCreated")]
		[Bindable(true)]
		public DateTime DateCreated 
		{
			get { return GetColumnValue<DateTime>(Columns.DateCreated); }
			set { SetColumnValue(Columns.DateCreated, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(DateTime varDateCreated)
		{
			Aont item = new Aont();
			
			item.DateCreated = varDateCreated;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varOrderNumber,DateTime varDateCreated)
		{
			Aont item = new Aont();
			
				item.OrderNumber = varOrderNumber;
			
				item.DateCreated = varDateCreated;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn OrderNumberColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn DateCreatedColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string OrderNumber = @"OrderNumber";
			 public static string DateCreated = @"DateCreated";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}