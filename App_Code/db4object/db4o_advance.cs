using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

/// <summary>
/// Summary description for db4oject_advance
/// </summary>

namespace DAL.Logistics
{
    public class db4o_advance
    {
        //classes for saving to db4objects
        //don't use the full classes in wwiprov as we don't need the extra overhead to store locally
        public class Carton {

            public string _cartonid;
            public decimal _length;
            public decimal _width;
            public decimal _height;
            public decimal _weight;

            //public TitleName _titlename;
            
            public Carton(decimal Length, decimal Width, decimal Height, decimal Weight){
                //this._titlename._titlename = titlename;
                //this._titlename._titlename = username;
                this._cartonid = wwi_func.pack_guid(Guid.NewGuid()); 
                this._length = Length;
                this._width = Width;
                this._height = Height;
                this._weight = Weight;
            }

            public void setCartonId(string v) { this._cartonid = v; }
            public string getCartonId() { return this._cartonid; } 

            public void setLength(decimal v){this._length = v;}  
            public decimal getLength(){return this._length;} 
        	      
            public void setWidth(decimal v){this._width = v;}  
            public decimal getWidth(){return this._width;} 
        	
            public void setHeight(decimal v){this._height = v;}  
            public decimal getHeight(){return this._height;} 
        	
            public void setWeight(decimal v){this._weight = v;}  
            public decimal getWeight(){return this._weight;}

            // void setTitleName(TitleName t) { this._titlename  = t; }
            //public TitleName getTitleName() { return this._titlename; } 

            public String toString() {
                return _length + ":" + _width + ":" + _height + ":" + _weight;
            }
        }

        public class BookTitle
        {
            public string _titleid;
            public string _username;
            public String _booktitle;
            IList _cartons;

            public BookTitle(String UserName, String ItemName)
            {
                this._titleid = wwi_func.pack_guid(Guid.NewGuid());  
                this._username = UserName;
                this._booktitle = ItemName;
                this._cartons = new ArrayList();
            }
        
            public void addCarton(Carton c)
            {
                _cartons.Add(c); 
            }

            public void setTitleId(string v) { this._titleid = v; }
            public string getTitleId() { return this._titleid; } 
            
            public void setUser(String u) { this._username = u; }
            public String getUser() { return this._username; }

            public void setTitle(String t) { this._booktitle = t; }
            public String getTitle() { return this._booktitle; }

            public void setCartons(IList c) { _cartons  = c; }
            public IList getCartons() { return _cartons; }       

            public String toString()
            {
                return _booktitle;
            }
        }
        //end classes

        public db4o_advance()
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
        public static List<BookTitle> SelectByTitle(string booktitle, int maximumRows, int startRowIndex)
        {
            List<BookTitle> _list = new List<BookTitle>();
            
            db4oManager.WithContainer(container =>
            {
                
                //(maximumRows < 1) maximumRows = int.MaxValue;
                _list = (from BookTitle t in container
                         where t._booktitle == booktitle 
                         select t).ToList();

                //_c = (from TitleName t in container
                 //                       where t._titlename  == thetitle
                //                        select t.getCartons()).ToList();
            
            });
            return _list;
        }
        public static List<BookTitle> SelectByTitleId(string titleid, int maximumRows, int startRowIndex)
        {
            List<BookTitle> _list = new List<BookTitle>();

            db4oManager.WithContainer(container =>
            {

                //(maximumRows < 1) maximumRows = int.MaxValue;
                _list = (from BookTitle t in container
                         where t._titleid == titleid select t).ToList();

            });
            return _list;
        }
        //end select booktitle

        /// <summary>
        /// returns just titles without cartons
        /// </summary>
        /// <param name="titleid"></param>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <returns></returns>
        public static List<BookTitle> SelectTitlesOnly()
        {
            List<BookTitle> _list = new List<BookTitle>();

            db4oManager.WithContainer(container =>
            {

                //(maximumRows < 1) maximumRows = int.MaxValue;
                _list = (from BookTitle t in container
                         select t).ToList();

            });
            return _list;
        }
        //end select booktitle


        /// <summary>
        /// returns titleid if save is successful
        /// </summary>
        /// <param name="booktitle"></param>
        /// <returns></returns>
        public static string InsertTitle(BookTitle booktitle)
        {
            db4oManager.WithContainer(container =>
            {
                container.Store(booktitle);
            });

            return booktitle._titleid;
        }

        /// <summary>
        /// update booktitle based in id
        /// </summary>
        /// <param name="booktitle"></param>
        public static void UpdateTitle(BookTitle booktitle)
        {
            db4oManager.WithContainer(container =>
            {
               // The office object that is passed to this procedure is not connected tot 
                // the db4o container so we first have to do that.
                List<BookTitle> _list = (from BookTitle t in container
                                            where t._booktitle == booktitle._titleid  
                                            select t).ToList();
                if (_list != null && _list.Count > 0)
                {
                    // Then we should pass the properties from the updated object to the 
                    // selected one. 
                    // TODO: Maybe we could make a reflection procedure for this?
                    _list[0]._booktitle = booktitle._booktitle;
                    _list[0]._username = booktitle._username;
                    container.Store(_list[0]);
                }
            });
        }
        //end update title

        /// <summary>
        /// delete based on titleid
        /// </summary>
        /// <param name="booktitle"></param>
        public static void DeleteTitle(BookTitle booktitle)
        {
            db4oManager.WithContainer(container =>
            {
                // The office object that is passed to this procedure is not connected tot 
                // the db4o container so we first have to do that.
                List<BookTitle> _list = (from BookTitle t in container
                                         where t._booktitle == booktitle._titleid 
                                         select t).ToList();
                if (_list != null && _list.Count > 0)
                {
                    // Then we should pass the properties from the updated object to the 
                    // selected one. 
                    // TODO: Maybe we could make a reflection procedure for this?
                    container.Delete(_list[_list.Count -1]); 
                }
            });
        }
        //end delete title
    }

}