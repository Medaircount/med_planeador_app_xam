using DroneScanning.Interface;
using DroneScanning.Models;
using Firebase.Firestore.Auth;
using Newtonsoft.Json;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Xamarin.Io.OpenCensus.Metrics.Export.Summary;

namespace DroneScanning.Services
{
    public class LogisticsService : ILogistics
    {
        async Task<List<Record>> ILogistics.GetRecords() {
            List<Record> records = new List<Record>();
            try
            {
                var rcs = await CrossCloudFirestore.Current
                                .Instance
                                .Collection("Records")
                                .GetAsync();
                
                foreach (var document in rcs.Documents)
                {
                    // Mapea cada documento a un objeto Usuario
                    string tmp1 = JsonConvert.SerializeObject(document.Data);

                    Record record = JsonConvert.DeserializeObject<Record>(tmp1);
                    if (!String.IsNullOrEmpty(record.UserId))
                    {   
                        records.Add(record);
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            return records;
        }

        async Task<Record> ILogistics.Register(string userId, string record, string recordid) {
            Record rc = new Record { 
                UserId = userId,
                RecordId = recordid,
                RecordName = record,
            };
            try
            {
                await CrossCloudFirestore.Current
                         .Instance
                         .Collection("Records")
                         .AddAsync(rc);
            }
            catch (Exception)
            {

                throw;
            }
            return rc;
        }
    }
}
