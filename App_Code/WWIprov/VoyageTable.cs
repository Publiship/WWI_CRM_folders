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
	/// Strongly-typed collection for the VoyageTable class.
	/// </summary>
    [Serializable]
	public partial class VoyageTableCollection : ActiveList<VoyageTable, VoyageTableCollection>
	{	   
		public VoyageTableCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>VoyageTableCollection</returns>
		public VoyageTableCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                VoyageTable o = this[i];
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
	/// This is an ActiveRecord class which wraps the VoyageTable table.
	/// </summary>
	[Serializable]
	public partial class VoyageTable : ActiveRecord<VoyageTable>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public VoyageTable()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public VoyageTable(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public VoyageTable(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public VoyageTable(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("VoyageTable", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarVoyageID = new TableSchema.TableColumn(schema);
				colvarVoyageID.ColumnName = "VoyageID";
				colvarVoyageID.DataType = DbType.Int32;
				colvarVoyageID.MaxLength = 0;
				colvarVoyageID.AutoIncrement = true;
				colvarVoyageID.IsNullable = false;
				colvarVoyageID.IsPrimaryKey = true;
				colvarVoyageID.IsForeignKey = false;
				colvarVoyageID.IsReadOnly = false;
				colvarVoyageID.DefaultSetting = @"";
				colvarVoyageID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVoyageID);
				
				TableSchema.TableColumn colvarVoyageNumber = new TableSchema.TableColumn(schema);
				colvarVoyageNumber.ColumnName = "VoyageNumber";
				colvarVoyageNumber.DataType = DbType.String;
				colvarVoyageNumber.MaxLength = 50;
				colvarVoyageNumber.AutoIncrement = false;
				colvarVoyageNumber.IsNullable = true;
				colvarVoyageNumber.IsPrimaryKey = false;
				colvarVoyageNumber.IsForeignKey = false;
				colvarVoyageNumber.IsReadOnly = false;
				colvarVoyageNumber.DefaultSetting = @"";
				colvarVoyageNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVoyageNumber);
				
				TableSchema.TableColumn colvarVesselID = new TableSchema.TableColumn(schema);
				colvarVesselID.ColumnName = "VesselID";
				colvarVesselID.DataType = DbType.Int32;
				colvarVesselID.MaxLength = 0;
				colvarVesselID.AutoIncrement = false;
				colvarVesselID.IsNullable = true;
				colvarVesselID.IsPrimaryKey = false;
				colvarVesselID.IsForeignKey = true;
				colvarVesselID.IsReadOnly = false;
				colvarVesselID.DefaultSetting = @"";
				
					colvarVesselID.ForeignKeyTableName = "VesselTable";
				schema.Columns.Add(colvarVesselID);
				
				TableSchema.TableColumn colvarJoined = new TableSchema.TableColumn(schema);
				colvarJoined.ColumnName = "Joined";
				colvarJoined.DataType = DbType.String;
				colvarJoined.MaxLength = 50;
				colvarJoined.AutoIncrement = false;
				colvarJoined.IsNullable = true;
				colvarJoined.IsPrimaryKey = false;
				colvarJoined.IsForeignKey = false;
				colvarJoined.IsReadOnly = false;
				colvarJoined.DefaultSetting = @"";
				colvarJoined.ForeignKeyTableName = "";
				schema.Columns.Add(colvarJoined);
				
				TableSchema.TableColumn colvarAddedBy = new TableSchema.TableColumn(schema);
				colvarAddedBy.ColumnName = "AddedBy";
				colvarAddedBy.DataType = DbType.Int32;
				colvarAddedBy.MaxLength = 0;
				colvarAddedBy.AutoIncrement = false;
				colvarAddedBy.IsNullable = true;
				colvarAddedBy.IsPrimaryKey = false;
				colvarAddedBy.IsForeignKey = false;
				colvarAddedBy.IsReadOnly = false;
				colvarAddedBy.DefaultSetting = @"";
				colvarAddedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddedBy);
				
				TableSchema.TableColumn colvarDateAdded = new TableSchema.TableColumn(schema);
				colvarDateAdded.ColumnName = "DateAdded";
				colvarDateAdded.DataType = DbType.DateTime;
				colvarDateAdded.MaxLength = 0;
				colvarDateAdded.AutoIncrement = false;
				colvarDateAdded.IsNullable = true;
				colvarDateAdded.IsPrimaryKey = false;
				colvarDateAdded.IsForeignKey = false;
				colvarDateAdded.IsReadOnly = false;
				colvarDateAdded.DefaultSetting = @"";
				colvarDateAdded.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDateAdded);
				
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
				DataService.Providers["WWIprov"].AddSchema("VoyageTable",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("VoyageID")]
		[Bindable(true)]
		public int VoyageID 
		{
			get { return GetColumnValue<int>(Columns.VoyageID); }
			set { SetColumnValue(Columns.VoyageID, value); }
		}
		  
		[XmlAttribute("VoyageNumber")]
		[Bindable(true)]
		public string VoyageNumber 
		{
			get { return GetColumnValue<string>(Columns.VoyageNumber); }
			set { SetColumnValue(Columns.VoyageNumber, value); }
		}
		  
		[XmlAttribute("VesselID")]
		[Bindable(true)]
		public int? VesselID 
		{
			get { return GetColumnValue<int?>(Columns.VesselID); }
			set { SetColumnValue(Columns.VesselID, value); }
		}
		  
		[XmlAttribute("Joined")]
		[Bindable(true)]
		public string Joined 
		{
			get { return GetColumnValue<string>(Columns.Joined); }
			set { SetColumnValue(Columns.Joined, value); }
		}
		  
		[XmlAttribute("AddedBy")]
		[Bindable(true)]
		public int? AddedBy 
		{
			get { return GetColumnValue<int?>(Columns.AddedBy); }
			set { SetColumnValue(Columns.AddedBy, value); }
		}
		  
		[XmlAttribute("DateAdded")]
		[Bindable(true)]
		public DateTime? DateAdded 
		{
			get { return GetColumnValue<DateTime?>(Columns.DateAdded); }
			set { SetColumnValue(Columns.DateAdded, value); }
		}
		  
		[XmlAttribute("Ts")]
		[Bindable(true)]
		public byte[] Ts 
		{
			get { return GetColumnValue<byte[]>(Columns.Ts); }
			set { SetColumnValue(Columns.Ts, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		public DAL.Logistics.VoyageETASubTableCollection VoyageETASubTableRecords()
		{
			return new DAL.Logistics.VoyageETASubTableCollection().Where(VoyageETASubTable.Columns.VoyageID, VoyageID).Load();
		}
		public DAL.Logistics.VoyageETSSubTableCollection VoyageETSSubTableRecords()
		{
			return new DAL.Logistics.VoyageETSSubTableCollection().Where(VoyageETSSubTable.Columns.VoyageID, VoyageID).Load();
		}
		#endregion
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a VesselTable ActiveRecord object related to this VoyageTable
		/// 
		/// </summary>
		public DAL.Logistics.VesselTable VesselTable
		{
			get { return DAL.Logistics.VesselTable.FetchByID(this.VesselID); }
			set { SetColumnValue("VesselID", value.VesselID); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varVoyageNumber,int? varVesselID,string varJoined,int? varAddedBy,DateTime? varDateAdded,byte[] varTs)
		{
			VoyageTable item = new VoyageTable();
			
			item.VoyageNumber = varVoyageNumber;
			
			item.VesselID = varVesselID;
			
			item.Joined = varJoined;
			
			item.AddedBy = varAddedBy;
			
			item.DateAdded = varDateAdded;
			
			item.Ts = varTs;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varVoyageID,string varVoyageNumber,int? varVesselID,string varJoined,int? varAddedBy,DateTime? varDateAdded,byte[] varTs)
		{
			VoyageTable item = new VoyageTable();
			
				item.VoyageID = varVoyageID;
			
				item.VoyageNumber = varVoyageNumber;
			
				item.VesselID = varVesselID;
			
				item.Joined = varJoined;
			
				item.AddedBy = varAddedBy;
			
				item.DateAdded = varDateAdded;
			
				item.Ts = varTs;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn VoyageIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn VoyageNumberColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn VesselIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn JoinedColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn AddedByColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn DateAddedColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn TsColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string VoyageID = @"VoyageID";
			 public static string VoyageNumber = @"VoyageNumber";
			 public static string VesselID = @"VesselID";
			 public static string Joined = @"Joined";
			 public static string AddedBy = @"AddedBy";
			 public static string DateAdded = @"DateAdded";
			 public static string Ts = @"TS";
						
		}
		#endregion
		
		#region Update PK Collections
		
        public void SetPKValues()
        {
}
        #endregion
    
        #region Deep Save
		
        public void DeepSave()
        {
            Save();
            
}
        #endregion
	}
}