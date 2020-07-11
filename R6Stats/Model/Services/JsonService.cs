using Newtonsoft.Json;
using System.IO;

namespace R6Stats
{
    class JsonService<T> : IDataService<T>
        where T : new()
    {
        public string Path { get; set; }

        public T Load()
        {
            if (File.Exists(Path))
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(Path));
            return new T();
        }

        public void Save(T data)
        {
            File.WriteAllText(Path, JsonConvert.SerializeObject(data));
        }
    }
}
