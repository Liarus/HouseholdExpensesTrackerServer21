using HouseholdExpensesTrackerServer21.Common.Object;
using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Core
{ 
    public abstract class AuditableEntity : BaseEntity, IAuditableEntity
    {
        public int Version { get; protected set; }

        protected DateTime _createdDate;

        protected string _createdBy;

        protected DateTime? _updatedDate;

        protected string _updatedBy;

        public void CreateAuditable(DateTime createdDate, string createdBy)
        {
            _createdDate = createdDate;
            _createdBy = createdBy;
            _createdDate = createdDate;
            _createdBy = createdBy;
            this.Version = 1;
        }

        public void UpdateAuditable(DateTime updatedDate, string updatedBy)
        {
            _updatedDate = updatedDate;
            _updatedBy = updatedBy;
            this.Version++;
        }
    }
}
