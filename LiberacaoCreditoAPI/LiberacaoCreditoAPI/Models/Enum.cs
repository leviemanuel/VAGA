using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LiberacaoCreditoAPI.Models
{
    public class Enums
    {
        public enum TipoCredito
        {
            CreditoDireto,

            CreditoConsignado,

            CreditoPessoaJuridica,

            CreditoPessoaFisica,

            CreditoImobiliario
        }
    }
}