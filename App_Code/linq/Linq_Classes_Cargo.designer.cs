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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PUblishipSQL")]
	public partial class Linq_Classes_CargoDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insertview_cargo_update(view_cargo_update instance);
    partial void Updateview_cargo_update(view_cargo_update instance);
    partial void Deleteview_cargo_update(view_cargo_update instance);
    #endregion
		
		public Linq_Classes_CargoDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public Linq_Classes_CargoDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Linq_Classes_CargoDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Linq_Classes_CargoDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Linq_Classes_CargoDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<view_cargo_update> view_cargo_updates
		{
			get
			{
				return this.GetTable<view_cargo_update>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.view_cargo_updates")]
	public partial class view_cargo_update : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _cargoupdateid;
		
		private System.Nullable<int> _orderid;
		
		private System.Nullable<System.DateTime> _pre_cargoready;
		
		private System.Nullable<int> _pre_estpallets;
		
		private System.Nullable<int> _pre_estweight;
		
		private System.Nullable<float> _pre_estvolume;
		
		private System.Nullable<System.DateTime> _post_cargoready;
		
		private System.Nullable<int> _post_estpallets;
		
		private System.Nullable<int> _post_estweight;
		
		private System.Nullable<float> _post_estvolume;
		
		private System.Nullable<System.DateTime> _dtupdated;
		
		private string _ContactName;
		
		private string _ContactInitials;
		
		private string _EMail;
		
		private string _CompanyName;
		
		private string _MainEmail;
		
		private System.Nullable<int> _OrderNumber;
		
		private string _HouseBLNUmber;
		
		private string _CustomersRef;
		
		private string _Title;
		
		private string _ISBN;
		
		private System.Nullable<int> _companyid;
		
		private System.Nullable<int> _userid;
		
		private string _updguid;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OncargoupdateidChanging(int value);
    partial void OncargoupdateidChanged();
    partial void OnorderidChanging(System.Nullable<int> value);
    partial void OnorderidChanged();
    partial void Onpre_cargoreadyChanging(System.Nullable<System.DateTime> value);
    partial void Onpre_cargoreadyChanged();
    partial void Onpre_estpalletsChanging(System.Nullable<int> value);
    partial void Onpre_estpalletsChanged();
    partial void Onpre_estweightChanging(System.Nullable<int> value);
    partial void Onpre_estweightChanged();
    partial void Onpre_estvolumeChanging(System.Nullable<float> value);
    partial void Onpre_estvolumeChanged();
    partial void Onpost_cargoreadyChanging(System.Nullable<System.DateTime> value);
    partial void Onpost_cargoreadyChanged();
    partial void Onpost_estpalletsChanging(System.Nullable<int> value);
    partial void Onpost_estpalletsChanged();
    partial void Onpost_estweightChanging(System.Nullable<int> value);
    partial void Onpost_estweightChanged();
    partial void Onpost_estvolumeChanging(System.Nullable<float> value);
    partial void Onpost_estvolumeChanged();
    partial void OndtupdatedChanging(System.Nullable<System.DateTime> value);
    partial void OndtupdatedChanged();
    partial void OnContactNameChanging(string value);
    partial void OnContactNameChanged();
    partial void OnContactInitialsChanging(string value);
    partial void OnContactInitialsChanged();
    partial void OnEMailChanging(string value);
    partial void OnEMailChanged();
    partial void OnCompanyNameChanging(string value);
    partial void OnCompanyNameChanged();
    partial void OnMainEmailChanging(string value);
    partial void OnMainEmailChanged();
    partial void OnOrderNumberChanging(System.Nullable<int> value);
    partial void OnOrderNumberChanged();
    partial void OnHouseBLNUmberChanging(string value);
    partial void OnHouseBLNUmberChanged();
    partial void OnCustomersRefChanging(string value);
    partial void OnCustomersRefChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnISBNChanging(string value);
    partial void OnISBNChanged();
    partial void OncompanyidChanging(System.Nullable<int> value);
    partial void OncompanyidChanged();
    partial void OnuseridChanging(System.Nullable<int> value);
    partial void OnuseridChanged();
    partial void OnupdguidChanging(string value);
    partial void OnupdguidChanged();
    #endregion
		
		public view_cargo_update()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cargoupdateid", DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true)]
		public int cargoupdateid
		{
			get
			{
				return this._cargoupdateid;
			}
			set
			{
				if ((this._cargoupdateid != value))
				{
					this.OncargoupdateidChanging(value);
					this.SendPropertyChanging();
					this._cargoupdateid = value;
					this.SendPropertyChanged("cargoupdateid");
					this.OncargoupdateidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_orderid", DbType="Int")]
		public System.Nullable<int> orderid
		{
			get
			{
				return this._orderid;
			}
			set
			{
				if ((this._orderid != value))
				{
					this.OnorderidChanging(value);
					this.SendPropertyChanging();
					this._orderid = value;
					this.SendPropertyChanged("orderid");
					this.OnorderidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pre_cargoready", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> pre_cargoready
		{
			get
			{
				return this._pre_cargoready;
			}
			set
			{
				if ((this._pre_cargoready != value))
				{
					this.Onpre_cargoreadyChanging(value);
					this.SendPropertyChanging();
					this._pre_cargoready = value;
					this.SendPropertyChanged("pre_cargoready");
					this.Onpre_cargoreadyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pre_estpallets", DbType="Int")]
		public System.Nullable<int> pre_estpallets
		{
			get
			{
				return this._pre_estpallets;
			}
			set
			{
				if ((this._pre_estpallets != value))
				{
					this.Onpre_estpalletsChanging(value);
					this.SendPropertyChanging();
					this._pre_estpallets = value;
					this.SendPropertyChanged("pre_estpallets");
					this.Onpre_estpalletsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pre_estweight", DbType="Int")]
		public System.Nullable<int> pre_estweight
		{
			get
			{
				return this._pre_estweight;
			}
			set
			{
				if ((this._pre_estweight != value))
				{
					this.Onpre_estweightChanging(value);
					this.SendPropertyChanging();
					this._pre_estweight = value;
					this.SendPropertyChanged("pre_estweight");
					this.Onpre_estweightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pre_estvolume", DbType="Real")]
		public System.Nullable<float> pre_estvolume
		{
			get
			{
				return this._pre_estvolume;
			}
			set
			{
				if ((this._pre_estvolume != value))
				{
					this.Onpre_estvolumeChanging(value);
					this.SendPropertyChanging();
					this._pre_estvolume = value;
					this.SendPropertyChanged("pre_estvolume");
					this.Onpre_estvolumeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_post_cargoready", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> post_cargoready
		{
			get
			{
				return this._post_cargoready;
			}
			set
			{
				if ((this._post_cargoready != value))
				{
					this.Onpost_cargoreadyChanging(value);
					this.SendPropertyChanging();
					this._post_cargoready = value;
					this.SendPropertyChanged("post_cargoready");
					this.Onpost_cargoreadyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_post_estpallets", DbType="Int")]
		public System.Nullable<int> post_estpallets
		{
			get
			{
				return this._post_estpallets;
			}
			set
			{
				if ((this._post_estpallets != value))
				{
					this.Onpost_estpalletsChanging(value);
					this.SendPropertyChanging();
					this._post_estpallets = value;
					this.SendPropertyChanged("post_estpallets");
					this.Onpost_estpalletsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_post_estweight", DbType="Int")]
		public System.Nullable<int> post_estweight
		{
			get
			{
				return this._post_estweight;
			}
			set
			{
				if ((this._post_estweight != value))
				{
					this.Onpost_estweightChanging(value);
					this.SendPropertyChanging();
					this._post_estweight = value;
					this.SendPropertyChanged("post_estweight");
					this.Onpost_estweightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_post_estvolume", DbType="Real")]
		public System.Nullable<float> post_estvolume
		{
			get
			{
				return this._post_estvolume;
			}
			set
			{
				if ((this._post_estvolume != value))
				{
					this.Onpost_estvolumeChanging(value);
					this.SendPropertyChanging();
					this._post_estvolume = value;
					this.SendPropertyChanged("post_estvolume");
					this.Onpost_estvolumeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dtupdated", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> dtupdated
		{
			get
			{
				return this._dtupdated;
			}
			set
			{
				if ((this._dtupdated != value))
				{
					this.OndtupdatedChanging(value);
					this.SendPropertyChanging();
					this._dtupdated = value;
					this.SendPropertyChanged("dtupdated");
					this.OndtupdatedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContactName", DbType="NVarChar(50)")]
		public string ContactName
		{
			get
			{
				return this._ContactName;
			}
			set
			{
				if ((this._ContactName != value))
				{
					this.OnContactNameChanging(value);
					this.SendPropertyChanging();
					this._ContactName = value;
					this.SendPropertyChanged("ContactName");
					this.OnContactNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContactInitials", DbType="NVarChar(5)")]
		public string ContactInitials
		{
			get
			{
				return this._ContactInitials;
			}
			set
			{
				if ((this._ContactInitials != value))
				{
					this.OnContactInitialsChanging(value);
					this.SendPropertyChanging();
					this._ContactInitials = value;
					this.SendPropertyChanged("ContactInitials");
					this.OnContactInitialsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EMail", DbType="NVarChar(50)")]
		public string EMail
		{
			get
			{
				return this._EMail;
			}
			set
			{
				if ((this._EMail != value))
				{
					this.OnEMailChanging(value);
					this.SendPropertyChanging();
					this._EMail = value;
					this.SendPropertyChanged("EMail");
					this.OnEMailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompanyName", DbType="NVarChar(50)")]
		public string CompanyName
		{
			get
			{
				return this._CompanyName;
			}
			set
			{
				if ((this._CompanyName != value))
				{
					this.OnCompanyNameChanging(value);
					this.SendPropertyChanging();
					this._CompanyName = value;
					this.SendPropertyChanged("CompanyName");
					this.OnCompanyNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MainEmail", DbType="NVarChar(50)")]
		public string MainEmail
		{
			get
			{
				return this._MainEmail;
			}
			set
			{
				if ((this._MainEmail != value))
				{
					this.OnMainEmailChanging(value);
					this.SendPropertyChanging();
					this._MainEmail = value;
					this.SendPropertyChanged("MainEmail");
					this.OnMainEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderNumber", DbType="Int")]
		public System.Nullable<int> OrderNumber
		{
			get
			{
				return this._OrderNumber;
			}
			set
			{
				if ((this._OrderNumber != value))
				{
					this.OnOrderNumberChanging(value);
					this.SendPropertyChanging();
					this._OrderNumber = value;
					this.SendPropertyChanged("OrderNumber");
					this.OnOrderNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HouseBLNUmber", DbType="NVarChar(50)")]
		public string HouseBLNUmber
		{
			get
			{
				return this._HouseBLNUmber;
			}
			set
			{
				if ((this._HouseBLNUmber != value))
				{
					this.OnHouseBLNUmberChanging(value);
					this.SendPropertyChanging();
					this._HouseBLNUmber = value;
					this.SendPropertyChanged("HouseBLNUmber");
					this.OnHouseBLNUmberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomersRef", DbType="NVarChar(20)")]
		public string CustomersRef
		{
			get
			{
				return this._CustomersRef;
			}
			set
			{
				if ((this._CustomersRef != value))
				{
					this.OnCustomersRefChanging(value);
					this.SendPropertyChanging();
					this._CustomersRef = value;
					this.SendPropertyChanged("CustomersRef");
					this.OnCustomersRefChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(100)")]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ISBN", DbType="NVarChar(50)")]
		public string ISBN
		{
			get
			{
				return this._ISBN;
			}
			set
			{
				if ((this._ISBN != value))
				{
					this.OnISBNChanging(value);
					this.SendPropertyChanging();
					this._ISBN = value;
					this.SendPropertyChanged("ISBN");
					this.OnISBNChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_companyid", DbType="Int")]
		public System.Nullable<int> companyid
		{
			get
			{
				return this._companyid;
			}
			set
			{
				if ((this._companyid != value))
				{
					this.OncompanyidChanging(value);
					this.SendPropertyChanging();
					this._companyid = value;
					this.SendPropertyChanged("companyid");
					this.OncompanyidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userid", DbType="Int")]
		public System.Nullable<int> userid
		{
			get
			{
				return this._userid;
			}
			set
			{
				if ((this._userid != value))
				{
					this.OnuseridChanging(value);
					this.SendPropertyChanging();
					this._userid = value;
					this.SendPropertyChanged("userid");
					this.OnuseridChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_updguid", DbType="VarChar(30)")]
		public string updguid
		{
			get
			{
				return this._updguid;
			}
			set
			{
				if ((this._updguid != value))
				{
					this.OnupdguidChanging(value);
					this.SendPropertyChanging();
					this._updguid = value;
					this.SendPropertyChanged("updguid");
					this.OnupdguidChanged();
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
