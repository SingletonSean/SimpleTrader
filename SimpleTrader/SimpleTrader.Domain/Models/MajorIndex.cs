using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Domain.Models
{
    public enum MajorIndexType
    {
        DowJones,
        Nasdaq,
        SP500
    }

    public class MajorIndex
    {
        public double Price { get; set; }
        public double Changes { get; set; }
        public MajorIndexType Type { get; set; }
    }
}
