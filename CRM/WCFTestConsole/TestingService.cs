using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFTestConsole;

namespace WCFTestingServise
{   
    public class TestingService : ITestingService
    {

        public List<DataType> GetTestList()
        {
            Console.WriteLine("Method: GetTestList");
            using (var db = new CRMEntities())
            {
                return db.tbl_Contact.Select(c => new DataType() {Name = c.Name, Surname = c.Surname , Tel = c.Telephone , Email = c.E_mail }).ToList();
            }
 //           return new List<DataType>(
   //             new []{
     //               new DataType(){Name = "example", Id=1},
       //             new DataType(){Name = "e2", Id=2},
         //           new DataType(){Name = "ex3", Id=3}
           //     });
        }


        public void PrintData( string a1)
        {
            Console.WriteLine("Method: PrintData");
            Console.WriteLine("data: " + a1);
        }
    }
}
