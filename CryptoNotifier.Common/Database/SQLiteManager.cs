using System;
using CryptoNotifier.Common.Database.Model;

namespace CryptoNotifier.Common.Database
{
    public class SQLiteManager
    {

        public SQLiteManager()
        {
            var db = new SQLite.SQLiteConnection("test.db3");
            db.CreateTable<Alert>();
            if(db != null)
            {
                
            }
        }
    }
}
