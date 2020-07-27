using Recodme.Rd.EDA.Data.Base;
using Recodme.Rd.EDA.Data.ServiceInfo;
using Recodme.Rd.EDA.Data.UserInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.Rd.EDA.Data.JobInfo
{
    public class Job : Entity
    {
        private string _address;
        private DateTime _startDate;
        private DateTime _endDate;
        private double _budget;
        private string _status;
        private double _rating;
        private bool _rated;
        private Guid _serviceId;
        private Guid _clientId;


        [Display(Name = "Address")]
        [Column("Address")]
        [Required(ErrorMessage = "Required Attribute")]
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                RegisterChange();
            }
        }

        [Display(Name = "Start Date")]
        [Column("StartDate")]
        [Required(ErrorMessage = "Required Attribute")]
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                RegisterChange();
            }
        }

        [Display(Name = "End Date")]
        [Column("EndDate")]
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                RegisterChange();
            }
        }

        [Display(Name = "Budget")]
        [Column("Budget")]
        public double Budget
        {
            get => _budget;
            set
            {
                _budget = value;
                RegisterChange();
            }
        }

        [Display(Name = "Status")]
        [Column("Status")]
        [Required(ErrorMessage = "Required Attribute")]
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                RegisterChange();
            }
        }

        [Display(Name = "Rating")]
        [Column("Rating")]
        [Range(1, 5)]
        public double Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                RegisterChange();
            }
        }

        [Display(Name = "Is Rated")]
        [Column("Rated")]
        public bool Rated
        {
            get => _rated;
            set
            {
                _rated = value;
                RegisterChange();
            }
        }

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

        [ForeignKey("Client")]
        [Display(Name = "Client")]
        public Guid ClientId
        {
            get => _clientId;
            set
            {
                _clientId = value;
                RegisterChange();
            }
        }
        public virtual Client Client { get; set; }

        public virtual ICollection<Proposal> Proposals { get; set; }



        public Job(string address, DateTime startDate, DateTime endDate, double budget, string status, double rating, bool rated, Guid serviceId, Guid clientId)
        {
            _address = address;
            _startDate = startDate;
            _endDate = endDate;
            _budget = budget;
            _status = status;
            _rating = rating;
            _rated = rated;
            _serviceId = serviceId;
            _clientId = clientId;
        }

        public Job(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string address, DateTime startDate, DateTime endDate, double budget, string status, double rating, bool rated, Guid serviceId, Guid clientId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _address = address;
            _startDate = startDate;
            _endDate = endDate;
            _budget = budget;
            _status = status;
            _rating = rating;
            _rated = rated;
            _serviceId = serviceId;
            _clientId = clientId;
        }
    }
}