namespace TestTaskVmarmysh.Common.Exceptions
{
    /// <summary>
    /// Base typed exception.
    /// </summary>
    public abstract class BaseTypedException : Exception
    {
        /// <summary>
        /// Exception type.
        /// </summary>
        public String Type { get; private set; }

        /// <summary>
        /// Constructor of <seealso cref="TestTaskVmarmysh.Common.Exceptions.BaseTypedException"/>
        /// </summary>
        /// <param name="type">Type of exception.</param>
        /// <param name="message">Message of exception.</param>
        public BaseTypedException(string type, string message):base(message)
        {
            Type = type;
        }
    }
}
