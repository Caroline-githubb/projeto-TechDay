using CarrefourApi.Model;

namespace CarrefourApi.Repository
{
    public interface IUsuarioRepository
    {
        bool VerificarUsuario(string email, string senha);
        void CadastrarUsuario(Usuario usuario);
         
    }
}