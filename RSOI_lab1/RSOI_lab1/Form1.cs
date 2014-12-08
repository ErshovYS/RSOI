using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using System.IO;

namespace RSOI_lab1
{
    public partial class Form1 : Form
    {
        enum InitState { None, Code, Token };
        InitState selfstate = InitState.None;

        void SetState(InitState newstate)
        {
            switch (selfstate = newstate)
            {
                case InitState.None:
                    SendAuthRequest();
                    break;
                case InitState.Code:
                    SendTokenRequest();
                    break;
                case InitState.Token:
                    DoSomethingWithToken();
                    break;                    
            }
        }

        string client_id = "756a4ae199514bfc99ee0f6ba62443df";
        string client_secret = "d8be7494941c48e7a792b82e7186215d";
        string redirectUrl = "http://localhost";

        string sessionID;
        Random r = new Random();
        StreamWriter SW = new StreamWriter("out.txt");

        string code;
        string token;
        string access_token;
        string expires_in;

        private void SendAuthRequest()
        {
            webControl1.Source = new Uri("https://oauth.yandex.ru/authorize"
                                            + "?response_type=" + "code"
                                            + "&client_id=" + client_id
            );
        }

        private void SendTokenRequest()
        {
            // почитать c# send post request
            var rc = new RestClient("https://oauth.yandex.ru/token");
            var rq = new RestRequest(Method.POST);
            rq.AddParameter("grant_type", "authorization_code");
            rq.AddParameter("code", code);
            rq.AddParameter("client_id", client_id);
            rq.AddParameter("client_secret", client_secret);

            var response_all = rc.Execute(rq);
            var response_raw = response_all.Content;

            var response = response_raw
                .Replace("\"", "")
                .Split(new char[] { ',', '{', '}', ':' },
                StringSplitOptions.RemoveEmptyEntries);
            if (response.Length > 0)
            {
                if (response[0] == "error")
                {
                    MessageBox.Show("Не получен access-токен");
                    SetState(InitState.None);
                }
                else
                {
                    token = response_raw;
                    access_token = response[3].Trim();
                    expires_in = response[4];
                    SW.WriteLine("access_token: " + access_token);
                    SW.WriteLine("expires_in: " + expires_in);
                    SetState(InitState.Token);
                }
            }
        }

        private void DoSomethingWithToken()
        {
            var rc = new RestClient("https://login.yandex.ru/info");
            var rq = new RestRequest();
            rq.AddParameter("format", "json");
            rq.AddParameter("oauth_token", access_token);
            
            var res = rc.Execute(rq);
            SW.WriteLine("Login info:");
            SW.WriteLine(res.Content);
            SW.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Awesomium_Windows_Forms_WebControl_TargetURLChanged(object sender, Awesomium.Core.UrlEventArgs e)
        {
            if (webControl1.Source.AbsoluteUri.StartsWith(redirectUrl) && selfstate == InitState.None)
            {
                string s = webControl1.Source.AbsoluteUri.Split(new string[] { "localhost" }, StringSplitOptions.None).ToArray()[1];
                var parvals = s.Split(new char[] { '/', '?', '=', '&' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                if (parvals[0] == "code")
                {
                    MessageBox.Show("Авторизация прошла успешно");
                    code = parvals[1];
                    SW.WriteLine("code: " + code);
                    SetState(InitState.Code);
                }
                else
                    if (parvals[0] == "error")
                    {
                        MessageBox.Show("Ошибка авторизации: \r\n" + parvals[3]);
                        SetState(InitState.None);
                    }
                    else
                    {
                        MessageBox.Show("Что-то пошло не так: \r\n" + webControl1.Source.AbsoluteUri);
                        SetState(InitState.None);
                    }
            }
        }

        private void Awesomium_Windows_Forms_WebControl_CertificateError(object sender, Awesomium.Core.CertificateErrorEventArgs e)
        {
            if (e.Url.OriginalString.StartsWith("http://yandex.ru"))
            {
                e.Handled = Awesomium.Core.EventHandling.Modal;
                e.Ignore = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sessionID = r.Next().ToString();
            SetState(InitState.None);
        }
    }
}
