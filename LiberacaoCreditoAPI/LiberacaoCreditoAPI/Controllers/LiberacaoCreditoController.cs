using LiberacaoCreditoAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace LiberacaoCreditoAPI.Controllers
{
    public class LiberacaoCreditoController : Controller
    {

        [System.Web.Http.HttpPost]
        public string SolicitarLiberacaoCredito(SolicitacaoLiberacaoCredito solicitacao)
        {
            return JsonConvert.SerializeObject(ProcessaLiberacaoCredito(solicitacao));
        }

        private SolicitacaoStatus ProcessaLiberacaoCredito(SolicitacaoLiberacaoCredito solicitacao)
        {
            var retorno = new SolicitacaoStatus() { SolicitacaoAceita = true, MensagemSolicitacao = "" };

            double valorJuros = 0;

            if (solicitacao.QtParcelas < 5)
                retorno.MensagemSolicitacao += "A quantidade de parcelas é menor que a mínima (5). \r\n";
            else if (solicitacao.QtParcelas > 72)
                retorno.MensagemSolicitacao += "A quantidade de parcelas é maior que a máxima (72). \r\n";

            if (solicitacao.Dt1oVencimento.Date < DateTime.Now.Date.AddDays(15))
                retorno.MensagemSolicitacao += "A data do vencimento da primeira parcela é menor que a mínima (" + DateTime.Now.Date.AddDays(15).ToString("dd/MM/yyyy") + "). \r\n";
            else if (solicitacao.Dt1oVencimento.Date > DateTime.Now.Date.AddDays(40))
                retorno.MensagemSolicitacao += "A data do vencimento da primeira parcela é maior que a máxima (" + DateTime.Now.Date.AddDays(40).ToString("dd/MM/yyyy") + "). \r\n";

            if (retorno.MensagemSolicitacao != string.Empty)
                retorno.SolicitacaoAceita = false;
            else
            {
                switch (solicitacao.TpCredito)
                {
                    case Enums.TipoCredito.CreditoDireto:
                        retorno.MensagemSolicitacao = ">>> CRÉDITO DIRETO APROVADO";
                        valorJuros = 0.02;
                        break;
                    case Enums.TipoCredito.CreditoConsignado:
                        retorno.MensagemSolicitacao = ">>> CRÉDITO CONSIGNADO APROVADO";
                        valorJuros = 0.01;
                        break;
                    case Enums.TipoCredito.CreditoPessoaJuridica:
                        retorno.MensagemSolicitacao = ">>> CRÉDITO PESSOA JURÍDICA APROVADO";
                        valorJuros = 0.05;
                        break;
                    case Enums.TipoCredito.CreditoPessoaFisica:
                        retorno.MensagemSolicitacao = ">>> CRÉDITO PESSOA FÍSICA APROVADO";
                        valorJuros = 0.03;
                        break;
                    case Enums.TipoCredito.CreditoImobiliario:
                        retorno.MensagemSolicitacao = ">>> CRÉDITO IMOBILIÁRIO APROVADO";
                        valorJuros = 0.09;
                        break;
                }

                retorno.ValorTotalComJuros = solicitacao.VlCredito * (1 + valorJuros);
                retorno.ValorJuros = retorno.ValorTotalComJuros - solicitacao.VlCredito;

                if (retorno.ValorTotalComJuros > 1000000.00)
                {
                    retorno.MensagemSolicitacao = "Valor maior que o máximo (R$ 1.000.000,00) . \r\n";

                    retorno.SolicitacaoAceita = false;
                }

                if (solicitacao.TpCredito == Enums.TipoCredito.CreditoPessoaJuridica && retorno.ValorTotalComJuros < 15000.00)
                {
                    retorno.MensagemSolicitacao = "Valor menor que o mínimo (R$ 15.000,00) para liberação de crédito Pessoa Jurídica. \r\n";

                    retorno.SolicitacaoAceita = false;
                }
            }

            return retorno;
        }
    }
}