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
    /// Controller class for TypeTable
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TypeTableController
    {
        // Preload our schema..
        TypeTable thisSchemaLoad = new TypeTable();
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
        public TypeTableCollection FetchAll()
        {
            TypeTableCollection coll = new TypeTableCollection();
            Query qry = new Query(TypeTable.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TypeTableCollection FetchByID(object TypeID)
        {
            TypeTableCollection coll = new TypeTableCollection().Where("TypeID", TypeID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TypeTableCollection FetchByQuery(Query qry)
        {
            TypeTableCollection coll = new TypeTableCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object TypeID)
        {
            return (TypeTable.Delete(TypeID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object TypeID)
        {
            return (TypeTable.Destroy(TypeID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string TypeName,byte[] Ts)
	    {
		    TypeTable item = new TypeTable();
		    
            item.TypeName = TypeName;
            
            item.Ts = Ts;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int TypeID,string TypeName,byte[] Ts)
	    {
		    TypeTable item = new TypeTable();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.TypeID = TypeID;
				
			item.TypeName = TypeName;
				
			item.Ts = Ts;
				
	        item.Save(UserName);
	    }
    }
}
