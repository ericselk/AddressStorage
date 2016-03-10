// Just a quick/dirty example to show the library does work... judge lightly :)

using AddressStorage.Services.AddressStorage;
using AddressStorage.Services.AddressStorage.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Sample.mdf in solution is set to always copy to output folder, so a new one will be created on each run.
                // You may change the connection string to use SQL Server if you like.  Just a quick/dirty example that should be easy to run.
                var mdfPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\Sample.mdf");
                var connectionString = string.Format(@"data source = (LocalDB)\MSSQLLocalDB; attachdbfilename = {0}; integrated security = True; MultipleActiveResultSets = True", mdfPath);
                var repository = new AddressStorageRepository(connectionString);
                var addressStorageService = new AddressStorageService(repository);

                var testAddress = new Address
                {
                    AddressLine1 = "300 E Main St",
                    AddressLine2 = "Suite 300",
                    City = "Brawley",
                    Company = "City of Brawley",
                    Name = "John Doe",
                    Zip = "90210",
                    State = new State
                    {
                        Abbreviation = "AZ"
                    }
                };

                var id = addressStorageService.Add(testAddress);

                var checkAddress = addressStorageService.GetById(id);

                Console.WriteLine("Added and fetched address:");
                Console.WriteLine(JsonConvert.SerializeObject(checkAddress, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();
        }
    }
}