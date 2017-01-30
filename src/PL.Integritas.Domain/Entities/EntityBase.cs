using DomainValidation.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Integritas.Domain.Entities
{
    public abstract class EntityBase
    {
        #region Properties

        public Int64 Id { get; set; }

        public Boolean Active { get; set; }

        public DateTime? DateRegistered { get; set; }

        public DateTime? DateUpdated { get; set; }        

        public ValidationResult ValidationResult { get; set; }

        #endregion
    }
}
