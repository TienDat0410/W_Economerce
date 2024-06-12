using Microsoft.AspNetCore.Mvc;
using WebSiteBanHang.Interfaces;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IAddressRepository _addressRepository;

        public AddressesController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public IActionResult Index()
        {
            var addresses = _addressRepository.GetAllAddresses();
            return View(addresses);
        }

        public IActionResult Details(int id)
        {
            var address = _addressRepository.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Address address)
        {
            if (ModelState.IsValid)
            {
                _addressRepository.AddAddress(address);
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        public IActionResult Edit(int id)
        {
            var address = _addressRepository.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Address address)
        {
            if (ModelState.IsValid)
            {
                _addressRepository.UpdateAddress(address);
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        public IActionResult Delete(int id)
        {
            var address = _addressRepository.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _addressRepository.DeleteAddress(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
