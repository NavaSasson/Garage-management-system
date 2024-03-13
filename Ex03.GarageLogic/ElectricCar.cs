using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : ElectricVehicle
    {
        private const float MaxCarBatteryTimeInHours = 4.8f;
        private CarInfo m_CarInfo;

        public CarInfo CarInfo
        {
            get
            {
                return m_CarInfo;
            }
            set
            {
                m_CarInfo = value;
            }
        }

        public ElectricCar(string i_LicenseNumber)
            : base(i_LicenseNumber, MaxCarBatteryTimeInHours, CarInfo.NumOfWheelsInCar, CarInfo.MaxAirPressureInCar)
        {
            CarInfo = new CarInfo();
        }

        public override Dictionary<string, string> GetVehicleParams()
        {
            Dictionary<string, string> paramsDict = base.GetVehicleParams();
            foreach (string carParam in CarInfo.GetCarParams())
            {
                paramsDict.Add(carParam, string.Empty);
            }

            return paramsDict;
        }

        public override void CheckIfVehicleDetailsInputIsLegal(Dictionary<string, string> i_VehicleParamsDict, out string o_InvalidParam
            , bool i_IsUserWantToInsertAllWheelsAtOnce)
        {
            base.CheckIfVehicleDetailsInputIsLegal(i_VehicleParamsDict, out o_InvalidParam, i_IsUserWantToInsertAllWheelsAtOnce);
            CarInfo.CheckIfCarDetailsInputIsLegal(i_VehicleParamsDict, out o_InvalidParam);
        }

        public override void UpdateVehicleParams(Dictionary<string, string> i_VehicleParamsDict)
        {
            base.UpdateVehicleParams(i_VehicleParamsDict);
            CarInfo.UpdateCarParams(i_VehicleParamsDict);
        }

        public override string ToString()
        {
            return base.ToString() + CarInfo.ToString();
        }
    }
}