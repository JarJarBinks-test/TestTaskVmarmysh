using TestTaskVmarmysh.Services.Interfaces;

namespace TestTaskVmarmysh.Services.Services
{
    internal class RequestIdService: IRequestIdService
    {
        public long Id { get; private set; }

        public RequestIdService(IRequestIdStorage requestIdStorage) {
            Id = requestIdStorage.GetNextId();
        }
    }
}
