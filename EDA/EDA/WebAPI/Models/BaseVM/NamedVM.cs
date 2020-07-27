using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.WebAPI.Models.BaseVM
{
    public class NamedVM : BasicVM
    {
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }
    }
}
