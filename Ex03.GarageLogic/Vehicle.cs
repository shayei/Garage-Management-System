using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        protected Client m_Client;
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_EnergyPercentage;
        protected List<Tire> m_TireDetails = null;
        protected eStatus m_Status;

        public Vehicle(string i_ModelName, string i_LicenseNumber)
        {
            m_LicenseNumber = i_LicenseNumber;
            m_ModelName = i_ModelName;
            m_TireDetails = new List<Tire>();
            m_Status = eStatus.InRepair;
        }

        public enum eStatus
        {
            InRepair = 1,
            Fixed,
            Paid
        }

        public Client ClientDetails
        {
            get { return m_Client; }
            set { m_Client = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public eStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public float EnergyPercentage
        {
            get { return m_EnergyPercentage; }
            set { m_EnergyPercentage = value; }
        }

        public void UpdateTireList(Tire theTireToAdd, int howManyTires)
        {
            while (howManyTires > 0)
            {
                m_TireDetails.Add(theTireToAdd);
                howManyTires--;
            }
        }

        public void InflatingAllWheelsInTheVehicle()
        {
            foreach (Tire tire in m_TireDetails)
            {
                tire.InflatingTire();
            }
        }

        public virtual void PrintDetails()
        {
            string msg = string.Format(
@"License Number: {0}
Model Name: {1}
Customer's name: {2}
Phone Number: {3}
Vehicle status: {4}
EnergyPercentage: {5}%", LicenseNumber, ModelName, m_Client.OwnerName, m_Client.OwnerPhoneNumber, Status, EnergyPercentage);
            Console.WriteLine(msg);

            if (m_TireDetails.Count > 0)
            {
                Console.WriteLine("{0} {1} tires, air pressure {2} (maximum {3})", m_TireDetails.Count, m_TireDetails[0].ManufactorName, m_TireDetails[0].CurrentAirPressure, m_TireDetails[0].MaxAirPressure);
            }
        }
    }
}
