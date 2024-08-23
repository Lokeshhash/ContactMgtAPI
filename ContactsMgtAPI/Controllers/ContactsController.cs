using ContactsMgtAPI.Models;
using ContactsMgtAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            var contacts = _contactService.GetContacts();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public IActionResult GetContactById(int id)
        {
            var contact = _contactService.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public IActionResult AddContact([FromBody] Contact contact)
        {
            //throw new Exception("Test Exception");
            _contactService.AddContact(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            var existingContact = _contactService.GetContactById(id);
            if (existingContact == null)
            {
                return NotFound();
            }

            _contactService.UpdateContact(contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var existingContact = _contactService.GetContactById(id);
            if (existingContact == null)
            {
                return NotFound();
            }

            _contactService.DeleteContact(id);
            return NoContent();
        }
    }
}
