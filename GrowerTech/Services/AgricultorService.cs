using GrowerTech_MVC.DTO;
using GrowerTech_MVC.Models;
using GrowerTech_MVC.Repository;
using System;
using System.Collections.Generic;

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
                Nome = agricultorDTO.Nome ?? string.Empty,
                Escala = agricultorDTO.Escala ?? string.Empty,
                Endereco = agricultorDTO.Endereco ?? string.Empty
            };

            _repository.AddAgricultor(agricultor);
        }
    }
}
