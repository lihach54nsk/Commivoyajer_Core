using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComivoyagerNext.ViewModels
{
    internal class GeneticAlgorithmViewModel
    {
        public int GenerationSize { get; set; } = 100;

        public int GenerationsCount { get; set; } = 1000;

        public int CandidateGenerationSize { get; set; } = 200;

        public int VipGenCount { get; set; } = 2;

        public double MutataionPropability { get; set; } = 0.2;
    }
}
