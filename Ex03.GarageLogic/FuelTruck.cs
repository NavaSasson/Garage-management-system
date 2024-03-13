using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class FuelTruck : FuelVehicle
    {
        private const float MaxFuelTankInLiter = 110;
        private TruckInfo m_TruckInfo;

        public TruckInfo TruckInfo
        {
            get
            {
                return m_TruckInfo;
            }
            set
            {
                m_TruckInfo = value;
            }
        }

        public FuelTruck(string i_LicenseNumber)
            : base(i_LicenseNumber, eFuelType.Soler, MaxFuelTankInLiter, TruckInfo.NumOfWheelsInTruck, TruckInfo.MaxAirPressureInTruck)
        {
            RemaningEnergyPrecentage = 0;
            TruckInfo = new TruckInfo();
        }

        public override Dictionary<string, string> GetVehicleParams()
        {
            Dictionary<string, string> paramsDict = base.GetVehicleParams();
            foreach (string truckParam in TruckInfo.GetTruckParams())
            {
                paramsDict.Add(truckParam, string.Empty);
            }

            return paramsDict;
        }

        public override void CheckIfVehicleDetailsInputIsLegal(Dictionary<string, string> i_VehicleParamsDict, out string o_InvalidParam, bool i_IsUserWantToInsertAllWheelsAtOnce)
        {
            base.CheckIfVehicleDetailsInputIsLegal(i_VehicleParamsDict, out o_InvalidParam, i_IsUserWantToInsertAllWheelsAtOnce);
            TruckInfo.CheckIfTruckDetailsInputIsLegal(i_VehicleParamsDict, out o_InvalidParam);
        }

        public override void UpdateVehicleParams(Dictionary<string, string> i_VehicleParamsDict)
        {
            base.UpdateVehicleParams(i_VehicleParamsDict);
            TruckInfo.UpdateTruckParams(i_VehicleParamsDict);
        }

        public override string ToString()
        {
            string fuelTruckDetails = base.ToString() + TruckInfo.ToString();

            return fuelTruckDetails;
        }
    }
}