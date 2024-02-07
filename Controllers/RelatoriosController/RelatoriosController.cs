using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class RelatoriosController : Controller
    {
        [HttpGet("CalcularMediaTempoEntreManutencoesCaminhao/{CodigoCaminhao}")]
        public IActionResult GetPreverRevisaoCaminhao(int CodigoCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var caminhaoEscolhido = _context.Caminhoes.FirstOrDefault(t => t.CodCaminhao == CodigoCaminhao);

                if (caminhaoEscolhido == null)
                {
                    throw new FKNotFoundException("Nenhum Caminhao registrado possui esse codigo.");
                }

                double mediaDias = ManipulacaoDadosHelper.GerarTempoEntreManutencoesCaminhao(_context, CodigoCaminhao);
                try
                {
                    if (mediaDias > 0)
                    {
                        return Ok($"Media de tempo entre as manutenções do caminhão selecionado é {mediaDias} dias.");
                    }
                    else
                    {
                        return Ok($"Manutenções do caminhão selecionado insuficientes para relatorio.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [HttpGet("TempoMedioEntreManutencoesModelos")]
        public Dictionary<int, double> GetTempoMedioEntreManutencoesModelos()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                try
                {
                    //====================================== PARTE 1 =====================================================
                    Dictionary<int, List<double>> modelosTemposInicial = ManipulacaoDadosHelper.GerarModelosTempos(_context);
                    //====================================== PARTE 2 =====================================================
                    Dictionary<int, List<double>> modelosTemposPreenchida = ManipulacaoDadosHelper.PreencherModelosTempos(_context, modelosTemposInicial);
                    //====================================== PARTE 3 =====================================================
                    Dictionary<int, double> modelosMediaTempos = ManipulacaoDadosHelper.CalcularMediaTempoModelosTempos(modelosTemposPreenchida);
                    return modelosMediaTempos;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [HttpGet("ListarEstoqueCaminhoesPorConcessionaria/{CodigoConcessionaria}")]
        public IActionResult GetListarModelosConcessionaria(int CodigoConcessionaria)
        {
            try
            {
                List<object> estoqueCaminhoes = ManipulacaoDadosHelper.GetEstoqueCaminhaoPorConcessionaria(CodigoConcessionaria);
                return Ok($"{string.Join(",", estoqueCaminhoes)}.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("ListarEstoquePecasPorConcessionaria/{CodigoConcessionaria}")]
        public IActionResult GetListarEstoquePecaPorConcessionaria(int CodigoConcessionaria)
        {
            try
            {
                List<object> listaPecas = ManipulacaoDadosHelper.GetEstoquePecaPorConcesiionaria(CodigoConcessionaria);
                return Ok($"{string.Join(",", listaPecas)}.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
