using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Linq;
using System.Net.Sockets;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataBaseWCF;
//using WCFTestingServise;
//using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace DataBaseWCF
{
    public class DataService : IDataService
    {

        public List<DataType> GetTestList()
        {
            Console.WriteLine("Method: GetTestList");
            Console.WriteLine("Method: GetTestList");
            using (var db = new CRMEntities())
            {
                return
                    db.tbl_Contact.Select(
                        c => new DataType() {Name = c.Name, Surname = c.Surname, Tel = c.Telephone, Email = c.E_mail})
                        .ToList();
            }
//            return new List<DataType>(
//                new []{
//                    new Model.Test(){Name = "example", Id=1},
//                    new Model.Test(){Name = "e2", Id=2},
//                   new Model.Test(){Name = "ex3", Id=3}
//                });
        }


        public void PrintData(int id, string a1)
        {
            Console.WriteLine("Method: PrintData");
            Console.WriteLine("data: " + a1);
        }

        public List<string> GetLoginData()
        {
            Console.WriteLine("Method: GetLoginData");
            Console.WriteLine("Method: GetLoginData");
            using (var db = new CRMEntities())
            {
                //Data Source=WIN-J2OBO120VJ2;Initial Catalog=CRM;Integrated Security=True;
               // var loginQuery = from c in db.tbl_Manager where c.Name == login select c.Name;
                var managers = (from man in db.tbl_Manager select man.Name).ToList();
                return managers;
            }
        }

        public List<CustomerData> GetCustomerData()
        {
            Console.WriteLine("Method: GetCustomerData");
            Console.WriteLine("Method: GetCustomerData");
            using (var db = new CRMEntities())
            {
                /*
                var customers = from cust in db.tbl_Customer
                    join c in db.tbl_Contact on cust.ContactID equals c.ID
                    select cust.Name;
                var dataList = new List<CustomerData>();
                foreach (var c in customers)
                {
                    var cust = new CustomerData
                    {
                        Name = c,
                        ContactName = (from contact in db.tbl_Contact
                            where
                                Equals(contact.ID.ToString(),
                                    from customer in db.tbl_Customer
                                    where customer.Name == c
                                    select customer.ContactID.ToString())
                            select contact.Name).ToString(),
                        ContactSurname = (from contact in db.tbl_Contact
                            where
                                Equals(contact.ID.ToString(),
                                    from customer in db.tbl_Customer
                                    where customer.Name == c
                                    select customer.ContactID.ToString())
                            select contact.Surname).ToString()
                    };
                    dataList.Add(cust);
                    Console.WriteLine(cust.Name);
                }
                 */

                //return dataList; 

                var query = from cust in db.tbl_Customer
                    join c in db.tbl_Contact on cust.ContactID equals c.ID
                    let contactName = c.Name
                    let contactSurname = c.Surname
                    select new {cust.Name, contactName, contactSurname};
                var dataList = new List<CustomerData>();
                foreach (var q in query)
                {
                    var cust = new CustomerData
                    {
                        Name = q.Name,
                        ContactName = q.contactName,
                        ContactSurname = q.contactSurname
                    };
                    dataList.Add(cust);
                }
                return dataList;
            }
        }

        public List<ShortMeetingData> GetShortMeetingData()
        {   
            Console.WriteLine("Method: GetShortMeetingData");
            Console.WriteLine("Method: GetShortMeetingData");
            using (var db = new CRMEntities())
            {
                var query = from m in db.tbl_Meeting
                    join c in db.tbl_Customer on m.CustomerID equals c.ID
                    select new {c.Name, m.Date};
                var dataList = new List<ShortMeetingData>();
                foreach (var q in query)
                {
                    var cust = new ShortMeetingData() 
                    {
                        CustomerName  = q.Name,
                        Date  = q.Date 
                    };
                    dataList.Add(cust);
                }
                return dataList;
            }
        }

        public List<ShortTaskData> GetShortTaskData()
        {
            Console.WriteLine("Method: GetShortTaskData");
            Console.WriteLine("Method: GetShortTaskData");
            using (var db = new CRMEntities())
            {
                var query = from t in db.tbl_Task
                            join c in db.tbl_Customer on t.CustomerID equals c.ID
                            join tt in db.tbl_TaskType on t.TaskTypeID equals tt.ID
                            let typeName = tt.Name 
                            select new { c.Name, typeName };
                var dataList = new List<ShortTaskData>();
                foreach (var q in query)
                {
                    var cust = new ShortTaskData()
                    {
                        CustomerName = q.Name,
                        TaskType = q.typeName
                    };
                    dataList.Add(cust);
                }
                return dataList;
            }
        }
    }
}
 