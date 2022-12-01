using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Websocket.Client;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

namespace ChattingApplication
{
    public partial class Chat : Form
    {
        public static ClientWebSocket socket = new ClientWebSocket();
        public Chat()
        {
            InitializeComponent();
        }

        private async void Chat_Load(object sender, EventArgs e)
        {
            if (ChattingApplication.Settings.isMuted == true)
            {
                MutedBg.Visible = true;
                MutedText.Visible = true;
            }
            else
            {
                MutedBg.Visible = false;
                MutedText.Visible = false;
            }

            Console.WriteLine("[ChatWebSocket] Connecting to Chat Servers!");
            await socket.ConnectAsync(new Uri("ws://127.0.0.1:3300"), CancellationToken.None);
            Console.WriteLine("[ChatWebSocket] Sent Welcome Message!");
            await Send(socket, "{ \"cmd\": \"c_new_user\", \"user\": \"" + ChattingApplication.Settings.Username + "\" }");
            await Receive(socket);
            
        }

        static async Task Send(ClientWebSocket socket, string data) =>
            await socket.SendAsync(Encoding.UTF8.GetBytes(data), WebSocketMessageType.Text, true, CancellationToken.None);

        private async static Task Receive(ClientWebSocket socket)
        {
            var buffer = new ArraySegment<byte>(new byte[2048]);
            do
            {
                WebSocketReceiveResult result;
                using (var ms = new MemoryStream())
                {
                    do
                    {
                        result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                        ms.Write(buffer.Array, buffer.Offset, result.Count);
                    } while (!result.EndOfMessage);

                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    ms.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(ms, Encoding.UTF8))
                        
                    using (var p = new Chat())
                    {
                        p.ExecFunc(p, await reader.ReadToEndAsync());
                        p.recheckProfile();
                    }
                }
            } while (true);
        }

        private async void ExecFunc(Chat p, string json)
        {
            //MessageBox.Show(json);
            Console.WriteLine("[ChatServerMsg] new Server command incoming");
            dynamic newJson = JsonConvert.DeserializeObject<dynamic>(json);

            switch (newJson["cmd"].ToString())
            {
                case "cl_new_member_msg":
                    Console.WriteLine("[ChatServerMsg] New Member message incoming!");
                    var chatinstance = Application.OpenForms.OfType<Chat>().FirstOrDefault();
                    if(chatinstance != null)
                    {
                        chatinstance.MsgBox.Text = chatinstance.MsgBox.Text + "|ﾟｰﾟ| SERVER: " + newJson["user"].ToString() + " has entered the chat!" + Environment.NewLine;
                    }
                    break;
                case "cl_append_msg":
                    Console.WriteLine("[ChatServerMsg] New message recieved!");
                    var chatinstance2 = Application.OpenForms.OfType<Chat>().FirstOrDefault();
                    if (chatinstance2 != null)
                    {
                        chatinstance2.MsgBox.Text = chatinstance2.MsgBox.Text + newJson["author"].ToString() + ": " + newJson["content"].ToString() + Environment.NewLine;
                    }
                    break;
                default:
                    // no command, no evidence
                    break;
            }
        }

        private async void recheckProfile()
        {
            Console.WriteLine("[ChatProfile] reChecking Profile");
            var ProfileJson = ChattingApplication.Settings.GetUpdateJSON();
            dynamic Json = JsonConvert.DeserializeObject<dynamic>(ProfileJson);


            if (Json["err"] == true)
            {
                Console.WriteLine("[ChatProfile] A sudden Profile change has been made, Profile deleted or name change?");
                MessageBox.Show("A profile change has been made. please restart Chatting App.");
                Application.Exit();
            } else
            {
                Console.WriteLine("[ChatProfile] setting new values!");
                if (Json["username"].ToString() != ChattingApplication.Settings.Username) { ChattingApplication.Settings.Username = Json["username"].ToString(); }
                if(Json["isVerified"] != ChattingApplication.Settings.IsVerified) { ChattingApplication.Settings.IsVerified = Json["isVerified"]; }
                if (Json["isMuted"] != ChattingApplication.Settings.isMuted) { ChattingApplication.Settings.isMuted = Json["isMuted"]; }
                if(Json["email"].ToString() != ChattingApplication.Settings.Email) { MessageBox.Show("A profile change has been made. please restart Chatting App."); Application.Exit(); }
                if (Json["password"].ToString() != ChattingApplication.Settings.Password) { MessageBox.Show("A profile change has been made. please restart Chatting App."); Application.Exit(); }
                
                var chatinstance = Application.OpenForms.OfType<Chat>().FirstOrDefault();
                // Update Stuff
                if (ChattingApplication.Settings.isMuted == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("[ChatProfile] User is muted!");
                    Console.ForegroundColor = ConsoleColor.White;
                    chatinstance.MutedBg.Visible = true;
                    chatinstance.MutedText.Visible = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[ChatProfile] User is not muted!");
                    Console.ForegroundColor = ConsoleColor.White;
                    chatinstance.MutedBg.Visible = false;
                    chatinstance.MutedText.Visible = false;
                }
            }
        }

        private void MsgBox_TextChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Shown");
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            await Send(socket, "{ \"cmd\": \"c_new_message_normal\", \"author\": \"" + ChattingApplication.Settings.Username + "\", \"content\": \"" + contentbox.Text + "\" }");
            contentbox.Text = "";
        }
    }
}
