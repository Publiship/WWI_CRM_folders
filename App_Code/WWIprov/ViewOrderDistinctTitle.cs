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
    /// Strongly-typed collection for the ViewOrderDistinctTitle class.
    /// </summary>
    [Serializable]
    public partial class ViewOrderDistinctTitleCollection : ReadOnlyList<ViewOrderDistinctTitle, ViewOrderDistinctTitleCollection>
    {        
        public ViewOrderDistinctTitleCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the view_order_distinct_title view.
    /// </summary>
    [Serializable]
    public partial class ViewOrderDistinctTitle : ReadOnlyRecord<ViewOrderDistinctTitle>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("view_order_distinct_title", TableType.View, DataService.GetInstance("WWIprov"));
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
                
                TableSchema.TableColumn colvarMintitle = new TableSchema.TableColumn(schema);
                colvarMintitle.ColumnName = "mintitle";
                colvarMintitle.DataType = DbType.String;
                colvarMintitle.MaxLength = 150;
                colvarMintitle.AutoIncrement = false;
                colvarMintitle.IsNullable = true;
                colvarMintitle.IsPrimaryKey = false;
                colvarMintitle.IsForeignKey = false;
                colvarMintitle.IsReadOnly = false;
                
                schema.Columns.Add(colvarMintitle);
                
                TableSchema.TableColumn colvarMintitlid = new TableSchema.TableColumn(schema);
                colvarMintitlid.ColumnName = "mintitlid";
                colvarMintitlid.DataType = DbType.Int32;
                colvarMintitlid.MaxLength = 0;
                colvarMintitlid.AutoIncrement = false;
                colvarMintitlid.IsNullable = true;
                colvarMintitlid.IsPrimaryKey = false;
                colvarMintitlid.IsForeignKey = false;
                colvarMintitlid.IsReadOnly = false;
                
                schema.Columns.Add(colvarMintitlid);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("view_order_distinct_title",schema);
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
	    public ViewOrderDistinctTitle()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public ViewOrderDistinctTitle(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public ViewOrderDistinctTitle(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public ViewOrderDistinctTitle(string columnName, object columnValue)
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
	      
        [XmlAttribute("Mintitle")]
        [Bindable(true)]
        public string Mintitle 
	    {
		    get
		    {
			    return GetColumnValue<string>("mintitle");
		    }
            set 
		    {
			    SetColumnValue("mintitle", value);
            }
        }
	      
        [XmlAttribute("Mintitlid")]
        [Bindable(true)]
        public int? Mintitlid 
	    {
		    get
		    {
			    return GetColumnValue<int?>("mintitlid");
		    }
            set 
		    {
			    SetColumnValue("mintitlid", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string OrderNumber = @"OrderNumber";
            
            public static string Mintitle = @"mintitle";
            
            public static string Mintitlid = @"mintitlid";
            
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
