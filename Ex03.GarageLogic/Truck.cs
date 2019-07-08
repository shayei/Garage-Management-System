using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : FuelVehicle
    {
        private bool m_HazardousMaterials;
        private float m_VolumeOfCargo;

        public Truck(string i_ModelName, string i_LicenseNumber, float i_CurrentFuel, float i_MaxFuel, eFuelType i_FuelType, bool i_HazardousMaterials, float i_VolumeOfCargo)
            : base(i_ModelName, i_LicenseNumber, i_CurrentFuel, i_MaxFuel, i_FuelType)
        {
            m_HazardousMaterials = i_HazardousMaterials;
            m_VolumeOfCargo = i_VolumeOfCargo;
        }

        public bool HazardousMaterials
        {
            get { return m_HazardousMaterials; }
            set { m_HazardousMaterials = value; }
        }

        public float VolumeOfCargo
        {
            get { return m_VolumeOfCargo; }
            set { m_VolumeOfCargo = value; }
        }

        public override void PrintDetails()
        {
            base.PrintDetails();
            string answer;
            if (HazardousMaterials == true)
            {
                answer = "yes";
            }
            else
            {
                answer = "no";
            }

            string msg = string.Format(
@"Is contain hazardous materials? {0}
Volume of cargo: {1}", answer, VolumeOfCargo);
            Console.WriteLine(msg);
        }
    }
}