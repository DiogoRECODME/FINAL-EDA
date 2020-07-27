using Recodme.Rd.EDA.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.Rd.EDA.Data.UserInfo
{
    public class Profile : Entity
    {
        private string _fullName;
        private string _country;
        private DateTime _beginDate;

        [Display(Name = "Full Name")]
        [Column("FullName")]
        [Required(ErrorMessage = "Required Attribute")]
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                RegisterChange();
            }
        }

        [Display(Name = "Country")]
        [Column("Country")]
        [Required(ErrorMessage = "Required Attribute")]
        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                RegisterChange();
            }
        }

        [Display(Name = "Begin Date")]
        [Column("BeginDate")]
        [Required(ErrorMessage = "Required Attribute")]
        public DateTime BeginDate
        {
            get => _beginDate;
            set
            {
                _beginDate = value;
                RegisterChange();
            }
        }

        public ICollection<Client> Clients { get; set; }



        public Profile(string fullName, string country, DateTime beginDate) : base()
        {
            _fullName = fullName;
            _country = country;
            _beginDate = beginDate;
        }

        public Profile(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string fullName, string country, DateTime beginDate) : base(id, createdAt, updatedAt, isDeleted)
        {
            _fullName = fullName;
            _country = country;
            _beginDate = beginDate;
        }
    }
}