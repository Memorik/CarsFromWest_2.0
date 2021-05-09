using FluentValidation;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CarsFromWest_master
{
    public partial class MainWindow : Window
    {

        AppContextUser dbUser;
        AppContextDeal dbDeal;
        System.Collections.ObjectModel.ObservableCollection<Deal> deals { get; set; }
                
        public MainWindow()
        {
            InitializeComponent();

            dbUser = new AppContextUser();
            dbDeal = new AppContextDeal();

            deals = new System.Collections.ObjectModel.ObservableCollection<Deal>(dbDeal.Deals.ToList());         
                
            ListOfDeals.ItemsSource = deals;
           
        }
               
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new UserControlHome();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCreate":
                    usc = new UserControlCreate();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void ToMain_Click(object sender, RoutedEventArgs e)
        {
            Create.Visibility = Visibility.Collapsed;
            Cars.Visibility = Visibility.Collapsed;
            Main.Visibility = Visibility.Visible;
        }
        
        private void ToCars_Click(object sender, RoutedEventArgs e)
        {
            Create.Visibility = Visibility.Collapsed;
            Main.Visibility = Visibility.Collapsed;
            Cars.Visibility = Visibility.Visible;
        }

        private void ToCreate_Click(object sender, RoutedEventArgs e)
        {
            Cars.Visibility = Visibility.Collapsed;
            Main.Visibility = Visibility.Collapsed;
            Create.Visibility = Visibility.Visible;
        }

        private void SkipFirsMenu_Click(object sender, RoutedEventArgs e)
        {
            FirstMenu.Visibility = Visibility.Collapsed;
            GrayBackground.Visibility = Visibility.Collapsed;
        }

        private void OpenFirstMenu_Click(object sender, RoutedEventArgs e)
        {
            FirstMenu.Visibility = Visibility.Visible;
            GrayBackground.Visibility = Visibility.Visible;
        }

        private void BackToFirstMenu_Click(object sender, RoutedEventArgs e)
        {
            RegisterMenu.Visibility = Visibility.Collapsed;
            LoginMenu.Visibility = Visibility.Visible;
        }

        private void GoToRegistration_Click(object sender, RoutedEventArgs e)
        {
            RegisterMenu.Visibility = Visibility.Visible;
            LoginMenu.Visibility = Visibility.Collapsed;
            textBoxLogin.Text = "";
            textBoxPass.Password = "";
        }

        private void Regeister_Click(object sender, RoutedEventArgs e)
        {
            string login = RegLogin.Text.Trim();
            string pass1 = RegPass1.Password.Trim();
            string pass2 = RegPass2.Password.Trim();
            string status = Status.Text.Trim().ToLower();
                                 

            if (login.Length < 5)
            {
                RegLogin.ToolTip = "Мінімальна довжина логіну = 5 символів";
                RegLogin.Background = Brushes.DarkRed;
            }
            else if (pass1.Length < 5)
            {
                RegPass1.ToolTip = "Мінімальна довжина паролю = 5 символів";
                RegPass1.Background = Brushes.DarkRed;
            }
            else if (pass1 != pass2)
            {
                RegPass2.ToolTip = "Паролі не співпадають";
                RegPass2.Background = Brushes.DarkRed;
            }
            else
            {
                RegLogin.ToolTip = "";
                RegLogin.Background = Brushes.Transparent;
                RegPass1.ToolTip = "";
                RegPass1.Background = Brushes.Transparent;
                RegPass2.ToolTip = "";
                RegPass2.Background = Brushes.Transparent;
            }

            if (login.Length >= 5)
            {
                RegLogin.ToolTip = "";
                RegLogin.Background = Brushes.Transparent;
            }

            if (pass1.Length >= 5)
            {
                RegPass1.ToolTip = "";
                RegPass1.Background = Brushes.Transparent;
            }

            if (pass1 == pass2)
            {
                RegPass2.ToolTip = "";
                RegPass2.Background = Brushes.Transparent;
            }

            User user = new User(login, pass1, status);

            User regUser = null;
            using (AppContextUser context = new AppContextUser())
            {
                regUser = context.Users.Where(b => b.Login == login).FirstOrDefault();
            }
            if (regUser != null)
            {
                RegLogin.ToolTip = "Цей логін вже зайнято";
                RegLogin.Background = Brushes.DarkRed;
            }
            if (regUser == null)
            {
                RegLogin.ToolTip = "";
                RegLogin.Background = Brushes.Transparent;
            }

            if (login.Length >= 5 && pass1.Length >= 5 && pass1 == pass2 && regUser == null)
            {
                RegisterMenu.Visibility = Visibility.Collapsed;
                LoginMenu.Visibility = Visibility.Visible;

                //Логика добавления пользователей

                RegLogin.Text = "";
                RegPass1.Password = "";
                RegPass2.Password = "";

                using (AppContextUser context = new AppContextUser())
                {
                    regUser = context.Users.Where(b => b.Login == login).FirstOrDefault();
                }
                if (regUser != null)
                {
                    RegLogin.ToolTip = "Цей логін вже зайнято";
                    RegLogin.Background = Brushes.DarkRed;
                }
                else
                {
                    RegLogin.ToolTip = "";
                    RegLogin.Background = Brushes.Transparent;
                    dbUser.Users.Add(user);
                    dbUser.SaveChanges();

                    MessageBox.Show("Користувач успішно зарегистрований");
                }


            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
               
            string login = textBoxLogin.Text.Trim();
            string pass = textBoxPass.Password.Trim();

            User authUser = null;
            using (AppContextUser context = new AppContextUser())
            {
                authUser = context.Users.Where(b => b.Login == login && b.Pass == pass).FirstOrDefault();
            }

            if (authUser != null)
            {
                textBoxLogin.Text = "";
                textBoxPass.Password = "";
                FirstMenu.Visibility = Visibility.Collapsed;
                GrayBackground.Visibility = Visibility.Collapsed;
                BuyIt.Visibility = Visibility.Visible; 
                MessageBox.Show("Ви вдало авторизувались");
                               
                if (authUser.Status == "admin")
                {
                    ToCreateButton.Visibility = Visibility.Visible;
                    DeleteThisDeal.Visibility = Visibility.Visible;
                }
                else
                {
                    ToCreateButton.Visibility = Visibility.Collapsed;
                    DeleteThisDeal.Visibility = Visibility.Collapsed;
                }

                UnLogUser.Visibility = Visibility.Collapsed;
                LogUser.Visibility = Visibility.Visible;

            }
            else
            {
                MessageBox.Show("Невірний логін або пароль");
            }

        }

        private void AddProposal_Click(object sender, RoutedEventArgs e)
        {
            string Name = TypeOfName.Text.Trim();
            string Body = TypeOfBody.Text.Trim();
            string Color = TypeOfColor.Text.Trim();
            string Drive = TypeOfDrive.Text.Trim();
            string Fuel = TypeOfFuel.Text.Trim();
            string Engine = TypeOfEngine.Text.Trim();
            string Price = TypeOfPrice.Text.Trim();
            string Odometr = TypeOfOdometr.Text.Trim();
            string Notes = TypeOfNotes.Text.Trim();

            if (Name == "")
            {
                TypeOfName.ToolTip = "Поле обов'язкове до заповнення";
                TypeOfName.Background = Brushes.DarkRed;
            }
            if (Body == "")
            {
                TypeOfBody.ToolTip = "Поле обов'язкове до заповнення";
                TypeOfBody.Background = Brushes.DarkRed;
            }
            if (Color == "")
            {
                TypeOfColor.ToolTip = "Поле обов'язкове до заповнення";
                TypeOfColor.Background = Brushes.DarkRed;
            }
            if (Drive == "")
            {
                TypeOfDrive.ToolTip = "Поле обов'язкове до заповнення";
                TypeOfDrive.Background = Brushes.DarkRed;
            }
            if (Fuel == "")
            {
                TypeOfFuel.ToolTip = "Поле обов'язкове до заповнення";
                TypeOfFuel.Background = Brushes.DarkRed;
            }
            if (Engine == "")
            {
                TypeOfEngine.ToolTip = "Поле обов'язкове до заповнення";
                TypeOfEngine.Background = Brushes.DarkRed;
            }
            if (Price == "")
            {
                TypeOfPrice.ToolTip = "Поле обов'язкове до заповнення";
                TypeOfPrice.Background = Brushes.DarkRed;
            }
            if (Odometr == "")
            {
                TypeOfOdometr.ToolTip = "Поле обов'язкове до заповнення";
                TypeOfOdometr.Background = Brushes.DarkRed;
            }
            if (Notes == "")
            {
                TypeOfNotes.ToolTip = "Поле обов'язкове до заповнення";
                TypeOfNotes.Background = Brushes.DarkRed;
            }


            if (Name != "")
            {
                TypeOfName.ToolTip = "";
                TypeOfName.Background = Brushes.Transparent;
            }
            if (Body != "")
            {
                TypeOfBody.ToolTip = "";
                TypeOfBody.Background = Brushes.Transparent;
            }
            if (Color != "")
            {
                TypeOfColor.ToolTip = "";
                TypeOfColor.Background = Brushes.Transparent;
            }
            if (Drive != "")
            {
                TypeOfDrive.ToolTip = "";
                TypeOfDrive.Background = Brushes.Transparent;
            }
            if (Fuel != "")
            {
                TypeOfFuel.ToolTip = "";
                TypeOfFuel.Background = Brushes.Transparent;
            }
            if (Engine != "")
            {
                TypeOfEngine.ToolTip = "";
                TypeOfEngine.Background = Brushes.Transparent;
            }
            if (Price != "")
            {
                TypeOfPrice.ToolTip = "";
                TypeOfPrice.Background = Brushes.Transparent;
            }
            if (Odometr != "")
            {
                TypeOfOdometr.ToolTip = "";
                TypeOfOdometr.Background = Brushes.Transparent;
            }
            if (Notes != "")
            {
                TypeOfNotes.ToolTip = "";
                TypeOfNotes.Background = Brushes.Transparent;
            }


            for (int i = 0; i < Engine.Length; i++)
            {
                if (Engine[i] >= '0' && Engine[i] <= '9' && Engine.Length <= 7)
                {
                    TypeOfEngine.ToolTip = "";
                    TypeOfEngine.Background = Brushes.Transparent;
                }
                else
                {
                    TypeOfEngine.ToolTip = "До вводу дозволено лише цифри";
                    TypeOfEngine.Background = Brushes.DarkRed;
                    break;
                }
            }

            for (int i = 0; i < Price.Length; i++)
            {
                if (Price.Length <= 10 && Price[i] >= '0' && Price[i] <= '9')
                {
                    TypeOfPrice.ToolTip = "";
                    TypeOfPrice.Background = Brushes.Transparent;
                }
                else
                {
                    TypeOfPrice.ToolTip = "До вводу дозволено лише цифри";
                    TypeOfPrice.Background = Brushes.DarkRed;
                    break;
                }
            }

            for (int i = 0; i < Odometr.Length; i++)
            {
                if (Odometr.Length <= 10 && Odometr[i] >= '0' && Odometr[i] <= '9')
                {
                    TypeOfOdometr.ToolTip = "";
                    TypeOfOdometr.Background = Brushes.Transparent;
                }
                else
                {
                    TypeOfOdometr.ToolTip = "До вводу дозволено лише цифри";
                    TypeOfOdometr.Background = Brushes.DarkRed;
                    break;
                }
            }

            if (TypeOfEngine.ToolTip.ToString() == "" && TypeOfPrice.ToolTip.ToString() == "" && TypeOfName.ToolTip.ToString() == "" && TypeOfOdometr.ToolTip.ToString() == "")
            {
                int engine = Convert.ToInt32(Engine);
                int odometr = Convert.ToInt32(Odometr);
                int price = Convert.ToInt32(Price);
                Deal deal = new Deal(Name, engine, odometr, Fuel, Color, Body, Drive, Notes, price);

                dbDeal.Deals.Add(deal);
                dbDeal.SaveChanges();
                deals.Add(deal);              
                            
                MessageBox.Show("Об'ява вдало додана");


                TypeOfName.Text = "";
                TypeOfName.Text = "";
                TypeOfBody.Text = "";
                TypeOfColor.Text = "";
                TypeOfDrive.Text = "";
                TypeOfFuel.Text = "";
                TypeOfEngine.Text = "";
                TypeOfPrice.Text = "";
                TypeOfOdometr.Text = "";
                TypeOfNotes.Text = "";
            }
        }

        Deal SelectedDeal { get; set; }

        private void DealMenuButton_Click(object sender, RoutedEventArgs e)
        {

            SelectedDeal = (Deal)this.ListOfDeals.SelectedItem;
            ShowSelectedDeal.DataContext = SelectedDeal;
            GrayBackground.Visibility = Visibility.Visible;
            ShowSelectedDeal.Visibility = Visibility.Visible;
          
            
        }

        private void SkipSelectionDeal_Click(object sender, RoutedEventArgs e)
        {
           ShowSelectedDeal.Visibility = Visibility.Collapsed;
            GrayBackground.Visibility = Visibility.Collapsed;
        }

        private void BuyIt_Click(object sender, RoutedEventArgs e)
        {
            deals.Remove(SelectedDeal);
            using (AppContextDeal db = new AppContextDeal())
            {
                Deal deal = db.Deals.FirstOrDefault(x => x.id == SelectedDeal.id);

                db.Deals.Remove(deal);
                db.SaveChanges();
            }

            MessageBox.Show("Ви вдало виконали купівлю");
            GrayBackground.Visibility = Visibility.Collapsed;
            ShowSelectedDeal.Visibility = Visibility.Collapsed;
        }

        private void DeleteThisDeal_Click(object sender, RoutedEventArgs e)
        {
            deals.Remove(SelectedDeal);
            using (AppContextDeal db = new AppContextDeal())
            {
                Deal deal = db.Deals.FirstOrDefault(x=>x.id == SelectedDeal.id);

                db.Deals.Remove(deal);
                db.SaveChanges();
            }
            GrayBackground.Visibility = Visibility.Collapsed;
            ShowSelectedDeal.Visibility = Visibility.Collapsed;
        }

        private void UnLog_Click(object sender, RoutedEventArgs e)
        {            
            UnLogUser.Visibility = Visibility.Visible;
            LogUser.Visibility = Visibility.Collapsed;
            ToCreateButton.Visibility = Visibility.Collapsed;
            DeleteThisDeal.Visibility = Visibility.Collapsed;
            BuyIt.Visibility = Visibility.Collapsed;
        }
              
    }
}
