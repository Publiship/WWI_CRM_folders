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
    /// Strongly-typed collection for the PearsonRugbyContainerView class.
    /// </summary>
    [Serializable]
    public partial class PearsonRugbyContainerViewCollection : ReadOnlyList<PearsonRugbyContainerView, PearsonRugbyContainerViewCollection>
    {        
        public PearsonRugbyContainerViewCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the PearsonRugbyContainerView view.
    /// </summary>
    [Serializable]
    public partial class PearsonRugbyContainerView : ReadOnlyRecord<PearsonRugbyContainerView>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("PearsonRugbyContainerView", TableType.View, DataService.GetInstance("WWIprov"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarContainerSubID = new TableSchema.TableColumn(schema);
                colvarContainerSubID.ColumnName = "ContainerSubID";
                colvarContainerSubID.DataType = DbType.Int32;
                colvarContainerSubID.MaxLength = 0;
                colvarContainerSubID.AutoIncrement = false;
                colvarContainerSubID.IsNullable = true;
                colvarContainerSubID.IsPrimaryKey = false;
                colvarContainerSubID.IsForeignKey = false;
                colvarContainerSubID.IsReadOnly = false;
                
                schema.Columns.Add(colvarContainerSubID);
                
                TableSchema.TableColumn colvarContainerNumber = new TableSchema.TableColumn(schema);
                colvarContainerNumber.ColumnName = "ContainerNumber";
                colvarContainerNumber.DataType = DbType.String;
                colvarContainerNumber.MaxLength = 50;
                colvarContainerNumber.AutoIncrement = false;
                colvarContainerNumber.IsNullable = true;
                colvarContainerNumber.IsPrimaryKey = false;
                colvarContainerNumber.IsForeignKey = false;
                colvarContainerNumber.IsReadOnly = false;
                
                schema.Columns.Add(colvarContainerNumber);
                
                TableSchema.TableColumn colvarDeliveryAddress = new TableSchema.TableColumn(schema);
                colvarDeliveryAddress.ColumnName = "DeliveryAddress";
                colvarDeliveryAddress.DataType = DbType.Int32;
                colvarDeliveryAddress.MaxLength = 0;
                colvarDeliveryAddress.AutoIncrement = false;
                colvarDeliveryAddress.IsNullable = true;
                colvarDeliveryAddress.IsPrimaryKey = false;
                colvarDeliveryAddress.IsForeignKey = false;
                colvarDeliveryAddress.IsReadOnly = false;
                
                schema.Columns.Add(colvarDeliveryAddress);
                
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
                
                TableSchema.TableColumn colvarEts = new TableSchema.TableColumn(schema);
                colvarEts.ColumnName = "ETS";
                colvarEts.DataType = DbType.DateTime;
                colvarEts.MaxLength = 0;
                colvarEts.AutoIncrement = false;
                colvarEts.IsNullable = true;
                colvarEts.IsPrimaryKey = false;
                colvarEts.IsForeignKey = false;
                colvarEts.IsReadOnly = false;
                
                schema.Columns.Add(colvarEts);
                
                TableSchema.TableColumn colvarDevanDate = new TableSchema.TableColumn(schema);
                colvarDevanDate.ColumnName = "DevanDate";
                colvarDevanDate.DataType = DbType.DateTime;
                colvarDevanDate.MaxLength = 0;
                colvarDevanDate.AutoIncrement = false;
                colvarDevanDate.IsNullable = true;
                colvarDevanDate.IsPrimaryKey = false;
                colvarDevanDate.IsForeignKey = false;
                colvarDevanDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarDevanDate);
                
                TableSchema.TableColumn colvarDevanWarehouse = new TableSchema.TableColumn(schema);
                colvarDevanWarehouse.ColumnName = "Devan Warehouse";
                colvarDevanWarehouse.DataType = DbType.String;
                colvarDevanWarehouse.MaxLength = 50;
                colvarDevanWarehouse.AutoIncrement = false;
                colvarDevanWarehouse.IsNullable = true;
                colvarDevanWarehouse.IsPrimaryKey = false;
                colvarDevanWarehouse.IsForeignKey = false;
                colvarDevanWarehouse.IsReadOnly = false;
                
                schema.Columns.Add(colvarDevanWarehouse);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("PearsonRugbyContainerView",schema);
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
	    public PearsonRugbyContainerView()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public PearsonRugbyContainerView(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public PearsonRugbyContainerView(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public PearsonRugbyContainerView(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("ContainerSubID")]
        [Bindable(true)]
        public int? ContainerSubID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("ContainerSubID");
		    }
            set 
		    {
			    SetColumnValue("ContainerSubID", value);
            }
        }
	      
        [XmlAttribute("ContainerNumber")]
        [Bindable(true)]
        public string ContainerNumber 
	    {
		    get
		    {
			    return GetColumnValue<string>("ContainerNumber");
		    }
            set 
		    {
			    SetColumnValue("ContainerNumber", value);
            }
        }
	      
        [XmlAttribute("DeliveryAddress")]
        [Bindable(true)]
        public int? DeliveryAddress 
	    {
		    get
		    {
			    return GetColumnValue<int?>("DeliveryAddress");
		    }
            set 
		    {
			    SetColumnValue("DeliveryAddress", value);
            }
        }
	      
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
	      
        [XmlAttribute("Ets")]
        [Bindable(true)]
        public DateTime? Ets 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("ETS");
		    }
            set 
		    {
			    SetColumnValue("ETS", value);
            }
        }
	      
        [XmlAttribute("DevanDate")]
        [Bindable(true)]
        public DateTime? DevanDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("DevanDate");
		    }
            set 
		    {
			    SetColumnValue("DevanDate", value);
            }
        }
	      
        [XmlAttribute("DevanWarehouse")]
        [Bindable(true)]
        public string DevanWarehouse 
	    {
		    get
		    {
			    return GetColumnValue<string>("Devan Warehouse");
		    }
            set 
		    {
			    SetColumnValue("Devan Warehouse", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string ContainerSubID = @"ContainerSubID";
            
            public static string ContainerNumber = @"ContainerNumber";
            
            public static string DeliveryAddress = @"DeliveryAddress";
            
            public static string OrderNumber = @"OrderNumber";
            
            public static string Ets = @"ETS";
            
            public static string DevanDate = @"DevanDate";
            
            public static string DevanWarehouse = @"Devan Warehouse";
            
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