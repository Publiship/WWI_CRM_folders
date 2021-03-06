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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Publiship_Pricer")]
	public partial class linq_pricer_feedsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insertpricer_origin_point(pricer_origin_point instance);
    partial void Updatepricer_origin_point(pricer_origin_point instance);
    partial void Deletepricer_origin_point(pricer_origin_point instance);
    partial void Insertpricer_dest_country(pricer_dest_country instance);
    partial void Updatepricer_dest_country(pricer_dest_country instance);
    partial void Deletepricer_dest_country(pricer_dest_country instance);
    partial void Insertpricer_dest_final(pricer_dest_final instance);
    partial void Updatepricer_dest_final(pricer_dest_final instance);
    partial void Deletepricer_dest_final(pricer_dest_final instance);
    #endregion
		
		public linq_pricer_feedsDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["Publiship_PricerConnectionString2"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public linq_pricer_feedsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_pricer_feedsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_pricer_feedsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_pricer_feedsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<pricer_origin_point> pricer_origin_points
		{
			get
			{
				return this.GetTable<pricer_origin_point>();
			}
		}
		
		public System.Data.Linq.Table<pricer_dest_country> pricer_dest_countries
		{
			get
			{
				return this.GetTable<pricer_dest_country>();
			}
		}
		
		public System.Data.Linq.Table<pricer_dest_final> pricer_dest_finals
		{
			get
			{
				return this.GetTable<pricer_dest_final>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.pricer_origin_point")]
	public partial class pricer_origin_point : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _origin_point_ID;
		
		private string _origin_point;
		
		private EntitySet<pricer_dest_country> _pricer_dest_countries;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onorigin_point_IDChanging(int value);
    partial void Onorigin_point_IDChanged();
    partial void Onorigin_pointChanging(string value);
    partial void Onorigin_pointChanged();
    #endregion
		
		public pricer_origin_point()
		{
			this._pricer_dest_countries = new EntitySet<pricer_dest_country>(new Action<pricer_dest_country>(this.attach_pricer_dest_countries), new Action<pricer_dest_country>(this.detach_pricer_dest_countries));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_origin_point_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int origin_point_ID
		{
			get
			{
				return this._origin_point_ID;
			}
			set
			{
				if ((this._origin_point_ID != value))
				{
					this.Onorigin_point_IDChanging(value);
					this.SendPropertyChanging();
					this._origin_point_ID = value;
					this.SendPropertyChanged("origin_point_ID");
					this.Onorigin_point_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_origin_point", DbType="VarChar(50)")]
		public string origin_point
		{
			get
			{
				return this._origin_point;
			}
			set
			{
				if ((this._origin_point != value))
				{
					this.Onorigin_pointChanging(value);
					this.SendPropertyChanging();
					this._origin_point = value;
					this.SendPropertyChanged("origin_point");
					this.Onorigin_pointChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="pricer_origin_point_pricer_dest_country", Storage="_pricer_dest_countries", ThisKey="origin_point_ID", OtherKey="origin_point_ID")]
		public EntitySet<pricer_dest_country> pricer_dest_countries
		{
			get
			{
				return this._pricer_dest_countries;
			}
			set
			{
				this._pricer_dest_countries.Assign(value);
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
		
		private void attach_pricer_dest_countries(pricer_dest_country entity)
		{
			this.SendPropertyChanging();
			entity.pricer_origin_point = this;
		}
		
		private void detach_pricer_dest_countries(pricer_dest_country entity)
		{
			this.SendPropertyChanging();
			entity.pricer_origin_point = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.pricer_dest_country")]
	public partial class pricer_dest_country : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _dest_country_ID;
		
		private int _origin_point_ID;
		
		private string _country_name;
		
		private int _country_id;
		
		private EntitySet<pricer_dest_final> _pricer_dest_finals;
		
		private EntityRef<pricer_origin_point> _pricer_origin_point;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Ondest_country_IDChanging(int value);
    partial void Ondest_country_IDChanged();
    partial void Onorigin_point_IDChanging(int value);
    partial void Onorigin_point_IDChanged();
    partial void Oncountry_nameChanging(string value);
    partial void Oncountry_nameChanged();
    partial void Oncountry_idChanging(int value);
    partial void Oncountry_idChanged();
    #endregion
		
		public pricer_dest_country()
		{
			this._pricer_dest_finals = new EntitySet<pricer_dest_final>(new Action<pricer_dest_final>(this.attach_pricer_dest_finals), new Action<pricer_dest_final>(this.detach_pricer_dest_finals));
			this._pricer_origin_point = default(EntityRef<pricer_origin_point>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dest_country_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int dest_country_ID
		{
			get
			{
				return this._dest_country_ID;
			}
			set
			{
				if ((this._dest_country_ID != value))
				{
					this.Ondest_country_IDChanging(value);
					this.SendPropertyChanging();
					this._dest_country_ID = value;
					this.SendPropertyChanged("dest_country_ID");
					this.Ondest_country_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_origin_point_ID", DbType="Int NOT NULL")]
		public int origin_point_ID
		{
			get
			{
				return this._origin_point_ID;
			}
			set
			{
				if ((this._origin_point_ID != value))
				{
					if (this._pricer_origin_point.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.Onorigin_point_IDChanging(value);
					this.SendPropertyChanging();
					this._origin_point_ID = value;
					this.SendPropertyChanged("origin_point_ID");
					this.Onorigin_point_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_country_name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string country_name
		{
			get
			{
				return this._country_name;
			}
			set
			{
				if ((this._country_name != value))
				{
					this.Oncountry_nameChanging(value);
					this.SendPropertyChanging();
					this._country_name = value;
					this.SendPropertyChanged("country_name");
					this.Oncountry_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_country_id", DbType="Int NOT NULL")]
		public int country_id
		{
			get
			{
				return this._country_id;
			}
			set
			{
				if ((this._country_id != value))
				{
					this.Oncountry_idChanging(value);
					this.SendPropertyChanging();
					this._country_id = value;
					this.SendPropertyChanged("country_id");
					this.Oncountry_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="pricer_dest_country_pricer_dest_final", Storage="_pricer_dest_finals", ThisKey="country_id", OtherKey="dest_country_ID")]
		public EntitySet<pricer_dest_final> pricer_dest_finals
		{
			get
			{
				return this._pricer_dest_finals;
			}
			set
			{
				this._pricer_dest_finals.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="pricer_origin_point_pricer_dest_country", Storage="_pricer_origin_point", ThisKey="origin_point_ID", OtherKey="origin_point_ID", IsForeignKey=true)]
		public pricer_origin_point pricer_origin_point
		{
			get
			{
				return this._pricer_origin_point.Entity;
			}
			set
			{
				pricer_origin_point previousValue = this._pricer_origin_point.Entity;
				if (((previousValue != value) 
							|| (this._pricer_origin_point.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._pricer_origin_point.Entity = null;
						previousValue.pricer_dest_countries.Remove(this);
					}
					this._pricer_origin_point.Entity = value;
					if ((value != null))
					{
						value.pricer_dest_countries.Add(this);
						this._origin_point_ID = value.origin_point_ID;
					}
					else
					{
						this._origin_point_ID = default(int);
					}
					this.SendPropertyChanged("pricer_origin_point");
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
		
		private void attach_pricer_dest_finals(pricer_dest_final entity)
		{
			this.SendPropertyChanging();
			entity.pricer_dest_country = this;
		}
		
		private void detach_pricer_dest_finals(pricer_dest_final entity)
		{
			this.SendPropertyChanging();
			entity.pricer_dest_country = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.pricer_dest_final")]
	public partial class pricer_dest_final : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _dest_final_ID;
		
		private int _dest_country_ID;
		
		private string _dest_final;
		
		private EntityRef<pricer_dest_country> _pricer_dest_country;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Ondest_final_IDChanging(int value);
    partial void Ondest_final_IDChanged();
    partial void Ondest_country_IDChanging(int value);
    partial void Ondest_country_IDChanged();
    partial void Ondest_finalChanging(string value);
    partial void Ondest_finalChanged();
    #endregion
		
		public pricer_dest_final()
		{
			this._pricer_dest_country = default(EntityRef<pricer_dest_country>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dest_final_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int dest_final_ID
		{
			get
			{
				return this._dest_final_ID;
			}
			set
			{
				if ((this._dest_final_ID != value))
				{
					this.Ondest_final_IDChanging(value);
					this.SendPropertyChanging();
					this._dest_final_ID = value;
					this.SendPropertyChanged("dest_final_ID");
					this.Ondest_final_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dest_country_ID", DbType="Int NOT NULL")]
		public int dest_country_ID
		{
			get
			{
				return this._dest_country_ID;
			}
			set
			{
				if ((this._dest_country_ID != value))
				{
					if (this._pricer_dest_country.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.Ondest_country_IDChanging(value);
					this.SendPropertyChanging();
					this._dest_country_ID = value;
					this.SendPropertyChanged("dest_country_ID");
					this.Ondest_country_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dest_final", DbType="VarChar(100)")]
		public string dest_final
		{
			get
			{
				return this._dest_final;
			}
			set
			{
				if ((this._dest_final != value))
				{
					this.Ondest_finalChanging(value);
					this.SendPropertyChanging();
					this._dest_final = value;
					this.SendPropertyChanged("dest_final");
					this.Ondest_finalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="pricer_dest_country_pricer_dest_final", Storage="_pricer_dest_country", ThisKey="dest_country_ID", OtherKey="country_id", IsForeignKey=true)]
		public pricer_dest_country pricer_dest_country
		{
			get
			{
				return this._pricer_dest_country.Entity;
			}
			set
			{
				pricer_dest_country previousValue = this._pricer_dest_country.Entity;
				if (((previousValue != value) 
							|| (this._pricer_dest_country.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._pricer_dest_country.Entity = null;
						previousValue.pricer_dest_finals.Remove(this);
					}
					this._pricer_dest_country.Entity = value;
					if ((value != null))
					{
						value.pricer_dest_finals.Add(this);
						this._dest_country_ID = value.country_id;
					}
					else
					{
						this._dest_country_ID = default(int);
					}
					this.SendPropertyChanged("pricer_dest_country");
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
