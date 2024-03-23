﻿using AirportFuelManagementWebAPI.Business;
using AirportFuelManagementWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportFuelManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IBusiness business;
        private readonly ILogger<TransactionController> logger;

        public TransactionController(ILogger<TransactionController> logger, IBusiness business)
        {
            this.logger = logger;
            this.business = business;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<FuelTransactionModel>>> GetAllTransactions()
        {

            List<FuelTransactionModel> transactions = await business.GetAllTransactions();
            if (transactions.Any())
            {
                return Ok(transactions);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [Authorize]
        [HttpGet("FuelConsumptionReport")]
        public async Task<ActionResult> FuelConsumptionReport()
        {

            List<AirportTransactionInfo> transactions = await business.FuelConsumptionReport();
            return Ok(transactions);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> InsertTransaction(FuelTransactionModel Transaction)
        {

            bool flag = await business.InsertTransaction(Transaction);
            if (flag)
                return Ok(Transaction);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to insert Transaction.");

        }
       
    }
}
