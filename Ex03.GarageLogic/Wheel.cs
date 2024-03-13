using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const float MinWheelAirPressure = 0;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
            set
            {
                m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        internal void InflateAWheel()
        {
            CurrentAirPressure = MaxAirPressure;
        }

        public List<string> GetWheelParams()
        {
            List<string> wheelParamsList = new List<string>() { "ManufacturerName", "CurrentAirPressure" };

            return wheelParamsList;
        }

        public void CheckIfWheelParamsLegal(Dictionary<string, string> i_VehicleParamsDict, out string o_InvalidParam
            , bool i_IsUserWantToInsertAllWheelsAtOnce, int i_WheelNum)
        {
            bool isWheel1, isCurrentAirPressureAFloat;
            string currentAirPressure;

            o_InvalidParam = string.Empty;
            foreach ((string key, int idx) in i_VehicleParamsDict.Select((keyValParam, index) => (keyValParam.Key, index + 1)))
            {
                if (key.EndsWith("CurrentAirPressure Wheel " + i_WheelNum.ToString()))
                {
                    currentAirPressure = i_VehicleParamsDict[key];
                    isCurrentAirPressureAFloat = float.TryParse(currentAirPressure, out float result);
                    isWheel1 = i_WheelNum == 1;
                    if (!isCurrentAirPressureAFloat && (isWheel1 || (!isWheel1 && !i_IsUserWantToInsertAllWheelsAtOnce)))
                    {
                        o_InvalidParam = key;
                        throw new FormatException();
                    }
                    else if (i_IsUserWantToInsertAllWheelsAtOnce && !isWheel1)
                    {
                        continue;
                    }
                    else if (float.Parse(currentAirPressure) < MinWheelAirPressure || float.Parse(currentAirPressure) > MaxAirPressure)
                    {
                        o_InvalidParam = key;
                        throw new ValueOutOfRangeException(MinWheelAirPressure, MaxAirPressure);
                    }
                }
            }
        }

        public void UpdateWheelParams(Dictionary<string, string> i_VehicleParamsDict, string i_WheelNumber)
        {
            ManufacturerName = i_VehicleParamsDict["ManufacturerName Wheel " + i_WheelNumber];
            CurrentAirPressure = float.Parse(i_VehicleParamsDict["CurrentAirPressure Wheel " + i_WheelNumber]);
        }

        public override string ToString()
        {
            return string.Format(
@"Manufacturer name: {0}  Current air pressure: {1}"
, ManufacturerName, CurrentAirPressure);
        }
    }
}