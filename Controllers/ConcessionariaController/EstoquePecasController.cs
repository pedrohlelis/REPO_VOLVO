using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class EstoquePecasController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostEstoquePeca([FromForm] PecaEstoque peca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                //vai verificar a integridade das FKs fornecidas
                // if (!_context.Concessionarias.Any(c => c.CodConc == peca.FkConcessionariasCodConc))
                // {
                //     throw new FKNotFoundException("Nenhuma concessionaria registrada possui esse codigo.");
                // }
                // else if (!_context.TiposPeca.Any(c => c.CodTipoPeca == peca.FkTiposPecaCodTipoPeca))
                // {
                //     throw new FKNotFoundException("Nenhum Tipo de peca registrado possui esse codigo.");
                // }
                //se chegou ate aqui significa que as FK inseridas estao ok, hora de tentar registrar no bd!!!
                try
                {
                    // validar os dados da peca
                    ValidationHelper.ValidateDateOnly($"{peca.DataFabricacao}", "Data de fabricacao invalida.");
                    // Hora de registrar as pecas da aquisicao no estoque
                    _context.EstoquePecas.Add(peca);
                    _context.SaveChanges();
                    return Ok("A peça foi adicionada ao estoque.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [HttpGet("Listar")]
        public List<PecaEstoque> GetTodasEstoquePecas()
        {
            try
            {
                using (var _context = new TrabalhoVolvoContext())
                {
                    return _context.EstoquePecas.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetEstoquePeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoquePecas.FirstOrDefault(t => t.CodPecaEstoque == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Tipo de peca registrado possui esse codigo.");
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Desativar/{Codigo}")]
        public IActionResult PutDeleteEstoquePeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoquePecas.FirstOrDefault(t => t.CodPecaEstoque == Codigo);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Estoque registrado possui esse Codigo.");
                }
                try
                {
                    item.PecaEstoqueAtiva = false;
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}