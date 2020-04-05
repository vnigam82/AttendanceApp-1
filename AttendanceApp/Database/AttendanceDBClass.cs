using System;
using System.Collections.Generic;
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
                database.CreateTable<DBLocationData>();
                database.CreateTable<DBReasonLanguage>();
                database.CreateTable<DBBookingModel>();
            }
            catch (Exception ex)
            {

            }
        }

        public AppLanguage GetLanguage()
        {
            return database.Table<AppLanguage>().FirstOrDefault();
        }


        public List<DBBookingModel> GetBooking()
        {
            return database.Table<DBBookingModel>().ToList();
        }
        public int ClearBooking()
        {
            var status = 0;
            try
            {
                var data = database.Table<DBBookingModel>().ToList();
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

        public int ClearBookingBasedOnId(DBBookingModel obj)
        {
            var status = 0;
            try
            {
                var data = database.Table<DBBookingModel>().ToList();
                foreach (var item in data)
                {
                    if (item.ID==obj.ID)
                    {
                        status = database.Delete(obj);
                    }
                    
                }

            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public int SaveBooking(DBBookingModel item)
        {
            return database.Insert(item);

        }




        public List<DBReasonLanguage> GetReason()
        {
            return database.Table<DBReasonLanguage>().ToList();
        }
        public int ClearReason()
        {
            var status = 0;
            try
            {
                var data = database.Table<DBReasonLanguage>().ToList();
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
        public int SaveReason(DBReasonLanguage item)
        {
            return database.Insert(item);

        }


        public List<DBLocationData> GetCheckinCheckoutLocation()
        {
            return database.Table<DBLocationData>().ToList();
        }
        public int SaveCheckinCheckoutLocation(DBLocationData item)
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
                var data = database.Table<LoginDBModel>().ToList();
                foreach (var cn in data)
                {
                    database.Delete(cn);
                }
                return database.Insert(item);
            }
        }
        public int SaveOrganizationUser(OrgProfileDBModel item)
        {
            var data = database.Table<OrgProfileDBModel>().ToList();
            foreach (var cn in data)
            {
                 database.Delete(cn);
            }
            return database.Insert(item);
            
        }
        public int SaveUserProfileDetails(clsDBUserProfile item)
        {
            var data = database.Table<clsDBUserProfile>().ToList();
            foreach (var cn in data)
            {
                database.Delete(cn);
            }
            return database.Insert(item);
             
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
        public int ClearCheckinCheckoutDetails()
        {
            var status = 0;
            try
            {
                var data = database.Table<DBLocationData>().ToList();
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
