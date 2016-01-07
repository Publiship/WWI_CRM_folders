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
	public partial class linq_aggregate_containers_udfDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public linq_aggregate_containers_udfDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public linq_aggregate_containers_udfDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_aggregate_containers_udfDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_aggregate_containers_udfDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public linq_aggregate_containers_udfDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.aggregate_containers_by_ets", IsComposable=true)]
		public IQueryable<aggregate_containers_by_etsResult> aggregate_containers_by_ets([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="SmallDateTime")] System.Nullable<System.DateTime> beginETS, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="SmallDateTime")] System.Nullable<System.DateTime> endETS)
		{
			return this.CreateMethodCallQuery<aggregate_containers_by_etsResult>(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), beginETS, endETS);
		}
	}
	
	public partial class aggregate_containers_by_etsResult
	{
		
		private System.Nullable<int> _ContainerIdx;
		
		private System.Nullable<int> _ContainerCount;
		
		private System.Nullable<double> _SumWeight;
		
		private System.Nullable<double> _SumCbm;
		
		private System.Nullable<int> _SumPackages;
		
		private string _ClientName;
		
		private System.Nullable<int> _DeliveryAddress;
		
		public aggregate_containers_by_etsResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContainerIdx", DbType="Int")]
		public System.Nullable<int> ContainerIdx
		{
			get
			{
				return this._ContainerIdx;
			}
			set
			{
				if ((this._ContainerIdx != value))
				{
					this._ContainerIdx = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContainerCount", DbType="Int")]
		public System.Nullable<int> ContainerCount
		{
			get
			{
				return this._ContainerCount;
			}
			set
			{
				if ((this._ContainerCount != value))
				{
					this._ContainerCount = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SumWeight", DbType="Float")]
		public System.Nullable<double> SumWeight
		{
			get
			{
				return this._SumWeight;
			}
			set
			{
				if ((this._SumWeight != value))
				{
					this._SumWeight = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SumCbm", DbType="Float")]
		public System.Nullable<double> SumCbm
		{
			get
			{
				return this._SumCbm;
			}
			set
			{
				if ((this._SumCbm != value))
				{
					this._SumCbm = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SumPackages", DbType="Int")]
		public System.Nullable<int> SumPackages
		{
			get
			{
				return this._SumPackages;
			}
			set
			{
				if ((this._SumPackages != value))
				{
					this._SumPackages = value;
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
	}
}
#pragma warning restore 1591