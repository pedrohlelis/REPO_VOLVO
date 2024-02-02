using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class ConcessionariasController : Controller
    {
        [HttpPost]
        public void Post([FromBody] Concessionaria concessionaria)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                _context.Concessionarias.Add(concessionaria);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<Concessionaria> Get()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Concessionarias.ToList();
            }
        }

        [HttpGet("{Cep}")]
        public IActionResult Get(string Cep)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Concessionarias.FirstOrDefault(t => t.CepConcessionaria == Cep);

                if(item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("{Cep}")]
        public void Put(string Cep,[FromBody] Concessionaria concessionaria)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Concessionarias.FirstOrDefault(t => t.CepConcessionaria == Cep);
                if(item == null)
                {
                    return; 
                }
                item.NomeConc = concessionaria.NomeConc;
                item.CepConcessionaria = concessionaria.CepConcessionaria;
                item.Pais = concessionaria.Pais;
                item.Estado = concessionaria.Estado;
                item.Cidade = concessionaria.Cidade;
                item.Rua = concessionaria.Rua;
                item.Numero = concessionaria.Numero;
                _context.SaveChanges();
            }
        }

        [HttpPut("Delete/{Cep}")]
        public void Put(string Cep)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Concessionarias.FirstOrDefault(t => t.CepConcessionaria== Cep);
                if(item == null)
                {
                    return; 
                }
                item.ConcessionariaAtivo = false;
                _context.SaveChanges();
            }
        }
    }
}