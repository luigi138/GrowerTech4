
using Microsoft.AspNetCore.Mvc;
using GrowerTech_MVC.DTO;
using GrowerTech_MVC.Services;

namespace GrowerTech_MVC.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgricultoresAPI : ControllerBase
    {
        private readonly AgricultorService _service;

        public AgricultoresAPI(AgricultorService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAgricultores()
        {
            var agricultores = _service.GetAllAgricultores();
            return Ok(agricultores);
        }

        [HttpPost]
        public IActionResult CreateAgricultor([FromBody] AgricultorDTO agricultorDTO)
        {
            _service.CreateAgricultor(agricultorDTO);
            return Ok();
        }
    }
}
