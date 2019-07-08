using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleNotFoundException : Exception
    {
        private readonly string r_licenseNum;

        public VehicleNotFoundException(string i_LicenseNum)
          : base()
        {
            r_licenseNum = i_LicenseNum;
        }

        public string LicenseNum
        {
            get { return r_licenseNum; }
        }
    }
}
