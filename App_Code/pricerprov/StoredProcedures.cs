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
namespace DAL.Pricer{
    public partial class SPs{
        
        /// <summary>
        /// Creates an object wrapper for the price_by_companyid_ext Procedure
        /// </summary>
        public static StoredProcedure PriceByCompanyidExt(int? companyid)
        {
            SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("price_by_companyid_ext", DataService.GetInstance("pricerprov"), "pricer_u");
        	
            sp.Command.AddParameter("@companyid", companyid, DbType.Int32, 0, 10);
        	
            return sp;
        }
        
    }
    
}