using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiberacaoCreditoAPI.Models
{
    public class SolicitacaoLiberacaoCredito
    {
        public double VlCredito { get; set; }
        public Enums.TipoCredito TpCredito { get; set; }
        public int QtParcelas { get; set; }
        public DateTime Dt1oVencimento { get; set; }
    }
}