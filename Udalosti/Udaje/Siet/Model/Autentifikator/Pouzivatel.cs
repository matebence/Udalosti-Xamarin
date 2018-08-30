namespace Udalosti.Udaje.Siet.Model.Autentifikator
{
    class Pouzivatel
    {
        public Pouzivatel(string token)
        {
            this.token = token;
        }

        public string token { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}