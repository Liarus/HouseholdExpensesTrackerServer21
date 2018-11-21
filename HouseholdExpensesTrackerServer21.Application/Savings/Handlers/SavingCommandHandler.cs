using HouseholdExpensesTrackerServer21.Application.Savings.Commands;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Common.Type;
using HouseholdExpensesTrackerServer21.Domain.Savings.Models;
using HouseholdExpensesTrackerServer21.Domain.Savings.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Application.Savings.Handlers
{
    public class SavingCommandHandler : ICommandHandlerAsync<CreateSavingCommand>,
                                        ICommandHandlerAsync<ModifySavingCommand>
    {
        private readonly ISavingRepository _savings;

        public SavingCommandHandler(ISavingRepository savings)
        {
            _savings = savings;
        }

        public async Task HandleAsync(CreateSavingCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var saving = Saving.Create(message.SavingId, message.HouseholdId, message.SavingTypeId, message.Name,
                message.Description, message.Amount, message.Date);
            _savings.Add(saving);
            await _savings.SaveChangesAsync(token);
        }

        public async Task HandleAsync(ModifySavingCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var saving = await _savings.GetByIdAsync(message.SavingId);
            if (saving == null)
            {
                throw new HouseholdException($"Saving {message.SavingId} doesn't exists");
            }
            saving.Modify(message.SavingTypeId, message.Name, message.Description, message.Amount,
                message.Date, message.Version);
            await _savings.SaveChangesAsync(token);
        }
    }
}
