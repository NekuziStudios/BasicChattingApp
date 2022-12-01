using Newtonsoft.Json;
using System;
using System.Net;

namespace ChattingApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            string rawJson = client.DownloadString("http://127.0.0.1:1564/api/login?email=" + emailBox.Text + "&password=" + passwordBox.Text);
            MessageBox.Show(rawJson);

            dynamic Json = JsonConvert.DeserializeObject<dynamic>(rawJson);

            if (Json["err"] == true)
            {
                if (Json["code"] == "profile_not_found")
                {
                    MessageBox.Show("An profile with that email doesn't exist. Did you put your email in right?");
                    return;

                } else if (Json["code"] == "password_email_failure")
                {
                    MessageBox.Show("Your password wasn't correct");
                    return;
                } else
                {
                    MessageBox.Show("Unknown error");
                    return;
                }
            }
            else
            {
                if (Json["isBanned"] == true)
                {
                    MessageBox.Show("You are banned from ChattingApp.");
                }
                else
                {
                    if (Json["username"].ToString() != "")
                    {
                        MessageBox.Show("asscheks");
                        ChattingApplication.Settings.Email = Json["email"].ToString();
                        ChattingApplication.Settings.Password = passwordBox.Text;
                        ChattingApplication.Settings.Username = Json["username"].ToString();
                        ChattingApplication.Settings.IsVerified = Json["isVerified"];
                        ChattingApplication.Settings.isMuted = Json["isMuted"];
                        Chat chat = new Chat();
                        chat.Show();
                        this.Hide();
                    }
                }
            }
        }

        private void debugConsoleBtn_Click(object sender, EventArgs e)
        {
            ChattingApplication.ConsoleHelper.ShowDebugConsole();
        }
    }
}