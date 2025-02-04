﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Models
{
    [Serializable]
    internal sealed record ModelWind
    {
        public float speed { get; set; }
        public int deg { get; set; }
        public float gust { get; set; }
    }
}
