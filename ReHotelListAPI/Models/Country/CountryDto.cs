using ReHotelListAPI.Models.Hotel;

namespace ReHotelListAPI.Models.Country
{
    public class CountryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }
        public virtual IList<BaseHotelDto> Hotels { get; set; }
    }
}
