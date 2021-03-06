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
    /// Strongly-typed collection for the Page2VesselView class.
    /// </summary>
    [Serializable]
    public partial class Page2VesselViewCollection : ReadOnlyList<Page2VesselView, Page2VesselViewCollection>
    {        
        public Page2VesselViewCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the Page2VesselView view.
    /// </summary>
    [Serializable]
    public partial class Page2VesselView : ReadOnlyRecord<Page2VesselView>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("Page2VesselView", TableType.View, DataService.GetInstance("WWIprov"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarVoyageID = new TableSchema.TableColumn(schema);
                colvarVoyageID.ColumnName = "VoyageID";
                colvarVoyageID.DataType = DbType.Int32;
                colvarVoyageID.MaxLength = 0;
                colvarVoyageID.AutoIncrement = false;
                colvarVoyageID.IsNullable = false;
                colvarVoyageID.IsPrimaryKey = false;
                colvarVoyageID.IsForeignKey = false;
                colvarVoyageID.IsReadOnly = false;
                
                schema.Columns.Add(colvarVoyageID);
                
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
                
                TableSchema.TableColumn colvarDestinationPortID = new TableSchema.TableColumn(schema);
                colvarDestinationPortID.ColumnName = "DestinationPortID";
                colvarDestinationPortID.DataType = DbType.Int32;
                colvarDestinationPortID.MaxLength = 0;
                colvarDestinationPortID.AutoIncrement = false;
                colvarDestinationPortID.IsNullable = true;
                colvarDestinationPortID.IsPrimaryKey = false;
                colvarDestinationPortID.IsForeignKey = false;
                colvarDestinationPortID.IsReadOnly = false;
                
                schema.Columns.Add(colvarDestinationPortID);
                
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
                
                TableSchema.TableColumn colvarOriginPortID = new TableSchema.TableColumn(schema);
                colvarOriginPortID.ColumnName = "OriginPortID";
                colvarOriginPortID.DataType = DbType.Int32;
                colvarOriginPortID.MaxLength = 0;
                colvarOriginPortID.AutoIncrement = false;
                colvarOriginPortID.IsNullable = true;
                colvarOriginPortID.IsPrimaryKey = false;
                colvarOriginPortID.IsForeignKey = false;
                colvarOriginPortID.IsReadOnly = false;
                
                schema.Columns.Add(colvarOriginPortID);
                
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
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("Page2VesselView",schema);
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
	    public Page2VesselView()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public Page2VesselView(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public Page2VesselView(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public Page2VesselView(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("VoyageID")]
        [Bindable(true)]
        public int VoyageID 
	    {
		    get
		    {
			    return GetColumnValue<int>("VoyageID");
		    }
            set 
		    {
			    SetColumnValue("VoyageID", value);
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
	      
        [XmlAttribute("DestinationPortID")]
        [Bindable(true)]
        public int? DestinationPortID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("DestinationPortID");
		    }
            set 
		    {
			    SetColumnValue("DestinationPortID", value);
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
	      
        [XmlAttribute("OriginPortID")]
        [Bindable(true)]
        public int? OriginPortID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("OriginPortID");
		    }
            set 
		    {
			    SetColumnValue("OriginPortID", value);
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
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string VoyageID = @"VoyageID";
            
            public static string Joined = @"Joined";
            
            public static string DestinationPortID = @"DestinationPortID";
            
            public static string Eta = @"ETA";
            
            public static string OriginPortID = @"OriginPortID";
            
            public static string Ets = @"ETS";
            
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
