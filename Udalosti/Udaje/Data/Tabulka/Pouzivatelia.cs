using System;

namespace Udalosti.Udaje.Data.Tabulka
{
    class Pouzivatelia
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int idPouzivatel { get; set; }
        public String email { get; set; }
        public String heslo { get; set; }
        public String token { get; set; }

        public Pouzivatelia()
        {
        }

        public Pouzivatelia(int idPouzivatel, string email, string heslo, string token)
        {
            this.idPouzivatel = idPouzivatel;
            this.email = email;
            this.heslo = heslo;
            this.token = token;
        }
    }
}