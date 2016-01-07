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
	/// Strongly-typed collection for the B3LookupPortID class.
	/// </summary>
    [Serializable]
	public partial class B3LookupPortIDCollection : ActiveList<B3LookupPortID, B3LookupPortIDCollection>
	{	   
		public B3LookupPortIDCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>B3LookupPortIDCollection</returns>
		public B3LookupPortIDCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                B3LookupPortID o = this[i];
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
	/// This is an ActiveRecord class which wraps the B3LookupPortID table.
	/// </summary>
	[Serializable]
	public partial class B3LookupPortID : ActiveRecord<B3LookupPortID>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public B3LookupPortID()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public B3LookupPortID(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public B3LookupPortID(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public B3LookupPortID(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("B3LookupPortID", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarB3PickUpLocationID = new TableSchema.TableColumn(schema);
				colvarB3PickUpLocationID.ColumnName = "B3PickUpLocationID";
				colvarB3PickUpLocationID.DataType = DbType.Int32;
				colvarB3PickUpLocationID.MaxLength = 0;
				colvarB3PickUpLocationID.AutoIncrement = false;
				colvarB3PickUpLocationID.IsNullable = false;
				colvarB3PickUpLocationID.IsPrimaryKey = true;
				colvarB3PickUpLocationID.IsForeignKey = false;
				colvarB3PickUpLocationID.IsReadOnly = false;
				colvarB3PickUpLocationID.DefaultSetting = @"";
				colvarB3PickUpLocationID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarB3PickUpLocationID);
				
				TableSchema.TableColumn colvarPortID = new TableSchema.TableColumn(schema);
				colvarPortID.ColumnName = "PortID";
				colvarPortID.DataType = DbType.Int32;
				colvarPortID.MaxLength = 0;
				colvarPortID.AutoIncrement = false;
				colvarPortID.IsNullable = false;
				colvarPortID.IsPrimaryKey = false;
				colvarPortID.IsForeignKey = false;
				colvarPortID.IsReadOnly = false;
				colvarPortID.DefaultSetting = @"";
				colvarPortID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPortID);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("B3LookupPortID",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("B3PickUpLocationID")]
		[Bindable(true)]
		public int B3PickUpLocationID 
		{
			get { return GetColumnValue<int>(Columns.B3PickUpLocationID); }
			set { SetColumnValue(Columns.B3PickUpLocationID, value); }
		}
		  
		[XmlAttribute("PortID")]
		[Bindable(true)]
		public int PortID 
		{
			get { return GetColumnValue<int>(Columns.PortID); }
			set { SetColumnValue(Columns.PortID, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varB3PickUpLocationID,int varPortID)
		{
			B3LookupPortID item = new B3LookupPortID();
			
			item.B3PickUpLocationID = varB3PickUpLocationID;
			
			item.PortID = varPortID;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varB3PickUpLocationID,int varPortID)
		{
			B3LookupPortID item = new B3LookupPortID();
			
				item.B3PickUpLocationID = varB3PickUpLocationID;
			
				item.PortID = varPortID;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn B3PickUpLocationIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn PortIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string B3PickUpLocationID = @"B3PickUpLocationID";
			 public static string PortID = @"PortID";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
