using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        public List<Vehicle> m_VehicleList = null;

        public Garage()
        {
            m_VehicleList = new List<Vehicle>();
        }

        public bool CheckIfCarAlreadyInTheGarage(string i_licenseNumber, ref int index)
        {
            bool result = false;
            foreach (Vehicle vehicle in m_VehicleList)
            {
                if (vehicle.LicenseNumber == i_licenseNumber)
                {
                    result = true;
                    break;
                }

                index++;
            }

            return result;
        }

        public List<Vehicle> AddToVehicleList(Vehicle i_NewVehicle)
        {
            m_VehicleList.Add(i_NewVehicle);
            return m_VehicleList;
        }

        public List<string> GetLicenseNumbersByStatus(Vehicle.eStatus i_StateInGarage)
        {
            List<string> licenseNumbers = new List<string>();
            foreach (Vehicle vehicle in m_VehicleList)
            {
                if (vehicle.Status == i_StateInGarage)
                {
                    licenseNumbers.Add(vehicle.LicenseNumber);
                }
            }

            return licenseNumbers;
        }

        public List<string> GetAllLicenseNumbers()
        {
            List<string> licenseNumbers = new List<string>();
            foreach (Vehicle vehicle in m_VehicleList)
            {
                licenseNumbers.Add(vehicle.LicenseNumber);
            }

            return licenseNumbers;
        }
    }
}
