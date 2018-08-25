using SQLite;
using System.Diagnostics;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udaje.Data
{
    class SQLiteDatabaza : SQLDataImplementacia
    {
        private SQLiteAsyncConnection databaza;

        public void VyvorDatabazu()
        {
            Debug.WriteLine("Metoda VyvorDatabazu bola vykonana");

            databaza = new SQLiteAsyncConnection(App.databaza);
            this.tabulkaExistuje();
        }

        public void tabulkaExistuje()
        {
            Debug.WriteLine("Metoda tabulkaExistuje bola vykonana");

            databaza.CreateTableAsync<Pouzivatelia>().Wait();
            databaza.CreateTableAsync<Miesto>().Wait();
        }

        private void init()
        {
            if (databaza == null)
            {
                databaza = new SQLiteAsyncConnection(App.databaza);
            }
        }

        public Task<int> novePouzivatelskeUdaje(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda novePouzivatelskeUdaje bola vykonana");

            init();
            if (pouzivatelia.idPouzivatel != 0)
            {
                return databaza.UpdateAsync(pouzivatelia);
            }
            else
            {
                return databaza.InsertAsync(pouzivatelia);
            }
        }

        public Task<int> odstranPouzivatelskeUdaje(Pouzivatelia pouzivatelia)
        {
            Debug.WriteLine("Metoda odstranPouzivatelskeUdaje bola vykonana");

            init();
            return databaza.DeleteAsync(pouzivatelia);
        }

        public bool pouzivatelskeUdaje()
        {
            Debug.WriteLine("Metoda pouzivatelskeUdaje bola vykonana");

            init();
            if (databaza.Table<Pouzivatelia>().FirstOrDefaultAsync().Result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<Pouzivatelia> vratAktualnehoPouzivatela()
        {
            Debug.WriteLine("Metoda vratAktualnehoPouzivatela bola vykonana");

            init();
            return databaza.Table<Pouzivatelia>().FirstOrDefaultAsync();
        }

        public Task<int> noveMiestoPrihlasenia(Miesto miesto)
        {
            Debug.WriteLine("Metoda noveMiestoPrihlasenia bola vykonana");

            init();
            if (miesto.idMiesto != 0)
            {
                return databaza.UpdateAsync(miesto);
            }
            else
            {
                return databaza.InsertAsync(miesto);
            }
        }

        public Task<int> odstranMiestoPrihlasenia(Miesto miesto)
        {
            Debug.WriteLine("Metoda odstranMiestoPrihlasenia bola vykonana");

            init();
            return databaza.DeleteAsync(miesto);
        }

        public bool miestoPrihlasenia()
        {
            Debug.WriteLine("Metoda miestoPrihlasenia bola vykonana");

            init();
            if (databaza.Table<Miesto>().FirstOrDefaultAsync().Result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<Miesto> vratMiestoPrihlasenia()
        {
            Debug.WriteLine("Metoda vratMiestoPrihlasenia bola vykonana");

            init();
            return databaza.Table<Miesto>().FirstOrDefaultAsync();
        }
    }
}