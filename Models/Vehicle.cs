using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTS_User_Service.Models
{
    public class Vehicle
    {

        public int VehicleID { get; set; }
        public int DeviceID { get; set; }
        public int UserID { get; set; }

        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public string ManufacturingYear { get; set; }
        public double LoadCarryingCapacity { get; set; }
        public string MakeOfVehicle { get; set; }
        public string ModelNumber { get; set; }
        public string BodyType { get; set; }
        public string OrganisationName { get; set; }

        public int UpdatedBy { get; set; }

    }
}
