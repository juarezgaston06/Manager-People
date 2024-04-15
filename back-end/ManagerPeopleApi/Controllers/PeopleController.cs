using DBPeople;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ManagerPeopleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var people = _context.People.ToList();
                var provinces = _context.Provinces.ToList();
                people.ForEach(async p => { p.Province = provinces.Find(pro => pro.Id == p.ProvinceId); });
                return Ok(people);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("person/{idPerson}")]
        public IActionResult GetProvinces(int idPerson)
        {
            try
            {
                var people = _context.People.ToList();
                var person = people.FirstOrDefault(p => p.Id == idPerson);
                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("provinces")]
        public IActionResult GetProvinces()
        {
            try
            {
                var provinces = _context.Provinces.ToList();
                return Ok(provinces);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        public IActionResult Create(Person person)
        {
            try
            {
                var existingDNI = _context.People.FirstOrDefault(p => p.DNI == person.DNI);
                if (existingDNI != null)
                {
                    return Ok(false);
                }
                _context.People.Add(person);
                _context.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("modify")]
        public async Task<IActionResult> Modify(Person person)
        {
            try
            {
                var existingPerson = await _context.People.FindAsync(person.Id);

                if (existingPerson == null)
                {
                    return Ok(false);
                }

                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.DNI = person.DNI;
                existingPerson.Phone = person.Phone;
                existingPerson.ProvinceId = person.ProvinceId;
                existingPerson.ModificationDate = person.ModificationDate;


                _context.People.Update(existingPerson);
                _context.SaveChanges();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var person = await _context.People.FindAsync(id);

                if (person == null)
                {
                    return NotFound("Persona no encontrada");
                }

                _context.People.Remove(person);
                await _context.SaveChangesAsync();

                return Ok(true); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
