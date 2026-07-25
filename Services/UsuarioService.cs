using OndeFoi.Repositories;
using OndeFoi.Models;
using OndeFoi.DTOs;
using Microsoft.AspNetCore.Identity;


namespace OndeFoi.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;

        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UsuarioResponseDto> Listar()
        {
            return _repository.Listar().Select(u => new UsuarioResponseDto
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
                Email = usuario.Email
            };

            return Resultado<UsuarioResponseDto>.Ok(resposta);

        }

        public Resultado<Usuario> Deletar(int id)
        {
            var usuario = _repository.BuscarPorId(id);
            if (usuario == null) return Resultado<Usuario>.Erro("Usuário não encontrado.");

            _repository.Remover(usuario);

            return Resultado<Usuario>.Ok();

        }

        public Resultado<UsuarioResponseDto> Editar(int id, EditarUsuarioDto dto)
        {
            var usuario = _repository.BuscarPorId(id);

            if (usuario == null) return Resultado<UsuarioResponseDto>.Erro("Usuário não encontrado!");

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

        public Resultado<UsuarioResponseDto> Login(LoginDto dto)
        {
            var usuario = _repository.BuscarPorEmail(dto.Email);;

            if (usuario == null) return Resultado<UsuarioResponseDto>.Erro("E-mail ou senha inválidos.");


            var hasher = new PasswordHasher<Usuario>();

            var resultado = hasher.VerifyHashedPassword(usuario, usuario.SenhaHash, dto.Senha);

            if (resultado != PasswordVerificationResult.Success) return Resultado<UsuarioResponseDto>.Erro("E-mail ou senha inválidos.");  

           UsuarioResponseDto resposta = new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
      
            return Resultado<UsuarioResponseDto>.Ok(resposta);
        }

        private List<string> ValidarUsuario(Usuario usuario)
        {
            var erros = new List<string>();

            if (_repository.ExisteEmail(usuario.Email, usuario.Id)) erros.Add("Esse email já está sendo usado.");

            return erros;

        }


    }
}