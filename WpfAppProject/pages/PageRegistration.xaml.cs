using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        TableUsers user;
        bool newUser;
        public PageRegistration()
        {
            InitializeComponent();
            newUser = true;
            CmbRoles.Items.Add("Все роли");
            List<TableRoles> listRoles = ClassBase.Base.TableRoles.ToList();
            foreach (TableRoles role in listRoles)
            {
                CmbRoles.Items.Add(role.RoleName);
            }
            
        }

        public PageRegistration(TableUsers user)
        {
            InitializeComponent();
            this.user = user;
            newUser = false;
            TbSurname.Text = user.UserSurname;
            TbName.Text = user.UserName;
            TbPatronymic.Text = user.UserPatronymic;
            DpBirthday.SelectedDate = user.UserBirthday;
            TbPhone.Text = user.UserPhone;
            TbLogin.Text = user.UserLogin;
            PbPassword.Text = user.UserPassword.ToString();
            CmbRoles.Items.Add("Все роли");
            List<TableRoles> listRoles = ClassBase.Base.TableRoles.ToList();
            foreach(TableRoles role in listRoles)
            {
                CmbRoles.Items.Add(role.RoleName);
            }
            CmbRoles.SelectedValue = user.TableRoles.RoleName;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Regex r1 = new Regex("^[8][(][9](\\d{2})[)]\\d{3}-\\d{2}-\\d{2}$");
            if(r1.IsMatch(TbPhone.Text))
            {
                Regex r2 = new Regex("[0-9].*[0-9].*[0-9]");
                Regex r3 = new Regex("[a-z].*[a-z].*[a-z]");
                Regex r4 = new Regex("[A-Z]");
                Regex r5 = new Regex("[!@#$%^&*]");
                if(newUser == true)
                {
                    if (r2.IsMatch(PbPassword.Text) && r3.IsMatch(PbPassword.Text) && r4.IsMatch(PbPassword.Text) && r5.IsMatch(PbPassword.Text))
                    {
                        if (newUser == true)
                        {
                            user = new TableUsers();
                        }
                        user.UserSurname = TbSurname.Text;
                        user.UserName = TbName.Text;
                        user.UserPatronymic = TbPatronymic.Text;
                        user.UserBirthday = DpBirthday.SelectedDate.Value;
                        user.UserPhone = TbPhone.Text;
                        user.UserLogin = TbLogin.Text;
                        if (newUser == true)
                        {
                            user.UserPassword = PbPassword.Text.GetHashCode();
                        }
                        else
                        {
                            user.UserPassword = Convert.ToInt32(PbPassword.Text);
                        }
                        TableRoles role = ClassBase.Base.TableRoles.FirstOrDefault(z => z.RoleName == CmbRoles.SelectedItem.ToString());
                        user.UserRole = role.RoleId;
                        if (newUser == true)
                        {
                            ClassBase.Base.TableUsers.Add(user);
                        }
                        ClassBase.Base.SaveChanges();
                        classes.ClassFrame.mainFrame.Navigate(new PageAuto());
                    }
                    else
                    {
                        MessageBox.Show("Пароль");
                    }
                }
                else
                {
                    if (newUser == true)
                    {
                        user = new TableUsers();
                    }
                    user.UserSurname = TbSurname.Text;
                    user.UserName = TbName.Text;
                    user.UserPatronymic = TbPatronymic.Text;
                    user.UserBirthday = DpBirthday.SelectedDate.Value;
                    user.UserPhone = TbPhone.Text;
                    user.UserLogin = TbLogin.Text;
                    if (newUser == true)
                    {
                        user.UserPassword = PbPassword.Text.GetHashCode();
                    }
                    else
                    {
                        user.UserPassword = Convert.ToInt32(PbPassword.Text);
                    }
                    TableRoles role = ClassBase.Base.TableRoles.FirstOrDefault(z => z.RoleName == CmbRoles.SelectedItem.ToString());
                    user.UserRole = role.RoleId;
                    if (newUser == true)
                    {
                        ClassBase.Base.TableUsers.Add(user);
                    }
                    ClassBase.Base.SaveChanges();
                    classes.ClassFrame.mainFrame.Navigate(new PageAuto());
                }
                
            }
            else
            {
                MessageBox.Show("Номер");
            }
        }
    }
}
