using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using SLMM.Domain;
using SLMM.Domain.LawnMap;

namespace SLMM.Api.Config
{
    public class LawnConfig
    {
        public Orientation Orientation { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int Length { get; set; }
        public int Width { get; set; }
    }
}