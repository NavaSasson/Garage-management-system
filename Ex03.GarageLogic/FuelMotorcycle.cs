using System.Collections.Generic;
namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : FuelVehicle
    {
        private const float MaxFuelTankInLiter = 5.8f;
        private MotorcycleInfo m_MotorcycleInfo;

        public MotorcycleInfo MotorcycleInfo
        {
            get
            {
                return m_MotorcycleInfo;
            }
            set
            {
                m_MotorcycleInfo = value;
            }
        }

        public FuelMotorcycle(string i_LicenseNumber)
            : base(i_LicenseNumber, eFuelType.Octan98, MaxFuelTankInLiter, MotorcycleInfo.NumOfWheelsInMotorcycle, MotorcycleInfo.MaxAirPressureInMotorcycle)
        {
            MotorcycleInfo = new MotorcycleInfo();
        }

        public override Dictionary<string, string> GetVehicleParams()
        {
            Dictionary<string, string> paramsDict = base.GetVehicleParams();
            foreach (string motorcycleParam in MotorcycleInfo.GetMotorcycleParams())
            {
                paramsDict.Add(motorcycleParam, string.Empty);
            }

            return paramsDict;
        }

        public override void CheckIfVehicleDetailsInputIsLegal(Dictionary<string, string> i_VehicleParamsDict, out string o_InvalidParam, bool i_IsUserWantToInsertAllWheelsAtOnce)
        {
            base.CheckIfVehicleDetailsInputIsLegal(i_VehicleParamsDict, out o_InvalidParam, i_IsUserWantToInsertAllWheelsAtOnce);
            MotorcycleInfo.CheckIfMotorcycleDetailsInputIsLegal(i_VehicleParamsDict, out o_InvalidParam);
        }

        public override void UpdateVehicleParams(Dictionary<string, string> i_VehicleParamsDict)
        {
            base.UpdateVehicleParams(i_VehicleParamsDict);
            MotorcycleInfo.UpdateMotorcycleParams(i_VehicleParamsDict);
        }

        public override string ToString()
        {
            string fuelMororcyclekDetails = base.ToString() + MotorcycleInfo.ToString();

            return fuelMororcyclekDetails;
        }
    }
}