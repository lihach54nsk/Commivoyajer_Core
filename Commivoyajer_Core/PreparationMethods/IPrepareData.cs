using Commivoyajer_Core.Models;
using System.Collections.Generic;

namespace Commivoyajer_Core.PreparationMethods
{
    public interface IPrepareData
    {
        public double[][] PrepareData(List<Input> input);
    }
}
