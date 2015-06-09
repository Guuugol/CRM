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
    [ServiceContract(Namespace = "http://tempuri.org/")]
    interface ITestingService
    {
        [OperationContract]
        List<Model.Test> GetTestList();

        [OperationContract]

        void PrintData(int id, string a1);

    }
}
