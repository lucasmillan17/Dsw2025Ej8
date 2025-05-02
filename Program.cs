using Dsw2025Ej8.Domain;
using Dsw2025Ej8.Excepciones;

namespace Dsw2025Ej8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Creamos cajas de ahorro
            CajaDeAhorro caja1 = new CajaDeAhorro("A1", 3000, ["Lucas"]) { TasaDeInteres = 0.05M };
            CajaDeAhorro caja2 = new CajaDeAhorro("A2", 10000, ["Fabri"]) { TasaDeInteres = 0.04M };

            //Creamos cuentas corrientes
            CuentaCorriente cuenta1 = new CuentaCorriente("C1", 5000, ["Juan"]) { LimiteDeDescubierto = 5000M };
            CuentaCorriente cuenta2 = new CuentaCorriente("C2", 3000, ["Javi", "Gonzalo"]) { LimiteDeDescubierto = 1500 };

            #region Comprobación de funciones

            try     //Probamos con caja1
            {
                caja1.Depositar(1000);
                caja1.Retirar(2000);
                caja1.Retirar(2001);    //Forzamos a que el saldo sea insuficiente para retirar y comprobar la excepcion
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }

            try     //caja1 (ahora suspendida)
            {
                caja1.Depositar(1000);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }

            try     //Probamos con caja2
            {
                caja2.Retirar(5000);
                caja2.AplicarInteres();
                caja2.Depositar(0);     //Depositamos 0 por lo que generaria una excepcion
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }

            try     //Probamos con cuenta1
            {
                cuenta1.Retirar(6000);
                cuenta1.Depositar(2000);
                cuenta1.Retirar(6001);  //Forzamos a que el saldo sea menor al Limite de descubierto al retirar para comprobar excepcion

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }

            try     //cuenta1 (ahora suspendida)
            {
                cuenta1.Depositar(6000);
                cuenta1.Retirar(1000);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }

            try     //Probamos con cuenta2
            {
                cuenta2.Retirar(4000);
                cuenta2.Depositar(0);   //Depositamos 0 para generar la excepcion
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }

            #endregion

            #region Listar Resumenes

            var cuentas = new List<CuentaBancaria>();
            cuentas.Add(cuenta1);
            cuentas.Add(cuenta2);
            cuentas.Add(caja1);
            cuentas.Add(caja2);


            var resumenCuentas = cuentas.Select(c => new
            {
                Numero = c.Numero,
                Tipo = c.GetType().Name,
                Titulares = string.Join(", ", c.Titulares),
                Estado = c.Estado.ToString(),
                Saldo = c.Saldo
            }
            );

            foreach ( var cuenta in resumenCuentas)
            {
                Console.WriteLine($"Cuenta N° {cuenta.Numero} - Tipo: {cuenta.Tipo} - Titulares: {cuenta.Titulares} - Estado: {cuenta.Estado} - Saldo: ${cuenta.Saldo}");
            }

            #endregion

        }
    }
}
