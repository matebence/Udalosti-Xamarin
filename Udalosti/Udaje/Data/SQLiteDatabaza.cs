using SQLite;
using System.Diagnostics;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udaje.Data
{
    class SQLiteDatabaza : SQLDataImplementacia
    {
        private SQLiteConnection databaza;

        public void VyvorDatabazu()
        {
            Debug.WriteLine("Metoda VyvorDatabazu bola vykonana");

            databaza = new SQLiteConnection(App.databaza);
            this.tabulkaExistuje();
        }

        public void tabulkaExistuje()
        {
            Debug.WriteLine("Metoda tabulkaExistuje bola vykonana");

            databaza.CreateTable<Pouzivatelia>();
            databaza.CreateTable<Miesto>();
        }

        private void init()
        {
            if (databaza == null)
            {
                databaza = new SQLiteConnection(App.databaza);
            }
        }

        public int novePouzivatelskeUdaje(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda novePouzivatelskeUdaje bola vykonana");

            init();
            return databaza.Insert(pouzivatelia);
        }

        public int aktualizujPouzivatelskeUdaje(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda aktualizujPouzivatelskeUdaje bola vykonana");

            init();
            return databaza.Execute("UPDATE Pouzivatelia SET heslo = ?, token = ? WHERE email= ?", pouzivatelia.heslo, pouzivatelia.token, pouzivatelia.email);
        }
        
        public int odstranPouzivatelskeUdaje(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda odstranPouzivatelskeUdaje bola vykonana");

            init();
            return databaza.Delete<Pouzivatelia>(pouzivatelia.idPouzivatel);
        }

        public bool pouzivatelskeUdaje()
        {
            Debug.WriteLine("Metoda pouzivatelskeUdaje bola vykonana");

            init();
            if (databaza.Table<Pouzivatelia>().FirstOrDefault() != null)
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
            return databaza.Table<Pouzivatelia>().FirstOrDefault();
        }

        public int noveMiestoPrihlasenia(Miesto miesto)
        {
            Debug.WriteLine("Metoda noveMiestoPrihlasenia bola vykonana");

            init();
            if (miesto.idMiesto != 0)
            {
                return databaza.Update(miesto);
            }
            else
            {
                return databaza.Insert(miesto);
            }
        }

        public int aktualizujMiestoPrihlasenia(Miesto miesto)
        {
            Debug.WriteLine("Metoda aktualizujMiestoPrihlasenia bola vykonana");

            init();
            return databaza.Execute("UPDATE Miesto SET stat = ?, okres = ?, mesto = ? WHERE idMiesto= ?", miesto.stat, miesto.okres, miesto.mesto, miesto.idMiesto);
        }

        public int odstranMiestoPrihlasenia(Miesto miesto)
        {
            Debug.WriteLine("Metoda odstranMiestoPrihlasenia bola vykonana");

            init();
            return databaza.Delete<Miesto>(miesto.idMiesto);
        }

        public bool miestoPrihlasenia()
        {
            Debug.WriteLine("Metoda miestoPrihlasenia bola vykonana");

            init();
            if (databaza.Table<Miesto>().FirstOrDefault() != null)
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
            return databaza.Table<Miesto>().FirstOrDefault();
        }
    }
}