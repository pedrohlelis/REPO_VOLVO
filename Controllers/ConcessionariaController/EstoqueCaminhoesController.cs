using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class EstoqueCaminhoesController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostEstoqueCaminhao([FromForm] CaminhaoEstoque caminhaoEstoque)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                //vai verificar a integridade das FKs fornecidas
                // if (!_context.Concessionarias.Any(c => c.CodConc == caminhaoEstoque.FkConcessionariasCodConc))
                // {
                //     throw new FKNotFoundException("Nenhuma concessionaria registrada possui esse codigo.");
                // }
                // else if (!_context.ModelosCaminhoes.Any(c => c.CodModelo == caminhaoEstoque.FkModelosCaminhoesCodModelo))
                // {
                //     throw new FKNotFoundException("Nenhum Modelo de caminhao registrado possui esse codigo.");
                // }
                //se chegou ate aqui significa que as FK inseridas estao ok, hora de tentar registrar no bd!!!
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        caminhaoEstoque.CodCaminhaoEstoque = 0;
                        // validar os dados do caminhao
                        ValidationHelper.ValidateAlphaNumericFormat(caminhaoEstoque.CodChassiEstoque, "Codigo chassi invalido.");
                        ValidationHelper.ValidateAlphaFormat(caminhaoEstoque.CorEstoqueCaminhao, "Cor de caminhao invalida.");
                        // feita a validacao tentar adicionar o caminhao ao estoque
                        _context.EstoqueCaminhao.Add(caminhaoEstoque);
                        // Hora de gerar o registro da aquisicao
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("O caminhao foi adicionado ao estoque.");
                    }
                    catch (Exception)
                    {
                        // se algo deu errado, fazer o rollback e lançar a exception
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        [HttpGet("ListarEstoque")]
        public List<CaminhaoEstoque> GetTodosEstoqueCaminhoes()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.EstoqueCaminhao.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetEstoqueCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoqueCaminhao.FirstOrDefault(t => t.CodCaminhaoEstoque == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Modelo de caminhao registrado possui esse codigo.");
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Desativar/{Codigo}")]
        public IActionResult PutDeleteEstoqueCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoqueCaminhao.FirstOrDefault(t => t.CodCaminhaoEstoque == Codigo);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Estoque registrado possui esse Codigo.");
                }
                try
                {
                    item.CaminhaoEstoqueAtivo = false;
                    _context.SaveChanges();
                    return Ok();
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }

        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteEstoqueCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoqueCaminhao.FirstOrDefault(t => t.CodCaminhaoEstoque == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Modelo de caminhao registrado possui esse codigo.");
                }
                try
                {
                    _context.EstoqueCaminhao.Remove(item);
                    _context.SaveChanges();
                    return Ok("O caminhao foi removido do estoque com sucesso.");
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
    }
}