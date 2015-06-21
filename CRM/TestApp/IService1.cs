using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        [OperationContract]
        List<CustomerData> GetCustomerData();

        [OperationContract]
        List<ShortMeetingData> GetShortMeetingData();

        [OperationContract]
        List<ShortTaskData> GetShortTaskData();


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

    public class CustomerData
    {
        public string Name { get; set; }
        
        public string  ContactName { get; set; }

        public string ContactSurname { get; set; }

    }

    public class ShortMeetingData
    {
        public string CustomerName { get; set; }

        public DateTime Date { get; set; }
    }

    public class FullMeetingData
    {
        public string CustomerName { get; set; }

        public DateTime Date { get; set; }

        public string Owner { get; set; }

        public string Goal { get; set; }

        public string Result { get; set; }
    }

    public class ShortTaskData
    {
        public string CustomerName { get; set; }

        public string TaskType { get; set; }

    }
}
