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
	/// Strongly-typed collection for the VoyageETSSubTable class.
	/// </summary>
    [Serializable]
	public partial class VoyageETSSubTableCollection : ActiveList<VoyageETSSubTable, VoyageETSSubTableCollection>
	{	   
		public VoyageETSSubTableCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>VoyageETSSubTableCollection</returns>
		public VoyageETSSubTableCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                VoyageETSSubTable o = this[i];
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
	/// This is an ActiveRecord class which wraps the VoyageETSSubTable table.
	/// </summary>
	[Serializable]
	public partial class VoyageETSSubTable : ActiveRecord<VoyageETSSubTable>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public VoyageETSSubTable()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public VoyageETSSubTable(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public VoyageETSSubTable(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public VoyageETSSubTable(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("VoyageETSSubTable", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarVoyageETSSubID = new TableSchema.TableColumn(schema);
				colvarVoyageETSSubID.ColumnName = "VoyageETSSubID";
				colvarVoyageETSSubID.DataType = DbType.Int32;
				colvarVoyageETSSubID.MaxLength = 0;
				colvarVoyageETSSubID.AutoIncrement = true;
				colvarVoyageETSSubID.IsNullable = false;
				colvarVoyageETSSubID.IsPrimaryKey = true;
				colvarVoyageETSSubID.IsForeignKey = false;
				colvarVoyageETSSubID.IsReadOnly = false;
				colvarVoyageETSSubID.DefaultSetting = @"";
				colvarVoyageETSSubID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarVoyageETSSubID);
				
				TableSchema.TableColumn colvarVoyageID = new TableSchema.TableColumn(schema);
				colvarVoyageID.ColumnName = "VoyageID";
				colvarVoyageID.DataType = DbType.Int32;
				colvarVoyageID.MaxLength = 0;
				colvarVoyageID.AutoIncrement = false;
				colvarVoyageID.IsNullable = true;
				colvarVoyageID.IsPrimaryKey = false;
				colvarVoyageID.IsForeignKey = true;
				colvarVoyageID.IsReadOnly = false;
				colvarVoyageID.DefaultSetting = @"";
				
					colvarVoyageID.ForeignKeyTableName = "VoyageTable";
				schema.Columns.Add(colvarVoyageID);
				
				TableSchema.TableColumn colvarOriginPortID = new TableSchema.TableColumn(schema);
				colvarOriginPortID.ColumnName = "OriginPortID";
				colvarOriginPortID.DataType = DbType.Int32;
				colvarOriginPortID.MaxLength = 0;
				colvarOriginPortID.AutoIncrement = false;
				colvarOriginPortID.IsNullable = true;
				colvarOriginPortID.IsPrimaryKey = false;
				colvarOriginPortID.IsForeignKey = true;
				colvarOriginPortID.IsReadOnly = false;
				colvarOriginPortID.DefaultSetting = @"";
				
					colvarOriginPortID.ForeignKeyTableName = "PortTable";
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
				colvarEts.DefaultSetting = @"";
				colvarEts.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEts);
				
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
				DataService.Providers["WWIprov"].AddSchema("VoyageETSSubTable",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("VoyageETSSubID")]
		[Bindable(true)]
		public int VoyageETSSubID 
		{
			get { return GetColumnValue<int>(Columns.VoyageETSSubID); }
			set { SetColumnValue(Columns.VoyageETSSubID, value); }
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
		  
		[XmlAttribute("Ets")]
		[Bindable(true)]
		public DateTime? Ets 
		{
			get { return GetColumnValue<DateTime?>(Columns.Ets); }
			set { SetColumnValue(Columns.Ets, value); }
		}
		  
		[XmlAttribute("Ts")]
		[Bindable(true)]
		public byte[] Ts 
		{
			get { return GetColumnValue<byte[]>(Columns.Ts); }
			set { SetColumnValue(Columns.Ts, value); }
		}
		
		#endregion
		
		
			
		
		#region ForeignKey Properties
		
		/// <summary>
		/// Returns a PortTable ActiveRecord object related to this VoyageETSSubTable
		/// 
		/// </summary>
		public DAL.Logistics.PortTable PortTable
		{
			get { return DAL.Logistics.PortTable.FetchByID(this.OriginPortID); }
			set { SetColumnValue("OriginPortID", value.PortID); }
		}
		
		
		/// <summary>
		/// Returns a VoyageTable ActiveRecord object related to this VoyageETSSubTable
		/// 
		/// </summary>
		public DAL.Logistics.VoyageTable VoyageTable
		{
			get { return DAL.Logistics.VoyageTable.FetchByID(this.VoyageID); }
			set { SetColumnValue("VoyageID", value.VoyageID); }
		}
		
		
		#endregion
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int? varVoyageID,int? varOriginPortID,DateTime? varEts,byte[] varTs)
		{
			VoyageETSSubTable item = new VoyageETSSubTable();
			
			item.VoyageID = varVoyageID;
			
			item.OriginPortID = varOriginPortID;
			
			item.Ets = varEts;
			
			item.Ts = varTs;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varVoyageETSSubID,int? varVoyageID,int? varOriginPortID,DateTime? varEts,byte[] varTs)
		{
			VoyageETSSubTable item = new VoyageETSSubTable();
			
				item.VoyageETSSubID = varVoyageETSSubID;
			
				item.VoyageID = varVoyageID;
			
				item.OriginPortID = varOriginPortID;
			
				item.Ets = varEts;
			
				item.Ts = varTs;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn VoyageETSSubIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn VoyageIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn OriginPortIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn EtsColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn TsColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string VoyageETSSubID = @"VoyageETSSubID";
			 public static string VoyageID = @"VoyageID";
			 public static string OriginPortID = @"OriginPortID";
			 public static string Ets = @"ETS";
			 public static string Ts = @"TS";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
