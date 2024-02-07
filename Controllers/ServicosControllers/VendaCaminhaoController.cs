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
                ValidationHelper.ValidatePostVenda(vendaCaminhao, _context);
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var estoqueCaminhao = _context.EstoqueCaminhao.FirstOrDefault(c => c.CodCaminhaoEstoque == vendaCaminhao.FkEstoqueCaminhoesCodCaminhaoEstoque
                                                                                            && c.CaminhaoEstoqueAtivo == true);
                        if (estoqueCaminhao == null)
                        {
                            throw new FKNotFoundException("Voce esta sem estoque para realizar essa venda!");
                        }
                        var modelo = _context.ModelosCaminhoes.FirstOrDefault(c => c.CodModelo == estoqueCaminhao.FkModelosCaminhoesCodModelo);
                        var novoCaminhao = new Caminhao
                        {
                            CodCaminhao = 0,
                            Quilometragem = 0,
                            PlacaCaminhao = "Undefined",
                            CorCaminhao = estoqueCaminhao.CorEstoqueCaminhao,
                            CodChassiCaminhao = estoqueCaminhao.CodChassiEstoque,
                            DataFabricacao = estoqueCaminhao.DataFabricacao,
                            FkModelosCaminhoesCodModelo = estoqueCaminhao.FkModelosCaminhoesCodModelo,
                            FkClientesCodCliente = vendaCaminhao.FkClientesCodCliente
                        };
                        vendaCaminhao.CodVenda = 0;
                        vendaCaminhao.ValorVenda = modelo.ValorModeloCaminhao;
                        _context.VendaCaminhoes.Add(vendaCaminhao);
                        _context.Caminhoes.Add(novoCaminhao);
                        estoqueCaminhao.CaminhaoEstoqueAtivo = false;
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("Venda registrada com sucesso.");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        ValidationHelper.FilterExceptionsPostVendaCaminhao(e.InnerException.Message);
                        throw new NotImplementedException();
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

        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteVendaCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.VendaCaminhoes.FirstOrDefault(t => t.CodVenda == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma venda registrada possui esse codigo");
                }
                try
                {
                    ManipulacaoDadosHelper.RegistrarDelete("VendaCaminhoes", "VendaCaminhao", item);
                    _context.VendaCaminhoes.Remove(item);
                    _context.SaveChanges();
                    return Ok("A Venda foi deletada.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}