using ServiceContracts;

namespace Services;

public class CitiesService : ICitiesService
{
    private List<string> _cities = new ();

    public CitiesService()
    {
        _cities = new List<string>()
        {
            "VietNam",
            "London"
        };
    }

    public List<string> GetCountries()
    {
        return _cities;
    }
}

