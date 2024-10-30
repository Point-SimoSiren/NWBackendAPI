using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWBackendAPI.Models;

namespace NWBackendAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Luodaan instanssi tietokantakontekstiluokasta (perinteinen)
        //private readonly NorthwindOriginalContext db = new NorthwindOriginalContext();


        // DI tyyli
        private readonly NorthwindOriginalContext db;

        public CustomersController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }



        // Hakee kaikki asiakkaat
        [HttpGet]
        public ActionResult GetAllCustomers()
        {
            List<Customer> asiakkaat = db.Customers.ToList();
            return Ok(asiakkaat);
        }


        // Hakee asiakkaan pääavaimella eli CustomerId:llä
        [HttpGet("{id}")]
        public ActionResult GetCustomerById(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);

                if (asiakas == null)
                {
                    // String interpolation tyyli liittää muuttuja arvoja merkkijonoon
                    return NotFound($"Asiakasta id:llä {id} ei löytynyt.");
                }
                else
                {
                    return Ok(asiakas);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Tapahtui virhe. Lue lisää: {ex.Message}");
            }
        }


        // Uuden lisääminen
        [HttpPost]
        public ActionResult AddNewCustomer([FromBody] Customer customer) {

            // Uusi asiakas tallennetaan tietokantaan
            db.Customers.Add(customer);
            db.SaveChanges();

            // Tämä on front sovellukselle palautettava kuittausviesti
            return Ok($"Lisätty uusi asiakas: {customer.CompanyName}");
        }


        // Asiakkaan poistaminen url parametrina annettavan id:n perusteella
        [HttpDelete("{id}")]
        public ActionResult RemoveCustomerById(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);

                if (asiakas != null)
                {
                    // Asiakkaan poisto tietokannasta
                    db.Customers.Remove(asiakas);
                    db.SaveChanges();

                    // Kuittausviesti fronttisovellukselle:
                    return Ok($"Poistettiin asiakas: {asiakas.CompanyName}");
                }
                else
                {
                    // String interpolation tyyli liittää muuttuja arvoja merkkijonoon
                    return NotFound($"Asiakasta id:llä {id} ei löytynyt poistettavaksi.");
                }
            }

            // Ohjelman generoima poikkeus otetaan kiinni ex muuttujaan ja sen sisältämä virheilmoitus voidaan hyödyntää
            catch (Exception ex) { 
                return BadRequest($"Tapahtui virhe. Lue lisää: {ex.Message}");
            }
        }


        // Asiakkaan haku Company namen perusteella (osa nimestä riittää)
        [HttpGet("compname/{cname}")]
        public ActionResult SearchCustomerByCompanyName(string cname)
        {
            var asiakkaat = db.Customers.Where(c => c.CompanyName.Contains(cname));
            //var asiakkaat = db.Customers.Where(c => c.CompanyName == cname); <----- täydellinen match
            //var asiakaat = from c db.Customers where c.CompanyName.Contains(cname); <--- perinteinen LINQ

            return Ok(asiakkaat);
        }

        
        // Asikkaan muokkaaminen
        [HttpPut("{id}")]
        public ActionResult EditCustomer(string id, [FromBody] Customer customer)
        {
            // Haetaan id:n perusteella tietokannasta vanha asiakasobjekti
            var asiakas = db.Customers.Find(id);
            if (asiakas != null)
            {
                // em. asiakasobjektiin sulautetaan parametrina saadun asiakkaan tiedot
                asiakas.CompanyName = customer.CompanyName;
                asiakas.ContactName = customer.ContactName;
                asiakas.Phone = customer.Phone;
                asiakas.City = customer.City;
                asiakas.PostalCode = customer.PostalCode;
                asiakas.Country = customer.Country;
                asiakas.Address = customer.Address;
                asiakas.Fax = customer.Fax;
                

                db.SaveChanges();

                return Ok("Muokattu asiakasta " + asiakas.CompanyName);
            }
            else
            {
                return NotFound("Asikasta ei löytynyt id:llä " + id);
            }
        }



    }
}







