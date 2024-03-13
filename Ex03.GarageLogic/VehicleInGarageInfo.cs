using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class VehicleInGarageInfo
    {
        private const int PhoneNumberLen = 10;
        private string m_OwnerName;
        private string m_PhoneNumber;
        private eVehicleStatusType m_VehicleStatus = eVehicleStatusType.UnderRepair;

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
            set
            {
                m_OwnerName = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return m_PhoneNumber;
            }
            set
            {
                m_PhoneNumber = value;
            }
        }

        public eVehicleStatusType VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }
            set
            {
                m_VehicleStatus = value;
            }
        }

        public VehicleInGarageInfo()
        {
            OwnerName = string.Empty;
            PhoneNumber = string.Empty;
            VehicleStatus = eVehicleStatusType.UnderRepair;
        }

        public static void CheckIfLegalVehicleStatus(int i_UserInput)
        {
            if (!Enum.IsDefined(typeof(eVehicleStatusType), i_UserInput))
            {
                throw new ArgumentException();
            }
        }

        public List<string> GetvehicleInGarageParams()
        {
            List<string> vehicleInGarageParamsList = new List<string>() { "OwnerName", "PhoneNumber" };

            return vehicleInGarageParamsList;
        }

        public void CheckIfVehicleInGarageDetailsIsLegal(Dictionary<string, string> i_VehicleParamsDict, out string o_InvalidParam)
        {
            string phoneNumberStr = i_VehicleParamsDict["PhoneNumber"];

            o_InvalidParam = string.Empty;
            if (!(phoneNumberStr.All(char.IsDigit) && phoneNumberStr.Length == PhoneNumberLen))
            {
                o_InvalidParam = "PhoneNumber";
                throw new FormatException();
            }
        }

        public void UpdateVehicleInGarageParams(Dictionary<string, string> i_VehicleParamsDict)
        {
            OwnerName = i_VehicleParamsDict["OwnerName"];
            PhoneNumber = i_VehicleParamsDict["PhoneNumber"];
        }

        public override string ToString()
        {
            return string.Format(
@"Owner name: {0}
Phone number: {1}
Status: {2}
", OwnerName, PhoneNumber, VehicleStatus.ToString());
        }
    }
}