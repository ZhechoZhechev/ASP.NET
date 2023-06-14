using Contacts.Contracts;
using Contacts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Contacts.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService contactService;

        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        public async Task<IActionResult> All()
        {
            var model = await contactService.GetAllContactsAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add() 
        {
            var model = new AddContactViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddContactViewModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            try
            {
                await contactService.AddContactAsync(model);
                return RedirectToAction("All", "Contacts");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, try again");
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int contactId)
        {
            var contactModel = await contactService.GetContactByIdAsync(contactId);
            if (contactModel == null)
            {
                return NotFound();
            }
            return View(contactModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddContactViewModel contactModel)
        {
            if (!ModelState.IsValid)
            {
                return View(contactModel);
            }

            try
            {
                await contactService.UpdateContactAsync(contactModel.Id, contactModel);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToTeam(int contactId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await contactService.AddToTeamAsync(contactId, userId);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromTeam(int contactId) 
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await contactService.RemoveFromTeamAsync(contactId, userId);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Team));
        }

        public async Task<IActionResult> Team() 
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await contactService.GetMyContactsAsync(userId);

            return View(model);
        }
    }
}
