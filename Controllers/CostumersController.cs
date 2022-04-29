﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaSystem.DataModel.DataContext;
using PharmaSystem.DataModel.Model;
using PharmaSystem.DataModel.Model.ViewModel;
using PharmaSystem.Repository;

namespace PharmaSystem.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CostumersController : Controller
    {
        private readonly ICostumerRepository _costumerRepository;

        public CostumersController(ICostumerRepository costumerRepository)
        {
            _costumerRepository = costumerRepository;
        }

        [HttpGet]
        [Route("costumer/get")]
        public async Task<IActionResult> GetAllCostumers()
        {
            var costumer = _costumerRepository.GetAll();   
            return Ok(costumer);
        }

        
        [HttpGet]
        [Route("costumer/get/{Id:Guid}")]
        public async Task<IActionResult> GetCostumerById(Guid id)
        {

            var costumer = _costumerRepository.Find(id);

            if (costumer == null)
            {
                return NotFound();
            }

            return Ok(costumer);
        }

        [HttpPost]
        [Route("costumer/new")]
        public async Task<IActionResult> AddCostumer(Costumer costumer)
        {
                _costumerRepository.Add(costumer);
                return Ok(costumer);
        }

        [HttpPut]
        [Route("costumer/update/{Id:Guid}")]
        public async Task<IActionResult> UpdateCostumer(CostumerViewModel costumerView, [FromRoute]Guid id)
        {
            var costumer = _costumerRepository.Find(id);
            if (costumer == null)
            {
                return NotFound();
            }

            try
            {
                costumer.FirstName = costumerView.FirstName;
                costumer.LastName = costumerView.LastName;
                costumer.Address = costumerView.Address;
                costumer.PhoneNumber = costumerView.PhoneNumber;
                costumer.CPF = costumerView.CPF;
                _costumerRepository.Update(costumer);
                return Ok(costumer);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("costumer/delete/{Id:Guid}")]
        public async Task<IActionResult> DeleteCostumer([FromRoute]Guid id)
        {
            _costumerRepository.Remove(id);
            return Ok();
        }
    }
}