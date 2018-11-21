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
    public class RoleController : BaseController
    {
        public RoleController(ICommandDispatcherAsync commandDispatcher,
            IQueryDispatcherAsync queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.GetQueryAsync<IEnumerable<RoleDto>>(new RoleListQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateRoleDto command)
        {
            await this.SendCommandAsync<CreateRoleCommand>(new CreateRoleCommand( command.Id, 
               command.Name, command.Code, command.PermissionIds));
            return Ok();
        }

        [HttpPost]
        [Route("~/api/role/{roleId:guid}/assignPermission/{permissionId:guid}")]
        public async Task<IActionResult> AssignPermission(Guid roleId, Guid permissionId)
        {
            await this.SendCommandAsync<AssignPermissionCommand>(
                new AssignPermissionCommand(permissionId, roleId));
            return Ok();
        }

        [HttpPost]
        [Route("~/api/role/{roleId:guid}/unassignPermission/{permissionId:guid}")]
        public async Task<IActionResult> UnAssignPermission(Guid roleId, Guid permissionId)
        {
            await this.SendCommandAsync<UnassignPermissionCommand>(
                new UnassignPermissionCommand(permissionId, roleId));
            return Ok();
        }

        public async Task<IActionResult> Put([FromBody]ModifyRoleDto command)
        {
            await this.SendCommandAsync<ModifyRoleCommand>(new ModifyRoleCommand(command.Id,
                command.Name, command.Code, command.PermissionIds, command.Version));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.SendCommandAsync<DeleteRoleCommand>(new DeleteRoleCommand(id));
            return Ok();
        }
    }
}