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
    public class SavingTypeCommandHandler : ICommandHandlerAsync<CreateSavingTypeCommand>,
                                            ICommandHandlerAsync<ModifySavingTypeCommand>,
                                            ICommandHandlerAsync<DeleteSavingTypeCommand>
    {
        private readonly ISavingTypeRepository _types;

        public SavingTypeCommandHandler(ISavingTypeRepository types)
        {
            _types = types;
        }

        public async Task HandleAsync(CreateSavingTypeCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var type = SavingType.Create(message.SavingTypeId, message.UserId, message.Name,
                message.Symbol);
            _types.Add(type);
            await _types.SaveChangesAsync(token);
        }

        public async Task HandleAsync(ModifySavingTypeCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var type = await this.GetSavingTypeAsync(message.SavingTypeId);
            type.Modify(message.Name, message.Symbol, message.Version);
            await _types.SaveChangesAsync(token);
        }

        public async Task HandleAsync(DeleteSavingTypeCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var type = await this.GetSavingTypeAsync(message.SavingTypeId);
            _types.Delete(type);
            await _types.SaveChangesAsync();
        }

        protected async Task<SavingType> GetSavingTypeAsync(Guid savingTypeId,
            CancellationToken token = default(CancellationToken))
        {
            var type = await _types.GetByIdAsync(savingTypeId, token);
            if (type == null)
            {
                throw new HouseholdException($"Saving Type {savingTypeId} doesn't exists");
            }
            return type;
        }
    }
}
