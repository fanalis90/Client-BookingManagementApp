using API.Utilities.Handlers;
using Client.Contracts;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories
{
    public class GeneralRepository<Entity, createEntity, TId> : IRepository<Entity, createEntity, TId>
         where Entity : class
    {
        private readonly string request;
        private readonly HttpContextAccessor contextAccessor;
        private HttpClient httpClient;

        //constructor
        public GeneralRepository(string request)
        {
            this.request = request;
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7290/api/")
            };
            //contextAccessor = new HttpContextAccessor();
            // Ini yg bawah skip dulu
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));
        }

        public async Task<ResponseOkHandler<Entity>> Delete(TId id)
        {
            ResponseOkHandler<Entity> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = httpClient.DeleteAsync(request + id).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<Entity>>(apiResponse);
            }
            return entityVM;
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
        public async Task<ResponseOkHandler<EmployeeDetailsDto>> GetDetail(Guid id)
        {
            ResponseOkHandler<EmployeeDetailsDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = await httpClient.GetAsync(request + "details/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<EmployeeDetailsDto>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOkHandler<Entity>> Get(TId id)
        {
            ResponseOkHandler<Entity> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
            using (var response = await httpClient.GetAsync(request+id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<Entity>>(apiResponse);
            }
            return entityVM;
        }
        


        public async Task<ResponseOkHandler<Entity>> Post(createEntity entity)
        {
            ResponseOkHandler<Entity> entityVM = null;
            string entityJson = JsonConvert.SerializeObject(entity);
            HttpContent httpContent = new StringContent(entityJson, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(request, httpContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<Entity>>(apiResponse);
            }
                
            return entityVM;
        }

        public async Task<ResponseOkHandler<Entity>> Put(TId id, Entity entity)
        {
            ResponseOkHandler<Entity> entityVM = null;
            string entityJson = JsonConvert.SerializeObject(entity);
            HttpContent httpContent = new StringContent(entityJson, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync(request, httpContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<Entity>>(apiResponse);
            }

            return entityVM;
        }
    }
}
