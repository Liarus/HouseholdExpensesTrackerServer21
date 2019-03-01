using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Application.Households.Commands;
using HouseholdExpensesTrackerServer21.Application.Households.Queries;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.DataTransferObject.Requests;
using HouseholdExpensesTrackerServer21.DataTransferObject.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HouseholdExpensesTrackerServer21.Application.Core.Enums;

namespace HouseholdExpensesTrackerServer21.Web.Controllers
{
    [Authorize]
    public class HouseholdController : BaseController
    {
        public HouseholdController(ICommandDispatcherAsync commandDispatcher,
            IQueryDispatcherAsync queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {

        }

        [Route("~/api/user/{userId:guid}/households")]
        public async Task<IActionResult> GetForUser(Guid userId, string search, int pageNumber, int pageSize, HouseholdSorting sort )
        {
            var result = await this.GetQueryAsync<HouseholdSearchResultDto>(
                new HouseholdSearchQuery(userId, pageNumber, pageSize, search, sort));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateHouseholdDto request)
        {
            await this.SendCommandAsync<CreateHouseholdCommand>(new CreateHouseholdCommand(request.Id, request.UserId,
                request.Name, request.Symbol, request.Description, request.Street, request.City, request.Country,
                request.ZipCode));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ModifyHouseholdDto request)
        {
            await this.SendCommandAsync<ModifyHouseholdCommand>(new ModifyHouseholdCommand(request.Id, request.Name,
                request.Symbol, request.Description, request.Street, request.City, request.Country, request.ZipCode,
                request.Version));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.SendCommandAsync<DeleteHouseholdCommand>(new DeleteHouseholdCommand(id));
            return Ok();
        }
    }
}