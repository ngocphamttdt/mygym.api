using MyGym.Service.Contracts;

namespace MyGym.Service.Implementations
{
    public class GuidService : IGuidService
    {
        Guid id;
        public GuidService() {
            id = Guid.NewGuid();
        }

        public Guid GetGuid() 
        {
            return id;
        }
       
    }
}
