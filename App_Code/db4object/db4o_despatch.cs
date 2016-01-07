using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Logistics;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

/// <summary>
/// Summary description for db4o_despatch
/// </summary>
public class db4o_despatch
{
    //classes for saving to db4objects
    //don't use the full classes in wwiprov as we don't need the extra overhead to store locally
    //end classes

	public db4o_despatch()
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
    public static List<DespatchNoteItem> SelectById(int despatchid)
    {
        List<DespatchNoteItem> _list = new List<DespatchNoteItem>();

        db4oManager.WithContainer(container =>
        {
            _list = (from DespatchNoteItem i in container
                     where i.DespatchNoteId == despatchid 
                     select i).ToList();
        });
        return _list;
    }
    public static List<DespatchNoteItem> SelectByUserSession(int despatchid)
    {
        //use itemid to track user as db4o generates it's own indexing so itemid not important
        List<DespatchNoteItem> _list = new List<DespatchNoteItem>();

        db4oManager.WithContainer(container =>
        {
            _list = (from DespatchNoteItem i in container
                     where i.DespatchNoteId == despatchid 
                     select i).ToList();
        });
        return _list;
    }
    //end select title
    /// <summary>
    /// returns titleid if save is successful
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static int InsertItem(DespatchNoteItem item)
    {
        item.ItemId = NextItemId();
 
        db4oManager.WithContainer(container =>
        {
            container.Store(item);
        });

        return item.ItemId;
    }
    /// <summary>
    /// update booktitle based in id
    /// </summary>
    /// <param name="item"></param>
    public static void UpdateItem(DespatchNoteItem item)
    {
        db4oManager.WithContainer(container =>
        {
            // The office object that is passed to this procedure is not connected to
            // the db4o container so we first have to do that.
            List<DespatchNoteItem> _list = (from DespatchNoteItem i in container
                                     where i.ItemId == item.ItemId && i.DespatchNoteId == item.DespatchNoteId 
                                     select i).ToList();
            if (_list != null)
            {
                // Then we should pass the properties from the updated object to the 
                // selected one. 
                // TODO: Maybe we could make a reflection procedure for this?
                _list[0].BuyersOrderNumber = item.BuyersOrderNumber;
                _list[0].PrintersJobNumber = item.PrintersJobNumber;
                _list[0].PublishipRef  = item.PublishipRef;
                _list[0].Isbn = item.Isbn;
                _list[0].Title = item.Title;
                _list[0].Impression = item.Impression;
                _list[0].TotalQty = item.TotalQty;
                _list[0].FullPallets = item.FullPallets;
                _list[0].UnitsFull = item.UnitsFull;
                _list[0].PartPallets = item.PartPallets;
                _list[0].UnitsPart = item.UnitsPart;
                _list[0].ParcelCount = item.ParcelCount;
                _list[0].UnitsPerParcel = item.UnitsPerParcel;
                _list[0].ParcelsPerLayer = item.ParcelsPerLayer;
                _list[0].OddsCount = item.OddsCount;
                _list[0].Height = item.Height;
                _list[0].Width = item.Width;
                _list[0].Depth = item.Depth;
                _list[0].UnitNetWeight = item.UnitNetWeight;
                _list[0].ParcelHeight = item.ParcelHeight;
                _list[0].ParcelWidth = item.ParcelWidth;
                _list[0].ParcelDepth = item.ParcelDepth;
                _list[0].ParcelUnitgrossweight = item.ParcelUnitgrossweight;

                container.Store(_list[0]);
            }
        });
    }
    //end update title

    /// <summary>
    /// delete based on itemid
    /// </summary>
    /// <param name="booktitle"></param>
    public static void DeleteItem(int index, int despatchid)
    {
        db4oManager.WithContainer(container =>
        {
            // The office object that is passed to this procedure is not connected tot 
            // the db4o container so we first have to do that.
            //List<ItemTable> _list = (from ItemTable i in container
            //                         where i.TitleID == item.TitleID && i.OrderNumber == item.OrderNumber
            //                         select i).ToList();
            List<DespatchNoteItem> _list = (from DespatchNoteItem i in container
                                     where i.ItemId == index && i.DespatchNoteId == despatchid
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

    public static int NextItemId()
    {
        int _id = 0;

        db4oManager.WithContainer(container =>
        {
            _id = (from DespatchNoteItem i in container select i.ItemId).DefaultIfEmpty(_id).Max();
            _id += 1;
        });

        return _id;
    }
    //end next item id
}
