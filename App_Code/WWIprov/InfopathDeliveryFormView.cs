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
    /// Strongly-typed collection for the InfopathDeliveryFormView class.
    /// </summary>
    [Serializable]
    public partial class InfopathDeliveryFormViewCollection : ReadOnlyList<InfopathDeliveryFormView, InfopathDeliveryFormViewCollection>
    {        
        public InfopathDeliveryFormViewCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the InfopathDeliveryFormView view.
    /// </summary>
    [Serializable]
    public partial class InfopathDeliveryFormView : ReadOnlyRecord<InfopathDeliveryFormView>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("InfopathDeliveryFormView", TableType.View, DataService.GetInstance("WWIprov"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
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
                
                TableSchema.TableColumn colvarDeliveryID = new TableSchema.TableColumn(schema);
                colvarDeliveryID.ColumnName = "DeliveryID";
                colvarDeliveryID.DataType = DbType.Int32;
                colvarDeliveryID.MaxLength = 0;
                colvarDeliveryID.AutoIncrement = false;
                colvarDeliveryID.IsNullable = false;
                colvarDeliveryID.IsPrimaryKey = false;
                colvarDeliveryID.IsForeignKey = false;
                colvarDeliveryID.IsReadOnly = false;
                
                schema.Columns.Add(colvarDeliveryID);
                
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
                
                TableSchema.TableColumn colvarTitle = new TableSchema.TableColumn(schema);
                colvarTitle.ColumnName = "Title";
                colvarTitle.DataType = DbType.String;
                colvarTitle.MaxLength = 150;
                colvarTitle.AutoIncrement = false;
                colvarTitle.IsNullable = true;
                colvarTitle.IsPrimaryKey = false;
                colvarTitle.IsForeignKey = false;
                colvarTitle.IsReadOnly = false;
                
                schema.Columns.Add(colvarTitle);
                
                TableSchema.TableColumn colvarTitleID = new TableSchema.TableColumn(schema);
                colvarTitleID.ColumnName = "TitleID";
                colvarTitleID.DataType = DbType.Int32;
                colvarTitleID.MaxLength = 0;
                colvarTitleID.AutoIncrement = false;
                colvarTitleID.IsNullable = true;
                colvarTitleID.IsPrimaryKey = false;
                colvarTitleID.IsForeignKey = false;
                colvarTitleID.IsReadOnly = false;
                
                schema.Columns.Add(colvarTitleID);
                
                TableSchema.TableColumn colvarCopies = new TableSchema.TableColumn(schema);
                colvarCopies.ColumnName = "Copies";
                colvarCopies.DataType = DbType.Int32;
                colvarCopies.MaxLength = 0;
                colvarCopies.AutoIncrement = false;
                colvarCopies.IsNullable = true;
                colvarCopies.IsPrimaryKey = false;
                colvarCopies.IsForeignKey = false;
                colvarCopies.IsReadOnly = false;
                
                schema.Columns.Add(colvarCopies);
                
                TableSchema.TableColumn colvarCopiesPerCarton = new TableSchema.TableColumn(schema);
                colvarCopiesPerCarton.ColumnName = "CopiesPerCarton";
                colvarCopiesPerCarton.DataType = DbType.Int32;
                colvarCopiesPerCarton.MaxLength = 0;
                colvarCopiesPerCarton.AutoIncrement = false;
                colvarCopiesPerCarton.IsNullable = true;
                colvarCopiesPerCarton.IsPrimaryKey = false;
                colvarCopiesPerCarton.IsForeignKey = false;
                colvarCopiesPerCarton.IsReadOnly = false;
                
                schema.Columns.Add(colvarCopiesPerCarton);
                
                TableSchema.TableColumn colvarTotalConsignmentWeight = new TableSchema.TableColumn(schema);
                colvarTotalConsignmentWeight.ColumnName = "TotalConsignmentWeight";
                colvarTotalConsignmentWeight.DataType = DbType.Single;
                colvarTotalConsignmentWeight.MaxLength = 0;
                colvarTotalConsignmentWeight.AutoIncrement = false;
                colvarTotalConsignmentWeight.IsNullable = true;
                colvarTotalConsignmentWeight.IsPrimaryKey = false;
                colvarTotalConsignmentWeight.IsForeignKey = false;
                colvarTotalConsignmentWeight.IsReadOnly = false;
                
                schema.Columns.Add(colvarTotalConsignmentWeight);
                
                TableSchema.TableColumn colvarTotalConsignmentCube = new TableSchema.TableColumn(schema);
                colvarTotalConsignmentCube.ColumnName = "TotalConsignmentCube";
                colvarTotalConsignmentCube.DataType = DbType.Single;
                colvarTotalConsignmentCube.MaxLength = 0;
                colvarTotalConsignmentCube.AutoIncrement = false;
                colvarTotalConsignmentCube.IsNullable = true;
                colvarTotalConsignmentCube.IsPrimaryKey = false;
                colvarTotalConsignmentCube.IsForeignKey = false;
                colvarTotalConsignmentCube.IsReadOnly = false;
                
                schema.Columns.Add(colvarTotalConsignmentCube);
                
                TableSchema.TableColumn colvarTotalCartons = new TableSchema.TableColumn(schema);
                colvarTotalCartons.ColumnName = "TotalCartons";
                colvarTotalCartons.DataType = DbType.Int32;
                colvarTotalCartons.MaxLength = 0;
                colvarTotalCartons.AutoIncrement = false;
                colvarTotalCartons.IsNullable = true;
                colvarTotalCartons.IsPrimaryKey = false;
                colvarTotalCartons.IsForeignKey = false;
                colvarTotalCartons.IsReadOnly = false;
                
                schema.Columns.Add(colvarTotalCartons);
                
                TableSchema.TableColumn colvarCartonWeight = new TableSchema.TableColumn(schema);
                colvarCartonWeight.ColumnName = "CartonWeight";
                colvarCartonWeight.DataType = DbType.Single;
                colvarCartonWeight.MaxLength = 0;
                colvarCartonWeight.AutoIncrement = false;
                colvarCartonWeight.IsNullable = true;
                colvarCartonWeight.IsPrimaryKey = false;
                colvarCartonWeight.IsForeignKey = false;
                colvarCartonWeight.IsReadOnly = false;
                
                schema.Columns.Add(colvarCartonWeight);
                
                TableSchema.TableColumn colvarLastCarton = new TableSchema.TableColumn(schema);
                colvarLastCarton.ColumnName = "LastCarton";
                colvarLastCarton.DataType = DbType.Single;
                colvarLastCarton.MaxLength = 0;
                colvarLastCarton.AutoIncrement = false;
                colvarLastCarton.IsNullable = true;
                colvarLastCarton.IsPrimaryKey = false;
                colvarLastCarton.IsForeignKey = false;
                colvarLastCarton.IsReadOnly = false;
                
                schema.Columns.Add(colvarLastCarton);
                
                TableSchema.TableColumn colvarJackets = new TableSchema.TableColumn(schema);
                colvarJackets.ColumnName = "Jackets";
                colvarJackets.DataType = DbType.Int32;
                colvarJackets.MaxLength = 0;
                colvarJackets.AutoIncrement = false;
                colvarJackets.IsNullable = true;
                colvarJackets.IsPrimaryKey = false;
                colvarJackets.IsForeignKey = false;
                colvarJackets.IsReadOnly = false;
                
                schema.Columns.Add(colvarJackets);
                
                TableSchema.TableColumn colvarFullPallets = new TableSchema.TableColumn(schema);
                colvarFullPallets.ColumnName = "FullPallets";
                colvarFullPallets.DataType = DbType.Int32;
                colvarFullPallets.MaxLength = 0;
                colvarFullPallets.AutoIncrement = false;
                colvarFullPallets.IsNullable = true;
                colvarFullPallets.IsPrimaryKey = false;
                colvarFullPallets.IsForeignKey = false;
                colvarFullPallets.IsReadOnly = false;
                
                schema.Columns.Add(colvarFullPallets);
                
                TableSchema.TableColumn colvarCartonsPerFullPallet = new TableSchema.TableColumn(schema);
                colvarCartonsPerFullPallet.ColumnName = "CartonsPerFullPallet";
                colvarCartonsPerFullPallet.DataType = DbType.Int32;
                colvarCartonsPerFullPallet.MaxLength = 0;
                colvarCartonsPerFullPallet.AutoIncrement = false;
                colvarCartonsPerFullPallet.IsNullable = true;
                colvarCartonsPerFullPallet.IsPrimaryKey = false;
                colvarCartonsPerFullPallet.IsForeignKey = false;
                colvarCartonsPerFullPallet.IsReadOnly = false;
                
                schema.Columns.Add(colvarCartonsPerFullPallet);
                
                TableSchema.TableColumn colvarPartPallets = new TableSchema.TableColumn(schema);
                colvarPartPallets.ColumnName = "PartPallets";
                colvarPartPallets.DataType = DbType.Int32;
                colvarPartPallets.MaxLength = 0;
                colvarPartPallets.AutoIncrement = false;
                colvarPartPallets.IsNullable = true;
                colvarPartPallets.IsPrimaryKey = false;
                colvarPartPallets.IsForeignKey = false;
                colvarPartPallets.IsReadOnly = false;
                
                schema.Columns.Add(colvarPartPallets);
                
                TableSchema.TableColumn colvarCartonsPerPartPallet = new TableSchema.TableColumn(schema);
                colvarCartonsPerPartPallet.ColumnName = "CartonsPerPartPallet";
                colvarCartonsPerPartPallet.DataType = DbType.Int32;
                colvarCartonsPerPartPallet.MaxLength = 0;
                colvarCartonsPerPartPallet.AutoIncrement = false;
                colvarCartonsPerPartPallet.IsNullable = true;
                colvarCartonsPerPartPallet.IsPrimaryKey = false;
                colvarCartonsPerPartPallet.IsForeignKey = false;
                colvarCartonsPerPartPallet.IsReadOnly = false;
                
                schema.Columns.Add(colvarCartonsPerPartPallet);
                
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
                
                TableSchema.TableColumn colvarStatusDate = new TableSchema.TableColumn(schema);
                colvarStatusDate.ColumnName = "StatusDate";
                colvarStatusDate.DataType = DbType.DateTime;
                colvarStatusDate.MaxLength = 0;
                colvarStatusDate.AutoIncrement = false;
                colvarStatusDate.IsNullable = true;
                colvarStatusDate.IsPrimaryKey = false;
                colvarStatusDate.IsForeignKey = false;
                colvarStatusDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarStatusDate);
                
                TableSchema.TableColumn colvarCurrentStatusID = new TableSchema.TableColumn(schema);
                colvarCurrentStatusID.ColumnName = "CurrentStatusID";
                colvarCurrentStatusID.DataType = DbType.Int32;
                colvarCurrentStatusID.MaxLength = 0;
                colvarCurrentStatusID.AutoIncrement = false;
                colvarCurrentStatusID.IsNullable = true;
                colvarCurrentStatusID.IsPrimaryKey = false;
                colvarCurrentStatusID.IsForeignKey = false;
                colvarCurrentStatusID.IsReadOnly = false;
                
                schema.Columns.Add(colvarCurrentStatusID);
                
                TableSchema.TableColumn colvarCurrentStatusDate = new TableSchema.TableColumn(schema);
                colvarCurrentStatusDate.ColumnName = "CurrentStatusDate";
                colvarCurrentStatusDate.DataType = DbType.DateTime;
                colvarCurrentStatusDate.MaxLength = 0;
                colvarCurrentStatusDate.AutoIncrement = false;
                colvarCurrentStatusDate.IsNullable = true;
                colvarCurrentStatusDate.IsPrimaryKey = false;
                colvarCurrentStatusDate.IsForeignKey = false;
                colvarCurrentStatusDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarCurrentStatusDate);
                
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
                
                TableSchema.TableColumn colvarField1 = new TableSchema.TableColumn(schema);
                colvarField1.ColumnName = "Field1";
                colvarField1.DataType = DbType.String;
                colvarField1.MaxLength = 50;
                colvarField1.AutoIncrement = false;
                colvarField1.IsNullable = false;
                colvarField1.IsPrimaryKey = false;
                colvarField1.IsForeignKey = false;
                colvarField1.IsReadOnly = false;
                
                schema.Columns.Add(colvarField1);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("InfopathDeliveryFormView",schema);
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
	    public InfopathDeliveryFormView()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public InfopathDeliveryFormView(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public InfopathDeliveryFormView(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public InfopathDeliveryFormView(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
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
	      
        [XmlAttribute("DeliveryID")]
        [Bindable(true)]
        public int DeliveryID 
	    {
		    get
		    {
			    return GetColumnValue<int>("DeliveryID");
		    }
            set 
		    {
			    SetColumnValue("DeliveryID", value);
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
	      
        [XmlAttribute("Title")]
        [Bindable(true)]
        public string Title 
	    {
		    get
		    {
			    return GetColumnValue<string>("Title");
		    }
            set 
		    {
			    SetColumnValue("Title", value);
            }
        }
	      
        [XmlAttribute("TitleID")]
        [Bindable(true)]
        public int? TitleID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("TitleID");
		    }
            set 
		    {
			    SetColumnValue("TitleID", value);
            }
        }
	      
        [XmlAttribute("Copies")]
        [Bindable(true)]
        public int? Copies 
	    {
		    get
		    {
			    return GetColumnValue<int?>("Copies");
		    }
            set 
		    {
			    SetColumnValue("Copies", value);
            }
        }
	      
        [XmlAttribute("CopiesPerCarton")]
        [Bindable(true)]
        public int? CopiesPerCarton 
	    {
		    get
		    {
			    return GetColumnValue<int?>("CopiesPerCarton");
		    }
            set 
		    {
			    SetColumnValue("CopiesPerCarton", value);
            }
        }
	      
        [XmlAttribute("TotalConsignmentWeight")]
        [Bindable(true)]
        public float? TotalConsignmentWeight 
	    {
		    get
		    {
			    return GetColumnValue<float?>("TotalConsignmentWeight");
		    }
            set 
		    {
			    SetColumnValue("TotalConsignmentWeight", value);
            }
        }
	      
        [XmlAttribute("TotalConsignmentCube")]
        [Bindable(true)]
        public float? TotalConsignmentCube 
	    {
		    get
		    {
			    return GetColumnValue<float?>("TotalConsignmentCube");
		    }
            set 
		    {
			    SetColumnValue("TotalConsignmentCube", value);
            }
        }
	      
        [XmlAttribute("TotalCartons")]
        [Bindable(true)]
        public int? TotalCartons 
	    {
		    get
		    {
			    return GetColumnValue<int?>("TotalCartons");
		    }
            set 
		    {
			    SetColumnValue("TotalCartons", value);
            }
        }
	      
        [XmlAttribute("CartonWeight")]
        [Bindable(true)]
        public float? CartonWeight 
	    {
		    get
		    {
			    return GetColumnValue<float?>("CartonWeight");
		    }
            set 
		    {
			    SetColumnValue("CartonWeight", value);
            }
        }
	      
        [XmlAttribute("LastCarton")]
        [Bindable(true)]
        public float? LastCarton 
	    {
		    get
		    {
			    return GetColumnValue<float?>("LastCarton");
		    }
            set 
		    {
			    SetColumnValue("LastCarton", value);
            }
        }
	      
        [XmlAttribute("Jackets")]
        [Bindable(true)]
        public int? Jackets 
	    {
		    get
		    {
			    return GetColumnValue<int?>("Jackets");
		    }
            set 
		    {
			    SetColumnValue("Jackets", value);
            }
        }
	      
        [XmlAttribute("FullPallets")]
        [Bindable(true)]
        public int? FullPallets 
	    {
		    get
		    {
			    return GetColumnValue<int?>("FullPallets");
		    }
            set 
		    {
			    SetColumnValue("FullPallets", value);
            }
        }
	      
        [XmlAttribute("CartonsPerFullPallet")]
        [Bindable(true)]
        public int? CartonsPerFullPallet 
	    {
		    get
		    {
			    return GetColumnValue<int?>("CartonsPerFullPallet");
		    }
            set 
		    {
			    SetColumnValue("CartonsPerFullPallet", value);
            }
        }
	      
        [XmlAttribute("PartPallets")]
        [Bindable(true)]
        public int? PartPallets 
	    {
		    get
		    {
			    return GetColumnValue<int?>("PartPallets");
		    }
            set 
		    {
			    SetColumnValue("PartPallets", value);
            }
        }
	      
        [XmlAttribute("CartonsPerPartPallet")]
        [Bindable(true)]
        public int? CartonsPerPartPallet 
	    {
		    get
		    {
			    return GetColumnValue<int?>("CartonsPerPartPallet");
		    }
            set 
		    {
			    SetColumnValue("CartonsPerPartPallet", value);
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
	      
        [XmlAttribute("StatusDate")]
        [Bindable(true)]
        public DateTime? StatusDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("StatusDate");
		    }
            set 
		    {
			    SetColumnValue("StatusDate", value);
            }
        }
	      
        [XmlAttribute("CurrentStatusID")]
        [Bindable(true)]
        public int? CurrentStatusID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("CurrentStatusID");
		    }
            set 
		    {
			    SetColumnValue("CurrentStatusID", value);
            }
        }
	      
        [XmlAttribute("CurrentStatusDate")]
        [Bindable(true)]
        public DateTime? CurrentStatusDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("CurrentStatusDate");
		    }
            set 
		    {
			    SetColumnValue("CurrentStatusDate", value);
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
	      
        [XmlAttribute("Field1")]
        [Bindable(true)]
        public string Field1 
	    {
		    get
		    {
			    return GetColumnValue<string>("Field1");
		    }
            set 
		    {
			    SetColumnValue("Field1", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string Id = @"ID";
            
            public static string DeliveryID = @"DeliveryID";
            
            public static string CompanyName = @"CompanyName";
            
            public static string Title = @"Title";
            
            public static string TitleID = @"TitleID";
            
            public static string Copies = @"Copies";
            
            public static string CopiesPerCarton = @"CopiesPerCarton";
            
            public static string TotalConsignmentWeight = @"TotalConsignmentWeight";
            
            public static string TotalConsignmentCube = @"TotalConsignmentCube";
            
            public static string TotalCartons = @"TotalCartons";
            
            public static string CartonWeight = @"CartonWeight";
            
            public static string LastCarton = @"LastCarton";
            
            public static string Jackets = @"Jackets";
            
            public static string FullPallets = @"FullPallets";
            
            public static string CartonsPerFullPallet = @"CartonsPerFullPallet";
            
            public static string PartPallets = @"PartPallets";
            
            public static string CartonsPerPartPallet = @"CartonsPerPartPallet";
            
            public static string OrderNumber = @"OrderNumber";
            
            public static string StatusDate = @"StatusDate";
            
            public static string CurrentStatusID = @"CurrentStatusID";
            
            public static string CurrentStatusDate = @"CurrentStatusDate";
            
            public static string DeliveryAddress = @"DeliveryAddress";
            
            public static string Field1 = @"Field1";
            
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
