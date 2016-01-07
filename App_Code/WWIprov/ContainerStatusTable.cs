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
	/// Strongly-typed collection for the ContainerStatusTable class.
	/// </summary>
    [Serializable]
	public partial class ContainerStatusTableCollection : ActiveList<ContainerStatusTable, ContainerStatusTableCollection>
	{	   
		public ContainerStatusTableCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>ContainerStatusTableCollection</returns>
		public ContainerStatusTableCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                ContainerStatusTable o = this[i];
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
	/// This is an ActiveRecord class which wraps the ContainerStatusTable table.
	/// </summary>
	[Serializable]
	public partial class ContainerStatusTable : ActiveRecord<ContainerStatusTable>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public ContainerStatusTable()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public ContainerStatusTable(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public ContainerStatusTable(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public ContainerStatusTable(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("ContainerStatusTable", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarContainerStatusID = new TableSchema.TableColumn(schema);
				colvarContainerStatusID.ColumnName = "ContainerStatusID";
				colvarContainerStatusID.DataType = DbType.Int32;
				colvarContainerStatusID.MaxLength = 0;
				colvarContainerStatusID.AutoIncrement = true;
				colvarContainerStatusID.IsNullable = false;
				colvarContainerStatusID.IsPrimaryKey = true;
				colvarContainerStatusID.IsForeignKey = false;
				colvarContainerStatusID.IsReadOnly = false;
				colvarContainerStatusID.DefaultSetting = @"";
				colvarContainerStatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContainerStatusID);
				
				TableSchema.TableColumn colvarContainerStatus = new TableSchema.TableColumn(schema);
				colvarContainerStatus.ColumnName = "ContainerStatus";
				colvarContainerStatus.DataType = DbType.String;
				colvarContainerStatus.MaxLength = 10;
				colvarContainerStatus.AutoIncrement = false;
				colvarContainerStatus.IsNullable = true;
				colvarContainerStatus.IsPrimaryKey = false;
				colvarContainerStatus.IsForeignKey = false;
				colvarContainerStatus.IsReadOnly = false;
				colvarContainerStatus.DefaultSetting = @"";
				colvarContainerStatus.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContainerStatus);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("ContainerStatusTable",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("ContainerStatusID")]
		[Bindable(true)]
		public int ContainerStatusID 
		{
			get { return GetColumnValue<int>(Columns.ContainerStatusID); }
			set { SetColumnValue(Columns.ContainerStatusID, value); }
		}
		  
		[XmlAttribute("ContainerStatus")]
		[Bindable(true)]
		public string ContainerStatus 
		{
			get { return GetColumnValue<string>(Columns.ContainerStatus); }
			set { SetColumnValue(Columns.ContainerStatus, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varContainerStatus)
		{
			ContainerStatusTable item = new ContainerStatusTable();
			
			item.ContainerStatus = varContainerStatus;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varContainerStatusID,string varContainerStatus)
		{
			ContainerStatusTable item = new ContainerStatusTable();
			
				item.ContainerStatusID = varContainerStatusID;
			
				item.ContainerStatus = varContainerStatus;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn ContainerStatusIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ContainerStatusColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string ContainerStatusID = @"ContainerStatusID";
			 public static string ContainerStatus = @"ContainerStatus";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
