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

        public static List<object> GetEstoqueCaminhaoPorConcesiionaria(int Codigo)
        {
            try
            {
                using (var context = new TrabalhoVolvoContext())
                {
                    var concessionariaEscolhida = context.Concessionarias.FirstOrDefault(t => t.CodConc == Codigo);

                    if (concessionariaEscolhida == null)
                    {
                        throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse codigo.");
                    }

                    var result = context.ModelosCaminhoes
                        .GroupJoin(
                            context.EstoqueCaminhao
                                .Where(ec => ec.FkConcessionariasCodConc == Codigo),
                            nm => nm.CodModelo,
                            ec => ec.FkModelosCaminhoesCodModelo,
                            (nm, ecGroup) => new
                            {
                                nm.CodModelo,
                                nm.NomeModelo,
                                Qtde_Disponivel = ecGroup.Count()
                            }
                        )
                        .ToList();

                    // Converte o resultado para uma lista de objetos
                    var resultList = result.Select(r => new { r.CodModelo, r.NomeModelo, r.Qtde_Disponivel }).Cast<object>().ToList();
                    
                    if (resultList.Count > 0)
                    {
                        return resultList;
                    }
                    throw new DadosInsuficientesException($"Número de Modelos insuficientes para gerar um relatório da concessionaria {concessionariaEscolhida.NomeConc}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<object> GetEstoquePecaPorConcesiionaria(int Codigo)
        {
            try
            {
                using (var context = new TrabalhoVolvoContext())
                {
                    var concessionariaEscolhida = context.Concessionarias.FirstOrDefault(t => t.CodConc == Codigo);

                    if (concessionariaEscolhida == null)
                    {
                        throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse codigo.");
                    }

                    var result = context.TiposPeca
                        .GroupJoin(
                            context.EstoquePecas
                                .Where(ec => ec.FkConcessionariasCodConc == Codigo),
                            nm => nm.CodTipoPeca,
                            ec => ec.FkTiposPecaCodTipoPeca,
                            (nm, ecGroup) => new
                            {
                                nm.CodTipoPeca,
                                nm.NomeTipoPeca,
                                Qtde_Disponivel = ecGroup.Count()
                            }
                        )
                        .ToList();

                    // Converte o resultado para uma lista de objetos
                    var resultList = result.Select(r => new { r.CodTipoPeca, r.NomeTipoPeca, r.Qtde_Disponivel }).Cast<object>().ToList();
                    
                    if (resultList.Count > 0)
                    {
                        return resultList;
                    }
                    throw new DadosInsuficientesException($"Número de Modelos insuficientes para gerar um relatório da concessionaria {concessionariaEscolhida.NomeConc}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
