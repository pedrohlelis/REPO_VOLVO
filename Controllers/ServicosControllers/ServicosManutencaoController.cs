using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class ServicoManutencaoController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostServicoManutencao([FromForm] ServicoManutencao servicoManutencao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                //verificar a integridade das FKs, e se o funcionario passado trabalha na concessionaria passada.
                var conc = _context.Concessionarias.FirstOrDefault(c => c.CodConc == servicoManutencao.FkConcessionariasCodConc);
                if (conc == null)
                {
                    throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse codigo.");
                }
                if (!_context.Funcionarios.Any(c => (c.CodFuncionario == servicoManutencao.FkFuncionariosCodFuncionario) && c.FkConcessionariasCodConc == conc.CodConc))
                {
                    throw new FKNotFoundException("Nenhum Funcionario registrado nessa concessionaria possui esse codigo.");
                }
                else if (!_context.Caminhoes.Any(c => c.CodCaminhao == servicoManutencao.FkCaminhoesCodCaminhao))
                {
                    throw new FKNotFoundException("Nenhum Caminhao registrado possui esse codigo.");
                }
                var caminhao = _context.Caminhoes.FirstOrDefault(c => c.CodCaminhao == servicoManutencao.FkCaminhoesCodCaminhao);
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        caminhao.Quilometragem = servicoManutencao.QuilometragemCaminhao;
                        servicoManutencao.CodManutencao = 0;
                        _context.ServicosManutencao.Add(servicoManutencao);
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("O servico foi registrado com sucesso.");
                    }
                    catch(Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        [HttpPost("CadastrarPecaUsada/{CodigoManutencao}/{CodigoPeca}")]
        public IActionResult PostServicoTipoPeca([FromForm] ServicoTipoPeca servicoTipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                if (_context.ServicosManutencao.Any(c => c.CodManutencao == servicoTipoPeca.FkCodManutencao)
                && _context.TiposPeca.Any(c => c.CodTipoPeca == servicoTipoPeca.FkTiposPecaCodTipoPeca))
                {
                }
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        servicoTipoPeca.CodServicoTipoPeca = 0;
                        var pecaEstoque = _context.EstoquePecas.FirstOrDefault(c => c.FkTiposPecaCodTipoPeca == servicoTipoPeca.FkTiposPecaCodTipoPeca);
                        if(pecaEstoque == null)
                        {
                            throw new FKNotFoundException("A concessionaria nao possui estoque desse tipo de peca no momento.");
                        }
                        _context.EstoquePecas.Remove(pecaEstoque);
                        _context.ServicoTiposPeca.Add(servicoTipoPeca);
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("A peca foi registrada no servico e removida do estoque");
                    }
                    catch(Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        [HttpGet("Listar")]
        public List<ServicoManutencao> GetTodosServicosManutencao()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.ServicosManutencao.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetServicoManutencao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ServicosManutencao.FirstOrDefault(t => t.CodManutencao == Codigo);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Servico registrado possui esse codigo.");
                }
                return new ObjectResult(item);
            }
        }

        [HttpGet("Pecas/byServicoID/{Codigo}")]
        public IActionResult GetPecasServicoManutencao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var servico = _context.ServicosManutencao.FirstOrDefault(t => t.CodManutencao == Codigo);

                if (servico == null)
                {
                    throw new FKNotFoundException("Nenhum Servico registrado possui esse codigo.");
                }
                var pecas = _context.ServicoTiposPeca
                    .Where(s => s.FkCodManutencao == servico.CodManutencao)
                    .Select(s => s.FkTiposPecaCodTipoPeca)
                    .ToList();
                return new ObjectResult(pecas);
            }
        }

        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult PutServicoManutencao(int Codigo, [FromForm] ServicoManutencao servicoManutencao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ServicosManutencao.FirstOrDefault(t => t.CodManutencao == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Servico registrado possui esse codigo.");
                }
                item.DescricaoManutencao = servicoManutencao.DescricaoManutencao;
                _context.SaveChanges();
                return Ok("Os dados do servico foram atualizados com sucesso.");
            }
        }

        [HttpPut("Delete/{Codigo}")]
        public IActionResult DeleteServicoManutencao(int Codigo, [FromForm] ServicoManutencao servicoManutencao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ServicosManutencao.FirstOrDefault(t => t.CodManutencao == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Servico registrado possui esse codigo.");
                }
                _context.ServicosManutencao.Remove(item);
                _context.SaveChanges();
                return Ok("O servico foi apagado com sucesso.");
            }
        }
    }
}