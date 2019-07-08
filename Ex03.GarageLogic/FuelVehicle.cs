using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelVehicle : Vehicle
    {
        protected float m_CurrentFuel;
        protected float m_MaxFuel;
        protected eFuelType m_FuelType;

        public FuelVehicle(string i_ModelName, string i_LicenseNumber, float i_CurrentFuel, float i_MaxFuel, eFuelType i_FuelType)
           : base(i_ModelName, i_LicenseNumber)
        {
            m_FuelType = i_FuelType;
            m_CurrentFuel = i_CurrentFuel;
            m_MaxFuel = i_MaxFuel;
        }

        public enum eFuelType
        {
            Soler,
            Octan95 = 95,
            Octan96 = 96,
            Octan98 = 98
        }

        public float MaxFuel
        {
            get { return m_MaxFuel; }
            set { m_MaxFuel = value; }
        }

        public float CurrentFuel
        {
            get { return m_CurrentFuel; }
            set { m_CurrentFuel = value; }
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public void UpdateEnergyPercentage()
        {
            EnergyPercentage = CurrentFuel / MaxFuel * 100;
        }

        public override void PrintDetails()
        {
            base.PrintDetails();
            string msg = string.Format(
@"Gasoline vehicle
{0} liters of fuel (maximum of {1})
Fuel type: {2}", CurrentFuel, MaxFuel, FuelType);
            Console.WriteLine(msg);
        }
    }
}
