using Recodme.Rd.EDA.Data.Base;
using Recodme.Rd.EDA.Data.JobInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.Rd.EDA.Data.ServiceInfo
{
    public class Service : NamedEntity
    {
        private string _description;
        private bool _isActive;

        [Display(Name = "Description")]
        [Column("Description")]
        [Required(ErrorMessage = "Required Attribute")]
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RegisterChange();
            }
        }

        [Display(Name = "Is Active")]
        [Column("IsActive")]
        [Required(ErrorMessage = "Required Attribute")]
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                RegisterChange();
            }
        }

        public ICollection<Job> Jobs { get; set; }

        public ICollection<ServiceProvided> ServicesProvided { get; set; }




        public Service(string name, string description, bool isActive) : base(name)
        {
            _description = description;
            _isActive = isActive;
        }

        public Service(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name, string description, bool isActive) : base(id, createdAt, updatedAt, isDeleted, name)
        {
            _description = description;
            _isActive = isActive;
        }
    }
}