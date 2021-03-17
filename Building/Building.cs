using System;
using System.Collections.Generic;
using System.Text;

namespace Building
{
     public class Building
    {
        private readonly int _floors;
        private readonly int _elevatorCount;
        private List<Elevator> elevators;
        public Building(int floors, int elevatorCount)
        {
            _elevatorCount = elevatorCount;
            _floors = floors;
            for (int i = 0; i < elevatorCount; i++)
            {
                elevators.Add(new Elevator(floors));
            }
        }
        public int Floors()
        {
            return _floors;
        }
        public int Elevators()
        {
            return _elevatorCount;
        }

    }

    internal class Elevator
    {
        public int Position { get; set; }
        private int topfloor;
        private ElevatorStatus status = ElevatorStatus.STOPPED;
        public Elevator(int NumberOfFloors)
        {
            Position = 1;
            topfloor = NumberOfFloors;
            status = ElevatorStatus.STOPPED;
        }
        public ElevatorStatus GetStatus()
        {
            return status;
        } 
        public enum ElevatorStatus
        {

            UP,
            STOPPED,
            DOWN
        }
       
    }

   
}
