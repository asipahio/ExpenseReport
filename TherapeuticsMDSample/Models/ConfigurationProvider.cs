using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TherapeuticsMDSample.Models.Interfaces;

namespace TherapeuticsMDSample.Models
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string ReceiptsLocation()
        {
            return ConfigurationManager.AppSettings["ReceiptsLocation"];
        }
    }
}