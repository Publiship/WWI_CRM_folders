using System;
using System.Web;

namespace ParameterPasser
{
	/// <summary>
	/// Summary description for SessionParameterPasser.
	/// </summary>
	public class SessionParameterPasser : BaseParameterPasser
	{
		#region Constructors
		public SessionParameterPasser() : base() {}		
		public SessionParameterPasser(string Url) : base(Url) {}
		#endregion

		#region Properties
		public override string this[string name]
		{
			get
			{
                if (HttpContext.Current != null)
                    if (HttpContext.Current.Session[name] != null)
                    {
                        return HttpContext.Current.Session[name].ToString();
                    }
                    else
                    {
                        return null;
                    }
                else {
                    return null;
                }
                    
      		}
			set
			{
				if (HttpContext.Current != null)
					HttpContext.Current.Session[name] = value;
			}
		}

		public override System.Collections.ICollection Keys
		{
			get
			{
				if (HttpContext.Current != null)
					return HttpContext.Current.Session.Keys;
				else
					return null;
			}
		}

		#endregion
	}
}
