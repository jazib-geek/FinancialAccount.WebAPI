using Azure.Core;
using D2Soft.Application.Abstractions;
using D2Soft.Application.FinancialAccounts.Commands;
using D2Soft.Application.FinancialAccounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using D2Soft.Application.FinancialAccounts.Validators;
using D2Soft.Infrastructure.Repositories;

namespace D2Soft.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialAccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFinancialAccountRepository _financialAccountRepository;
        private readonly ILogger<FinancialAccountController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public FinancialAccountController(IMediator mediator, ILogger<FinancialAccountController> logger, IFinancialAccountRepository financialAccountRepository, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _logger = logger;
            _financialAccountRepository = financialAccountRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFinancialAccounts()
        {
            try
            {
                var query = new GetAllFinancialAccounts();
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving financial accounts: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFinancialAccount([FromBody] CreateFinancialAccount model)
        {
            try
            {
                var validationResult = await new CreateFinancialAccountValidator().ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
                }

                // Map to command
                var command = new CreateFinancialAccount
                {
                    AccountNumber = model.AccountNumber,
                    AccountType = model.AccountType,
                    Balance = model.Balance,
                    OwnerId = model.OwnerId
                 };

                // Send request via Mediator
                var createdAccount = await _mediator.Send(command);

                await _unitOfWork.CommitAsync();

                return Ok(createdAccount);
            }
            catch (Exception ex)
            {
                // Rollback changes on error
                _unitOfWork.Rollback();

                // Log error
                _logger.LogError(ex, "An error occurred while creating a financial account.");

                // Return error response
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetFinancialAccountById(int id)
        {
            try
            {
                var query = new GetFinancialAccountById { Id = id };
                var result = await _mediator.Send(query);

                if (result == null)
                {
                    return NotFound("The requested financial account was not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "An error occurred while processing the request.");

                 return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinancialAccount(int id)
        {
            try
            {
                var checkIfExist = _financialAccountRepository.GetFinancialAccountById(id);

                if (checkIfExist == null)
                {
                    return BadRequest("Requested Account does not exist");
                }

                var command = new DeleteFinancialAccount { FinancialAccountId = id };
                var result = await _mediator.Send(command);

                if (result == "Success")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);  
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the financial account: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFinancialAccount(int id, [FromBody] UpdateFinancialAccount command)
        {
            try
            {
                command.FinancialAccountId = id;
                var result = await _mediator.Send(command);

                if (!result.Item1)
                {
                    return BadRequest(result.Item2);
                }


                return Ok(result.Item2);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the financial account: {ex.Message}");
            }
        }
    }
}
