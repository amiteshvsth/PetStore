using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Text;

namespace PetStore.utilities
{
    public class JsonHelpes<TDto>
    {
        public StringContent ObjectToJsonString(object dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public TDto DeserializeJsonObject(string jsonFilePath)
        {
            return JsonConvert.DeserializeObject<TDto>(JObject.Parse(File.ReadAllText(jsonFilePath)).ToString());
        }
    }
}
