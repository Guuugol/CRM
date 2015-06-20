using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CRM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContactPage : Page
    {
        public ContactPage()
        {
            this.InitializeComponent();

            //grdContact.ItemsSource = new List<DataType>()
            //{
            //    new DataType {Id = 1, Name = "Name1"},
            //    new DataType {Id = 2, Name = "Name2"}
            //};

            CallServiceMethod();

        }

        public async void CallServiceMethod()
        {
            var cl = new ServiceReference.DataServiceClient();

            var results = await cl.GetTestListAsync();  
            var result = results; 
            if (result != null)
            {
                grdContact.ItemsSource = result;
            }

        }



        public class DataType
        {
            public string Name { get; set; }

            public string Surname { get; set; }

            public string Telephone { get; set; }

            public string EMail { get; set; }

        }


        private void btnToTasks_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TaskPage));
        }

        private void btnToMeetings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MeetingPage));
        }

        private void btnToCustomers_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (CustomerPage));
        }

        private void btnReLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (FirstPage));
        }

    }
}
    

