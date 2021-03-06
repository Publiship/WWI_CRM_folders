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
    /// Strongly-typed collection for the ViewDeliveryInstruction class.
    /// </summary>
    [Serializable]
    public partial class ViewDeliveryInstructionCollection : ReadOnlyList<ViewDeliveryInstruction, ViewDeliveryInstructionCollection>
    {        
        public ViewDeliveryInstructionCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the view_delivery_instruction view.
    /// </summary>
    [Serializable]
    public partial class ViewDeliveryInstruction : ReadOnlyRecord<ViewDeliveryInstruction>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("view_delivery_instruction", TableType.View, DataService.GetInstance("WWIprov"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarDeliveryID = new TableSchema.TableColumn(schema);
                colvarDeliveryID.ColumnName = "DeliveryID";
                colvarDeliveryID.DataType = DbType.Int32;
                colvarDeliveryID.MaxLength = 0;
                colvarDeliveryID.AutoIncrement = false;
                colvarDeliveryID.IsNullable = true;
                colvarDeliveryID.IsPrimaryKey = false;
                colvarDeliveryID.IsForeignKey = false;
                colvarDeliveryID.IsReadOnly = false;
                
                schema.Columns.Add(colvarDeliveryID);
                
                TableSchema.TableColumn colvarDeliveryNoteID = new TableSchema.TableColumn(schema);
                colvarDeliveryNoteID.ColumnName = "DeliveryNoteID";
                colvarDeliveryNoteID.DataType = DbType.Int32;
                colvarDeliveryNoteID.MaxLength = 0;
                colvarDeliveryNoteID.AutoIncrement = false;
                colvarDeliveryNoteID.IsNullable = true;
                colvarDeliveryNoteID.IsPrimaryKey = false;
                colvarDeliveryNoteID.IsForeignKey = false;
                colvarDeliveryNoteID.IsReadOnly = false;
                
                schema.Columns.Add(colvarDeliveryNoteID);
                
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
                
                TableSchema.TableColumn colvarPODRequired = new TableSchema.TableColumn(schema);
                colvarPODRequired.ColumnName = "PODRequired";
                colvarPODRequired.DataType = DbType.Boolean;
                colvarPODRequired.MaxLength = 0;
                colvarPODRequired.AutoIncrement = false;
                colvarPODRequired.IsNullable = true;
                colvarPODRequired.IsPrimaryKey = false;
                colvarPODRequired.IsForeignKey = false;
                colvarPODRequired.IsReadOnly = false;
                
                schema.Columns.Add(colvarPODRequired);
                
                TableSchema.TableColumn colvarPODurl = new TableSchema.TableColumn(schema);
                colvarPODurl.ColumnName = "PODurl";
                colvarPODurl.DataType = DbType.String;
                colvarPODurl.MaxLength = 1073741823;
                colvarPODurl.AutoIncrement = false;
                colvarPODurl.IsNullable = true;
                colvarPODurl.IsPrimaryKey = false;
                colvarPODurl.IsForeignKey = false;
                colvarPODurl.IsReadOnly = false;
                
                schema.Columns.Add(colvarPODurl);
                
                TableSchema.TableColumn colvarAdded = new TableSchema.TableColumn(schema);
                colvarAdded.ColumnName = "Added";
                colvarAdded.DataType = DbType.Boolean;
                colvarAdded.MaxLength = 0;
                colvarAdded.AutoIncrement = false;
                colvarAdded.IsNullable = true;
                colvarAdded.IsPrimaryKey = false;
                colvarAdded.IsForeignKey = false;
                colvarAdded.IsReadOnly = false;
                
                schema.Columns.Add(colvarAdded);
                
                TableSchema.TableColumn colvarCompanyID = new TableSchema.TableColumn(schema);
                colvarCompanyID.ColumnName = "CompanyID";
                colvarCompanyID.DataType = DbType.Int32;
                colvarCompanyID.MaxLength = 0;
                colvarCompanyID.AutoIncrement = false;
                colvarCompanyID.IsNullable = true;
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
                
                TableSchema.TableColumn colvarPostCode = new TableSchema.TableColumn(schema);
                colvarPostCode.ColumnName = "PostCode";
                colvarPostCode.DataType = DbType.String;
                colvarPostCode.MaxLength = 50;
                colvarPostCode.AutoIncrement = false;
                colvarPostCode.IsNullable = true;
                colvarPostCode.IsPrimaryKey = false;
                colvarPostCode.IsForeignKey = false;
                colvarPostCode.IsReadOnly = false;
                
                schema.Columns.Add(colvarPostCode);
                
                TableSchema.TableColumn colvarCountryID = new TableSchema.TableColumn(schema);
                colvarCountryID.ColumnName = "CountryID";
                colvarCountryID.DataType = DbType.Int32;
                colvarCountryID.MaxLength = 0;
                colvarCountryID.AutoIncrement = false;
                colvarCountryID.IsNullable = true;
                colvarCountryID.IsPrimaryKey = false;
                colvarCountryID.IsForeignKey = false;
                colvarCountryID.IsReadOnly = false;
                
                schema.Columns.Add(colvarCountryID);
                
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
                
                TableSchema.TableColumn colvarPalletDims = new TableSchema.TableColumn(schema);
                colvarPalletDims.ColumnName = "PalletDims";
                colvarPalletDims.DataType = DbType.String;
                colvarPalletDims.MaxLength = 50;
                colvarPalletDims.AutoIncrement = false;
                colvarPalletDims.IsNullable = true;
                colvarPalletDims.IsPrimaryKey = false;
                colvarPalletDims.IsForeignKey = false;
                colvarPalletDims.IsReadOnly = false;
                
                schema.Columns.Add(colvarPalletDims);
                
                TableSchema.TableColumn colvarMaxPalletWeight = new TableSchema.TableColumn(schema);
                colvarMaxPalletWeight.ColumnName = "MaxPalletWeight";
                colvarMaxPalletWeight.DataType = DbType.Int32;
                colvarMaxPalletWeight.MaxLength = 0;
                colvarMaxPalletWeight.AutoIncrement = false;
                colvarMaxPalletWeight.IsNullable = true;
                colvarMaxPalletWeight.IsPrimaryKey = false;
                colvarMaxPalletWeight.IsForeignKey = false;
                colvarMaxPalletWeight.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaxPalletWeight);
                
                TableSchema.TableColumn colvarMaxPalletHeight = new TableSchema.TableColumn(schema);
                colvarMaxPalletHeight.ColumnName = "MaxPalletHeight";
                colvarMaxPalletHeight.DataType = DbType.Int32;
                colvarMaxPalletHeight.MaxLength = 0;
                colvarMaxPalletHeight.AutoIncrement = false;
                colvarMaxPalletHeight.IsNullable = true;
                colvarMaxPalletHeight.IsPrimaryKey = false;
                colvarMaxPalletHeight.IsForeignKey = false;
                colvarMaxPalletHeight.IsReadOnly = false;
                
                schema.Columns.Add(colvarMaxPalletHeight);
                
                TableSchema.TableColumn colvarSpecialDeliveryInstructions = new TableSchema.TableColumn(schema);
                colvarSpecialDeliveryInstructions.ColumnName = "SpecialDeliveryInstructions";
                colvarSpecialDeliveryInstructions.DataType = DbType.String;
                colvarSpecialDeliveryInstructions.MaxLength = 50;
                colvarSpecialDeliveryInstructions.AutoIncrement = false;
                colvarSpecialDeliveryInstructions.IsNullable = true;
                colvarSpecialDeliveryInstructions.IsPrimaryKey = false;
                colvarSpecialDeliveryInstructions.IsForeignKey = false;
                colvarSpecialDeliveryInstructions.IsReadOnly = false;
                
                schema.Columns.Add(colvarSpecialDeliveryInstructions);
                
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
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("view_delivery_instruction",schema);
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
	    public ViewDeliveryInstruction()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public ViewDeliveryInstruction(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public ViewDeliveryInstruction(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public ViewDeliveryInstruction(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("DeliveryID")]
        [Bindable(true)]
        public int? DeliveryID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("DeliveryID");
		    }
            set 
		    {
			    SetColumnValue("DeliveryID", value);
            }
        }
	      
        [XmlAttribute("DeliveryNoteID")]
        [Bindable(true)]
        public int? DeliveryNoteID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("DeliveryNoteID");
		    }
            set 
		    {
			    SetColumnValue("DeliveryNoteID", value);
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
	      
        [XmlAttribute("PODRequired")]
        [Bindable(true)]
        public bool? PODRequired 
	    {
		    get
		    {
			    return GetColumnValue<bool?>("PODRequired");
		    }
            set 
		    {
			    SetColumnValue("PODRequired", value);
            }
        }
	      
        [XmlAttribute("PODurl")]
        [Bindable(true)]
        public string PODurl 
	    {
		    get
		    {
			    return GetColumnValue<string>("PODurl");
		    }
            set 
		    {
			    SetColumnValue("PODurl", value);
            }
        }
	      
        [XmlAttribute("Added")]
        [Bindable(true)]
        public bool? Added 
	    {
		    get
		    {
			    return GetColumnValue<bool?>("Added");
		    }
            set 
		    {
			    SetColumnValue("Added", value);
            }
        }
	      
        [XmlAttribute("CompanyID")]
        [Bindable(true)]
        public int? CompanyID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("CompanyID");
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
	      
        [XmlAttribute("PostCode")]
        [Bindable(true)]
        public string PostCode 
	    {
		    get
		    {
			    return GetColumnValue<string>("PostCode");
		    }
            set 
		    {
			    SetColumnValue("PostCode", value);
            }
        }
	      
        [XmlAttribute("CountryID")]
        [Bindable(true)]
        public int? CountryID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("CountryID");
		    }
            set 
		    {
			    SetColumnValue("CountryID", value);
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
	      
        [XmlAttribute("PalletDims")]
        [Bindable(true)]
        public string PalletDims 
	    {
		    get
		    {
			    return GetColumnValue<string>("PalletDims");
		    }
            set 
		    {
			    SetColumnValue("PalletDims", value);
            }
        }
	      
        [XmlAttribute("MaxPalletWeight")]
        [Bindable(true)]
        public int? MaxPalletWeight 
	    {
		    get
		    {
			    return GetColumnValue<int?>("MaxPalletWeight");
		    }
            set 
		    {
			    SetColumnValue("MaxPalletWeight", value);
            }
        }
	      
        [XmlAttribute("MaxPalletHeight")]
        [Bindable(true)]
        public int? MaxPalletHeight 
	    {
		    get
		    {
			    return GetColumnValue<int?>("MaxPalletHeight");
		    }
            set 
		    {
			    SetColumnValue("MaxPalletHeight", value);
            }
        }
	      
        [XmlAttribute("SpecialDeliveryInstructions")]
        [Bindable(true)]
        public string SpecialDeliveryInstructions 
	    {
		    get
		    {
			    return GetColumnValue<string>("SpecialDeliveryInstructions");
		    }
            set 
		    {
			    SetColumnValue("SpecialDeliveryInstructions", value);
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
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string DeliveryID = @"DeliveryID";
            
            public static string DeliveryNoteID = @"DeliveryNoteID";
            
            public static string OrderNumber = @"OrderNumber";
            
            public static string StatusDate = @"StatusDate";
            
            public static string CurrentStatusID = @"CurrentStatusID";
            
            public static string CurrentStatusDate = @"CurrentStatusDate";
            
            public static string DeliveryAddress = @"DeliveryAddress";
            
            public static string Delivered = @"Delivered";
            
            public static string PODRequired = @"PODRequired";
            
            public static string PODurl = @"PODurl";
            
            public static string Added = @"Added";
            
            public static string CompanyID = @"CompanyID";
            
            public static string CompanyName = @"CompanyName";
            
            public static string Address1 = @"Address1";
            
            public static string Address2 = @"Address2";
            
            public static string Address3 = @"Address3";
            
            public static string PostCode = @"PostCode";
            
            public static string CountryID = @"CountryID";
            
            public static string TelNo = @"TelNo";
            
            public static string PalletDims = @"PalletDims";
            
            public static string MaxPalletWeight = @"MaxPalletWeight";
            
            public static string MaxPalletHeight = @"MaxPalletHeight";
            
            public static string SpecialDeliveryInstructions = @"SpecialDeliveryInstructions";
            
            public static string CountryName = @"CountryName";
            
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
