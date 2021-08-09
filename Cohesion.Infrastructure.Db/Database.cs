using Cohesion.Core;
using Cohesion.Core.ServiceRequest.Models;
using System;
using System.Collections.Generic;

namespace Cohesion.Infrastructure.Db
{
    public static class Database
    {
        public static List<ServiceRequest> ServiceRequestList = new List<ServiceRequest>();
        public class ServiceRequest : Auditable
        {
            public string BuildingCode { get; set; }
            public string Description { get; set; }
            public CurrentStatus CurrentStatus { get; set; }
        }
    }
}
