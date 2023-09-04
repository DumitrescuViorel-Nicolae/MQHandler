using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;

        public static readonly List<Booking> _bookings = new();

        public BookingsController(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;    
        }

        [HttpPost]
        public IActionResult CreateBooking(Booking booking)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bookings.Add(booking);

            _messageProducer.SendingMessages<Booking>(booking);

            return Ok(); 

        }

    }
}
