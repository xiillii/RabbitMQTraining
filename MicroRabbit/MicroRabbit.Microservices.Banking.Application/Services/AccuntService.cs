using MicroRabbit.Microservices.Banking.Application.Interfaces;
using MicroRabbit.Microservices.Banking.Domain.Interfaces;
using MicroRabbit.Microservices.Banking.Domain.Models;

namespace MicroRabbit.Microservices.Banking.Application.Services;

public class AccuntService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccuntService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public IEnumerable<Account> GetAccounts()
    {
        return _accountRepository.GetAccounts();
    }
}
