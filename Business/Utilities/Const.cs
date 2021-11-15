using System.Collections.Generic;

namespace Fravega.Business.Utilities
{
    public static class MediosDePago
    {
        public static string TarjetaCredito = "TARJETA_CREDITO";
        public static string TarjetaDebito = "TARJETA_DEBITO";
        public static string Efectivo = "EFECTIVO";
        public static string GiftCard = "GIFT_CARD";

        public static string Error = "Uno de los metodos de pago no es valido";

        public static List<string> ListaDePagos()
        {
            var list = new List<string>
            {
                TarjetaCredito,
                TarjetaDebito,
                Efectivo,
                GiftCard
            };

            return list;
        }
    }

    public static class Bancos
    {
        public static string Galicia = "Galicia";
        public static string SantanderRio = "Santander Rio";
        public static string Ciudad = "Ciudad";
        public static string Nacion = "Nacion";
        public static string ICBC = "ICBC";
        public static string BBVA = "BBVA";
        public static string Macro = "Macro";

        public static string Error = "Uno de los bancos no es valido";
        public static List<string> ListaDeBancos()
        {
            var list = new List<string>
            {
                Galicia,
                SantanderRio,
                Ciudad,
                Nacion,
                ICBC,
                BBVA,
                Macro
            };

            return list;
        }
    }

    public static class CategoriasProductos
    {
        public static string Hogar = "Hogar";
        public static string Jardin = "Jardin";
        public static string ElectroCocina = "ElectroCocina";
        public static string GrandesElectro = "GrandesElectro";
        public static string Colchones = "Colchones";
        public static string Celulares = "Celulares";
        public static string Tecnologia = "Tecnologia";
        public static string Audio = "Audio";

        public static string Error = "Una de las categorias no es valida";

        public static List<string> ListaDeCategorias()
        {
            var list = new List<string>
            {
                Hogar,
                Jardin,
                ElectroCocina,
                GrandesElectro,
                Colchones,
                Celulares,
                Tecnologia,
                Audio
            };

            return list;
        }
    }
}
