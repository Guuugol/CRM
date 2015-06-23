using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Grid = Windows.UI.Xaml.Controls.Grid;


// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CRM
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            this.InitializeComponent();

            CallServiceMethod();
        }

        private void btnToMeetings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (MeetingPage));
        }

        private void btnToTasks_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (TaskPage));
        }

        private void btnToContacts_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContactPage));
        }

        private void btnReLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FirstPage));
        }

        public async void CallServiceMethod()
        {
            var cl = new ServiceReference.DataServiceClient();

            var results = await cl.GetCustomerDataAsync();
            var result = results;
            if (result != null)
            {
                grdCustomer.ItemsSource = result;
            }

        }

        public class CustomerData
        {
            public string Name { get; set; }

            public string ContactName { get; set; }

            public string ContactSurname { get; set; }

        }

        private void Grid_Holding(object sender, HoldingRoutedEventArgs e)
        {
            var record = grdCustomer.SelectedItem as ServiceReference.CustomerData;

            // popup
            var popup = new Popup();
            popup.IsLightDismissEnabled = true;
            Windows.UI.Xaml.Controls.Grid panel = new Windows.UI.Xaml.Controls.Grid();
            panel.Background = TopAppBar.Background;
            panel.Height = 250;
            panel.Width = 150;
            panel.Transitions = new TransitionCollection();
            panel.Transitions.Add(new PopupThemeTransition());
            Button btnMain = new Button();
            btnMain.Content = "Кнопка";
            btnMain.VerticalAlignment = VerticalAlignment.Center;
            btnMain.HorizontalAlignment = HorizontalAlignment.Center;
            panel.Children.Add(btnMain);
            popup.Child = panel;
            var tbCustomer = new TextBlock();
            var tbContact = new TextBlock();
            tbCustomer.Text = record.Name.ToString();
            tbContact.Text  = record.ContactName  + ' ' + record.ContactSurname ;
            var button = new Button();
            var transform = button.TransformToVisual(this);
            var point = transform.TransformPoint(new Point());
            popup.HorizontalOffset = point.X;
            popup.VerticalOffset = Window.Current.CoreWindow.Bounds.Top /*-TopAppBar.ActualHeight — panel.Height — 4*/;
            popup.IsOpen = true;
        }

        private void grdCustomer_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.RightButton)
            {
                Grid_Holding(null, null);
            }
        }
    }
}
