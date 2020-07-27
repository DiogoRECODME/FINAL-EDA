using Recodme.Rd.EDA.Data.Base;
using Recodme.Rd.EDA.Data.ExtraInfo;
using Recodme.Rd.EDA.Data.JobInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Recodme.Rd.EDA.Data.UserInfo
{
    public class Client : Entity
    {
        private Guid _profileId { get; set; }

        [ForeignKey("Profile")]
        [Display(Name = "Profile")]
        public Guid ProfileId
        {
            get => _profileId;
            set
            {
                _profileId = value;
                RegisterChange();
            }
        }
        public virtual Profile Profile { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Job> Jobs { get; set; }



        public Client(Guid profileId) : base()
        {
            _profileId = profileId;
        }

        public Client(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, Guid profileId) : base(id, createdAt, updatedAt, isDeleted)
        {
            _profileId = profileId;
        }
    }
}