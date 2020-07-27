using Recodme.Rd.EDA.Data.ExtraInfo;
using Recodme.Rd.EDA.Data.UserInfo;
using Recodme.Rd.EDA.WebAPI.Models.BaseVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.WebAPI.Models.ExtraInfoVM
{
    public class CommentVM : BasicVM
    {
        [Required(ErrorMessage = "Message required")]
        public string Message { get; set; }

        [Display(Name = "Client")]
        [Required(ErrorMessage = "Client required")]
        public Guid ClientId { get; set; }

        public Comment ToComment()
        {
            return new Comment(Message, ClientId);
        }

        public Comment ToComment(Comment comment)
        {
            comment.Message = Message;
            comment.ClientId = ClientId;

            return comment;
        }

        public static CommentVM Parse(Comment vm)
        {
            return new CommentVM()
            {
                Id = vm.Id,
                Message = vm.Message,
                ClientId = vm.ClientId
            };
        }

        public bool CompareToModel(Comment model)
        {
            return Message == model.Message &&
                    ClientId == model.ClientId;
        }
    }
}