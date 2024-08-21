using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEBAPIAssessment.Models;

namespace WEBAPIAssessment.Controllers
{
    public class CountryController : ApiController
    {
        private static List<Country> countries = new List<Country>
        {
            new Country {ID=1, CountryName="Afganistan",Capital="Kabul"},
            new Country{ID=2, CountryName="france",Capital="paris"},
            new Country{ID=3, CountryName="Italy",Capital="Rome"},
            new Country{ID=4, CountryName="Norway",Capital="Oslo"}


        };

        public IEnumerable<Country> Get()
        {
            return countries;
        }

        public IHttpActionResult Get(int id)
        {
            var country = countries.FirstOrDefault(c => c.ID == id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        public IHttpActionResult Post(Country country)
        {
            if (country == null)
                return BadRequest("Invalid data.");
            country.ID = countries.Max(c => c.ID) + 1;
            countries.Add(country);
            return CreatedAtRoute("DefaultApi", new { id = country.ID }, country);
        }
        public IHttpActionResult Put(int id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingCountry = countries.FirstOrDefault(c => c.ID == id);
            if (existingCountry == null)
            {
                return NotFound();
            }
            existingCountry.CountryName = country.CountryName;
            existingCountry.Capital = country.Capital;
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(int id)
        {
            var country = countries.FirstOrDefault(c => c.ID == id);
            if (country == null)
            {
                return NotFound();
            }
            countries.Remove(country);
            return Ok(country);
        }

    }
 
}
