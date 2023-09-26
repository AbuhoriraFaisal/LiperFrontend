using LiperFrontend.Models;
using LiperFrontend.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiperFrontend.Controllers
{
    public class ContactsController : Controller
    {
        // GET: ContactsController
        public async Task<ActionResult> Index()
        {
            var contacts = await ApiCaller<Contacts, string>.CallApiGet("Contacts", "", "");
            return View(contacts.Item1.contacts);
        }

        // GET: ContactsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContactsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact contact)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Contact>.CallApiPost($"Contacts", contact, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var getcategory = await ApiCaller<GetContacts, string>.CallApiGet($"Contacts/GetById?id={id}", "", "");
            Contact contact = getcategory.Item1.contact;
            if (contact != null)
            {
                return View(contact);
            }
            return View();
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Contact contact)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Contact>.CallApiPut($"Contacts", contact, "");
                responseMessage responseMessage = response.Item1.responseMessage;
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var contact_result = await ApiCaller<GetContacts, string>.CallApiGet($"Contacts/GetById?id={id}", "", "");
            Contact contact = contact_result.Item1.contact;
            if (contact != null)
            {
                return View(contact);
            }
            return View();
        }

        // POST: ContactsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Contacts?id={id}", "", "");
                if (response.Item1.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
