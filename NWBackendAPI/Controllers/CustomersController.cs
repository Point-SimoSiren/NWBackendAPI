using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWBackendAPI.Models;

namespace NWBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Luodaan instanssi tietokantakontekstiluokasta
        private readonly NorthwindOriginalContext db = new();

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
            catch (Exception ex)
            {
                return BadRequest($"Tapahtui virhe. Lue lisää: {ex.Message}");
            }
        }


    }
}







