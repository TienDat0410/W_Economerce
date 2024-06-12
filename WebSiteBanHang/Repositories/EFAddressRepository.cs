using WebSiteBanHang.Interfaces;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Repositories
{
    public class EFAddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public EFAddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            return _context.Addresses.ToList();
        }

        public Address GetAddressById(int addressId)
        {
            return _context.Addresses.Find(addressId);
        }

        public void AddAddress(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        public void UpdateAddress(Address address)
        {
            _context.Addresses.Update(address);
            _context.SaveChanges();
        }

        public void DeleteAddress(int addressId)
        {
            var address = _context.Addresses.Find(addressId);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                _context.SaveChanges();
            }
        }
    }
}

