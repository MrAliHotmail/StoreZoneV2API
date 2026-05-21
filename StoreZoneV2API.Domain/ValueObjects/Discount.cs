using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.ValueObjects
{
    public class Discount
    {
        public decimal Percent { get; set; }

        public Discount() { }

        public Discount(decimal percent)
        {
            if(percent <0 || percent > 100)
                throw new ArgumentException("Discount percent must be between 0 and 100.", nameof(percent));
            Percent = percent;
        }
    }
}
