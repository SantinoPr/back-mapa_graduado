using back_mapa_graduado.Controllers;

namespace back_mapa_graduado.Models
{
    public interface IGraduadoDataAcces
    {
        Task<IEnumerable<Graduado>> GetAllGraduados();
        Task<Graduado> GetGraduado(int id);
        void AddGraduado(Graduado graduado);
        void AuthenticationGraduado(string mail, string password);
        Task<bool> UpdateGraduado(int id);
        Task<bool> DeleteGraduado(int id);

    }
}
