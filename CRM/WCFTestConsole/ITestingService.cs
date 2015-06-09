using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WCFTestingServise
{
    [ServiceContract]
    interface ITestingService
    {
        [OperationContract]
        List<DataType> GetTestList();

        [OperationContract]

        void PrintData( string a1);

    }

    public class DataType
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }
    }
}
