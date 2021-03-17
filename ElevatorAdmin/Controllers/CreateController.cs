using ElevatorAdmin.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ElevatorAdmin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreateController : Controller
    {
        private readonly IBuildingStore _repo;
        public CreateController(IBuildingStore repo)
        {
            _repo = repo;
        }
       
        [HttpPost("building")]
        public void Index([FromBody] CreateBuildingModel createBuilding)
        {
            _repo.Create(createBuilding.Floors, createBuilding.Elevators);
        }
    }
}
