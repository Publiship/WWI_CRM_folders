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
namespace DAL.CustomerTarget
{
	/// <summary>
	/// Strongly-typed collection for the CustomerTargetList class.
	/// </summary>
    [Serializable]
	public partial class CustomerTargetListCollection : ActiveList<CustomerTargetList, CustomerTargetListCollection>
	{	   
		public CustomerTargetListCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>CustomerTargetListCollection</returns>
		public CustomerTargetListCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                CustomerTargetList o = this[i];
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
	/// This is an ActiveRecord class which wraps the customer_target_list table.
	/// </summary>
	[Serializable]
	public partial class CustomerTargetList : ActiveRecord<CustomerTargetList>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public CustomerTargetList()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public CustomerTargetList(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public CustomerTargetList(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public CustomerTargetList(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("customer_target_list", TableType.Table, DataService.GetInstance("targetprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarTargetID = new TableSchema.TableColumn(schema);
				colvarTargetID.ColumnName = "targetID";
				colvarTargetID.DataType = DbType.Int32;
				colvarTargetID.MaxLength = 0;
				colvarTargetID.AutoIncrement = true;
				colvarTargetID.IsNullable = false;
				colvarTargetID.IsPrimaryKey = true;
				colvarTargetID.IsForeignKey = false;
				colvarTargetID.IsReadOnly = false;
				colvarTargetID.DefaultSetting = @"";
				colvarTargetID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTargetID);
				
				TableSchema.TableColumn colvarInsertDate = new TableSchema.TableColumn(schema);
				colvarInsertDate.ColumnName = "insert_date";
				colvarInsertDate.DataType = DbType.DateTime;
				colvarInsertDate.MaxLength = 0;
				colvarInsertDate.AutoIncrement = false;
				colvarInsertDate.IsNullable = true;
				colvarInsertDate.IsPrimaryKey = false;
				colvarInsertDate.IsForeignKey = false;
				colvarInsertDate.IsReadOnly = false;
				colvarInsertDate.DefaultSetting = @"";
				colvarInsertDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsertDate);
				
				TableSchema.TableColumn colvarInsertUser = new TableSchema.TableColumn(schema);
				colvarInsertUser.ColumnName = "insert_user";
				colvarInsertUser.DataType = DbType.String;
				colvarInsertUser.MaxLength = 25;
				colvarInsertUser.AutoIncrement = false;
				colvarInsertUser.IsNullable = true;
				colvarInsertUser.IsPrimaryKey = false;
				colvarInsertUser.IsForeignKey = false;
				colvarInsertUser.IsReadOnly = false;
				colvarInsertUser.DefaultSetting = @"";
				colvarInsertUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInsertUser);
				
				TableSchema.TableColumn colvarCompanyName = new TableSchema.TableColumn(schema);
				colvarCompanyName.ColumnName = "company_name";
				colvarCompanyName.DataType = DbType.String;
				colvarCompanyName.MaxLength = 75;
				colvarCompanyName.AutoIncrement = false;
				colvarCompanyName.IsNullable = true;
				colvarCompanyName.IsPrimaryKey = false;
				colvarCompanyName.IsForeignKey = false;
				colvarCompanyName.IsReadOnly = false;
				colvarCompanyName.DefaultSetting = @"";
				colvarCompanyName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCompanyName);
				
				TableSchema.TableColumn colvarContactName = new TableSchema.TableColumn(schema);
				colvarContactName.ColumnName = "contact_name";
				colvarContactName.DataType = DbType.String;
				colvarContactName.MaxLength = 50;
				colvarContactName.AutoIncrement = false;
				colvarContactName.IsNullable = true;
				colvarContactName.IsPrimaryKey = false;
				colvarContactName.IsForeignKey = false;
				colvarContactName.IsReadOnly = false;
				colvarContactName.DefaultSetting = @"";
				colvarContactName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContactName);
				
				TableSchema.TableColumn colvarContactEmail = new TableSchema.TableColumn(schema);
				colvarContactEmail.ColumnName = "contact_email";
				colvarContactEmail.DataType = DbType.String;
				colvarContactEmail.MaxLength = 50;
				colvarContactEmail.AutoIncrement = false;
				colvarContactEmail.IsNullable = true;
				colvarContactEmail.IsPrimaryKey = false;
				colvarContactEmail.IsForeignKey = false;
				colvarContactEmail.IsReadOnly = false;
				colvarContactEmail.DefaultSetting = @"";
				colvarContactEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContactEmail);
				
				TableSchema.TableColumn colvarContactPosition = new TableSchema.TableColumn(schema);
				colvarContactPosition.ColumnName = "contact_position";
				colvarContactPosition.DataType = DbType.String;
				colvarContactPosition.MaxLength = 30;
				colvarContactPosition.AutoIncrement = false;
				colvarContactPosition.IsNullable = true;
				colvarContactPosition.IsPrimaryKey = false;
				colvarContactPosition.IsForeignKey = false;
				colvarContactPosition.IsReadOnly = false;
				colvarContactPosition.DefaultSetting = @"";
				colvarContactPosition.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContactPosition);
				
				TableSchema.TableColumn colvarCompanyAddress = new TableSchema.TableColumn(schema);
				colvarCompanyAddress.ColumnName = "company_address";
				colvarCompanyAddress.DataType = DbType.String;
				colvarCompanyAddress.MaxLength = 200;
				colvarCompanyAddress.AutoIncrement = false;
				colvarCompanyAddress.IsNullable = true;
				colvarCompanyAddress.IsPrimaryKey = false;
				colvarCompanyAddress.IsForeignKey = false;
				colvarCompanyAddress.IsReadOnly = false;
				colvarCompanyAddress.DefaultSetting = @"";
				colvarCompanyAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCompanyAddress);
				
				TableSchema.TableColumn colvarTelNo = new TableSchema.TableColumn(schema);
				colvarTelNo.ColumnName = "tel_no";
				colvarTelNo.DataType = DbType.String;
				colvarTelNo.MaxLength = 50;
				colvarTelNo.AutoIncrement = false;
				colvarTelNo.IsNullable = true;
				colvarTelNo.IsPrimaryKey = false;
				colvarTelNo.IsForeignKey = false;
				colvarTelNo.IsReadOnly = false;
				colvarTelNo.DefaultSetting = @"";
				colvarTelNo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTelNo);
				
				TableSchema.TableColumn colvarPrintersInfo = new TableSchema.TableColumn(schema);
				colvarPrintersInfo.ColumnName = "printers_info";
				colvarPrintersInfo.DataType = DbType.AnsiString;
				colvarPrintersInfo.MaxLength = 2147483647;
				colvarPrintersInfo.AutoIncrement = false;
				colvarPrintersInfo.IsNullable = true;
				colvarPrintersInfo.IsPrimaryKey = false;
				colvarPrintersInfo.IsForeignKey = false;
				colvarPrintersInfo.IsReadOnly = false;
				colvarPrintersInfo.DefaultSetting = @"";
				colvarPrintersInfo.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPrintersInfo);
				
				TableSchema.TableColumn colvarLastContacted = new TableSchema.TableColumn(schema);
				colvarLastContacted.ColumnName = "last_contacted";
				colvarLastContacted.DataType = DbType.String;
				colvarLastContacted.MaxLength = 200;
				colvarLastContacted.AutoIncrement = false;
				colvarLastContacted.IsNullable = true;
				colvarLastContacted.IsPrimaryKey = false;
				colvarLastContacted.IsForeignKey = false;
				colvarLastContacted.IsReadOnly = false;
				colvarLastContacted.DefaultSetting = @"";
				colvarLastContacted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastContacted);
				
				TableSchema.TableColumn colvarShippingProfile = new TableSchema.TableColumn(schema);
				colvarShippingProfile.ColumnName = "shipping_profile";
				colvarShippingProfile.DataType = DbType.AnsiString;
				colvarShippingProfile.MaxLength = 2147483647;
				colvarShippingProfile.AutoIncrement = false;
				colvarShippingProfile.IsNullable = true;
				colvarShippingProfile.IsPrimaryKey = false;
				colvarShippingProfile.IsForeignKey = false;
				colvarShippingProfile.IsReadOnly = false;
				colvarShippingProfile.DefaultSetting = @"";
				colvarShippingProfile.ForeignKeyTableName = "";
				schema.Columns.Add(colvarShippingProfile);
				
				TableSchema.TableColumn colvarComments = new TableSchema.TableColumn(schema);
				colvarComments.ColumnName = "comments";
				colvarComments.DataType = DbType.AnsiString;
				colvarComments.MaxLength = 2147483647;
				colvarComments.AutoIncrement = false;
				colvarComments.IsNullable = true;
				colvarComments.IsPrimaryKey = false;
				colvarComments.IsForeignKey = false;
				colvarComments.IsReadOnly = false;
				colvarComments.DefaultSetting = @"";
				colvarComments.ForeignKeyTableName = "";
				schema.Columns.Add(colvarComments);
				
				TableSchema.TableColumn colvarSalesCode = new TableSchema.TableColumn(schema);
				colvarSalesCode.ColumnName = "sales_code";
				colvarSalesCode.DataType = DbType.String;
				colvarSalesCode.MaxLength = 5;
				colvarSalesCode.AutoIncrement = false;
				colvarSalesCode.IsNullable = true;
				colvarSalesCode.IsPrimaryKey = false;
				colvarSalesCode.IsForeignKey = false;
				colvarSalesCode.IsReadOnly = false;
				colvarSalesCode.DefaultSetting = @"";
				colvarSalesCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesCode);
				
				TableSchema.TableColumn colvarPriorityCode = new TableSchema.TableColumn(schema);
				colvarPriorityCode.ColumnName = "priority_code";
				colvarPriorityCode.DataType = DbType.String;
				colvarPriorityCode.MaxLength = 5;
				colvarPriorityCode.AutoIncrement = false;
				colvarPriorityCode.IsNullable = true;
				colvarPriorityCode.IsPrimaryKey = false;
				colvarPriorityCode.IsForeignKey = false;
				colvarPriorityCode.IsReadOnly = false;
				
						colvarPriorityCode.DefaultSetting = @"((0))";
				colvarPriorityCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPriorityCode);
				
				TableSchema.TableColumn colvarUpdateDate = new TableSchema.TableColumn(schema);
				colvarUpdateDate.ColumnName = "update_date";
				colvarUpdateDate.DataType = DbType.DateTime;
				colvarUpdateDate.MaxLength = 0;
				colvarUpdateDate.AutoIncrement = false;
				colvarUpdateDate.IsNullable = true;
				colvarUpdateDate.IsPrimaryKey = false;
				colvarUpdateDate.IsForeignKey = false;
				colvarUpdateDate.IsReadOnly = false;
				colvarUpdateDate.DefaultSetting = @"";
				colvarUpdateDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUpdateDate);
				
				TableSchema.TableColumn colvarUpdateUser = new TableSchema.TableColumn(schema);
				colvarUpdateUser.ColumnName = "update_user";
				colvarUpdateUser.DataType = DbType.String;
				colvarUpdateUser.MaxLength = 25;
				colvarUpdateUser.AutoIncrement = false;
				colvarUpdateUser.IsNullable = true;
				colvarUpdateUser.IsPrimaryKey = false;
				colvarUpdateUser.IsForeignKey = false;
				colvarUpdateUser.IsReadOnly = false;
				colvarUpdateUser.DefaultSetting = @"";
				colvarUpdateUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUpdateUser);
				
				TableSchema.TableColumn colvarDbtimestamp = new TableSchema.TableColumn(schema);
				colvarDbtimestamp.ColumnName = "dbtimestamp";
				colvarDbtimestamp.DataType = DbType.Binary;
				colvarDbtimestamp.MaxLength = 0;
				colvarDbtimestamp.AutoIncrement = false;
				colvarDbtimestamp.IsNullable = false;
				colvarDbtimestamp.IsPrimaryKey = false;
				colvarDbtimestamp.IsForeignKey = false;
				colvarDbtimestamp.IsReadOnly = true;
				colvarDbtimestamp.DefaultSetting = @"";
				colvarDbtimestamp.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDbtimestamp);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["targetprov"].AddSchema("customer_target_list",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("TargetID")]
		[Bindable(true)]
		public int TargetID 
		{
			get { return GetColumnValue<int>(Columns.TargetID); }
			set { SetColumnValue(Columns.TargetID, value); }
		}
		  
		[XmlAttribute("InsertDate")]
		[Bindable(true)]
		public DateTime? InsertDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.InsertDate); }
			set { SetColumnValue(Columns.InsertDate, value); }
		}
		  
		[XmlAttribute("InsertUser")]
		[Bindable(true)]
		public string InsertUser 
		{
			get { return GetColumnValue<string>(Columns.InsertUser); }
			set { SetColumnValue(Columns.InsertUser, value); }
		}
		  
		[XmlAttribute("CompanyName")]
		[Bindable(true)]
		public string CompanyName 
		{
			get { return GetColumnValue<string>(Columns.CompanyName); }
			set { SetColumnValue(Columns.CompanyName, value); }
		}
		  
		[XmlAttribute("ContactName")]
		[Bindable(true)]
		public string ContactName 
		{
			get { return GetColumnValue<string>(Columns.ContactName); }
			set { SetColumnValue(Columns.ContactName, value); }
		}
		  
		[XmlAttribute("ContactEmail")]
		[Bindable(true)]
		public string ContactEmail 
		{
			get { return GetColumnValue<string>(Columns.ContactEmail); }
			set { SetColumnValue(Columns.ContactEmail, value); }
		}
		  
		[XmlAttribute("ContactPosition")]
		[Bindable(true)]
		public string ContactPosition 
		{
			get { return GetColumnValue<string>(Columns.ContactPosition); }
			set { SetColumnValue(Columns.ContactPosition, value); }
		}
		  
		[XmlAttribute("CompanyAddress")]
		[Bindable(true)]
		public string CompanyAddress 
		{
			get { return GetColumnValue<string>(Columns.CompanyAddress); }
			set { SetColumnValue(Columns.CompanyAddress, value); }
		}
		  
		[XmlAttribute("TelNo")]
		[Bindable(true)]
		public string TelNo 
		{
			get { return GetColumnValue<string>(Columns.TelNo); }
			set { SetColumnValue(Columns.TelNo, value); }
		}
		  
		[XmlAttribute("PrintersInfo")]
		[Bindable(true)]
		public string PrintersInfo 
		{
			get { return GetColumnValue<string>(Columns.PrintersInfo); }
			set { SetColumnValue(Columns.PrintersInfo, value); }
		}
		  
		[XmlAttribute("LastContacted")]
		[Bindable(true)]
		public string LastContacted 
		{
			get { return GetColumnValue<string>(Columns.LastContacted); }
			set { SetColumnValue(Columns.LastContacted, value); }
		}
		  
		[XmlAttribute("ShippingProfile")]
		[Bindable(true)]
		public string ShippingProfile 
		{
			get { return GetColumnValue<string>(Columns.ShippingProfile); }
			set { SetColumnValue(Columns.ShippingProfile, value); }
		}
		  
		[XmlAttribute("Comments")]
		[Bindable(true)]
		public string Comments 
		{
			get { return GetColumnValue<string>(Columns.Comments); }
			set { SetColumnValue(Columns.Comments, value); }
		}
		  
		[XmlAttribute("SalesCode")]
		[Bindable(true)]
		public string SalesCode 
		{
			get { return GetColumnValue<string>(Columns.SalesCode); }
			set { SetColumnValue(Columns.SalesCode, value); }
		}
		  
		[XmlAttribute("PriorityCode")]
		[Bindable(true)]
		public string PriorityCode 
		{
			get { return GetColumnValue<string>(Columns.PriorityCode); }
			set { SetColumnValue(Columns.PriorityCode, value); }
		}
		  
		[XmlAttribute("UpdateDate")]
		[Bindable(true)]
		public DateTime? UpdateDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.UpdateDate); }
			set { SetColumnValue(Columns.UpdateDate, value); }
		}
		  
		[XmlAttribute("UpdateUser")]
		[Bindable(true)]
		public string UpdateUser 
		{
			get { return GetColumnValue<string>(Columns.UpdateUser); }
			set { SetColumnValue(Columns.UpdateUser, value); }
		}
		  
		[XmlAttribute("Dbtimestamp")]
		[Bindable(true)]
		public byte[] Dbtimestamp 
		{
			get { return GetColumnValue<byte[]>(Columns.Dbtimestamp); }
			set { SetColumnValue(Columns.Dbtimestamp, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(DateTime? varInsertDate,string varInsertUser,string varCompanyName,string varContactName,string varContactEmail,string varContactPosition,string varCompanyAddress,string varTelNo,string varPrintersInfo,string varLastContacted,string varShippingProfile,string varComments,string varSalesCode,string varPriorityCode,DateTime? varUpdateDate,string varUpdateUser,byte[] varDbtimestamp)
		{
			CustomerTargetList item = new CustomerTargetList();
			
			item.InsertDate = varInsertDate;
			
			item.InsertUser = varInsertUser;
			
			item.CompanyName = varCompanyName;
			
			item.ContactName = varContactName;
			
			item.ContactEmail = varContactEmail;
			
			item.ContactPosition = varContactPosition;
			
			item.CompanyAddress = varCompanyAddress;
			
			item.TelNo = varTelNo;
			
			item.PrintersInfo = varPrintersInfo;
			
			item.LastContacted = varLastContacted;
			
			item.ShippingProfile = varShippingProfile;
			
			item.Comments = varComments;
			
			item.SalesCode = varSalesCode;
			
			item.PriorityCode = varPriorityCode;
			
			item.UpdateDate = varUpdateDate;
			
			item.UpdateUser = varUpdateUser;
			
			item.Dbtimestamp = varDbtimestamp;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varTargetID,DateTime? varInsertDate,string varInsertUser,string varCompanyName,string varContactName,string varContactEmail,string varContactPosition,string varCompanyAddress,string varTelNo,string varPrintersInfo,string varLastContacted,string varShippingProfile,string varComments,string varSalesCode,string varPriorityCode,DateTime? varUpdateDate,string varUpdateUser,byte[] varDbtimestamp)
		{
			CustomerTargetList item = new CustomerTargetList();
			
				item.TargetID = varTargetID;
			
				item.InsertDate = varInsertDate;
			
				item.InsertUser = varInsertUser;
			
				item.CompanyName = varCompanyName;
			
				item.ContactName = varContactName;
			
				item.ContactEmail = varContactEmail;
			
				item.ContactPosition = varContactPosition;
			
				item.CompanyAddress = varCompanyAddress;
			
				item.TelNo = varTelNo;
			
				item.PrintersInfo = varPrintersInfo;
			
				item.LastContacted = varLastContacted;
			
				item.ShippingProfile = varShippingProfile;
			
				item.Comments = varComments;
			
				item.SalesCode = varSalesCode;
			
				item.PriorityCode = varPriorityCode;
			
				item.UpdateDate = varUpdateDate;
			
				item.UpdateUser = varUpdateUser;
			
				item.Dbtimestamp = varDbtimestamp;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn TargetIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn InsertDateColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn InsertUserColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn CompanyNameColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn ContactNameColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn ContactEmailColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn ContactPositionColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn CompanyAddressColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn TelNoColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn PrintersInfoColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn LastContactedColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn ShippingProfileColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn CommentsColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn SalesCodeColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn PriorityCodeColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateDateColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateUserColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn DbtimestampColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string TargetID = @"targetID";
			 public static string InsertDate = @"insert_date";
			 public static string InsertUser = @"insert_user";
			 public static string CompanyName = @"company_name";
			 public static string ContactName = @"contact_name";
			 public static string ContactEmail = @"contact_email";
			 public static string ContactPosition = @"contact_position";
			 public static string CompanyAddress = @"company_address";
			 public static string TelNo = @"tel_no";
			 public static string PrintersInfo = @"printers_info";
			 public static string LastContacted = @"last_contacted";
			 public static string ShippingProfile = @"shipping_profile";
			 public static string Comments = @"comments";
			 public static string SalesCode = @"sales_code";
			 public static string PriorityCode = @"priority_code";
			 public static string UpdateDate = @"update_date";
			 public static string UpdateUser = @"update_user";
			 public static string Dbtimestamp = @"dbtimestamp";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
