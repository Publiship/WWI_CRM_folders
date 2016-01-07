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
namespace DAL.Logistics
{
    /// <summary>
    /// Controller class for MasterBLTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class MasterBLTableController
    {
        // Preload our schema..
        MasterBLTable thisSchemaLoad = new MasterBLTable();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public MasterBLTableCollection FetchAll()
        {
            MasterBLTableCollection coll = new MasterBLTableCollection();
            Query qry = new Query(MasterBLTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public MasterBLTableCollection FetchByID(object Mblid)
        {
            MasterBLTableCollection coll = new MasterBLTableCollection().Where("MBLID", Mblid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public MasterBLTableCollection FetchByQuery(Query qry)
        {
            MasterBLTableCollection coll = new MasterBLTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Mblid)
        {
            return (MasterBLTable.Delete(Mblid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Mblid)
        {
            return (MasterBLTable.Destroy(Mblid) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MasterBLNumber,int? Carrier,int? VoyageID,int? OriginPortID,int? DestinationPortID,DateTime? Ets,DateTime? Eta,DateTime? MasterBLDate,int? ConsigneeID,int? AgentAtDestinationID,byte[] Ts)
	    {
		    MasterBLTable item = new MasterBLTable();
		    
            item.MasterBLNumber = MasterBLNumber;
            
            item.Carrier = Carrier;
            
            item.VoyageID = VoyageID;
            
            item.OriginPortID = OriginPortID;
            
            item.DestinationPortID = DestinationPortID;
            
            item.Ets = Ets;
            
            item.Eta = Eta;
            
            item.MasterBLDate = MasterBLDate;
            
            item.ConsigneeID = ConsigneeID;
            
            item.AgentAtDestinationID = AgentAtDestinationID;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Mblid,string MasterBLNumber,int? Carrier,int? VoyageID,int? OriginPortID,int? DestinationPortID,DateTime? Ets,DateTime? Eta,DateTime? MasterBLDate,int? ConsigneeID,int? AgentAtDestinationID,byte[] Ts)
	    {
		    MasterBLTable item = new MasterBLTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Mblid = Mblid;
				
			item.MasterBLNumber = MasterBLNumber;
				
			item.Carrier = Carrier;
				
			item.VoyageID = VoyageID;
				
			item.OriginPortID = OriginPortID;
				
			item.DestinationPortID = DestinationPortID;
				
			item.Ets = Ets;
				
			item.Eta = Eta;
				
			item.MasterBLDate = MasterBLDate;
				
			item.ConsigneeID = ConsigneeID;
				
			item.AgentAtDestinationID = AgentAtDestinationID;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}
