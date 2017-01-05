using Microsoft.Extensions.Logging;

namespace DataCommand.Core
{
    /// <summary> 
    ///     Values that are used as the eventId when logging messages from the Apolo4 Command API. 
    /// </summary> 
    public static class CommandEventId
    {
        /// <summary>
        /// A generic error.
        /// </summary>
        public static EventId GenericError = 0;

        /// <summary>
        /// An error occurred while opening connections.
        /// </summary>
        public static EventId ConnectionError = 1;

        /// <summary>
        /// An error reported by the database backend, for instance: SQL issues.
        /// </summary>
        public static EventId DatabaseError = 2;
    }
}
