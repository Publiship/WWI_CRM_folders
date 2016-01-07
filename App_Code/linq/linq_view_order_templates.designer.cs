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
	public partial class linq_view_order_templatesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public linq_view_order_templatesDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public linq_view_order_templatesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_view_order_templatesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_view_order_templatesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_view_order_templatesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<view_order_template> view_order_templates
		{
			get
			{
				return this.GetTable<view_order_template>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.view_order_template")]
	public partial class view_order_template
	{
		
		private int _OrderTemplateID;
		
		private string _TemplateName;
		
		private string _OfficeIndicator;
		
		private System.Nullable<bool> _PublishipOrder;
		
		private string _CustomersRef;
		
		private string _OrderController;
		
		private string _OpsController;
		
		private string _ContactName;
		
		private string _OriginPlace;
		
		private string _OriginPort;
		
		private string _DestinationPort;
		
		private string _FinalDestination;
		
		private string _ClientName;
		
		private string _PrinterName;
		
		private string _ConsigneeName;
		
		private string _OriginAgentName;
		
		private string _NotifyName;
		
		private string _OriginController;
		
		private string _DestAgentName;
		
		private string _DestController;
		
		private string _ClearingAgentName;
		
		private string _OnCarriageName;
		
		public view_order_template()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderTemplateID", DbType="Int NOT NULL")]
		public int OrderTemplateID
		{
			get
			{
				return this._OrderTemplateID;
			}
			set
			{
				if ((this._OrderTemplateID != value))
				{
					this._OrderTemplateID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TemplateName", DbType="NVarChar(50)")]
		public string TemplateName
		{
			get
			{
				return this._TemplateName;
			}
			set
			{
				if ((this._TemplateName != value))
				{
					this._TemplateName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OfficeIndicator", DbType="NVarChar(50)")]
		public string OfficeIndicator
		{
			get
			{
				return this._OfficeIndicator;
			}
			set
			{
				if ((this._OfficeIndicator != value))
				{
					this._OfficeIndicator = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PublishipOrder", DbType="Bit")]
		public System.Nullable<bool> PublishipOrder
		{
			get
			{
				return this._PublishipOrder;
			}
			set
			{
				if ((this._PublishipOrder != value))
				{
					this._PublishipOrder = value;
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
					this._CustomersRef = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderController", DbType="NVarChar(50)")]
		public string OrderController
		{
			get
			{
				return this._OrderController;
			}
			set
			{
				if ((this._OrderController != value))
				{
					this._OrderController = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OpsController", DbType="NVarChar(50)")]
		public string OpsController
		{
			get
			{
				return this._OpsController;
			}
			set
			{
				if ((this._OpsController != value))
				{
					this._OpsController = value;
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
					this._ContactName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OriginPlace", DbType="NVarChar(30)")]
		public string OriginPlace
		{
			get
			{
				return this._OriginPlace;
			}
			set
			{
				if ((this._OriginPlace != value))
				{
					this._OriginPlace = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OriginPort", DbType="NVarChar(30)")]
		public string OriginPort
		{
			get
			{
				return this._OriginPort;
			}
			set
			{
				if ((this._OriginPort != value))
				{
					this._OriginPort = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DestinationPort", DbType="NVarChar(30)")]
		public string DestinationPort
		{
			get
			{
				return this._DestinationPort;
			}
			set
			{
				if ((this._DestinationPort != value))
				{
					this._DestinationPort = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FinalDestination", DbType="NVarChar(30)")]
		public string FinalDestination
		{
			get
			{
				return this._FinalDestination;
			}
			set
			{
				if ((this._FinalDestination != value))
				{
					this._FinalDestination = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClientName", DbType="NVarChar(50)")]
		public string ClientName
		{
			get
			{
				return this._ClientName;
			}
			set
			{
				if ((this._ClientName != value))
				{
					this._ClientName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PrinterName", DbType="NVarChar(50)")]
		public string PrinterName
		{
			get
			{
				return this._PrinterName;
			}
			set
			{
				if ((this._PrinterName != value))
				{
					this._PrinterName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ConsigneeName", DbType="NVarChar(50)")]
		public string ConsigneeName
		{
			get
			{
				return this._ConsigneeName;
			}
			set
			{
				if ((this._ConsigneeName != value))
				{
					this._ConsigneeName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OriginAgentName", DbType="NVarChar(50)")]
		public string OriginAgentName
		{
			get
			{
				return this._OriginAgentName;
			}
			set
			{
				if ((this._OriginAgentName != value))
				{
					this._OriginAgentName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NotifyName", DbType="NVarChar(50)")]
		public string NotifyName
		{
			get
			{
				return this._NotifyName;
			}
			set
			{
				if ((this._NotifyName != value))
				{
					this._NotifyName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OriginController", DbType="NVarChar(50)")]
		public string OriginController
		{
			get
			{
				return this._OriginController;
			}
			set
			{
				if ((this._OriginController != value))
				{
					this._OriginController = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DestAgentName", DbType="NVarChar(50)")]
		public string DestAgentName
		{
			get
			{
				return this._DestAgentName;
			}
			set
			{
				if ((this._DestAgentName != value))
				{
					this._DestAgentName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DestController", DbType="NVarChar(50)")]
		public string DestController
		{
			get
			{
				return this._DestController;
			}
			set
			{
				if ((this._DestController != value))
				{
					this._DestController = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClearingAgentName", DbType="NVarChar(50)")]
		public string ClearingAgentName
		{
			get
			{
				return this._ClearingAgentName;
			}
			set
			{
				if ((this._ClearingAgentName != value))
				{
					this._ClearingAgentName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OnCarriageName", DbType="NVarChar(50)")]
		public string OnCarriageName
		{
			get
			{
				return this._OnCarriageName;
			}
			set
			{
				if ((this._OnCarriageName != value))
				{
					this._OnCarriageName = value;
				}
			}
		}
	}
}
#pragma warning restore 1591