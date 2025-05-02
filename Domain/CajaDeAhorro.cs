using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Ej8.Excepciones;

namespace Dsw2025Ej8.Domain
{
    public class CajaDeAhorro : CuentaBancaria
    {
        public decimal TasaDeInteres { get; init; }
        public CajaDeAhorro(string numero, decimal saldo, string[] titulares) : base(numero, saldo, titulares) { }

        public override void Depositar(decimal monto) {
            if (Estado != Estado.Activa) throw new CuentaNoActiva(" No se puede operar con la cuenta" + Estado.ToString());
            if (monto <= 0) throw new MontoNoValido("El monto ingresado no es válido para la operación solicitada");
            Saldo += monto;
        }

        public override void Retirar(decimal monto)
        {
            if (Estado != Estado.Activa) throw new CuentaNoActiva(" No se puede operar con la cuenta" + Estado.ToString());
            if (monto <= 0) throw new MontoNoValido("El monto ingresado no es válido para la operación solicitada");
            if ((Saldo - monto) < 0)
            {
                Estado = Estado.Suspendida;
                throw new SaldoInsuficiente("La cuenta no cuenta con saldo para la operación solicitada.Fue suspendida.");
            }

            Saldo -= monto;
        }
        public void AplicarInteres()
        {
            if (Estado != Estado.Activa) throw new CuentaNoActiva(" No se puede operar con la cuenta" + Estado.ToString());
            Saldo += Saldo * TasaDeInteres;
        }
    }
}
