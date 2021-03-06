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
namespace DAL.Logistics{
    /// <summary>
    /// Strongly-typed collection for the PurchaseValueView class.
    /// </summary>
    [Serializable]
    public partial class PurchaseValueViewCollection : ReadOnlyList<PurchaseValueView, PurchaseValueViewCollection>
    {        
        public PurchaseValueViewCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the PurchaseValueView view.
    /// </summary>
    [Serializable]
    public partial class PurchaseValueView : ReadOnlyRecord<PurchaseValueView>, IReadOnlyRecord
    {
    
	    #region Default Settings
	    protected static void SetSQLProps() 
	    {
		    GetTableSchema();
	    }
	    #endregion
        #region Schema Accessor
	    public static TableSchema.Table Schema
        {
            get
            {
                if (BaseSchema == null)
                {
                    SetSQLProps();
                }
                return BaseSchema;
            }
        }
    	
        private static void GetTableSchema() 
        {
            if(!IsSchemaInitialized)
            {
                //Schema declaration
                TableSchema.Table schema = new TableSchema.Table("PurchaseValueView", TableType.View, DataService.GetInstance("WWIprov"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarInvoiceNumber = new TableSchema.TableColumn(schema);
                colvarInvoiceNumber.ColumnName = "InvoiceNumber";
                colvarInvoiceNumber.DataType = DbType.Int32;
                colvarInvoiceNumber.MaxLength = 0;
                colvarInvoiceNumber.AutoIncrement = false;
                colvarInvoiceNumber.IsNullable = false;
                colvarInvoiceNumber.IsPrimaryKey = false;
                colvarInvoiceNumber.IsForeignKey = false;
                colvarInvoiceNumber.IsReadOnly = false;
                
                schema.Columns.Add(colvarInvoiceNumber);
                
                TableSchema.TableColumn colvarValueForProfit = new TableSchema.TableColumn(schema);
                colvarValueForProfit.ColumnName = "ValueForProfit";
                colvarValueForProfit.DataType = DbType.Currency;
                colvarValueForProfit.MaxLength = 0;
                colvarValueForProfit.AutoIncrement = false;
                colvarValueForProfit.IsNullable = true;
                colvarValueForProfit.IsPrimaryKey = false;
                colvarValueForProfit.IsForeignKey = false;
                colvarValueForProfit.IsReadOnly = false;
                
                schema.Columns.Add(colvarValueForProfit);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("PurchaseValueView",schema);
            }
        }
        #endregion
        
        #region Query Accessor
	    public static Query CreateQuery()
	    {
		    return new Query(Schema);
	    }
	    #endregion
	    
	    #region .ctors
	    public PurchaseValueView()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public PurchaseValueView(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public PurchaseValueView(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public PurchaseValueView(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("InvoiceNumber")]
        [Bindable(true)]
        public int InvoiceNumber 
	    {
		    get
		    {
			    return GetColumnValue<int>("InvoiceNumber");
		    }
            set 
		    {
			    SetColumnValue("InvoiceNumber", value);
            }
        }
	      
        [XmlAttribute("ValueForProfit")]
        [Bindable(true)]
        public decimal? ValueForProfit 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("ValueForProfit");
		    }
            set 
		    {
			    SetColumnValue("ValueForProfit", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string InvoiceNumber = @"InvoiceNumber";
            
            public static string ValueForProfit = @"ValueForProfit";
            
	    }
	    #endregion
	    
	    
	    #region IAbstractRecord Members
        public new CT GetColumnValue<CT>(string columnName) {
            return base.GetColumnValue<CT>(columnName);
        }
        public object GetColumnValue(string columnName) {
            return base.GetColumnValue<object>(columnName);
        }
        #endregion
	    
    }
}
