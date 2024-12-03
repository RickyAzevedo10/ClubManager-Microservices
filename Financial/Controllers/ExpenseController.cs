using Financial.App.Services.Infrastructures;
using Financial.Domain.DTOs;
using Financial.Domain.Interfaces;
using Financial.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly INotificationContext _notificationContext;
        private readonly IModelErrorsContext _modelErrorsContext;
        private readonly IExpenseAppService _expenseAppService;

        public ExpenseController(INotificationContext notificationContext, IModelErrorsContext modelErrorsContext, IExpenseAppService expenseAppService)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            _expenseAppService = expenseAppService;
        }

        /// <summary>
        /// create expense
        /// </summary>
        /// <param name="expenseBody"></param>
        /// <returns></returns>
        [HttpPost("Expense")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro")]
        public async Task<IActionResult> PostExpense([FromBody] ExpenseDTO expenseBody)
        {
            ExpenseResponseDTO? response = await _expenseAppService.CreateExpense(expenseBody);
            return DomainResult<ExpenseResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete Expense
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Expense")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro")]
        public async Task<IActionResult> DeleteExpense([FromQuery] long id)
        {
            ExpenseResponseDTO? response = await _expenseAppService.DeleteExpense(id);
            return DomainResult<ExpenseResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update expense
        /// </summary>
        /// <param name="expenseToUpdate"></param>
        /// <returns></returns>
        [HttpPut("Expense")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro")]
        public async Task<IActionResult> PutExpense([FromBody] UpdateExpenseDTO expenseToUpdate)
        {
            ExpenseResponseDTO? response = await _expenseAppService.UpdateExpense(expenseToUpdate);
            return DomainResult<ExpenseResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get expense
        /// </summary>
        /// <param name="expenseId"></param>
        /// <returns></returns>
        [HttpGet("Expense")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro,Secretário")]
        public async Task<IActionResult> GetExpense([FromQuery] long expenseId)
        {
            ExpenseResponseDTO? response = await _expenseAppService.GetExpense(expenseId);
            return DomainResult<ExpenseResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get all expense
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllExpense")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro,Secretário")]
        public async Task<IActionResult> GetAllExpense()
        {
            List<ExpenseResponseDTO>? response = await _expenseAppService.GetAllExpense();
            return DomainResult<List<ExpenseResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
    }
}
