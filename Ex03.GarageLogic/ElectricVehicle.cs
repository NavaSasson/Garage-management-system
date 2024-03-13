using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        private const float MinBatteryTimeInHours = 0;
        private float m_RemaningBatteryTimeInHours;
        private readonly float r_MaxBatteryTimeInHours;

        public float RemaningBatteryTimeInHours
        {
            get
            {
                return m_RemaningBatteryTimeInHours;
            }
            set
            {
                m_RemaningBatteryTimeInHours = value;
            }
        }

        public float MaxBatteryTimeInHour
        {
            get
            {
                return r_MaxBatteryTimeInHours;
            }
        }

        public ElectricVehicle(string i_LicenseNumber, float i_MaxBatteryTimeInHours, int i_NumOfWheels, float i_MaxAirPressure)
            : base(i_LicenseNumber, i_NumOfWheels, i_MaxAirPressure)
        {
            m_RemaningBatteryTimeInHours = 0;
            r_MaxBatteryTimeInHours = i_MaxBatteryTimeInHours;
        }

        protected override void UpdateRemainigEnergyPrecentage()
        {
            RemaningEnergyPrecentage = RemaningBatteryTimeInHours / MaxBatteryTimeInHour;
        }

        public override void CheckIfLegalEnergyAmount(float i_EnergyAmount)
        {
            if (RemaningBatteryTimeInHours + i_EnergyAmount > MaxBatteryTimeInHour || i_EnergyAmount < 0)
            {
                throw new ValueOutOfRangeException(MinBatteryTimeInHours, MaxBatteryTimeInHour);
            }
        }

        public override void FillEnergySource(float i_EnergyToFill)
        {
            RemaningBatteryTimeInHours += i_EnergyToFill;
            UpdateRemainigEnergyPrecentage();
        }

        public override Dictionary<string, string> GetVehicleParams()
        {
            Dictionary<string, string> paramsDict = base.GetVehicleParams();
            paramsDict.Add("RemaningBatteryTimeInHours", string.Empty);

            return paramsDict;
        }

        public override void CheckIfVehicleDetailsInputIsLegal(Dictionary<string, string> i_VehicleParamsDict, out string o_InvalidParam
            , bool i_IsUserWantToInsertAllWheelsAtOnce)
        {
            base.CheckIfVehicleDetailsInputIsLegal(i_VehicleParamsDict, out o_InvalidParam, i_IsUserWantToInsertAllWheelsAtOnce);
            string remaningBatteryTimeInHours = i_VehicleParamsDict["RemaningBatteryTimeInHours"];
            if (!float.TryParse(remaningBatteryTimeInHours, out float result))
            {
                o_InvalidParam = "RemaningBatteryTimeInHours";
                throw new FormatException();
            }
            else if (float.Parse(remaningBatteryTimeInHours) < MinBatteryTimeInHours || float.Parse(remaningBatteryTimeInHours) > MaxBatteryTimeInHour)
            {
                o_InvalidParam = "RemaningBatteryTimeInHours";
                throw new ValueOutOfRangeException(MinBatteryTimeInHours, MaxBatteryTimeInHour);
            }
        }

        public override void UpdateVehicleParams(Dictionary<string, string> i_VehicleParamsDict)
        {
            base.UpdateVehicleParams(i_VehicleParamsDict);
            RemaningBatteryTimeInHours = float.Parse(i_VehicleParamsDict["RemaningBatteryTimeInHours"]);
            UpdateRemainigEnergyPrecentage();
        }
    }
}