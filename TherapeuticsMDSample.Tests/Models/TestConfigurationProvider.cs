using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TherapeuticsMDSample.Models.Interfaces;

namespace TherapeuticsMDSample.Tests.Models
{
    public class TestConfigurationProvider : IConfigurationProvider
    {
        public string ReceiptsLocation()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
