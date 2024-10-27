using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace ControllerAPI
{
    public class Connect
    {
        private readonly string strCon = "Host=localhost;Port=5434;Username=admin;Password=password;Database=myDatabase";

        public NpgsqlConnection CreateConnection(){
            NpgsqlConnection conn = new NpgsqlConnection(strCon);
            conn.Open();

            return conn;
        }
    }
}