using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using GoogleCalendarEventManager.DTO;
using GoogleCalendarEventManager.Models;
using GoogleCalendarEventManager.Pagination;
using System.Net;

namespace GoogleCalendarEventManager.Service.Implementation
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly string _calendarId;
        private readonly CalendarService _calendarService;

        public GoogleCalendarService()
        {
            _calendarId = "primary";
            var credential = GetCredential();
            _calendarService = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });
        }

        public async Task<GoogleEvent> CreateEvent(EventDto newEvent)
        {
            try
            {
                Event eventIns = new Event()
                {
                    Summary = newEvent.Summary,
                    Description = newEvent.Description,
                    Location = newEvent.Location,
                    Start = new EventDateTime
                    {
                        DateTime = newEvent.Start,
                        TimeZone = "Africa/Cairo"
                    },
                    End = new EventDateTime
                    {
                        DateTime = newEvent.End,
                        TimeZone = "Africa/Cairo"
                    }
                };
                var request = _calendarService.Events.Insert(eventIns, _calendarId);
                var reqConvert = await request.ExecuteAsync();
                return new GoogleEvent()
                {
                    ID = reqConvert.Id,
                    Summary = reqConvert.Summary,
                    Description = reqConvert.Description,
                    Location = reqConvert.Location,
                    Start = reqConvert.Start.DateTime.Value,
                    End = reqConvert.End.DateTime.Value
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PagedResults> GetAllEvents(string pageToken = null, int pageSize = 10)
        {
            try
            {
                var request = _calendarService.Events.List(_calendarId);
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
                request.PageToken = pageToken;
                request.MaxResults = pageSize;

                var eventsReturn = await request.ExecuteAsync();

                var allEventsConv = new List<GoogleEvent>();

                foreach (var eventItem in eventsReturn.Items)
                {
                    var googleEvent = new GoogleEvent
                    {
                        ID = eventItem.Id,
                        Summary = eventItem.Summary,
                        Description = eventItem.Description,
                        Location = eventItem.Location,
                    };

                    if (eventItem.Start != null && eventItem.Start.DateTime != null)
                    {
                        googleEvent.Start = eventItem.Start.DateTime.Value;
                    }

                    if (eventItem.End != null && eventItem.End.DateTime != null)
                    {
                        googleEvent.End = eventItem.End.DateTime.Value;
                    }

                    allEventsConv.Add(googleEvent);
                }

                var nextPageToken = eventsReturn.NextPageToken;

                PagedResults response = new PagedResults
                {
                    Items = allEventsConv,
                    NextPageToken = nextPageToken
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<PagedResults> GetEventsFilterByName(string searchKey, string pageToken = null, int pageSize = 10)
        {
            try
            {
                var request = _calendarService.Events.List(_calendarId);
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
                request.PageToken = pageToken;
                request.MaxResults = pageSize;

                var eventsReturn = await request.ExecuteAsync();

                var filteredEvents = eventsReturn.Items
                    .Where(eventItem => eventItem.Summary != null && eventItem.Summary.Contains(searchKey, StringComparison.OrdinalIgnoreCase))
                    .Select(eventItem => new GoogleEvent
                    {
                        ID = eventItem.Id,
                        Summary = eventItem.Summary,
                        Description = eventItem.Description,
                        Location = eventItem.Location,
                        Start = (DateTime)(eventItem.Start?.DateTime.Value),
                        End = (DateTime)(eventItem.End?.DateTime.Value)

                    })
                    .ToList();

                var nextPageToken = eventsReturn.NextPageToken;

                PagedResults response = new PagedResults
                {
                    Items = filteredEvents,
                    NextPageToken = nextPageToken
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<PagedResults> GetEventsFilterByDate(DateTime startDate, DateTime endDate, string pageToken = null, int pageSize = 10)
        {
            try
            {
                var request = _calendarService.Events.List(_calendarId);
                request.TimeMin = startDate;
                request.TimeMax = endDate;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
                request.PageToken = pageToken;
                request.MaxResults = pageSize;

                var eventsReturn = await request.ExecuteAsync();

                var allEventsConv = new List<GoogleEvent>();

                foreach (var eventItem in eventsReturn.Items)
                {
                    allEventsConv.Add(new GoogleEvent()
                    {
                        ID = eventItem.Id,
                        Summary = eventItem.Summary,
                        Description = eventItem.Description,
                        Location = eventItem.Location,
                        Start = (DateTime)(eventItem.Start?.DateTime.Value),
                        End = (DateTime)(eventItem.End?.DateTime.Value)
                    });
                }

                var nextPageToken = eventsReturn.NextPageToken;

                PagedResults response = new PagedResults
                {
                    Items = allEventsConv,
                    NextPageToken = nextPageToken
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteEvent(string eventId)
        {
            try
            {
                var exists = _calendarService.Events.Get(_calendarId, eventId);
                if (exists != null)
                {
                    var request = _calendarService.Events.Delete(_calendarId, eventId);
                    await request.ExecuteAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (GoogleApiException ex)
            {
                if (ex.HttpStatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserCredential GetCredential()
        {
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "ClientSecret", "Secret.json"), FileMode.Open, FileAccess.Read))
            {
                var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    new[] { CalendarService.Scope.Calendar },
                    "user",
                    CancellationToken.None).Result;

                return credential;
            }
        }
    }
}
