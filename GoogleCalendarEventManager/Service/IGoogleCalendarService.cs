using Google.Apis.Auth.OAuth2;
using GoogleCalendarEventManager.DTO;
using GoogleCalendarEventManager.Models;
using GoogleCalendarEventManager.Pagination;

namespace GoogleCalendarEventManager.Service
{
    public interface IGoogleCalendarService
    {
        public Task<GoogleEvent> CreateEvent(EventDto newEvent);
        public Task<PagedResults> GetAllEvents(string pageToken = null, int pageSize = 10);
        public Task<PagedResults> GetEventsFilterByName(string searchKey, string pageToken = null, int pageSize = 10);
        public Task<PagedResults> GetEventsFilterByDate(DateTime startDate, DateTime endDate, string pageToken = null, int pageSize = 10);
        public Task<bool> DeleteEvent(string eventId);
        public UserCredential GetCredential();
    }
}
