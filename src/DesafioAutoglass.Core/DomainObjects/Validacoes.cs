using System;

namespace DesafioAutoglass.Core.DomainDeObjetos
{
    public class Validacoes
    {
        public static void ValidarSeVazio(string valor, string mensagem)
        {
            if (valor == null || valor.Trim().Length == 0)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidarDataFabricacaoEValidade(DateTime DataDeFabricacao, DateTime DataDeValidade, string mensagem)
        {
            if (DataDeFabricacao >= DataDeValidade)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidarSeIgual(object object1, object object2, string mensagem)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(mensagem);
            }
        }
    }
}