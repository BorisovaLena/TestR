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
using WpfAppProject.classes;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;

namespace WpfAppProject.pages
{
    /// <summary>
    /// Логика взаимодействия для PageAuto.xaml
    /// </summary>
    public partial class PageAuto : Page
    {
        public PageAuto()
        {
            InitializeComponent();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            int i = TbPassword.Text.GetHashCode();
            TableUsers user = ClassBase.Base.TableUsers.FirstOrDefault(z => z.UserLogin == TbLogin.Text && z.UserPassword == i);
            if(user!= null)
            {
                ClassFrame.mainFrame.Navigate(new PageList(user));
            }
            else
            {
                MessageBox.Show("Нет такого пользователя", "Сообщение");
            }
        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.mainFrame.Navigate(new PageRegistration());
        }

        private void BtnPDF_Click(object sender, RoutedEventArgs e)
        {
            List<TableUsers> listUsers = ClassBase.Base.TableUsers.ToList();
            int height = 0;
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Заголовок документа";
            PdfPage page = document.AddPage();
            XGraphics graphics = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont("Comic Sans MS", 18, XFontStyle.Bold);
            graphics.DrawString("Заголовок заголовок", fontHeader, XBrushes.Black, new XRect(10, height, page.Width, page.Height), XStringFormats.TopCenter);
            height += 30;
            XFont fontText = new XFont("Comic Sans MS", 14);
            foreach (TableUsers user in listUsers)
            {
                graphics.DrawString("ФИО: " + user.FIO, fontText, XBrushes.Black, new XRect(10, height, page.Width, page.Height), XStringFormats.TopLeft);
                height += 30;
                graphics.DrawString("Дата рождения: " + user.UserBirthday.ToString("D"), fontText, XBrushes.Black, new XRect(10, height, page.Width, page.Height), XStringFormats.TopLeft);
                height += 30;
                graphics.DrawString("Роль: " + user.TableRoles.RoleName, fontText, XBrushes.Black, new XRect(10, height, page.Width, page.Height), XStringFormats.TopLeft);
                height += 30;
                graphics.DrawString("Номер телефона: " + user.UserPhone, fontText, XBrushes.Black, new XRect(10, height, page.Width, page.Height), XStringFormats.TopLeft);
                height += 30;
            }
            string filename = "TicketPDF.pdf";
            document.Save(filename);
            Process.Start(filename);
        }

        private void CbCb_Checked(object sender, RoutedEventArgs e)
        {
            gbPP.Visibility = Visibility.Collapsed;
            TbPassword.Text = PbPassword.Password;
            gbP.Visibility = Visibility.Visible;
        }

        private void CbCb_Unchecked(object sender, RoutedEventArgs e)
        {
            PbPassword.Password = TbPassword.Text;
            gbPP.Visibility = Visibility.Visible;
            gbP.Visibility = Visibility.Collapsed;
        }
    }
}
