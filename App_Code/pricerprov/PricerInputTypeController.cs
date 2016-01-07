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
namespace DAL.Pricer
{
    /// <summary>
    /// Controller class for pricer_input_type
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class PricerInputTypeController
    {
        // Preload our schema..
        PricerInputType thisSchemaLoad = new PricerInputType();
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
        public PricerInputTypeCollection FetchAll()
        {
            PricerInputTypeCollection coll = new PricerInputTypeCollection();
            Query qry = new Query(PricerInputType.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PricerInputTypeCollection FetchByID(object InTypeId)
        {
            PricerInputTypeCollection coll = new PricerInputTypeCollection().Where("in_type_id", InTypeId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public PricerInputTypeCollection FetchByQuery(Query qry)
        {
            PricerInputTypeCollection coll = new PricerInputTypeCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object InTypeId)
        {
            return (PricerInputType.Delete(InTypeId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object InTypeId)
        {
            return (PricerInputType.Destroy(InTypeId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int InDimensionsId,string InDescription,byte[] InTs)
	    {
		    PricerInputType item = new PricerInputType();
		    
            item.InDimensionsId = InDimensionsId;
            
            item.InDescription = InDescription;
            
            item.InTs = InTs;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int InTypeId,int InDimensionsId,string InDescription,byte[] InTs)
	    {
		    PricerInputType item = new PricerInputType();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.InTypeId = InTypeId;
				
			item.InDimensionsId = InDimensionsId;
				
			item.InDescription = InDescription;
				
			item.InTs = InTs;
				
	        item.Save(UserName);
	    }
    }
}
