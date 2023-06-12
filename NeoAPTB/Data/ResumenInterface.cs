﻿using NeoAPTB.NeoModels;
namespace NeoAPTB.Data
{
    public interface ResumenInterface
    {
        List<Resuman> resumenlinea { get; set; }
        List<Resuman> resumenlineafecha { get; set; }
        List<Resuman> resumencentro { get; set; }
        List<Resuman> resumensuplencia { get; set; }
        List<TipIncen> tipoincentivo { get; set; }
        List<TipSuple> tiposuple { get; set; }
        List<Personal> personal { get; set; }

        Task<List<Resuman>> GetResumenxLinea(int id);
        Task<List<Resuman>> GetResumenxCentro(int id);
        Task<List<Resuman>> GetResumenxlineafecha(int idlinea, DateTime f1, DateTime f2);
        Task<List<Resuman>> GetResumenSuplencias(int idCentro, DateTime f1, DateTime f2);
        Task<List<TipIncen>> GetTipoInce();
        Task<List<TipSuple>> GetTipoSuple();
        Task<List<TipSuple>> GetTipoSupleAll();
        Task<List<Personal>> GetPersonal(int id);
        Task UpdatePuestoTrabajo(PuesTrab puesto, int id);
        Task InsertResumen(List<Resuman> resumen);
        Task InsertTipoInce(TipIncen tipoince);
        Task InsertTipoSuple(TipSuple tiposuple);
        Task UpdateTipoInce(TipIncen tipoince);
        Task UpdateTipoSuple(TipSuple tiposuple);

    }
}
