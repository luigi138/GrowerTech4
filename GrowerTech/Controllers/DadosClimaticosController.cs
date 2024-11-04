using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrowerTech_MVC.Data;
using GrowerTech_MVC.Models;
using System.Text;    // 添加这行用于 StringBuilder
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GrowerTech_MVC.Controllers
{
    public class DadosClimaticosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DadosClimaticosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DadosClimaticos
        public async Task<IActionResult> Index()
        {
            var dados = await _context.DadosClimaticos
                .Include(d => d.Sensor)
                .OrderByDescending(d => d.Data)
                .ToListAsync();
            return View(dados);
        }

        // GET: DadosClimaticos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dadoClimatico = await _context.DadosClimaticos
                .Include(d => d.Sensor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dadoClimatico == null)
            {
                return NotFound();
            }

            return View(dadoClimatico);
        }

        // POST: DadosClimaticos/Create (API Endpoint)
        [HttpPost]
        [Route("api/dadosclimaticos")]
        public async Task<IActionResult> CreateDado([FromBody] DadoClimatico dado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.DadosClimaticos.Add(dado);
                await _context.SaveChangesAsync();

                // 检查是否需要发送警报
                if (dado.IsTemperaturaAlta() || dado.IsUmidadeAlta())
                {
                    // TODO: 实现警报系统
                    // await _alertService.SendAlert(dado);
                }

                return Ok(dado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: DadosClimaticos/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var ultimosDados = await _context.DadosClimaticos
                .Include(d => d.Sensor)
                .OrderByDescending(d => d.Data)
                .Take(24) // 最后24小时的数据
                .ToListAsync();

            return View(ultimosDados);
        }

        // GET: DadosClimaticos/Export
        public async Task<IActionResult> Export()
        {
            var dados = await _context.DadosClimaticos
                .Include(d => d.Sensor)
                .OrderByDescending(d => d.Data)
                .ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Data,Sensor,Temperatura,Umidade,Localização");

            foreach (var dado in dados)
            {
                csv.AppendLine($"{dado.Data:yyyy-MM-dd HH:mm:ss},{dado.Sensor.Tipo},{dado.Temperatura},{dado.Umidade},{dado.Sensor.Localizacao}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            var result = new FileContentResult(bytes, "text/csv");
            result.FileDownloadName = $"dados-climaticos-{DateTime.Now:yyyy-MM-dd}.csv";

            return result;
        }
    }
}