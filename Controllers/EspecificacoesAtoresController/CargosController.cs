using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class CargosController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult Post([FromForm] Cargo cargo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                cargo.CodCargo = 0;
                _context.Cargos.Add(cargo);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpGet("Listar")]
        public List<Cargo> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Cargos.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult Get(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Cargos.FirstOrDefault(t => t.CodCargo == Codigo);

                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult Put(int Codigo, [FromForm] Cargo Cargo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Cargos.FirstOrDefault(t => t.CodCargo == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                item.NomeCargo = Cargo.NomeCargo;
                item.SalarioBase = Cargo.SalarioBase;
                item.PorcentagemComissao = Cargo.PorcentagemComissao;
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult Delete(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var cargoParaExcluir = _context.Cargos.FirstOrDefault(c => c.CodCargo == Codigo);

                if (cargoParaExcluir == null)
                {
                    return NotFound();
                }

                var funcionariosComCargo = _context.Funcionarios.Where(f => f.FkCargosCodCargo == Codigo);

                foreach (var funcionario in funcionariosComCargo)
                {
                    funcionario.FkCargosCodCargo = null;
                }
                _context.Cargos.Remove(cargoParaExcluir);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}