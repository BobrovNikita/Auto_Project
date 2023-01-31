using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Common
{
    public class CheckDateAttribute : ValidationAttribute
    {
        public CheckDateAttribute()
        {

        }
        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                var dt = (DateTime)value;

                if (
                    (dt.Year >= 2000 && dt.Year <= 2050)
                   )
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
