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
    public static class GetResumeCounter
    {
        [FunctionName("GetResumeCounter")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            //This 1st binding will allows us to retrieve an item that has the id of 1
            //It'll also connect to the AzureResumeConnectionString
            //It'll look for that item inside of the collectionName container named Counter
            [CosmosDB(databaseName:"AzureResume",
                containerName: "Counter",
                Connection = "AzureResumeConnectionString",
                Id = "1",
                PartitionKey = "1")] Counter currentCounter,
            [CosmosDB(databaseName:"AzureResume",
                containerName: "Counter",
                Connection = "AzureResumeConnectionString",
                Id = "1",
                PartitionKey = "1")] out Counter updatedCounter,
            ILogger log) 
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            updatedCounter = currentCounter;
            updatedCounter.Count += 1;

            //return new OkObjectResult(updatedCounter);

            var jsonToReturn = JsonConvert.SerializeObject(currentCounter);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }
}
