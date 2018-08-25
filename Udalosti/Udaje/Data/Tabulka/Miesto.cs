using System;

namespace Udalosti.Udaje.Data.Tabulka
{
    class Miesto
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int idMiesto { get; set; }
        public String stat { get; set; }
        public String okres { get; set; }
        public String mesto { get; set; }

        public Miesto()
        {
        }

        public Miesto(int idMiesto, string stat, string okres, string mesto)
        {
            this.idMiesto = idMiesto;
            this.stat = stat;
            this.okres = okres;
            this.mesto = mesto;
        }
    }
}