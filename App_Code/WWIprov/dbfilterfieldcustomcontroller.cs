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
    /// custom Controller class for db_filter_fields
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class dbcustomfilterfieldcontroller
    {
        // Preload our schema..
        DbFilterField thisSchemaLoad = new DbFilterField();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
                if (userName.Length == 0)
                {
                    if (System.Web.HttpContext.Current != null)
                    {
                        userName = System.Web.HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        userName = System.Threading.Thread.CurrentPrincipal.Identity.Name;
                    }
                }
                return userName;
            }
        }
                
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DbFilterFieldCollection FetchByActive()
        {
            DbFilterFieldCollection coll = new DbFilterFieldCollection().Where("is_quick_filter", true).Where("applies_to", "shipment").OrderByAsc("sort_order").Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DbFilterFieldCollection FetchByActiveAnonymous()
        {
            DbFilterFieldCollection coll = new DbFilterFieldCollection().Where("is_quick_filter", true).Where("applies_to", "shipment").Where("login_required", false).OrderByAsc("sort_order").Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DbFilterFieldCollection FetchByAdvanced()
        {
            DbFilterFieldCollection coll = new DbFilterFieldCollection().Where("applies_to", "shipment").Where("is_active", true).OrderByAsc("sort_order").Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DbFilterFieldCollection FetchByActivePricer()
        {
            DbFilterFieldCollection coll = new DbFilterFieldCollection().Where("applies_to","pricer").Where("is_active", true).OrderByAsc("sort_order").Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DbFilterFieldCollection FetchByActiveAdvance()
        {
            DbFilterFieldCollection coll = new DbFilterFieldCollection().Where("applies_to", "advance").Where("is_active", true).OrderByAsc("sort_order").Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DbFilterFieldCollection FetchByDeliveries()
        {
            DbFilterFieldCollection coll = new DbFilterFieldCollection().Where("applies_to", "delivery").Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DbFilterFieldCollection FetchByContainers()
        {
            DbFilterFieldCollection coll = new DbFilterFieldCollection().Where("applies_to", "container").Load();
            return coll;
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DbFilterFieldCollection FetchBySourceForm(string sourceform)
        {
            //string _src = wwi_security.DecryptString(sourceform, "publiship");
            
            DbFilterFieldCollection coll = new DbFilterFieldCollection().Where("applies_to", sourceform).Where("is_active", true).OrderByAsc("sort_order").Load();
            return coll;
        }
    }
}