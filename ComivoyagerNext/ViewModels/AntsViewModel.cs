using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComivoyagerNext.ViewModels
{
    internal class AntsViewModel
    {
        public int ItherationsCount { get; set; } = 100;

        public int AntsCount { get; set; } = 5;

        public double Alpha { get; set; } = 0.5;

        public double Beta { get; set; } = 0.5;

        public double EvaporationRate { get; set; } = 100.0;

        public double PheromoneProductionIntesity { get; set; } = 100.0;
    }
}
