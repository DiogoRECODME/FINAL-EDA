using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.Rd.EDA.Data.Base
{
    public abstract class NamedEntity : Entity
    {
        private string _name;

        [Display(Name = "Name")]
        [Column("Name")]
        [Required(ErrorMessage = "Required Attribute")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RegisterChange();
            }
        }

        protected NamedEntity(string name)
        {
            _name = name;
        }

        protected NamedEntity(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted, string name) : base(id, createdAt, updatedAt, isDeleted)
        {
            _name = name;
        }
    }
}