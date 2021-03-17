using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorAdmin.Repository
{
    public interface IBuildingStore
    {
        void Create(int floors, int elevators);
        Building GetBuilding();
    }
}
