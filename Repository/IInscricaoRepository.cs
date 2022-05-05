using CarrefourApi.Model;

namespace CarrefourApi.Repository
{
    public interface IInscricaoRepository
    {
        void InserirInscricao(Inscricao inscricao);
        List<Inscricao> ListarEmails();   
       
    }
}