﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActualPlaylistBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyAuthController : ControllerBase
    {
        // GET: api/<SpotifyAuthController>
        [HttpGet("token/{code}")]
        public IEnumerable<string> Get(string code)
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SpotifyAuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SpotifyAuthController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SpotifyAuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SpotifyAuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
