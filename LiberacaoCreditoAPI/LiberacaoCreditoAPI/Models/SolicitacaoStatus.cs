using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiberacaoCreditoAPI.Models
{
    public class SolicitacaoStatus
    {
        public bool SolicitacaoAceita { get; set; }
        
        public string MensagemSolicitacao { get; set; }

        public double ValorTotalComJuros { get; set; }

        public double ValorJuros { get; set; }
    }
}