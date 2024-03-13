namespace Ex03.GarageLogic
{
    public static class Factory
    {
        public static Vehicle CreateVehicleAccordingToUserInput(int i_VehicleType, string i_LicenseNumberInput)
        {
            eVehicleType vehicleType = (eVehicleType)i_VehicleType;
            Vehicle vehicle = null;
            switch (vehicleType)
            {
                case eVehicleType.FuelMotorcycle:
                    vehicle = new FuelMotorcycle(i_LicenseNumberInput);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = new ElectricMotorcycle(i_LicenseNumberInput);
                    break;
                case eVehicleType.FuelCar:
                    vehicle = new FuelCar(i_LicenseNumberInput);
                    break;
                case eVehicleType.ElectricCar:
                    vehicle = new ElectricCar(i_LicenseNumberInput);
                    break;
                case eVehicleType.FuelTruck:
                    vehicle = new FuelTruck(i_LicenseNumberInput);
                    break;
            }

            return vehicle;
        }
    }
}