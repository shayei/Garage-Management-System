using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UIHandler
    {
        public Garage m_Garage;
        private static bool s_ExitProgram = false;
        public const int k_Zero = 0;
        public const int k_One = 1;
        public const int k_Two = 2;
        public const int k_Four = 4;
        public const int k_Five = 5;
        public const int k_TypesOfVehicles = 5;
        public const float k_MinutsInHour = 60f;
        public const int k_NumberOfMenuSelections = 8;
        public const int k_NumberOfStatusSelections = 3;

        public UIHandler()
        {
            m_Garage = new Garage();
        }

        public enum eChoice
        {
            AddVehicle = 1,
            ShowVehicleList,
            ChangeStatus,
            InflatingTire,
            FuelingVehicle,
            ChargeVehicle,
            FullDataOfVehicle,
            Exit
        }

        public enum eCarType
        {
            GasolineCar = 1,
            ElectiricCar,
            GasolineMotorbike,
            ElectricMotorbike,
            Truck
        }

        public static string GetNameInput()
        {
            bool isLegalInput = false;
            string nameInput;
            do
            {
                nameInput = Console.ReadLine();
                if (nameInput.Length > k_Zero)
                {
                    isLegalInput = true;
                }
                else
                {
                    WrongInput();
                }
            }
            while (!isLegalInput);
            return nameInput;
        }

        public static void ElectricalBatterytInput(ref float io_Current, ref float io_Maximum)
        {
            bool checkCurrent = false;
            Console.WriteLine("please enter maximum battery life in hours");
            io_Maximum = GetLegalNumber();
            Console.WriteLine("please enter current fuel in liters:");
            do
            {
                io_Current = GetLegalNumber();
                if (io_Current > io_Maximum)
                {
                    Console.WriteLine("invalid current battery,please enter again");
                }
                else
                {
                    checkCurrent = true;
                }
            }
            while (!checkCurrent);
        }

        public static void FuelInput(ref float io_Current, ref float io_Maximum)
        {
            bool checkCurrent = false;
            Console.WriteLine("please enter maximum fuel in liters:");
            io_Maximum = GetLegalNumber();
            Console.WriteLine("please enter current fuel in liters:");
            do
            {
                io_Current = GetLegalNumber();
                checkCurrent = io_Current <= io_Maximum;

                if (!checkCurrent)
                {
                    Console.WriteLine("invalid current fuel,please enter again");
                }
            }
            while (!checkCurrent);
        }

        public static float GetLegalNumber()
        {
            float input = 0;
            bool isLegalNumber = false;
            do
            {
                isLegalNumber = float.TryParse(Console.ReadLine(), out input);

                if (!isLegalNumber || input < k_Zero)
                {
                    WrongInput();
                }
            }
            while (!isLegalNumber);

            return input;
        }

        public static void WrongInput()
        {
            Console.WriteLine("Wrong input, try again.");
        }

        public static bool CheckInput(int i_Left, int i_Right, int i_Input)
        {
            bool input = false;
            if (i_Input >= i_Left && i_Input <= i_Right)
            {
                input = true;
            }

            return input;
        }

        public static bool CheckIfInputIsANumber(string input)
        {
            bool result = false;
            long number;
            result = long.TryParse(input, out number);
            return result;
        }

        public static void Menu()
        {
            string menu = string.Format(
@"---------------------------------------------------------
Choose what you want to do (enter the number):
1)Put a car in the garage
2)Show list of vehicle license numbers in the garage
3)Change the condition of a vehicle in a garage
4)Inflating the air in the wheels of a vehicle
5)Fueling vehicles powered by fuel
6)Charge an electric vehicle
7)View full data of a vehicle by license number
8)Exit
---------------------------------------------------------");
            Console.WriteLine(menu);
        }

        public void EnterVehicleToTheGarage()
        {
            string licenseNumber;
            bool isLegalInput = false;
            bool isCarExist = false;
            Console.WriteLine("Insert vehicle license number:");
            do
            {
                licenseNumber = Console.ReadLine();
                isLegalInput = CheckIfInputIsANumber(licenseNumber);
                if (!isLegalInput)
                {
                    WrongInput();
                }
            }
            while (!isLegalInput);

            int index = 0;
            if (m_Garage.m_VehicleList.Count > k_Zero)
            {
                isCarExist = m_Garage.CheckIfCarAlreadyInTheGarage(licenseNumber, ref index);
            }

            if (isCarExist)
            {
                Console.WriteLine("The vehicle is already in the garage, and has been moved to repair.");
                m_Garage.m_VehicleList[index].Status = Vehicle.eStatus.InRepair;
            }
            else
            {
                ClientInput(licenseNumber);
            }
        }

        public void ClientInput(string licenseNumber)
        {
            bool isLegalInput = false;
            string phoneNumberInput;
            string nameInput;
            Console.WriteLine("please enter your name:");
            nameInput = GetNameInput();
            Console.WriteLine("please enter your phone number:");
            do
            {
                phoneNumberInput = Console.ReadLine();
                isLegalInput = CheckIfInputIsANumber(phoneNumberInput);
                if (!isLegalInput)
                {
                    WrongInput();
                }
            }
            while (!isLegalInput);

            Client newClient = new Client(nameInput, phoneNumberInput);
            string msg = string.Format(
@"Select what type of vehicle you have (enter the number selection) :
1- gasoline car       2- electiric car       3- gasoline motorbike       4-electric motorbike       5- truck");
            Console.WriteLine(msg);
            int.TryParse(Console.ReadLine(), out int vehicleSelect);
            bool validInput = CheckInput(k_One, k_TypesOfVehicles, vehicleSelect);
            while (!validInput)
            {
                WrongInput();
                int.TryParse(Console.ReadLine(), out vehicleSelect);
                validInput = CheckInput(k_One, k_TypesOfVehicles, vehicleSelect);
            }

            Console.WriteLine("please enter your vehicle model");
            string vehicleModel = GetNameInput();
            switch (vehicleSelect)
            {
                case (int)eCarType.GasolineCar:
                    Vehicle newCar = FuelCarInput(licenseNumber, vehicleModel);
                    Tire fuelCarTire = TireInput(31f);
                    newCar.UpdateTireList(fuelCarTire, 4);
                    newCar.ClientDetails = newClient;
                    m_Garage.AddToVehicleList(newCar);
                    break;
                case (int)eCarType.ElectiricCar:
                    Vehicle newElectricCar = ElectricCarInput(licenseNumber, vehicleModel);
                    Tire electricCarTire = TireInput(31);
                    newElectricCar.UpdateTireList(electricCarTire, 4);
                    newElectricCar.ClientDetails = newClient;
                    m_Garage.AddToVehicleList(newElectricCar);
                    break;
                case (int)eCarType.GasolineMotorbike:
                    Vehicle newMotorBike = FuelMotorBikeInput(licenseNumber, vehicleModel);
                    Tire fuelMotorBikeTire = TireInput(33);
                    newMotorBike.UpdateTireList(fuelMotorBikeTire, 2);
                    newMotorBike.ClientDetails = newClient;
                    m_Garage.AddToVehicleList(newMotorBike);
                    break;
                case (int)eCarType.ElectricMotorbike:
                    Vehicle newElectricMotorBike = ElectricMotorBikeInput(licenseNumber, vehicleModel);
                    Tire electricMotorBikeTire = TireInput(33);
                    newElectricMotorBike.UpdateTireList(electricMotorBikeTire, 2);
                    newElectricMotorBike.ClientDetails = newClient;
                    m_Garage.AddToVehicleList(newElectricMotorBike);
                    break;
                case (int)eCarType.Truck:
                    Vehicle newTruck = TruckInput(licenseNumber, vehicleModel);
                    Tire truckTire = TireInput(26);
                    newTruck.UpdateTireList(truckTire, 12);
                    newTruck.ClientDetails = newClient;
                    m_Garage.AddToVehicleList(newTruck);
                    break;
            }
        }

        private static Tire TireInput(float i_maxAirPressure)
        {
            Tire tire = new Tire();
            bool isNumber = false;
            bool validInput = false;
            string input;
            float i_currentAirPressure = 0;
            Console.WriteLine("please enter the manufactor name of the tire");
            string i_tireManufactor = GetNameInput();
            Console.WriteLine("please your current air pressure");
            while (!isNumber || !validInput)
            {
                input = Console.ReadLine();
                isNumber = float.TryParse(input, out i_currentAirPressure);
                if (!isNumber)
                {
                    WrongInput();
                }
                else if (i_currentAirPressure > i_maxAirPressure)
                {
                    Console.WriteLine("your current air prresure is higher than the max pressure,please try again");
                }
                else
                {
                    validInput = true;
                    isNumber = true;
                }
            }

            tire.CurrentAirPressure = i_currentAirPressure;
            tire.ManufactorName = i_tireManufactor;
            tire.MaxAirPressure = i_maxAirPressure;
            return tire;
        }

        private static Truck TruckInput(string licenseNumber, string vehicleModel)
        {
            float currentFuel = 0, maxFuel = 0;
            FuelInput(ref currentFuel, ref maxFuel);
            bool hazardousMatrial = HazardousMaterialsInput();
            Console.WriteLine("please enter your volume of cargo");
            float o_VolumeOfCargo = GetLegalNumber();
            Truck NewMotorBike = new Truck(vehicleModel, licenseNumber, currentFuel, maxFuel, FuelVehicle.eFuelType.Soler, hazardousMatrial, o_VolumeOfCargo);
            return NewMotorBike;
        }

        private static ElectricMotorBike ElectricMotorBikeInput(string licenseNumber, string vehicleModel)
        {
            float currentBattery = 0, maxBattery = 0;
            ElectricalBatterytInput(ref currentBattery, ref maxBattery);
            int licenseType = LicenseTypeInput();
            ElectricMotorBike.eLicenseType license = (ElectricMotorBike.eLicenseType)licenseType;
            Console.WriteLine("please enter your engine capacity");
            int o_EngineCapcity = (int)GetLegalNumber();
            ElectricMotorBike NewMotorBike = new ElectricMotorBike(vehicleModel, licenseNumber, currentBattery, maxBattery, o_EngineCapcity, license);
            return NewMotorBike;
        }

        private static ElectricCar ElectricCarInput(string licenseNumber, string vehicleModel)
        {
            float currentBattery = 0, maxBattery = 0;
            ElectricalBatterytInput(ref currentBattery, ref maxBattery);
            int doorsInput = NumOfDoorInput();
            ElectricCar.eNumOfDoors numOfDoors = (ElectricCar.eNumOfDoors)doorsInput;
            int colorCar = ColorInput();
            ElectricCar.eColorType colorOfCar = (ElectricCar.eColorType)colorCar;
            ElectricCar NewElectricCar = new ElectricCar(vehicleModel, licenseNumber, currentBattery, maxBattery, numOfDoors, colorOfCar);
            return NewElectricCar;
        }

        private static FuelMotorBike FuelMotorBikeInput(string licenseNumber, string vehicleModel)
        {
            float currentFuel = 0, maxFuel = 0;
            FuelInput(ref currentFuel, ref maxFuel);
            int licenseType = LicenseTypeInput();
            FuelMotorBike.eLicenseType license = (FuelMotorBike.eLicenseType)licenseType;
            Console.WriteLine("please enter your engine capacity");
            int o_EngineCapcity = (int)GetLegalNumber();
            FuelMotorBike NewMotorBike = new FuelMotorBike(vehicleModel, licenseNumber, currentFuel, maxFuel, FuelVehicle.eFuelType.Octan95, o_EngineCapcity, license);
            return NewMotorBike;
        }

        private static FuelCar FuelCarInput(string licenseNumber, string vehicleModel)
        {
            float currentFuel = 0, maxFuel = 0;
            FuelInput(ref currentFuel, ref maxFuel);
            int doorsInput = NumOfDoorInput();
            FuelCar.eNumOfDoors numOfDoors = (FuelCar.eNumOfDoors)doorsInput;
            int colorCar = ColorInput();
            FuelCar.eColorType colorOfCar = (FuelCar.eColorType)colorCar;
            FuelCar NewCar = new FuelCar(vehicleModel, licenseNumber, currentFuel, maxFuel, FuelVehicle.eFuelType.Octan96, numOfDoors, colorOfCar);
            return NewCar;
        }

        private static int LicenseTypeInput()
        {
            bool validInput = false;
            string msg = string.Format(
    @"Select the type of license you have(enter the number selection) :
1- A,    2- A1,     3- A2    4-B");

            Console.WriteLine(msg);
            int.TryParse(Console.ReadLine(), out int o_LicenseType);
            validInput = CheckInput(k_One, k_Four, o_LicenseType);
            while (!validInput)
            {
                Console.WriteLine("invalid input");
                int.TryParse(Console.ReadLine(), out o_LicenseType);
                validInput = CheckInput(k_One, k_Four, o_LicenseType);
            }

            return o_LicenseType;
        }

        private static bool HazardousMaterialsInput()
        {
            bool validInput = false;
            bool result;
            string msg = string.Format(
    @"Do you have Hazardous Materials (enter the number selection) :
1-yes,   2-no");

            Console.WriteLine(msg);
            int.TryParse(Console.ReadLine(), out int o_HazardousMaterials);
            validInput = CheckInput(k_One, k_Two, o_HazardousMaterials);
            while (!validInput)
            {
                Console.WriteLine("invalid input");
                int.TryParse(Console.ReadLine(), out o_HazardousMaterials);
                validInput = CheckInput(k_One, k_Two, o_HazardousMaterials);
            }

            result = o_HazardousMaterials == k_One;

            return result;
        }

        private static int ColorInput()
        {
            bool validInput = false;
            string msg = string.Format(
    @"Select the color of the car you have (enter the number selection) :
1- red,    2- blue,     3- black    4-grey");

            Console.WriteLine(msg);
            int.TryParse(Console.ReadLine(), out int o_Color);
            validInput = CheckInput(k_One, k_Four, o_Color);
            while (!validInput)
            {
                Console.WriteLine("invalid input");
                int.TryParse(Console.ReadLine(), out o_Color);
                validInput = CheckInput(k_One, k_Four, o_Color);
            }

            return o_Color;
        }

        private static int NumOfDoorInput()
        {
            bool validInput = false;
            string msg = string.Format(
    @"Select how many doors does your car have (enter the number selection) :
2- two,    3- three,     4- four    5-five");
            Console.WriteLine(msg);
            int.TryParse(Console.ReadLine(), out int o_Doors);
            validInput = CheckInput(k_Two, k_Five, o_Doors);
            while (!validInput)
            {
                Console.WriteLine("invalid input");
                int.TryParse(Console.ReadLine(), out o_Doors);
                validInput = CheckInput(k_Two, k_Five, o_Doors);
            }

            return o_Doors;
        }

        public void ShowVehiclesInTheGarage()
        {
            int number;
            bool success = false;
            List<string> licenseNumbers;
            string msg = string.Format(
@"Choose which vehicles you want to display(enter the number):
1- InRepair,    2- Fixed,     3- Paid    4-Show all");
            Console.WriteLine(msg);
            do
            {
                string input = Console.ReadLine();
                success = int.TryParse(input, out number);

                if (!success || (number < k_One || number > k_Four))
                {
                    WrongInput();
                }
            }
            while (!success || (number < k_One || number > k_Four));

            if (number == k_Four)
            {
                licenseNumbers = m_Garage.GetAllLicenseNumbers();
            }
            else
            {
                Vehicle.eStatus optionChoise = (Vehicle.eStatus)number;
                licenseNumbers = m_Garage.GetLicenseNumbersByStatus(optionChoise);
            }

            if (licenseNumbers.Count > k_Zero)
            {
                foreach (string licenseNumber in licenseNumbers)
                {
                    Console.WriteLine(licenseNumber);
                }
            }
            else
            {
                Console.WriteLine("There are no vehicles.");
            }
        }

        public void ChangingStatusOfVehiclesInTheGarage()
        {
            int index = 0;
            string licenseNumber = GetLicenseNumber(ref index);
            string input;
            int statusInput;
            bool isLegalNumber = false;
            string msg = string.Format(
          @"Select the new mode of the vehicle (enter the number selection) :
1- InRepair,    2- Fixed,     3- Paid");
            Console.WriteLine(msg);
            do
            {
                input = Console.ReadLine();
                statusInput = 0;
                isLegalNumber = int.TryParse(input, out statusInput);
                if (isLegalNumber)
                {
                    if (CheckInput(k_One, k_NumberOfStatusSelections, statusInput))
                    {
                        m_Garage.m_VehicleList[index].Status = (Vehicle.eStatus)statusInput;
                        isLegalNumber = true;
                    }
                    else
                    {
                        isLegalNumber = false;
                    }
                }

                if (!isLegalNumber)
                {
                    WrongInput();
                }
            }
            while (isLegalNumber == false);
        }

        public string GetLicenseNumber(ref int io_index)
        {
            string licenseNumber;
            bool isLegalInput = false, isCarExist = true;
            Console.WriteLine("Insert vehicle license number:");
            do
            {
                licenseNumber = Console.ReadLine();
                isLegalInput = CheckIfInputIsANumber(licenseNumber);
                if (!isLegalInput)
                {
                    WrongInput();
                }
                else
                {
                    io_index = 0;
                    isCarExist = m_Garage.CheckIfCarAlreadyInTheGarage(licenseNumber, ref io_index);
                    if (!isCarExist)
                    {
                        throw new VehicleNotFoundException(licenseNumber);
                    }

                }
            }
            while (!isLegalInput || !isCarExist);
            return licenseNumber;
        }

        public void InflatingAirInTheWheels()
        {
            int index = 0;
            string licenseNumber = GetLicenseNumber(ref index);

            m_Garage.m_VehicleList[index].InflatingAllWheelsInTheVehicle();
            Console.WriteLine("The tires were inflated to the maximum!");
        }

        public void PrintFullDataOfVehicle()
        {
            int index = 0;
            string licenseNumber = GetLicenseNumber(ref index);
            Vehicle vehicle = m_Garage.m_VehicleList[index];
            vehicle.PrintDetails();
        }

        public void ProgramRun()
        {
            bool validInput = false;
            bool isNumber = false;
            int number = 0;
            string input;

            while (!s_ExitProgram)
            {
                try
                {
                    Menu();
                    do
                    {
                        number = 0;
                        input = Console.ReadLine();
                        isNumber = int.TryParse(input, out number);
                        validInput = CheckInput(k_One, k_NumberOfMenuSelections, number);
                        if (!validInput || !isNumber)
                        {
                            WrongInput();
                        }
                    }
                    while (!validInput || !isNumber);

                    switch (number)
                    {
                        case (int)eChoice.AddVehicle:
                            EnterVehicleToTheGarage();
                            break;
                        case (int)eChoice.ShowVehicleList:
                            ShowVehiclesInTheGarage();
                            break;
                        case (int)eChoice.ChangeStatus:
                            ChangingStatusOfVehiclesInTheGarage();
                            break;
                        case (int)eChoice.InflatingTire:
                            InflatingAirInTheWheels();
                            break;
                        case (int)eChoice.FuelingVehicle:
                            FuelingGasolineVehicle();
                            break;
                        case (int)eChoice.ChargeVehicle:
                            ChargingAnElectricVehicle();
                            break;
                        case (int)eChoice.FullDataOfVehicle:
                            PrintFullDataOfVehicle();
                            break;
                        default:
                            Console.WriteLine("Goodbye!");
                            ExitProgram();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Bad input format");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("invalid argument");
                }
                catch (VehicleNotFoundException exception)
                {
                    Console.WriteLine("the vehicle with license number {0} was not found in the garage", exception.LicenseNum);
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine("invalid input, input range should be {0}-{1}", exception.MinValue, exception.MaxValue);
                }
                catch (IncompatibleEnergyType)
                {
                    Console.WriteLine("invalid input, trying to add energy that is incompatible");
                }
                catch (Exception)
                {
                    Console.WriteLine("error: closing program");
                    s_ExitProgram = true;
                }
            }
        }

        private void ExitProgram()
        {
            Console.WriteLine("Goodbye");
            s_ExitProgram = true;
        }

        public void FuelingGasolineVehicle()
        {
            int index = 0;
            string licenseNumber = GetLicenseNumber(ref index);
            string input;
            int fuelTypeInput;
            bool isLegalNumber = false;
            bool isCompatibleFuel = false;
            bool isFueled = false;
            FuelVehicle compare = null;
            float literInput;
            if (m_Garage.m_VehicleList[index] is ElectricVehicle)
            {
                throw new IncompatibleEnergyType();
            }

            string msg = string.Format(
    @"Choose a fuel type to fill (enter the number (X) selection) :
(0)- Soler,    (95)- Octan95,    (96)- Octan96,    (98)-Octan98");
            Console.WriteLine(msg);
            do
            {
                input = Console.ReadLine();

                isLegalNumber = int.TryParse(input, out fuelTypeInput);
                if (isLegalNumber)
                {
                    if (fuelTypeInput == (int)FuelVehicle.eFuelType.Soler || fuelTypeInput == (int)FuelVehicle.eFuelType.Octan95 || fuelTypeInput == (int)FuelVehicle.eFuelType.Octan96 || fuelTypeInput == (int)FuelVehicle.eFuelType.Octan98)
                    {
                        compare = m_Garage.m_VehicleList[index] as FuelVehicle;
                        if ((int)compare.FuelType != fuelTypeInput)
                        {
                            throw new IncompatibleEnergyType();
                        }
                        else
                        {
                            isCompatibleFuel = true;
                        }

                        isLegalNumber = true;
                    }
                    else
                    {
                        isLegalNumber = false;
                    }
                }

                if (!isLegalNumber)
                {
                    WrongInput();
                }
            }
            while (!isLegalNumber || !isCompatibleFuel);

            string msg2 = string.Format(
    @"The vehicle contains {0} liters from a maximum of {1}.
How much do you want to refuel?", compare.CurrentFuel, compare.MaxFuel);
            Console.WriteLine(msg2);

            do
            {
                input = Console.ReadLine();

                isLegalNumber = float.TryParse(input, out literInput);
                if (isLegalNumber)
                {
                    if (literInput <= (compare.MaxFuel - compare.CurrentFuel) && literInput >= k_Zero)
                    {
                        compare.CurrentFuel += literInput;
                        compare.UpdateEnergyPercentage();
                        isFueled = true;
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(0, compare.MaxFuel - compare.CurrentFuel);
                    }

                    isLegalNumber = true;
                }

                if (!isLegalNumber)
                {
                    WrongInput();
                }
            }
            while (!isLegalNumber || !isFueled);
        }

        public void ChargingAnElectricVehicle()
        {
            int index = 0;
            string licenseNumber = GetLicenseNumber(ref index);
            string input;

            bool isLegalNumber = false;
            bool isLegalCharge = false;
            ElectricVehicle compare = null;
            float minutsInput;
            if (m_Garage.m_VehicleList[index] is FuelVehicle)
            {
                throw new IncompatibleEnergyType();
            }

            compare = m_Garage.m_VehicleList[index] as ElectricVehicle;

            string msg = string.Format(
    @"Currently loaded in a battery {0} hours out of a maximum of {1} hours.
How many minutes would you like to charge? (in minuts)", compare.RemainingBattery, compare.MaxBattery);
            Console.WriteLine(msg);
            do
            {
                input = Console.ReadLine();

                isLegalNumber = float.TryParse(input, out minutsInput);
                if (isLegalNumber)
                {
                    if ((compare.RemainingBattery + (minutsInput / k_MinutsInHour)) <= compare.MaxBattery)
                    {
                        if (minutsInput >= k_Zero)
                        {
                            compare.RemainingBattery += minutsInput / k_MinutsInHour;
                            compare.UpdateEnergyPercentage();
                            isLegalCharge = true;
                        }
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(0, compare.MaxBattery - compare.RemainingBattery);
                    }
                }

                if (!isLegalNumber || !isLegalCharge)
                {
                    WrongInput();
                }
            }
            while (!isLegalNumber || !isLegalCharge);
        }
    }
}