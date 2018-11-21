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
    public class PermissionController : BaseController
    {
        public PermissionController(ICommandDispatcherAsync commandDispatcher,
            IQueryDispatcherAsync queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.GetQueryAsync<IEnumerable<PermissionDto>>(new PermissionListQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreatePermissionDto command)
        {
            await this.SendCommandAsync<CreatePermissionCommand>(new CreatePermissionCommand(command.Id,
                command.Name, command.Code));
            return Ok();
        }

        public async Task<IActionResult> Put([FromBody]ModifyPermissionDto command)
        {
            await this.SendCommandAsync<ModifyPermissionCommand>(new ModifyPermissionCommand(command.Id,
                command.Name, command.Code, command.Version));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.SendCommandAsync<DeletePermissionCommand>(new DeletePermissionCommand(id));
            return Ok();
        }
    }
}