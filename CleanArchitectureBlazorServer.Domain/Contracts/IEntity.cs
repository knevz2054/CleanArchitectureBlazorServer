using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Common.Contracts
{
    public interface IEntity<TId> : IEntity
    {
        TId Id { get; set; }
    }
    public interface IEntity
    {
    }
}
