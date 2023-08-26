using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that is used as return for most of CountriesService methods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(CountryResponse)) return false;

            CountryResponse other = (CountryResponse)obj;

            return this.CountryID == other.CountryID && this.CountryName == other.CountryName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CountryID, CountryName);
        }
    }

    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse() { 
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };
        }
    }
}