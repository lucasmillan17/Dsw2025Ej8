using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Ej8.Excepciones;

namespace Dsw2025Ej8.Domain
{
    public class CuentaCorriente : CuentaBancaria
    {
        public decimal LimiteDeDescubierto { get; init; }
        public decimal Comision { get; set; }
        
        public CuentaCorriente(string numero, decimal saldo, string[] titulares) : base(numero, saldo, titulares) 
        {
            Estado = Estado.Activa;
        }

        public override void Depositar(decimal monto)
        {
            if (Estado != Estado.Activa) throw new CuentaNoActiva(" No se puede operar con la cuenta" + Estado.ToString());
            if (monto <= 0) throw new MontoNoValido("El monto ingresado no es válido para la operación solicitada");
            monto -= monto * Comision;
            Saldo += monto;
        }

        public override void Retirar(decimal monto)
        {
            if (Estado != Estado.Activa) throw new CuentaNoActiva(" No se puede operar con la cuenta " + Estado.ToString());
            if ((Saldo-monto)<-LimiteDeDescubierto)
            {
                Estado = Estado.Suspendida;
                throw new SaldoInsuficiente("La cuenta no cuenta con saldo para la operación solicitada.Fue suspendida.");
            }

            /*
            if (Saldo - monto >= -LimiteDeDescubierto)
            {
                Saldo -= monto;
            }
            */

            Saldo -= monto;
        }
    }
}
