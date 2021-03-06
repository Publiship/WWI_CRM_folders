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
    /// Strongly-typed collection for the ClientNameView class.
    /// </summary>
    [Serializable]
    public partial class ClientNameViewCollection : ReadOnlyList<ClientNameView, ClientNameViewCollection>
    {        
        public ClientNameViewCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the ClientNameView view.
    /// </summary>
    [Serializable]
    public partial class ClientNameView : ReadOnlyRecord<ClientNameView>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("ClientNameView", TableType.View, DataService.GetInstance("WWIprov"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarCompanyID = new TableSchema.TableColumn(schema);
                colvarCompanyID.ColumnName = "CompanyID";
                colvarCompanyID.DataType = DbType.Int32;
                colvarCompanyID.MaxLength = 0;
                colvarCompanyID.AutoIncrement = false;
                colvarCompanyID.IsNullable = false;
                colvarCompanyID.IsPrimaryKey = false;
                colvarCompanyID.IsForeignKey = false;
                colvarCompanyID.IsReadOnly = false;
                
                schema.Columns.Add(colvarCompanyID);
                
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
                
                TableSchema.TableColumn colvarAddress1 = new TableSchema.TableColumn(schema);
                colvarAddress1.ColumnName = "Address1";
                colvarAddress1.DataType = DbType.String;
                colvarAddress1.MaxLength = 40;
                colvarAddress1.AutoIncrement = false;
                colvarAddress1.IsNullable = true;
                colvarAddress1.IsPrimaryKey = false;
                colvarAddress1.IsForeignKey = false;
                colvarAddress1.IsReadOnly = false;
                
                schema.Columns.Add(colvarAddress1);
                
                TableSchema.TableColumn colvarAddress2 = new TableSchema.TableColumn(schema);
                colvarAddress2.ColumnName = "Address2";
                colvarAddress2.DataType = DbType.String;
                colvarAddress2.MaxLength = 40;
                colvarAddress2.AutoIncrement = false;
                colvarAddress2.IsNullable = true;
                colvarAddress2.IsPrimaryKey = false;
                colvarAddress2.IsForeignKey = false;
                colvarAddress2.IsReadOnly = false;
                
                schema.Columns.Add(colvarAddress2);
                
                TableSchema.TableColumn colvarAddress3 = new TableSchema.TableColumn(schema);
                colvarAddress3.ColumnName = "Address3";
                colvarAddress3.DataType = DbType.String;
                colvarAddress3.MaxLength = 40;
                colvarAddress3.AutoIncrement = false;
                colvarAddress3.IsNullable = true;
                colvarAddress3.IsPrimaryKey = false;
                colvarAddress3.IsForeignKey = false;
                colvarAddress3.IsReadOnly = false;
                
                schema.Columns.Add(colvarAddress3);
                
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
                
                TableSchema.TableColumn colvarTelNo = new TableSchema.TableColumn(schema);
                colvarTelNo.ColumnName = "TelNo";
                colvarTelNo.DataType = DbType.String;
                colvarTelNo.MaxLength = 20;
                colvarTelNo.AutoIncrement = false;
                colvarTelNo.IsNullable = true;
                colvarTelNo.IsPrimaryKey = false;
                colvarTelNo.IsForeignKey = false;
                colvarTelNo.IsReadOnly = false;
                
                schema.Columns.Add(colvarTelNo);
                
                TableSchema.TableColumn colvarCustomer = new TableSchema.TableColumn(schema);
                colvarCustomer.ColumnName = "Customer";
                colvarCustomer.DataType = DbType.Boolean;
                colvarCustomer.MaxLength = 0;
                colvarCustomer.AutoIncrement = false;
                colvarCustomer.IsNullable = true;
                colvarCustomer.IsPrimaryKey = false;
                colvarCustomer.IsForeignKey = false;
                colvarCustomer.IsReadOnly = false;
                
                schema.Columns.Add(colvarCustomer);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("ClientNameView",schema);
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
	    public ClientNameView()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public ClientNameView(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public ClientNameView(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public ClientNameView(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("CompanyID")]
        [Bindable(true)]
        public int CompanyID 
	    {
		    get
		    {
			    return GetColumnValue<int>("CompanyID");
		    }
            set 
		    {
			    SetColumnValue("CompanyID", value);
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
	      
        [XmlAttribute("Address1")]
        [Bindable(true)]
        public string Address1 
	    {
		    get
		    {
			    return GetColumnValue<string>("Address1");
		    }
            set 
		    {
			    SetColumnValue("Address1", value);
            }
        }
	      
        [XmlAttribute("Address2")]
        [Bindable(true)]
        public string Address2 
	    {
		    get
		    {
			    return GetColumnValue<string>("Address2");
		    }
            set 
		    {
			    SetColumnValue("Address2", value);
            }
        }
	      
        [XmlAttribute("Address3")]
        [Bindable(true)]
        public string Address3 
	    {
		    get
		    {
			    return GetColumnValue<string>("Address3");
		    }
            set 
		    {
			    SetColumnValue("Address3", value);
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
	      
        [XmlAttribute("TelNo")]
        [Bindable(true)]
        public string TelNo 
	    {
		    get
		    {
			    return GetColumnValue<string>("TelNo");
		    }
            set 
		    {
			    SetColumnValue("TelNo", value);
            }
        }
	      
        [XmlAttribute("Customer")]
        [Bindable(true)]
        public bool? Customer 
	    {
		    get
		    {
			    return GetColumnValue<bool?>("Customer");
		    }
            set 
		    {
			    SetColumnValue("Customer", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string CompanyID = @"CompanyID";
            
            public static string CompanyName = @"CompanyName";
            
            public static string Address1 = @"Address1";
            
            public static string Address2 = @"Address2";
            
            public static string Address3 = @"Address3";
            
            public static string CountryName = @"CountryName";
            
            public static string TelNo = @"TelNo";
            
            public static string Customer = @"Customer";
            
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
