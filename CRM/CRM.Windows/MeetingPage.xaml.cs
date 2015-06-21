using CRM.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Шаблон элемента страницы с разделением задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234234

namespace CRM
{
    /// <summary>
    /// Страница, на которой отображается заголовок группы, список элементов этой группы и сведения о
    /// выбранном в данный момент элементе.
    /// </summary>
    public sealed partial class MeetingPage : Page
    {
        private readonly NavigationHelper _navigationHelper;
        private readonly ObservableDictionary _defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// Эту настройку можно изменить на модель строго типизированных представлений.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this._defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper используется на каждой странице для облегчения навигации и 
        /// управление жизненным циклом процесса
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this._navigationHelper; }
        }

        public MeetingPage()
        {
            this.InitializeComponent();

            // Настройка вспомогательного приложения навигации
            this._navigationHelper = new NavigationHelper(this);
            this._navigationHelper.LoadState += navigationHelper_LoadState;
            this._navigationHelper.SaveState += navigationHelper_SaveState;

            // Настройка логических компонентов навигации по страницам, позволяющих
            // чтобы на странице отображалась только одна плитка.
            //this._navigationHelper.GoBackCommand = new CRM.Common.RelayCommand(() => this.GoBack(), () => this.CanGoBack());
            //this.LwMeeting.SelectionChanged += itemListView_SelectionChanged;

            // Начало прослушивания изменений размера окна 
            // чтобы перейти от отображения двух панелей к отображению одной
            //Window.Current.SizeChanged += Window_SizeChanged;
            //this.InvalidateVisualState();

            CallServiceMethod();

        }

        public async void CallServiceMethod()
        {
            var cl = new ServiceReference.DataServiceClient();

            var results = await cl.GetShortMeetingDataAsync();
            var result = results;
            if (result != null)
            {
                grdMeeting.ItemsSource = result;
            }

        }
        void itemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.UsingLogicalPageNavigation())
            {
                this._navigationHelper.GoBackCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Заполняет страницу содержимым, передаваемым в процессе навигации.  Также предоставляется любое сохраненное состояние
        /// при повторном создании страницы из предыдущего сеанса.
        /// </summary>
        /// <param name="sender">
        /// Источник события; как правило, <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Данные события, предоставляющие параметр навигации, который передается
        /// <see cref="Frame.Navigate(Type, Object)"/> при первоначальном запросе этой страницы и
        /// словарь состояний, сохраненных этой страницей в ходе предыдущего
        /// сеанса.  Это состояние будет равно NULL при первом посещении страницы.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Присвоить Me.DefaultViewModel("Group") привязываемую группу
            // TODO: Присвоить Me.DefaultViewModel("Items") коллекцию привязываемых элементов

            if (e.PageState == null)
            {
                // Если это новая страница, первый элемент выбирается автоматически, если не действует логическая
                // навигация по страницам (см. ниже область #region логической навигации по страницам).
                if (!this.UsingLogicalPageNavigation() && this.itemsViewSource.View != null)
                {
                    this.itemsViewSource.View.MoveCurrentToFirst();
                }
            }
            else
            {
                // Восстановление ранее сохраненного состояния, связанного с этой страницей
                if (e.PageState.ContainsKey("SelectedItem") && this.itemsViewSource.View != null)
                {
                    // TODO: Вызов метода Me.itemsViewSource.View.MoveCurrentTo() с выбранным
                    //       элементом, указанным значением pageState("SelectedItem")

                }
            }
        }

        /// <summary>
        /// Сохраняет состояние, связанное с данной страницей, в случае приостановки приложения или
        /// удаления страницы из кэша навигации.  Значения должны соответствовать требованиям сериализации
        /// <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">Источник события; как правило, <see cref="NavigationHelper"/></param>
        /// <param name="e">Данные события, которые предоставляют пустой словарь для заполнения
        /// сериализуемым состоянием.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            if (this.itemsViewSource.View != null)
            {
                // TODO: Создание производного сериализуемого параметра навигации и его назначение объекту
                //       pageState("SelectedItem")

            }
        }

        #region Логическая навигация по страницам

        // Разделенная страница разработана таким образом, чтобы в случае, когда в окне недостаточно места для отображения
        // как списка, так и сведений, отображалась только одна из панелей.
        //
        // Все это реализуется с помощью единственной физической страницы, которая может представлять две логические
        // страницы.  В следующем коде эта цель достигается без необходимости ставить пользователя в известность о
        // разделении.

        private const int MinimumWidthForSupportingTwoPanes = 768;

        /// <summary>
        /// Вызывается, чтобы определить, должна страница использоваться в качестве одной логической страницы или в качестве двух.
        /// </summary>
        /// <returns>Значение True, если окно должно вести себя как одна логическая страница, значение False
        /// в противном случае.</returns>
        private bool UsingLogicalPageNavigation()
        {
            return Window.Current.Bounds.Width < MinimumWidthForSupportingTwoPanes;
        }

     

        /// <summary>
        /// Вызывается, чтобы определить имя состояния отображения, соответствующее состоянию
        /// отображения приложения.
        /// </summary>
        /// <returns>Имя требуемого состояния отображения.  Это имя совпадает с именем состояния
        /// отображения, кроме случаев, когда есть выделенный элемент в книжном или прикрепленном представлении, где
        /// эта дополнительная логическая страница представляется добавлением суффикса _Detail.</returns>
        

        #endregion

        #region выполнить регистрацию NavigationHelper

        /// Методы, предоставленные в этом разделе, используются исключительно для того, чтобы
        /// NavigationHelper для отклика на методы навигации страницы.
        /// 
        /// Логика страницы должна быть размещена в обработчиках событий для 
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// и <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// Параметр навигации доступен в методе LoadState 
        /// в дополнение к состоянию страницы, сохраненному в ходе предыдущего сеанса.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void btnToTasks_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TaskPage));
        }

        private void btnToContacts_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContactPage));
        }

        private void btnToCustomers_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CustomerPage));
        }

        private void btnReLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FirstPage));
        }
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
}
