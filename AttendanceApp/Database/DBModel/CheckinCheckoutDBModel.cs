using System;
using SQLite;

namespace AttendanceApp.Database.DBModel
{
    
    public class ClsDBLocationData 
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string mapLocation { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public double radius { get; set; }
    }
}
