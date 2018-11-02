using SQLite;
using System.Diagnostics;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udaje.Data
{
    class SQLiteDatabaza : SQLDataImplementacia
    {
        private SQLiteConnection databaza;

        public void Databaza()
        {
            Debug.WriteLine("Metoda databaza bola vykonana");

            this.databaza = new SQLiteConnection(App.databaza);
            this.databaza.CreateTable<Pouzivatelia>();
            this.databaza.CreateTable<Miesto>();
        }

        private void init()
        {
            Debug.WriteLine("Metoda databaza - init bola vykonana");

            if (this.databaza == null)
            {
                this.databaza = new SQLiteConnection(App.databaza);
            }
        }

        public int novyPouzivatel(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda novyPouzivatel bola vykonana");

            init();
            return this.databaza.Insert(pouzivatelia);
        }

        public int aktualizujPouzivatela(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda aktualizujPouzivatela bola vykonana");

            init();
            return this.databaza.Execute("UPDATE Pouzivatelia SET heslo = ?, token = ? WHERE email= ?", pouzivatelia.heslo, pouzivatelia.token, pouzivatelia.email);
        }
        
        public int odstranPouzivatela(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda odstranPouzivatela bola vykonana");

            init();
            return this.databaza.Delete<Pouzivatelia>(pouzivatelia.idPouzivatel);
        }

        public bool pouzivatel()
        {
            Debug.WriteLine("Metoda pouzivatel bola vykonana");

            init();
            if (this.databaza.Table<Pouzivatelia>().FirstOrDefault() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Pouzivatelia vratPouzivatela()
        {
            Debug.WriteLine("Metoda vratPouzivatela bola vykonana");

            init();
            return this.databaza.Table<Pouzivatelia>().FirstOrDefault();
        }

        public int noveMiesto(Miesto miesto)
        {
            Debug.WriteLine("Metoda noveMiesto bola vykonana");

            init();
            if (miesto.idMiesto != 0)
            {
                return this.databaza.Update(miesto);
            }
            else
            {
                return this.databaza.Insert(miesto);
            }
        }

        public int aktualizujMiesto(Miesto miesto)
        {
            Debug.WriteLine("Metoda aktualizujMiesto bola vykonana");

            init();
            const int idMiesto = 1;
            return this.databaza.Execute("UPDATE Miesto SET pozicia = ?, okres = ?, kraj = ?, psc = ?, stat = ?, znakStatu = ? WHERE idMiesto= ?", miesto.pozicia, miesto.okres, miesto.kraj, miesto.psc, miesto.stat, miesto.znakStatu, idMiesto);
        }

        public int odstranMiesto(Miesto miesto)
        {
            Debug.WriteLine("Metoda odstranMiesto bola vykonana");

            init();
            return this.databaza.Delete<Miesto>(miesto.idMiesto);
        }

        public bool miesto()
        {
            Debug.WriteLine("Metoda miesto bola vykonana");

            init();
            if (this.databaza.Table<Miesto>().FirstOrDefault() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Miesto vratMiesto()
        {
            Debug.WriteLine("Metoda vratMiesto bola vykonana");

            init();
            return this.databaza.Table<Miesto>().FirstOrDefault();
        }
    }
}