﻿namespace Udalosti.Udaje.Zdroje
{
    public class ObsahUdalosti
    {
        public ObsahUdalosti(int idUdalost, string obrazok, string nazov, string den, string mesiac, string cas, string mesto, string ulica, float vstupenka, int zaujemcovia, int zaujem)
        {
            this.idUdalost = idUdalost;
            this.obrazok = obrazok;
            this.nazov = nazov;
            this.den = den;
            this.mesiac = mesiac;
            this.cas = cas;
            this.mesto = mesto;
            this.ulica = ulica;
            this.vstupenka = vstupenka;
            this.zaujemcovia = zaujemcovia;
            this.zaujem = zaujem;
        }

        public int idUdalost { get; set; }
        public string obrazok { get; set; }
        public string nazov { get; set; }
        public string den { get; set; }
        public string mesiac { get; set; }
        public string cas { get; set; }
        public string mesto { get; set; }
        public string ulica { get; set; }
        public float vstupenka { get; set; }
        public int zaujemcovia { get; set; }
        public int zaujem { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
