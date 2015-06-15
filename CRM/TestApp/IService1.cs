using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace DataBaseWCF 
{
    [ServiceContract(Namespace = "http://tempuri.org/")]
    interface IDataService
    {
        [OperationContract]
        List<DataType> GetTestList();

        [OperationContract]

        void PrintData(int id, string a1);


        [OperationContract]
        List<string> GetLoginData();

    }

    public class DataType
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }
    }

    public class LoginData
    {
        public string Name { get; set; }
    }
}
