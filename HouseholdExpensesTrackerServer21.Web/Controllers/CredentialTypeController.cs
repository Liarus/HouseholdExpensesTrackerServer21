using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Application.Identities.Commands;
using HouseholdExpensesTrackerServer21.Application.Identities.Queries;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.DataTransferObject.Requests;
using HouseholdExpensesTrackerServer21.DataTransferObject.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdExpensesTrackerServer21.Web.Controllers
{
    public class CredentialTypeController : BaseController
    {
        public CredentialTypeController(ICommandDispatcherAsync commandDispatcher,
            IQueryDispatcherAsync queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.GetQueryAsync<IEnumerable<CredentialTypeDto>>(new CredentialTypeListQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateCredentialTypeDto command)
        {
            await this.SendCommandAsync<CreateCredentialTypeCommand>(new CreateCredentialTypeCommand(command.Id,
               command.Name, command.Code));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ModifyCredentialTypeDto command)
        {
            await this.SendCommandAsync<ModifyCredentialTypeCommand>(new ModifyCredentialTypeCommand(command.Id,
            command.Name, command.Code, command.Version));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.SendCommandAsync<DeleteCredentialTypeCommand>(new DeleteCredentialTypeCommand(id));
            return Ok();
        }
    }
}
