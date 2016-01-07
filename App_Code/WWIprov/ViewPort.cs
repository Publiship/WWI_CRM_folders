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
    /// Strongly-typed collection for the ViewPort class.
    /// </summary>
    [Serializable]
    public partial class ViewPortCollection : ReadOnlyList<ViewPort, ViewPortCollection>
    {        
        public ViewPortCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the view_ports view.
    /// </summary>
    [Serializable]
    public partial class ViewPort : ReadOnlyRecord<ViewPort>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("view_ports", TableType.View, DataService.GetInstance("WWIprov"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarPortID = new TableSchema.TableColumn(schema);
                colvarPortID.ColumnName = "PortID";
                colvarPortID.DataType = DbType.Int32;
                colvarPortID.MaxLength = 0;
                colvarPortID.AutoIncrement = false;
                colvarPortID.IsNullable = false;
                colvarPortID.IsPrimaryKey = false;
                colvarPortID.IsForeignKey = false;
                colvarPortID.IsReadOnly = false;
                
                schema.Columns.Add(colvarPortID);
                
                TableSchema.TableColumn colvarPortName = new TableSchema.TableColumn(schema);
                colvarPortName.ColumnName = "PortName";
                colvarPortName.DataType = DbType.String;
                colvarPortName.MaxLength = 30;
                colvarPortName.AutoIncrement = false;
                colvarPortName.IsNullable = true;
                colvarPortName.IsPrimaryKey = false;
                colvarPortName.IsForeignKey = false;
                colvarPortName.IsReadOnly = false;
                
                schema.Columns.Add(colvarPortName);
                
                TableSchema.TableColumn colvarCountryName = new TableSchema.TableColumn(schema);
                colvarCountryName.ColumnName = "CountryName";
                colvarCountryName.DataType = DbType.String;
                colvarCountryName.MaxLength = 50;
                colvarCountryName.AutoIncrement = false;
                colvarCountryName.IsNullable = true;
                colvarCountryName.IsPrimaryKey = false;
                colvarCountryName.IsForeignKey = false;
                colvarCountryName.IsReadOnly = false;
                
                schema.Columns.Add(colvarCountryName);
                
                TableSchema.TableColumn colvarEu = new TableSchema.TableColumn(schema);
                colvarEu.ColumnName = "EU";
                colvarEu.DataType = DbType.Boolean;
                colvarEu.MaxLength = 0;
                colvarEu.AutoIncrement = false;
                colvarEu.IsNullable = true;
                colvarEu.IsPrimaryKey = false;
                colvarEu.IsForeignKey = false;
                colvarEu.IsReadOnly = false;
                
                schema.Columns.Add(colvarEu);
                
                TableSchema.TableColumn colvarISOCode = new TableSchema.TableColumn(schema);
                colvarISOCode.ColumnName = "ISOCode";
                colvarISOCode.DataType = DbType.AnsiStringFixedLength;
                colvarISOCode.MaxLength = 10;
                colvarISOCode.AutoIncrement = false;
                colvarISOCode.IsNullable = true;
                colvarISOCode.IsPrimaryKey = false;
                colvarISOCode.IsForeignKey = false;
                colvarISOCode.IsReadOnly = false;
                
                schema.Columns.Add(colvarISOCode);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("view_ports",schema);
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
	    public ViewPort()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public ViewPort(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public ViewPort(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public ViewPort(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("PortID")]
        [Bindable(true)]
        public int PortID 
	    {
		    get
		    {
			    return GetColumnValue<int>("PortID");
		    }
            set 
		    {
			    SetColumnValue("PortID", value);
            }
        }
	      
        [XmlAttribute("PortName")]
        [Bindable(true)]
        public string PortName 
	    {
		    get
		    {
			    return GetColumnValue<string>("PortName");
		    }
            set 
		    {
			    SetColumnValue("PortName", value);
            }
        }
	      
        [XmlAttribute("CountryName")]
        [Bindable(true)]
        public string CountryName 
	    {
		    get
		    {
			    return GetColumnValue<string>("CountryName");
		    }
            set 
		    {
			    SetColumnValue("CountryName", value);
            }
        }
	      
        [XmlAttribute("Eu")]
        [Bindable(true)]
        public bool? Eu 
	    {
		    get
		    {
			    return GetColumnValue<bool?>("EU");
		    }
            set 
		    {
			    SetColumnValue("EU", value);
            }
        }
	      
        [XmlAttribute("ISOCode")]
        [Bindable(true)]
        public string ISOCode 
	    {
		    get
		    {
			    return GetColumnValue<string>("ISOCode");
		    }
            set 
		    {
			    SetColumnValue("ISOCode", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string PortID = @"PortID";
            
            public static string PortName = @"PortName";
            
            public static string CountryName = @"CountryName";
            
            public static string Eu = @"EU";
            
            public static string ISOCode = @"ISOCode";
            
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