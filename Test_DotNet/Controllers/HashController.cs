﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Test_DotNet.Controllers
{
    [Route("/[controller]/")]
    [ApiController]
    public class HashController : Controller
    {
        [HttpGet("{firstName}")]
        public JsonResult GetHash(string firstName)
        {
            string input = firstName;
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            
            using (var hash = SHA256.Create())
            {
                byte[] hashedBytes = hash.ComputeHash(inputBytes);
                 StringBuilder hashedString = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    hashedString.Append(hashedBytes[i].ToString("x2"));
                }
                string reshash = hashedString.ToString().ToUpper();
                // Return a HashResponse Object which contains the hashString (alphanumeric, without '-') converted from hashedBytes
                var response = new HashResponse
                {
                    Hash = reshash
                };
                return new JsonResult(response);
            }
        }

    }

    public class HashResponse
    {
        public string Hash { get; set; }
    }
}
