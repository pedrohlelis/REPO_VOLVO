using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TRABALHO_VOLVO
{
    public class ManipulacaoDadosHelper
    {
        public static double GetGerarPrevisaoRevisaoCaminhaoEspecifico(int Codigo)
        {
            try
            {
                using (var context = new TrabalhoVolvoContext())
                {
                    var caminhaoEscolhido = context.Caminhoes.FirstOrDefault(t => t.CodCaminhao == Codigo);

                    if (caminhaoEscolhido == null)
                    {
                        throw new FKNotFoundException("Nenhum Caminhao registrado possui esse codigo.");
                    }

                    var historicoManutencoes = context.ServicosManutencao
                        .Where(m => m.FkCaminhoesCodCaminhao == Codigo)
                        .OrderBy(m => m.DataManutencao)
                        .ToList();

                    double totalMeses = 0;
                    int numManutencoes = historicoManutencoes.Count;
                    
                    if(numManutencoes >= 2)
                    {
                        for (int i = 1; i < numManutencoes; i++)
                        {
                            totalMeses += (historicoManutencoes[i].DataManutencao - historicoManutencoes[i - 1].DataManutencao).TotalDays / 30;
                        }
                        double mediaMeses = numManutencoes > 1 ? totalMeses / (numManutencoes - 1) : 0;
                        return mediaMeses;
                    }
                    throw new DadosInsuficientesException("Número de manutenções insuficientes para gerar um relatório.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
