using Core;
using Microsoft.AspNet.Mvc;
using RawRabbit;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class PrimesController
    {
        private readonly IBusClient _bus;

        public PrimesController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public void Post([FromBody] IsPrimeRequest request)
        {
            _bus.PublishAsync(new IsPrimeCommand {Number = request.Number});
        }
    }

    public class IsPrimeRequest
    {
        public ulong Number { get; set; }
    }
}