﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonServiceTest
    {
        private readonly IPersonsService _personsService;
        private readonly ICountryService _countryService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonServiceTest(ITestOutputHelper testOutputHelper) {
            _personsService = new PersonsService();
            _countryService = new CountriesService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson

        [Fact]
        public void AddPerson_NullPerson()
        {
            // Arrange
            PersonAddRequest? personAddRequest = null;

            // Act
            Assert.Throws<ArgumentNullException>(() => 
            {
                _personsService.AddPerson(personAddRequest);
            });
        }

        [Fact]
        public void AddPerson_PersonNameNull()
        {
            // Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { 
                PersonName = null
            };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.AddPerson(personAddRequest);
            });
        }

        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            // Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "Hau",
                Email = "hau@gmail.com",
                Address = "Viet Nam",
                CountryID = Guid.NewGuid(),
                Gender = GenderOption.Male,
                DateOfBirth = DateTime.Parse("1996-07-10"),
                ReceiveNewsLetters = true
            };

            // Act
            PersonResponse person_response_from_add = _personsService.AddPerson(personAddRequest);
            List<PersonResponse> person_list = _personsService.GetAllPersons();

            // Assert
            Assert.True(person_response_from_add.PersonID != Guid.Empty);
            Assert.Contains(person_response_from_add, person_list);
        }

        #endregion

        #region GetPersonByPersonID

        [Fact]
        public void GetPersonByPersonID_NullPersonID()
        {
            // Arrange
            Guid? personID = null;

            // Act
            PersonResponse? personResponse = _personsService.GetPersonByPersonID(personID);

            // Assert
            Assert.Null(personResponse);
        }

        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            // Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() {
                CountryName = "VietNam"
            };
            CountryResponse countryResponse = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest() { 
                PersonName = "hau",
                Email = "hau@gmail.com",
                Address = "address",
                CountryID = countryResponse.CountryID,
                DateOfBirth = DateTime.Parse("1996-07-10"),
                Gender = GenderOption.Male,
                ReceiveNewsLetters = true
            };
            PersonResponse person_response_from_add = _personsService.AddPerson(personAddRequest);

            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonID(person_response_from_add.PersonID);

            // Assert
            Assert.Equal(person_response_from_add, person_response_from_get);
        }

        #endregion

        #region GetAllPersons

        [Fact]
        public void GetAllPersons_EmptyList()
        {
            // Act
            List<PersonResponse> person_from_get = _personsService.GetAllPersons();

            // Assert
            Assert.Empty(person_from_get);
        }

        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            // Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest() {
                CountryName = "VietNam"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest()
            {
                CountryName = "Singapore"
            };
            CountryResponse country_response_1 = _countryService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countryService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "hau",
                Email = "hau@gmail.com",
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("1996-07-10"),
                Gender = GenderOption.Male,
                ReceiveNewsLetters = true
            };
            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "nguyen",
                Email = "nguyen@gmail.com",
                Address = "address",
                CountryID = country_response_2.CountryID,
                DateOfBirth = DateTime.Parse("2002-07-10"),
                Gender = GenderOption.Male,
                ReceiveNewsLetters = true
            };
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() {
                person_request_1, person_request_2
            };
            List<PersonResponse> person_responses = new List<PersonResponse>();
            foreach(PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personsService.AddPerson(person_request);
                person_responses.Add(person_response);
            }

            _testOutputHelper.WriteLine("Expected:");
            foreach(PersonResponse person_response in person_responses)
            {
                _testOutputHelper.WriteLine(person_response.ToString());
            }

            // Act
            List<PersonResponse> persons_list_from_get = _personsService.GetAllPersons();

            _testOutputHelper.WriteLine("Actual:");
            foreach (PersonResponse person_response in persons_list_from_get)
            {
                _testOutputHelper.WriteLine(person_response.ToString());
            }

            // Assert
            foreach (PersonResponse person_response_from_add in person_responses)
            {
                Assert.Contains(person_response_from_add, persons_list_from_get);
            }
        }

        #endregion

        #region GetFilteredPersons

        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            // Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest()
            {
                CountryName = "VietNam"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest()
            {
                CountryName = "Singapore"
            };
            CountryResponse country_response_1 = _countryService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countryService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "hau",
                Email = "hau@gmail.com",
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("1996-07-10"),
                Gender = GenderOption.Male,
                ReceiveNewsLetters = true
            };
            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "nguyen",
                Email = "nguyen@gmail.com",
                Address = "address",
                CountryID = country_response_2.CountryID,
                DateOfBirth = DateTime.Parse("2002-07-10"),
                Gender = GenderOption.Male,
                ReceiveNewsLetters = true
            };
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() {
                person_request_1, person_request_2
            };
            List<PersonResponse> person_responses = new List<PersonResponse>();
            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personsService.AddPerson(person_request);
                person_responses.Add(person_response);
            }

            // Act
            List<PersonResponse> persons_list_from_search = _personsService.GetFilteredPersons(nameof(PersonAddRequest.PersonName), "");

            // Assert
            foreach (PersonResponse person_response_from_add in person_responses)
            {
                Assert.Contains(person_response_from_add, persons_list_from_search);
            }
        }

        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            // Arrange
            CountryAddRequest country_request_1 = new CountryAddRequest()
            {
                CountryName = "VietNam"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest()
            {
                CountryName = "Singapore"
            };
            CountryResponse country_response_1 = _countryService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countryService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "hau",
                Email = "hau@gmail.com",
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("1996-07-10"),
                Gender = GenderOption.Male,
                ReceiveNewsLetters = true
            };
            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "nguyen",
                Email = "nguyen@gmail.com",
                Address = "address",
                CountryID = country_response_2.CountryID,
                DateOfBirth = DateTime.Parse("2002-07-10"),
                Gender = GenderOption.Male,
                ReceiveNewsLetters = true
            };
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() {
                person_request_1, person_request_2
            };
            List<PersonResponse> person_responses = new List<PersonResponse>();
            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personsService.AddPerson(person_request);
                person_responses.Add(person_response);
            }

            // Act
            List<PersonResponse> persons_list_from_search = _personsService.GetFilteredPersons(nameof(PersonAddRequest.PersonName), "hau");

            // Assert
            foreach (PersonResponse person_response_from_add in person_responses)
            {
                if (!string.IsNullOrEmpty(person_response_from_add.PersonName) && person_response_from_add.PersonName == "hau")
                {
                    Assert.Contains(person_response_from_add, persons_list_from_search);
                }
            }
        }

        #endregion
    }
}
