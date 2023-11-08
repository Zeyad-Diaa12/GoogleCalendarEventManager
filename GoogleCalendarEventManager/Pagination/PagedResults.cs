using GoogleCalendarEventManager.Models;

namespace GoogleCalendarEventManager.Pagination
{
    public class PagedResults
    {
        public IEnumerable<GoogleEvent> Items { get; set; }
        public string NextPageToken { get; set; }
    }
}
