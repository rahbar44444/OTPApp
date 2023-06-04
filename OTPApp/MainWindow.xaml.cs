using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OTPApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string CurrentOTP = string.Empty;
        private int _Count=0;

        public int Count
        {
            get { return _Count; }
            set 
            { 
                _Count = value; 
                if(Count>=10)
                {
                    MessageBox.Show("OTP is wrong after 10 tries.", "STATUS_OTP_FAIL");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private async void SendOTP_Click(object sender, RoutedEventArgs e)
        {
           await SendOTP();
        }

        private void VerifyOTP_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(CurrentOTP))
            {
                var mainVM = this.DataContext as MainViewModel;
                if(mainVM !=null)
                {
                    if (mainVM.IsTimeOut == true) 
                    {
                        MessageBox.Show("Timeout after 1 min.", "STATUS_OTP_TIMEOUT");
                        mainVM.UnSubscribeTimer();
                        mainVM.OTP = string.Empty;
                    }
                    else if (mainVM.OTP.Equals(CurrentOTP))
                    {
                        MessageBox.Show("OTP is valid and checked.", "STATUS_OTP_OK");
                        mainVM.UnSubscribeTimer();
                        mainVM.OTP = string.Empty;
                    }
                    else
                    {
                        Count++;
                    }
                }
            }
        }

        private async Task SendOTP()
        {
            var mainVM = this.DataContext as MainViewModel;
            if (mainVM != null)
            {
                mainVM.IsTimeOut = false;
                mainVM.OTP = string.Empty;
                string[] allowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

                CurrentOTP = GenerateRandomOTP(6, allowedCharacters);

                var senderConfig = new SmtpSender(() => new SmtpClient("localhost")
                {
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Port = 25
                    //DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                    //PickupDirectoryLocation = @"C:\Demos"
                });

                StringBuilder template = new();
                template.AppendLine("Hi there,");
                template.AppendLine("<p>You OTP Code is @Model.OTP. The code is valid for \r\n1 minute.</p>");
                template.AppendLine("- The DSO Team");

                Email.DefaultSender = senderConfig;
                Email.DefaultRenderer = new RazorRenderer();

                var email = await Email
                    .From("rahbar@test.com")
                    .To(mainVM.Email, "DSO")
                    .Subject("OTP")
                    .UsingTemplate(template.ToString(), new { OTP = CurrentOTP })
                    .SendAsync();//.ConfigureAwait(true).GetAwaiter().GetResult();
                mainVM.SetTimer();
            }
        }

        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {
            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[1-9][0-9]{0,9}$");
            //e.Handled = regex.IsMatch(e.Text);
            if (regex.IsMatch(e.Text) == true)
                e.Handled = false;
            else if(e.Text == "0")
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                string text = (string)e.DataObject.GetData(typeof(String));
                Regex regex = new Regex("[^0-9]+");
                if (regex.IsMatch(text) == true)
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9)
              || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
              || e.Key == Key.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}