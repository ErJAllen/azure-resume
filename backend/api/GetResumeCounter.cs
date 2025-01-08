using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Worker;
using System.Data.Common;
using Microsoft.Azure.Cosmos;
using System.Text;
using Microsoft.Identity.Client;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Company.Function
{
    public static class GetResumeCounter{

    [FunctionName("GetResumeCounter")]
    public static HttpResponseMessage Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        [CosmosDB(databaseName: "AzureResume",
            containerName: "Counter",
            Connection = "AzureResumeConnectionString",
            Id = "1",
            PartitionKey = "1")] Counter currentCounter,
        [CosmosDB(databaseName: "AzureResume",
            containerName: "Counter",
            Connection = "AzureResumeConnectionString",
            Id = "1",
            PartitionKey = "1")] out Counter updatedCounter,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        if (currentCounter == null)
        {
            log.LogError("Counter document not found in Cosmos DB.");
            updatedCounter = null;
            return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
            {
                Content = new StringContent("Counter document not found.", Encoding.UTF8, "application/json")
            };
        }

        updatedCounter = currentCounter;
        updatedCounter.Count += 1;

        var jsonToReturn = JsonConvert.SerializeObject(updatedCounter);

        log.LogInformation($"Counter updated to: {updatedCounter.Count}");

        return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
        };
    }
    }
}
