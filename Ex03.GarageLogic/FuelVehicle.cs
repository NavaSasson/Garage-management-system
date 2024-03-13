using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class FuelVehicle : Vehicle
    {
        private const float MinFuelInLiter = 0;
        private eFuelType m_FuelType;
        private float m_CurrentFuelInLiter;
        private readonly float r_MaxFuelInLiter;

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
            set
            {
                m_FuelType = value;
            }
        }

        public float CurrentFuelInLiter
        {
            get
            {
                return m_CurrentFuelInLiter;
            }
            set
            {
                m_CurrentFuelInLiter = value;
            }
        }

        public float MaxFuelInLiter
        {
            get
            {
                return r_MaxFuelInLiter;
            }
        }

        public FuelVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_MaxFuelInLiter, int i_NumOfWheels, float i_MaxAirPressure)
            : base(i_LicenseNumber, i_NumOfWheels, i_MaxAirPressure)
        {
            m_FuelType = i_FuelType;
            m_CurrentFuelInLiter = 0;
            r_MaxFuelInLiter = i_MaxFuelInLiter;
        }

        protected override void UpdateRemainigEnergyPrecentage()
        {
            RemaningEnergyPrecentage = CurrentFuelInLiter / MaxFuelInLiter;
        }

        public void CheckIfLegalFuelType(int i_FuelTypeInput)
        {
            if ((eFuelType)i_FuelTypeInput != FuelType)
            {
                throw new ArgumentException();
            }
        }

        public override void CheckIfLegalEnergyAmount(float i_EnergyAmount)
        {
            if (CurrentFuelInLiter + i_EnergyAmount > MaxFuelInLiter || i_EnergyAmount < 0)
            {
                throw new ValueOutOfRangeException(MaxFuelInLiter, MinFuelInLiter);
            }
        }

        public override void FillEnergySource(float i_EnergyToFill)
        {
            CurrentFuelInLiter += i_EnergyToFill;
            UpdateRemainigEnergyPrecentage();
        }

        public override Dictionary<string, string> GetVehicleParams()
        {
            Dictionary<string, string> paramsDict = base.GetVehicleParams();
            paramsDict.Add("CurrentFuelInLiter", string.Empty);

            return paramsDict;
        }

        public override void CheckIfVehicleDetailsInputIsLegal(Dictionary<string, string> i_VehicleParamsDict, out string o_InvalidParam
            , bool i_IsUserWantToInsertAllWheelsAtOnce)
        {
            base.CheckIfVehicleDetailsInputIsLegal(i_VehicleParamsDict, out o_InvalidParam, i_IsUserWantToInsertAllWheelsAtOnce);
            string currentFuelInLiter = i_VehicleParamsDict["CurrentFuelInLiter"];
            if (!float.TryParse(currentFuelInLiter, out float result))
            {
                o_InvalidParam = "CurrentFuelInLiter";
                throw new FormatException();
            }
            else if (float.Parse(currentFuelInLiter) < MinFuelInLiter || float.Parse(currentFuelInLiter) > MaxFuelInLiter)
            {
                o_InvalidParam = "CurrentFuelInLiter";
                throw new ValueOutOfRangeException(MinFuelInLiter, MaxFuelInLiter);
            }
        }

        public override void UpdateVehicleParams(Dictionary<string, string> i_VehicleParamsDict)
        {
            base.UpdateVehicleParams(i_VehicleParamsDict);
            CurrentFuelInLiter = float.Parse(i_VehicleParamsDict["CurrentFuelInLiter"]);
            UpdateRemainigEnergyPrecentage();
        }

        public override string ToString()
        {
            string fuelVehicleDetails = string.Format(
@"Fuel Type: {0}
", FuelType);
            return base.ToString() + fuelVehicleDetails;
        }
    }
}