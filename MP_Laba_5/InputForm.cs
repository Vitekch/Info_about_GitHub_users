using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
namespace MP_Laba_5
{
 
    class InputForm:Window
    {
      [UI] private Button _confirm = null;
        [UI] private Entry _entry = null;
        [UI] private Label _load = null;
        [UI] private Label label1 = null;
        [UI] private Image _img = null;
        
        public InputForm() : this(new Builder("InputForm.glade")) { }

        private InputForm(Builder builder) : base(builder.GetObject("InputForm").Handle)
        {
            
            builder.Autoconnect(this);
           
            DeleteEvent += Window_DeleteEvent;
             _confirm.Clicked += Confirm_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private async void Confirm_Clicked(object sender,EventArgs e)
        {
            
            HttpClient client=new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            string find_usr = _entry.Text;
            _load.Text = "Load...";
            HttpResponseMessage response = await client.GetAsync("https://api.github.com/users/"+find_usr);
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json_obj = JObject.Parse(responseBody);
            if(json_obj["login"]== null)
            {
                _load.Text = "Пользователя не существует";

            }
            else { 
                string login = json_obj["login"].ToString();
                string name = json_obj["name"].ToString();
                string email = json_obj["location"].ToString();
                string link = json_obj["html_url"].ToString();
                string avatar = json_obj["avatar_url"].ToString();
                var win = new UserInfo(login,name,email,link,avatar);
            

                win.Show();

                _load.Text = "";
            }
        }

        

    }
}
