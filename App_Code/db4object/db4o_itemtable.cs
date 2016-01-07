using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Logistics;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

/// <summary>
/// Summary description for db4oject_itemtable
/// </summary>

namespace DAL.Logistics
{
    public class db4o_itemtable
    {
        //classes for saving to db4objects
        //don't use the full classes in wwiprov as we don't need the extra overhead to store locally
       
        //end classes

        public db4o_itemtable()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// returns the item and all it's associated cartons
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
        /// returns titleid if save is successful
        /// </summary>
        /// <param name="booktitle"></param>
        /// <returns></returns>
        public static int InsertItem(ItemTable item)
        {
            db4oManager.WithContainer(container =>
            {
                container.Store(item);
            });

            return item.TitleID;
        }
        /// <summary>
        /// update booktitle based in id
        /// </summary>
        /// <param name="booktitle"></param>
        public static void UpdateItem(ItemTable item)
        {
            db4oManager.WithContainer(container =>
            {
                // The office object that is passed to this procedure is not connected to
                // the db4o container so we first have to do that.
                List<ItemTable> _list = (from ItemTable i in container
                                         where i.TitleID ==  item.TitleID && i.OrderNumber == item.OrderNumber  
                                         select i).ToList();
                if (_list != null)
                {
                    // Then we should pass the properties from the updated object to the 
                    // selected one. 
                    // TODO: Maybe we could make a reflection procedure for this?
                    _list[0].OrderNumber = item.OrderNumber;
                    _list[0].Title = item.Title;
                    _list[0].Copies = item.Copies;
                    _list[0].Isbn = item.Isbn;
                    _list[0].Impression  = item.Impression;
                    _list[0].PONumber = item.PONumber;
                    _list[0].OtherRef = item.OtherRef;
                    _list[0].TotalValue = item.TotalValue;
                    _list[0].SSRNo  = item.SSRNo;
                    _list[0].SSRDate = item.SSRDate;
                    _list[0].PerCopy = item.PerCopy;
                    container.Store(_list[0]);
                }
            });
        }
        //end update title

        /// <summary>
        /// delete based on titleid
        /// </summary>
        /// <param name="booktitle"></param>
        public static void DeleteItem(int index, int ordernumber)
        {
            db4oManager.WithContainer(container =>
            {
                // The office object that is passed to this procedure is not connected tot 
                // the db4o container so we first have to do that.
                //List<ItemTable> _list = (from ItemTable i in container
                //                         where i.TitleID == item.TitleID && i.OrderNumber == item.OrderNumber
                //                         select i).ToList();
                List<ItemTable> _list = (from ItemTable i in container
                                         where i.TitleID == index && i.OrderNumber == ordernumber 
                                         select i).ToList();
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