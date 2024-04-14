namespace ConsoleApp20
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await GetExchangeRates();
        }

        static async Task GetExchangeRates()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        dynamic exchangeRates = JsonConvert.DeserializeObject(responseBody);

                        foreach (var rate in exchangeRates)
                        {
                            Console.WriteLine($"Currency: {rate.ccy}, Buy: {rate.buy}, Sell: {rate.sale}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to get exchange rates. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
