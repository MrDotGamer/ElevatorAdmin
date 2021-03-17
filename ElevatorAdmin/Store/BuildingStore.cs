namespace ElevatorAdmin.Repository
{
    public class BuildingStore : IBuildingStore
    {
        private Building _building;

        public void Create(int floors,int elevatorCount)
        {
            _building = new Building(floors, elevatorCount);
        }

        public Building GetBuilding()
        {
            return _building;
        }
    }
}