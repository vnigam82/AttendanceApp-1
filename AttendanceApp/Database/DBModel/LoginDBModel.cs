using System;
using SQLite;

namespace AttendanceApp.Database
{
    public class LoginDBModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserGUID { get; set; }
        public bool RememberMe { get; set; }
    }
}
