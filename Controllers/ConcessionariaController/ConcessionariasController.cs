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
                ValidationHelper.ValidateNameFormat(concessionaria.NomeConc,"Nome invalido.");
                ValidationHelper.ValidateNumericFormat(concessionaria.CepConcessionaria,"Formato do CEP invalido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Pais, "Pais invalido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Estado, "Estado invalido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Cidade, "Cidade invalido.");
                ValidationHelper.ValidateNameFormat(concessionaria.Rua, "Rua invalida.");
                try
                {
                    _context.Concessionarias.Add(concessionaria);
                    _context.SaveChanges();
                    return Ok("Concessionaria registrada com sucesso.");
                }
                catch(Exception)
                {
                    throw;
                }
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
                    throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse CEP.");
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
                    throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse CEP.");
                }
                ValidationHelper.ValidateNameFormat(concessionaria.NomeConc,"Nome invalido.");
                ValidationHelper.ValidateNumericFormat(concessionaria.CepConcessionaria,"Formato do CEP invalido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Pais, "Pais invalido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Estado, "Estado invalido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Cidade, "Cidade invalido.");
                ValidationHelper.ValidateNameFormat(concessionaria.Rua, "Rua invalida.");
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        item.NomeConc = concessionaria.NomeConc;
                        item.CepConcessionaria = concessionaria.CepConcessionaria;
                        item.Pais = concessionaria.Pais;
                        item.Estado = concessionaria.Estado;
                        item.Cidade = concessionaria.Cidade;
                        item.Rua = concessionaria.Rua;
                        item.Numero = concessionaria.Numero;
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("Os dados da concessionaria foram atualizados.");
                    }
                    catch(Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        [HttpPut("Desativar/{Cep}")]
        public IActionResult PutDeleteConcessionaria(string Cep)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Concessionarias.FirstOrDefault(t => t.CepConcessionaria == Cep);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse CEP.");
                }
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var funcionariosConcessionaria = _context.Funcionarios.Where(f => f.FkConcessionariasCodConc == item.CodConc);
                        foreach (var funcionario in funcionariosConcessionaria)
                        {
                            funcionario.FuncionarioAtivo = false;
                        }
                        item.ConcessionariaAtivo = false;
                        _context.SaveChanges();
                        return Ok("A concessionaria foi desativada com sucesso.");
                    }
                    catch(Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}