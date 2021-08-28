using System;
namespace XamIntJul2021.Model
{
    public class Country
    {
        public string CountryId { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
