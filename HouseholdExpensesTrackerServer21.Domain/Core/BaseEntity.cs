using HouseholdExpensesTrackerServer21.Common.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Core
{
    public abstract class BaseEntity : Searchable, IEntity
    {
        public Guid Id { get; protected set; }
    }
}
