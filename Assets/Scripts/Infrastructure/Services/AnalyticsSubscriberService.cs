using Infrastructure.EventBusSystem;

namespace Infrastructure.Services
{
    public class AnalyticsSubscriberService : IService
    {
        public AnalyticsSubscriberService(IEventBus eventBus) => Subscribe(eventBus);

        private void Subscribe(IEventBus eventBus)
        {
        }

        private void OnWatchingAd(int id)
        {
        }

        private void OnGetReward()
        {
        
        }

        private void OnShowDefeatPopup(int numberOpenLevel)
        {
        
        }
    }
}