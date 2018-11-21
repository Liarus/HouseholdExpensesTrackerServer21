using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Repositories
{
    public class Filter<TModel>
    {
        public Filter(Expression<Func<TModel, bool>> expression)
        {
            Expression = expression;
        }

        public Expression<Func<TModel, bool>> Expression { get; private set; }
    }
}
