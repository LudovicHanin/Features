using System.Collections.Generic;

namespace _0_Features.Utils.Manager.Interfaces
{
    public interface IHandler<TID, TItem> where TItem : IHandlerItem<TID>
    {
        public Dictionary<TID, TItem> HandlerItems { get; }

        public TItem Get(TID id);

        public TID Get(TItem item);

        bool Exist(TID id);

        bool Exist(TItem item);
    
        public void RegisterItem(TItem item);

        public void UnregisterItem(TItem item);

        public void UnregisterItem(TID id);
    }
}