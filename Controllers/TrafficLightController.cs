using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Docker.Models;
using Microsoft.EntityFrameworkCore;
using Server.DataBase;
using System.Security.Cryptography;
using System.Text;
using Server.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrafficLightController : ControllerBase
    {
        private PostgreDataBase _db;

        public TrafficLightController(PostgreDataBase db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<List<TrafficLight>> GetTrafficLight()
        {
            var traffic_lights = _db.traffic_lights.ToList();
            return traffic_lights
                .Join(_db.districts, t => t.district_id, d => d.id, PostgreDataBase.join_district).ToList();
        }
    }
}
