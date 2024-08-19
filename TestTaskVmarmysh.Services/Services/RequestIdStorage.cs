using TestTaskVmarmysh.Services.Interfaces;

namespace TestTaskVmarmysh.Services.Services
{
    /// <summary>
    /// Storage for request id.
    /// </summary>
    public class RequestIdStorage: IRequestIdStorage
    {
        long _id = DateTimeOffset.UtcNow.Ticks;

        /// <summary>
        /// Last request id.
        /// </summary>
        public long id { get => _id; }

        /// <summary>
        /// Constructor for <seealso cref="TestTaskVmarmysh.Services.Services.RequestIdStorage"/>
        /// </summary>
        public RequestIdStorage() {
        }

        /// <inheritdoc />
        public long GetNextId()
        {
            return Interlocked.Add(ref _id, 1);
        }
    }
}
