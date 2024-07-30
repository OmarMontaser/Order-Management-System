using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class JWT
    {
        public string KEY { get; set; }
        public string Issuer {  get; set; }
        public string Audiance {  get; set; }
        public double DurationInDays {  get; set; }
    }
}
