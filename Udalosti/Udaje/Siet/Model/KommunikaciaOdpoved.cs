﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Udalosti.Udaje.Siet.Model
{
    interface KommunikaciaOdpoved
    {
        Task odpovedServeraAsync(String odpoved, String od, Dictionary<String, String> udaje);

        void odpovedServera(String odpoved, String od, Dictionary<String, String> udaje);
    }
}