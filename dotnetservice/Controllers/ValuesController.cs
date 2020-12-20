using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using System.Threading;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace dotnetservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        Random rnd = new Random();
        private readonly NpgsqlConnection _dbConnection;
        public ValuesController([FromServices] NpgsqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            List<string> tables = new List<string>();

            _dbConnection.Open();
            DataTable dt = _dbConnection.GetSchema("Databases");
            _dbConnection.Close();
            foreach (DataRow row in dt.Rows)
            {
                string tablename = (string)row[2];
                tables.Add(tablename);
            }

            // Add random sleep
            int sleepWaitMilliSeconds = rnd.Next(100, 200);
            Thread.Sleep(sleepWaitMilliSeconds);

            return tables;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] JsonElement value)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(value);

            // call an external service
            string baseUrl = "https://httpbin.org/status/200";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            HttpResponseMessage response = client.GetAsync(baseUrl).Result; 
            if (response.IsSuccessStatusCode)
            {
                int sleepWaitMilliSeconds = rnd.Next(100, 200);
                // slowdown and sometimes return error
                if (sleepWaitMilliSeconds > 150 )
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
                else
                {
                    Thread.Sleep(sleepWaitMilliSeconds); // simulate long process
                    return Ok(value);
                }
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            client.Dispose();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
