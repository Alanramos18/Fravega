using System;
using System.Collections.Generic;
using Fravega.Dto;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity
{
    public class Promocion
    {
        [BsonId]
        public Guid Id { get; set; }
        public IEnumerable<string> MediosDePago { get; private set; }
        public IEnumerable<string> Bancos { get; private set; }
        public IEnumerable<string> CategoriasProductos { get; private set; }
        public int? MaximaCantidadDeCuotas { get; private set; }
        public decimal? ValorInteresCuotas { get; private set; }
        public decimal? PorcentajeDeDescuento { get; private set; }

        [BsonDateTimeOptions]
        public DateTime? FechaInicio { get; private set; }
        
        [BsonDateTimeOptions]
        public DateTime? FechaFin { get; private set; }

        private bool _activo;
        public bool Activo
        {
            get
            {
                return _activo;
            }
            private set
            {
                if (FechaCreacion == null || FechaFin == null)
                    _activo = false;

                if ((FechaFin - DateTime.Now).Value.TotalDays > 0)
                    _activo = true;
                else
                    _activo = false;
            } 
        }

        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaModificacion{ get; private set; }

        public void UpdateDate(DateTime? start, DateTime? end)
        {
            if (start != null)
                this.FechaInicio = start.Value;

            if (end != null)
                this.FechaFin = end.Value;
        }

        public Promocion Convert(PromotionDto dto, Guid? id)
        {
            if (id.HasValue)
                Id = id.Value;

            MediosDePago = dto.PaymentMethods;
            Bancos = dto.Banks;
            CategoriasProductos = dto.Categories;
            MaximaCantidadDeCuotas = dto.PaymentsNumber;
            ValorInteresCuotas = dto.PaymentInterestPorcentage;
            PorcentajeDeDescuento = dto.DiscountPorcentage;
            FechaInicio = dto.StartDate;
            FechaFin = dto.EndDate;
            Activo = (dto.EndDate - DateTime.Now).Value.TotalDays > 0;
            FechaCreacion = DateTime.Now;

            return this;
        }
    }
}
