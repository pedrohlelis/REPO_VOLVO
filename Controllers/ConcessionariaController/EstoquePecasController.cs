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
                if (!_context.Concessionarias.Any(c => c.CodConc == peca.FkConcessionariasCodConc))
                {
                    throw new FKNotFoundException("Nenhuma concessionaria registrada possui esse codigo.");
                }
                else if (!_context.TiposPeca.Any(c => c.CodTipoPeca == peca.FkTiposPecaCodTipoPeca))
                {
                    throw new FKNotFoundException("Nenhum Tipo de peca registrado possui esse codigo.");
                }
                //se chegou ate aqui significa que as FK inseridas estao ok, hora de tentar registrar no bd!!!
                try
                {
                    // validar os dados da peca
                    ValidationHelper.ValidateDateOnly($"{peca.DataFabricacao}", "Data de fabricacao invalida.");
                    // Hora de registrar as pecas da aquisicao no estoque
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
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.EstoquePecas.ToList();
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

        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteEstoquePeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoquePecas.FirstOrDefault(t => t.CodPecaEstoque == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Tipo de peca registrado possui esse codigo.");
                }
                _context.EstoquePecas.Remove(item);
                _context.SaveChanges();
                return Ok("A peca foi removida do estoque com sucesso.");
            }
        }
    }
}