using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.BLL.Models
{
    public class HistoricResource
    {

        public int HistoricResourceId { get; set; }

        public decimal Value { get; set; }

        public Types Type { get; set; }

        public int Day { get; set; }

        public int MonthId { get; set; }
        public virtual Month Month { get; set; }

        public int Year { get; set; }
    }

    public enum Types
    {
        Input, Output
    }
}
