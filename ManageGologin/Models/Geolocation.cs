using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.Models
{
    public class Geolocation
    {
        public double? Latitude { get; set; } = 0;
        public double? Longitude { get; set; } = 0;
        public string? TimeZone { get; set; } = "Asia/Bangkok";
    }
}
