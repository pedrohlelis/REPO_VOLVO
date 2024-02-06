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
                if(mediaMeses > 0)
                {
                    return Ok($"Media de meses entre as manutenções do caminhão selecionado é {mediaMeses:2F}.");
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
}
