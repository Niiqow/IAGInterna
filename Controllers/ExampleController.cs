using IAGInterna.Data;
using IAGInterna.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;

namespace IAGInterna.Controllers
{
    [EnableCors("corspolicy")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class ExampleController : Controller
    {

        public static Example example = new Example();

        private readonly IConfiguration _configuration;

        public ExampleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private ConsultaExample consulta = new ConsultaExample();


        [HttpGet]
        public IActionResult Example()
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            var list = consulta.Example();

            string token = CreateToken(example);
            return Ok(token);
        }



        private string CreateToken(Example example)
        {
            List<Claim> claims = new List<Claim>();
            var options = new JsonSerializerOptions { WriteIndented = true };
            claims.Add(new Claim("data", JsonSerializer.Serialize<Example>(example)));

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Crear un objeto anónimo para contener el token y el ID
            var response = new
            {
                token = jwt
            };

            // Serializar el objeto a JSON
            var jsonResponse = JsonSerializer.Serialize(response, options);

            return jsonResponse;
        }


    }

}
