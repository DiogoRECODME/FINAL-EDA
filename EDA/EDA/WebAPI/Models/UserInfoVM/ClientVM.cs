using Recodme.Rd.EDA.Data.UserInfo;
using Recodme.Rd.EDA.WebAPI.Models.BaseVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.WebAPI.Models.UserInfoVM
{
    public class ClientVM : BasicVM
    {
        [Display(Name = "Profile")]
        [Required(ErrorMessage = "Profile required")]
        public Guid ProfileId { get; set; }


        public Client ToClient()
        {
            return new Client(ProfileId);
        }

        public Client ToClient(Client client)
        {
            client.ProfileId = ProfileId;

            return client;
        }

        public static ClientVM Parse(Client vm)
        {
            return new ClientVM()
            {
                Id = vm.Id,
                ProfileId = vm.ProfileId
            };
        }

        public bool CompareToModel(Client model)
        {
            return ProfileId == model.ProfileId;
        }
    }
}