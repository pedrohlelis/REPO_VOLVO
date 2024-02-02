using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class CargosController : Controller
    {
        [HttpPost]
        public void Post([FromBody] Cargo cargo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                _context.Cargos.Add(cargo);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<Cargo> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Cargos.ToList();
            }
        }

        [HttpGet("{Codigo}")]
        public IActionResult Get(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Cargos.FirstOrDefault(t => t.CodCargo == Codigo);

                if(item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("{Codigo}")]
        public void Put(int Codigo,[FromBody] Cargo Cargo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Cargos.FirstOrDefault(t => t.CodCargo == Codigo);
                if(item == null)
                {
                    return; 
                }
                item.NomeCargo = Cargo.NomeCargo;
                item.SalarioBase = Cargo.SalarioBase;
                item.PorcentagemComissao = Cargo.PorcentagemComissao;
                _context.SaveChanges();
            }
        }

        [HttpDelete("{Codigo}")]
        public IActionResult Delete(int Codigo)
        {   
            var _context = new TrabalhoVolvoContext();
            
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