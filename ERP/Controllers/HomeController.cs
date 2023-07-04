using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstApp.Models;

namespace MyFirstApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "Asp.Net Core Demo App";
            List<Person> people = new List<Person>()
            {
                new Person() {Name = "JohnA", DateOfBirth = DateTime.Parse("2000-05-06"), PersonGender = Gender.Male},
                new Person() {Name = "JohnB", DateOfBirth = DateTime.Parse("2000-05-06"), PersonGender = Gender.Female},
                new Person() {Name = "JohnC", DateOfBirth = DateTime.Parse("2000-05-06"), PersonGender = Gender.Other}
            };
            return View(people);
        }

        [Route("person-details/{name}")]
        public IActionResult Details(string? name)
        {
            if (name == null)
            {
                return Content("Person's name can't be null");
            }
            List<Person> people = new List<Person>()
            {
                new Person() {Name = "JohnA", DateOfBirth = DateTime.Parse("2000-05-06"), PersonGender = Gender.Male},
                new Person() {Name = "JohnB", DateOfBirth = DateTime.Parse("2000-05-06"), PersonGender = Gender.Female},
                new Person() {Name = "JohnC", DateOfBirth = DateTime.Parse("2000-05-06"), PersonGender = Gender.Other}
            };
            Person? matchingPerson = people.Where(temp => temp.Name == name).FirstOrDefault();
            return View(matchingPerson);
        }
    }
}

