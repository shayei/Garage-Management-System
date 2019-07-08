using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Tire
    {
        private string m_ManufactorName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public void InflatingTire()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }

        public string ManufactorName
        {
            get { return m_ManufactorName; }
            set { m_ManufactorName = value; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
            set { m_MaxAirPressure = value; }
        }
    }
}
