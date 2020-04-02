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
                database.CreateTable<OrgProfileDBModel>();
                database.CreateTable<clsDBUserProfile>();
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
        public OrgProfileDBModel GetOrganizationDetails()
        {
            return database.Table<OrgProfileDBModel>().FirstOrDefault();
        }
        public clsDBUserProfile GetUserProfileDetails()
        {
            return database.Table<clsDBUserProfile>().FirstOrDefault();
        }

        public int SaveLoggedInUser(LoginDBModel item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                database.Delete(item);
                return database.Insert(item);
            }
        }
        public int SaveOrganizationUser(OrgProfileDBModel item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                //database.Delete(item);
                return database.Insert(item);
            }
        }
        public int SaveUserProfileDetails(clsDBUserProfile item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                //database.Delete(item);
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
        public int ClearOrganizationDetails()
        {
            var status = 0;
            try
            {
                var data = database.Table<OrgProfileDBModel>().ToList();
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
        public int ClearUserProfileDetails()
        {
            var status = 0;
            try
            {
                var data = database.Table<clsDBUserProfile>().ToList();
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
