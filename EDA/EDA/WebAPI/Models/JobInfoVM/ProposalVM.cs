using Recodme.Rd.EDA.Data.JobInfo;
using Recodme.Rd.EDA.WebAPI.Models.BaseVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.WebAPI.Models.JobInfoVM
{
    public class ProposalVM : BasicVM
    {
        [Required(ErrorMessage = "Sender required")]
        public string Sender { get; set; }

        [Required(ErrorMessage = "Message required")]
        public string Message { get; set; }

        [Display(Name = "Job")]
        [Required(ErrorMessage = "Job required")]
        public Guid JobId { get; set; }

        public Proposal ToProposal()
        {
            return new Proposal(Sender, Message, JobId);
        }
        
        public Proposal ToProposal(Proposal proposal)
        {
            proposal.Sender = Sender;
            proposal.Message = Message;
            proposal.JobId = JobId;

            return proposal;
        }    

        public static ProposalVM Parse(Proposal vm)
        {
            return new ProposalVM()
            {
                Id = vm.Id,
                Sender = vm.Sender,
                Message = vm.Message,
                JobId = vm.JobId
            };
        }

        public bool CompareToModel(Proposal model)
        {
            return Sender == model.Sender &&                    
                    Message == model.Message &&
                    JobId == model.JobId;
        }
    }
}