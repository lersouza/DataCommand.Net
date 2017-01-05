using System;

namespace DataCommand.Core
{
    /// <summary>
    /// Represents a command's execution statistics, with useful information about the command performance.
    /// </summary>
    public sealed class CommandStatistics
    {
        #region Fields

        private TimeSpan _lastElapsedTime;
        private TimeSpan _lastExecElapsedTime;

        #endregion

        /// <summary>
        /// Gets or sets a reference name for this statistics.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the min elapsed time for a command execution
        /// </summary>
        public TimeSpan MinElapsedTime { get; private set; }

        /// <summary>
        /// Gets or sets the last command's execution elapsed time.
        /// </summary>
        public TimeSpan LastElapsedTime
        {
            get
            {
                return _lastElapsedTime;
            }
            set
            {
                _lastElapsedTime = value;

                if (MinElapsedTime == TimeSpan.MinValue || value < MinElapsedTime)
                    MinElapsedTime = value;

            }
        }

        /// <summary>
        /// Gets the min elapsed time for a command execution, except for the connection establishment.
        /// </summary>
        public TimeSpan MinExecElapsedTime { get; private set; }

        /// <summary>
        /// Gets or sets the last command's execution elapsed time, except for the connection establishment..
        /// </summary>
        public TimeSpan LastExecElapsedTime
        {
            get
            {
                return _lastExecElapsedTime;
            }
            set
            {
                _lastExecElapsedTime = value;

                if (MinExecElapsedTime == TimeSpan.MinValue || value < MinExecElapsedTime)
                    MinExecElapsedTime = value;
            }
        }
    }
}
