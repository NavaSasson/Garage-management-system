using Ex03.GarageLogic;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    internal class UserInterface
    {
        public void CreateNewGarage()
        {
            Garage garage = new Garage();

            start(ref garage);
        }

        private void start(ref Garage io_Garage)
        {
            string userMenuInput = string.Empty;
            bool isLegalMenuOptionInput = false;

            Console.WriteLine(
@"Hello!
Choose the action you want to do:
1.Add a vehicle to the garage
2.See list of vehicles in the garage (according to a specific status)
3.Change vehicle status
4.Inflate wheels to max pressure
5.Refuel vehicle
6.Charge electric vehicle
7.See vehicle details");
            while (!isLegalMenuOptionInput)
            {
                try
                {
                    userMenuInput = Console.ReadLine();
                    checkIfIntParsingSucceeded(userMenuInput);
                    isLegalMenuOptionInput = checkIfLegalMenuInput(int.Parse(userMenuInput));
                    if (!isLegalMenuOptionInput)
                    {
                        Console.WriteLine("This number is not from the menu options, pls try again");
                    }
                }
                catch
                {
                    Console.WriteLine("This input is not from the menu options, pls try again");
                }
            }

            int userIntegerInput = int.Parse(userMenuInput);
            activatefunctionAccordingToSelectedMenuOption((eMenuType)userIntegerInput, ref io_Garage);
        }

        private void goBackToMenuOrExit(ref Garage io_Garage)
        {
            Console.WriteLine(
@"
To the menu press 1
To exit press any other key");
            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                Console.Clear();
                start(ref io_Garage);
            }
        }

        private void activatefunctionAccordingToSelectedMenuOption(eMenuType i_MenuOption, ref Garage io_Garage)
        {
            switch (i_MenuOption)
            {
                case eMenuType.AddVehicle:
                    addVehicleToGarage(ref io_Garage);
                    break;
                case eMenuType.ShowList:
                    showLicenseNumbersListOfVehiclesInGarage(io_Garage);
                    break;
                case eMenuType.ChangeStatus:
                    changeVehicleStatusInGarage(ref io_Garage);
                    break;
                case eMenuType.InflateWheelsToMax:
                    inflateWheelsToMaxPressure(ref io_Garage);
                    break;
                case eMenuType.RefuleVehicle:
                    refuelVehiclePoweredByFuel(ref io_Garage);
                    break;
                case eMenuType.ChargeVehicle:
                    fillVehicleBattery(ref io_Garage);
                    break;
                case eMenuType.SeeDetails:
                    printVehicleInGarageDetails(io_Garage);
                    break;
            }

            goBackToMenuOrExit(ref io_Garage);
        }

        private static bool checkIfLegalMenuInput(int i_UserInput)
        {
            return Enum.IsDefined(typeof(eMenuType), i_UserInput);
        }

        private void addVehicleToGarage(ref Garage io_Garage)
        {
            Console.WriteLine("Enter license number");
            string licenseNumberInput = Console.ReadLine();
            int vehicleIdx;
            try
            {
                vehicleIdx = io_Garage.CheckIfVehicleInGarageAndfindItIdx(licenseNumberInput);
                io_Garage.ChangeVehicleStatus(vehicleIdx, (int)eVehicleStatusType.UnderRepair);
                Console.WriteLine("This vehicle is allready in the garage, it's status changed to UnderRepair");
            }
            catch
            {
                addNewVehicleToGarage(ref io_Garage, licenseNumberInput);
            }
        }

        private void addNewVehicleToGarage(ref Garage io_Garage, string i_LicenseNumber)
        {
            Dictionary<string, string> vehicleDetailsDict = new Dictionary<string, string>();

            Console.WriteLine("Choose which type of vehicle you want to enter to the garage:");
            printEnumOptions<eVehicleType>();
            getDetailsForNewVehicleInGarage(io_Garage, i_LicenseNumber, ref vehicleDetailsDict);
            io_Garage.VehiclesList[io_Garage.VehiclesList.Count - 1].UpdateVehicleParams(vehicleDetailsDict);
        }

        private void getDetailsForNewVehicleInGarage(Garage i_Garage, string i_LicenseNumber, ref Dictionary<string, string> io_VehicleDetailsDict)
        {
            try
            {
                string userVehicleTypeInput = Console.ReadLine();
                checkIfIntParsingSucceeded(userVehicleTypeInput);
                try
                {
                    int vehicleType = int.Parse(userVehicleTypeInput);
                    i_Garage.CheckIfLegalVehicleType(vehicleType);
                    Vehicle vehicle = Factory.CreateVehicleAccordingToUserInput(vehicleType, i_LicenseNumber);
                    i_Garage.AddVehicleToGarage(vehicle);
                    int lastIdxInVehicleList = i_Garage.VehiclesList.Count - 1;
                    getValidDetailsForNewVehicle(i_Garage, lastIdxInVehicleList, ref io_VehicleDetailsDict);
                }
                catch
                {
                    Console.WriteLine("Invalid vehicle type, please try again");
                    getDetailsForNewVehicleInGarage(i_Garage, i_LicenseNumber, ref io_VehicleDetailsDict);
                }
            }
            catch
            {
                Console.WriteLine("Invalid input, please try again");
                getDetailsForNewVehicleInGarage(i_Garage, i_LicenseNumber, ref io_VehicleDetailsDict);
            }
        }

        private void getValidDetailsForNewVehicle(Garage i_Garage, int i_VehicleIdxInList, ref Dictionary<string, string> io_VehicleDetailsDict)
        {
            string vehicleError = string.Empty;

            io_VehicleDetailsDict = i_Garage.VehiclesList[i_VehicleIdxInList].GetVehicleParams();
            List<string> keysList = new List<string>(io_VehicleDetailsDict.Keys);
            Console.WriteLine("Enter the following details about the vehicle");
            Console.WriteLine(string.Format(
@"Would you like the details you enter for the first wheel to be updated for all other wheels?
Press 1 for Yes
Otherwise press any other key"));
            string userAnsAboutWheels = Console.ReadLine();
            bool isUserWantToInsertAllWheelsAtOnce = userAnsAboutWheels == "1";
            foreach (string key in keysList)
            {
                if (isUserWantToInsertAllWheelsAtOnce && !key.EndsWith("Wheel 1") && key.Contains("Wheel"))
                {
                    continue;
                }
                else
                {
                    Console.WriteLine("Enter value for " + key);
                    string userInputValue = Console.ReadLine();
                    io_VehicleDetailsDict[key] = userInputValue;
                }
            }

            try
            {
                i_Garage.VehiclesList[i_VehicleIdxInList].CheckIfVehicleDetailsInputIsLegal(io_VehicleDetailsDict, out vehicleError, isUserWantToInsertAllWheelsAtOnce);
            }
            catch
            {
                Console.WriteLine($"Invalid {vehicleError}");
                getValidDetailsForNewVehicle(i_Garage, i_VehicleIdxInList, ref io_VehicleDetailsDict);
            }
        }

        private void showLicenseNumbersListOfVehiclesInGarage(Garage i_Garage)
        {
            Console.WriteLine(
@"Would you like to filter the list of vehicles according to a specific status?
If you want to see the full list press 1
otherwise press any other key");
            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                printLicenseNumberFromVehicleList(i_Garage.VehiclesList);
            }
            else
            {
                string vehicleStatus = getValidVehicleStatusInput();
                Console.WriteLine("Here are all the license numbers of the vehicles with the status you have chosen:");
                List<Vehicle> listOfVehiclesWithSameStatus = i_Garage.CreateListOfVehiclesWithSameStatusInGarage(int.Parse(vehicleStatus));
                printLicenseNumberFromVehicleList(listOfVehiclesWithSameStatus);
            }
        }

        private void inflateWheelsToMaxPressure(ref Garage io_Garage)
        {
            int vehicleIdxInGarageList = getValidLicenseNumAndFindItsIdx(io_Garage);
            io_Garage.InflateAllWheelsToMaxPressure(vehicleIdxInGarageList);
            Console.WriteLine("The wheels were inflated to the maximum successfully");
        }

        private int getValidLicenseNumAndFindItsIdx(Garage i_Garage)
        {
            Console.WriteLine("Enter license number");
            string licenseNumberInput = Console.ReadLine();
            int vehicleIdx;
            try
            {
                vehicleIdx = i_Garage.CheckIfVehicleInGarageAndfindItIdx(licenseNumberInput);
            }
            catch
            {
                Console.WriteLine("This license number is not in the garage, pls try again");
                vehicleIdx = getValidLicenseNumAndFindItsIdx(i_Garage);
            }

            return vehicleIdx;
        }

        private void changeVehicleStatusInGarage(ref Garage io_Garage)
        {
            int vehicleIdxInGarageList = getValidLicenseNumAndFindItsIdx(io_Garage);
            string vehicleStatusStr = getValidVehicleStatusInput();
            io_Garage.ChangeVehicleStatus(vehicleIdxInGarageList, int.Parse(vehicleStatusStr));
            Console.WriteLine("Status changed successfully");
        }

        private static string getValidVehicleStatusInput()
        {
            string vehicleStatusInput = string.Empty;
            bool isLegalVehicleStatus = false;

            Console.WriteLine("Enter the status you want");
            printEnumOptions<eVehicleStatusType>();
            while (!isLegalVehicleStatus)
            {
                try
                {
                    vehicleStatusInput = Console.ReadLine();
                    checkIfIntParsingSucceeded(vehicleStatusInput);
                    try
                    {
                        VehicleInGarageInfo.CheckIfLegalVehicleStatus(int.Parse(vehicleStatusInput));
                        isLegalVehicleStatus = true;
                    }
                    catch
                    {
                        Console.WriteLine("This number is not in the options, pls try again");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input, pls try again");
                }
            }

            return vehicleStatusInput;
        }

        private static void checkIfFloatParsingSucceeded(string i_Input)
        {
            bool isParsingSucceeded = float.TryParse(i_Input, out float result);

            throwFormatExceptionIfNeeded(isParsingSucceeded);
        }

        private static void checkIfIntParsingSucceeded(string i_Input)
        {
            bool isParsingSucceeded = int.TryParse(i_Input, out int result);

            throwFormatExceptionIfNeeded(isParsingSucceeded);
        }

        private static void throwFormatExceptionIfNeeded(bool i_IsParsingSucceeded)
        {
            if (!i_IsParsingSucceeded)
            {
                throw new FormatException();
            }
        }

        private static void printLicenseNumberFromVehicleList(List<Vehicle> i_ListToPrint)
        {
            foreach (Vehicle vehicleInGarage in i_ListToPrint)
            {
                Console.WriteLine(vehicleInGarage.LicenseNumber);
            }
        }

        private void refuelVehiclePoweredByFuel(ref Garage io_Garage)
        {
            int vehicleIdxInGarageList = getValidLicenseNumAndFindItsIdx(io_Garage);
            try
            {
                io_Garage.CheckIfVehicleIsFuelVehicle(vehicleIdxInGarageList);
                int fuelType = getValidFuelType(io_Garage, vehicleIdxInGarageList);
                float fuelAmount = getValidEnergyAmount(io_Garage, vehicleIdxInGarageList);
                io_Garage.VehiclesList[vehicleIdxInGarageList].FillEnergySource(fuelAmount);
                Console.WriteLine("The fuel is filled successfully");
            }
            catch
            {
                Console.WriteLine("The vehicle you chose is not powered by fuel, pls enter again");
                refuelVehiclePoweredByFuel(ref io_Garage);
            }
        }

        private void fillVehicleBattery(ref Garage io_Garage)
        {
            int vehicleIdxInGarageList = getValidLicenseNumAndFindItsIdx(io_Garage);
            try
            {
                io_Garage.CheckIfVehicleIsElectricVehicle(vehicleIdxInGarageList);
                float fuelAmount = getValidEnergyAmount(io_Garage, vehicleIdxInGarageList);
                io_Garage.VehiclesList[vehicleIdxInGarageList].FillEnergySource(fuelAmount);
                Console.WriteLine("The battery has been charged successfully");
            }
            catch
            {
                Console.WriteLine("The vehicle you chose is not electric, pls enter again");
                fillVehicleBattery(ref io_Garage);
            }
        }

        private float getValidEnergyAmount(Garage i_Garage, int i_indexOfVehicleInList)
        {
            Console.WriteLine("Enter the amount of energy you want to fill");
            string energyAmountStr = Console.ReadLine();
            float energyAmount;
            try
            {
                checkIfFloatParsingSucceeded(energyAmountStr);
                try
                {
                    energyAmount = float.Parse(energyAmountStr);
                    i_Garage.VehiclesList[i_indexOfVehicleInList].CheckIfLegalEnergyAmount(energyAmount);
                }
                catch
                {
                    Console.WriteLine("Invalid energy amount, pls enter again");
                    energyAmount = getValidEnergyAmount(i_Garage, i_indexOfVehicleInList);
                }
            }
            catch
            {
                Console.WriteLine("Invalid input, pls try again");
                energyAmount = getValidEnergyAmount(i_Garage, i_indexOfVehicleInList);
            }

            return energyAmount;
        }

        private static void printEnumOptions<T>() where T : Enum
        {
            int idx = 1;

            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"{enumValue} press {idx}");
                idx += 1;
            }
        }

        private int getValidFuelType(Garage i_Garage, int i_indexOfVehicleInList)
        {
            Console.WriteLine("Enter fuel Type:");
            printEnumOptions<eFuelType>();
            string fuelTypeStr = Console.ReadLine();
            int fuelTypeInt;
            try
            {
                checkIfIntParsingSucceeded(fuelTypeStr);
                try
                {
                    fuelTypeInt = int.Parse(fuelTypeStr);
                    FuelVehicle fuelVehicle = (FuelVehicle)i_Garage.VehiclesList[i_indexOfVehicleInList];
                    fuelVehicle.CheckIfLegalFuelType(fuelTypeInt);
                }
                catch
                {
                    Console.WriteLine("Invalid fuel type, pls enter again");
                    fuelTypeInt = getValidFuelType(i_Garage, i_indexOfVehicleInList);
                }
            }
            catch
            {
                Console.WriteLine("Invalid input, pls try again");
                fuelTypeInt = getValidFuelType(i_Garage, i_indexOfVehicleInList);
            }

            return fuelTypeInt;
        }

        private void printVehicleInGarageDetails(Garage i_Garage)
        {
            int vehicleIdxInGarageList = getValidLicenseNumAndFindItsIdx(i_Garage);
            string vehicleDetails = i_Garage.VehiclesList[vehicleIdxInGarageList].ToString();
            Console.WriteLine(vehicleDetails);
        }
    }
}