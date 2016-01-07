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
	/// Strongly-typed collection for the ChargeTypeTable class.
	/// </summary>
    [Serializable]
	public partial class ChargeTypeTableCollection : ActiveList<ChargeTypeTable, ChargeTypeTableCollection>
	{	   
		public ChargeTypeTableCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>ChargeTypeTableCollection</returns>
		public ChargeTypeTableCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                ChargeTypeTable o = this[i];
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
	/// This is an ActiveRecord class which wraps the ChargeTypeTable table.
	/// </summary>
	[Serializable]
	public partial class ChargeTypeTable : ActiveRecord<ChargeTypeTable>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public ChargeTypeTable()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public ChargeTypeTable(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public ChargeTypeTable(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public ChargeTypeTable(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("ChargeTypeTable", TableType.Table, DataService.GetInstance("WWIprov"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarChargeID = new TableSchema.TableColumn(schema);
				colvarChargeID.ColumnName = "ChargeID";
				colvarChargeID.DataType = DbType.Int32;
				colvarChargeID.MaxLength = 0;
				colvarChargeID.AutoIncrement = true;
				colvarChargeID.IsNullable = false;
				colvarChargeID.IsPrimaryKey = true;
				colvarChargeID.IsForeignKey = false;
				colvarChargeID.IsReadOnly = false;
				colvarChargeID.DefaultSetting = @"";
				colvarChargeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChargeID);
				
				TableSchema.TableColumn colvarChargeName = new TableSchema.TableColumn(schema);
				colvarChargeName.ColumnName = "ChargeName";
				colvarChargeName.DataType = DbType.String;
				colvarChargeName.MaxLength = 50;
				colvarChargeName.AutoIncrement = false;
				colvarChargeName.IsNullable = false;
				colvarChargeName.IsPrimaryKey = false;
				colvarChargeName.IsForeignKey = false;
				colvarChargeName.IsReadOnly = false;
				colvarChargeName.DefaultSetting = @"";
				colvarChargeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarChargeName);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["WWIprov"].AddSchema("ChargeTypeTable",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("ChargeID")]
		[Bindable(true)]
		public int ChargeID 
		{
			get { return GetColumnValue<int>(Columns.ChargeID); }
			set { SetColumnValue(Columns.ChargeID, value); }
		}
		  
		[XmlAttribute("ChargeName")]
		[Bindable(true)]
		public string ChargeName 
		{
			get { return GetColumnValue<string>(Columns.ChargeName); }
			set { SetColumnValue(Columns.ChargeName, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		public DAL.Logistics.InternalInvoiceSubTableCollection InternalInvoiceSubTableRecords()
		{
			return new DAL.Logistics.InternalInvoiceSubTableCollection().Where(InternalInvoiceSubTable.Columns.ChargeType, ChargeID).Load();
		}
		#endregion
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varChargeName)
		{
			ChargeTypeTable item = new ChargeTypeTable();
			
			item.ChargeName = varChargeName;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varChargeID,string varChargeName)
		{
			ChargeTypeTable item = new ChargeTypeTable();
			
				item.ChargeID = varChargeID;
			
				item.ChargeName = varChargeName;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn ChargeIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ChargeNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string ChargeID = @"ChargeID";
			 public static string ChargeName = @"ChargeName";
						
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