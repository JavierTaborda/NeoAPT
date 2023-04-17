﻿using Microsoft.AspNetCore.Components;
using NeoAPT.NeoModels;

namespace NeoAPT.Data
{
    public class ResumenService : ResumenInterface
    {

        private readonly DbNeoContext _neocontext;
        private readonly NavigationManager _navigationManager;


        public ResumenService(NavigationManager navigationManager, DbNeoContext _NeoContext)
        {
            _navigationManager = navigationManager;
            _neocontext = _NeoContext;
        }
        public List<Resuman> resumen { get ; set ; }
        public List<TipIncen> tipoincentivo { get; set; }
        public List<TipSuple> tiposuple { get; set; }
        public List<Personal> personal { get; set; }
        public List<Resuman> resumenlinea { get; set ; }
        public List<Resuman> resumencentro { get; set; }
        public List<Resuman> resumensuplencia { get; set; }

        public Task<List<Personal>> GetPersonal(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Resuman>> GetResumen(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Resuman>> GetResumenSuplencias(int idCentro, DateTime f1, DateTime f2)
        {
          
            return resumensuplencia; 
        }

        public Task<List<Resuman>> GetResumenxCentro(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Resuman>> GetResumenxLinea(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TipIncen>> GetTipoInce(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TipSuple>> GetTipoSuple(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertarPuestoTrabajo(PuesTrab puesto)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePuestoTrabajo(PuesTrab puesto, int id)
        {
            throw new NotImplementedException();
        }
    }
}
