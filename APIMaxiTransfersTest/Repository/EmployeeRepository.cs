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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> CreateEmployee(Employee employee)
        {
            SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await Conn.OpenAsync();
            try
            {
                Employee _employee = new Employee();

                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("CreateEmployee", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Clear();

                SqlParameter Name = new SqlParameter();
                Name.ParameterName = "@Name";
                Name.DbType = DbType.String;
                Name.SqlValue = employee.Name;
                Name.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Name);

                SqlParameter LastName = new SqlParameter();
                LastName.ParameterName = "@LastNAme";
                LastName.DbType = DbType.String;
                LastName.SqlValue = employee.LastName;
                LastName.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(LastName);

                SqlParameter DoB = new SqlParameter();
                DoB.ParameterName = "@DoB";
                DoB.DbType = DbType.DateTime;
                DoB.SqlValue = employee.DoB.ToShortDateString();
                DoB.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(DoB);

                SqlParameter EmployeeNumber = new SqlParameter();
                EmployeeNumber.ParameterName = "@EmployeeNumber";
                EmployeeNumber.DbType = DbType.Int32;
                EmployeeNumber.SqlValue = employee.EmployeeNumber;
                EmployeeNumber.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(EmployeeNumber);

                SqlParameter Curp = new SqlParameter();
                Curp.ParameterName = "@Curp";
                Curp.DbType = DbType.String;
                Curp.SqlValue = employee.Curp;
                Curp.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Curp);

                SqlParameter Ssn = new SqlParameter();
                Ssn.ParameterName = "@Ssn";
                Ssn.DbType = DbType.String;
                Ssn.SqlValue = employee.Ssn;
                Ssn.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Ssn);

                SqlParameter Phone = new SqlParameter();
                Phone.ParameterName = "@Phone";
                Phone.DbType = DbType.String;
                Phone.SqlValue = employee.Phone;
                Phone.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Phone);

                SqlParameter Nationality = new SqlParameter();
                Nationality.ParameterName = "@Nationality";
                Nationality.DbType = DbType.String;
                Nationality.SqlValue = employee.Nationality;
                Nationality.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Nationality);

                SqlParameter Id = new SqlParameter();
                Id.ParameterName = "@Id";
                Id.DbType = DbType.Int32;
                Id.Direction = ParameterDirection.Output;
                objCommand.Parameters.Add(Id);
                SqlDataReader _Reader = await objCommand.ExecuteReaderAsync();

                //while (await _Reader.ReadAsync())
                //{
                //    _employee.Id = Convert.ToInt32(_Reader["Id"]);
                //}
                _employee.Id = (int)(Id.Value ?? 0);

                return _employee.Id;
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
        public async Task<int> DeleteEmployee(int id)
        {
            SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await Conn.OpenAsync();
            try
            {
                Employee _employee = new Employee();

                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("DeleteEmployee", Conn);
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
                    _employee.Id = Convert.ToInt32(_Reader["IdUpdated"]);
                }

                return _employee.Id;
            }
            catch(Exception ex)
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
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(int id)
        {

            SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await Conn.OpenAsync();
            try
            {
                List<Employee> _listEmployee = new List<Employee>();

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
                    Employee objEmployee = new Employee();
                    objEmployee.Id = Convert.ToInt32(_Reader["Id"]);
                    objEmployee.Name = _Reader["Name"].ToString();
                    objEmployee.LastName = _Reader["LastNAme"].ToString();
                    objEmployee.DoB = Convert.ToDateTime(_Reader["DoB"]);
                    objEmployee.EmployeeNumber =Convert.ToInt32(_Reader["EmployeeNumber"]);
                    objEmployee.Curp = _Reader["Curp"].ToString();
                    objEmployee.Ssn = _Reader["Ssn"].ToString();
                    objEmployee.Phone = _Reader["Phone"].ToString();
                    objEmployee.Nationality = _Reader["Nationality"].ToString();
                    _listEmployee.Add(objEmployee);
                }

                return _listEmployee;
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
        public async Task<int> UpdateEmployee(Employee employee)
        {
            SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await Conn.OpenAsync();
            try
            {
                Employee _employee = new Employee();

                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("UpdateEmployee", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Clear();

                SqlParameter Id = new SqlParameter();
                Id.ParameterName = "@Id";
                Id.DbType = DbType.Int32;
                Id.SqlValue = employee.Id;
                Id.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Id);

                SqlParameter Name = new SqlParameter();
                Name.ParameterName = "@Name";
                Name.DbType = DbType.String;
                Name.SqlValue = employee.Name;
                Name.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Name);

                SqlParameter LastName = new SqlParameter();
                LastName.ParameterName = "@LastNAme";
                LastName.DbType = DbType.String;
                LastName.SqlValue = employee.LastName;
                LastName.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(LastName);

                SqlParameter DoB = new SqlParameter();
                DoB.ParameterName = "@DoB";
                DoB.DbType = DbType.DateTime;
                DoB.SqlValue = employee.DoB;
                DoB.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(DoB);

                SqlParameter EmployeeNumber = new SqlParameter();
                EmployeeNumber.ParameterName = "@EmployeeNumber";
                EmployeeNumber.DbType = DbType.Int32;
                EmployeeNumber.SqlValue = employee.EmployeeNumber;
                EmployeeNumber.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(EmployeeNumber);

                SqlParameter Curp = new SqlParameter();
                Curp.ParameterName = "@Curp";
                Curp.DbType = DbType.String;
                Curp.SqlValue = employee.Curp;
                Curp.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Curp);

                SqlParameter Ssn = new SqlParameter();
                Ssn.ParameterName = "@Ssn";
                Ssn.DbType = DbType.String;
                Ssn.SqlValue = employee.Ssn;
                Ssn.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Ssn);

                SqlParameter Phone = new SqlParameter();
                Phone.ParameterName = "@Phone";
                Phone.DbType = DbType.String;
                Phone.SqlValue = employee.Phone;
                Phone.Direction = ParameterDirection.Input;
                objCommand.Parameters.Add(Phone);

                SqlParameter Nationality = new SqlParameter();
                Nationality.ParameterName = "@Nationality";
                Nationality.DbType = DbType.String;
                Nationality.SqlValue = employee.Nationality;
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
                    _employee.Id = Convert.ToInt32(_Reader[0]);
                }
                _employee.Id = (int)(Id.Value ?? 0);

                return _employee.Id;
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
