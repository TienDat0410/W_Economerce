using WebSiteBanHang.Models;

namespace WebSiteBanHang.Interfaces
{
    public interface IAddressRepository
    {
        IEnumerable<Address> GetAllAddresses();
        Address GetAddressById(int addressId);
        void AddAddress(Address address);
        void UpdateAddress(Address address);
        void DeleteAddress(int addressId);
    }
}
