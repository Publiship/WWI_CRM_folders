using System;
using System.Collections;
using System.Web;

namespace ParameterPasser
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public abstract class BaseParameterPasser
	{
		#region Member Variables
		private string url = string.Empty;
		#endregion

		#region Constructors
		public BaseParameterPasser()
		{
			if (HttpContext.Current != null)
				url = HttpContext.Current.Request.Url.ToString();
		}

		public BaseParameterPasser(string Url)
		{
			this.url = Url;
		}
		#endregion

		#region Methods
		public virtual void PassParameters()
		{
			if (HttpContext.Current != null)
				HttpContext.Current.Response.Redirect(Url);
		}
		#endregion

		#region Properties
		public string Url
		{
			get
			{
				return url;
			}
			set
			{
				url = value;
			}
		}

		public abstract string this[string name]
		{
			get;
			set;
		}

		public abstract ICollection Keys
		{
			get;
		}
		#endregion
	}
}
