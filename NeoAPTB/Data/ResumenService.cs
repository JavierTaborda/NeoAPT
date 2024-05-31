﻿using Microsoft.AspNetCore.Components;
using NeoAPTB.NeoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using NeoAPTB.Interfaces;
using NeoAPTB.ModelsDOCIng;

namespace NeoAPTB.Data
{
    public class ResumenService : IResumen
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
        public List<Resuman> resumenlineafecha { get; set ; }
        public List<Resuman> resumencentro { get; set; }
        public List<Resuman> resumensuplencia { get; set; }


        public Task<List<Personal>> GetPersonal(int id)
        {
            throw new NotImplementedException();
        }

        //Personal registrado del día
        public async Task<List<Resuman>> GetResumenFichas(DateTime f1)
        {
            resumen = await _neocontext.Resumen.Where(f => f.Rfecha >= f1.Date)
                .Include(p=>p.IdPersonalNavigation)
                .AsNoTracking()
                .ToListAsync();

            return resumen;
        }

        public async Task<List<Resuman>> GetResumenSuplencias(int idCentro, DateTime f1, DateTime f2)
        {
            resumensuplencia = await _neocontext.Resumen
                .Include(r => r.IdTipIncenNavigation)
                .Include(r => r.IdPersonalNavigation)
                .Include(r => r.IdTipSupleNavigation)
                .Include(r => r.IdMontosNavigation)
                .Include(m=>m.IdMontosNavigation.IdPuesTrabNavigation)
                .Include(m=>m.IdMontosNavigation.IdLineaNavigation)
                .Where(r =>( r.IdMontosNavigation.IdLineaNavigation.IdLinea == idCentro) & (r.IdTipSupleNavigation.IdTipSuple!=1))
                .ToListAsync();


            return resumensuplencia; 
        }

        public async Task<List<Resuman>> GetResumenxCentro(int id)
        {
            resumencentro =await _neocontext.Resumen
                .Include(r => r.IdTipIncenNavigation)
                .Include(r => r.IdPersonalNavigation)
                .Include(r => r.IdTipSupleNavigation)
                .Include(r => r.IdMontosNavigation)
                .Include(m => m.IdMontosNavigation.IdPuesTrabNavigation)
                .Include(m => m.IdMontosNavigation.IdLineaNavigation)
                .Where(r => (r.IdMontosNavigation.IdLineaNavigation.IdLinea == id) & (r.Rfecha >= DateTime.Today & r.Rfecha < DateTime.Today.AddDays(1)))
                .ToListAsync();

            return resumencentro;
        }

        public async Task<List<Resuman>> GetResumenxLinea(int id)
        {

            resumenlinea = await _neocontext.Resumen
              .Include(r => r.IdTipIncenNavigation)
              .Include(r => r.IdPersonalNavigation)
              .Include(r => r.IdTipSupleNavigation)
              .Include(r => r.IdMontosNavigation)
              .Include(m => m.IdMontosNavigation.IdPuesTrabNavigation)
              .Include(m => m.IdMontosNavigation.IdLineaNavigation)
              .Where(r => (r.IdMontosNavigation.IdLineaNavigation.Master.IdLinea == id) & (r.Rfecha >= DateTime.Today & r.Rfecha < DateTime.Today.AddDays(1)))
              .ToListAsync();

            return resumenlinea;
            
        }        
        public async Task<List<Resuman>> GetResumenxlineafecha(int id, DateTime f1, DateTime f2)
        {

            resumenlineafecha = await _neocontext.Resumen
              .Include(r => r.IdTipIncenNavigation)
              .Include(r => r.IdPersonalNavigation)
              .Include(r => r.IdTipSupleNavigation)
              .Include(r => r.IdMontosNavigation)
              .Include(m => m.IdMontosNavigation.IdPuesTrabNavigation)
              .Include(m => m.IdMontosNavigation.IdLineaNavigation)
              .Where(r => (r.IdMontosNavigation.IdLineaNavigation.Master.IdLinea == id) & (r.Rfecha >= f1.Date & r.Rfecha <= f2.Date.AddDays(1)))
              .ToListAsync();

            return resumenlineafecha;
            
        }
        public async  Task<List<Monto>> GetMontoPuesto(int lineaid)
        {
            var result = await _neocontext.Montos
                .Include(p=>p.IdPuesTrabNavigation)
                .Where(m=>m.Mmonto==0 && m.IdLinea==lineaid)
                .ToListAsync();

                return result;
        }

        //Valida que el personal nuevo no este registrada en la bd de personal.
        public async Task<List<Personal>> FiltarListaPersonalNuevo(List<Personal> personals)
        {
             List<Personal> personalnoregistrado = new List<Personal>();
            
            foreach (Personal personal in personals) 
            {
                if(!_neocontext.Personals.Any(P => P.PeFicha == personal.PeFicha))
                {
                    personalnoregistrado.Add(personal);
                }
            }
            return personalnoregistrado;
        }

        public async Task<Personal> GetPersonalSinTempus(string Ficha)
        {
            var result= await _neocontext.Personals.Where(f => f.PeFicha.Contains(Ficha))
                 .Include(p => p.Plantillas).FirstOrDefaultAsync();
            return  result ?? new Personal();
        }
        public async Task<List<TipIncen>> GetTipoInce()
        {
           return tipoincentivo = await _neocontext.TipIncens
                .Where(t => t.Tiesta == true)
                .ToListAsync();
        }

        public async Task<List<TipSuple>> GetTipoSuple()
        {
            return tiposuple = await _neocontext.TipSuples
                           .Where(t => t.Tsestado == true)
                           .ToListAsync();
        }
         public async Task<List<TipSuple>> GetTipoSupleAll()
        {
            return tiposuple = await _neocontext.TipSuples
                           .ToListAsync();
        }


        public async Task InsertResumen(List<Resuman> resumen)
        {
            try
            {
               
                foreach (var rp in resumen)
                {
                    if (rp.IdResumen > 0)
                    {
                        _neocontext.Entry(rp).State = EntityState.Modified;
                    }
                    else
                    {
                        rp.IdMontosNavigation = null;
                        rp.IdPersonalNavigation = null;
                        rp.IdTipSupleNavigation = null;
                        rp.IdTipIncenNavigation = null; 
                        _neocontext.Resumen.Add(rp);
                    }

                  
                }
                await _neocontext.SaveChangesAsync();
               
            }
            catch (Exception ex)
            {
               
            }
        }

        public Task InsertTipoInce(TipIncen tipoince)
        {
            throw new NotImplementedException();
        }

        public async Task InsertTipoSuple(TipSuple tiposuple)
        {
            _neocontext.TipSuples.Add(tiposuple);
            await _neocontext.SaveChangesAsync();
        }

      

        public Task UpdateTipoInce(TipIncen tipoince)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTipoSuple(TipSuple tiposuple)
        {
            _neocontext.Entry(tiposuple).State = EntityState.Modified;
            await _neocontext.SaveChangesAsync();
        }

    }
}
