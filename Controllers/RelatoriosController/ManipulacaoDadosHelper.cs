using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TRABALHO_VOLVO
{
    public class ManipulacaoDadosHelper
    {
        public static double GerarTempoEntreManutencoesCaminhao(TrabalhoVolvoContext context, int Codigo)
        {
            try
            {
                var historicoManutencoes = context.ServicosManutencao
                    .Where(m => m.FkCaminhoesCodCaminhao == Codigo)
                    .OrderBy(m => m.DataManutencao)
                    .ToList();
                double totalDias = 0;
                int numManutencoes = historicoManutencoes.Count;
                if (numManutencoes >= 2)
                {
                    for (int i = 1; i < numManutencoes; i++)
                    {
                        totalDias += (historicoManutencoes[i].DataManutencao - historicoManutencoes[i - 1].DataManutencao).TotalDays;
                    }
                    double mediaDias = numManutencoes > 1 ? totalDias / (numManutencoes - 1) : 0;
                    return Math.Round(mediaDias, 1);
                }
                else { return 0; }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Dictionary<int, List<double>> GerarModelosTempos(TrabalhoVolvoContext context)
        {
            Dictionary<int, List<double>> modelosTempos = new Dictionary<int, List<double>>();
            List<ModelosCaminhao> ListaModelos = context.ModelosCaminhoes.ToList();
            foreach (ModelosCaminhao mc in ListaModelos)
            {
                List<double> values = new List<double>();
                modelosTempos.Add(mc.CodModelo, values);
            }
            return modelosTempos;
        }
        public static Dictionary<int, List<double>> PreencherModelosTempos(TrabalhoVolvoContext context, Dictionary<int, List<double>> modelosTempos)
        {
            List<ServicoManutencao> ListaServicosManutencao = context.ServicosManutencao.ToList();
            foreach (ServicoManutencao sm in ListaServicosManutencao)
            {
                Caminhao caminhao = context.Caminhoes.FirstOrDefault(c => c.CodCaminhao == sm.FkCaminhoesCodCaminhao);
                List<double> valuesList = modelosTempos[caminhao.FkModelosCaminhoesCodModelo];
                valuesList.Add(GerarTempoEntreManutencoesCaminhao(context, caminhao.CodCaminhao));
            }
            return modelosTempos;
        }

        public static Dictionary<int, double> CalcularMediaTempoModelosTempos(Dictionary<int, List<double>> modelosTempos)
        {
            Dictionary<int, double> modelosMediaTempos = new Dictionary<int, double>();
            foreach (var pair in modelosTempos)
            {
                List<double> Tempos = pair.Value;
                if (Tempos.Count > 0)
                {
                    double mediaTempo = Tempos.Average();
                    modelosMediaTempos.Add(pair.Key, Math.Round(mediaTempo, 1));
                }
                else
                {
                    modelosMediaTempos.Add(pair.Key, 0);
                }
            }
            return modelosMediaTempos;
        }
        public static List<object> GetEstoqueCaminhaoPorConcessionaria(int Codigo)
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
