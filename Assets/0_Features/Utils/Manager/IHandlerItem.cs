namespace _0_Features.Utils.Manager.Interfaces
{
    public interface IHandlerItem<TID>
    {
        public TID ID { get; }

        public void RegisterHandlerItem();

        public void UnregisterHandlerItem();

        public void Enable();

        public void Disable();
    }
}