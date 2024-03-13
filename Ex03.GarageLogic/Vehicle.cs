using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private const string NumOfFirstWheel = "1";
        private string m_ModleName;
        private readonly string r_LicenseNumber;
        private float m_RemaningEnergyPrecentage;
        private List<Wheel> m_VehicleWheels;
        private VehicleInGarageInfo m_VehicleInGarageInfo;

        public string ModleName
        {
            get
            {
                return m_ModleName;
            }
            set
            {
                m_ModleName = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public VehicleInGarageInfo VehicleInGarageInfo
        {
            get
            {
                return m_VehicleInGarageInfo;
            }
            set
            {
                m_VehicleInGarageInfo = value;
            }
        }

        protected float RemaningEnergyPrecentage
        {
            get
            {
                return m_RemaningEnergyPrecentage;
            }
            set
            {
                m_RemaningEnergyPrecentage = value;
            }
        }

        public List<Wheel> VehicleWheels
        {
            get
            {
                return m_VehicleWheels;
            }
            set
            {
                m_VehicleWheels = value;
            }
        }

        protected Vehicle(string i_LicenseNumber, int i_NumOfWheels, float i_MaxAirPressure)
        {
            m_ModleName = "";
            r_LicenseNumber = i_LicenseNumber;
            m_RemaningEnergyPrecentage = 0;
            m_VehicleInGarageInfo = new VehicleInGarageInfo();
            m_VehicleWheels = new List<Wheel>();
            createWheels(i_NumOfWheels, i_MaxAirPressure);
        }

        private void createWheels(int i_NumOfWheels, float i_MaxAirPressure)
        {
            while (i_NumOfWheels > 0)
            {
                VehicleWheels.Add(new Wheel(i_MaxAirPressure));
                i_NumOfWheels--;
            }
        }

        protected abstract void UpdateRemainigEnergyPrecentage();

        public abstract void FillEnergySource(float i_EnergyToFill);

        public abstract void CheckIfLegalEnergyAmount(float i_EnergyAmount);

        public virtual Dictionary<string, string> GetVehicleParams()
        {
            int wheelNum = 1;
            Dictionary<string, string> vehicleParamsDict = new Dictionary<string, string>()
            {
                {"ModleName", string.Empty}
            };

            foreach (string param in VehicleInGarageInfo.GetvehicleInGarageParams())
            {
                vehicleParamsDict.Add(param, string.Empty);
            }

            foreach (Wheel wheel in VehicleWheels)
            {
                foreach (string param in wheel.GetWheelParams())
                {
                    vehicleParamsDict.Add($"{param} Wheel {wheelNum}", string.Empty);
                }

                wheelNum += 1;
            }

            return vehicleParamsDict;
        }

        public virtual void CheckIfVehicleDetailsInputIsLegal(Dictionary<string, string> i_VehicleParamsDict, out string o_InvalidParam
            , bool i_IsUserWantToInsertAllWheelsAtOnce)
        {
            o_InvalidParam = string.Empty;
            VehicleInGarageInfo.CheckIfVehicleInGarageDetailsIsLegal(i_VehicleParamsDict, out o_InvalidParam);

            foreach ((Wheel wheel, int wheelNum) in VehicleWheels.Select((wheel, idx) => (wheel, idx)))
            {
                wheel.CheckIfWheelParamsLegal(i_VehicleParamsDict, out o_InvalidParam, i_IsUserWantToInsertAllWheelsAtOnce, wheelNum + 1);
            }
        }

        public virtual void UpdateVehicleParams(Dictionary<string, string> i_VehicleParamsDict)
        {
            ModleName = i_VehicleParamsDict["ModleName"];

            foreach ((Wheel wheel, int idx) in VehicleWheels.Select((wheel, idx) => (wheel, idx)))
            {
                foreach (string key in i_VehicleParamsDict.Keys)
                {
                    if (key.Contains("Wheel " + (idx + 1).ToString()))
                    {
                        if (key.Contains("CurrentAirPressure"))
                        {
                            continue;
                        }

                        if (i_VehicleParamsDict[key] != string.Empty)
                        {
                            string wheelNumStr = (idx + 1).ToString();
                            wheel.UpdateWheelParams(i_VehicleParamsDict, wheelNumStr);
                        }
                        else
                        {
                            wheel.UpdateWheelParams(i_VehicleParamsDict, NumOfFirstWheel);
                        }
                    }
                }
            }

            VehicleInGarageInfo.UpdateVehicleInGarageParams(i_VehicleParamsDict);
        }

        public override bool Equals(object obj)
        {
            bool equals = false;

            Vehicle toCompareTo = obj as Vehicle;
            if (toCompareTo != null)
            {
                equals = LicenseNumber == toCompareTo.LicenseNumber;
            }

            return equals;
        }

        public static bool operator ==(Vehicle obj1, Vehicle obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Vehicle obj1, Vehicle obj2)
        {
            return !(obj1 == obj2);
        }

        public override int GetHashCode()
        {
            return LicenseNumber.GetHashCode();
        }

        public override string ToString()
        {
            string wheelsDetailsStr = string.Empty;

            foreach ((Wheel wheelInList, int index) in VehicleWheels.Select((wheel, idx) => (wheel, idx)))
            {
                wheelsDetailsStr += string.Format(
@"Wheel {0}: {1}
", index + 1, wheelInList.ToString());
            }

            string vehicleDetailsStr = string.Format(
@"Modle name: {0}
License number: {1}
Remaning energy precentage: {2}%
", ModleName, LicenseNumber, RemaningEnergyPrecentage);

            return vehicleDetailsStr + VehicleInGarageInfo.ToString() + wheelsDetailsStr;
        }
    }
}