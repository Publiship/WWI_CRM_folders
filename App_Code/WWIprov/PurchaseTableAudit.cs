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
	/// Strongly-typed collection for the PurchaseTableAudit class.
	/// </summary>
    [Serializable]
	public partial class PurchaseTableAuditCollection : ActiveList<PurchaseTableAudit, PurchaseTableAuditCollection>
	{	   
		public PurchaseTableAuditCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>PurchaseTableAuditCollection</returns>
		public PurchaseTableAuditCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                PurchaseTableAudit o = this[i];
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
	/// This is an ActiveRecord class which wraps the PurchaseTableAudit table.
	/// </summary>
	[Serializable]
	public partial class PurchaseTableAudit : ActiveRecord<PurchaseTableAudit>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public PurchaseTableAudit()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public PurchaseTableAudit(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public PurchaseTableAudit(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public PurchaseTableAudit(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("PurchaseTableAudit", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarPurchaseAuditIDa = new TableSchema.TableColumn(schema);
				colvarPurchaseAuditIDa.ColumnName = "PurchaseAuditIDa";
				colvarPurchaseAuditIDa.DataType = DbType.Int32;
				colvarPurchaseAuditIDa.MaxLength = 0;
				colvarPurchaseAuditIDa.AutoIncrement = true;
				colvarPurchaseAuditIDa.IsNullable = false;
				colvarPurchaseAuditIDa.IsPrimaryKey = true;
				colvarPurchaseAuditIDa.IsForeignKey = false;
				colvarPurchaseAuditIDa.IsReadOnly = false;
				colvarPurchaseAuditIDa.DefaultSetting = @"";
				colvarPurchaseAuditIDa.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaseAuditIDa);
				
				TableSchema.TableColumn colvarPuchaseTableID = new TableSchema.TableColumn(schema);
				colvarPuchaseTableID.ColumnName = "PuchaseTableID";
				colvarPuchaseTableID.DataType = DbType.Int32;
				colvarPuchaseTableID.MaxLength = 0;
				colvarPuchaseTableID.AutoIncrement = false;
				colvarPuchaseTableID.IsNullable = true;
				colvarPuchaseTableID.IsPrimaryKey = false;
				colvarPuchaseTableID.IsForeignKey = true;
				colvarPuchaseTableID.IsReadOnly = false;
				colvarPuchaseTableID.DefaultSetting = @"";
				
					colvarPuchaseTableID.ForeignKeyTableName = "PurchaseTablex";
				schema.Columns.Add(colvarPuchaseTableID);
				
				TableSchema.TableColumn colvarInvoiceNumber = new TableSchema.TableColumn(schema);
				colvarInvoiceNumber.ColumnName = "InvoiceNumber";
				colvarInvoiceNumber.DataType = DbType.Int32;
				colvarInvoiceNumber.MaxLength = 0;
				colvarInvoiceNumber.AutoIncrement = false;
				colvarInvoiceNumber.IsNullable = true;
				colvarInvoiceNumber.IsPrimaryKey = false;
				colvarInvoiceNumber.IsForeignKey = false;
				colvarInvoiceNumber.IsReadOnly = false;
				colvarInvoiceNumber.DefaultSetting = @"";
				colvarInvoiceNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInvoiceNumber);
				
				TableSchema.TableColumn colvarSupplierID = new TableSchema.TableColumn(schema);
				colvarSupplierID.ColumnName = "SupplierID";
				colvarSupplierID.DataType = DbType.Int32;
				colvarSupplierID.MaxLength = 0;
				colvarSupplierID.AutoIncrement = false;
				colvarSupplierID.IsNullable = true;
				colvarSupplierID.IsPrimaryKey = false;
				colvarSupplierID.IsForeignKey = false;
				colvarSupplierID.IsReadOnly = false;
				colvarSupplierID.DefaultSetting = @"";
				colvarSupplierID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSupplierID);
				
				TableSchema.TableColumn colvarEstimatedAmount = new TableSchema.TableColumn(schema);
				colvarEstimatedAmount.ColumnName = "EstimatedAmount";
				colvarEstimatedAmount.DataType = DbType.Decimal;
				colvarEstimatedAmount.MaxLength = 0;
				colvarEstimatedAmount.AutoIncrement = false;
				colvarEstimatedAmount.IsNullable = true;
				colvarEstimatedAmount.IsPrimaryKey = false;
				colvarEstimatedAmount.IsForeignKey = false;
				colvarEstimatedAmount.IsReadOnly = false;
				colvarEstimatedAmount.DefaultSetting = @"";
				colvarEstimatedAmount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEstimatedAmount);
				
				TableSchema.TableColumn colvarEstimationDate = new TableSchema.TableColumn(schema);
				colvarEstimationDate.ColumnName = "EstimationDate";
				colvarEstimationDate.DataType = DbType.DateTime;
				colvarEstimationDate.MaxLength = 0;
				colvarEstimationDate.AutoIncrement = false;
				colvarEstimationDate.IsNullable = true;
				colvarEstimationDate.IsPrimaryKey = false;
				colvarEstimationDate.IsForeignKey = false;
				colvarEstimationDate.IsReadOnly = false;
				colvarEstimationDate.DefaultSetting = @"";
				colvarEstimationDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEstimationDate);
				
				TableSchema.TableColumn colvarOldValue = new TableSchema.TableColumn(schema);
				colvarOldValue.ColumnName = "OldValue";
				colvarOldValue.DataType = DbType.String;
				colvarOldValue.MaxLength = 50;
				colvarOldValue.AutoIncrement = false;
				colvarOldValue.IsNullable = true;
				colvarOldValue.IsPrimaryKey = false;
				colvarOldValue.IsForeignKey = false;
				colvarOldValue.IsReadOnly = false;
				colvarOldValue.DefaultSetting = @"";
				colvarOldValue.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOldValue);
				
				TableSchema.TableColumn colvarAmount = new TableSchema.TableColumn(schema);
				colvarAmount.ColumnName = "Amount";
				colvarAmount.DataType = DbType.Decimal;
				colvarAmount.MaxLength = 0;
				colvarAmount.AutoIncrement = false;
				colvarAmount.IsNullable = true;
				colvarAmount.IsPrimaryKey = false;
				colvarAmount.IsForeignKey = false;
				colvarAmount.IsReadOnly = false;
				colvarAmount.DefaultSetting = @"";
				colvarAmount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAmount);
				
				TableSchema.TableColumn colvarDatePassed = new TableSchema.TableColumn(schema);
				colvarDatePassed.ColumnName = "DatePassed";
				colvarDatePassed.DataType = DbType.DateTime;
				colvarDatePassed.MaxLength = 0;
				colvarDatePassed.AutoIncrement = false;
				colvarDatePassed.IsNullable = true;
				colvarDatePassed.IsPrimaryKey = false;
				colvarDatePassed.IsForeignKey = false;
				colvarDatePassed.IsReadOnly = false;
				colvarDatePassed.DefaultSetting = @"";
				colvarDatePassed.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDatePassed);
				
				TableSchema.TableColumn colvarPurchaseInvNumber = new TableSchema.TableColumn(schema);
				colvarPurchaseInvNumber.ColumnName = "PurchaseInvNumber";
				colvarPurchaseInvNumber.DataType = DbType.String;
				colvarPurchaseInvNumber.MaxLength = 50;
				colvarPurchaseInvNumber.AutoIncrement = false;
				colvarPurchaseInvNumber.IsNullable = true;
				colvarPurchaseInvNumber.IsPrimaryKey = false;
				colvarPurchaseInvNumber.IsForeignKey = false;
				colvarPurchaseInvNumber.IsReadOnly = false;
				colvarPurchaseInvNumber.DefaultSetting = @"";
				colvarPurchaseInvNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPurchaseInvNumber);
				
				TableSchema.TableColumn colvarRemarks = new TableSchema.TableColumn(schema);
				colvarRemarks.ColumnName = "Remarks";
				colvarRemarks.DataType = DbType.String;
				colvarRemarks.MaxLength = 50;
				colvarRemarks.AutoIncrement = false;
				colvarRemarks.IsNullable = true;
				colvarRemarks.IsPrimaryKey = false;
				colvarRemarks.IsForeignKey = false;
				colvarRemarks.IsReadOnly = false;
				colvarRemarks.DefaultSetting = @"";
				colvarRemarks.ForeignKeyTableName = "";
				schema.Columns.Add(colvarRemarks);
				
				TableSchema.TableColumn colvarValueForProfit = new TableSchema.TableColumn(schema);
				colvarValueForProfit.ColumnName = "ValueForProfit";
				colvarValueForProfit.DataType = DbType.Decimal;
				colvarValueForProfit.MaxLength = 0;
				colvarValueForProfit.AutoIncrement = false;
				colvarValueForProfit.IsNullable = true;
				colvarValueForProfit.IsPrimaryKey = false;
				colvarValueForProfit.IsForeignKey = false;
				colvarValueForProfit.IsReadOnly = false;
				colvarValueForProfit.DefaultSetting = @"";
				colvarValueForProfit.ForeignKeyTableName = "";
				schema.Columns.Add(colvarValueForProfit);
				
				TableSchema.TableColumn colvarAdded = new TableSchema.TableColumn(schema);
				colvarAdded.ColumnName = "Added";
				colvarAdded.DataType = DbType.DateTime;
				colvarAdded.MaxLength = 0;
				colvarAdded.AutoIncrement = false;
				colvarAdded.IsNullable = true;
				colvarAdded.IsPrimaryKey = false;
				colvarAdded.IsForeignKey = false;
				colvarAdded.IsReadOnly = false;
				colvarAdded.DefaultSetting = @"";
				colvarAdded.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAdded);
				
				TableSchema.TableColumn colvarAddedBy = new TableSchema.TableColumn(schema);
				colvarAddedBy.ColumnName = "AddedBy";
				colvarAddedBy.DataType = DbType.Int32;
				colvarAddedBy.MaxLength = 0;
				colvarAddedBy.AutoIncrement = false;
				colvarAddedBy.IsNullable = true;
				colvarAddedBy.IsPrimaryKey = false;
				colvarAddedBy.IsForeignKey = false;
				colvarAddedBy.IsReadOnly = false;
				colvarAddedBy.DefaultSetting = @"";
				colvarAddedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddedBy);
				
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
				DataService.Providers["WWIprov"].AddSchema("PurchaseTableAudit",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("PurchaseAuditIDa")]
		[Bindable(true)]
		public int PurchaseAuditIDa 
		{
			get { return GetColumnValue<int>(Columns.PurchaseAuditIDa); }
			set { SetColumnValue(Columns.PurchaseAuditIDa, value); }
		}
		  
		[XmlAttribute("PuchaseTableID")]
		[Bindable(true)]
		public int? PuchaseTableID 
		{
			get { return GetColumnValue<int?>(Columns.PuchaseTableID); }
			set { SetColumnValue(Columns.PuchaseTableID, value); }
		}
		  
		[XmlAttribute("InvoiceNumber")]
		[Bindable(true)]
		public int? InvoiceNumber 
		{
			get { return GetColumnValue<int?>(Columns.InvoiceNumber); }
			set { SetColumnValue(Columns.InvoiceNumber, value); }
		}
		  
		[XmlAttribute("SupplierID")]
		[Bindable(true)]
		public int? SupplierID 
		{
			get { return GetColumnValue<int?>(Columns.SupplierID); }
			set { SetColumnValue(Columns.SupplierID, value); }
		}
		  
		[XmlAttribute("EstimatedAmount")]
		[Bindable(true)]
		public decimal? EstimatedAmount 
		{
			get { return GetColumnValue<decimal?>(Columns.EstimatedAmount); }
			set { SetColumnValue(Columns.EstimatedAmount, value); }
		}
		  
		[XmlAttribute("EstimationDate")]
		[Bindable(true)]
		public DateTime? EstimationDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.EstimationDate); }
			set { SetColumnValue(Columns.EstimationDate, value); }
		}
		  
		[XmlAttribute("OldValue")]
		[Bindable(true)]
		public string OldValue 
		{
			get { return GetColumnValue<string>(Columns.OldValue); }
			set { SetColumnValue(Columns.OldValue, value); }
		}
		  
		[XmlAttribute("Amount")]
		[Bindable(true)]
		public decimal? Amount 
		{
			get { return GetColumnValue<decimal?>(Columns.Amount); }
			set { SetColumnValue(Columns.Amount, value); }
		}
		  
		[XmlAttribute("DatePassed")]
		[Bindable(true)]
		public DateTime? DatePassed 
		{
			get { return GetColumnValue<DateTime?>(Columns.DatePassed); }
			set { SetColumnValue(Columns.DatePassed, value); }
		}
		  
		[XmlAttribute("PurchaseInvNumber")]
		[Bindable(true)]
		public string PurchaseInvNumber 
		{
			get { return GetColumnValue<string>(Columns.PurchaseInvNumber); }
			set { SetColumnValue(Columns.PurchaseInvNumber, value); }
		}
		  
		[XmlAttribute("Remarks")]
		[Bindable(true)]
		public string Remarks 
		{
			get { return GetColumnValue<string>(Columns.Remarks); }
			set { SetColumnValue(Columns.Remarks, value); }
		}
		  
		[XmlAttribute("ValueForProfit")]
		[Bindable(true)]
		public decimal? ValueForProfit 
		{
			get { return GetColumnValue<decimal?>(Columns.ValueForProfit); }
			set { SetColumnValue(Columns.ValueForProfit, value); }
		}
		  
		[XmlAttribute("Added")]
		[Bindable(true)]
		public DateTime? Added 
		{
			get { return GetColumnValue<DateTime?>(Columns.Added); }
			set { SetColumnValue(Columns.Added, value); }
		}
		  
		[XmlAttribute("AddedBy")]
		[Bindable(true)]
		public int? AddedBy 
		{
			get { return GetColumnValue<int?>(Columns.AddedBy); }
			set { SetColumnValue(Columns.AddedBy, value); }
		}
		  
		[XmlAttribute("Ts")]
		[Bindable(true)]
		public byte[] Ts 
		{
			get { return GetColumnValue<byte[]>(Columns.Ts); }
			set { SetColumnValue(Columns.Ts, value); }
		}
		
		#endregion
		
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a PurchaseTablex ActiveRecord object related to this PurchaseTableAudit
		/// 
		/// </summary>
		public DAL.Logistics.PurchaseTablex PurchaseTablex
		{
			get { return DAL.Logistics.PurchaseTablex.FetchByID(this.PuchaseTableID); }
			set { SetColumnValue("PuchaseTableID", value.Id); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int? varPuchaseTableID,int? varInvoiceNumber,int? varSupplierID,decimal? varEstimatedAmount,DateTime? varEstimationDate,string varOldValue,decimal? varAmount,DateTime? varDatePassed,string varPurchaseInvNumber,string varRemarks,decimal? varValueForProfit,DateTime? varAdded,int? varAddedBy,byte[] varTs)
		{
			PurchaseTableAudit item = new PurchaseTableAudit();
			
			item.PuchaseTableID = varPuchaseTableID;
			
			item.InvoiceNumber = varInvoiceNumber;
			
			item.SupplierID = varSupplierID;
			
			item.EstimatedAmount = varEstimatedAmount;
			
			item.EstimationDate = varEstimationDate;
			
			item.OldValue = varOldValue;
			
			item.Amount = varAmount;
			
			item.DatePassed = varDatePassed;
			
			item.PurchaseInvNumber = varPurchaseInvNumber;
			
			item.Remarks = varRemarks;
			
			item.ValueForProfit = varValueForProfit;
			
			item.Added = varAdded;
			
			item.AddedBy = varAddedBy;
			
			item.Ts = varTs;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varPurchaseAuditIDa,int? varPuchaseTableID,int? varInvoiceNumber,int? varSupplierID,decimal? varEstimatedAmount,DateTime? varEstimationDate,string varOldValue,decimal? varAmount,DateTime? varDatePassed,string varPurchaseInvNumber,string varRemarks,decimal? varValueForProfit,DateTime? varAdded,int? varAddedBy,byte[] varTs)
		{
			PurchaseTableAudit item = new PurchaseTableAudit();
			
				item.PurchaseAuditIDa = varPurchaseAuditIDa;
			
				item.PuchaseTableID = varPuchaseTableID;
			
				item.InvoiceNumber = varInvoiceNumber;
			
				item.SupplierID = varSupplierID;
			
				item.EstimatedAmount = varEstimatedAmount;
			
				item.EstimationDate = varEstimationDate;
			
				item.OldValue = varOldValue;
			
				item.Amount = varAmount;
			
				item.DatePassed = varDatePassed;
			
				item.PurchaseInvNumber = varPurchaseInvNumber;
			
				item.Remarks = varRemarks;
			
				item.ValueForProfit = varValueForProfit;
			
				item.Added = varAdded;
			
				item.AddedBy = varAddedBy;
			
				item.Ts = varTs;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn PurchaseAuditIDaColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn PuchaseTableIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn InvoiceNumberColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SupplierIDColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn EstimatedAmountColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn EstimationDateColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn OldValueColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn AmountColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn DatePassedColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn PurchaseInvNumberColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn RemarksColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn ValueForProfitColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn AddedColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn AddedByColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn TsColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string PurchaseAuditIDa = @"PurchaseAuditIDa";
			 public static string PuchaseTableID = @"PuchaseTableID";
			 public static string InvoiceNumber = @"InvoiceNumber";
			 public static string SupplierID = @"SupplierID";
			 public static string EstimatedAmount = @"EstimatedAmount";
			 public static string EstimationDate = @"EstimationDate";
			 public static string OldValue = @"OldValue";
			 public static string Amount = @"Amount";
			 public static string DatePassed = @"DatePassed";
			 public static string PurchaseInvNumber = @"PurchaseInvNumber";
			 public static string Remarks = @"Remarks";
			 public static string ValueForProfit = @"ValueForProfit";
			 public static string Added = @"Added";
			 public static string AddedBy = @"AddedBy";
			 public static string Ts = @"TS";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
