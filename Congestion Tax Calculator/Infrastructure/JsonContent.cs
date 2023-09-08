using Congestion_Tax_Calculator.Domain;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace Congestion_Tax_Calculator.Infrastructure
{
    /// <summary>
    /// If my manager tells me to create a solution for managing content outside the application, as mentioned in the Bonus Scenario section,
    /// I would suggest using a JSON file where each country can update the values as they see fit. or we can make it in the one of relational database line sqllite or sql server
    /// </summary>
    public class JsonDataContent
    {
        private static string JsonFileURL = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")).Root + "/DataSource/Content.json";
        public static TaxRuleBase GetDataSourceAsJson()
        {
            if (File.Exists(JsonFileURL))
            {
                string jsonContent = File.ReadAllText(JsonFileURL);
                try
                {
                    TaxRuleBase taxRules = JsonConvert.DeserializeObject<TaxRuleBase>(jsonContent);
                    return taxRules;
                }
                catch {
                    return null;
                }
            }
            return null;
        }
    }
}
