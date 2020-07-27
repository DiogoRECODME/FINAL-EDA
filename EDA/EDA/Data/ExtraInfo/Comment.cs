using Recodme.Rd.EDA.Data.Base;
using Recodme.Rd.EDA.Data.UserInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.Rd.EDA.Data.ExtraInfo
{
    public class Comment : Entity
    {
        private string _message;
        private Guid _clientId;

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

        public Comment(string message, Guid clientId)
        {
            _message = message;
            _clientId = clientId;
        }

        public Comment(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string message, Guid clientId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _message = message;
            _clientId = clientId;
        }
    }
}