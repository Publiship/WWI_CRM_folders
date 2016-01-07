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
	/// Strongly-typed collection for the CourierDetailsSubTable class.
	/// </summary>
    [Serializable]
	public partial class CourierDetailsSubTableCollection : ActiveList<CourierDetailsSubTable, CourierDetailsSubTableCollection>
	{	   
		public CourierDetailsSubTableCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>CourierDetailsSubTableCollection</returns>
		public CourierDetailsSubTableCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                CourierDetailsSubTable o = this[i];
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
	/// This is an ActiveRecord class which wraps the CourierDetailsSubTable table.
	/// </summary>
	[Serializable]
	public partial class CourierDetailsSubTable : ActiveRecord<CourierDetailsSubTable>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public CourierDetailsSubTable()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public CourierDetailsSubTable(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public CourierDetailsSubTable(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public CourierDetailsSubTable(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("CourierDetailsSubTable", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarCourierDetailID = new TableSchema.TableColumn(schema);
				colvarCourierDetailID.ColumnName = "CourierDetailID";
				colvarCourierDetailID.DataType = DbType.Int32;
				colvarCourierDetailID.MaxLength = 0;
				colvarCourierDetailID.AutoIncrement = true;
				colvarCourierDetailID.IsNullable = false;
				colvarCourierDetailID.IsPrimaryKey = true;
				colvarCourierDetailID.IsForeignKey = false;
				colvarCourierDetailID.IsReadOnly = false;
				colvarCourierDetailID.DefaultSetting = @"";
				colvarCourierDetailID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCourierDetailID);
				
				TableSchema.TableColumn colvarOrderNumber = new TableSchema.TableColumn(schema);
				colvarOrderNumber.ColumnName = "OrderNumber";
				colvarOrderNumber.DataType = DbType.Int32;
				colvarOrderNumber.MaxLength = 0;
				colvarOrderNumber.AutoIncrement = false;
				colvarOrderNumber.IsNullable = true;
				colvarOrderNumber.IsPrimaryKey = false;
				colvarOrderNumber.IsForeignKey = false;
				colvarOrderNumber.IsReadOnly = false;
				colvarOrderNumber.DefaultSetting = @"";
				colvarOrderNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOrderNumber);
				
				TableSchema.TableColumn colvarDocsDespatchID = new TableSchema.TableColumn(schema);
				colvarDocsDespatchID.ColumnName = "DocsDespatchID";
				colvarDocsDespatchID.DataType = DbType.Int32;
				colvarDocsDespatchID.MaxLength = 0;
				colvarDocsDespatchID.AutoIncrement = false;
				colvarDocsDespatchID.IsNullable = true;
				colvarDocsDespatchID.IsPrimaryKey = false;
				colvarDocsDespatchID.IsForeignKey = false;
				colvarDocsDespatchID.IsReadOnly = false;
				colvarDocsDespatchID.DefaultSetting = @"";
				colvarDocsDespatchID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDocsDespatchID);
				
				TableSchema.TableColumn colvarOriginal = new TableSchema.TableColumn(schema);
				colvarOriginal.ColumnName = "Original";
				colvarOriginal.DataType = DbType.Int32;
				colvarOriginal.MaxLength = 0;
				colvarOriginal.AutoIncrement = false;
				colvarOriginal.IsNullable = true;
				colvarOriginal.IsPrimaryKey = false;
				colvarOriginal.IsForeignKey = false;
				colvarOriginal.IsReadOnly = false;
				colvarOriginal.DefaultSetting = @"";
				colvarOriginal.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOriginal);
				
				TableSchema.TableColumn colvarDocumentationDespatched = new TableSchema.TableColumn(schema);
				colvarDocumentationDespatched.ColumnName = "DocumentationDespatched";
				colvarDocumentationDespatched.DataType = DbType.DateTime;
				colvarDocumentationDespatched.MaxLength = 0;
				colvarDocumentationDespatched.AutoIncrement = false;
				colvarDocumentationDespatched.IsNullable = true;
				colvarDocumentationDespatched.IsPrimaryKey = false;
				colvarDocumentationDespatched.IsForeignKey = false;
				colvarDocumentationDespatched.IsReadOnly = false;
				colvarDocumentationDespatched.DefaultSetting = @"";
				colvarDocumentationDespatched.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDocumentationDespatched);
				
				TableSchema.TableColumn colvarDocsLastUpdated = new TableSchema.TableColumn(schema);
				colvarDocsLastUpdated.ColumnName = "DocsLastUpdated";
				colvarDocsLastUpdated.DataType = DbType.DateTime;
				colvarDocsLastUpdated.MaxLength = 0;
				colvarDocsLastUpdated.AutoIncrement = false;
				colvarDocsLastUpdated.IsNullable = true;
				colvarDocsLastUpdated.IsPrimaryKey = false;
				colvarDocsLastUpdated.IsForeignKey = false;
				colvarDocsLastUpdated.IsReadOnly = false;
				colvarDocsLastUpdated.DefaultSetting = @"";
				colvarDocsLastUpdated.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDocsLastUpdated);
				
				TableSchema.TableColumn colvarCourierName = new TableSchema.TableColumn(schema);
				colvarCourierName.ColumnName = "CourierName";
				colvarCourierName.DataType = DbType.String;
				colvarCourierName.MaxLength = 50;
				colvarCourierName.AutoIncrement = false;
				colvarCourierName.IsNullable = true;
				colvarCourierName.IsPrimaryKey = false;
				colvarCourierName.IsForeignKey = false;
				colvarCourierName.IsReadOnly = false;
				colvarCourierName.DefaultSetting = @"";
				colvarCourierName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCourierName);
				
				TableSchema.TableColumn colvarAWBNumber = new TableSchema.TableColumn(schema);
				colvarAWBNumber.ColumnName = "AWBNumber";
				colvarAWBNumber.DataType = DbType.String;
				colvarAWBNumber.MaxLength = 20;
				colvarAWBNumber.AutoIncrement = false;
				colvarAWBNumber.IsNullable = true;
				colvarAWBNumber.IsPrimaryKey = false;
				colvarAWBNumber.IsForeignKey = false;
				colvarAWBNumber.IsReadOnly = false;
				colvarAWBNumber.DefaultSetting = @"";
				colvarAWBNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAWBNumber);
				
				TableSchema.TableColumn colvarSendByEmail = new TableSchema.TableColumn(schema);
				colvarSendByEmail.ColumnName = "SendByEmail";
				colvarSendByEmail.DataType = DbType.Boolean;
				colvarSendByEmail.MaxLength = 0;
				colvarSendByEmail.AutoIncrement = false;
				colvarSendByEmail.IsNullable = false;
				colvarSendByEmail.IsPrimaryKey = false;
				colvarSendByEmail.IsForeignKey = false;
				colvarSendByEmail.IsReadOnly = false;
				
						colvarSendByEmail.DefaultSetting = @"(0)";
				colvarSendByEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSendByEmail);
				
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("CourierDetailsSubTable",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("CourierDetailID")]
		[Bindable(true)]
		public int CourierDetailID 
		{
			get { return GetColumnValue<int>(Columns.CourierDetailID); }
			set { SetColumnValue(Columns.CourierDetailID, value); }
		}
		  
		[XmlAttribute("OrderNumber")]
		[Bindable(true)]
		public int? OrderNumber 
		{
			get { return GetColumnValue<int?>(Columns.OrderNumber); }
			set { SetColumnValue(Columns.OrderNumber, value); }
		}
		  
		[XmlAttribute("DocsDespatchID")]
		[Bindable(true)]
		public int? DocsDespatchID 
		{
			get { return GetColumnValue<int?>(Columns.DocsDespatchID); }
			set { SetColumnValue(Columns.DocsDespatchID, value); }
		}
		  
		[XmlAttribute("Original")]
		[Bindable(true)]
		public int? Original 
		{
			get { return GetColumnValue<int?>(Columns.Original); }
			set { SetColumnValue(Columns.Original, value); }
		}
		  
		[XmlAttribute("DocumentationDespatched")]
		[Bindable(true)]
		public DateTime? DocumentationDespatched 
		{
			get { return GetColumnValue<DateTime?>(Columns.DocumentationDespatched); }
			set { SetColumnValue(Columns.DocumentationDespatched, value); }
		}
		  
		[XmlAttribute("DocsLastUpdated")]
		[Bindable(true)]
		public DateTime? DocsLastUpdated 
		{
			get { return GetColumnValue<DateTime?>(Columns.DocsLastUpdated); }
			set { SetColumnValue(Columns.DocsLastUpdated, value); }
		}
		  
		[XmlAttribute("CourierName")]
		[Bindable(true)]
		public string CourierName 
		{
			get { return GetColumnValue<string>(Columns.CourierName); }
			set { SetColumnValue(Columns.CourierName, value); }
		}
		  
		[XmlAttribute("AWBNumber")]
		[Bindable(true)]
		public string AWBNumber 
		{
			get { return GetColumnValue<string>(Columns.AWBNumber); }
			set { SetColumnValue(Columns.AWBNumber, value); }
		}
		  
		[XmlAttribute("SendByEmail")]
		[Bindable(true)]
		public bool SendByEmail 
		{
			get { return GetColumnValue<bool>(Columns.SendByEmail); }
			set { SetColumnValue(Columns.SendByEmail, value); }
		}
		  
		[XmlAttribute("ContactID")]
		[Bindable(true)]
		public int? ContactID 
		{
			get { return GetColumnValue<int?>(Columns.ContactID); }
			set { SetColumnValue(Columns.ContactID, value); }
		}
		  
		[XmlAttribute("EmailAddress")]
		[Bindable(true)]
		public string EmailAddress 
		{
			get { return GetColumnValue<string>(Columns.EmailAddress); }
			set { SetColumnValue(Columns.EmailAddress, value); }
		}
		  
		[XmlAttribute("Ts")]
		[Bindable(true)]
		public byte[] Ts 
		{
			get { return GetColumnValue<byte[]>(Columns.Ts); }
			set { SetColumnValue(Columns.Ts, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int? varOrderNumber,int? varDocsDespatchID,int? varOriginal,DateTime? varDocumentationDespatched,DateTime? varDocsLastUpdated,string varCourierName,string varAWBNumber,bool varSendByEmail,int? varContactID,string varEmailAddress,byte[] varTs)
		{
			CourierDetailsSubTable item = new CourierDetailsSubTable();
			
			item.OrderNumber = varOrderNumber;
			
			item.DocsDespatchID = varDocsDespatchID;
			
			item.Original = varOriginal;
			
			item.DocumentationDespatched = varDocumentationDespatched;
			
			item.DocsLastUpdated = varDocsLastUpdated;
			
			item.CourierName = varCourierName;
			
			item.AWBNumber = varAWBNumber;
			
			item.SendByEmail = varSendByEmail;
			
			item.ContactID = varContactID;
			
			item.EmailAddress = varEmailAddress;
			
			item.Ts = varTs;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varCourierDetailID,int? varOrderNumber,int? varDocsDespatchID,int? varOriginal,DateTime? varDocumentationDespatched,DateTime? varDocsLastUpdated,string varCourierName,string varAWBNumber,bool varSendByEmail,int? varContactID,string varEmailAddress,byte[] varTs)
		{
			CourierDetailsSubTable item = new CourierDetailsSubTable();
			
				item.CourierDetailID = varCourierDetailID;
			
				item.OrderNumber = varOrderNumber;
			
				item.DocsDespatchID = varDocsDespatchID;
			
				item.Original = varOriginal;
			
				item.DocumentationDespatched = varDocumentationDespatched;
			
				item.DocsLastUpdated = varDocsLastUpdated;
			
				item.CourierName = varCourierName;
			
				item.AWBNumber = varAWBNumber;
			
				item.SendByEmail = varSendByEmail;
			
				item.ContactID = varContactID;
			
				item.EmailAddress = varEmailAddress;
			
				item.Ts = varTs;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn CourierDetailIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn OrderNumberColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn DocsDespatchIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn OriginalColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn DocumentationDespatchedColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn DocsLastUpdatedColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn CourierNameColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn AWBNumberColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn SendByEmailColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn ContactIDColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn EmailAddressColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn TsColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string CourierDetailID = @"CourierDetailID";
			 public static string OrderNumber = @"OrderNumber";
			 public static string DocsDespatchID = @"DocsDespatchID";
			 public static string Original = @"Original";
			 public static string DocumentationDespatched = @"DocumentationDespatched";
			 public static string DocsLastUpdated = @"DocsLastUpdated";
			 public static string CourierName = @"CourierName";
			 public static string AWBNumber = @"AWBNumber";
			 public static string SendByEmail = @"SendByEmail";
			 public static string ContactID = @"ContactID";
			 public static string EmailAddress = @"EmailAddress";
			 public static string Ts = @"TS";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}