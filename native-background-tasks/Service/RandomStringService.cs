namespace NativeBackgroundTasks.Service
{
    public class RandomStringService(HttpClient httpClient)
    {
        private const string RandomStringURL = "https://www.random.org/strings/?num=1&len=10&digits=on&upperalpha=on&loweralpha=on&unique=on&format=plain&rnd=new";

        private readonly HttpClient _httpClient = httpClient;
        public List<string> RandomString { get; set; } = [];

        public async Task GetRandomStringAsync(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetAsync(RandomStringURL, cancellationToken);
                response.EnsureSuccessStatusCode();
                var randomString = await response.Content.ReadAsStringAsync(cancellationToken);
                RandomString.Add(randomString.Trim());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);   
            }
        }
    }
}
