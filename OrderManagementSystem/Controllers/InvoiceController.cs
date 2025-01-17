﻿using AutoMapper;
using DataAccess.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using System.Collections;

namespace OrderManagementSystem.Controllers
{

    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[Controller]")]
    public class InvoiceController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetInvoicDetails([FromRoute] int invoiceId)
        {
            var invoice = await _unitOfWork.Invoices.FindAsync(i => i.InvoiceId == invoiceId);
            if (invoice is null)
            {
                return NotFound("No Invoice Found");
            }
            var convertInvoice =  _mapper.Map<GetInvoiceData>(invoice);
            return Ok(convertInvoice);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetInvoiceData>>> GetAllInvoice()
        {
            var invoice = await _unitOfWork.Invoices.GetAllAsync();
            if (invoice == null)
            {
                return NotFound("No Invoice Found");
            }
            var convertInvoice = _mapper.Map<IEnumerable<GetInvoiceData>>(invoice);
            return Ok(convertInvoice);
        }
    }
}
