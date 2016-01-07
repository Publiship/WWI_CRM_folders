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
namespace DAL.Logistics
{
	/// <summary>
	/// Strongly-typed collection for the ContainerTable class.
	/// </summary>
    [Serializable]
	public partial class ContainerTableCollection : ActiveList<ContainerTable, ContainerTableCollection>
	{	   
		public ContainerTableCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>ContainerTableCollection</returns>
		public ContainerTableCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                ContainerTable o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the ContainerTable table.
	/// </summary>
	[Serializable]
	public partial class ContainerTable : ActiveRecord<ContainerTable>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public ContainerTable()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public ContainerTable(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public ContainerTable(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public ContainerTable(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("ContainerTable", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarContainerID = new TableSchema.TableColumn(schema);
				colvarContainerID.ColumnName = "ContainerID";
				colvarContainerID.DataType = DbType.Int32;
				colvarContainerID.MaxLength = 0;
				colvarContainerID.AutoIncrement = true;
				colvarContainerID.IsNullable = false;
				colvarContainerID.IsPrimaryKey = true;
				colvarContainerID.IsForeignKey = false;
				colvarContainerID.IsReadOnly = false;
				colvarContainerID.DefaultSetting = @"";
				colvarContainerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContainerID);
				
				TableSchema.TableColumn colvarContainerNumber = new TableSchema.TableColumn(schema);
				colvarContainerNumber.ColumnName = "ContainerNumber";
				colvarContainerNumber.DataType = DbType.String;
				colvarContainerNumber.MaxLength = 50;
				colvarContainerNumber.AutoIncrement = false;
				colvarContainerNumber.IsNullable = true;
				colvarContainerNumber.IsPrimaryKey = false;
				colvarContainerNumber.IsForeignKey = false;
				colvarContainerNumber.IsReadOnly = false;
				colvarContainerNumber.DefaultSetting = @"";
				colvarContainerNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContainerNumber);
				
				TableSchema.TableColumn colvarSizeTypeID = new TableSchema.TableColumn(schema);
				colvarSizeTypeID.ColumnName = "SizeTypeID";
				colvarSizeTypeID.DataType = DbType.Int32;
				colvarSizeTypeID.MaxLength = 0;
				colvarSizeTypeID.AutoIncrement = false;
				colvarSizeTypeID.IsNullable = true;
				colvarSizeTypeID.IsPrimaryKey = false;
				colvarSizeTypeID.IsForeignKey = false;
				colvarSizeTypeID.IsReadOnly = false;
				colvarSizeTypeID.DefaultSetting = @"";
				colvarSizeTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSizeTypeID);
				
				TableSchema.TableColumn colvarVoyageID = new TableSchema.TableColumn(schema);
				colvarVoyageID.ColumnName = "VoyageID";
				colvarVoyageID.DataType = DbType.Int32;
				colvarVoyageID.MaxLength = 0;
				colvarVoyageID.AutoIncrement = false;
				colvarVoyageID.IsNullable = true;
				colvarVoyageID.IsPrimaryKey = false;
				colvarVoyageID.IsForeignKey = false;
				colvarVoyageID.IsReadOnly = false;
				colvarVoyageID.DefaultSetting = @"";
				colvarVoyageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVoyageID);
				
				TableSchema.TableColumn colvarOriginPortID = new TableSchema.TableColumn(schema);
				colvarOriginPortID.ColumnName = "OriginPortID";
				colvarOriginPortID.DataType = DbType.Int32;
				colvarOriginPortID.MaxLength = 0;
				colvarOriginPortID.AutoIncrement = false;
				colvarOriginPortID.IsNullable = true;
				colvarOriginPortID.IsPrimaryKey = false;
				colvarOriginPortID.IsForeignKey = false;
				colvarOriginPortID.IsReadOnly = false;
				colvarOriginPortID.DefaultSetting = @"";
				colvarOriginPortID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOriginPortID);
				
				TableSchema.TableColumn colvarDestinationPortID = new TableSchema.TableColumn(schema);
				colvarDestinationPortID.ColumnName = "DestinationPortID";
				colvarDestinationPortID.DataType = DbType.Int32;
				colvarDestinationPortID.MaxLength = 0;
				colvarDestinationPortID.AutoIncrement = false;
				colvarDestinationPortID.IsNullable = true;
				colvarDestinationPortID.IsPrimaryKey = false;
				colvarDestinationPortID.IsForeignKey = false;
				colvarDestinationPortID.IsReadOnly = false;
				colvarDestinationPortID.DefaultSetting = @"";
				colvarDestinationPortID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDestinationPortID);
				
				TableSchema.TableColumn colvarCycfs = new TableSchema.TableColumn(schema);
				colvarCycfs.ColumnName = "CYCFS";
				colvarCycfs.DataType = DbType.Int32;
				colvarCycfs.MaxLength = 0;
				colvarCycfs.AutoIncrement = false;
				colvarCycfs.IsNullable = true;
				colvarCycfs.IsPrimaryKey = false;
				colvarCycfs.IsForeignKey = false;
				colvarCycfs.IsReadOnly = false;
				colvarCycfs.DefaultSetting = @"";
				colvarCycfs.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCycfs);
				
				TableSchema.TableColumn colvarDevanDate = new TableSchema.TableColumn(schema);
				colvarDevanDate.ColumnName = "DevanDate";
				colvarDevanDate.DataType = DbType.DateTime;
				colvarDevanDate.MaxLength = 0;
				colvarDevanDate.AutoIncrement = false;
				colvarDevanDate.IsNullable = true;
				colvarDevanDate.IsPrimaryKey = false;
				colvarDevanDate.IsForeignKey = false;
				colvarDevanDate.IsReadOnly = false;
				colvarDevanDate.DefaultSetting = @"";
				colvarDevanDate.ForeignKeyTableName = "";
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
				colvarDevanWarehouseID.DefaultSetting = @"";
				colvarDevanWarehouseID.ForeignKeyTableName = "";
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
				colvarDevanned.DefaultSetting = @"";
				colvarDevanned.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDevanned);
				
				TableSchema.TableColumn colvarDeliveryDate = new TableSchema.TableColumn(schema);
				colvarDeliveryDate.ColumnName = "DeliveryDate";
				colvarDeliveryDate.DataType = DbType.DateTime;
				colvarDeliveryDate.MaxLength = 0;
				colvarDeliveryDate.AutoIncrement = false;
				colvarDeliveryDate.IsNullable = true;
				colvarDeliveryDate.IsPrimaryKey = false;
				colvarDeliveryDate.IsForeignKey = false;
				colvarDeliveryDate.IsReadOnly = false;
				colvarDeliveryDate.DefaultSetting = @"";
				colvarDeliveryDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeliveryDate);
				
				TableSchema.TableColumn colvarDelivered = new TableSchema.TableColumn(schema);
				colvarDelivered.ColumnName = "Delivered";
				colvarDelivered.DataType = DbType.Boolean;
				colvarDelivered.MaxLength = 0;
				colvarDelivered.AutoIncrement = false;
				colvarDelivered.IsNullable = true;
				colvarDelivered.IsPrimaryKey = false;
				colvarDelivered.IsForeignKey = false;
				colvarDelivered.IsReadOnly = false;
				colvarDelivered.DefaultSetting = @"";
				colvarDelivered.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDelivered);
				
				TableSchema.TableColumn colvarLoadedOnBoard = new TableSchema.TableColumn(schema);
				colvarLoadedOnBoard.ColumnName = "LoadedOnBoard";
				colvarLoadedOnBoard.DataType = DbType.Boolean;
				colvarLoadedOnBoard.MaxLength = 0;
				colvarLoadedOnBoard.AutoIncrement = false;
				colvarLoadedOnBoard.IsNullable = true;
				colvarLoadedOnBoard.IsPrimaryKey = false;
				colvarLoadedOnBoard.IsForeignKey = false;
				colvarLoadedOnBoard.IsReadOnly = false;
				colvarLoadedOnBoard.DefaultSetting = @"";
				colvarLoadedOnBoard.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLoadedOnBoard);
				
				TableSchema.TableColumn colvarUpdated = new TableSchema.TableColumn(schema);
				colvarUpdated.ColumnName = "Updated";
				colvarUpdated.DataType = DbType.DateTime;
				colvarUpdated.MaxLength = 0;
				colvarUpdated.AutoIncrement = false;
				colvarUpdated.IsNullable = true;
				colvarUpdated.IsPrimaryKey = false;
				colvarUpdated.IsForeignKey = false;
				colvarUpdated.IsReadOnly = false;
				colvarUpdated.DefaultSetting = @"";
				colvarUpdated.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUpdated);
				
				TableSchema.TableColumn colvarDevanNotes = new TableSchema.TableColumn(schema);
				colvarDevanNotes.ColumnName = "DevanNotes";
				colvarDevanNotes.DataType = DbType.String;
				colvarDevanNotes.MaxLength = 50;
				colvarDevanNotes.AutoIncrement = false;
				colvarDevanNotes.IsNullable = true;
				colvarDevanNotes.IsPrimaryKey = false;
				colvarDevanNotes.IsForeignKey = false;
				colvarDevanNotes.IsReadOnly = false;
				colvarDevanNotes.DefaultSetting = @"";
				colvarDevanNotes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDevanNotes);
				
				TableSchema.TableColumn colvarOriginControllerID = new TableSchema.TableColumn(schema);
				colvarOriginControllerID.ColumnName = "OriginControllerID";
				colvarOriginControllerID.DataType = DbType.Int32;
				colvarOriginControllerID.MaxLength = 0;
				colvarOriginControllerID.AutoIncrement = false;
				colvarOriginControllerID.IsNullable = true;
				colvarOriginControllerID.IsPrimaryKey = false;
				colvarOriginControllerID.IsForeignKey = false;
				colvarOriginControllerID.IsReadOnly = false;
				colvarOriginControllerID.DefaultSetting = @"";
				colvarOriginControllerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOriginControllerID);
				
				TableSchema.TableColumn colvarDestinationControllerID = new TableSchema.TableColumn(schema);
				colvarDestinationControllerID.ColumnName = "DestinationControllerID";
				colvarDestinationControllerID.DataType = DbType.Int32;
				colvarDestinationControllerID.MaxLength = 0;
				colvarDestinationControllerID.AutoIncrement = false;
				colvarDestinationControllerID.IsNullable = true;
				colvarDestinationControllerID.IsPrimaryKey = false;
				colvarDestinationControllerID.IsForeignKey = false;
				colvarDestinationControllerID.IsReadOnly = false;
				colvarDestinationControllerID.DefaultSetting = @"";
				colvarDestinationControllerID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDestinationControllerID);
				
				TableSchema.TableColumn colvarDeliveryTime = new TableSchema.TableColumn(schema);
				colvarDeliveryTime.ColumnName = "DeliveryTime";
				colvarDeliveryTime.DataType = DbType.DateTime;
				colvarDeliveryTime.MaxLength = 0;
				colvarDeliveryTime.AutoIncrement = false;
				colvarDeliveryTime.IsNullable = true;
				colvarDeliveryTime.IsPrimaryKey = false;
				colvarDeliveryTime.IsForeignKey = false;
				colvarDeliveryTime.IsReadOnly = false;
				colvarDeliveryTime.DefaultSetting = @"";
				colvarDeliveryTime.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeliveryTime);
				
				TableSchema.TableColumn colvarBookingRef = new TableSchema.TableColumn(schema);
				colvarBookingRef.ColumnName = "BookingRef";
				colvarBookingRef.DataType = DbType.String;
				colvarBookingRef.MaxLength = 50;
				colvarBookingRef.AutoIncrement = false;
				colvarBookingRef.IsNullable = true;
				colvarBookingRef.IsPrimaryKey = false;
				colvarBookingRef.IsForeignKey = false;
				colvarBookingRef.IsReadOnly = false;
				colvarBookingRef.DefaultSetting = @"";
				colvarBookingRef.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBookingRef);
				
				TableSchema.TableColumn colvarContainerStatusID = new TableSchema.TableColumn(schema);
				colvarContainerStatusID.ColumnName = "ContainerStatusID";
				colvarContainerStatusID.DataType = DbType.Int32;
				colvarContainerStatusID.MaxLength = 0;
				colvarContainerStatusID.AutoIncrement = false;
				colvarContainerStatusID.IsNullable = true;
				colvarContainerStatusID.IsPrimaryKey = false;
				colvarContainerStatusID.IsForeignKey = false;
				colvarContainerStatusID.IsReadOnly = false;
				colvarContainerStatusID.DefaultSetting = @"";
				colvarContainerStatusID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContainerStatusID);
				
				TableSchema.TableColumn colvarTs = new TableSchema.TableColumn(schema);
				colvarTs.ColumnName = "TS";
				colvarTs.DataType = DbType.Binary;
				colvarTs.MaxLength = 0;
				colvarTs.AutoIncrement = false;
				colvarTs.IsNullable = true;
				colvarTs.IsPrimaryKey = false;
				colvarTs.IsForeignKey = false;
				colvarTs.IsReadOnly = true;
				colvarTs.DefaultSetting = @"";
				colvarTs.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTs);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("ContainerTable",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("ContainerID")]
		[Bindable(true)]
		public int ContainerID 
		{
			get { return GetColumnValue<int>(Columns.ContainerID); }
			set { SetColumnValue(Columns.ContainerID, value); }
		}
		  
		[XmlAttribute("ContainerNumber")]
		[Bindable(true)]
		public string ContainerNumber 
		{
			get { return GetColumnValue<string>(Columns.ContainerNumber); }
			set { SetColumnValue(Columns.ContainerNumber, value); }
		}
		  
		[XmlAttribute("SizeTypeID")]
		[Bindable(true)]
		public int? SizeTypeID 
		{
			get { return GetColumnValue<int?>(Columns.SizeTypeID); }
			set { SetColumnValue(Columns.SizeTypeID, value); }
		}
		  
		[XmlAttribute("VoyageID")]
		[Bindable(true)]
		public int? VoyageID 
		{
			get { return GetColumnValue<int?>(Columns.VoyageID); }
			set { SetColumnValue(Columns.VoyageID, value); }
		}
		  
		[XmlAttribute("OriginPortID")]
		[Bindable(true)]
		public int? OriginPortID 
		{
			get { return GetColumnValue<int?>(Columns.OriginPortID); }
			set { SetColumnValue(Columns.OriginPortID, value); }
		}
		  
		[XmlAttribute("DestinationPortID")]
		[Bindable(true)]
		public int? DestinationPortID 
		{
			get { return GetColumnValue<int?>(Columns.DestinationPortID); }
			set { SetColumnValue(Columns.DestinationPortID, value); }
		}
		  
		[XmlAttribute("Cycfs")]
		[Bindable(true)]
		public int? Cycfs 
		{
			get { return GetColumnValue<int?>(Columns.Cycfs); }
			set { SetColumnValue(Columns.Cycfs, value); }
		}
		  
		[XmlAttribute("DevanDate")]
		[Bindable(true)]
		public DateTime? DevanDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.DevanDate); }
			set { SetColumnValue(Columns.DevanDate, value); }
		}
		  
		[XmlAttribute("DevanWarehouseID")]
		[Bindable(true)]
		public int? DevanWarehouseID 
		{
			get { return GetColumnValue<int?>(Columns.DevanWarehouseID); }
			set { SetColumnValue(Columns.DevanWarehouseID, value); }
		}
		  
		[XmlAttribute("Devanned")]
		[Bindable(true)]
		public bool? Devanned 
		{
			get { return GetColumnValue<bool?>(Columns.Devanned); }
			set { SetColumnValue(Columns.Devanned, value); }
		}
		  
		[XmlAttribute("DeliveryDate")]
		[Bindable(true)]
		public DateTime? DeliveryDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.DeliveryDate); }
			set { SetColumnValue(Columns.DeliveryDate, value); }
		}
		  
		[XmlAttribute("Delivered")]
		[Bindable(true)]
		public bool? Delivered 
		{
			get { return GetColumnValue<bool?>(Columns.Delivered); }
			set { SetColumnValue(Columns.Delivered, value); }
		}
		  
		[XmlAttribute("LoadedOnBoard")]
		[Bindable(true)]
		public bool? LoadedOnBoard 
		{
			get { return GetColumnValue<bool?>(Columns.LoadedOnBoard); }
			set { SetColumnValue(Columns.LoadedOnBoard, value); }
		}
		  
		[XmlAttribute("Updated")]
		[Bindable(true)]
		public DateTime? Updated 
		{
			get { return GetColumnValue<DateTime?>(Columns.Updated); }
			set { SetColumnValue(Columns.Updated, value); }
		}
		  
		[XmlAttribute("DevanNotes")]
		[Bindable(true)]
		public string DevanNotes 
		{
			get { return GetColumnValue<string>(Columns.DevanNotes); }
			set { SetColumnValue(Columns.DevanNotes, value); }
		}
		  
		[XmlAttribute("OriginControllerID")]
		[Bindable(true)]
		public int? OriginControllerID 
		{
			get { return GetColumnValue<int?>(Columns.OriginControllerID); }
			set { SetColumnValue(Columns.OriginControllerID, value); }
		}
		  
		[XmlAttribute("DestinationControllerID")]
		[Bindable(true)]
		public int? DestinationControllerID 
		{
			get { return GetColumnValue<int?>(Columns.DestinationControllerID); }
			set { SetColumnValue(Columns.DestinationControllerID, value); }
		}
		  
		[XmlAttribute("DeliveryTime")]
		[Bindable(true)]
		public DateTime? DeliveryTime 
		{
			get { return GetColumnValue<DateTime?>(Columns.DeliveryTime); }
			set { SetColumnValue(Columns.DeliveryTime, value); }
		}
		  
		[XmlAttribute("BookingRef")]
		[Bindable(true)]
		public string BookingRef 
		{
			get { return GetColumnValue<string>(Columns.BookingRef); }
			set { SetColumnValue(Columns.BookingRef, value); }
		}
		  
		[XmlAttribute("ContainerStatusID")]
		[Bindable(true)]
		public int? ContainerStatusID 
		{
			get { return GetColumnValue<int?>(Columns.ContainerStatusID); }
			set { SetColumnValue(Columns.ContainerStatusID, value); }
		}
		  
		[XmlAttribute("Ts")]
		[Bindable(true)]
		public byte[] Ts 
		{
			get { return GetColumnValue<byte[]>(Columns.Ts); }
			set { SetColumnValue(Columns.Ts, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varContainerNumber,int? varSizeTypeID,int? varVoyageID,int? varOriginPortID,int? varDestinationPortID,int? varCycfs,DateTime? varDevanDate,int? varDevanWarehouseID,bool? varDevanned,DateTime? varDeliveryDate,bool? varDelivered,bool? varLoadedOnBoard,DateTime? varUpdated,string varDevanNotes,int? varOriginControllerID,int? varDestinationControllerID,DateTime? varDeliveryTime,string varBookingRef,int? varContainerStatusID,byte[] varTs)
		{
			ContainerTable item = new ContainerTable();
			
			item.ContainerNumber = varContainerNumber;
			
			item.SizeTypeID = varSizeTypeID;
			
			item.VoyageID = varVoyageID;
			
			item.OriginPortID = varOriginPortID;
			
			item.DestinationPortID = varDestinationPortID;
			
			item.Cycfs = varCycfs;
			
			item.DevanDate = varDevanDate;
			
			item.DevanWarehouseID = varDevanWarehouseID;
			
			item.Devanned = varDevanned;
			
			item.DeliveryDate = varDeliveryDate;
			
			item.Delivered = varDelivered;
			
			item.LoadedOnBoard = varLoadedOnBoard;
			
			item.Updated = varUpdated;
			
			item.DevanNotes = varDevanNotes;
			
			item.OriginControllerID = varOriginControllerID;
			
			item.DestinationControllerID = varDestinationControllerID;
			
			item.DeliveryTime = varDeliveryTime;
			
			item.BookingRef = varBookingRef;
			
			item.ContainerStatusID = varContainerStatusID;
			
			item.Ts = varTs;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varContainerID,string varContainerNumber,int? varSizeTypeID,int? varVoyageID,int? varOriginPortID,int? varDestinationPortID,int? varCycfs,DateTime? varDevanDate,int? varDevanWarehouseID,bool? varDevanned,DateTime? varDeliveryDate,bool? varDelivered,bool? varLoadedOnBoard,DateTime? varUpdated,string varDevanNotes,int? varOriginControllerID,int? varDestinationControllerID,DateTime? varDeliveryTime,string varBookingRef,int? varContainerStatusID,byte[] varTs)
		{
			ContainerTable item = new ContainerTable();
			
				item.ContainerID = varContainerID;
			
				item.ContainerNumber = varContainerNumber;
			
				item.SizeTypeID = varSizeTypeID;
			
				item.VoyageID = varVoyageID;
			
				item.OriginPortID = varOriginPortID;
			
				item.DestinationPortID = varDestinationPortID;
			
				item.Cycfs = varCycfs;
			
				item.DevanDate = varDevanDate;
			
				item.DevanWarehouseID = varDevanWarehouseID;
			
				item.Devanned = varDevanned;
			
				item.DeliveryDate = varDeliveryDate;
			
				item.Delivered = varDelivered;
			
				item.LoadedOnBoard = varLoadedOnBoard;
			
				item.Updated = varUpdated;
			
				item.DevanNotes = varDevanNotes;
			
				item.OriginControllerID = varOriginControllerID;
			
				item.DestinationControllerID = varDestinationControllerID;
			
				item.DeliveryTime = varDeliveryTime;
			
				item.BookingRef = varBookingRef;
			
				item.ContainerStatusID = varContainerStatusID;
			
				item.Ts = varTs;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn ContainerIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ContainerNumberColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SizeTypeIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn VoyageIDColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn OriginPortIDColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn DestinationPortIDColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn CycfsColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn DevanDateColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn DevanWarehouseIDColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn DevannedColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn DeliveryDateColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn DeliveredColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn LoadedOnBoardColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdatedColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn DevanNotesColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn OriginControllerIDColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn DestinationControllerIDColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn DeliveryTimeColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn BookingRefColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn ContainerStatusIDColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn TsColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string ContainerID = @"ContainerID";
			 public static string ContainerNumber = @"ContainerNumber";
			 public static string SizeTypeID = @"SizeTypeID";
			 public static string VoyageID = @"VoyageID";
			 public static string OriginPortID = @"OriginPortID";
			 public static string DestinationPortID = @"DestinationPortID";
			 public static string Cycfs = @"CYCFS";
			 public static string DevanDate = @"DevanDate";
			 public static string DevanWarehouseID = @"DevanWarehouseID";
			 public static string Devanned = @"Devanned";
			 public static string DeliveryDate = @"DeliveryDate";
			 public static string Delivered = @"Delivered";
			 public static string LoadedOnBoard = @"LoadedOnBoard";
			 public static string Updated = @"Updated";
			 public static string DevanNotes = @"DevanNotes";
			 public static string OriginControllerID = @"OriginControllerID";
			 public static string DestinationControllerID = @"DestinationControllerID";
			 public static string DeliveryTime = @"DeliveryTime";
			 public static string BookingRef = @"BookingRef";
			 public static string ContainerStatusID = @"ContainerStatusID";
			 public static string Ts = @"TS";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}