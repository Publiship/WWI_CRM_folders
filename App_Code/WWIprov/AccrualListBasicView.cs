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
    /// Strongly-typed collection for the AccrualListBasicView class.
    /// </summary>
    [Serializable]
    public partial class AccrualListBasicViewCollection : ReadOnlyList<AccrualListBasicView, AccrualListBasicViewCollection>
    {        
        public AccrualListBasicViewCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the AccrualListBasicView view.
    /// </summary>
    [Serializable]
    public partial class AccrualListBasicView : ReadOnlyRecord<AccrualListBasicView>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("AccrualListBasicView", TableType.View, DataService.GetInstance("WWIprov"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarOrderNumber = new TableSchema.TableColumn(schema);
                colvarOrderNumber.ColumnName = "OrderNumber";
                colvarOrderNumber.DataType = DbType.Int32;
                colvarOrderNumber.MaxLength = 0;
                colvarOrderNumber.AutoIncrement = false;
                colvarOrderNumber.IsNullable = true;
                colvarOrderNumber.IsPrimaryKey = false;
                colvarOrderNumber.IsForeignKey = false;
                colvarOrderNumber.IsReadOnly = false;
                
                schema.Columns.Add(colvarOrderNumber);
                
                TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
                colvarId.ColumnName = "ID";
                colvarId.DataType = DbType.Int32;
                colvarId.MaxLength = 0;
                colvarId.AutoIncrement = false;
                colvarId.IsNullable = false;
                colvarId.IsPrimaryKey = false;
                colvarId.IsForeignKey = false;
                colvarId.IsReadOnly = false;
                
                schema.Columns.Add(colvarId);
                
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
                
                TableSchema.TableColumn colvarInvoiceDate = new TableSchema.TableColumn(schema);
                colvarInvoiceDate.ColumnName = "InvoiceDate";
                colvarInvoiceDate.DataType = DbType.DateTime;
                colvarInvoiceDate.MaxLength = 0;
                colvarInvoiceDate.AutoIncrement = false;
                colvarInvoiceDate.IsNullable = true;
                colvarInvoiceDate.IsPrimaryKey = false;
                colvarInvoiceDate.IsForeignKey = false;
                colvarInvoiceDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarInvoiceDate);
                
                TableSchema.TableColumn colvarCompanyName = new TableSchema.TableColumn(schema);
                colvarCompanyName.ColumnName = "CompanyName";
                colvarCompanyName.DataType = DbType.String;
                colvarCompanyName.MaxLength = 50;
                colvarCompanyName.AutoIncrement = false;
                colvarCompanyName.IsNullable = true;
                colvarCompanyName.IsPrimaryKey = false;
                colvarCompanyName.IsForeignKey = false;
                colvarCompanyName.IsReadOnly = false;
                
                schema.Columns.Add(colvarCompanyName);
                
                TableSchema.TableColumn colvarEstimatedAmount = new TableSchema.TableColumn(schema);
                colvarEstimatedAmount.ColumnName = "EstimatedAmount";
                colvarEstimatedAmount.DataType = DbType.Currency;
                colvarEstimatedAmount.MaxLength = 0;
                colvarEstimatedAmount.AutoIncrement = false;
                colvarEstimatedAmount.IsNullable = true;
                colvarEstimatedAmount.IsPrimaryKey = false;
                colvarEstimatedAmount.IsForeignKey = false;
                colvarEstimatedAmount.IsReadOnly = false;
                
                schema.Columns.Add(colvarEstimatedAmount);
                
                TableSchema.TableColumn colvarRemarks = new TableSchema.TableColumn(schema);
                colvarRemarks.ColumnName = "Remarks";
                colvarRemarks.DataType = DbType.String;
                colvarRemarks.MaxLength = 100;
                colvarRemarks.AutoIncrement = false;
                colvarRemarks.IsNullable = true;
                colvarRemarks.IsPrimaryKey = false;
                colvarRemarks.IsForeignKey = false;
                colvarRemarks.IsReadOnly = false;
                
                schema.Columns.Add(colvarRemarks);
                
                TableSchema.TableColumn colvarDepartmentID = new TableSchema.TableColumn(schema);
                colvarDepartmentID.ColumnName = "DepartmentID";
                colvarDepartmentID.DataType = DbType.Int32;
                colvarDepartmentID.MaxLength = 0;
                colvarDepartmentID.AutoIncrement = false;
                colvarDepartmentID.IsNullable = true;
                colvarDepartmentID.IsPrimaryKey = false;
                colvarDepartmentID.IsForeignKey = false;
                colvarDepartmentID.IsReadOnly = false;
                
                schema.Columns.Add(colvarDepartmentID);
                
                TableSchema.TableColumn colvarDepartment = new TableSchema.TableColumn(schema);
                colvarDepartment.ColumnName = "Department";
                colvarDepartment.DataType = DbType.AnsiStringFixedLength;
                colvarDepartment.MaxLength = 10;
                colvarDepartment.AutoIncrement = false;
                colvarDepartment.IsNullable = true;
                colvarDepartment.IsPrimaryKey = false;
                colvarDepartment.IsForeignKey = false;
                colvarDepartment.IsReadOnly = false;
                
                schema.Columns.Add(colvarDepartment);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("AccrualListBasicView",schema);
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
	    public AccrualListBasicView()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public AccrualListBasicView(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public AccrualListBasicView(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public AccrualListBasicView(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("OrderNumber")]
        [Bindable(true)]
        public int? OrderNumber 
	    {
		    get
		    {
			    return GetColumnValue<int?>("OrderNumber");
		    }
            set 
		    {
			    SetColumnValue("OrderNumber", value);
            }
        }
	      
        [XmlAttribute("Id")]
        [Bindable(true)]
        public int Id 
	    {
		    get
		    {
			    return GetColumnValue<int>("ID");
		    }
            set 
		    {
			    SetColumnValue("ID", value);
            }
        }
	      
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
	      
        [XmlAttribute("InvoiceDate")]
        [Bindable(true)]
        public DateTime? InvoiceDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("InvoiceDate");
		    }
            set 
		    {
			    SetColumnValue("InvoiceDate", value);
            }
        }
	      
        [XmlAttribute("CompanyName")]
        [Bindable(true)]
        public string CompanyName 
	    {
		    get
		    {
			    return GetColumnValue<string>("CompanyName");
		    }
            set 
		    {
			    SetColumnValue("CompanyName", value);
            }
        }
	      
        [XmlAttribute("EstimatedAmount")]
        [Bindable(true)]
        public decimal? EstimatedAmount 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("EstimatedAmount");
		    }
            set 
		    {
			    SetColumnValue("EstimatedAmount", value);
            }
        }
	      
        [XmlAttribute("Remarks")]
        [Bindable(true)]
        public string Remarks 
	    {
		    get
		    {
			    return GetColumnValue<string>("Remarks");
		    }
            set 
		    {
			    SetColumnValue("Remarks", value);
            }
        }
	      
        [XmlAttribute("DepartmentID")]
        [Bindable(true)]
        public int? DepartmentID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("DepartmentID");
		    }
            set 
		    {
			    SetColumnValue("DepartmentID", value);
            }
        }
	      
        [XmlAttribute("Department")]
        [Bindable(true)]
        public string Department 
	    {
		    get
		    {
			    return GetColumnValue<string>("Department");
		    }
            set 
		    {
			    SetColumnValue("Department", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string OrderNumber = @"OrderNumber";
            
            public static string Id = @"ID";
            
            public static string InvoiceNumber = @"InvoiceNumber";
            
            public static string InvoiceDate = @"InvoiceDate";
            
            public static string CompanyName = @"CompanyName";
            
            public static string EstimatedAmount = @"EstimatedAmount";
            
            public static string Remarks = @"Remarks";
            
            public static string DepartmentID = @"DepartmentID";
            
            public static string Department = @"Department";
            
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