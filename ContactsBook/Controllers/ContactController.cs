using ContactsBook.Domain.Entities.Contacts;
using ContactsBook.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : Controller
    {
        public readonly IContactDomainService _contactDomainService;

        public ContactController(IContactDomainService contactDomainService)
        {
            _contactDomainService = contactDomainService;
        }

        /// <summary>
        /// Gets a list of contacts
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Contact>>> Get(CancellationToken cancellationToken)
        {
            var contacts = await _contactDomainService.GetAsync(cancellationToken);

            return Ok(contacts);
        }

        /// <summary>
        /// Creates contact
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Contact>>> Create(Contact contact, CancellationToken cancellationToken)
        {
            var result = await _contactDomainService.CreateAsync(contact, cancellationToken);

            return Ok(result);
        }
    }
}