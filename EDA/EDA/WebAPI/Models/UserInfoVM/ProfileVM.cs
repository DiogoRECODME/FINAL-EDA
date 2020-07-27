using Recodme.Rd.EDA.Data.UserInfo;
using Recodme.Rd.EDA.WebAPI.Models.BaseVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.WebAPI.Models.UserInfoVM
{
    public class ProfileVM : BasicVM
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name required")]
        public string FullName { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country required")]
        public string Country { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date required")]
        public DateTime BeginDate { get; set; }
        

        public Profile ToProfile()
        {
            return new Profile(FullName, Country, BeginDate);
        }

        public Profile ToProfile(Profile profile)
        {
            profile.FullName = FullName;
            profile.Country = Country;
            profile.BeginDate = BeginDate;

            return profile;
        }

        public static ProfileVM Parse(Profile vm)
        {
            return new ProfileVM()
            {
                Id = vm.Id,
                FullName = vm.FullName,
                Country = vm.Country,
                BeginDate = vm.BeginDate
            };
        }

        public bool CompareToModel(Profile model)
        {
            return FullName == model.FullName &&
                    Country == model.Country &&
                    BeginDate == model.BeginDate;
        }
    }
}