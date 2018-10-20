﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace SpaceApps.Controllers.RawData
{
    [Route("api/[controller]")]
    public class LaunchController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://launchlibrary.net/1.4/");
                    var response = await client.GetAsync($"launch/next/5");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<Models.RawData.Launch[]>(stringResult);
                    return Ok("Helloo");
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }
    }
}