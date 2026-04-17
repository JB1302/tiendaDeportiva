using System;
using System.ComponentModel.DataAnnotations;

namespace tiendaDeportiva.Helpers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MayorOIgualQueCeroAttribute : ValidationAttribute
    {
        public MayorOIgualQueCeroAttribute()
        {
            ErrorMessage = "El campo debe ser mayor o igual que 0.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            try
            {
                return Convert.ToDecimal(value) >= 0;
            }
            catch
            {
                return false;
            }
        }
    }
}