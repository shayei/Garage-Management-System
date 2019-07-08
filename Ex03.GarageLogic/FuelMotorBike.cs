using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelMotorBike : FuelVehicle
    {
        private int m_EngineCapacity;
        private eLicenseType m_LicenseType;

        public FuelMotorBike(string i_ModelName, string i_LicenseNumber, float i_CurrentFuel, float i_MaxFuel, eFuelType i_FuelType, int i_EngineCapacity, eLicenseType i_LicenseType)
            : base(i_ModelName, i_LicenseNumber, i_CurrentFuel, i_MaxFuel, i_FuelType)
        {
            m_EngineCapacity = i_EngineCapacity;
            m_LicenseType = i_LicenseType;
        }

        public enum eLicenseType
        {
            A = 1,
            A1,
            A2,
            B
        }

        public int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
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
