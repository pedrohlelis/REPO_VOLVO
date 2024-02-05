using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class ConcessionariasController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostConcessionaria([FromForm] Concessionaria concessionaria)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                concessionaria.CodConc = 0;
                concessionaria.ConcessionariaAtivo = true;
                _context.Concessionarias.Add(concessionaria);
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpGet("Listar")]
        public List<Concessionaria> GetTodasConcessionarias()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Concessionarias.ToList();
            }
        }

        [HttpGet("Buscar/{Cep}")]
        public IActionResult GetConcessionaria(string Cep)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Concessionarias.FirstOrDefault(t => t.CepConcessionaria == Cep);

                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Cep}")]
        public IActionResult PutConcessionaria(string Cep, [FromForm] Concessionaria concessionaria)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Concessionarias.FirstOrDefault(t => t.CepConcessionaria == Cep);
                if (item == null)
                {
                    return NotFound();
                }
                item.NomeConc = concessionaria.NomeConc;
                item.CepConcessionaria = concessionaria.CepConcessionaria;
                item.Pais = concessionaria.Pais;
                item.Estado = concessionaria.Estado;
                item.Cidade = concessionaria.Cidade;
                item.Rua = concessionaria.Rua;
                item.Numero = concessionaria.Numero;
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpPut("Deletar/{Cep}")]
        public IActionResult PutDeleteConcessionaria(string Cep)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Concessionarias.FirstOrDefault(t => t.CepConcessionaria == Cep);
                if (item == null)
                {
                    return NotFound();
                }
                
                var funcionariosConcessionaria = _context.Funcionarios.Where(f => f.FkConcessionariasCodConc == item.CodConc);
                foreach (var funcionario in funcionariosConcessionaria)
                {
                    funcionario.FuncionarioAtivo = false;
                }

                item.ConcessionariaAtivo = false;
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}