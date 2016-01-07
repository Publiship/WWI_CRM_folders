using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Logistics;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

/// <summary>
/// Summary description for db4oject_advance
/// </summary>

namespace DAL.Logistics
{
    public class db4o_ordertable
    {
        public db4o_ordertable()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// returns the order
        /// </summary>
        /// <param name="thetitle"></param>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public static List<ItemTable> SelectByOrderNumber(int ordernumber)
        {
            List<ItemTable> _list = new List<ItemTable>();

            db4oManager.WithContainer(container =>
            {
                _list = (from ItemTable i in container
                         where i.OrderNumber == ordernumber
                         select i).ToList();
                //_items = container.Query<ItemTable>(delegate(ItemTable _item)
                //{
                //    return _item.OrderNumber == orderNumber;
                //});
            });
            return _list;
        }
        public static List<ItemTable> SelectByUserSession(int ordernumber, int userid)
        {
            //use titleID to track user as db4o generates it's own indexing so titleID not important
            List<ItemTable> _list = new List<ItemTable>();

            db4oManager.WithContainer(container =>
            {
                _list = (from ItemTable i in container
                         where i.OrderNumber == ordernumber && i.TitleID == userid
                         select i).ToList();
                //_items = container.Query<ItemTable>(delegate(ItemTable _item)
                //{
                //    return _item.OrderNumber == orderNumber;
                //});
            });
            return _list;
        }
        //end select title
        /// <summary>
        /// returns orderid if save is successful
        /// </summary>
        /// <param name="booktitle"></param>
        /// <returns></returns>
        public static int InsertOrder(OrderTable order)
        {
            db4oManager.WithContainer(container =>
            {
                container.Store(order);
            });

            return order.OrderID;
        }
        /// <summary>
        /// update booktitle based in id
        /// </summary>
        /// <param name="booktitle"></param>
        public static void UpdateOrder(OrderTable order)
        {
            db4oManager.WithContainer(container =>
            {
                // The office object that is passed to this procedure is not connected to
                // the db4o container so we first have to do that.
                List<OrderTable> _list = (from OrderTable o in container
                                         where o.OrderID == order.OrderID  && o.OrderNumber == order.OrderNumber
                                         select o).ToList();
                if (_list != null)
                {
                    // Then we should pass the properties from the updated object to the 
                    // selected one. 
                    // TODO: Maybe we could make a reflection procedure for this?
                    //defaults
                    _list[0].OrderNumber = order.OrderNumber;
                    _list[0].OfficeIndicator = order.OfficeIndicator;
                    _list[0].DateOrderCreated = order.DateOrderCreated;
                    _list[0].EWDLastUpdated = order.EWDLastUpdated; //exworks last updated
                    //dlls
                    _list[0].OrderControllerID = order.OrderControllerID;
                    _list[0].OperationsControllerID = order.OperationsControllerID;
                    _list[0].CompanyID = order.CompanyID;
                    _list[0].CountryID = order.CountryID;
                    _list[0].OriginPointID = order.OriginPointID;
                    _list[0].PortID = order.PortID;
                    _list[0].DestinationPortID = order.DestinationPortID;
                    _list[0].FinalDestinationID = order.FinalDestinationID;
                    _list[0].ContactID = order.ContactID;
                    _list[0].PrinterID = order.PrinterID;
                    _list[0].AgentAtOriginID = order.AgentAtOriginID;
                    _list[0].OriginPortControllerID = order.OriginPortControllerID;
                    //dates
                    _list[0].ExWorksDate = order.ExWorksDate;
                    _list[0].CargoReady = order.CargoReady;
                    _list[0].WarehouseDate = order.WarehouseDate;
                    _list[0].BookingReceived = order.BookingReceived;
                    _list[0].DocsApprovedDate = order.DocsApprovedDate;
                    //checkboxes  
                    _list[0].PublishipOrder = order.PublishipOrder;
                    _list[0].HotJob = order.HotJob;
                    _list[0].Palletise = order.Palletise;
                    _list[0].DocsRcdAndApproved = order.DocsRcdAndApproved;
                    _list[0].ExpressBL = order.ExpressBL;
                    _list[0].FumigationCert = order.FumigationCert;
                    _list[0].GSPCert = order.GSPCert;
                    _list[0].PackingDeclaration = order.PackingDeclaration;
                    //memos
                    _list[0].RemarksToCustomer = order.RemarksToCustomer;
                    _list[0].Remarks = order.Remarks;
                    _list[0].OtherDocsRequired = order.OtherDocsRequired;
                    //text boxes
                    _list[0].CustomersRef = order.CustomersRef;
                    _list[0].Sellingrate = order.Sellingrate;
                    _list[0].SellingrateAgent = order.SellingrateAgent;
                    //save               
                    container.Store(_list[0]);
                }
            });
        }
        //end update title

        /// <summary>
        /// delete based on titleid
        /// </summary>
        /// <param name="booktitle"></param>
        public static void DeleteOrder(int orderid)
        {
            db4oManager.WithContainer(container =>
            {
                // The office object that is passed to this procedure is not connected tot 
                // the db4o container so we first have to do that.
                //List<ItemTable> _list = (from ItemTable i in container
                //                         where i.TitleID == item.TitleID && i.OrderNumber == item.OrderNumber
                //                         select i).ToList();
                List<OrderTable> _list = (from OrderTable o in container
                                         where o.OrderID == orderid
                                         select o).ToList();
                if (_list != null)
                //if (_list != null && _list.Count ==1)
                {
                    // Then we should pass the properties from the updated object to the 
                    // selected one. 
                    // TODO: Maybe we could make a reflection procedure for this?
                    container.Delete(_list[_list.Count - 1]);
                }
            });
        }
        //end delete title
    }
}
