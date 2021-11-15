using System;
using Data.Entity;
using Fravega.Dto;

namespace Fravega.Business.Extensions
{
    public static class PromotionDtoExtensions
    {
        public static Promocion Convert(this PromotionDto dto, Guid? id)
        {
            if (dto == null)
                return null;

            var prom = new Promocion();
            
            return prom.Convert(dto, id);
        }
    }
}
