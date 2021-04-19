using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
namespace MP_Laba_5
{
    class Commits : Window
    {
        [UI] private Label name_1=null;
        [UI] private Label name_2 = null;
        [UI] private Label name_3= null;
        [UI] private Label name_4 = null;

        [UI] private Label message_1 = null;
        [UI] private Label message_2 = null;
        [UI] private Label message_3 = null;
        [UI] private Label message_4 = null;

        [UI] private Label date_1 = null;
        [UI] private Label date_2 = null;
        [UI] private Label date_3 = null;
        [UI] private Label date_4 = null;

        [UI] private Button next = null;
        [UI] private Button prev = null;


        private JArray json_obj;
        private int i = 0;
        private int page;

        private async void getCommits(string lgn,string repo)
        {
            HttpClient client_http = new HttpClient();
            client_http.DefaultRequestHeaders.Add("User-Agent", "C# App");


            HttpResponseMessage response = await client_http.GetAsync("https://api.github.com/repos/" + lgn + '/'+repo+"/commits");
            string responseBody = await response.Content.ReadAsStringAsync();
            json_obj = JArray.Parse(responseBody);

            int count = json_obj.Count;

            i = count / 4;
            if (count > 3)
            {
                name_1.Text = json_obj[0]["commit"]["author"]["name"].ToString();
                message_1.Text = json_obj[0]["commit"]["message"].ToString();
                date_1.Text= json_obj[0]["commit"]["author"]["date"].ToString();

                name_2.Text = json_obj[1]["commit"]["author"]["name"].ToString();
                message_1.Text = json_obj[1]["commit"]["message"].ToString();
                date_2.Text = json_obj[1]["commit"]["author"]["date"].ToString();
                i++;
                name_3.Text = json_obj[2]["commit"]["author"]["name"].ToString();
                message_3.Text = json_obj[2]["commit"]["message"].ToString();
                date_3.Text = json_obj[2]["commit"]["author"]["date"].ToString();
                i++;
                name_4.Text = json_obj[3]["commit"]["author"]["name"].ToString();
                message_4.Text = json_obj[3]["commit"]["message"].ToString();
                date_4.Text = json_obj[3]["commit"]["author"]["date"].ToString();


            }
            else
            {
                for (int temp = 0; temp < count; temp++)
                {
                    if (temp + 1 % 4 == 1)
                    {
                        name_1.Text = json_obj[temp]["commit"]["author"]["name"].ToString();
                        message_1.Text = json_obj[temp]["commit"]["message"].ToString();
                        date_1.Text = json_obj[temp]["commit"]["author"]["date"].ToString();
                    }
                    else if (temp + 1 % 4 == 2)
                    {
                        name_2.Text = json_obj[temp]["commit"]["author"]["name"].ToString();
                        message_2.Text = json_obj[temp]["commit"]["message"].ToString();
                        date_2.Text = json_obj[temp]["commit"]["author"]["date"].ToString();
                    }
                    else if (temp + 1 % 4 == 3)
                    {
                        name_3.Text = json_obj[temp]["commit"]["author"]["name"].ToString();
                        message_3.Text = json_obj[temp]["commit"]["message"].ToString();
                        date_3.Text = json_obj[temp]["commit"]["author"]["date"].ToString();
                    }
                    else if (temp + 1 % 4 == 0)
                    {
                        name_4.Text = json_obj[temp]["commit"]["author"]["name"].ToString();
                        message_4.Text = json_obj[temp]["commit"]["message"].ToString();
                        date_4.Text = json_obj[temp]["commit"]["author"]["date"].ToString();
                    }

                }
            }
            page = 0;
        }
        public Commits(string lgn,string repo) : this(new Builder("Commits.glade")) {
            getCommits(lgn, repo);
        }

        private Commits(Builder builder) : base(builder.GetObject("Commits").Handle)
        {

            builder.Autoconnect(this);


            next.Clicked += Next_Button;
            prev.Clicked += Prev_Button;
        }



        private void Next_Button(object sunder, EventArgs e)
        {
          if (page < i - 2)
            {
                page++;
                if (page * 4 < json_obj.Count)
                {
                    name_1.Text = json_obj[page * 4]["commit"]["author"]["name"].ToString();
                    message_1.Text = json_obj[page * 4]["commit"]["message"].ToString();
                    date_1.Text = json_obj[page * 4]["commit"]["author"]["date"].ToString();

                }
                else
                {
                    name_1.Text = "";
                    message_1.Text = "";
                    date_1.Text = "";
                }
                if (page * 4+1 < json_obj.Count)
                {
                    name_2.Text = json_obj[page * 4+1]["commit"]["author"]["name"].ToString();
                    message_2.Text = json_obj[page * 4+1]["commit"]["message"].ToString();
                    date_2.Text = json_obj[page * 4+1]["commit"]["author"]["date"].ToString();

                }
                else
                {
                    name_2.Text = "";
                    message_2.Text = "";
                    date_2.Text = "";
                }
                if (page * 4+2 < json_obj.Count)
                {
                    name_3.Text = json_obj[page * 4+2]["commit"]["author"]["name"].ToString();
                    message_3.Text = json_obj[page * 4+2]["commit"]["message"].ToString();
                    date_3.Text = json_obj[page * 4+2]["commit"]["author"]["date"].ToString();

                }
                else
                {
                    name_3.Text = "";
                    message_3.Text = "";
                    date_3.Text = "";
                }
                if (page * 4+3 < json_obj.Count)
                {
                    name_4.Text = json_obj[page * 4+3]["commit"]["author"]["name"].ToString();
                    message_4.Text = json_obj[page * 4+3]["commit"]["message"].ToString();
                    date_4.Text = json_obj[page * 4+3]["commit"]["author"]["date"].ToString();

                }
                else
                {
                    name_4.Text = "";
                    message_4.Text = "";
                    date_4.Text = "";
                }

            }


        }

        private void Prev_Button(object sender, EventArgs e)
        {
            int count = json_obj.Count;
            if (page > 0)
            {
                page--;
                if (page * 4 < json_obj.Count)
                {
                    name_1.Text = json_obj[page * 4]["commit"]["author"]["name"].ToString();
                    message_1.Text = json_obj[page * 4]["commit"]["message"].ToString();
                    date_1.Text = json_obj[page * 4]["commit"]["author"]["date"].ToString();

                }
                else
                {
                    name_1.Text = "";
                    message_1.Text = "";
                    date_1.Text = "";
                }
                if (page * 4 + 1 < json_obj.Count)
                {
                    name_2.Text = json_obj[page * 4 + 1]["commit"]["author"]["name"].ToString();
                    message_2.Text = json_obj[page * 4 + 1]["commit"]["message"].ToString();
                    date_2.Text = json_obj[page * 4 + 1]["commit"]["author"]["date"].ToString();

                }
                else
                {
                    name_2.Text = "";
                    message_2.Text = "";
                    date_2.Text = "";
                }
                if (page * 4 + 2 < json_obj.Count)
                {
                    name_3.Text = json_obj[page * 4 + 2]["commit"]["author"]["name"].ToString();
                    message_3.Text = json_obj[page * 4 + 2]["commit"]["message"].ToString();
                    date_3.Text = json_obj[page * 4 + 2]["commit"]["author"]["date"].ToString();

                }
                else
                {
                    name_3.Text = "";
                    message_3.Text = "";
                    date_3.Text = "";
                }
                if (page * 4 + 3 < json_obj.Count)
                {
                    name_4.Text = json_obj[page * 4 + 3]["commit"]["author"]["name"].ToString();
                    message_4.Text = json_obj[page * 4 + 3]["commit"]["message"].ToString();
                    date_4.Text = json_obj[page * 4 + 3]["commit"]["author"]["date"].ToString();

                }
                else
                {
                    name_4.Text = "";
                    message_4.Text = "";
                    date_4.Text = "";
                }

            }

        }



    }
}
