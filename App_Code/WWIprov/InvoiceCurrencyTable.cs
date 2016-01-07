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
	/// Strongly-typed collection for the InvoiceCurrencyTable class.
	/// </summary>
    [Serializable]
	public partial class InvoiceCurrencyTableCollection : ActiveList<InvoiceCurrencyTable, InvoiceCurrencyTableCollection>
	{	   
		public InvoiceCurrencyTableCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>InvoiceCurrencyTableCollection</returns>
		public InvoiceCurrencyTableCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                InvoiceCurrencyTable o = this[i];
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
	/// This is an ActiveRecord class which wraps the InvoiceCurrencyTable table.
	/// </summary>
	[Serializable]
	public partial class InvoiceCurrencyTable : ActiveRecord<InvoiceCurrencyTable>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public InvoiceCurrencyTable()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public InvoiceCurrencyTable(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public InvoiceCurrencyTable(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public InvoiceCurrencyTable(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("InvoiceCurrencyTable", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarCurrencyID = new TableSchema.TableColumn(schema);
				colvarCurrencyID.ColumnName = "CurrencyID";
				colvarCurrencyID.DataType = DbType.Int32;
				colvarCurrencyID.MaxLength = 0;
				colvarCurrencyID.AutoIncrement = false;
				colvarCurrencyID.IsNullable = false;
				colvarCurrencyID.IsPrimaryKey = true;
				colvarCurrencyID.IsForeignKey = false;
				colvarCurrencyID.IsReadOnly = false;
				colvarCurrencyID.DefaultSetting = @"";
				colvarCurrencyID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrencyID);
				
				TableSchema.TableColumn colvarInvoiceCurrencyName = new TableSchema.TableColumn(schema);
				colvarInvoiceCurrencyName.ColumnName = "InvoiceCurrencyName";
				colvarInvoiceCurrencyName.DataType = DbType.String;
				colvarInvoiceCurrencyName.MaxLength = 50;
				colvarInvoiceCurrencyName.AutoIncrement = false;
				colvarInvoiceCurrencyName.IsNullable = true;
				colvarInvoiceCurrencyName.IsPrimaryKey = false;
				colvarInvoiceCurrencyName.IsForeignKey = false;
				colvarInvoiceCurrencyName.IsReadOnly = false;
				colvarInvoiceCurrencyName.DefaultSetting = @"";
				colvarInvoiceCurrencyName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarInvoiceCurrencyName);
				
				TableSchema.TableColumn colvarExchangeRate = new TableSchema.TableColumn(schema);
				colvarExchangeRate.ColumnName = "ExchangeRate";
				colvarExchangeRate.DataType = DbType.Single;
				colvarExchangeRate.MaxLength = 0;
				colvarExchangeRate.AutoIncrement = false;
				colvarExchangeRate.IsNullable = true;
				colvarExchangeRate.IsPrimaryKey = false;
				colvarExchangeRate.IsForeignKey = false;
				colvarExchangeRate.IsReadOnly = false;
				colvarExchangeRate.DefaultSetting = @"";
				colvarExchangeRate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarExchangeRate);
				
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
				DataService.Providers["WWIprov"].AddSchema("InvoiceCurrencyTable",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("CurrencyID")]
		[Bindable(true)]
		public int CurrencyID 
		{
			get { return GetColumnValue<int>(Columns.CurrencyID); }
			set { SetColumnValue(Columns.CurrencyID, value); }
		}
		  
		[XmlAttribute("InvoiceCurrencyName")]
		[Bindable(true)]
		public string InvoiceCurrencyName 
		{
			get { return GetColumnValue<string>(Columns.InvoiceCurrencyName); }
			set { SetColumnValue(Columns.InvoiceCurrencyName, value); }
		}
		  
		[XmlAttribute("ExchangeRate")]
		[Bindable(true)]
		public float? ExchangeRate 
		{
			get { return GetColumnValue<float?>(Columns.ExchangeRate); }
			set { SetColumnValue(Columns.ExchangeRate, value); }
		}
		  
		[XmlAttribute("Ts")]
		[Bindable(true)]
		public byte[] Ts 
		{
			get { return GetColumnValue<byte[]>(Columns.Ts); }
			set { SetColumnValue(Columns.Ts, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		public DAL.Logistics.InvoiceTableAuditCollection InvoiceTableAuditRecords()
		{
			return new DAL.Logistics.InvoiceTableAuditCollection().Where(InvoiceTableAudit.Columns.InvoiceCurrencyID, CurrencyID).Load();
		}
		#endregion
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int varCurrencyID,string varInvoiceCurrencyName,float? varExchangeRate,byte[] varTs)
		{
			InvoiceCurrencyTable item = new InvoiceCurrencyTable();
			
			item.CurrencyID = varCurrencyID;
			
			item.InvoiceCurrencyName = varInvoiceCurrencyName;
			
			item.ExchangeRate = varExchangeRate;
			
			item.Ts = varTs;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varCurrencyID,string varInvoiceCurrencyName,float? varExchangeRate,byte[] varTs)
		{
			InvoiceCurrencyTable item = new InvoiceCurrencyTable();
			
				item.CurrencyID = varCurrencyID;
			
				item.InvoiceCurrencyName = varInvoiceCurrencyName;
			
				item.ExchangeRate = varExchangeRate;
			
				item.Ts = varTs;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn CurrencyIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn InvoiceCurrencyNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ExchangeRateColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn TsColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string CurrencyID = @"CurrencyID";
			 public static string InvoiceCurrencyName = @"InvoiceCurrencyName";
			 public static string ExchangeRate = @"ExchangeRate";
			 public static string Ts = @"TS";
						
		}
		#endregion
		
		#region Update PK Collections
		
        public void SetPKValues()
        {
}
        #endregion
    
        #region Deep Save
		
        public void DeepSave()
        {
            Save();
            
}
        #endregion
	}
}