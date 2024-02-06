using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class RelatoriosController : Controller
    {
        [HttpGet("PreverRevisaoCaminhao/{CodigoCaminhao}")]
        public IActionResult GetPreverRevisaoCaminhao(int CodigoCaminhao)
        {
            double mediaMeses = ManipulacaoDadosHelper.GetGerarPrevisaoRevisaoCaminhaoEspecifico(CodigoCaminhao);
            try
            {
                if (mediaMeses > 0)
                {
                    return Ok($"Média de meses entre as manutenções do caminhão selecionado é {mediaMeses:F2}.");
                }

                e lse
                { 
                    return Ok($"Manutenções do caminhão selecionado insuficientes para relatorio.");
                }
            }   
            catch (Exception)
            {
                throw;
            }        
        }

        [HttpGet("ListarModelosConcessionaria/{CodigoConcessionaria}")]
        public IActionResult GetListarModelosConcessionaria(int CodigoConcessionaria)
        {
            try
            {
                List<object> listaModelos = ManipulacaoDadosHelper.GetEstoqueCaminhaoPorConcesiionaria(CodigoConcessionaria);

                if (listaModelos.Count > 0)
                {
                    return Ok($"Modelos da concessionaria selecionada são: {string.Join(",", listaModelos)}.");
                }
                else
                {
                    return Ok($"Modelos da concessionaria selecionada insuficientes para relatorio.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("ListarEstoquePecaPorConcesiionaria/{CodigoConcessionaria}")]
        public IActionResult GetListarEstoquePecaPorConcesiionaria(int CodigoConcessionaria)
        {
            try
            {
                List<object> listaPecas = ManipulacaoDadosHelper.GetEstoquePecaPorConcesiionaria(CodigoConcessionaria);

                if (listaPecas.Count > 0)
                {
                    return Ok($"Peças da concessionaria selecionada são: {string.Join(",", listaPecas)}.");
                }
                else
                {
                    return Ok($"Peças da concessionaria selecionada insuficientes para relatorio.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
