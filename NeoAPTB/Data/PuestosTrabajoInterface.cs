﻿using Microsoft.AspNetCore.Mvc;
using NeoAPTB.NeoModels;
//using NeoAPTB.DTOs;

namespace NeoAPTB.Data
{
    public interface PuestosTrabajoInterface
    {
        List<PuesTrab> puesTrab { get; set; }

     
        Task<List<PuesTrab>> GetPuestosTrabajo(int id);
        Task InsertarPuestoTrabajo(PuesTrab puesto);
        Task UpdatePuestoTrabajo(PuesTrab puesto, int id);
    }
}