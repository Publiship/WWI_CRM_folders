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
    /// Strongly-typed collection for the ContainerSearchView class.
    /// </summary>
    [Serializable]
    public partial class ContainerSearchViewCollection : ReadOnlyList<ContainerSearchView, ContainerSearchViewCollection>
    {        
        public ContainerSearchViewCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the ContainerSearchView view.
    /// </summary>
    [Serializable]
    public partial class ContainerSearchView : ReadOnlyRecord<ContainerSearchView>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("ContainerSearchView", TableType.View, DataService.GetInstance("WWIprov"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
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
                
                TableSchema.TableColumn colvarJoined = new TableSchema.TableColumn(schema);
                colvarJoined.ColumnName = "Joined";
                colvarJoined.DataType = DbType.String;
                colvarJoined.MaxLength = 50;
                colvarJoined.AutoIncrement = false;
                colvarJoined.IsNullable = true;
                colvarJoined.IsPrimaryKey = false;
                colvarJoined.IsForeignKey = false;
                colvarJoined.IsReadOnly = false;
                
                schema.Columns.Add(colvarJoined);
                
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
                
                TableSchema.TableColumn colvarEta = new TableSchema.TableColumn(schema);
                colvarEta.ColumnName = "ETA";
                colvarEta.DataType = DbType.DateTime;
                colvarEta.MaxLength = 0;
                colvarEta.AutoIncrement = false;
                colvarEta.IsNullable = true;
                colvarEta.IsPrimaryKey = false;
                colvarEta.IsForeignKey = false;
                colvarEta.IsReadOnly = false;
                
                schema.Columns.Add(colvarEta);
                
                TableSchema.TableColumn colvarLoadedOnBoard = new TableSchema.TableColumn(schema);
                colvarLoadedOnBoard.ColumnName = "LoadedOnBoard";
                colvarLoadedOnBoard.DataType = DbType.Boolean;
                colvarLoadedOnBoard.MaxLength = 0;
                colvarLoadedOnBoard.AutoIncrement = false;
                colvarLoadedOnBoard.IsNullable = true;
                colvarLoadedOnBoard.IsPrimaryKey = false;
                colvarLoadedOnBoard.IsForeignKey = false;
                colvarLoadedOnBoard.IsReadOnly = false;
                
                schema.Columns.Add(colvarLoadedOnBoard);
                
                TableSchema.TableColumn colvarDelivered = new TableSchema.TableColumn(schema);
                colvarDelivered.ColumnName = "Delivered";
                colvarDelivered.DataType = DbType.Boolean;
                colvarDelivered.MaxLength = 0;
                colvarDelivered.AutoIncrement = false;
                colvarDelivered.IsNullable = true;
                colvarDelivered.IsPrimaryKey = false;
                colvarDelivered.IsForeignKey = false;
                colvarDelivered.IsReadOnly = false;
                
                schema.Columns.Add(colvarDelivered);
                
                TableSchema.TableColumn colvarDeliveryDate = new TableSchema.TableColumn(schema);
                colvarDeliveryDate.ColumnName = "DeliveryDate";
                colvarDeliveryDate.DataType = DbType.DateTime;
                colvarDeliveryDate.MaxLength = 0;
                colvarDeliveryDate.AutoIncrement = false;
                colvarDeliveryDate.IsNullable = true;
                colvarDeliveryDate.IsPrimaryKey = false;
                colvarDeliveryDate.IsForeignKey = false;
                colvarDeliveryDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarDeliveryDate);
                
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
                
                TableSchema.TableColumn colvarDestPort = new TableSchema.TableColumn(schema);
                colvarDestPort.ColumnName = "DestPort";
                colvarDestPort.DataType = DbType.String;
                colvarDestPort.MaxLength = 30;
                colvarDestPort.AutoIncrement = false;
                colvarDestPort.IsNullable = true;
                colvarDestPort.IsPrimaryKey = false;
                colvarDestPort.IsForeignKey = false;
                colvarDestPort.IsReadOnly = false;
                
                schema.Columns.Add(colvarDestPort);
                
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
                
                TableSchema.TableColumn colvarDevanWarehouseID = new TableSchema.TableColumn(schema);
                colvarDevanWarehouseID.ColumnName = "DevanWarehouseID";
                colvarDevanWarehouseID.DataType = DbType.Int32;
                colvarDevanWarehouseID.MaxLength = 0;
                colvarDevanWarehouseID.AutoIncrement = false;
                colvarDevanWarehouseID.IsNullable = true;
                colvarDevanWarehouseID.IsPrimaryKey = false;
                colvarDevanWarehouseID.IsForeignKey = false;
                colvarDevanWarehouseID.IsReadOnly = false;
                
                schema.Columns.Add(colvarDevanWarehouseID);
                
                TableSchema.TableColumn colvarDevanned = new TableSchema.TableColumn(schema);
                colvarDevanned.ColumnName = "Devanned";
                colvarDevanned.DataType = DbType.Boolean;
                colvarDevanned.MaxLength = 0;
                colvarDevanned.AutoIncrement = false;
                colvarDevanned.IsNullable = true;
                colvarDevanned.IsPrimaryKey = false;
                colvarDevanned.IsForeignKey = false;
                colvarDevanned.IsReadOnly = false;
                
                schema.Columns.Add(colvarDevanned);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("ContainerSearchView",schema);
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
	    public ContainerSearchView()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public ContainerSearchView(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public ContainerSearchView(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public ContainerSearchView(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
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
	      
        [XmlAttribute("Joined")]
        [Bindable(true)]
        public string Joined 
	    {
		    get
		    {
			    return GetColumnValue<string>("Joined");
		    }
            set 
		    {
			    SetColumnValue("Joined", value);
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
	      
        [XmlAttribute("Eta")]
        [Bindable(true)]
        public DateTime? Eta 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("ETA");
		    }
            set 
		    {
			    SetColumnValue("ETA", value);
            }
        }
	      
        [XmlAttribute("LoadedOnBoard")]
        [Bindable(true)]
        public bool? LoadedOnBoard 
	    {
		    get
		    {
			    return GetColumnValue<bool?>("LoadedOnBoard");
		    }
            set 
		    {
			    SetColumnValue("LoadedOnBoard", value);
            }
        }
	      
        [XmlAttribute("Delivered")]
        [Bindable(true)]
        public bool? Delivered 
	    {
		    get
		    {
			    return GetColumnValue<bool?>("Delivered");
		    }
            set 
		    {
			    SetColumnValue("Delivered", value);
            }
        }
	      
        [XmlAttribute("DeliveryDate")]
        [Bindable(true)]
        public DateTime? DeliveryDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("DeliveryDate");
		    }
            set 
		    {
			    SetColumnValue("DeliveryDate", value);
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
	      
        [XmlAttribute("DestPort")]
        [Bindable(true)]
        public string DestPort 
	    {
		    get
		    {
			    return GetColumnValue<string>("DestPort");
		    }
            set 
		    {
			    SetColumnValue("DestPort", value);
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
	      
        [XmlAttribute("DevanWarehouseID")]
        [Bindable(true)]
        public int? DevanWarehouseID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("DevanWarehouseID");
		    }
            set 
		    {
			    SetColumnValue("DevanWarehouseID", value);
            }
        }
	      
        [XmlAttribute("Devanned")]
        [Bindable(true)]
        public bool? Devanned 
	    {
		    get
		    {
			    return GetColumnValue<bool?>("Devanned");
		    }
            set 
		    {
			    SetColumnValue("Devanned", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string ContainerNumber = @"ContainerNumber";
            
            public static string Joined = @"Joined";
            
            public static string Ets = @"ETS";
            
            public static string Eta = @"ETA";
            
            public static string LoadedOnBoard = @"LoadedOnBoard";
            
            public static string Delivered = @"Delivered";
            
            public static string DeliveryDate = @"DeliveryDate";
            
            public static string OriginPort = @"OriginPort";
            
            public static string DestPort = @"DestPort";
            
            public static string DevanDate = @"DevanDate";
            
            public static string DevanWarehouseID = @"DevanWarehouseID";
            
            public static string Devanned = @"Devanned";
            
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