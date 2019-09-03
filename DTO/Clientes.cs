using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Clientes
    {
        public int IdCliente { get; set; }
        public String Nome { get; set; }
        public String CPF { get; set; }
        public String DataNascimento { get; set; }
        public Decimal LimiteDeCredito { get; set; }
    }
}
