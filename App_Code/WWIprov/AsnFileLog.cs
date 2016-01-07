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
	/// Strongly-typed collection for the AsnFileLog class.
	/// </summary>
    [Serializable]
	public partial class AsnFileLogCollection : ActiveList<AsnFileLog, AsnFileLogCollection>
	{	   
		public AsnFileLogCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>AsnFileLogCollection</returns>
		public AsnFileLogCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                AsnFileLog o = this[i];
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
	/// This is an ActiveRecord class which wraps the ASN_file_log table.
	/// </summary>
	[Serializable]
	public partial class AsnFileLog : ActiveRecord<AsnFileLog>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public AsnFileLog()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public AsnFileLog(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public AsnFileLog(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public AsnFileLog(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("ASN_file_log", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarLogId = new TableSchema.TableColumn(schema);
				colvarLogId.ColumnName = "log_ID";
				colvarLogId.DataType = DbType.Int32;
				colvarLogId.MaxLength = 0;
				colvarLogId.AutoIncrement = true;
				colvarLogId.IsNullable = false;
				colvarLogId.IsPrimaryKey = true;
				colvarLogId.IsForeignKey = false;
				colvarLogId.IsReadOnly = false;
				colvarLogId.DefaultSetting = @"";
				colvarLogId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLogId);
				
				TableSchema.TableColumn colvarFileNumber = new TableSchema.TableColumn(schema);
				colvarFileNumber.ColumnName = "file_number";
				colvarFileNumber.DataType = DbType.Int32;
				colvarFileNumber.MaxLength = 0;
				colvarFileNumber.AutoIncrement = false;
				colvarFileNumber.IsNullable = true;
				colvarFileNumber.IsPrimaryKey = false;
				colvarFileNumber.IsForeignKey = false;
				colvarFileNumber.IsReadOnly = false;
				colvarFileNumber.DefaultSetting = @"";
				colvarFileNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileNumber);
				
				TableSchema.TableColumn colvarFileName = new TableSchema.TableColumn(schema);
				colvarFileName.ColumnName = "file_name";
				colvarFileName.DataType = DbType.String;
				colvarFileName.MaxLength = 50;
				colvarFileName.AutoIncrement = false;
				colvarFileName.IsNullable = true;
				colvarFileName.IsPrimaryKey = false;
				colvarFileName.IsForeignKey = false;
				colvarFileName.IsReadOnly = false;
				colvarFileName.DefaultSetting = @"";
				colvarFileName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileName);
				
				TableSchema.TableColumn colvarDateRequested = new TableSchema.TableColumn(schema);
				colvarDateRequested.ColumnName = "date_requested";
				colvarDateRequested.DataType = DbType.DateTime;
				colvarDateRequested.MaxLength = 0;
				colvarDateRequested.AutoIncrement = false;
				colvarDateRequested.IsNullable = true;
				colvarDateRequested.IsPrimaryKey = false;
				colvarDateRequested.IsForeignKey = false;
				colvarDateRequested.IsReadOnly = false;
				colvarDateRequested.DefaultSetting = @"";
				colvarDateRequested.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDateRequested);
				
				TableSchema.TableColumn colvarRequestedBy = new TableSchema.TableColumn(schema);
				colvarRequestedBy.ColumnName = "requested_by";
				colvarRequestedBy.DataType = DbType.String;
				colvarRequestedBy.MaxLength = 50;
				colvarRequestedBy.AutoIncrement = false;
				colvarRequestedBy.IsNullable = true;
				colvarRequestedBy.IsPrimaryKey = false;
				colvarRequestedBy.IsForeignKey = false;
				colvarRequestedBy.IsReadOnly = false;
				colvarRequestedBy.DefaultSetting = @"";
				colvarRequestedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRequestedBy);
				
				TableSchema.TableColumn colvarDateUploaded = new TableSchema.TableColumn(schema);
				colvarDateUploaded.ColumnName = "date_uploaded";
				colvarDateUploaded.DataType = DbType.DateTime;
				colvarDateUploaded.MaxLength = 0;
				colvarDateUploaded.AutoIncrement = false;
				colvarDateUploaded.IsNullable = true;
				colvarDateUploaded.IsPrimaryKey = false;
				colvarDateUploaded.IsForeignKey = false;
				colvarDateUploaded.IsReadOnly = false;
				colvarDateUploaded.DefaultSetting = @"";
				colvarDateUploaded.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDateUploaded);
				
				TableSchema.TableColumn colvarLogTs = new TableSchema.TableColumn(schema);
				colvarLogTs.ColumnName = "log_ts";
				colvarLogTs.DataType = DbType.Binary;
				colvarLogTs.MaxLength = 0;
				colvarLogTs.AutoIncrement = false;
				colvarLogTs.IsNullable = true;
				colvarLogTs.IsPrimaryKey = false;
				colvarLogTs.IsForeignKey = false;
				colvarLogTs.IsReadOnly = true;
				colvarLogTs.DefaultSetting = @"";
				colvarLogTs.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLogTs);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("ASN_file_log",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("LogId")]
		[Bindable(true)]
		public int LogId 
		{
			get { return GetColumnValue<int>(Columns.LogId); }
			set { SetColumnValue(Columns.LogId, value); }
		}
		  
		[XmlAttribute("FileNumber")]
		[Bindable(true)]
		public int? FileNumber 
		{
			get { return GetColumnValue<int?>(Columns.FileNumber); }
			set { SetColumnValue(Columns.FileNumber, value); }
		}
		  
		[XmlAttribute("FileName")]
		[Bindable(true)]
		public string FileName 
		{
			get { return GetColumnValue<string>(Columns.FileName); }
			set { SetColumnValue(Columns.FileName, value); }
		}
		  
		[XmlAttribute("DateRequested")]
		[Bindable(true)]
		public DateTime? DateRequested 
		{
			get { return GetColumnValue<DateTime?>(Columns.DateRequested); }
			set { SetColumnValue(Columns.DateRequested, value); }
		}
		  
		[XmlAttribute("RequestedBy")]
		[Bindable(true)]
		public string RequestedBy 
		{
			get { return GetColumnValue<string>(Columns.RequestedBy); }
			set { SetColumnValue(Columns.RequestedBy, value); }
		}
		  
		[XmlAttribute("DateUploaded")]
		[Bindable(true)]
		public DateTime? DateUploaded 
		{
			get { return GetColumnValue<DateTime?>(Columns.DateUploaded); }
			set { SetColumnValue(Columns.DateUploaded, value); }
		}
		  
		[XmlAttribute("LogTs")]
		[Bindable(true)]
		public byte[] LogTs 
		{
			get { return GetColumnValue<byte[]>(Columns.LogTs); }
			set { SetColumnValue(Columns.LogTs, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int? varFileNumber,string varFileName,DateTime? varDateRequested,string varRequestedBy,DateTime? varDateUploaded,byte[] varLogTs)
		{
			AsnFileLog item = new AsnFileLog();
			
			item.FileNumber = varFileNumber;
			
			item.FileName = varFileName;
			
			item.DateRequested = varDateRequested;
			
			item.RequestedBy = varRequestedBy;
			
			item.DateUploaded = varDateUploaded;
			
			item.LogTs = varLogTs;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varLogId,int? varFileNumber,string varFileName,DateTime? varDateRequested,string varRequestedBy,DateTime? varDateUploaded,byte[] varLogTs)
		{
			AsnFileLog item = new AsnFileLog();
			
				item.LogId = varLogId;
			
				item.FileNumber = varFileNumber;
			
				item.FileName = varFileName;
			
				item.DateRequested = varDateRequested;
			
				item.RequestedBy = varRequestedBy;
			
				item.DateUploaded = varDateUploaded;
			
				item.LogTs = varLogTs;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn LogIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn FileNumberColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn FileNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn DateRequestedColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn RequestedByColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn DateUploadedColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn LogTsColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string LogId = @"log_ID";
			 public static string FileNumber = @"file_number";
			 public static string FileName = @"file_name";
			 public static string DateRequested = @"date_requested";
			 public static string RequestedBy = @"requested_by";
			 public static string DateUploaded = @"date_uploaded";
			 public static string LogTs = @"log_ts";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
