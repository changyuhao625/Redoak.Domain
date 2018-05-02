using Redoak.Domain.Model.Models;

namespace Redoak.Domain.Service
{
    public abstract class BaseService
    {
        public RedoakContext Context;

        public BaseService(RedoakContext context)
        {
            Context = context;
        }
    }
}