using Recodme.Rd.EDA.Data.ServiceInfo;
using Recodme.Rd.EDA.WebAPI.Models.BaseVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.WebAPI.Models.ServiceInfoVM
{
    public class ServiceProvidedVM : BasicVM
    {
        [Display(Name = "Service")]
        [Required(ErrorMessage = "Service required")]
        public Guid ServiceId { get; set; }

        public ServiceProvided ToServiceProvided()
        {
            return new ServiceProvided(ServiceId);
        }

        public ServiceProvided ToServiceProvided(ServiceProvided serviceProvided)
        {
            serviceProvided.ServiceId = ServiceId;

            return serviceProvided;
        }

        public static ServiceProvidedVM Parse(ServiceProvided vm)
        {
            return new ServiceProvidedVM()
            {
                Id = vm.Id,
                ServiceId = vm.ServiceId,
            };
        }

        public bool CompareToModel(ServiceProvided model)
        {
            return ServiceId == model.ServiceId;                
        }
    }
}