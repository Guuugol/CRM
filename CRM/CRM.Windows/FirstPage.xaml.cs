using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CRM.ServiceReference;



// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CRM
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class FirstPage : Page
    {

    

        public FirstPage()
        {
            this.InitializeComponent();
        }
        
        public async void CallGetLoginData(string login)
        {
            var cl = new ServiceReference.DataServiceClient();

            try
            {
                var results = await cl.GetLoginDataAsync();
                

                if (!results.Contains(login))
                {
                    System.Exception ex = new Exception();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog("Неверное имя пользователя");
                dialog.ShowAsync();
                return;

                //tbLogin.Text = "";
                // throw;
            }
                
            var contactPage = new ContactPage();
            Frame.Navigate(typeof(ContactPage));
            return;



        }
        
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var caption = tbLogin.Text;
            
            if (String.IsNullOrEmpty(caption))
            {
                //tbError.Text = "Введите имя пользователя";
                //tbError.Visibility = Visibility.Visible;
                var dialog = new MessageDialog("Введите имя пользователя");
                dialog.ShowAsync();
            }
            else
            {
                CallGetLoginData(caption);
                //task.RunSynchronously();
               // var right = task.Result;
               // t.RunSynchronously();
                //CallGetLoginData(caption).RunSynchronously();
                //Task task = new ServiceReference.DataServiceClient().GetLoginDataAsync(caption);
                //task.Wait();
                
            }
        }

        private void TbLogin_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                btnLogin_Click(null, null);
            }
        }
    }
}
