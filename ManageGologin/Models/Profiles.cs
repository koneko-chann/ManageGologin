namespace ManageGologin.Models
{
    public class Profiles
    {
        public uint STT { get; set; }
        public string ProfileName { get; set; }
        public string DataPath { get; set; }
        public CustomProxy? Proxy { get; set; }

    }
}
