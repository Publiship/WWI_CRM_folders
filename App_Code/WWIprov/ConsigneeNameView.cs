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
    /// Strongly-typed collection for the ConsigneeNameView class.
    /// </summary>
    [Serializable]
    public partial class ConsigneeNameViewCollection : ReadOnlyList<ConsigneeNameView, ConsigneeNameViewCollection>
    {        
        public ConsigneeNameViewCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the ConsigneeNameView view.
    /// </summary>
    [Serializable]
    public partial class ConsigneeNameView : ReadOnlyRecord<ConsigneeNameView>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("ConsigneeNameView", TableType.View, DataService.GetInstance("WWIprov"));
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
                
                TableSchema.TableColumn colvarConsigneeName = new TableSchema.TableColumn(schema);
                colvarConsigneeName.ColumnName = "ConsigneeName";
                colvarConsigneeName.DataType = DbType.String;
                colvarConsigneeName.MaxLength = 50;
                colvarConsigneeName.AutoIncrement = false;
                colvarConsigneeName.IsNullable = true;
                colvarConsigneeName.IsPrimaryKey = false;
                colvarConsigneeName.IsForeignKey = false;
                colvarConsigneeName.IsReadOnly = false;
                
                schema.Columns.Add(colvarConsigneeName);
                
                TableSchema.TableColumn colvarConsigneeAdd1 = new TableSchema.TableColumn(schema);
                colvarConsigneeAdd1.ColumnName = "ConsigneeAdd1";
                colvarConsigneeAdd1.DataType = DbType.String;
                colvarConsigneeAdd1.MaxLength = 40;
                colvarConsigneeAdd1.AutoIncrement = false;
                colvarConsigneeAdd1.IsNullable = true;
                colvarConsigneeAdd1.IsPrimaryKey = false;
                colvarConsigneeAdd1.IsForeignKey = false;
                colvarConsigneeAdd1.IsReadOnly = false;
                
                schema.Columns.Add(colvarConsigneeAdd1);
                
                TableSchema.TableColumn colvarConsigneeAdd2 = new TableSchema.TableColumn(schema);
                colvarConsigneeAdd2.ColumnName = "ConsigneeAdd2";
                colvarConsigneeAdd2.DataType = DbType.String;
                colvarConsigneeAdd2.MaxLength = 40;
                colvarConsigneeAdd2.AutoIncrement = false;
                colvarConsigneeAdd2.IsNullable = true;
                colvarConsigneeAdd2.IsPrimaryKey = false;
                colvarConsigneeAdd2.IsForeignKey = false;
                colvarConsigneeAdd2.IsReadOnly = false;
                
                schema.Columns.Add(colvarConsigneeAdd2);
                
                TableSchema.TableColumn colvarConsigneeAdd3 = new TableSchema.TableColumn(schema);
                colvarConsigneeAdd3.ColumnName = "ConsigneeAdd3";
                colvarConsigneeAdd3.DataType = DbType.String;
                colvarConsigneeAdd3.MaxLength = 40;
                colvarConsigneeAdd3.AutoIncrement = false;
                colvarConsigneeAdd3.IsNullable = true;
                colvarConsigneeAdd3.IsPrimaryKey = false;
                colvarConsigneeAdd3.IsForeignKey = false;
                colvarConsigneeAdd3.IsReadOnly = false;
                
                schema.Columns.Add(colvarConsigneeAdd3);
                
                TableSchema.TableColumn colvarConsigneeCountry = new TableSchema.TableColumn(schema);
                colvarConsigneeCountry.ColumnName = "ConsigneeCountry";
                colvarConsigneeCountry.DataType = DbType.String;
                colvarConsigneeCountry.MaxLength = 50;
                colvarConsigneeCountry.AutoIncrement = false;
                colvarConsigneeCountry.IsNullable = false;
                colvarConsigneeCountry.IsPrimaryKey = false;
                colvarConsigneeCountry.IsForeignKey = false;
                colvarConsigneeCountry.IsReadOnly = false;
                
                schema.Columns.Add(colvarConsigneeCountry);
                
                TableSchema.TableColumn colvarConsigneeTelNo = new TableSchema.TableColumn(schema);
                colvarConsigneeTelNo.ColumnName = "ConsigneeTelNo";
                colvarConsigneeTelNo.DataType = DbType.String;
                colvarConsigneeTelNo.MaxLength = 20;
                colvarConsigneeTelNo.AutoIncrement = false;
                colvarConsigneeTelNo.IsNullable = true;
                colvarConsigneeTelNo.IsPrimaryKey = false;
                colvarConsigneeTelNo.IsForeignKey = false;
                colvarConsigneeTelNo.IsReadOnly = false;
                
                schema.Columns.Add(colvarConsigneeTelNo);
                
                TableSchema.TableColumn colvarConsignee = new TableSchema.TableColumn(schema);
                colvarConsignee.ColumnName = "Consignee";
                colvarConsignee.DataType = DbType.Boolean;
                colvarConsignee.MaxLength = 0;
                colvarConsignee.AutoIncrement = false;
                colvarConsignee.IsNullable = true;
                colvarConsignee.IsPrimaryKey = false;
                colvarConsignee.IsForeignKey = false;
                colvarConsignee.IsReadOnly = false;
                
                schema.Columns.Add(colvarConsignee);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("ConsigneeNameView",schema);
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
	    public ConsigneeNameView()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public ConsigneeNameView(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public ConsigneeNameView(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public ConsigneeNameView(string columnName, object columnValue)
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
	      
        [XmlAttribute("ConsigneeName")]
        [Bindable(true)]
        public string ConsigneeName 
	    {
		    get
		    {
			    return GetColumnValue<string>("ConsigneeName");
		    }
            set 
		    {
			    SetColumnValue("ConsigneeName", value);
            }
        }
	      
        [XmlAttribute("ConsigneeAdd1")]
        [Bindable(true)]
        public string ConsigneeAdd1 
	    {
		    get
		    {
			    return GetColumnValue<string>("ConsigneeAdd1");
		    }
            set 
		    {
			    SetColumnValue("ConsigneeAdd1", value);
            }
        }
	      
        [XmlAttribute("ConsigneeAdd2")]
        [Bindable(true)]
        public string ConsigneeAdd2 
	    {
		    get
		    {
			    return GetColumnValue<string>("ConsigneeAdd2");
		    }
            set 
		    {
			    SetColumnValue("ConsigneeAdd2", value);
            }
        }
	      
        [XmlAttribute("ConsigneeAdd3")]
        [Bindable(true)]
        public string ConsigneeAdd3 
	    {
		    get
		    {
			    return GetColumnValue<string>("ConsigneeAdd3");
		    }
            set 
		    {
			    SetColumnValue("ConsigneeAdd3", value);
            }
        }
	      
        [XmlAttribute("ConsigneeCountry")]
        [Bindable(true)]
        public string ConsigneeCountry 
	    {
		    get
		    {
			    return GetColumnValue<string>("ConsigneeCountry");
		    }
            set 
		    {
			    SetColumnValue("ConsigneeCountry", value);
            }
        }
	      
        [XmlAttribute("ConsigneeTelNo")]
        [Bindable(true)]
        public string ConsigneeTelNo 
	    {
		    get
		    {
			    return GetColumnValue<string>("ConsigneeTelNo");
		    }
            set 
		    {
			    SetColumnValue("ConsigneeTelNo", value);
            }
        }
	      
        [XmlAttribute("Consignee")]
        [Bindable(true)]
        public bool? Consignee 
	    {
		    get
		    {
			    return GetColumnValue<bool?>("Consignee");
		    }
            set 
		    {
			    SetColumnValue("Consignee", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string CompanyID = @"CompanyID";
            
            public static string ConsigneeName = @"ConsigneeName";
            
            public static string ConsigneeAdd1 = @"ConsigneeAdd1";
            
            public static string ConsigneeAdd2 = @"ConsigneeAdd2";
            
            public static string ConsigneeAdd3 = @"ConsigneeAdd3";
            
            public static string ConsigneeCountry = @"ConsigneeCountry";
            
            public static string ConsigneeTelNo = @"ConsigneeTelNo";
            
            public static string Consignee = @"Consignee";
            
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
