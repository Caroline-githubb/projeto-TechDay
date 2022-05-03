using CarrefourApi.Model;

namespace CarrefourApi.Repository
{
    public interface IProgramaRepository
    {
        List<Programa> ListarProgramas();        
        void CadastrarPrograma(Programa programa);
        void AlterarPrograma(Programa programa);        
        void DeletarPrograma(int codigo);
    }
}