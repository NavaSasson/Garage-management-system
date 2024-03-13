using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class MotorcycleInfo
    {
        internal const int NumOfWheelsInMotorcycle = 2;
        internal const float MaxAirPressureInMotorcycle = 29;
        private eMotorcycleLicenseType m_LicenseType;
        private int m_EngineCapacityCC;

        public eMotorcycleLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineCapacityCC
        {
            get
            {
                return m_EngineCapacityCC;
            }
            set
            {
                m_EngineCapacityCC = value;
            }
        }

        public MotorcycleInfo()
        {
            m_LicenseType = eMotorcycleLicenseType.A1;
            m_EngineCapacityCC = 0;
        }

        public List<string> GetMotorcycleParams()
        {
            List<string> motorcycleParamsList = new List<string>() { "LicenseType", "EngineCapacityCC" };

            return motorcycleParamsList;
        }

        public void CheckIfMotorcycleDetailsInputIsLegal(Dictionary<string, string> i_VehicleParamsDict, out string o_InvalidParam)
        {
            o_InvalidParam = string.Empty;
            string engineCapacityCC = i_VehicleParamsDict["EngineCapacityCC"];
            if (!int.TryParse(engineCapacityCC, out int result))
            {
                o_InvalidParam = "EngineCapacityCC";
                throw new FormatException();
            }

            string licenseType = i_VehicleParamsDict["LicenseType"];
            if (!Enum.TryParse(licenseType, out eMotorcycleLicenseType license))
            {
                o_InvalidParam = "LicenseType";
                throw new ArgumentException();
            }
        }

        public void UpdateMotorcycleParams(Dictionary<string, string> i_VehicleParamsDict)
        {
            LicenseType = (eMotorcycleLicenseType)Enum.Parse(typeof(eMotorcycleLicenseType), i_VehicleParamsDict["LicenseType"]);
            EngineCapacityCC = int.Parse(i_VehicleParamsDict["EngineCapacityCC"]);
        }

        public override string ToString()
        {
            return string.Format(
@"License type: {0}
Engine capacity in cc: {1}"
, LicenseType.ToString(), EngineCapacityCC);
        }
    }
}