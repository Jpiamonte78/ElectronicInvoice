﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    public class documentolineaCargos
    {
        public documentoLineaCargo[] cargo { get; set; }
    }
}
