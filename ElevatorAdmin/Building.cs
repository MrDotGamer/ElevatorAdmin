using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorAdmin
{
    public class Building : IBuilding
    {
        private readonly List<Elevator> _elevators = new List<Elevator>();
        public Building(int floors, int elevatorCount)
        {
            for (int i = 0; i < elevatorCount; i++)
            {
                _elevators.Add(new Elevator(floors,i));
            }
        }
        public List<Elevator> Elevators()
        {
            return _elevators;
        }

        public async Task CallElevator(Direction direction)
        {
            var elevator = _elevators.FirstOrDefault(x => x.ElevatorId == direction.ElevatorNrfromTo[0]);
            await elevator.FloorButton(direction);
        }

        public ElevatorStatus GetStatus(int elevatorId)
        {
            return _elevators.Find(x => x.ElevatorId == elevatorId).GetStatus();
        }
    }

    public class Elevator
    {
        public int ElevatorId { get; set; }
        private int _position { get; set; }
        private int _topfloor;
        private bool[] neededFloor;
        private ElevatorAction _action = ElevatorAction.STOPPED;
        public Elevator(int NumberOfFloors,int elevatorId)
        {
            neededFloor = new bool [NumberOfFloors + 1];
            _position = 1;
            _topfloor = NumberOfFloors;
            _action = ElevatorAction.STOPPED;
            ElevatorId = elevatorId;
        }
        private void Stop(int floor)
        {
            _action = ElevatorAction.STOPPED;
            _position = floor;
            neededFloor[floor] = false;
        }
        private async Task MoveUp(int floor)
        {

            for (int i = _position; i <= _topfloor; i++)
            {
                if (neededFloor[i])
                {
                    Stop(floor);
                    await DoorOpening(ElevatorId);
                    await DoorClosing(ElevatorId);
                    break;
                }
                else
                {
                    Debug.WriteLine($"Kylam...Elevator nr {ElevatorId}, Aukstas{i},Time {DateTime.Now}");
                    _action = ElevatorAction.MOVEUP;
                    await Task.Delay(Const.ElevatorSpeed);
                    _position = i;
                    continue;
                }
            }

            _action = ElevatorAction.STOPPED;
        }
        private async Task MoveDown(int floor)
        {
            for (int i = _position; i >= 1; i--)
            {
                if (neededFloor[i])
                {
                    
                    Stop(floor);
                    await DoorOpening(ElevatorId);
                    await DoorClosing(ElevatorId);
                    break;
                }
                else
                {
                    Debug.WriteLine($"Leidziames...Elevator nr {ElevatorId}, Aukstas{i},Time {DateTime.Now}");
                    _action = ElevatorAction.MOVEDOWN;
                    await Task.Delay(Const.ElevatorSpeed) ;
                    _position = i;
                    continue;
                }
            }
            _action = ElevatorAction.STOPPED;
        }
        void DontMove()
        {
            Debug.WriteLine($"Musu aukstas ,Time {DateTime.Now}");
        }
        public async Task FloorButton(Direction direction)
        {
            if (direction.ElevatorNrfromTo[1] > _topfloor)
            {
                return;
            }
           for(int i = 1; i< direction.ElevatorNrfromTo.Length ; i++)
            {
                neededFloor[direction.ElevatorNrfromTo[i]] = true;
                var floor = direction.ElevatorNrfromTo[i];
                switch (_action)
                {

                    case ElevatorAction.MOVEDOWN:
                        await Check(_position, floor);
                        break;

                    case ElevatorAction.STOPPED:
                        await Check(_position, floor);
                        break;

                    case ElevatorAction.MOVEUP:
                        await Check(_position, floor);
                        break;

                    default:
                        break;
                }
            }
        }
        private async Task DoorOpening(int elevatorId)
        {
            Debug.WriteLine($"{elevatorId} Lifto durys atsidaro ,Time {DateTime.Now}");
            await Task.Delay(Const.DoorOpenOrClosed);
        }
        private async Task DoorClosing(int elevatorId)
        {
            Debug.WriteLine($"{elevatorId} Lifto durys uzsidaro ,Time {DateTime.Now}");
            await Task.Delay(Const.DoorOpenOrClosed);
        }
        private async Task Check(int position,int floor)
        {
            if (position < floor)
                await MoveUp(floor);
            else if (position == floor)
                DontMove();
            else
                await MoveDown(floor);
        }
        public ElevatorStatus GetStatus()
        {
            return new ElevatorStatus {  ElevatorAction = _action,Position = _position, ElevatorId = ElevatorId };
        }
    }
    public interface IBuilding
    {
        public Task CallElevator(Direction direction);
        public ElevatorStatus GetStatus(int elevatorId);
        List<Elevator> Elevators();
    }
    public enum ElevatorAction
    {
        MOVEUP,
        STOPPED,
        MOVEDOWN
    }
    
    public class ElevatorStatus
    {
        public int ElevatorId { get; set; }
        public ElevatorAction ElevatorAction { get; set; }
        public int Position { get; set; }
    }
    public class CreateBuildingModel
    {
        public int Floors { get; set; }
        public int Elevators { get; set; }
    }
    public class Direction
    {
        public int [] ElevatorNrfromTo { get; set; }
    }
}
