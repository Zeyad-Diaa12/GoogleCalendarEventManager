namespace GoogleCalendarEventManager.DTO
{
    public static class EventValidator
    {
        public static string? CalendarEventValidator(this EventDto validateEvent)
        {
            if (validateEvent.Start > validateEvent.End)
            {
                return "ERROR: Start Date Cannot be Greater than End Date";
            }

            if (validateEvent.Start < DateTime.UtcNow || validateEvent.End < DateTime.UtcNow)
            {
                return "ERROR: Cannot Create an Event in the past";
            }

            if ((validateEvent.Start.DayOfWeek == DayOfWeek.Friday || validateEvent.Start.DayOfWeek == DayOfWeek.Saturday) || (validateEvent.End.DayOfWeek == DayOfWeek.Saturday || validateEvent.End.DayOfWeek == DayOfWeek.Friday))
            {
                return "ERROR: Cannot Create Event in Saturday or Friday. ";
            }

            return null;
        }
    }
}
