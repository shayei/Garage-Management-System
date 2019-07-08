using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricMotorBike : ElectricVehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public ElectricMotorBike(string i_ModelName, string i_LicenseNumber, float i_RemainingBattery, float i_MaxBattery, int i_EngineCapacity, eLicenseType i_LicenseType)
            : base(i_ModelName, i_LicenseNumber, i_RemainingBattery, i_MaxBattery)
        {
            m_LicenseType = i_LicenseType;
            m_EngineCapacity = i_EngineCapacity;
        }

        public enum eLicenseType
        {
            A = 1,
            A1,
            A2,
            B
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }

        public override void PrintDetails()
        {
            base.PrintDetails();
            string msg = string.Format(
@"License type: {0}
Engine capacity: {1}", LicenseType, EngineCapacity);
            Console.WriteLine(msg);
        }
    }
}
