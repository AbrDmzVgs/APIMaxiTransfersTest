using APIMaxiTransfersTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIMaxiTransfersTest.Interface
{
    public interface IBeneficiaryRepository
    {
        Task<IEnumerable<Beneficiary>> GetAllBeneficiariesAsync(int id);
        Task<bool> CreateBeneficiary(IEnumerable<Beneficiary> beneficiaries);
        Task<int> UpdateBeneficiary(Beneficiary beneficiary);
        Task<int> DeleteBeneficiary(int id);
    }
}
