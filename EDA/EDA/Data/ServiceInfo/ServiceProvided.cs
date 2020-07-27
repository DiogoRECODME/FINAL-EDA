using Recodme.Rd.EDA.Data.Base;
using Recodme.Rd.EDA.Data.UserInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.Rd.EDA.Data.ServiceInfo
{
    public class ServiceProvided : Entity
    {
        private Guid _serviceId;

        [ForeignKey("Service")]
        [Display(Name = "Service")]
        public Guid ServiceId
        {
            get => _serviceId;
            set
            {
                _serviceId = value;
                RegisterChange();
            }
        }
        public virtual Service Service { get; set; }



        public ServiceProvided(Guid serviceId)
        {
            _serviceId = serviceId;
        }

        public ServiceProvided(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, Guid serviceId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _serviceId = serviceId;
        }
    }
}