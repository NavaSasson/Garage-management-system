using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class TruckInfo
    {
        internal const int NumOfWheelsInTruck = 12;
        internal const float MaxAirPressureInTruck = 28;
        private eIsTransportDangerousMaterialsType m_IsTransportingDangerousMaterials;
        private float m_CargoVolume;

        public eIsTransportDangerousMaterialsType IsTransportingDangerousMaterials
        {
            get
            {
                return m_IsTransportingDangerousMaterials;
            }
            set
            {
                m_IsTransportingDangerousMaterials = value;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
            set
            {
                m_CargoVolume = value;
            }
        }

        public TruckInfo()
        {
            m_IsTransportingDangerousMaterials = eIsTransportDangerousMaterialsType.no;
            m_CargoVolume = 0;
        }

        public List<string> GetTruckParams()
        {
            List<string> truckParamsList = new List<string>() { "IsTransportingDangerousMaterials", "CargoVolume" };

            return truckParamsList;
        }

        public void CheckIfTruckDetailsInputIsLegal(Dictionary<string, string> i_VehicleParamsDict, out string o_InvalidParam)
        {
            o_InvalidParam = string.Empty;
            string isTransportingDangerousMaterials = i_VehicleParamsDict["IsTransportingDangerousMaterials"].ToLower();
            if (!Enum.TryParse(isTransportingDangerousMaterials, out eIsTransportDangerousMaterialsType transporting))
            {
                o_InvalidParam = "IsTransportingDangerousMaterials";
                throw new ArgumentException();
            }

            string cargoVolume = i_VehicleParamsDict["CargoVolume"];
            if (!float.TryParse(cargoVolume, out float result))
            {
                o_InvalidParam = "CargoVolume";
                throw new FormatException();
            }
        }

        public void UpdateTruckParams(Dictionary<string, string> i_VehicleParamsDict)
        {
            IsTransportingDangerousMaterials = (eIsTransportDangerousMaterialsType)Enum.Parse(typeof(eIsTransportDangerousMaterialsType), i_VehicleParamsDict["IsTransportingDangerousMaterials"].ToLower());
            CargoVolume = float.Parse(i_VehicleParamsDict["CargoVolume"]);
        }

        public override string ToString()
        {
            return string.Format(
@"Is transporting dangerous materials: {0}
Cargo volume: {1}"
, IsTransportingDangerousMaterials, CargoVolume);
        }
    }
}