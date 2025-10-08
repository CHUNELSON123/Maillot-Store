namespace MaillotStore.Services
{
    public class OrderStateService
    {
        public event Action? OnOrderPlaced;

        public void NotifyOrderPlaced()
        {
            OnOrderPlaced?.Invoke();
        }
    }
}