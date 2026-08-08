using OndeFoi.Repositories;
using OndeFoi.Models;
using OndeFoi.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;


namespace OndeFoi.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;
        private readonly IConfiguration _configuration;

        public UsuarioService(UsuarioRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public IEnumerable<UsuarioResponseDto> Listar(int usuarioId)
        {
            return _repository.Listar(usuarioId).Select(u => new UsuarioResponseDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            });
        }

        public Resultado<UsuarioResponseDto> Cadastrar(CadastrarUsuarioDto dto)
        {
            Usuario usuario = new Usuario(dto.Nome, dto.Email, dto.Senha);

            var erros = ValidarUsuario(usuario);

            if (erros.Any()) return Resultado<UsuarioResponseDto>.Erro(erros);

            var hasher = new PasswordHasher<Usuario>();
            usuario.SenhaHash = hasher.HashPassword(usuario, dto.Senha);

            _repository.Adicionar(usuario);

            UsuarioResponseDto resposta = new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
            };

            return Resultado<UsuarioResponseDto>.Ok(resposta);

        }

        public Resultado<Usuario> Deletar(int id)
        {
            var usuario = _repository.BuscarPorId(id);
            if (usuario == null) return Resultado<Usuario>.Erro("usuario", "Usuário não encontrado.");

            _repository.Remover(usuario);

            return Resultado<Usuario>.Ok();

        }

        public Resultado<UsuarioResponseDto> Editar(int id, EditarUsuarioDto dto)
        {
            var usuario = _repository.BuscarPorId(id);

            if (usuario == null) return Resultado<UsuarioResponseDto>.Erro("usuario", "Usuário não encontrado!");

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.SenhaHash = dto.Senha;

            var erros = ValidarUsuario(usuario);
            if (erros.Any()) return Resultado<UsuarioResponseDto>.Erro(erros);

            _repository.Salvar();

            UsuarioResponseDto resposta = new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            return Resultado<UsuarioResponseDto>.Ok(resposta);

        }

        public Resultado<bool> EditarRenda(int idUsuario, EditarRendaDto dto)
        {
            var usuario = _repository.BuscarPorId(idUsuario);
            if (usuario == null) return Resultado<bool>.Erro("usuario", "Usuário não encontrado!");

            usuario.Renda = dto.Renda;

            _repository.Salvar();

            return Resultado<bool>.Ok(true);

        }

        public Resultado<LoginResponseDto> Login(LoginDto dto)
        {
            var usuario = _repository.BuscarPorEmail(dto.Email); ;

            if (usuario == null) return Resultado<LoginResponseDto>.Erro("geral", "E-mail ou senha inválidos.");


            var hasher = new PasswordHasher<Usuario>();

            var resultado = hasher.VerifyHashedPassword(usuario, usuario.SenhaHash, dto.Senha);

            if (resultado != PasswordVerificationResult.Success) return Resultado<LoginResponseDto>.Erro("geral", "E-mail ou senha inválidos.");

            var token = GerarToken(usuario);

            var resposta = new LoginResponseDto
            {
                Token = token,
                Usuario = new UsuarioResponseDto
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                }
            };

            return Resultado<LoginResponseDto>.Ok(resposta);
        }

        private Dictionary<string, string> ValidarUsuario(Usuario usuario)
        {
            var erros = new Dictionary<string, string>();

            if (_repository.ExisteEmail(usuario.Email, usuario.Id)) erros.Add("email", "Esse email já está sendo usado.");

            return erros;

        }

        private string GerarToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name,usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };//define as iformações do usuário

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            ); // define a chave secreta

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //usa a chave secreta para assinar o token com o algoritmo de codificação

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // converte o objeto JwtSecurityToken em uma string JWT.


        }


    }
}