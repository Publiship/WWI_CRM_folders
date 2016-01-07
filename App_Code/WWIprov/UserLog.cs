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
	/// Strongly-typed collection for the UserLog class.
	/// </summary>
    [Serializable]
	public partial class UserLogCollection : ActiveList<UserLog, UserLogCollection>
	{	   
		public UserLogCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>UserLogCollection</returns>
		public UserLogCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                UserLog o = this[i];
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
	/// This is an ActiveRecord class which wraps the UserLogs table.
	/// </summary>
	[Serializable]
	public partial class UserLog : ActiveRecord<UserLog>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public UserLog()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public UserLog(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public UserLog(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public UserLog(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("UserLogs", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "ID";
				colvarId.DataType = DbType.Int32;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarEmployeeID = new TableSchema.TableColumn(schema);
				colvarEmployeeID.ColumnName = "EmployeeID";
				colvarEmployeeID.DataType = DbType.Int32;
				colvarEmployeeID.MaxLength = 0;
				colvarEmployeeID.AutoIncrement = false;
				colvarEmployeeID.IsNullable = true;
				colvarEmployeeID.IsPrimaryKey = false;
				colvarEmployeeID.IsForeignKey = false;
				colvarEmployeeID.IsReadOnly = false;
				colvarEmployeeID.DefaultSetting = @"";
				colvarEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmployeeID);
				
				TableSchema.TableColumn colvarContactID = new TableSchema.TableColumn(schema);
				colvarContactID.ColumnName = "ContactID";
				colvarContactID.DataType = DbType.Int32;
				colvarContactID.MaxLength = 0;
				colvarContactID.AutoIncrement = false;
				colvarContactID.IsNullable = true;
				colvarContactID.IsPrimaryKey = false;
				colvarContactID.IsForeignKey = false;
				colvarContactID.IsReadOnly = false;
				colvarContactID.DefaultSetting = @"";
				colvarContactID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContactID);
				
				TableSchema.TableColumn colvarLogDate = new TableSchema.TableColumn(schema);
				colvarLogDate.ColumnName = "LogDate";
				colvarLogDate.DataType = DbType.DateTime;
				colvarLogDate.MaxLength = 0;
				colvarLogDate.AutoIncrement = false;
				colvarLogDate.IsNullable = true;
				colvarLogDate.IsPrimaryKey = false;
				colvarLogDate.IsForeignKey = false;
				colvarLogDate.IsReadOnly = false;
				colvarLogDate.DefaultSetting = @"";
				colvarLogDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLogDate);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("UserLogs",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public int Id 
		{
			get { return GetColumnValue<int>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("EmployeeID")]
		[Bindable(true)]
		public int? EmployeeID 
		{
			get { return GetColumnValue<int?>(Columns.EmployeeID); }
			set { SetColumnValue(Columns.EmployeeID, value); }
		}
		  
		[XmlAttribute("ContactID")]
		[Bindable(true)]
		public int? ContactID 
		{
			get { return GetColumnValue<int?>(Columns.ContactID); }
			set { SetColumnValue(Columns.ContactID, value); }
		}
		  
		[XmlAttribute("LogDate")]
		[Bindable(true)]
		public DateTime? LogDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.LogDate); }
			set { SetColumnValue(Columns.LogDate, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int? varEmployeeID,int? varContactID,DateTime? varLogDate)
		{
			UserLog item = new UserLog();
			
			item.EmployeeID = varEmployeeID;
			
			item.ContactID = varContactID;
			
			item.LogDate = varLogDate;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,int? varEmployeeID,int? varContactID,DateTime? varLogDate)
		{
			UserLog item = new UserLog();
			
				item.Id = varId;
			
				item.EmployeeID = varEmployeeID;
			
				item.ContactID = varContactID;
			
				item.LogDate = varLogDate;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn EmployeeIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ContactIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn LogDateColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string EmployeeID = @"EmployeeID";
			 public static string ContactID = @"ContactID";
			 public static string LogDate = @"LogDate";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
