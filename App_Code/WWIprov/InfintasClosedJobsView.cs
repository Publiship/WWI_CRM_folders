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
    /// Strongly-typed collection for the InfintasClosedJobsView class.
    /// </summary>
    [Serializable]
    public partial class InfintasClosedJobsViewCollection : ReadOnlyList<InfintasClosedJobsView, InfintasClosedJobsViewCollection>
    {        
        public InfintasClosedJobsViewCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the InfintasClosedJobsView view.
    /// </summary>
    [Serializable]
    public partial class InfintasClosedJobsView : ReadOnlyRecord<InfintasClosedJobsView>, IReadOnlyRecord
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
                TableSchema.Table schema = new TableSchema.Table("InfintasClosedJobsView", TableType.View, DataService.GetInstance("WWIprov"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarIsbn = new TableSchema.TableColumn(schema);
                colvarIsbn.ColumnName = "ISBN";
                colvarIsbn.DataType = DbType.String;
                colvarIsbn.MaxLength = 50;
                colvarIsbn.AutoIncrement = false;
                colvarIsbn.IsNullable = true;
                colvarIsbn.IsPrimaryKey = false;
                colvarIsbn.IsForeignKey = false;
                colvarIsbn.IsReadOnly = false;
                
                schema.Columns.Add(colvarIsbn);
                
                TableSchema.TableColumn colvarCustomersRef = new TableSchema.TableColumn(schema);
                colvarCustomersRef.ColumnName = "CustomersRef";
                colvarCustomersRef.DataType = DbType.String;
                colvarCustomersRef.MaxLength = 20;
                colvarCustomersRef.AutoIncrement = false;
                colvarCustomersRef.IsNullable = true;
                colvarCustomersRef.IsPrimaryKey = false;
                colvarCustomersRef.IsForeignKey = false;
                colvarCustomersRef.IsReadOnly = false;
                
                schema.Columns.Add(colvarCustomersRef);
                
                TableSchema.TableColumn colvarPrinter = new TableSchema.TableColumn(schema);
                colvarPrinter.ColumnName = "Printer";
                colvarPrinter.DataType = DbType.String;
                colvarPrinter.MaxLength = 50;
                colvarPrinter.AutoIncrement = false;
                colvarPrinter.IsNullable = true;
                colvarPrinter.IsPrimaryKey = false;
                colvarPrinter.IsForeignKey = false;
                colvarPrinter.IsReadOnly = false;
                
                schema.Columns.Add(colvarPrinter);
                
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
                
                TableSchema.TableColumn colvarTitle = new TableSchema.TableColumn(schema);
                colvarTitle.ColumnName = "Title";
                colvarTitle.DataType = DbType.String;
                colvarTitle.MaxLength = 100;
                colvarTitle.AutoIncrement = false;
                colvarTitle.IsNullable = true;
                colvarTitle.IsPrimaryKey = false;
                colvarTitle.IsForeignKey = false;
                colvarTitle.IsReadOnly = false;
                
                schema.Columns.Add(colvarTitle);
                
                TableSchema.TableColumn colvarExWorksDate = new TableSchema.TableColumn(schema);
                colvarExWorksDate.ColumnName = "ExWorksDate";
                colvarExWorksDate.DataType = DbType.DateTime;
                colvarExWorksDate.MaxLength = 0;
                colvarExWorksDate.AutoIncrement = false;
                colvarExWorksDate.IsNullable = true;
                colvarExWorksDate.IsPrimaryKey = false;
                colvarExWorksDate.IsForeignKey = false;
                colvarExWorksDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarExWorksDate);
                
                TableSchema.TableColumn colvarSailingDate = new TableSchema.TableColumn(schema);
                colvarSailingDate.ColumnName = "SailingDate";
                colvarSailingDate.DataType = DbType.DateTime;
                colvarSailingDate.MaxLength = 0;
                colvarSailingDate.AutoIncrement = false;
                colvarSailingDate.IsNullable = true;
                colvarSailingDate.IsPrimaryKey = false;
                colvarSailingDate.IsForeignKey = false;
                colvarSailingDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarSailingDate);
                
                TableSchema.TableColumn colvarEtaPOrtDestination = new TableSchema.TableColumn(schema);
                colvarEtaPOrtDestination.ColumnName = "ETA POrt Destination";
                colvarEtaPOrtDestination.DataType = DbType.DateTime;
                colvarEtaPOrtDestination.MaxLength = 0;
                colvarEtaPOrtDestination.AutoIncrement = false;
                colvarEtaPOrtDestination.IsNullable = true;
                colvarEtaPOrtDestination.IsPrimaryKey = false;
                colvarEtaPOrtDestination.IsForeignKey = false;
                colvarEtaPOrtDestination.IsReadOnly = false;
                
                schema.Columns.Add(colvarEtaPOrtDestination);
                
                TableSchema.TableColumn colvarDueWarehouseDate = new TableSchema.TableColumn(schema);
                colvarDueWarehouseDate.ColumnName = "DueWarehouseDate";
                colvarDueWarehouseDate.DataType = DbType.DateTime;
                colvarDueWarehouseDate.MaxLength = 0;
                colvarDueWarehouseDate.AutoIncrement = false;
                colvarDueWarehouseDate.IsNullable = true;
                colvarDueWarehouseDate.IsPrimaryKey = false;
                colvarDueWarehouseDate.IsForeignKey = false;
                colvarDueWarehouseDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarDueWarehouseDate);
                
                TableSchema.TableColumn colvarCurrentStatus = new TableSchema.TableColumn(schema);
                colvarCurrentStatus.ColumnName = "CurrentStatus";
                colvarCurrentStatus.DataType = DbType.String;
                colvarCurrentStatus.MaxLength = 50;
                colvarCurrentStatus.AutoIncrement = false;
                colvarCurrentStatus.IsNullable = true;
                colvarCurrentStatus.IsPrimaryKey = false;
                colvarCurrentStatus.IsForeignKey = false;
                colvarCurrentStatus.IsReadOnly = false;
                
                schema.Columns.Add(colvarCurrentStatus);
                
                TableSchema.TableColumn colvarVessel = new TableSchema.TableColumn(schema);
                colvarVessel.ColumnName = "Vessel";
                colvarVessel.DataType = DbType.String;
                colvarVessel.MaxLength = 50;
                colvarVessel.AutoIncrement = false;
                colvarVessel.IsNullable = true;
                colvarVessel.IsPrimaryKey = false;
                colvarVessel.IsForeignKey = false;
                colvarVessel.IsReadOnly = false;
                
                schema.Columns.Add(colvarVessel);
                
                TableSchema.TableColumn colvarRemarksToCustomer = new TableSchema.TableColumn(schema);
                colvarRemarksToCustomer.ColumnName = "RemarksToCustomer";
                colvarRemarksToCustomer.DataType = DbType.String;
                colvarRemarksToCustomer.MaxLength = 1073741823;
                colvarRemarksToCustomer.AutoIncrement = false;
                colvarRemarksToCustomer.IsNullable = true;
                colvarRemarksToCustomer.IsPrimaryKey = false;
                colvarRemarksToCustomer.IsForeignKey = false;
                colvarRemarksToCustomer.IsReadOnly = false;
                
                schema.Columns.Add(colvarRemarksToCustomer);
                
                TableSchema.TableColumn colvarPublishipRef = new TableSchema.TableColumn(schema);
                colvarPublishipRef.ColumnName = "PublishipRef";
                colvarPublishipRef.DataType = DbType.Int32;
                colvarPublishipRef.MaxLength = 0;
                colvarPublishipRef.AutoIncrement = false;
                colvarPublishipRef.IsNullable = true;
                colvarPublishipRef.IsPrimaryKey = false;
                colvarPublishipRef.IsForeignKey = false;
                colvarPublishipRef.IsReadOnly = false;
                
                schema.Columns.Add(colvarPublishipRef);
                
                TableSchema.TableColumn colvarClient = new TableSchema.TableColumn(schema);
                colvarClient.ColumnName = "Client";
                colvarClient.DataType = DbType.String;
                colvarClient.MaxLength = 50;
                colvarClient.AutoIncrement = false;
                colvarClient.IsNullable = true;
                colvarClient.IsPrimaryKey = false;
                colvarClient.IsForeignKey = false;
                colvarClient.IsReadOnly = false;
                
                schema.Columns.Add(colvarClient);
                
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
                
                TableSchema.TableColumn colvarFinalDestination = new TableSchema.TableColumn(schema);
                colvarFinalDestination.ColumnName = "FinalDestination";
                colvarFinalDestination.DataType = DbType.String;
                colvarFinalDestination.MaxLength = 30;
                colvarFinalDestination.AutoIncrement = false;
                colvarFinalDestination.IsNullable = true;
                colvarFinalDestination.IsPrimaryKey = false;
                colvarFinalDestination.IsForeignKey = false;
                colvarFinalDestination.IsReadOnly = false;
                
                schema.Columns.Add(colvarFinalDestination);
                
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
                
                TableSchema.TableColumn colvarJobClosed = new TableSchema.TableColumn(schema);
                colvarJobClosed.ColumnName = "JobClosed";
                colvarJobClosed.DataType = DbType.Boolean;
                colvarJobClosed.MaxLength = 0;
                colvarJobClosed.AutoIncrement = false;
                colvarJobClosed.IsNullable = false;
                colvarJobClosed.IsPrimaryKey = false;
                colvarJobClosed.IsForeignKey = false;
                colvarJobClosed.IsReadOnly = false;
                
                schema.Columns.Add(colvarJobClosed);
                
                TableSchema.TableColumn colvarRemarks = new TableSchema.TableColumn(schema);
                colvarRemarks.ColumnName = "Remarks";
                colvarRemarks.DataType = DbType.String;
                colvarRemarks.MaxLength = 1073741823;
                colvarRemarks.AutoIncrement = false;
                colvarRemarks.IsNullable = true;
                colvarRemarks.IsPrimaryKey = false;
                colvarRemarks.IsForeignKey = false;
                colvarRemarks.IsReadOnly = false;
                
                schema.Columns.Add(colvarRemarks);
                
                TableSchema.TableColumn colvarPONumber = new TableSchema.TableColumn(schema);
                colvarPONumber.ColumnName = "PONumber";
                colvarPONumber.DataType = DbType.String;
                colvarPONumber.MaxLength = 50;
                colvarPONumber.AutoIncrement = false;
                colvarPONumber.IsNullable = true;
                colvarPONumber.IsPrimaryKey = false;
                colvarPONumber.IsForeignKey = false;
                colvarPONumber.IsReadOnly = false;
                
                schema.Columns.Add(colvarPONumber);
                
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
                
                TableSchema.TableColumn colvarJobClosureDate = new TableSchema.TableColumn(schema);
                colvarJobClosureDate.ColumnName = "JobClosureDate";
                colvarJobClosureDate.DataType = DbType.DateTime;
                colvarJobClosureDate.MaxLength = 0;
                colvarJobClosureDate.AutoIncrement = false;
                colvarJobClosureDate.IsNullable = true;
                colvarJobClosureDate.IsPrimaryKey = false;
                colvarJobClosureDate.IsForeignKey = false;
                colvarJobClosureDate.IsReadOnly = false;
                
                schema.Columns.Add(colvarJobClosureDate);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["WWIprov"].AddSchema("InfintasClosedJobsView",schema);
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
	    public InfintasClosedJobsView()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public InfintasClosedJobsView(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public InfintasClosedJobsView(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public InfintasClosedJobsView(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("Isbn")]
        [Bindable(true)]
        public string Isbn 
	    {
		    get
		    {
			    return GetColumnValue<string>("ISBN");
		    }
            set 
		    {
			    SetColumnValue("ISBN", value);
            }
        }
	      
        [XmlAttribute("CustomersRef")]
        [Bindable(true)]
        public string CustomersRef 
	    {
		    get
		    {
			    return GetColumnValue<string>("CustomersRef");
		    }
            set 
		    {
			    SetColumnValue("CustomersRef", value);
            }
        }
	      
        [XmlAttribute("Printer")]
        [Bindable(true)]
        public string Printer 
	    {
		    get
		    {
			    return GetColumnValue<string>("Printer");
		    }
            set 
		    {
			    SetColumnValue("Printer", value);
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
	      
        [XmlAttribute("ExWorksDate")]
        [Bindable(true)]
        public DateTime? ExWorksDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("ExWorksDate");
		    }
            set 
		    {
			    SetColumnValue("ExWorksDate", value);
            }
        }
	      
        [XmlAttribute("SailingDate")]
        [Bindable(true)]
        public DateTime? SailingDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("SailingDate");
		    }
            set 
		    {
			    SetColumnValue("SailingDate", value);
            }
        }
	      
        [XmlAttribute("EtaPOrtDestination")]
        [Bindable(true)]
        public DateTime? EtaPOrtDestination 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("ETA POrt Destination");
		    }
            set 
		    {
			    SetColumnValue("ETA POrt Destination", value);
            }
        }
	      
        [XmlAttribute("DueWarehouseDate")]
        [Bindable(true)]
        public DateTime? DueWarehouseDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("DueWarehouseDate");
		    }
            set 
		    {
			    SetColumnValue("DueWarehouseDate", value);
            }
        }
	      
        [XmlAttribute("CurrentStatus")]
        [Bindable(true)]
        public string CurrentStatus 
	    {
		    get
		    {
			    return GetColumnValue<string>("CurrentStatus");
		    }
            set 
		    {
			    SetColumnValue("CurrentStatus", value);
            }
        }
	      
        [XmlAttribute("Vessel")]
        [Bindable(true)]
        public string Vessel 
	    {
		    get
		    {
			    return GetColumnValue<string>("Vessel");
		    }
            set 
		    {
			    SetColumnValue("Vessel", value);
            }
        }
	      
        [XmlAttribute("RemarksToCustomer")]
        [Bindable(true)]
        public string RemarksToCustomer 
	    {
		    get
		    {
			    return GetColumnValue<string>("RemarksToCustomer");
		    }
            set 
		    {
			    SetColumnValue("RemarksToCustomer", value);
            }
        }
	      
        [XmlAttribute("PublishipRef")]
        [Bindable(true)]
        public int? PublishipRef 
	    {
		    get
		    {
			    return GetColumnValue<int?>("PublishipRef");
		    }
            set 
		    {
			    SetColumnValue("PublishipRef", value);
            }
        }
	      
        [XmlAttribute("Client")]
        [Bindable(true)]
        public string Client 
	    {
		    get
		    {
			    return GetColumnValue<string>("Client");
		    }
            set 
		    {
			    SetColumnValue("Client", value);
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
	      
        [XmlAttribute("FinalDestination")]
        [Bindable(true)]
        public string FinalDestination 
	    {
		    get
		    {
			    return GetColumnValue<string>("FinalDestination");
		    }
            set 
		    {
			    SetColumnValue("FinalDestination", value);
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
	      
        [XmlAttribute("JobClosed")]
        [Bindable(true)]
        public bool JobClosed 
	    {
		    get
		    {
			    return GetColumnValue<bool>("JobClosed");
		    }
            set 
		    {
			    SetColumnValue("JobClosed", value);
            }
        }
	      
        [XmlAttribute("Remarks")]
        [Bindable(true)]
        public string Remarks 
	    {
		    get
		    {
			    return GetColumnValue<string>("Remarks");
		    }
            set 
		    {
			    SetColumnValue("Remarks", value);
            }
        }
	      
        [XmlAttribute("PONumber")]
        [Bindable(true)]
        public string PONumber 
	    {
		    get
		    {
			    return GetColumnValue<string>("PONumber");
		    }
            set 
		    {
			    SetColumnValue("PONumber", value);
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
	      
        [XmlAttribute("JobClosureDate")]
        [Bindable(true)]
        public DateTime? JobClosureDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("JobClosureDate");
		    }
            set 
		    {
			    SetColumnValue("JobClosureDate", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string Isbn = @"ISBN";
            
            public static string CustomersRef = @"CustomersRef";
            
            public static string Printer = @"Printer";
            
            public static string Copies = @"Copies";
            
            public static string Title = @"Title";
            
            public static string ExWorksDate = @"ExWorksDate";
            
            public static string SailingDate = @"SailingDate";
            
            public static string EtaPOrtDestination = @"ETA POrt Destination";
            
            public static string DueWarehouseDate = @"DueWarehouseDate";
            
            public static string CurrentStatus = @"CurrentStatus";
            
            public static string Vessel = @"Vessel";
            
            public static string RemarksToCustomer = @"RemarksToCustomer";
            
            public static string PublishipRef = @"PublishipRef";
            
            public static string Client = @"Client";
            
            public static string OriginPort = @"OriginPort";
            
            public static string FinalDestination = @"FinalDestination";
            
            public static string CompanyID = @"CompanyID";
            
            public static string JobClosed = @"JobClosed";
            
            public static string Remarks = @"Remarks";
            
            public static string PONumber = @"PONumber";
            
            public static string CurrentStatusDate = @"CurrentStatusDate";
            
            public static string JobClosureDate = @"JobClosureDate";
            
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