using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberacaoCredito.Models
{
    public class SolicitacaoLiberacaoCredito
    {
        public double VlCredito { get; set; }
        public int TpCredito { get; set; }
        public int QtParcelas { get; set; }
        public DateTime Dt1oVencimento { get; set; }
    }
}
