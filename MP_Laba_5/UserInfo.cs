using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace MP_Laba_5
{

    class UserInfo : Window
    {
        [UI] private Label _login = null;
        [UI] private Label _name = null;
        [UI] private Label _loc= null;
        [UI] private LinkButton _link = null;
        [UI] private Image _avatar = null;
        [UI] private LinkButton _repo_1 = null;
        [UI] private LinkButton _repo_2 = null;
        [UI] private LinkButton _repo_3 = null;
        [UI] private LinkButton _repo_4 = null;
        [UI] private Button _next_repos = null;
        [UI] private Button _prev_repos = null;

        [UI] private Button _comm_1 = null;
        [UI] private Button _comm_2 = null;
        [UI] private Button _comm_3 = null;
        [UI] private Button _comm_4 = null;

        private JArray json_obj;
        private int i=0;
        private int page;

        private string repo;
        private async void getRepos(string lgn)
        {
            HttpClient client_http = new HttpClient();
            client_http.DefaultRequestHeaders.Add("User-Agent", "C# App");


            HttpResponseMessage response = await client_http.GetAsync("https://api.github.com/users/" + lgn + "/repos");
            string responseBody = await response.Content.ReadAsStringAsync();
            json_obj = JArray.Parse(responseBody);

            int count = json_obj.Count;

            i = count / 4;
            if (count > 3)
            {
                _repo_1.Uri = json_obj[0]["html_url"].ToString();
                _repo_1.Label = json_obj[0]["name"].ToString();
                
                
                _repo_2.Uri = json_obj[1]["html_url"].ToString();
                _repo_2.Label = json_obj[1]["name"].ToString();
                i++;
                _repo_3.Uri = json_obj[2]["html_url"].ToString();
                _repo_3.Label = json_obj[2]["name"].ToString();
                i++;
                _repo_4.Uri = json_obj[3]["html_url"].ToString();
                _repo_4.Label = json_obj[3]["name"].ToString();


            }
            else
            {
                for (int temp=0; temp< count; temp++)
                {
                    if (temp + 1 % 4 == 1)
                    {
                        _repo_1.Uri = json_obj[temp]["html_url"].ToString();
                        _repo_1.Label = json_obj[temp]["name"].ToString();
                    }
                    else if (temp + 1 % 4 == 2)
                    {
                        _repo_2.Uri = json_obj[temp]["html_url"].ToString();
                        _repo_2.Label = json_obj[temp]["name"].ToString();
                    }
                    else if (temp + 1 % 4 == 3)
                    {
                        _repo_3.Uri = json_obj[temp]["html_url"].ToString();
                        _repo_3.Label = json_obj[temp]["name"].ToString();
                    }
                    else if (temp + 1 % 4 == 0)
                    {
                        _repo_4.Uri = json_obj[temp]["html_url"].ToString();
                        _repo_4.Label = json_obj[temp]["name"].ToString();
                    }

                }
            }
            page = 0;
        }

        public UserInfo(string lgn,string nm,string lc,string lnk,string avtr) : this(new Builder("UserInfo.glade")) {
            _login.Text = lgn;
            _name.Text = nm;
            _loc.Text = lc;
            _link.Uri = lnk;
            _link.Label = lnk;
            WebClient client = new WebClient();
            client.DownloadFile(avtr, "temp.png");

            _avatar.FromFile = "temp.png";
            _avatar.FromPixbuf=_avatar.Pixbuf.ScaleSimple(50, 50, Gdk.InterpType.Bilinear);

            getRepos(lgn);
            System.IO.File.Delete("temp.png");



           

        }

        private UserInfo(Builder builder) : base(builder.GetObject("UserInfo").Handle)
        {
            builder.Autoconnect(this);

            _next_repos.Clicked += Next_Button;
            _prev_repos.Clicked += Prev_Button;

            _comm_1.Clicked += Comm1_Btn;
            _comm_2.Clicked += Comm2_Btn;
            _comm_3.Clicked += Comm3_Btn;
            _comm_4.Clicked += Comm4_Btn;

        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
        
        private void Next_Button(object sunder, EventArgs e)
        {
            if (page < i-2)
            {
                page++;
                if (page*4<json_obj.Count)
                {
                    _repo_1.Uri = json_obj[page * 4]["html_url"].ToString();
                    _repo_1.Label = json_obj[page * 4]["name"].ToString();
                }
                else
                {
                    _repo_1.Uri = "";
                    _repo_1.Label = "";
                }
                if (page * 4 + 1 < json_obj.Count)
                {
                    _repo_2.Uri = json_obj[page * 4 + 1]["html_url"].ToString();
                    _repo_2.Label = json_obj[page * 4 + 1]["name"].ToString();
                }
                else
                {
                    _repo_2.Uri = "";
                    _repo_2.Label = "";
                }
                if (page * 4 + 2 < json_obj.Count)
                {
                    _repo_3.Uri = json_obj[page * 4 + 2]["html_url"].ToString();
                    _repo_3.Label = json_obj[page * 4 + 2]["name"].ToString();
                }
                else
                {
                    _repo_3.Uri = "";
                    _repo_3.Label = "";
                }
                if (page * 4 + 3 < json_obj.Count)
                {
                    _repo_4.Uri = json_obj[page * 4 + 3]["html_url"].ToString();
                    _repo_4.Label = json_obj[page * 4 + 3]["name"].ToString();
                }
                else
                {
                    _repo_4.Uri = "";
                    _repo_4.Label = "";
                }
                
            }
            
           
        } 

        private void Prev_Button(object sender,EventArgs e)
        {
            int count = json_obj.Count;
            if (page >0)
            {
                page--;
                if (page * 4 < json_obj.Count)
                {
                    _repo_1.Uri = json_obj[page * 4]["html_url"].ToString();
                    _repo_1.Label = json_obj[page * 4]["name"].ToString();
                }
                else
                {
                    _repo_1.Uri = "";
                    _repo_1.Label = "";
                }
                if (page * 4 + 1 < json_obj.Count)
                {
                    _repo_2.Uri = json_obj[page * 4 + 1]["html_url"].ToString();
                    _repo_2.Label = json_obj[page * 4 + 1]["name"].ToString();
                }
                else
                {
                    _repo_2.Uri = "";
                    _repo_2.Label = "";
                }
                if (page * 4 + 2 < json_obj.Count)
                {
                    _repo_3.Uri = json_obj[page * 4 + 2]["html_url"].ToString();
                    _repo_3.Label = json_obj[page * 4 + 2]["name"].ToString();
                }
                else
                {
                    _repo_3.Uri = "";
                    _repo_3.Label = "";
                }
                if (page * 4 + 3 < json_obj.Count)
                {
                    _repo_4.Uri = json_obj[page * 4 + 3]["html_url"].ToString();
                    _repo_4.Label = json_obj[page * 4 + 3]["name"].ToString();
                }
                else
                {
                    _repo_4.Uri = "";
                    _repo_4.Label = "";
                }
                
            }

        }
       
        private void Comm1_Btn(object sunder,EventArgs e)
        {
           
            var win = new Commits(_login.Text,_repo_1.Label);
            win.Show();
        }
        private void Comm2_Btn(object sunder, EventArgs e)
        {
            var win = new Commits(_login.Text, _repo_2.Label);
            win.Show();
        }
        private void Comm3_Btn(object sunder, EventArgs e)
        {
            var win = new Commits(_login.Text, _repo_3.Label);
            win.Show();
        }
        private void Comm4_Btn(object sunder, EventArgs e)
        {
            var win = new Commits(_login.Text, _repo_4.Label);
            win.Show();
        }


    }
}