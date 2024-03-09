using DroneScanning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneScanning.Interface
{
    public interface ILogistics
    {
        internal Task<List<Record>> GetRecords();
        internal Task<Record> Register(string userId, string record, string recordid);
    }
}
