using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricVehicle : Vehicle
    {
        protected float m_RemainingBattery;
        protected float m_MaxBattery;

        public ElectricVehicle(string i_ModelName, string i_LicenseNumber, float i_RemainingBattery, float i_MaxBattery)
            : base(i_ModelName, i_LicenseNumber)
        {
            m_RemainingBattery = i_RemainingBattery;
            m_MaxBattery = i_MaxBattery;
            UpdateEnergyPercentage();
        }

        public float RemainingBattery
        {
            get { return m_RemainingBattery; }
            set { m_RemainingBattery = value; }
        }

        public float MaxBattery
        {
            get { return m_MaxBattery; }
            set { m_MaxBattery = value; }
        }

        public void UpdateEnergyPercentage()
        {
            EnergyPercentage = (RemainingBattery / MaxBattery) * 100;
        }

        public override void PrintDetails()
        {
            base.PrintDetails();
            string msg = string.Format(
@"Electric vehicle
The battery is charged for {0} hours (maximum {1} hours)", RemainingBattery, MaxBattery);
            Console.WriteLine(msg);
        }
    }
}
