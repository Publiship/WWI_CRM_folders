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
    /// Strongly-typed collection for the PortsAndPlacesView class.
    /// </summary>
    [Serializable]
    public partial class PortsAndPlacesViewCollection : ReadOnlyList<PortsAndPlacesView, PortsAndPlacesViewCollection>
    {        
        public PortsAndPlacesViewCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the PortsAndPlacesView view.
    /// </summary>
    [Serializable]
    public partial class PortsAndPlacesView : ReadOnlyRecord<PortsAndPlacesView>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("PortsAndPlacesView", TableType.View, DataService.GetInstance("WWIprov"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarOrderID = new TableSchema.TableColumn(schema);
                colvarOrderID.ColumnName = "OrderID";
                colvarOrderID.DataType = DbType.Int32;
                colvarOrderID.MaxLength = 0;
                colvarOrderID.AutoIncrement = false;
                colvarOrderID.IsNullable = false;
                colvarOrderID.IsPrimaryKey = false;
                colvarOrderID.IsForeignKey = false;
                colvarOrderID.IsReadOnly = false;
                
                schema.Columns.Add(colvarOrderID);
                
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
                
                TableSchema.TableColumn colvarOriginPort = new TableSchema.TableColumn(schema);
                colvarOriginPort.ColumnName = "OriginPort";
                colvarOriginPort.DataType = DbType.String;
                colvarOriginPort.MaxLength = 30;
                colvarOriginPort.AutoIncrement = false;
                colvarOriginPort.IsNullable = true;
                colvarOriginPort.IsPrimaryKey = false;
                colvarOriginPort.IsForeignKey = false;
                colvarOriginPort.IsReadOnly = false;
                
                schema.Columns.Add(colvarOriginPort);
                
                TableSchema.TableColumn colvarDestinationPort = new TableSchema.TableColumn(schema);
                colvarDestinationPort.ColumnName = "DestinationPort";
                colvarDestinationPort.DataType = DbType.String;
                colvarDestinationPort.MaxLength = 30;
                colvarDestinationPort.AutoIncrement = false;
                colvarDestinationPort.IsNullable = true;
                colvarDestinationPort.IsPrimaryKey = false;
                colvarDestinationPort.IsForeignKey = false;
                colvarDestinationPort.IsReadOnly = false;
                
                schema.Columns.Add(colvarDestinationPort);
                
                TableSchema.TableColumn colvarFinalDest = new TableSchema.TableColumn(schema);
                colvarFinalDest.ColumnName = "FinalDest";
                colvarFinalDest.DataType = DbType.String;
                colvarFinalDest.MaxLength = 30;
                colvarFinalDest.AutoIncrement = false;
                colvarFinalDest.IsNullable = true;
                colvarFinalDest.IsPrimaryKey = false;
                colvarFinalDest.IsForeignKey = false;
                colvarFinalDest.IsReadOnly = false;
                
                schema.Columns.Add(colvarFinalDest);
                
                TableSchema.TableColumn colvarOriginPoint = new TableSchema.TableColumn(schema);
                colvarOriginPoint.ColumnName = "OriginPoint";
                colvarOriginPoint.DataType = DbType.String;
                colvarOriginPoint.MaxLength = 30;
                colvarOriginPoint.AutoIncrement = false;
                colvarOriginPoint.IsNullable = true;
                colvarOriginPoint.IsPrimaryKey = false;
                colvarOriginPoint.IsForeignKey = false;
                colvarOriginPoint.IsReadOnly = false;
                
                schema.Columns.Add(colvarOriginPoint);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("PortsAndPlacesView",schema);
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
	    public PortsAndPlacesView()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public PortsAndPlacesView(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public PortsAndPlacesView(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public PortsAndPlacesView(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("OrderID")]
        [Bindable(true)]
        public int OrderID 
	    {
		    get
		    {
			    return GetColumnValue<int>("OrderID");
		    }
            set 
		    {
			    SetColumnValue("OrderID", value);
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
	      
        [XmlAttribute("OriginPort")]
        [Bindable(true)]
        public string OriginPort 
	    {
		    get
		    {
			    return GetColumnValue<string>("OriginPort");
		    }
            set 
		    {
			    SetColumnValue("OriginPort", value);
            }
        }
	      
        [XmlAttribute("DestinationPort")]
        [Bindable(true)]
        public string DestinationPort 
	    {
		    get
		    {
			    return GetColumnValue<string>("DestinationPort");
		    }
            set 
		    {
			    SetColumnValue("DestinationPort", value);
            }
        }
	      
        [XmlAttribute("FinalDest")]
        [Bindable(true)]
        public string FinalDest 
	    {
		    get
		    {
			    return GetColumnValue<string>("FinalDest");
		    }
            set 
		    {
			    SetColumnValue("FinalDest", value);
            }
        }
	      
        [XmlAttribute("OriginPoint")]
        [Bindable(true)]
        public string OriginPoint 
	    {
		    get
		    {
			    return GetColumnValue<string>("OriginPoint");
		    }
            set 
		    {
			    SetColumnValue("OriginPoint", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string OrderID = @"OrderID";
            
            public static string OrderNumber = @"OrderNumber";
            
            public static string OriginPort = @"OriginPort";
            
            public static string DestinationPort = @"DestinationPort";
            
            public static string FinalDest = @"FinalDest";
            
            public static string OriginPoint = @"OriginPoint";
            
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
