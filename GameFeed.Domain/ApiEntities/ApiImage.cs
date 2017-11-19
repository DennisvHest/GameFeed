using Newtonsoft.Json;

namespace GameFeed.Domain.ApiEntities {

    public class ApiImage {

        [JsonProperty("cloudinary_id")]
        public string Id { get; set; }
    }
}
