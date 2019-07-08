using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
    {
        private eNumOfDoors m_NumOfDoors;
        private eColorType m_Color;

        public ElectricCar(string i_ModelName, string i_LicenseNumber, float i_RemainingBattery, float i_MaxBattery, eNumOfDoors i_NumOfDoors, eColorType i_Color)
            : base(i_ModelName, i_LicenseNumber, i_RemainingBattery, i_MaxBattery)
        {
            m_Color = i_Color;
            m_NumOfDoors = i_NumOfDoors;
        }

        public enum eNumOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public enum eColorType
        {
            Red = 1,
            Blue,
            Black,
            Grey
        }

        public eColorType Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public eNumOfDoors numOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }

        public override void PrintDetails()
        {
            base.PrintDetails();
            string msg = string.Format(
@"Number of doors: {0}
Vehicle color: {1}", numOfDoors, Color);
            Console.WriteLine(msg);
        }
    }
}
