using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    public class AmountClass
    {
        public string name { get; set; }
        public int amount { get; set; }
        public int TotalAmount { get; set; }
    }

    public class CheckClass
    {
        public string CheckNo { get; set; }
        public string SName { get; set; }
    }

    public class Check2Class
    {
        public int CheckYear { get; set; }
        public int CheckHiatusCount { get; set; }
    }

    public class Check3Class
    {
        public int CheckYear { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }        
    }

    public class Check4Class
    {
        public int CheckYear { get; set; }
        public string Name { get; set; }
        public int HiatusCount { get; set; }
        public int TotalCount { get; set; }
        public double HiatusCountPercent { get; set; }
    }

    public class Check5Class
    {
        public int CheckYear { get; set; }
        public string Business_theme { get; set; }
        public int CheckCount { get; set; }
        public int CheckAllDoesmeet { get; set; }
        public double CheckNoHiatusCount { get; set; }
    }
}