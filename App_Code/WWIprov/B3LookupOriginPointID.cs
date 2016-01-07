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
	/// Strongly-typed collection for the B3LookupOriginPointID class.
	/// </summary>
    [Serializable]
	public partial class B3LookupOriginPointIDCollection : ActiveList<B3LookupOriginPointID, B3LookupOriginPointIDCollection>
	{	   
		public B3LookupOriginPointIDCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>B3LookupOriginPointIDCollection</returns>
		public B3LookupOriginPointIDCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                B3LookupOriginPointID o = this[i];
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
	/// This is an ActiveRecord class which wraps the B3LookupOriginPointID table.
	/// </summary>
	[Serializable]
	public partial class B3LookupOriginPointID : ActiveRecord<B3LookupOriginPointID>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public B3LookupOriginPointID()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public B3LookupOriginPointID(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public B3LookupOriginPointID(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public B3LookupOriginPointID(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("B3LookupOriginPointID", TableType.Table, DataService.GetInstance("WWIprov"));
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
				
				TableSchema.TableColumn colvarOriginPointID = new TableSchema.TableColumn(schema);
				colvarOriginPointID.ColumnName = "OriginPointID";
				colvarOriginPointID.DataType = DbType.Int32;
				colvarOriginPointID.MaxLength = 0;
				colvarOriginPointID.AutoIncrement = false;
				colvarOriginPointID.IsNullable = false;
				colvarOriginPointID.IsPrimaryKey = false;
				colvarOriginPointID.IsForeignKey = false;
				colvarOriginPointID.IsReadOnly = false;
				colvarOriginPointID.DefaultSetting = @"";
				colvarOriginPointID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOriginPointID);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("B3LookupOriginPointID",schema);
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
		  
		[XmlAttribute("OriginPointID")]
		[Bindable(true)]
		public int OriginPointID 
		{
			get { return GetColumnValue<int>(Columns.OriginPointID); }
			set { SetColumnValue(Columns.OriginPointID, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varB3PickUpLocationID,int varOriginPointID)
		{
			B3LookupOriginPointID item = new B3LookupOriginPointID();
			
			item.B3PickUpLocationID = varB3PickUpLocationID;
			
			item.OriginPointID = varOriginPointID;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varB3PickUpLocationID,int varOriginPointID)
		{
			B3LookupOriginPointID item = new B3LookupOriginPointID();
			
				item.B3PickUpLocationID = varB3PickUpLocationID;
			
				item.OriginPointID = varOriginPointID;
			
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
        
        
        
        public static TableSchema.TableColumn OriginPointIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string B3PickUpLocationID = @"B3PickUpLocationID";
			 public static string OriginPointID = @"OriginPointID";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
