// BACKEND: TFG-BACK/Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Text;
using TFG_BACK.Models.DTOs; // Asegúrate de que esta ruta sea correcta

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    
    public AuthController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }
    
    [HttpPost("registro")]
    public async Task<ActionResult<AuthResponseDTO>> Registro([FromBody] RegistroDTO registro)
    {
        // Verificar si el usuario ya existe
        if (await _usuarioService.GetByGmailAsync(registro.Gmail) != null)
        {
            return BadRequest("El correo electrónico ya está registrado");
        }
        
        // Hashear la contraseña con SHA256 (método integrado en .NET)
        string passwordHash = ComputeSha256Hash(registro.Contraseña);
        
        // Crear el nuevo usuario
        var nuevoUsuario = new Usuario
        {
            Nombre = registro.Nombre,
            Apellidos = registro.Apellidos,
            Gmail = registro.Gmail,
            Telefono = registro.Telefono,
            Contraseña = passwordHash,
            IdRol = registro.IdRol
        };
        
        await _usuarioService.AddAsync(nuevoUsuario);
        
        // Obtener el usuario completo (con su ID generado)
        var usuarioCreado = await _usuarioService.GetByGmailAsync(registro.Gmail);
        
        // Crear un token simple (podría ser un GUID, por ejemplo)
        string token = Guid.NewGuid().ToString();
        
        // En una aplicación real, deberías almacenar este token en algún lugar (base de datos, caché, etc.)
        // para validarlo posteriormente
        
        return Ok(new AuthResponseDTO
        {
            IdUsuario = usuarioCreado.IdUsuario,
            Nombre = usuarioCreado.Nombre,
            Token = token,
            Rol = usuarioCreado.Rol?.Nombre ?? "Usuario"
        });
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginDTO login)
    {
        // Buscar el usuario por correo
        var usuario = await _usuarioService.GetByGmailAsync(login.Gmail);
        if (usuario == null)
        {
            return Unauthorized("Credenciales inválidas");
        }
        
        // Verificar la contraseña
        string hashedPassword = ComputeSha256Hash(login.Contraseña);
        if (usuario.Contraseña != hashedPassword)
        {
            return Unauthorized("Credenciales inválidas");
        }
        
        // Crear un token simple (podría ser un GUID, por ejemplo)
        string token = Guid.NewGuid().ToString();
        
        // En una aplicación real, deberías almacenar este token en algún lugar (base de datos, caché, etc.)
        // para validarlo posteriormente
        
        return Ok(new AuthResponseDTO
        {
            IdUsuario = usuario.IdUsuario,
            Nombre = usuario.Nombre,
            Token = token,
            Rol = usuario.Rol?.Nombre ?? "Usuario"
        });
    }
    
    // Método para hashear contraseñas usando SHA256 (integrado en .NET)
    private string ComputeSha256Hash(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}