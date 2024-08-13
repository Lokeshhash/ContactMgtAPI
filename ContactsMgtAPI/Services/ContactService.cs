using ContactsMgtAPI.Models;
using System.Text.Json;

namespace ContactsMgtAPI.Services
{
    public class ContactService
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "contacts.json");

        public IEnumerable<Contact> GetContacts()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<IEnumerable<Contact>>(json);
        }

        public Contact GetContactById(int id)
        {
            return GetContacts().FirstOrDefault(c => c.Id == id);
        }

        public void AddContact(Contact contact)
        {
            var contacts = GetContacts().ToList();
            contact.Id = contacts.Any() ? contacts.Max(c => c.Id) + 1 : 1;
            contacts.Add(contact);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(contacts));
        }

        public void UpdateContact(Contact contact)
        {
            var contacts = GetContacts().ToList();
            var existingContact = contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Email = contact.Email;
                File.WriteAllText(_filePath, JsonSerializer.Serialize(contacts));
            }
        }

        public void DeleteContact(int id)
        {
            var contacts = GetContacts().ToList();
            var contact = contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                contacts.Remove(contact);
                File.WriteAllText(_filePath, JsonSerializer.Serialize(contacts));
            }
        }
    }
}
