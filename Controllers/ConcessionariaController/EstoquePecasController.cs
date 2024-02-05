using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class EstoquePecasController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostEstoquePeca([FromForm] AquisicaoEstoquePeca AquisicaoPecas)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                //vai verificar a integridade das FKs fornecidas
                if (!_context.Concessionarias.Any(c => c.CodConc == AquisicaoPecas.FkConcessionariasCodConc))
                {
                    throw new FKNotFoundException("Nenhuma concessionaria registrada possui esse codigo.");
                }
                else if (!_context.TiposPeca.Any(c => c.CodTipoPeca == AquisicaoPecas.FkTiposPecaCodTipoPeca))
                {
                    throw new FKNotFoundException("Nenhum Tipo de peca registrado possui esse codigo.");
                }
                //se chegou ate aqui significa que as FK inseridas estao ok, hora de tentar registrar no bd!!!
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        AquisicaoPecas.CodAquisicao = 0;
                        AquisicaoPecas.DataHora = DateTime.Now;
                        // validar os dados da aquisicao
                        // feita a validacao registrar aquisicao no historico
                        _context.AquisicoesEstoquePecas.Add(AquisicaoPecas);
                        if(AquisicaoPecas.Quantidade > 20)
                        {
                            throw new QntdAltaDemaisException("Voce nao pode adicionar mais que 20 pecas de uma vez.");
                        }
                        // Hora de registrar as pecas da aquisicao no estoque
                        for(int i = 0; i < AquisicaoPecas.Quantidade; i++)
                        {
                            PecaEstoque NovaAquisicao = new PecaEstoque
                            {
                                CodPecaEstoque = 0,
                                DataFabricacao = AquisicaoPecas.DataFabPecas,
                                FkConcessionariasCodConc = AquisicaoPecas.FkConcessionariasCodConc,
                                FkTiposPecaCodTipoPeca = AquisicaoPecas.FkTiposPecaCodTipoPeca
                            };
                            _context.EstoquePecas.Add(NovaAquisicao);
                        }
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("As peças foram adicionadas ao estoque.");
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

        [HttpGet("Listar")]
        public List<PecaEstoque> GetTodasEstoquePecas()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.EstoquePecas.ToList();
            }
        }

        [HttpGet("HistoricoAquisicoes")]
        public List<AquisicaoEstoquePeca> GetTodasAquisicoesPecas()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.AquisicoesEstoquePecas.ToList();
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