using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.Models
{
    public class Profiles
    {
        public uint STT { get; set; }
        [Display(Name ="Profile Name")]
        public string ProfileName { get; set; }
        public string? Proxy { get; set; }
        public string? DataPath { get; set; }
    }
}
