using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Application.Savings.Commands;
using HouseholdExpensesTrackerServer21.Application.Savings.Queries;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.DataTransferObject.Requests;
using HouseholdExpensesTrackerServer21.DataTransferObject.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdExpensesTrackerServer21.Web.Controllers
{
    public class SavingTypeController : BaseController
    {
        public SavingTypeController(ICommandDispatcherAsync commandDispatcher,
            IQueryDispatcherAsync queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {

        }

        [Route("~/api/user/{userId:guid}/savingTypes")]
        public async Task<IActionResult> GetForUser(Guid userId)
        {
            var result = await this.GetQueryAsync<IEnumerable<SavingTypeDto>>(new SavingTypeListQuery(userId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateSavingTypeDto command)
        {
            await this.SendCommandAsync<CreateSavingTypeCommand>(new CreateSavingTypeCommand(command.Id,
                command.UserId, command.Name, command.Symbol));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ModifySavingTypeDto command)
        {
            await this.SendCommandAsync<ModifySavingTypeCommand>(new ModifySavingTypeCommand(command.Id,
               command.Name, command.Symbol, command.Version));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.SendCommandAsync<DeleteSavingTypeCommand>(new DeleteSavingTypeCommand(id));
            return Ok();
        }

    }
}