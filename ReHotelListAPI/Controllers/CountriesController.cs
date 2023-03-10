using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReHotelListAPI.Contracts;
using ReHotelListAPI.Data;
using ReHotelListAPI.Models.Country;

namespace ReHotelListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
       
        public readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController( IMapper mapper, ICountriesRepository countriesRepository)
        {
            
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _countriesRepository.GetAllAsync();
            //var records = _mapper.Map<IEnumerable<GetCountryDto>>(countries);
            //return Ok(records);
            //sau
            var records = _mapper.Map<List<GetCountryDto>>(countries);
            return records;
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            //var country = await _context.Countries.FindAsync(id);
            var country = await _countriesRepository.GetDetails(id);
            

            if (country == null)
            {
                return NotFound();
            }
            var record = _mapper.Map<CountryDto>(country);  

            return Ok(record);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest();
            }
            var country = await _countriesRepository.GetAsync(id);
            //_context.Entry(country).State = EntityState.Modified;

            //var country = await _context.Countries.FindAsync(id);// il scoate din db ca sa il mapeze
             if (country == null)
            {
               return NotFound();
            }
            _mapper.Map(updateCountryDto, country);// mapare in country

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {
            var countryOld = new Country() { //metoda fara automapper
            Name= createCountryDto.Name,
            ShortName =createCountryDto.ShortName
            };

            var country = _mapper.Map<Country>(createCountryDto);

             await _countriesRepository.AddAsync(country);
             

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            //_context.Countries.Remove(country);
            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
