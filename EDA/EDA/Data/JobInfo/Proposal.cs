using Recodme.Rd.EDA.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.Rd.EDA.Data.JobInfo
{
    public class Proposal : Entity
    {
        private string _sender;
        private string _message;
        private Guid _jobId;

        [Display(Name = "Sender")]
        [Column("Sender")]
        [Required(ErrorMessage = "Required Attribute")]
        public string Sender
        {
            get => _sender;
            set
            {
                _sender = value;
                RegisterChange();
            }
        }

        [Display(Name = "Message")]
        [Column("Message")]
        [Required(ErrorMessage = "Required Attribute")]
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                RegisterChange();
            }
        }

        [ForeignKey("Job")]
        public Guid JobId
        {
            get => _jobId;
            set
            {
                _jobId = value;
                RegisterChange();
            }
        }
        public virtual Job Job { get; set; }



        public Proposal(string sender, string message, Guid jobId)
        {
            _sender = sender;
            _message = message;
            _jobId = jobId;
        }

        public Proposal(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string sender, string message, Guid jobId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _sender = sender;
            _message = message;
            _jobId = jobId;
        }
    }
}