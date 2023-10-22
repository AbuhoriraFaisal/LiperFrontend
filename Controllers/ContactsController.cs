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
            try
            {
                var contacts = await ApiCaller<Contacts, string>.CallApiGet("Contacts", "", "");
                if (contacts == null)
                    return View(new List<Contact>());
                return View(contacts.contacts);
            }
            catch (Exception ex)
            {
                return View(new List<Contact>());
            }
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
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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
            try
            {
                var getcategory = await ApiCaller<GetContacts, string>.CallApiGet($"Contacts/GetById?id={id}", "", "");
                Contact contact = getcategory.contact;
                if (contact != null)
                {
                    return View(contact);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Contact contact)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, Contact>.CallApiPut($"Contacts", contact, "");
                responseMessage responseMessage = response.responseMessage;
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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
            try
            {
                var contact_result = await ApiCaller<GetContacts, string>.CallApiGet($"Contacts/GetById?id={id}", "", "");
                Contact contact = contact_result.contact;
                if (contact != null)
                {
                    return View(contact);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: ContactsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await ApiCaller<defaultResponse, string>.CallApiDelete($"Contacts?id={id}", "", "");
                if (response.responseMessage.statusCode.Equals(StatusCodes.Status200OK))
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
