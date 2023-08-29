using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;
using Entities;
using Services;
using ServiceContracts.DTO;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountryService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry
        [Fact]
        public void AddCountry_NullCountry()
        {
            // Arrange
            CountryAddRequest? request = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _countriesService.AddCountry(request);
            });
        }

        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = null
            };

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _countriesService.AddCountry(request);
            });
        }

        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            // Arrange
            CountryAddRequest? request1 = new CountryAddRequest()
            {
                CountryName = "VietNam"
            };
            CountryAddRequest? request2 = new CountryAddRequest()
            {
                CountryName = "VietNam"
            };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }

        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "VietNam"
            };

            // Act
            CountryResponse response = _countriesService.AddCountry(request);
            List<CountryResponse> countries_from_GetAllCountries = _countriesService.GetAllCountries();

            // Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);
        }

        #endregion

        #region GetAllCountries

        [Fact]
        public void GetAllCountries_EmptyList()
        {
            // Act
            List<CountryResponse> actual_country_response_list = _countriesService.GetAllCountries();

            // Assert
            Assert.Empty(actual_country_response_list);
        }

        [Fact]
        public void GetAllCountry_AddFewCountries()
        {
            // Arrange
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>() {
                new CountryAddRequest() { CountryName = "VietNam" },
                new CountryAddRequest() { CountryName = "Singaport" }
            };

            // Act 
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();
            foreach (CountryAddRequest country_request in country_request_list)
            {
                countries_list_from_add_country.Add(_countriesService.AddCountry(country_request));
            }

            List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries();

            foreach (CountryResponse expected_country in countries_list_from_add_country)
            {
                Assert.Contains(expected_country, actualCountryResponseList);
            }
        }

        #endregion

        #region GetCountryByCountryId

        [Fact]
        public void GetCountryByCountryID_NullCountryID()
        {
            // Arrange
            Guid? countryID = null;

            // Act
            CountryResponse? countryResponse = _countriesService.GetCountryByCountryId(countryID);

            // Assert
            Assert.Null(countryResponse);
        }

        [Fact]
        public void GetCountryByCountryID_ValidCountry()
        {
            // Arrange
            CountryAddRequest country_add_request = new CountryAddRequest() { 
                CountryName = "VietNam"
            };
            CountryResponse country_response_from_add_request = _countriesService.AddCountry(country_add_request);

            // Act
            CountryResponse? country_response_from_get = _countriesService.GetCountryByCountryId(country_response_from_add_request.CountryID);

            // Assert
            Assert.Equal(country_response_from_add_request, country_response_from_get);
        }

        #endregion
    }
}
