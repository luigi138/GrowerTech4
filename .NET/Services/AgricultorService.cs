
using GrowerTech_MVC.DTO;
using GrowerTech_MVC.Models;
using GrowerTech_MVC.Repository;

namespace GrowerTech_MVC.Services
{
    public class AgricultorService
    {
        private readonly AgricultorRepository _repository;

        public AgricultorService(AgricultorRepository repository)
        {
            _repository = repository;
        }

        public List<Agricultor> GetAllAgricultores()
        {
            return _repository.GetAgricultores();
        }

        public void CreateAgricultor(AgricultorDTO agricultorDTO)
        {
            var agricultor = new Agricultor
            {
                Nome = agricultorDTO.Nome,
                Escala = agricultorDTO.Escala,
                Endereco = agricultorDTO.Endereco
            };
            _repository.AddAgricultor(agricultor);
        }
    }
}
