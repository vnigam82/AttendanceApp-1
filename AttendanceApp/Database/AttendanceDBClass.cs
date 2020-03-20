using System;
using SQLite;

namespace AttendanceApp.Database
{
    public class AttendanceDBClass
    {
        readonly SQLiteConnection database;

        public AttendanceDBClass(string dbPath)
        {
            try
            {
                database = new SQLiteConnection(dbPath);
                //database.CreateTable<LoggedInUser>();

            }
            catch (Exception ex)
            {

            }
        }

        //public LoggedInUser GetLoggedInUser()
        //{
        //    return database.Table<LoggedInUser>().FirstOrDefault();
        //}

        //public int SaveLoggedInUser(LoggedInUser item)
        //{
        //    if (item.ID != 0)
        //    {
        //        return database.Update(item);
        //    }
        //    else
        //    {
        //        return database.Insert(item);
        //    }
        //}

        //public int ClearLoginDetails()
        //{
        //    var status = 0;
        //    try
        //    {
        //        var data = database.Table<LoggedInUser>().ToList();
        //        foreach (var item in data)
        //        {
        //            status = database.Delete(item);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return status;
        //}
    }
}
