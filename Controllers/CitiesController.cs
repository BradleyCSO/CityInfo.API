﻿using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController] // Not necessary but improves development experience when building APIs, this should be documented as we go on
    [Route("api/cities")]
    public class CitiesController : ControllerBase // Using controller base here as a derived class. If we inherited from the Controller class, it has additional helper methods for when returning views1q
    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(CitiesDataStore.Current.Cities);
        }
        [HttpGet("{id}")]
        public JsonResult GetCity(int id)
        {
            return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(get => get.Id == id));
        }
    }
}
