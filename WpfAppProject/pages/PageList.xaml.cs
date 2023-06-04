using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppProject.pages
{
    /// <summary>
    /// Логика взаимодействия для PageList.xaml
    /// </summary>
    public partial class PageList : Page
    {
        TableUsers user;
        public PageList(TableUsers user)
        {
            InitializeComponent();
            this.user = user;
            lvUsers.ItemsSource = ClassBase.Base.TableUsers.ToList();
            CmbSortRole.Items.Add("Все роли");
            List<TableRoles> listRole = ClassBase.Base.TableRoles.ToList();
            foreach(TableRoles role in listRole)
            {
                CmbSortRole.Items.Add(role.RoleName);
            }
        }

        private void BtnUpdate_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if(user.TableRoles.RoleName == "админ")
            {
                btn.Visibility = Visibility.Visible;
            }
            else
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnDelete_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (user.TableRoles.RoleName != "пользователь")
            {
                btn.Visibility = Visibility.Visible;
            }
            else
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            TableUsers updateUser = ClassBase.Base.TableUsers.FirstOrDefault(z => z.UserId == index);
            classes.ClassFrame.mainFrame.Navigate(new PageRegistration(updateUser));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            switch(MessageBox.Show("Удалить?", "Сообщение", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    TableUsers deleteUser = ClassBase.Base.TableUsers.FirstOrDefault(z => z.UserId == index);
                    ClassBase.Base.TableUsers.Remove(deleteUser);
                    ClassBase.Base.SaveChanges();
                    classes.ClassFrame.mainFrame.Navigate(new pages.PageList(user));
                    break;
                default:
                    break;
            }
        }

        private void TbSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void CmbSortRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        public void Filter()
        {
            List<TableUsers> listFilter = ClassBase.Base.TableUsers.ToList();

            if(!string.IsNullOrWhiteSpace(TbSearch.Text))
            {
                listFilter = listFilter.Where(z => z.FIO.ToLower().Contains(TbSearch.Text.ToLower()) || z.TableRoles.RoleName.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
            }

            switch(CmbSort.SelectedIndex)
            {
                case 1:
                    //listFilter = listFilter.OrderBy(z => z.FIO).ToList();
                    listFilter.Sort((x, y) => x.FIO.CompareTo(y.FIO));
                    break;
                case 2:
                    // listFilter = listFilter.OrderByDescending(z=> z.FIO).ToList();
                    listFilter.Sort((x, y) => x.FIO.CompareTo(y.FIO));
                    listFilter.Reverse();
                    break;
                default:
                    break;
            }

            if(CmbSortRole.SelectedIndex>0)
            {

                listFilter = listFilter.Where(z => z.TableRoles.RoleName == CmbSortRole.SelectedItem.ToString()).ToList();
            }

            if(dpSearch.SelectedDate!=null)
            {
                listFilter = listFilter.Where(z => z.UserBirthday == dpSearch.SelectedDate).ToList();
            }

            lvUsers.ItemsSource = listFilter;
        }

        private void dpSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
