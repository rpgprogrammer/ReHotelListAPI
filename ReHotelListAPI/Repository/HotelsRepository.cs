using ReHotelListAPI.Contracts;
using ReHotelListAPI.Data;

namespace ReHotelListAPI.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        public HotelsRepository(HotelListingDbContext context) : base(context)
        {
        }
    }
}
