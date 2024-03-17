using DesafioAutoglass.Core.DomainDeObjetos;
using System;

namespace DesafioAutoglass.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWorck { get; }
    }
}
