﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using ReaderAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VesselsApi.Models;

namespace VesselsApi.Controllers {
	[Route("api/[controller]")]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private readonly BasicDbContext _context;
        private readonly IReader _reader;
        private readonly IMemoryCache _cache;

        public BasicController(BasicDbContext context, IReader reader, IMemoryCache cache)
        {
            _cache = cache;
            _reader = reader;
            _context = context;
        }

        // GET: api/Basic/refresh
        [HttpGet("refresh")]
        public async Task<IActionResult> GetBasicRefresh() {

            string json = await _reader.GetBasics();

            if (json != null) {
                _context.basic.RemoveRange(_context.basic);
                _context.AddRange(JsonConvert.DeserializeObject<List<Basic>>(json));
                _context.SaveChanges();
            }

            return NoContent();
        }

        // GET: api/Basic/cache
        [HttpGet("cache")]
        public async Task<IActionResult> GetLocations() {

            string gl = await _reader.GetLocations();

            var list =JsonConvert.DeserializeObject<List<Location>>(gl);

			foreach (var location in list) {
				_cache.Set(
					location.VesselID,
					JsonConvert.DeserializeObject<Position>(JsonConvert.SerializeObject(location)),
					new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10))
				);
			}

            return Ok();
        }

        // GET: api/Basic
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Basic>>> Getbasic() {
            return await _context.basic.ToListAsync();
        }

        // GET: api/Basic/location/18
        [HttpGet("location/{id}")]
        public ActionResult<Position> GetPosition(int id) {
            Position p;
			return _cache.TryGetValue(id, out p) ? p : null;
		}
    }
}
