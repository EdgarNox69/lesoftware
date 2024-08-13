using AutoMapper;
using lesoftware.Server.DTOs;
using lesoftware.Server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace lesoftware.Server.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: Controller
    {
        private readonly TiendaContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        
        static string cadena = "Data Source=EDGARPC;Initial Catalog=Tienda;user id=tienda;pwd=hatter4CARGO.dissuade0complex;TrustServerCertificate=True";

        public AccountController(TiendaContext context, IMapper mapper, IConfiguration configuration)

        {
            this.context = context;
            this.mapper = mapper;
            this._configuration = configuration;
           
        }

        [HttpPost]
        [Route("registrar")]
        public async Task<ActionResult> Registrar(ClienteCredentials clienteCredentials)
        {
            if (clienteCredentials == null)
            {
                return BadRequest("Cliente es null");
            }

            var usuarioDTO = mapper.Map<Cliente>(clienteCredentials);
            usuarioDTO.Password = ConvertirSha256(usuarioDTO.Password);

            context.Add(usuarioDTO);
            await context.SaveChangesAsync();

            return Ok(usuarioDTO);
            // return CreatedAtRoute("obtenerUsuario", new { usuarioId = usuarioDTO.Id }, usuarioDTO);
        }
        //Hce validacion de username y contraseña en storedprocedure de la BD y genera token
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(ClienteDTO usuario)
        {
            var usuarioDTO = mapper.Map<Cliente>(usuario);
            usuarioDTO.Password = ConvertirSha256(usuarioDTO.Password);

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_VALIDAR_USUARIO", cn);
                cmd.Parameters.AddWithValue("Nombre", usuarioDTO.Nombre);
                cmd.Parameters.AddWithValue("Password", usuarioDTO.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteScalar();

                usuarioDTO.Id = Convert.ToInt32(cmd.ExecuteScalar());

            }


            //si las credenciales no coinciden
            if (usuarioDTO.Id == 0)
            {
                return BadRequest("Las credenciales no coinciden");
            }

            var token = TokenBuilder(usuarioDTO);

            Result res = new Result
            {
                id = usuarioDTO.Id,
                token = token
            };

            return Ok(res);

        }
        //Hace validacion aqui en el backend y se trae el usuario de la bd y genera token
        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult> IniciarSesion(ClienteDTO usuario)
        {
            var usuarioDTO = mapper.Map<Cliente>(usuario);
            usuarioDTO.Password = ConvertirSha256(usuarioDTO.Password);

            string user = usuarioDTO.Nombre.ToString();
            string contrasena = usuarioDTO.Password.ToString();


            var usuariobd = await context.Clientes
                 .Where(x => x.Nombre == user && x.Password == contrasena)
                 .FirstOrDefaultAsync();

            if (usuariobd == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            var token = TokenBuilder(usuariobd);

            return Ok(token);
        }



        public string TokenBuilder(Cliente usuariobd)
        {
            var jwt = _configuration.GetSection("Jwt").Get<AuthResponse>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, DateTime.UtcNow.ToString()),
                new Claim("id", usuariobd.Id.ToString()),
                new Claim("nombre", usuariobd.Nombre)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var SignIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokencontent = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: SignIn
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokencontent).ToString();

            return token;
        }


        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}
