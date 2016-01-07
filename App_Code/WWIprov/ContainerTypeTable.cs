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
	/// Strongly-typed collection for the ContainerTypeTable class.
	/// </summary>
    [Serializable]
	public partial class ContainerTypeTableCollection : ActiveList<ContainerTypeTable, ContainerTypeTableCollection>
	{	   
		public ContainerTypeTableCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>ContainerTypeTableCollection</returns>
		public ContainerTypeTableCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                ContainerTypeTable o = this[i];
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
	/// This is an ActiveRecord class which wraps the ContainerTypeTable table.
	/// </summary>
	[Serializable]
	public partial class ContainerTypeTable : ActiveRecord<ContainerTypeTable>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public ContainerTypeTable()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public ContainerTypeTable(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public ContainerTypeTable(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public ContainerTypeTable(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("ContainerTypeTable", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarContainerSizeID = new TableSchema.TableColumn(schema);
				colvarContainerSizeID.ColumnName = "ContainerSizeID";
				colvarContainerSizeID.DataType = DbType.Int32;
				colvarContainerSizeID.MaxLength = 0;
				colvarContainerSizeID.AutoIncrement = true;
				colvarContainerSizeID.IsNullable = false;
				colvarContainerSizeID.IsPrimaryKey = true;
				colvarContainerSizeID.IsForeignKey = false;
				colvarContainerSizeID.IsReadOnly = false;
				colvarContainerSizeID.DefaultSetting = @"";
				colvarContainerSizeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContainerSizeID);
				
				TableSchema.TableColumn colvarContainerType = new TableSchema.TableColumn(schema);
				colvarContainerType.ColumnName = "ContainerType";
				colvarContainerType.DataType = DbType.String;
				colvarContainerType.MaxLength = 50;
				colvarContainerType.AutoIncrement = false;
				colvarContainerType.IsNullable = false;
				colvarContainerType.IsPrimaryKey = false;
				colvarContainerType.IsForeignKey = false;
				colvarContainerType.IsReadOnly = false;
				colvarContainerType.DefaultSetting = @"";
				colvarContainerType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContainerType);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("ContainerTypeTable",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("ContainerSizeID")]
		[Bindable(true)]
		public int ContainerSizeID 
		{
			get { return GetColumnValue<int>(Columns.ContainerSizeID); }
			set { SetColumnValue(Columns.ContainerSizeID, value); }
		}
		  
		[XmlAttribute("ContainerType")]
		[Bindable(true)]
		public string ContainerType 
		{
			get { return GetColumnValue<string>(Columns.ContainerType); }
			set { SetColumnValue(Columns.ContainerType, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varContainerType)
		{
			ContainerTypeTable item = new ContainerTypeTable();
			
			item.ContainerType = varContainerType;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varContainerSizeID,string varContainerType)
		{
			ContainerTypeTable item = new ContainerTypeTable();
			
				item.ContainerSizeID = varContainerSizeID;
			
				item.ContainerType = varContainerType;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn ContainerSizeIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ContainerTypeColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string ContainerSizeID = @"ContainerSizeID";
			 public static string ContainerType = @"ContainerType";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}