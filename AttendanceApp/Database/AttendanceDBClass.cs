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
                database.CreateTable<LoginDBModel>();

            }
            catch (Exception ex)
            {

            }
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
