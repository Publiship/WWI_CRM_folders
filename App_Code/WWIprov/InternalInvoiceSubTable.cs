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
	/// Strongly-typed collection for the InternalInvoiceSubTable class.
	/// </summary>
    [Serializable]
	public partial class InternalInvoiceSubTableCollection : ActiveList<InternalInvoiceSubTable, InternalInvoiceSubTableCollection>
	{	   
		public InternalInvoiceSubTableCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>InternalInvoiceSubTableCollection</returns>
		public InternalInvoiceSubTableCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                InternalInvoiceSubTable o = this[i];
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
	/// This is an ActiveRecord class which wraps the InternalInvoiceSubTable table.
	/// </summary>
	[Serializable]
	public partial class InternalInvoiceSubTable : ActiveRecord<InternalInvoiceSubTable>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public InternalInvoiceSubTable()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public InternalInvoiceSubTable(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public InternalInvoiceSubTable(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public InternalInvoiceSubTable(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("InternalInvoiceSubTable", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarEntryNumber = new TableSchema.TableColumn(schema);
				colvarEntryNumber.ColumnName = "EntryNumber";
				colvarEntryNumber.DataType = DbType.Int32;
				colvarEntryNumber.MaxLength = 0;
				colvarEntryNumber.AutoIncrement = true;
				colvarEntryNumber.IsNullable = false;
				colvarEntryNumber.IsPrimaryKey = true;
				colvarEntryNumber.IsForeignKey = false;
				colvarEntryNumber.IsReadOnly = false;
				colvarEntryNumber.DefaultSetting = @"";
				colvarEntryNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEntryNumber);
				
				TableSchema.TableColumn colvarInvoiceNumber = new TableSchema.TableColumn(schema);
				colvarInvoiceNumber.ColumnName = "InvoiceNumber";
				colvarInvoiceNumber.DataType = DbType.Int32;
				colvarInvoiceNumber.MaxLength = 0;
				colvarInvoiceNumber.AutoIncrement = false;
				colvarInvoiceNumber.IsNullable = true;
				colvarInvoiceNumber.IsPrimaryKey = false;
				colvarInvoiceNumber.IsForeignKey = true;
				colvarInvoiceNumber.IsReadOnly = false;
				colvarInvoiceNumber.DefaultSetting = @"";
				
					colvarInvoiceNumber.ForeignKeyTableName = "InternalInvoiceTable";
				schema.Columns.Add(colvarInvoiceNumber);
				
				TableSchema.TableColumn colvarChargeType = new TableSchema.TableColumn(schema);
				colvarChargeType.ColumnName = "ChargeType";
				colvarChargeType.DataType = DbType.Int32;
				colvarChargeType.MaxLength = 0;
				colvarChargeType.AutoIncrement = false;
				colvarChargeType.IsNullable = true;
				colvarChargeType.IsPrimaryKey = false;
				colvarChargeType.IsForeignKey = true;
				colvarChargeType.IsReadOnly = false;
				colvarChargeType.DefaultSetting = @"";
				
					colvarChargeType.ForeignKeyTableName = "ChargeTypeTable";
				schema.Columns.Add(colvarChargeType);
				
				TableSchema.TableColumn colvarDetail = new TableSchema.TableColumn(schema);
				colvarDetail.ColumnName = "Detail";
				colvarDetail.DataType = DbType.String;
				colvarDetail.MaxLength = 50;
				colvarDetail.AutoIncrement = false;
				colvarDetail.IsNullable = true;
				colvarDetail.IsPrimaryKey = false;
				colvarDetail.IsForeignKey = false;
				colvarDetail.IsReadOnly = false;
				colvarDetail.DefaultSetting = @"";
				colvarDetail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDetail);
				
				TableSchema.TableColumn colvarAmount = new TableSchema.TableColumn(schema);
				colvarAmount.ColumnName = "Amount";
				colvarAmount.DataType = DbType.Currency;
				colvarAmount.MaxLength = 0;
				colvarAmount.AutoIncrement = false;
				colvarAmount.IsNullable = true;
				colvarAmount.IsPrimaryKey = false;
				colvarAmount.IsForeignKey = false;
				colvarAmount.IsReadOnly = false;
				colvarAmount.DefaultSetting = @"";
				colvarAmount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAmount);
				
				TableSchema.TableColumn colvarVat = new TableSchema.TableColumn(schema);
				colvarVat.ColumnName = "VAT";
				colvarVat.DataType = DbType.Boolean;
				colvarVat.MaxLength = 0;
				colvarVat.AutoIncrement = false;
				colvarVat.IsNullable = false;
				colvarVat.IsPrimaryKey = false;
				colvarVat.IsForeignKey = false;
				colvarVat.IsReadOnly = false;
				
						colvarVat.DefaultSetting = @"(0)";
				colvarVat.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVat);
				
				TableSchema.TableColumn colvarVATAmount = new TableSchema.TableColumn(schema);
				colvarVATAmount.ColumnName = "VATAmount";
				colvarVATAmount.DataType = DbType.Currency;
				colvarVATAmount.MaxLength = 0;
				colvarVATAmount.AutoIncrement = false;
				colvarVATAmount.IsNullable = true;
				colvarVATAmount.IsPrimaryKey = false;
				colvarVATAmount.IsForeignKey = false;
				colvarVATAmount.IsReadOnly = false;
				colvarVATAmount.DefaultSetting = @"";
				colvarVATAmount.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVATAmount);
				
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
				DataService.Providers["WWIprov"].AddSchema("InternalInvoiceSubTable",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("EntryNumber")]
		[Bindable(true)]
		public int EntryNumber 
		{
			get { return GetColumnValue<int>(Columns.EntryNumber); }
			set { SetColumnValue(Columns.EntryNumber, value); }
		}
		  
		[XmlAttribute("InvoiceNumber")]
		[Bindable(true)]
		public int? InvoiceNumber 
		{
			get { return GetColumnValue<int?>(Columns.InvoiceNumber); }
			set { SetColumnValue(Columns.InvoiceNumber, value); }
		}
		  
		[XmlAttribute("ChargeType")]
		[Bindable(true)]
		public int? ChargeType 
		{
			get { return GetColumnValue<int?>(Columns.ChargeType); }
			set { SetColumnValue(Columns.ChargeType, value); }
		}
		  
		[XmlAttribute("Detail")]
		[Bindable(true)]
		public string Detail 
		{
			get { return GetColumnValue<string>(Columns.Detail); }
			set { SetColumnValue(Columns.Detail, value); }
		}
		  
		[XmlAttribute("Amount")]
		[Bindable(true)]
		public decimal? Amount 
		{
			get { return GetColumnValue<decimal?>(Columns.Amount); }
			set { SetColumnValue(Columns.Amount, value); }
		}
		  
		[XmlAttribute("Vat")]
		[Bindable(true)]
		public bool Vat 
		{
			get { return GetColumnValue<bool>(Columns.Vat); }
			set { SetColumnValue(Columns.Vat, value); }
		}
		  
		[XmlAttribute("VATAmount")]
		[Bindable(true)]
		public decimal? VATAmount 
		{
			get { return GetColumnValue<decimal?>(Columns.VATAmount); }
			set { SetColumnValue(Columns.VATAmount, value); }
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
		/// Returns a ChargeTypeTable ActiveRecord object related to this InternalInvoiceSubTable
		/// 
		/// </summary>
		public DAL.Logistics.ChargeTypeTable ChargeTypeTable
		{
			get { return DAL.Logistics.ChargeTypeTable.FetchByID(this.ChargeType); }
			set { SetColumnValue("ChargeType", value.ChargeID); }
		}
		
		
		/// <summary>
		/// Returns a InternalInvoiceTable ActiveRecord object related to this InternalInvoiceSubTable
		/// 
		/// </summary>
		public DAL.Logistics.InternalInvoiceTable InternalInvoiceTable
		{
			get { return DAL.Logistics.InternalInvoiceTable.FetchByID(this.InvoiceNumber); }
			set { SetColumnValue("InvoiceNumber", value.InvoiceNumber); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int? varInvoiceNumber,int? varChargeType,string varDetail,decimal? varAmount,bool varVat,decimal? varVATAmount,byte[] varTs)
		{
			InternalInvoiceSubTable item = new InternalInvoiceSubTable();
			
			item.InvoiceNumber = varInvoiceNumber;
			
			item.ChargeType = varChargeType;
			
			item.Detail = varDetail;
			
			item.Amount = varAmount;
			
			item.Vat = varVat;
			
			item.VATAmount = varVATAmount;
			
			item.Ts = varTs;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varEntryNumber,int? varInvoiceNumber,int? varChargeType,string varDetail,decimal? varAmount,bool varVat,decimal? varVATAmount,byte[] varTs)
		{
			InternalInvoiceSubTable item = new InternalInvoiceSubTable();
			
				item.EntryNumber = varEntryNumber;
			
				item.InvoiceNumber = varInvoiceNumber;
			
				item.ChargeType = varChargeType;
			
				item.Detail = varDetail;
			
				item.Amount = varAmount;
			
				item.Vat = varVat;
			
				item.VATAmount = varVATAmount;
			
				item.Ts = varTs;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn EntryNumberColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn InvoiceNumberColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ChargeTypeColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn DetailColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn AmountColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn VatColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn VATAmountColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn TsColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string EntryNumber = @"EntryNumber";
			 public static string InvoiceNumber = @"InvoiceNumber";
			 public static string ChargeType = @"ChargeType";
			 public static string Detail = @"Detail";
			 public static string Amount = @"Amount";
			 public static string Vat = @"VAT";
			 public static string VATAmount = @"VATAmount";
			 public static string Ts = @"TS";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}