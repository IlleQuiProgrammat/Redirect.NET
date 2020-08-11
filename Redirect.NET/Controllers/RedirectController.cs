using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Redirect.NET.Models;

namespace Redirect.NET.Controllers
{
    [ApiController]
    [Route("/")]
    public class RedirectController : ControllerBase
    {
        private UrlContext _context;
        private readonly string _defaultUrl;
        private readonly string _password;
        
        public RedirectController(UrlContext context, IConfiguration configuration)
        {
            _context = context;
            _password = configuration.GetValue<string>("Password");
            _defaultUrl = configuration.GetValue<string>("DefaultUrl");
        }
        
        public class SetterParams
        {
            public string RedirectUrl { get; set; }
            public string Password { get; set; }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> SetUrlById(string id, SetterParams setterParams)
        {
            if (setterParams.Password == _password)
            {
                var dbUrl = await _context.Urls.FindAsync(id);
                
                if (dbUrl != null)
                {
                    dbUrl.Id = id;
                    dbUrl.UsageCount = 0;
                    dbUrl.RedirectUrl = setterParams.RedirectUrl;
                    dbUrl.LastUpdate = DateTime.Now;
                    dbUrl.LastUsed = DateTime.MinValue;
                }
                else
                {
                    _context.Add(new Url
                    {
                        Id = id,
                        RedirectUrl = setterParams.RedirectUrl,
                        LastUpdate = DateTime.Now,
                        LastUsed = DateTime.MinValue,
                        UsageCount = 0
                    });
                }
                
                await _context.SaveChangesAsync();
                return Ok();
            }

            return Unauthorized();
        }

        public class GetterParams
        {
            public string Password { get; set; }
        }
        
        [HttpPost("/get/{id}")]
        public async Task<IActionResult> GetUrlInfoById(string id, GetterParams getterParams)
        {
            if (getterParams.Password == _password)
            {
                if (id == "all")
                {
                    return Ok(await _context.Urls.ToListAsync());
                }

                var url = await _context.Urls.FindAsync(id);
                if (url == null) return NotFound();
                return Ok(url);
            }

            return Unauthorized();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RedirectById(string id)
        {
            var url = await _context.Urls.FindAsync(id);
            if (url == null) return Redirect(_defaultUrl);
            url.UsageCount++;
            url.LastUsed = DateTime.Now;
            await _context.SaveChangesAsync();
            return Redirect(url.RedirectUrl);
        }
        
    }
}