using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TherapeuticsMDSample.Models.Interfaces;

namespace TherapeuticsMDSample.Tests.Models
{
    public class TestPathProvider : IPathProvider
    {
        public string MapPath(string path)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            return Path.Combine(basePath, path);
        }
    }
}
