﻿using API.Utilities.Handlers;
using Client.Contracts;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Repositories
{
    public class GeneralRepository<Entity, createEntity, TId> : IRepository<Entity, createEntity, TId>
         where Entity : class
    {
        protected readonly string request;
        private readonly HttpContextAccessor contextAccessor;
        protected readonly HttpClient httpClient;

        //constructor
        public GeneralRepository(string request)
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7290/api/")
            };
            contextAccessor = new HttpContextAccessor();
        
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));
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
