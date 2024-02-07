using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class TiposPecasController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostTiposPeca([FromForm] TipoPeca tipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                tipoPeca.CodTipoPeca = 0;
                tipoPeca.PecaAtivo = true;
                _context.TiposPeca.Add(tipoPeca);
                _context.SaveChanges();
                return Ok("Tipo de peça cadastrado com sucesso.");
            }
        }

        [HttpGet("Listar")]
        public List<TipoPeca> GetTodosTiposPecas()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.TiposPeca.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetTiposPeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.TiposPeca.FirstOrDefault(t => t.CodTipoPeca == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult PutTiposPeca(int Codigo, [FromForm] TipoPeca tipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.TiposPeca.FirstOrDefault(t => t.CodTipoPeca == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                item.NomeTipoPeca = tipoPeca.NomeTipoPeca;
                item.ValorTipoPeca = tipoPeca.ValorTipoPeca;
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpPut("Desativar/{Codigo}")]
        public IActionResult PutDeleteTiposPeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.TiposPeca.FirstOrDefault(t => t.CodTipoPeca == Codigo);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma Peca registrado possui esse Codigo.");
                }

                try
                {
                    var EstoquePeca = _context.EstoquePecas.Where(f => f.FkTiposPecaCodTipoPeca == Codigo);

                    foreach (var estoquePeca in EstoquePeca)
                    {
                        estoquePeca.PecaEstoqueAtiva = false;
                    }

                    item.PecaAtivo = false;
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteTiposPeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.TiposPeca.FirstOrDefault(c => c.CodTipoPeca == Codigo);

                if (item == null)
                {
                    return NotFound();
                }

                try
                {
                    var EstoquePeca = _context.EstoquePecas.Where(f => f.FkTiposPecaCodTipoPeca == Codigo);

                    foreach (var estoquePeca in EstoquePeca)
                    {
                        ManipulacaoDadosHelper.RegistrarDelete("EstoquePecas", "PecaEstoque", item);
                        _context.EstoquePecas.Remove(estoquePeca);
                    }
                    ManipulacaoDadosHelper.RegistrarDelete("TiposPeca", "TipoPeca", item);
                    _context.TiposPeca.Remove(item);
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