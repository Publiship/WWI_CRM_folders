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
    public partial class publishipadvanceOrderTableCustomcontroller
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
        public PublishipAdvanceOrderTableCollection FetchByOrderID(object OrderID)
        {
            PublishipAdvanceOrderTableCollection coll = new PublishipAdvanceOrderTableCollection().Where("OrderID", OrderID).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PublishipAdvanceOrderTableCollection FetchByContactID(object ContactID)
        {
            PublishipAdvanceOrderTableCollection coll = new PublishipAdvanceOrderTableCollection().Where("ContactID", ContactID).Load();
            return coll;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool DeleteByOrderID(object OrderID)
        {
            return (PublishipAdvanceOrderTable.Delete(OrderID) == 1);
        }

        /// <summary>
        /// Inserts a partial record e.g from client input forms, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public Int32 PartialInsert(string OrderNumber, string Payee, string DeliveryAddress, string Country, int? CompanyID, int? ContactID, DateTime? CargoReady , string Fao)
        {
            PublishipAdvanceOrderTable item = new PublishipAdvanceOrderTable();

            item.OrderNumber = OrderNumber != null? OrderNumber: "[new order]";

            item.DateOrderReceived = DateTime.Now; //DateTime.Now.ToString("yyyy-MM-dd"); //to avoid Conversion failed when converting date and/or time from character string.

            item.Payee = Payee;

            item.DeliveryAddress = DeliveryAddress;

            item.DestinationCountry = Country;

            item.CompanyID = CompanyID;

            item.ContactID = ContactID;

            //if (!string.IsNullOrEmpty(CargoReady))
            //{
                //DateTime _dt = new DateTime();
                //_dt = DateTime.ParseExact(CargoReady, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat); 
            item.CargoReadyDate = CargoReady; //Convert.ToDateTime(CargoReady).ToString("yyyy-MM-dd");
            //}

            item.Fao = Fao;
            
            item.Save(UserName);

            Int32 _returnId = (Int32)item.GetPrimaryKeyValue();
            return _returnId;
        }
        //end insert

        /// <summary>
        /// partial updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void PartialUpdate(int OrderID, string OrderNumber, string Payee, string DeliveryAddress, string Country, int? CompanyID, int? ContactID, DateTime? CargoReady, string Fao)
        {
            PublishipAdvanceOrderTable item = new PublishipAdvanceOrderTable();
            item.MarkOld();
            item.IsLoaded = true;

            item.OrderID = OrderID;

            item.OrderNumber = OrderNumber;

            item.Payee = Payee;

            item.DeliveryAddress = DeliveryAddress;

            item.DestinationCountry = Country;

            item.CompanyID = CompanyID;

            item.ContactID = ContactID;

            //if (!string.IsNullOrEmpty(CargoReady))
            //{
            //    item.CargoReadyDate = Convert.ToDateTime(CargoReady).ToString("yyyy-MM-dd");
            //}
            item.CargoReadyDate = CargoReady;

            item.Fao = Fao;

            item.Save(UserName);
        }
        //end partial update

        /// <summary>
        /// partial updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void UpdateTitleCount(int OrderID, int newValue)
        {
            PublishipAdvanceOrderTable item = new PublishipAdvanceOrderTable();
            item.MarkOld();
            item.IsLoaded = true;

            item.OrderID = OrderID;
            int _v = item.Titles ?? 0;
            item.Titles = _v + newValue >= 0 ? _v + newValue: 0;
            
            item.Save(UserName);
        }
        //end update
        /// <summary>
        /// partial updates a record, can be used with the Object Data Source
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void UpdateCartonCount(int OrderID, int newValue)
        {
            PublishipAdvanceOrderTable item = new PublishipAdvanceOrderTable();
            item.MarkOld();
            item.IsLoaded = true;

            item.OrderID = OrderID;
            int _v = item.Cartons ?? 0;
            item.Cartons = _v + newValue >= 0 ? _v + newValue : 0;

            item.Save(UserName);
        }
        //end update
    }
}