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
	/// Strongly-typed collection for the EmployeesTable class.
	/// </summary>
    [Serializable]
	public partial class EmployeesTableCollection : ActiveList<EmployeesTable, EmployeesTableCollection>
	{	   
		public EmployeesTableCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>EmployeesTableCollection</returns>
		public EmployeesTableCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                EmployeesTable o = this[i];
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
	/// This is an ActiveRecord class which wraps the EmployeesTable table.
	/// </summary>
	[Serializable]
	public partial class EmployeesTable : ActiveRecord<EmployeesTable>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public EmployeesTable()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public EmployeesTable(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public EmployeesTable(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public EmployeesTable(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("EmployeesTable", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarEmployeeID = new TableSchema.TableColumn(schema);
				colvarEmployeeID.ColumnName = "EmployeeID";
				colvarEmployeeID.DataType = DbType.Int32;
				colvarEmployeeID.MaxLength = 0;
				colvarEmployeeID.AutoIncrement = true;
				colvarEmployeeID.IsNullable = false;
				colvarEmployeeID.IsPrimaryKey = true;
				colvarEmployeeID.IsForeignKey = false;
				colvarEmployeeID.IsReadOnly = false;
				colvarEmployeeID.DefaultSetting = @"";
				colvarEmployeeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmployeeID);
				
				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.String;
				colvarName.MaxLength = 50;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = true;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);
				
				TableSchema.TableColumn colvarDepartmentID = new TableSchema.TableColumn(schema);
				colvarDepartmentID.ColumnName = "DepartmentID";
				colvarDepartmentID.DataType = DbType.Int32;
				colvarDepartmentID.MaxLength = 0;
				colvarDepartmentID.AutoIncrement = false;
				colvarDepartmentID.IsNullable = true;
				colvarDepartmentID.IsPrimaryKey = false;
				colvarDepartmentID.IsForeignKey = false;
				colvarDepartmentID.IsReadOnly = false;
				colvarDepartmentID.DefaultSetting = @"";
				colvarDepartmentID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDepartmentID);
				
				TableSchema.TableColumn colvarSectionID = new TableSchema.TableColumn(schema);
				colvarSectionID.ColumnName = "SectionID";
				colvarSectionID.DataType = DbType.Int32;
				colvarSectionID.MaxLength = 0;
				colvarSectionID.AutoIncrement = false;
				colvarSectionID.IsNullable = true;
				colvarSectionID.IsPrimaryKey = false;
				colvarSectionID.IsForeignKey = false;
				colvarSectionID.IsReadOnly = false;
				colvarSectionID.DefaultSetting = @"";
				colvarSectionID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSectionID);
				
				TableSchema.TableColumn colvarOfficeID = new TableSchema.TableColumn(schema);
				colvarOfficeID.ColumnName = "OfficeID";
				colvarOfficeID.DataType = DbType.Int32;
				colvarOfficeID.MaxLength = 0;
				colvarOfficeID.AutoIncrement = false;
				colvarOfficeID.IsNullable = true;
				colvarOfficeID.IsPrimaryKey = false;
				colvarOfficeID.IsForeignKey = false;
				colvarOfficeID.IsReadOnly = false;
				colvarOfficeID.DefaultSetting = @"";
				colvarOfficeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOfficeID);
				
				TableSchema.TableColumn colvarEmailAddress = new TableSchema.TableColumn(schema);
				colvarEmailAddress.ColumnName = "EmailAddress";
				colvarEmailAddress.DataType = DbType.String;
				colvarEmailAddress.MaxLength = 50;
				colvarEmailAddress.AutoIncrement = false;
				colvarEmailAddress.IsNullable = true;
				colvarEmailAddress.IsPrimaryKey = false;
				colvarEmailAddress.IsForeignKey = false;
				colvarEmailAddress.IsReadOnly = false;
				colvarEmailAddress.DefaultSetting = @"";
				colvarEmailAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmailAddress);
				
				TableSchema.TableColumn colvarLive = new TableSchema.TableColumn(schema);
				colvarLive.ColumnName = "Live";
				colvarLive.DataType = DbType.Boolean;
				colvarLive.MaxLength = 0;
				colvarLive.AutoIncrement = false;
				colvarLive.IsNullable = true;
				colvarLive.IsPrimaryKey = false;
				colvarLive.IsForeignKey = false;
				colvarLive.IsReadOnly = false;
				colvarLive.DefaultSetting = @"";
				colvarLive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLive);
				
				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.String;
				colvarPassword.MaxLength = 50;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = true;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);
				
				TableSchema.TableColumn colvarOpeningForm = new TableSchema.TableColumn(schema);
				colvarOpeningForm.ColumnName = "OpeningForm";
				colvarOpeningForm.DataType = DbType.String;
				colvarOpeningForm.MaxLength = 50;
				colvarOpeningForm.AutoIncrement = false;
				colvarOpeningForm.IsNullable = true;
				colvarOpeningForm.IsPrimaryKey = false;
				colvarOpeningForm.IsForeignKey = false;
				colvarOpeningForm.IsReadOnly = false;
				colvarOpeningForm.DefaultSetting = @"";
				colvarOpeningForm.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOpeningForm);
				
				TableSchema.TableColumn colvarTs = new TableSchema.TableColumn(schema);
				colvarTs.ColumnName = "TS";
				colvarTs.DataType = DbType.Binary;
				colvarTs.MaxLength = 0;
				colvarTs.AutoIncrement = false;
				colvarTs.IsNullable = true;
				colvarTs.IsPrimaryKey = false;
				colvarTs.IsForeignKey = false;
				colvarTs.IsReadOnly = true;
				colvarTs.DefaultSetting = @"";
				colvarTs.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTs);
				
				TableSchema.TableColumn colvarDefaultView = new TableSchema.TableColumn(schema);
				colvarDefaultView.ColumnName = "DefaultView";
				colvarDefaultView.DataType = DbType.Int32;
				colvarDefaultView.MaxLength = 0;
				colvarDefaultView.AutoIncrement = false;
				colvarDefaultView.IsNullable = true;
				colvarDefaultView.IsPrimaryKey = false;
				colvarDefaultView.IsForeignKey = false;
				colvarDefaultView.IsReadOnly = false;
				
						colvarDefaultView.DefaultSetting = @"((0))";
				colvarDefaultView.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDefaultView);
				
				TableSchema.TableColumn colvarIsEditor = new TableSchema.TableColumn(schema);
				colvarIsEditor.ColumnName = "IsEditor";
				colvarIsEditor.DataType = DbType.Byte;
				colvarIsEditor.MaxLength = 0;
				colvarIsEditor.AutoIncrement = false;
				colvarIsEditor.IsNullable = true;
				colvarIsEditor.IsPrimaryKey = false;
				colvarIsEditor.IsForeignKey = false;
				colvarIsEditor.IsReadOnly = false;
				
						colvarIsEditor.DefaultSetting = @"((0))";
				colvarIsEditor.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsEditor);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("EmployeesTable",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("EmployeeID")]
		[Bindable(true)]
		public int EmployeeID 
		{
			get { return GetColumnValue<int>(Columns.EmployeeID); }
			set { SetColumnValue(Columns.EmployeeID, value); }
		}
		  
		[XmlAttribute("Name")]
		[Bindable(true)]
		public string Name 
		{
			get { return GetColumnValue<string>(Columns.Name); }
			set { SetColumnValue(Columns.Name, value); }
		}
		  
		[XmlAttribute("DepartmentID")]
		[Bindable(true)]
		public int? DepartmentID 
		{
			get { return GetColumnValue<int?>(Columns.DepartmentID); }
			set { SetColumnValue(Columns.DepartmentID, value); }
		}
		  
		[XmlAttribute("SectionID")]
		[Bindable(true)]
		public int? SectionID 
		{
			get { return GetColumnValue<int?>(Columns.SectionID); }
			set { SetColumnValue(Columns.SectionID, value); }
		}
		  
		[XmlAttribute("OfficeID")]
		[Bindable(true)]
		public int? OfficeID 
		{
			get { return GetColumnValue<int?>(Columns.OfficeID); }
			set { SetColumnValue(Columns.OfficeID, value); }
		}
		  
		[XmlAttribute("EmailAddress")]
		[Bindable(true)]
		public string EmailAddress 
		{
			get { return GetColumnValue<string>(Columns.EmailAddress); }
			set { SetColumnValue(Columns.EmailAddress, value); }
		}
		  
		[XmlAttribute("Live")]
		[Bindable(true)]
		public bool? Live 
		{
			get { return GetColumnValue<bool?>(Columns.Live); }
			set { SetColumnValue(Columns.Live, value); }
		}
		  
		[XmlAttribute("Password")]
		[Bindable(true)]
		public string Password 
		{
			get { return GetColumnValue<string>(Columns.Password); }
			set { SetColumnValue(Columns.Password, value); }
		}
		  
		[XmlAttribute("OpeningForm")]
		[Bindable(true)]
		public string OpeningForm 
		{
			get { return GetColumnValue<string>(Columns.OpeningForm); }
			set { SetColumnValue(Columns.OpeningForm, value); }
		}
		  
		[XmlAttribute("Ts")]
		[Bindable(true)]
		public byte[] Ts 
		{
			get { return GetColumnValue<byte[]>(Columns.Ts); }
			set { SetColumnValue(Columns.Ts, value); }
		}
		  
		[XmlAttribute("DefaultView")]
		[Bindable(true)]
		public int? DefaultView 
		{
			get { return GetColumnValue<int?>(Columns.DefaultView); }
			set { SetColumnValue(Columns.DefaultView, value); }
		}
		  
		[XmlAttribute("IsEditor")]
		[Bindable(true)]
		public byte? IsEditor 
		{
			get { return GetColumnValue<byte?>(Columns.IsEditor); }
			set { SetColumnValue(Columns.IsEditor, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varName,int? varDepartmentID,int? varSectionID,int? varOfficeID,string varEmailAddress,bool? varLive,string varPassword,string varOpeningForm,byte[] varTs,int? varDefaultView,byte? varIsEditor)
		{
			EmployeesTable item = new EmployeesTable();
			
			item.Name = varName;
			
			item.DepartmentID = varDepartmentID;
			
			item.SectionID = varSectionID;
			
			item.OfficeID = varOfficeID;
			
			item.EmailAddress = varEmailAddress;
			
			item.Live = varLive;
			
			item.Password = varPassword;
			
			item.OpeningForm = varOpeningForm;
			
			item.Ts = varTs;
			
			item.DefaultView = varDefaultView;
			
			item.IsEditor = varIsEditor;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varEmployeeID,string varName,int? varDepartmentID,int? varSectionID,int? varOfficeID,string varEmailAddress,bool? varLive,string varPassword,string varOpeningForm,byte[] varTs,int? varDefaultView,byte? varIsEditor)
		{
			EmployeesTable item = new EmployeesTable();
			
				item.EmployeeID = varEmployeeID;
			
				item.Name = varName;
			
				item.DepartmentID = varDepartmentID;
			
				item.SectionID = varSectionID;
			
				item.OfficeID = varOfficeID;
			
				item.EmailAddress = varEmailAddress;
			
				item.Live = varLive;
			
				item.Password = varPassword;
			
				item.OpeningForm = varOpeningForm;
			
				item.Ts = varTs;
			
				item.DefaultView = varDefaultView;
			
				item.IsEditor = varIsEditor;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn EmployeeIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn NameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn DepartmentIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SectionIDColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn OfficeIDColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn EmailAddressColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn LiveColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn PasswordColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn OpeningFormColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn TsColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn DefaultViewColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn IsEditorColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string EmployeeID = @"EmployeeID";
			 public static string Name = @"Name";
			 public static string DepartmentID = @"DepartmentID";
			 public static string SectionID = @"SectionID";
			 public static string OfficeID = @"OfficeID";
			 public static string EmailAddress = @"EmailAddress";
			 public static string Live = @"Live";
			 public static string Password = @"Password";
			 public static string OpeningForm = @"OpeningForm";
			 public static string Ts = @"TS";
			 public static string DefaultView = @"DefaultView";
			 public static string IsEditor = @"IsEditor";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}