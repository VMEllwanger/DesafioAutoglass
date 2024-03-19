using DesafioAutoglass.Core.Notificacoes;
using System.Collections.Generic;

namespace DesafioAutoglass.Core.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
