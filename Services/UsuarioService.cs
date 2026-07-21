using OndeFoi.Repositories;
using OndeFoi.Models;
using OndeFoi.DTOs;


namespace OndeFoi.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;

        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<Usuario> Listar()
        {
            var resultado = _repository.Listar();
            return resultado;
        }

        public Resultado<Usuario> Cadastrar(CadastrarUsuarioDto dto)
        {
            Usuario usuario = new Usuario(dto.Nome, dto.Email, dto.Senha);
            var erros = validarUsuario(usuario);

            if (erros.Any()) return Resultado<Usuario>.Erro(erros);

            _repository.Adicionar(usuario);

            return Resultado<Usuario>.Ok(usuario);

        }

        public Resultado<Usuario> Deletar(int id)
        {
            var usuario = _repository.BuscarPorId(id);
            if (usuario == null) return Resultado<Usuario>.Erro("Usuário não encontrado.");

            return Resultado<Usuario>.Ok();

        }

        public Resultado<Usuario> Editar(int id, EditarUsuarioDto dto)
        {
            var usuario = _repository.BuscarPorId(id);

            if (usuario == null) return Resultado<Usuario>.Erro("Usuário não encontrado!");

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.SenhaHash = dto.Senha;

            var erros = validarUsuario(usuario);
            if (erros.Any()) return Resultado<Usuario>.Erro(erros);

            _repository.Salvar();

            return Resultado<Usuario>.Ok();

        }

        private List<string> validarUsuario(Usuario usuario)
        {
            var erros = new List<string>();
            if (string.IsNullOrWhiteSpace(usuario.Nome)) erros.Add("Nome é obrigatório!");
            if (string.IsNullOrWhiteSpace(usuario.Email)) erros.Add("Email é obrigatório!");
            if (!usuario.Email.Contains('@')) erros.Add("O e-mail deve conter '@'.");
            if (_repository.ExisteEmail(usuario.Nome, usuario.Id)) erros.Add("Esse email já está sendo usado.");
            if (string.IsNullOrWhiteSpace(usuario.SenhaHash)) erros.Add("Senha é obrigatória!");

            return erros;

        }


    }
}