using FormulaOne.DataService.Repository.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests.Driver;
using FormulaOne.Entities.Dtos.Responses;
using FormulaOne.Entities.Dtos.Responses.Driver;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DriversController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetDriverById(Guid id)
        {
            var driver = await _unitOfWork.DriverRepo.GetDriverById(id);
            if (driver is null)
                return NotFound();

            var driverResponse = driver.Adapt<DriverResponse>();
            return Ok(driverResponse);            
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _unitOfWork.DriverRepo.GetAll();

            var driverResponse = drivers.Adapt<List<DriverResponse>>();
            return Ok(driverResponse);
        }


        [HttpPost]
        public async Task<IActionResult> AddDriver(CreateDriverRequest driverRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var driver = driverRequest.Adapt<Driver>();
            await _unitOfWork.DriverRepo.Add(driver);

            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (!isSaved)
                return BadRequest("Driver faild to be added");

            return CreatedAtAction(nameof(GetDriverById), new {id = driver.Id}, driver);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateDriver(UpdateDriverRequest driverRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var driver = driverRequest.Adapt<Driver>();
            var updateResult = await _unitOfWork.DriverRepo.Update(driver);
            if (!updateResult)
                return NotFound("Driver not found");

            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (!isSaved)
                return BadRequest("Driver faild to be updated");

            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDriver(Guid id)
        {
            var isDeleted = await _unitOfWork.DriverRepo.Delete(id);
            if (!isDeleted)
                return NotFound("Driver not found");

            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (!isSaved)
                return BadRequest("Driver faild to be deleted");

            return NoContent();
        }
    }
}
