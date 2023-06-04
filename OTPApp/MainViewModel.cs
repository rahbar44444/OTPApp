using FluentEmail.Core;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace OTPApp
{
    public class MainViewModel : BindableBase, IDataErrorInfo
    {
        private string _Email;
        private string _OTP =null;
        private bool _IsSendOTPEnable = false;
        private bool _IsVerifyOTPEnable = false;
        public bool IsTimeOut = false;
        private static System.Timers.Timer Timer;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; RaisePropertyChanged(); }
        }

        public string OTP
        {
            get { return _OTP; }
            set { _OTP = value; RaisePropertyChanged(); }
        }

        public bool IsSendOTPEnable
        {
            get { return _IsSendOTPEnable; }
            set { _IsSendOTPEnable = value; RaisePropertyChanged(); }
        }

        public bool IsVerifyOTPEnable
        {
            get { return _IsVerifyOTPEnable; }
            set { _IsVerifyOTPEnable = value; RaisePropertyChanged(); }
        }


        public MainViewModel()
        {
            IsSendOTPEnable = false;
            IsVerifyOTPEnable = false;
        }

        public void SetTimer()
        {
            // Create a timer with a two second interval.
            Timer = new System.Timers.Timer(60000);
            // Hook up the Elapsed event for the timer. 
            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if(string.IsNullOrEmpty(OTP.ToString()))
            {
                IsTimeOut = true;
                MessageBox.Show("Timeout after 1 min.\", \"STATUS_OTP_TIMEOUT");
                UnSubscribeTimer();
            }
            else if (!(string.IsNullOrEmpty(OTP)) && OTP.ToArray().Count() ==6)
            {
                IsTimeOut = true;
                UnSubscribeTimer();
            }
            else
            {
                UnSubscribeTimer();
            }
        }

        public void UnSubscribeTimer()
        {
            Timer.Elapsed -= OnTimedEvent;
            Timer.Close();
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(Email):
                        if (!string.IsNullOrEmpty(Email))
                        {
                            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                            Match match = regex.Match(Email);
                            if (match.Success)
                            {
                                MailAddress address = new MailAddress(Email);
                                string host = address.Host;
                                if (host != "dso.org.sg")
                                {
                                    error = "Email doesn't belongs to correct domain.";
                                    IsSendOTPEnable = false;
                                }
                                else
                                {
                                    IsSendOTPEnable = true;
                                }
                            }
                            else
                            {
                                error = "Enter email in correct format.";
                                IsSendOTPEnable = false;
                            }
                        }
                        else if(string.IsNullOrEmpty(Email))
                        {
                            IsSendOTPEnable = false;
                        }
                        else
                        {
                            IsSendOTPEnable = true;
                        }
                        break;

                    case nameof(OTP):
                        if (!String.IsNullOrEmpty(OTP))
                        {
                            var otpValue = Int32.Parse(OTP);
                            if (otpValue == 000000)
                            {
                                error = "000000 is not a valid OTP. Please enter valid OTP.";
                                IsVerifyOTPEnable = false;
                            }
                            else if (OTP.ToArray().Count() <= 5)
                            {
                                error = "Please enter valid OTP.";
                                IsVerifyOTPEnable = false;
                            }
                            else
                            {
                                IsVerifyOTPEnable = true;
                            }
                        }
                        else
                        {
                            IsVerifyOTPEnable = false;
                        }
                        break;
                }

                return error;
            }
        }

        public string Error => string.Empty;
    }
}
