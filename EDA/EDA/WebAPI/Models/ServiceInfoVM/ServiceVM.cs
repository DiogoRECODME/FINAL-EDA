using Recodme.Rd.EDA.Data.ServiceInfo;
using Recodme.Rd.EDA.WebAPI.Models.BaseVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.WebAPI.Models.ServiceInfoVM
{
    public class ServiceVM : NamedVM
    {
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public Service ToService()
        {
            return new Service(Name, Description, IsActive);
        }

        public Service ToService(Service service)
        {
            service.Name = Name;
            service.Description = Description;
            service.IsActive = IsActive;

            return service;
        }

        public static ServiceVM Parse(Service vm)
        {
            return new ServiceVM()
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                IsActive = vm.IsActive
            };
        }

        public bool CompareToModel(Service model)
        {
            return Name == model.Name &&
                    Description == model.Description &&
                    IsActive == model.IsActive;
        }
    }
}