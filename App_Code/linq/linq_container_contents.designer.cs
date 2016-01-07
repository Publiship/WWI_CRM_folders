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
	public partial class linq_container_contentsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public linq_container_contentsDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public linq_container_contentsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_container_contentsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_container_contentsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_container_contentsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.view_container_contents", IsComposable=true)]
		public IQueryable<view_container_contentsResult> view_container_contents([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="SmallDateTime")] System.Nullable<System.DateTime> startex, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(50)")] string containerno)
		{
			return this.CreateMethodCallQuery<view_container_contentsResult>(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), startex, containerno);
		}
	}
	
	public partial class view_container_contentsResult
	{
		
		private string _Title;
		
		private System.Nullable<int> _Copies;
		
		private string _ISBN;
		
		private System.Nullable<System.DateTime> _ExWorksDate;
		
		private System.Nullable<System.DateTime> _BookingReceived;
		
		private System.Nullable<System.DateTime> _CargoReady;
		
		private string _vessel_name;
		
		private System.Nullable<System.DateTime> _ETS;
		
		private System.Nullable<System.DateTime> _ETA;
		
		private string _ContainerNumber;
		
		private string _current_status;
		
		private System.Nullable<System.DateTime> _StatusDate;
		
		private System.Nullable<System.DateTime> _status_on;
		
		private System.Nullable<int> _OrderNumber;
		
		private System.Nullable<int> _DeliveryAddress;
		
		private System.Nullable<decimal> _EstPPC;
		
		private System.Nullable<decimal> _ActualPPC;
		
		private System.Nullable<int> _CompanyID;
		
		private string _client_name;
		
		private string _delivery_to;
		
		private string _delivery_addr1;
		
		private string _delivery_postcode;
		
		private string _delivery_telno;
		
		private System.Nullable<bool> _JobClosed;
		
		private System.Nullable<int> _TitleID;
		
		private System.Nullable<int> _OrderID;
		
		private string _RemarksToCustomer;
		
		private System.Nullable<int> _FullPallets;
		
		private System.Nullable<int> _CartonsPerFullPallet;
		
		private System.Nullable<int> _PartPallets;
		
		private System.Nullable<int> _CartonsPerPartPallet;
		
		private System.Nullable<float> _TotalConsignmentWeight;
		
		private System.Nullable<float> _TotalConsignmentCube;
		
		private int _SubDeliveryID;
		
		private System.Nullable<int> _ContainerID;
		
		private string _OrderIx;
		
		public view_container_contentsResult()
		{
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
					this._Title = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Copies", DbType="Int")]
		public System.Nullable<int> Copies
		{
			get
			{
				return this._Copies;
			}
			set
			{
				if ((this._Copies != value))
				{
					this._Copies = value;
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
					this._ISBN = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExWorksDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> ExWorksDate
		{
			get
			{
				return this._ExWorksDate;
			}
			set
			{
				if ((this._ExWorksDate != value))
				{
					this._ExWorksDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BookingReceived", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> BookingReceived
		{
			get
			{
				return this._BookingReceived;
			}
			set
			{
				if ((this._BookingReceived != value))
				{
					this._BookingReceived = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CargoReady", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> CargoReady
		{
			get
			{
				return this._CargoReady;
			}
			set
			{
				if ((this._CargoReady != value))
				{
					this._CargoReady = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_vessel_name", DbType="NVarChar(50)")]
		public string vessel_name
		{
			get
			{
				return this._vessel_name;
			}
			set
			{
				if ((this._vessel_name != value))
				{
					this._vessel_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ETS", DbType="DateTime")]
		public System.Nullable<System.DateTime> ETS
		{
			get
			{
				return this._ETS;
			}
			set
			{
				if ((this._ETS != value))
				{
					this._ETS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ETA", DbType="DateTime")]
		public System.Nullable<System.DateTime> ETA
		{
			get
			{
				return this._ETA;
			}
			set
			{
				if ((this._ETA != value))
				{
					this._ETA = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContainerNumber", DbType="NVarChar(50)")]
		public string ContainerNumber
		{
			get
			{
				return this._ContainerNumber;
			}
			set
			{
				if ((this._ContainerNumber != value))
				{
					this._ContainerNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_current_status", DbType="NVarChar(50)")]
		public string current_status
		{
			get
			{
				return this._current_status;
			}
			set
			{
				if ((this._current_status != value))
				{
					this._current_status = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StatusDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> StatusDate
		{
			get
			{
				return this._StatusDate;
			}
			set
			{
				if ((this._StatusDate != value))
				{
					this._StatusDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_status_on", DbType="DateTime")]
		public System.Nullable<System.DateTime> status_on
		{
			get
			{
				return this._status_on;
			}
			set
			{
				if ((this._status_on != value))
				{
					this._status_on = value;
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
					this._OrderNumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DeliveryAddress", DbType="Int")]
		public System.Nullable<int> DeliveryAddress
		{
			get
			{
				return this._DeliveryAddress;
			}
			set
			{
				if ((this._DeliveryAddress != value))
				{
					this._DeliveryAddress = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EstPPC", DbType="Decimal(18,4)")]
		public System.Nullable<decimal> EstPPC
		{
			get
			{
				return this._EstPPC;
			}
			set
			{
				if ((this._EstPPC != value))
				{
					this._EstPPC = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActualPPC", DbType="Decimal(18,0)")]
		public System.Nullable<decimal> ActualPPC
		{
			get
			{
				return this._ActualPPC;
			}
			set
			{
				if ((this._ActualPPC != value))
				{
					this._ActualPPC = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompanyID", DbType="Int")]
		public System.Nullable<int> CompanyID
		{
			get
			{
				return this._CompanyID;
			}
			set
			{
				if ((this._CompanyID != value))
				{
					this._CompanyID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_client_name", DbType="NVarChar(50)")]
		public string client_name
		{
			get
			{
				return this._client_name;
			}
			set
			{
				if ((this._client_name != value))
				{
					this._client_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_delivery_to", DbType="NVarChar(50)")]
		public string delivery_to
		{
			get
			{
				return this._delivery_to;
			}
			set
			{
				if ((this._delivery_to != value))
				{
					this._delivery_to = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_delivery_addr1", DbType="NVarChar(40)")]
		public string delivery_addr1
		{
			get
			{
				return this._delivery_addr1;
			}
			set
			{
				if ((this._delivery_addr1 != value))
				{
					this._delivery_addr1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_delivery_postcode", DbType="NVarChar(50)")]
		public string delivery_postcode
		{
			get
			{
				return this._delivery_postcode;
			}
			set
			{
				if ((this._delivery_postcode != value))
				{
					this._delivery_postcode = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_delivery_telno", DbType="NVarChar(20)")]
		public string delivery_telno
		{
			get
			{
				return this._delivery_telno;
			}
			set
			{
				if ((this._delivery_telno != value))
				{
					this._delivery_telno = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_JobClosed", DbType="Bit")]
		public System.Nullable<bool> JobClosed
		{
			get
			{
				return this._JobClosed;
			}
			set
			{
				if ((this._JobClosed != value))
				{
					this._JobClosed = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TitleID", DbType="Int")]
		public System.Nullable<int> TitleID
		{
			get
			{
				return this._TitleID;
			}
			set
			{
				if ((this._TitleID != value))
				{
					this._TitleID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderID", DbType="Int")]
		public System.Nullable<int> OrderID
		{
			get
			{
				return this._OrderID;
			}
			set
			{
				if ((this._OrderID != value))
				{
					this._OrderID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RemarksToCustomer", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string RemarksToCustomer
		{
			get
			{
				return this._RemarksToCustomer;
			}
			set
			{
				if ((this._RemarksToCustomer != value))
				{
					this._RemarksToCustomer = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FullPallets", DbType="Int")]
		public System.Nullable<int> FullPallets
		{
			get
			{
				return this._FullPallets;
			}
			set
			{
				if ((this._FullPallets != value))
				{
					this._FullPallets = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CartonsPerFullPallet", DbType="Int")]
		public System.Nullable<int> CartonsPerFullPallet
		{
			get
			{
				return this._CartonsPerFullPallet;
			}
			set
			{
				if ((this._CartonsPerFullPallet != value))
				{
					this._CartonsPerFullPallet = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PartPallets", DbType="Int")]
		public System.Nullable<int> PartPallets
		{
			get
			{
				return this._PartPallets;
			}
			set
			{
				if ((this._PartPallets != value))
				{
					this._PartPallets = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CartonsPerPartPallet", DbType="Int")]
		public System.Nullable<int> CartonsPerPartPallet
		{
			get
			{
				return this._CartonsPerPartPallet;
			}
			set
			{
				if ((this._CartonsPerPartPallet != value))
				{
					this._CartonsPerPartPallet = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TotalConsignmentWeight", DbType="Real")]
		public System.Nullable<float> TotalConsignmentWeight
		{
			get
			{
				return this._TotalConsignmentWeight;
			}
			set
			{
				if ((this._TotalConsignmentWeight != value))
				{
					this._TotalConsignmentWeight = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TotalConsignmentCube", DbType="Real")]
		public System.Nullable<float> TotalConsignmentCube
		{
			get
			{
				return this._TotalConsignmentCube;
			}
			set
			{
				if ((this._TotalConsignmentCube != value))
				{
					this._TotalConsignmentCube = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SubDeliveryID", DbType="Int NOT NULL")]
		public int SubDeliveryID
		{
			get
			{
				return this._SubDeliveryID;
			}
			set
			{
				if ((this._SubDeliveryID != value))
				{
					this._SubDeliveryID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContainerID", DbType="Int")]
		public System.Nullable<int> ContainerID
		{
			get
			{
				return this._ContainerID;
			}
			set
			{
				if ((this._ContainerID != value))
				{
					this._ContainerID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderIx", DbType="VarChar(120)")]
		public string OrderIx
		{
			get
			{
				return this._OrderIx;
			}
			set
			{
				if ((this._OrderIx != value))
				{
					this._OrderIx = value;
				}
			}
		}
	}
}
#pragma warning restore 1591