using LiberacaoCredito.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LiberacaoCredito
{
    class Program
    {
        #region DADOS
        private static double valorCredito = 0;
        private static int tipoCredito = 0;
        private static int qtParcelas = 0;
        private static DateTime? dt1oVencimento;

        private static SolicitacaoStatus solicitacao;
        #endregion

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("===== PROCESSAMENTO DE LIBERAÇÃO DE CRÉDITO =====");
                Console.WriteLine("");

                SolicitaDados();

                solicitacao = SolicitaLiberacaoCredito();

                Console.WriteLine("------------------------------------------");

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");


                if (solicitacao.SolicitacaoAceita)
                {
                    Console.WriteLine("");
                    Console.WriteLine(solicitacao.MensagemSolicitacao);
                    Console.WriteLine("Valor total com juros: " + solicitacao.ValorTotalComJuros.ToString("0,000.00"));
                    Console.WriteLine("Valor dos juros: " + solicitacao.ValorJuros.ToString("0,000.00"));
                }
                else
                {
                    Console.WriteLine(">>> CRÉDITO RECUSADO");
                    Console.WriteLine("Motivo(s):");
                    Console.WriteLine(solicitacao.MensagemSolicitacao);
                }

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Deseja iniciar outro processamento? (S/N)");

                valorCredito = 0;
                tipoCredito = 0;
                qtParcelas = 0;
                dt1oVencimento = null;

                if (Console.ReadLine().ToUpper() == "S")
                    Console.Clear();
                else
                    break;
            }


        }

        private static void SolicitaDados()
        {
            while (valorCredito == 0)
            {
                Console.WriteLine("Informe o valor do crédito:");

                if (double.TryParse(Console.ReadLine(), out double result))
                    valorCredito = result;
            }

            while (tipoCredito < 1 || tipoCredito > 6)
            {
                Console.WriteLine("");
                Console.WriteLine("Informe uma das opções de Tipo do crédito:");
                Console.WriteLine("(Informe apenas o número)");
                Console.WriteLine("[1] Crédito Direto");
                Console.WriteLine("[2] Crédito Consignado");
                Console.WriteLine("[3] Crédito Pessoa Jurídica");
                Console.WriteLine("[4] Crédito Pessoa Física");
                Console.WriteLine("[5] Crédito Imobiliário");
                if (int.TryParse(Console.ReadLine(), out int result))
                    tipoCredito = result;
            }

            while (qtParcelas == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Informe a quantidade de parcelas:");
                if (int.TryParse(Console.ReadLine(), out int result))
                    qtParcelas = result;
            }

            while (dt1oVencimento == null)
            {
                Console.WriteLine("");
                Console.WriteLine("Informe a data do primeiro vencimento (no formato dd/mm/aaaa):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime result))
                    dt1oVencimento = result;
            }
        }

        private static SolicitacaoStatus SolicitaLiberacaoCredito()
        {
            SolicitacaoStatus retorno = new SolicitacaoStatus();

            SolicitacaoLiberacaoCredito solicitacao = new SolicitacaoLiberacaoCredito()
            {
                VlCredito = valorCredito,
                TpCredito = tipoCredito - 1,
                QtParcelas = qtParcelas,
                Dt1oVencimento = dt1oVencimento.Value
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392/LiberacaoCredito/");
                var content = new StringContent(JsonConvert.SerializeObject(solicitacao), Encoding.UTF8, "application/json");
                var resp = client.PostAsync("SolicitarLiberacaoCredito", content).Result;
                retorno = JsonConvert.DeserializeObject<SolicitacaoStatus>(resp.Content.ReadAsStringAsync().Result);
            }

            return retorno;
        }
    }
}
