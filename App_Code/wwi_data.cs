using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Text;
using SubSonic;
using DAL.Logistics;
/// <summary>
/// Summary description for wwi_data
/// </summary>
public class wwi_data
{
	public wwi_data()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// return next available ordernumber based on officeid
    /// </summary>
    /// <param name="officeid"></param>
    /// <returns></returns>
    public static int next_order_number(int officeid)
    {
        int _nextorderno = 0;
        int _maxorderno = 0;

        switch (officeid)
        {
            case 1: //uk office
                {
                    _maxorderno = new SubSonic.Select(Aggregate.Max(OrderNumberTable.OrderNumberColumn)).
                        From(DAL.Logistics.Tables.OrderNumberTable).ExecuteScalar<int>();   
                    break;

                }
            case 2: //hong kong office
                {
                    _maxorderno = new SubSonic.Select(Aggregate.Max(Hkont.OrderNumberColumn)).
                        From(DAL.Logistics.Tables.Hkont).ExecuteScalar<int>();   
                    break;
                }
            case 3: //singapore office
                {
                    _maxorderno = new SubSonic.Select(Aggregate.Max(Sgont.OrderNumberColumn)).
                        From(DAL.Logistics.Tables.Sgont).ExecuteScalar<int>();   
                    break;
                }
            case 4: //australia order
                {
                    _maxorderno = new SubSonic.Select(Aggregate.Max(Aont.OrderNumberColumn)).
                       From(DAL.Logistics.Tables.Aont).ExecuteScalar<int>();   
                    break;
                }
            case 5: //italy order
                {
                    _maxorderno = new SubSonic.Select(Aggregate.Max(Itont.OrderNumberColumn)).
                       From(DAL.Logistics.Tables.Itont).ExecuteScalar<int>();   
                    break;
                }
            case 6: //us order
                {
                    _maxorderno = new SubSonic.Select(Aggregate.Max(Usont.OrderNumberColumn)).
                       From(DAL.Logistics.Tables.Usont).ExecuteScalar<int>();   
                    break;
                }
            case 7: //netherlands order
                {
                    _maxorderno = new SubSonic.Select(Aggregate.Max(Nlont.OrderNumberColumn)).
                       From(DAL.Logistics.Tables.Nlont).ExecuteScalar<int>();   
                    break;
                }
            case 9: //germany order
                {
                    _maxorderno = new SubSonic.Select(Aggregate.Max(Deont.OrderNumberColumn)).
                       From(DAL.Logistics.Tables.Deont).ExecuteScalar<int>();   
                    break;
                }
            case 10: //thailand order
                {
                    _maxorderno = new SubSonic.Select(Aggregate.Max(Thont.OrderNumberColumn)).
                       From(DAL.Logistics.Tables.Thont).ExecuteScalar<int>();   
                    break;
                }
           case 13: //brazil order 
                {
                    _maxorderno = new SubSonic.Select(Aggregate.Max(Bront.OrderNumberColumn)).
                       From(DAL.Logistics.Tables.Bront).ExecuteScalar<int>();   
                    break;
                }
            default:
                {
                    _maxorderno = -1;
                    break;
                }
        }//end switch

        _nextorderno = _maxorderno + 1; //get next available number or return 0 if not found
        return _nextorderno;
    }
    /// <summary>
    /// append to appropriate ordernumber table depending on office
    /// to appropriate log table e.g. ordernumbertable for uk 
    /// other offices use different table e.g. NLONT (NetherlandsOrderNumberTable
    /// </summary>
    /// <param name="office">ID of requesting office derived from logged in user</param>
    /// <returns>OrderNumber primary key on all office ordernumber tables</returns>
    public static int insert_order_number(int officeid)
    {
        int _neworderno = 0;

        switch (officeid)
        {
            case 1: //uk office
                {
                    //06/12/13 primary key added to ordernumbertable
                    OrderNumberTable _tbl = new OrderNumberTable();
                    _tbl.DateCreated = DateTime.Now;
                    _tbl.RefX = "";
                    _tbl.Save();
                    _neworderno = (int)_tbl.GetPrimaryKeyValue();
                    //why does ordernumbertable not have a primary key?
                    //using (SqlConnection _cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PublishipSQLConnectionString"].ConnectionString)) 
                    //{
                    //    string _sql = "INSERT INTO OrderNumberTable(DateCreated, Ref) VALUES (@DateCreated, @Ref);";
                    //    _cn.Open();
                    //    using (SqlCommand _cmd = new SqlCommand(_sql, _cn))
                    //    {
                    //        _cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now.ToString());
                    //        _cmd.Parameters.AddWithValue("@Ref", "");
                    //
                    //        try
                    //        {
                    //            object _obj =  _cmd.ExecuteScalar();
                    //            _neworderno = wwi_func.vint(_obj.ToString()); 
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            this.dxlblErr.Text = ex.Message.ToString();
                    //           this.dxpnlErr.ClientVisible = true;
                    //        }
                    //        finally
                    //        {
                    //            _cn.Close();
                    //        }//end error trapping
                    //    } //end using command 
                    //} //end using connection
                    break;

                }
            case 2: //hong kong office
                {
                    Hkont _tbl = new Hkont();
                    _tbl.DateCreated = DateTime.Now;
                    _tbl.Save();
                    _neworderno = (int)_tbl.GetPrimaryKeyValue();
                    break;
                }
            case 3: //singapore office
                {
                    Sgont _tbl = new Sgont();
                    _tbl.DateCreated = DateTime.Now;
                    _tbl.Save();
                    _neworderno = (int)_tbl.GetPrimaryKeyValue();
                    break;
                }
            case 4: //australia order
                {
                    Aont _tbl = new Aont();
                    _tbl.DateCreated = DateTime.Now;
                    _tbl.Save();
                    _neworderno = (int)_tbl.GetPrimaryKeyValue();
                    break;
                }
            case 5: //italy order
                {
                    Itont _tbl = new Itont();
                    _tbl.DateCreated = DateTime.Now;
                    _tbl.Save();
                    _neworderno = (int)_tbl.GetPrimaryKeyValue();
                    break;
                }
            case 6: //us order
                {
                    Usont _tbl = new Usont();
                    _tbl.DateCreated = DateTime.Now;
                    _tbl.Save();
                    _neworderno = (int)_tbl.GetPrimaryKeyValue();
                    break;
                }
            case 7: //netherlands order
                {
                    Nlont _tbl = new Nlont();
                    _tbl.DateCreated = DateTime.Now;
                    _tbl.Save();
                    _neworderno = (int)_tbl.GetPrimaryKeyValue();
                    break;
                }
            case 9: //germany order
                {
                    Deont _tbl = new Deont();
                    _tbl.DateCreated = DateTime.Now;
                    _tbl.Save();
                    _neworderno = (int)_tbl.GetPrimaryKeyValue();
                    break;
                }
            case 10: //thailand order
                {
                    Thont _tbl = new Thont();
                    _tbl.DateCreated = DateTime.Now;
                    _tbl.Save();
                    _neworderno = (int)_tbl.GetPrimaryKeyValue();
                    break;
                }
            case 13: //brazil order 
                {
                    Bront _tbl = new Bront();
                    _tbl.DateCreated = DateTime.Now;
                    _tbl.Save();
                    _neworderno = (int)_tbl.GetPrimaryKeyValue();
                    break;
                }
            default:
                {
                    break;
                }
        }

        return _neworderno;
    }
    //end insert order number

    /// <summary>
    /// function to update order number in ordertable
    /// creates a new ordernumber in office table depending on officeid and updates specified order with this ordernumber
    /// </summary>
    /// <param name="orderid">from newly saved order on ordertable</param>
    /// <param name="officeid">from user profile or selected office</param>
    /// <returns></returns>
    public static int update_office_ordernumber(int orderid, int officeid)
    {
        int _newordernumber = 0;
        //get officeid, officeindicator - this will be used to determine the office indicator when the cloned order is saved
        //and it might be different to the original order depending on who is creating the clone
        //get new office indicator
        //using xml file for lookup at the officelookuptable in the database is not correctly configured (check with Dave re: fix)
        //make sure we have a default but it should come from lookup
        string _newoffice = wwi_func.lookup_xml_string("//xml//office_names.xml", "value", officeid.ToString(), "name", "UK Order");

        //create and return a new ordernumber from appropriate table
        _newordernumber = wwi_data.insert_order_number(officeid);
        //update the order just saved
        OrderTable _t = new OrderTable(orderid);
        _t.OrderNumber = _newordernumber;
        _t.OfficeIndicator = _newoffice;
        //update order
        _t.Save();
 
        return _newordernumber;
    }
    //end update office order number
}
