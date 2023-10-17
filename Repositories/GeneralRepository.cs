using API.Utilities.Handlers;
using Client.Contracts;
using Newtonsoft.Json;

namespace Client.Repositories
{
    public class GeneralRepository<Entity, TId> : IRepository<Entity, TId>
         where Entity : class
    {
        private readonly string request;
        private readonly HttpContextAccessor contextAccessor;
        private HttpClient httpClient;

        //constructor
        public GeneralRepository(string request)
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7290/api/")
            };
            //contextAccessor = new HttpContextAccessor();
            // Ini yg bawah skip dulu
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));
        }

        public Task<ResponseOkHandler<Entity>> Delete(TId id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseOkHandler<IEnumerable<Entity>>> Get()
        {
            ResponseOkHandler<IEnumerable<Entity>> entityVM = null;
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<IEnumerable<Entity>>>(apiResponse);
            }
            return entityVM;
        }

        public Task<ResponseOkHandler<Entity>> Get(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseOkHandler<Entity>> Post(Entity entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseOkHandler<Entity>> Put(TId id, Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
