using APIMaxiTransfersTest.Interface;
using APIMaxiTransfersTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIMaxiTransfersTest.Controllers
{
    [Route("Beneficiaries")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryRepository _repository;
        public BeneficiaryController(IBeneficiaryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Beneficiary>> Get([FromBody] Beneficiary beneficiary)
        {
            return await _repository.GetAllBeneficiariesAsync(beneficiary.Id);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post([FromBody] IEnumerable<Beneficiary> beneficiaries)
        {
            var resp = await _repository.CreateBeneficiary(beneficiaries);

            return resp ? new Response { IsSuccess = true, Message = "Beneficiary saved successfully", Result = beneficiaries } : new Response { IsSuccess = false, Message = "Error trying to save beneficiary", Result = beneficiaries };
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Put([FromBody] Beneficiary beneficiary)
        {
            var id = await _repository.UpdateBeneficiary(beneficiary);
            return id > 0 ? new Response { IsSuccess = true, Message = "Beneficiary updated successfully", Result = beneficiary } : new Response { IsSuccess = false, Message = "Error trying to update beneficiary", Result = beneficiary };
        }

        [HttpDelete]
        public async Task<ActionResult<Response>> Delete([FromBody] int id)
        {
            var idDeleted = await _repository.DeleteBeneficiary(id);
            return idDeleted > 0 ? new Response { IsSuccess = true, Message = "Beneficiary deleted successfully", Result = id } : new Response { IsSuccess = false, Message = "Error trying to delete beneficiary", Result = id };
        }
    }
}
