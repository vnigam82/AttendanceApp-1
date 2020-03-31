using System;
using AttendanceApp.Database.DBModel;
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
                database.CreateTable<LoginDBModel>();
                database.CreateTable<AppLanguage>();

            }
            catch (Exception ex)
            {

            }
        }
        public AppLanguage GetLanguage()
        {
            return database.Table<AppLanguage>().FirstOrDefault();
        }

        public int SaveLanguage(AppLanguage item)
        {
            try
            {
                var data = database.Table<AppLanguage>().ToList();
                foreach (var cn in data)
                {
                     database.Delete(cn);
                }
            }
            catch (Exception ex)
            {
            }
            return database.Insert(item);
             
        }
     
        
        
        public LoginDBModel GetLoggedInUser()
        {
            return database.Table<LoginDBModel>().FirstOrDefault();
        }

        public int SaveLoggedInUser(LoginDBModel item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }

        public int ClearLoginDetails()
        {
            var status = 0;
            try
            {
                var data = database.Table<LoginDBModel>().ToList();
                foreach (var item in data)
                {
                    status = database.Delete(item);
                }

            }
            catch (Exception ex)
            {

            }
            return status;
        }
    }
}
