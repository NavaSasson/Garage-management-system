using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private List<Vehicle> m_VehiclesList;

        public List<Vehicle> VehiclesList
        {
            get
            {
                return m_VehiclesList;
            }
            set
            {
                m_VehiclesList = value;
            }
        }

        public Garage()
        {
            m_VehiclesList = new List<Vehicle>();
        }

        public List<Vehicle> CreateListOfVehiclesWithSameStatusInGarage(int i_VehicleStatus)
        {
            List<Vehicle> vehicleInGarageWithSameStatus = new List<Vehicle>();

            foreach (Vehicle vehicle in VehiclesList)
            {
                if ((int)vehicle.VehicleInGarageInfo.VehicleStatus == i_VehicleStatus)
                {
                    vehicleInGarageWithSameStatus.Add(vehicle);
                }
            }

            return vehicleInGarageWithSameStatus;
        }

        public int CheckIfVehicleInGarageAndfindItIdx(string i_LicenseNum)
        {
            int idxOfVehicle = VehiclesList.FindIndex(vehicle => vehicle.LicenseNumber == i_LicenseNum);

            if (idxOfVehicle == -1)
            {
                throw new ArgumentException();
            }

            return idxOfVehicle;
        }

        public void ChangeVehicleStatus(int i_VehicleIdx, int i_VehicleStatus)
        {
            m_VehiclesList[i_VehicleIdx].VehicleInGarageInfo.VehicleStatus = (eVehicleStatusType)i_VehicleStatus;
        }

        public void InflateAllWheelsToMaxPressure(int i_IndexOfLicenseNum)
        {
            List<Wheel> vehiclesWheels = VehiclesList[i_IndexOfLicenseNum].VehicleWheels;

            foreach (Wheel wheel in vehiclesWheels)
            {
                wheel.InflateAWheel();
            }
        }

        public void CheckIfVehicleIsFuelVehicle(int i_VehicleIdx)
        {
            if (!(VehiclesList[i_VehicleIdx] is FuelVehicle))
            {
                throw new ArgumentException();
            }
        }

        public void CheckIfVehicleIsElectricVehicle(int i_VehicleIdx)
        {
            if (!(VehiclesList[i_VehicleIdx] is ElectricVehicle))
            {
                throw new ArgumentException();
            }
        }

        public void AddVehicleToGarage(Vehicle i_VehicleToAdd)
        {
            VehiclesList.Add(i_VehicleToAdd);
        }

        public void CheckIfLegalVehicleType(int i_VehiclTypeInput)
        {
            if (!Enum.IsDefined(typeof(eVehicleType), i_VehiclTypeInput))
            {
                throw new ArgumentException();
            }
        }
    }
}