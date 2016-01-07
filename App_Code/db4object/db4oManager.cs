using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

/// <summary>
/// wrapper for db4object client/server mode
/// </summary>
public static class db4oManager
{
    //private static IConfiguration _config = Db4oFactory.NewConfiguration();

    public static void WithContainer(Action<IObjectContainer> action)
    {
        string _dbfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db4o_linq");

        IObjectServer server = Db4oFactory.OpenServer(_dbfile, 0);
        try
        {
            using (var container = server.OpenClient())
            {
                action(container);
            }
        }
        finally
        {
            server.Close();
        }
    }
}