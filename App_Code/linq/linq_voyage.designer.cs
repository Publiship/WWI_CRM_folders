﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace linq
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PublishipSQL")]
	public partial class linq_voyageDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertVoyageTable(VoyageTable instance);
    partial void UpdateVoyageTable(VoyageTable instance);
    partial void DeleteVoyageTable(VoyageTable instance);
    #endregion
		
		public linq_voyageDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public linq_voyageDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_voyageDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_voyageDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_voyageDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VoyageTable> VoyageTables
		{
			get
			{
				return this.GetTable<VoyageTable>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VoyageTable")]
	public partial class VoyageTable : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _VoyageID;
		
		private string _VoyageNumber;
		
		private System.Nullable<int> _VesselID;
		
		private string _Joined;
		
		private System.Nullable<int> _AddedBy;
		
		private System.Nullable<System.DateTime> _DateAdded;
		
		private System.Data.Linq.Binary _TS;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnVoyageIDChanging(int value);
    partial void OnVoyageIDChanged();
    partial void OnVoyageNumberChanging(string value);
    partial void OnVoyageNumberChanged();
    partial void OnVesselIDChanging(System.Nullable<int> value);
    partial void OnVesselIDChanged();
    partial void OnJoinedChanging(string value);
    partial void OnJoinedChanged();
    partial void OnAddedByChanging(System.Nullable<int> value);
    partial void OnAddedByChanged();
    partial void OnDateAddedChanging(System.Nullable<System.DateTime> value);
    partial void OnDateAddedChanged();
    partial void OnTSChanging(System.Data.Linq.Binary value);
    partial void OnTSChanged();
    #endregion
		
		public VoyageTable()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VoyageID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
		public int VoyageID
		{
			get
			{
				return this._VoyageID;
			}
			set
			{
				if ((this._VoyageID != value))
				{
					this.OnVoyageIDChanging(value);
					this.SendPropertyChanging();
					this._VoyageID = value;
					this.SendPropertyChanged("VoyageID");
					this.OnVoyageIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VoyageNumber", DbType="NVarChar(50)", UpdateCheck=UpdateCheck.Never)]
		public string VoyageNumber
		{
			get
			{
				return this._VoyageNumber;
			}
			set
			{
				if ((this._VoyageNumber != value))
				{
					this.OnVoyageNumberChanging(value);
					this.SendPropertyChanging();
					this._VoyageNumber = value;
					this.SendPropertyChanged("VoyageNumber");
					this.OnVoyageNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VesselID", DbType="Int", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<int> VesselID
		{
			get
			{
				return this._VesselID;
			}
			set
			{
				if ((this._VesselID != value))
				{
					this.OnVesselIDChanging(value);
					this.SendPropertyChanging();
					this._VesselID = value;
					this.SendPropertyChanged("VesselID");
					this.OnVesselIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Joined", DbType="NVarChar(50)", UpdateCheck=UpdateCheck.Never)]
		public string Joined
		{
			get
			{
				return this._Joined;
			}
			set
			{
				if ((this._Joined != value))
				{
					this.OnJoinedChanging(value);
					this.SendPropertyChanging();
					this._Joined = value;
					this.SendPropertyChanged("Joined");
					this.OnJoinedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AddedBy", DbType="Int", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<int> AddedBy
		{
			get
			{
				return this._AddedBy;
			}
			set
			{
				if ((this._AddedBy != value))
				{
					this.OnAddedByChanging(value);
					this.SendPropertyChanging();
					this._AddedBy = value;
					this.SendPropertyChanged("AddedBy");
					this.OnAddedByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateAdded", DbType="SmallDateTime", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<System.DateTime> DateAdded
		{
			get
			{
				return this._DateAdded;
			}
			set
			{
				if ((this._DateAdded != value))
				{
					this.OnDateAddedChanging(value);
					this.SendPropertyChanging();
					this._DateAdded = value;
					this.SendPropertyChanged("DateAdded");
					this.OnDateAddedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TS", AutoSync=AutoSync.Always, DbType="rowversion", IsDbGenerated=true, IsVersion=true, UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary TS
		{
			get
			{
				return this._TS;
			}
			set
			{
				if ((this._TS != value))
				{
					this.OnTSChanging(value);
					this.SendPropertyChanging();
					this._TS = value;
					this.SendPropertyChanged("TS");
					this.OnTSChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
