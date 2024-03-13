using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class CarInfo
    {
        internal const int NumOfWheelsInCar = 5;
        internal const int MaxAirPressureInCar = 30;
        private eNumOfDoorsType m_NumOfDoorsInCar;
        private eCarColorType m_CarColor;

        public eNumOfDoorsType NumOfDoorsInCar
        {
            get
            {
                return m_NumOfDoorsInCar;
            }
            set
            {
                m_NumOfDoorsInCar = value;
            }
        }

        public eCarColorType CarColor
        {
            get
            {
                return m_CarColor;
            }
            set
            {
                m_CarColor = value;
            }
        }

        public CarInfo()
        {
            m_NumOfDoorsInCar = eNumOfDoorsType.Two;
            m_CarColor = default;
        }

        public List<string> GetCarParams()
        {
            List<string> carParamsList = new List<string>() { "NumOfDoorsInCar", "CarColor" };

            return carParamsList;
        }

        public void CheckIfCarDetailsInputIsLegal(Dictionary<string, string> i_VehicleParamsDict, out string o_InvalidParam)
        {
            o_InvalidParam = string.Empty;
            string numOfDoors = i_VehicleParamsDict["NumOfDoorsInCar"];
            if (!int.TryParse(numOfDoors, out int result))
            {
                o_InvalidParam = "NumOfDoorsInCar";
                throw new FormatException();
            }
            else if (!Enum.IsDefined(typeof(eNumOfDoorsType), int.Parse(numOfDoors)))
            {
                o_InvalidParam = "NumOfDoorsInCar";
                throw new ArgumentException();
            }

            string carColor = i_VehicleParamsDict["CarColor"].ToLower();
            if (!Enum.TryParse(carColor, out eCarColorType color))
            {
                o_InvalidParam = "CarColor";
                throw new ArgumentException();
            }
        }

        public void UpdateCarParams(Dictionary<string, string> i_VehicleParamsDict)
        {
            NumOfDoorsInCar = (eNumOfDoorsType)int.Parse(i_VehicleParamsDict["NumOfDoorsInCar"]);
            CarColor = (eCarColorType)Enum.Parse(typeof(eCarColorType), i_VehicleParamsDict["CarColor"].ToLower());
        }

        public override string ToString()
        {
            return string.Format(
@"Number of doors: {0}
Car's color: {1}"
, NumOfDoorsInCar.ToString(), CarColor.ToString());
        }
    }
}