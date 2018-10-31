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
            Debug.WriteLine("Metoda this.databaza bola vykonana");

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

        public int novePouzivatelskeUdaje(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda novePouzivatelskeUdaje bola vykonana");

            init();
            return this.databaza.Insert(pouzivatelia);
        }

        public int aktualizujPouzivatelskeUdaje(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda aktualizujPouzivatelskeUdaje bola vykonana");

            init();
            return this.databaza.Execute("UPDATE Pouzivatelia SET heslo = ?, token = ? WHERE email= ?", pouzivatelia.heslo, pouzivatelia.token, pouzivatelia.email);
        }
        
        public int odstranPouzivatelskeUdaje(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda odstranPouzivatelskeUdaje bola vykonana");

            init();
            return this.databaza.Delete<Pouzivatelia>(pouzivatelia.idPouzivatel);
        }

        public bool pouzivatelskeUdaje()
        {
            Debug.WriteLine("Metoda pouzivatelskeUdaje bola vykonana");

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

        public Pouzivatelia vratAktualnehoPouzivatela()
        {
            Debug.WriteLine("Metoda vratAktualnehoPouzivatela bola vykonana");

            init();
            return this.databaza.Table<Pouzivatelia>().FirstOrDefault();
        }

        public int noveMiestoPrihlasenia(Miesto miesto)
        {
            Debug.WriteLine("Metoda noveMiestoPrihlasenia bola vykonana");

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

        public int aktualizujMiestoPrihlasenia(Miesto miesto)
        {
            Debug.WriteLine("Metoda aktualizujMiestoPrihlasenia bola vykonana");

            init();
            const int idMiesto = 1;
            return this.databaza.Execute("UPDATE Miesto SET pozicia = ?, okres = ?, kraj = ?, psc = ?, stat = ?, znakStatu = ? WHERE idMiesto= ?", miesto.pozicia, miesto.okres, miesto.kraj, miesto.psc, miesto.stat, miesto.znakStatu, idMiesto);
        }

        public int odstranMiestoPrihlasenia(Miesto miesto)
        {
            Debug.WriteLine("Metoda odstranMiestoPrihlasenia bola vykonana");

            init();
            return this.databaza.Delete<Miesto>(miesto.idMiesto);
        }

        public bool miestoPrihlasenia()
        {
            Debug.WriteLine("Metoda miestoPrihlasenia bola vykonana");

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

        public Miesto vratMiestoPrihlasenia()
        {
            Debug.WriteLine("Metoda vratMiestoPrihlasenia bola vykonana");

            init();
            return this.databaza.Table<Miesto>().FirstOrDefault();
        }
    }
}