using System;

namespace Udalosti.Udaje.Zdroje
{
    public class Navigacia
    {
        public string Nazov { get; set; }
        public string Obrazok { get; set; }
        public Type Stranka { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}