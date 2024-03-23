using FormulaOne.DataService.Repository.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests.Achievement;
using FormulaOne.Entities.Dtos.Responses.Achievement;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AchievementsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetDriverAchievements(Guid id)
        {
            var driverAchievements = await _unitOfWork.AchievementRepo.GetDriverAchievementsAsync(id);

            if (driverAchievements is null)
                return NotFound("Achievements not found");

            var response = driverAchievements.Adapt<DriverAchievementResponse>();
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAchievement(Guid id)
        {
            var achievement = await _unitOfWork.AchievementRepo.GetById(id);
            if (achievement is null)
                return NotFound("Achievements not found");

            var achievementResponse = achievement.Adapt<DriverAchievementResponse>();
            return Ok(achievementResponse);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var achievements = await _unitOfWork.AchievementRepo.GetAll();

            var achievementResponse = achievements.Adapt<List<DriverAchievementResponse>>();
            return Ok(achievementResponse);
        }


        [HttpPost]
        public async Task<IActionResult> AddAchievement(CreateDriverAchievementRequest achievementRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);

            
            var achievement = achievementRequest.Adapt<Achievement>();
            await _unitOfWork.AchievementRepo.Add(achievement);

            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (!isSaved)
                return BadRequest("Achievement faild to be added");

            return CreatedAtAction(nameof(GetAchievement), new { id = achievement.Id }, achievement);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAchievement(UpdateDriverAchievementRequest achievementRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);


            var achievement = achievementRequest.Adapt<Achievement>();

            var updateResult = await _unitOfWork.AchievementRepo.Update(achievement);
            if (!updateResult)
                return NotFound("Achievement not found");

            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (!isSaved)
                return BadRequest("Achievement faild to be updated");

            var achievementResponse = achievement.Adapt<DriverAchievementResponse>();
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAchievement(Guid id)
        {
            var isDeleted = await _unitOfWork.AchievementRepo.Delete(id);
            if (!isDeleted)
                return NotFound("Achievement not found");

            var isSaved = await _unitOfWork.SaveChangesAsync();
            if (!isSaved)
                return BadRequest("Achievement faild to be deleted");

            return NoContent();
        }
    }
}
