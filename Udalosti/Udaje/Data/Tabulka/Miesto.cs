namespace Udalosti.Udaje.Data.Tabulka
{
    class Miesto
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int idMiesto { get; set; }
        public string stat { get; set; }
        public string okres { get; set; }
        public string mesto { get; set; }

        public Miesto()
        {
        }

        public Miesto(string stat, string okres, string mesto)
        {
            this.stat = stat;
            this.okres = okres;
            this.mesto = mesto;
        }
    }
}