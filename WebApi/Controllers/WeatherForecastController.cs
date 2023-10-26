using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private Company company = new Company();
        private Employee employee = new Employee();
        public WeatherForecastController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _repository.Company.Delete1(company);
            _repository.Employee.Create(employee);
            return new string[] { "value1", "value2" };
        }
    }
}