using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApp.Database.DBModel
{
   public class AppLanguage
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string LangKey { get; set; }
    }
}
