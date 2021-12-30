using APIMaxiTransfersTest.Interface;
using APIMaxiTransfersTest.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace APIMaxiTransfersTest.Repository
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly IConfiguration _configuration;
        public BeneficiaryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateBeneficiary(IEnumerable<Beneficiary>  lstBeneficiary)
        {
            bool resp = true;
            foreach (Beneficiary item in lstBeneficiary)
            {
                SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                await Conn.OpenAsync();
                try
                {
                    Beneficiary _beneficiary1 = new Beneficiary();

                    if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                    SqlCommand objCommand = new SqlCommand("CreateBeneficiary", Conn);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.Clear();

                    SqlParameter Name = new SqlParameter();
                    Name.ParameterName = "@Name";
                    Name.DbType = DbType.String;
                    Name.SqlValue = item.Name;
                    Name.Direction = ParameterDirection.Input;
                    objCommand.Parameters.Add(Name);

                    SqlParameter LastName = new SqlParameter();
                    LastName.ParameterName = "@LastNAme";
                    LastName.DbType = DbType.String;
                    LastName.SqlValue = item.LastName;
                    LastName.Direction = ParameterDirection.Input;
                    objCommand.Parameters.Add(LastName);

                    SqlParameter DoB = new SqlParameter();
                    DoB.ParameterName = "@DoB";
                    DoB.DbType = DbType.DateTime;
                    DoB.SqlValue = item.DoB.ToShortDateString();
                    DoB.Direction = ParameterDirection.Input;
                    objCommand.Parameters.Add(DoB);

                    SqlParameter EmployeeNumber = new SqlParameter();
                    EmployeeNumber.ParameterName = "@EmployeeId";
                    EmployeeNumber.DbType = DbType.Int32;
                    EmployeeNumber.SqlValue = item.EmployeeId;
                    EmployeeNumber.Direction = ParameterDirection.Input;
                    objCommand.Parameters.Add(EmployeeNumber);

                    SqlParameter Curp = new SqlParameter();
                    Curp.ParameterName = "@Curp";
                    Curp.DbType = DbType.String;
                    Curp.SqlValue = item.Curp;
                    Curp.Direction = ParameterDirection.Input;
                    objCommand.Parameters.Add(Curp);

                    SqlParameter Ssn = new SqlParameter();
                    Ssn.ParameterName = "@Ssn";
                    Ssn.DbType = DbType.String;
                    Ssn.SqlValue = item.Ssn;
                    Ssn.Direction = ParameterDirection.Input;
                    objCommand.Parameters.Add(Ssn);

                    SqlParameter Phone = new SqlParameter();
                    Phone.ParameterName = "@Phone";
                    Phone.DbType = DbType.String;
                    Phone.SqlValue = item.Phone;
                    Phone.Direction = ParameterDirection.Input;
                    objCommand.Parameters.Add(Phone);

                    SqlParameter Nationality = new SqlParameter();
                    Nationality.ParameterName = "@Nationality";
                    Nationality.DbType = DbType.String;
                    Nationality.SqlValue = item.Nationality;
                    Nationality.Direction = ParameterDirection.Input;
                    objCommand.Parameters.Add(Nationality);

                    SqlParameter Id = new SqlParameter();
                    Id.ParameterName = "@Id";
                    Id.DbType = DbType.Int32;
                    Id.Direction = ParameterDirection.Output;
                    objCommand.Parameters.Add(Id);
                    SqlDataReader _Reader = await objCommand.ExecuteReaderAsync();

                    _beneficiary1.Id = (int)(Id.Value ?? 0);

                    //return _beneficiary1.Id;
                }
                catch (Exception ex)
                {
                    resp = false;

                    return resp; 
                }
                finally
                {
                    if (Conn != null)
                    {
                        if (Conn.State == ConnectionState.Open)
                        {
                            Conn.Close();
                            Conn.Dispose();
                        }
                    }
                }
            }
            return resp;
        }
        public async Task<int> DeleteBeneficiary(int id)
        {
            SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await Conn.OpenAsync();
            try
            {
                Beneficiary _beneficiary = new Beneficiary();

                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("DeleteBeneficiary", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Clear();

                SqlParameter Id = new SqlParameter();
                Id.ParameterName = "@Id";
                Id.DbType = DbType.Int32;
                Id.SqlValue = id;
                Id.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Id);

                SqlParameter IdUpdated = new SqlParameter();
                IdUpdated.ParameterName = "@IdUpdated";
                IdUpdated.DbType = DbType.Int32;
                IdUpdated.Direction = ParameterDirection.Output;
                objCommand.Parameters.Add(IdUpdated);
                SqlDataReader _Reader = await objCommand.ExecuteReaderAsync();

                while (await _Reader.ReadAsync())
                {
                    _beneficiary.Id = Convert.ToInt32(_Reader["IdUpdated"]);
                }

                return _beneficiary.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
        public async Task<IEnumerable<Beneficiary>> GetAllBeneficiariesAsync(int id)
        {

            SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await Conn.OpenAsync();
            try
            {
                List<Beneficiary> _listBeneficiary = new List<Beneficiary>();

                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("GetAllEmployees", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Clear();

                SqlParameter Id = new SqlParameter();
                Id.ParameterName = "@Id";
                Id.DbType = DbType.Int32;
                Id.SqlValue = id;
                Id.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Id);
                SqlDataReader _Reader = await objCommand.ExecuteReaderAsync();

                while (await _Reader.ReadAsync())
                {
                    Beneficiary objBeneficiary = new Beneficiary();
                    objBeneficiary.Id = Convert.ToInt32(_Reader["Id"]);
                    objBeneficiary.Name = _Reader["Name"].ToString();
                    objBeneficiary.LastName = _Reader["LastNAme"].ToString();
                    objBeneficiary.DoB = Convert.ToDateTime(_Reader["DoB"]);
                    objBeneficiary.EmployeeId = Convert.ToInt32(_Reader["EmployeeId"]);
                    objBeneficiary.Curp = _Reader["Curp"].ToString();
                    objBeneficiary.Ssn = _Reader["Ssn"].ToString();
                    objBeneficiary.Phone = _Reader["Phone"].ToString();
                    objBeneficiary.Nationality = _Reader["Nationality"].ToString();
                    _listBeneficiary.Add(objBeneficiary);
                }

                return _listBeneficiary;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
        public async Task<int> UpdateBeneficiary(Beneficiary beneficiary)
        {
            SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await Conn.OpenAsync();
            try
            {
                Beneficiary _beneficiary = new Beneficiary();

                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("UpdateBeneficiary", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Clear();

                SqlParameter Id = new SqlParameter();
                Id.ParameterName = "@Id";
                Id.DbType = DbType.Int32;
                Id.SqlValue = beneficiary.Id;
                Id.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Id);

                SqlParameter Name = new SqlParameter();
                Name.ParameterName = "@Name";
                Name.DbType = DbType.String;
                Name.SqlValue = beneficiary.Name;
                Name.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Name);

                SqlParameter LastName = new SqlParameter();
                LastName.ParameterName = "@LastNAme";
                LastName.DbType = DbType.String;
                LastName.SqlValue = beneficiary.LastName;
                LastName.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(LastName);

                SqlParameter DoB = new SqlParameter();
                DoB.ParameterName = "@DoB";
                DoB.DbType = DbType.DateTime;
                DoB.SqlValue = beneficiary.DoB;
                DoB.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(DoB);

                SqlParameter EmployeeNumber = new SqlParameter();
                EmployeeNumber.ParameterName = "@EmployeeId";
                EmployeeNumber.DbType = DbType.Int32;
                EmployeeNumber.SqlValue = beneficiary.EmployeeId;
                EmployeeNumber.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(EmployeeNumber);

                SqlParameter Curp = new SqlParameter();
                Curp.ParameterName = "@Curp";
                Curp.DbType = DbType.String;
                Curp.SqlValue = beneficiary.Curp;
                Curp.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Curp);

                SqlParameter Ssn = new SqlParameter();
                Ssn.ParameterName = "@Ssn";
                Ssn.DbType = DbType.String;
                Ssn.SqlValue = beneficiary.Ssn;
                Ssn.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Ssn);

                SqlParameter Phone = new SqlParameter();
                Phone.ParameterName = "@Phone";
                Phone.DbType = DbType.String;
                Phone.SqlValue = beneficiary.Phone;
                Phone.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Phone);

                SqlParameter Nationality = new SqlParameter();
                Nationality.ParameterName = "@Nationality";
                Nationality.DbType = DbType.String;
                Nationality.SqlValue = beneficiary.Nationality;
                Nationality.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Nationality);

                SqlParameter IdUpdated = new SqlParameter();
                IdUpdated.ParameterName = "@IdUpdated";
                IdUpdated.DbType = DbType.Int32;
                IdUpdated.Direction = ParameterDirection.Output;
                objCommand.Parameters.Add(IdUpdated);
                SqlDataReader _Reader = await objCommand.ExecuteReaderAsync();

                while (await _Reader.ReadAsync())
                {
                    _beneficiary.Id = Convert.ToInt32(_Reader[0]);
                }
                _beneficiary.Id = (int)(Id.Value ?? 0);

                return _beneficiary.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
    }
}
