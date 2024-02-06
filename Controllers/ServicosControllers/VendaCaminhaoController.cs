using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class VendaCaminhaoController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostVendaCaminhao([FromForm] VendaCaminhao vendaCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var conc = _context.Concessionarias.FirstOrDefault(c => c.CodConc == vendaCaminhao.FkConcessionariasCodConc);
                if (conc == null)
                {
                    throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse codigo.");
                }
                else if(!_context.Clientes.Any(c => c.CodCliente == vendaCaminhao.FkClientesCodCliente))
                {
                    throw new FKNotFoundException("Nenhum Cliente registrado possui esse codigo.");
                }
                else if (!_context.Funcionarios.Any(c => (c.CodFuncionario == vendaCaminhao.FkFuncionariosCodFuncionario) && c.FkConcessionariasCodConc == conc.CodConc))
                {
                    throw new FKNotFoundException("Nenhum Funcionario registrado nessa concessionaria possui esse codigo.");
                }
                else if (!_context.EstoqueCaminhao.Any(c => (c.CodCaminhaoEstoque == vendaCaminhao.FkEstoqueCaminhoesCodCaminhaoEstoque) && c.FkConcessionariasCodConc == conc.CodConc))
                {
                    throw new FKNotFoundException("Nenhum caminhao registrado no estoque da concessionaria possui esse codigo.");
                }
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var caminhao = _context.EstoqueCaminhao.FirstOrDefault(c => c.CodCaminhaoEstoque == vendaCaminhao.FkEstoqueCaminhoesCodCaminhaoEstoque);
                        var modelo = _context.ModelosCaminhoes.FirstOrDefault(c => c.CodModelo == caminhao.FkModelosCaminhoesCodModelo);
                        vendaCaminhao.CodVenda = 0;
                        vendaCaminhao.ValorVenda = modelo.ValorModeloCaminhao;
                        var novoCaminhao = new Caminhao
                        {
                            CodCaminhao = 0,
                            Quilometragem = 0,
                            PlacaCaminhao = "Undefined",
                            CorCaminhao = caminhao.CorEstoqueCaminhao,
                            CodChassiCaminhao = caminhao.CodChassiEstoque,
                            DataFabricacao = caminhao.DataFabricacao,
                            FkModelosCaminhoesCodModelo = caminhao.FkModelosCaminhoesCodModelo,
                            FkClientesCodCliente = vendaCaminhao.FkClientesCodCliente
                        };
                        _context.EstoqueCaminhao.Remove(caminhao);
                        _context.Caminhoes.Add(novoCaminhao);
                        _context.VendaCaminhoes.Add(vendaCaminhao);
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("Venda registrada com sucesso.");
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
        public List<VendaCaminhao> GetTodasVendaCaminhoes()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.VendaCaminhoes.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetVendaCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.VendaCaminhoes.FirstOrDefault(t => t.CodVenda == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma venda registrada possui esse codigo");
                }
                return new ObjectResult(item);
            }
        }
    }
}