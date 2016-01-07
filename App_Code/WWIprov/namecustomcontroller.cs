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


namespace DAL.Logistics
{
    /// <summary>
    /// ordertablecustomcontroller
    /// custom functions for ordertable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class namecustomcontroller
    {
        // Preload our schema..
        //NameAndAddressBook thisSchemaLoad = new NameAndAddressBook();
        
        /// <summary>
        /// returns group id for specified company id
        /// group id is used to group companies in the database with multiple addresses e.g. hachette
        /// </summary>
        /// <param name="requestCompanyId">int unique company id</param>
        /// <returns>string company group id</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public string get_company_group(int requestCompanyId)
        {
            string _cg = "0";
            string[] _tcols = { "NameAndAddressBook.CompanyId", "NameAndAddressBook.Pricer_Group" };
            SubSonic.SqlQuery _qry = DAL.Logistics.DB.Select(_tcols).From(DAL.Logistics.Tables.NameAndAddressBook).Where("CompanyID").IsEqualTo(requestCompanyId);
            DataSet _dt = _qry.ExecuteDataSet();

            if (_dt.Tables.Count > 0 && _dt.Tables[0].Rows.Count > 0)
            {
                _cg = !string.IsNullOrEmpty(_dt.Tables[0].Rows[0]["Pricer_Group"].ToString()) ? _dt.Tables[0].Rows[0]["Pricer_Group"].ToString() : "0";
            }

            return _cg;
        }
        //end
    }
}