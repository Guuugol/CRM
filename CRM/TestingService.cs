using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFTestingServise
{   
    public class TestingService : ITestingService
    {
        
        public List<Model.Test> GetTestList()
        {
            Console.WriteLine("Method: GetTestList");
            return new List<Model.Test>(
                new []{
                    new Model.Test(){Name = "example", Id=1},
                    new Model.Test(){Name = "e2", Id=2},
                    new Model.Test(){Name = "ex3", Id=3}
                });
        }


        public void PrintData(int id, string a1)
        {
            Console.WriteLine("Method: PrintData");
            Console.WriteLine("data: " + a1);
        }
    }
}
