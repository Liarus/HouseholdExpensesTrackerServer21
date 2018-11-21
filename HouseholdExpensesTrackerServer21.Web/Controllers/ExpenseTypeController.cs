using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Application.Expenses.Commands;
using HouseholdExpensesTrackerServer21.Application.Expenses.Queries;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.DataTransferObject.Requests;
using HouseholdExpensesTrackerServer21.DataTransferObject.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdExpensesTrackerServer21.Web.Controllers
{
    public class ExpenseTypeController : BaseController
    {
        public ExpenseTypeController(ICommandDispatcherAsync commandDispatcher,
            IQueryDispatcherAsync queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {

        }

        [Route("~/api/user/{userId:guid}/expenseTypes")]
        public async Task<IActionResult> GetForUser(Guid userId)
        {
            var result = await this.GetQueryAsync<IEnumerable<ExpenseTypeDto>>(new ExpenseTypeListQuery(userId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateExpenseTypeDto command)
        {
            await this.SendCommandAsync<CreateExpenseTypeCommand>(new CreateExpenseTypeCommand(command.Id,
               command.UserId, command.Name, command.Symbol));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ModifyExpenseTypeDto command)
        {
            await this.SendCommandAsync<ModifyExpenseTypeCommand>(new ModifyExpenseTypeCommand(command.Id,
              command.Name, command.Symbol, command.Version));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.SendCommandAsync<DeleteExpenseTypeCommand>(new DeleteExpenseTypeCommand(id));
            return Ok();
        }
    }
}