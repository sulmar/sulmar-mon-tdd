﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehiclesContext context;

        public VehiclesController()
        {
            context = new VehiclesContext();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vehicle = context.Vehicles.Find(id);
            context.Vehicles.Remove(vehicle);
            context.SaveChanges();
            return RedirectToAction("Vehicles");
        }
    }
}
