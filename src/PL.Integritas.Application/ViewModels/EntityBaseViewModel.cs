using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Integritas.Application.ViewModels
{
    public class EntityBaseViewModel
    {
        #region Properties

        [Key]
        public Int64 Id { get; set; }

        public Boolean Active { get; set; }
        
        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

        #endregion
    }
}
