using BigBang_Assessment_26_5_23_.Model_Request_Response_;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public interface IRepoEmployee
    {
        Task<EmployeeResponse> GetEmployees();
        Task<EmployeeResponse> GetEmployeeById(string id);
        Task<EmployeeResponse> PostEmployee(EmployeeRequest employee);
        Task<EmployeeResponse> PutEmployee(string id, EmployeeRequest employee);
        Task<EmployeeResponse> DeleteEmployee(string id);
    }
}
