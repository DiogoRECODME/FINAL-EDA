using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.Rd.EDA.Data.Base
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; private set; }

        [Display(Name = "Created At")]
        [Column("CreatedAt")]
        [Required(ErrorMessage = "Required Attribute")]
        public DateTime CreatedAt { get; private set; }

        [Display(Name = "Updated At")]
        [Column("UpdatedAt")]
        [Required(ErrorMessage = "Required Attribute")]
        public DateTime UpdatedAt { get; protected set; }

        private bool _isDeleted;
        [Display(Name = "Is Deleted")]
        [Column("IsDeleted")]
        [Required(ErrorMessage = "Required Attribute")]
        public bool IsDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                RegisterChange();
            }
        }

        protected virtual void RegisterChange()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        protected Entity() : this(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, false) { }

        protected Entity(Guid id, DateTime createdAt, DateTime updatedAt, bool isDeleted)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            _isDeleted = isDeleted;
        }
    }
}
