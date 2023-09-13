using Microsoft.AspNetCore.Mvc;

namespace webapidocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
       
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }       

        [HttpGet]
        public IEnumerable<ClienteModel> Get()
        {
            ClienteService clienteService = new ClienteService();
            return clienteService.GetClientes();
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClienteModel clienteModel)
        {
            ClienteService clienteService = new ClienteService();
            clienteService.AddCliente(clienteModel);
            return Ok();
        }
    }
}