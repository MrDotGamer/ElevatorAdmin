using ElevatorAdmin.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ElevatorAdmin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElevatorController : ControllerBase
    {
        private readonly IBuilding building;
        public ElevatorController(IBuildingStore repo)
        {
            building = repo.GetBuilding();
        }
        [HttpGet("status/{id}")]
        public ElevatorStatus GetElevatorStatus(int id)
        {
            var elevator = building.Elevators().FirstOrDefault(x => x.ElevatorId == id);
            return elevator.GetStatus();
        }
        [HttpPost("direction")]
        public void Go([FromBody] Direction  direction)
        {
            building.CallElevator(direction);
        }

    }
}
