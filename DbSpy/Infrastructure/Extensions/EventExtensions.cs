using System.ComponentModel;

namespace DbSpy.Infrastructure.Extensions
{
    public static class EventExtensions
    {
        public static void Raise(this PropertyChangedEventHandler eventToRaise, object sender, string propertyName)
        {
            var eventHandler = eventToRaise;

            if (eventHandler != null)
                eventHandler(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}
