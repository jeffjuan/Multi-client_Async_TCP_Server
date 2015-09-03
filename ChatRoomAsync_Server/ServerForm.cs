using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatRoomAsync_Server
{
    public partial class ServerForm : Form
    {
        private Socket serverSocket;
        private List<StateObject> ClientLists = new List<StateObject>();

        public ServerForm()
        {
            InitializeComponent();
        }

        // 啟動 Server
        private void buttonStart_Click(object sender, EventArgs e)
        {
            String ServerPort = System.Configuration.ConfigurationManager.AppSettings["ServerPort"].ToString();
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, Int32.Parse(ServerPort));
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                serverSocket.Bind(localEP);
                serverSocket.Listen(10);
                listBoxMsg.Items.Add("等待使用者端的連線...");
                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // 當任何使用者端發動連線，以下程式將被執行
        private void AcceptCallback(IAsyncResult ar)
        {
            Socket handler = ((Socket)ar.AsyncState).EndAccept(ar);
            StateObject user = new StateObject();
            user.workSocket = handler;
            // 添加 User 到 list
            ClientLists.Add(user);
            // 開始接收訊息
            user.workSocket.BeginReceive(user.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), user);
            // 持續監聽
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            StateObject user = new StateObject();
            user = (StateObject)ar.AsyncState;
            Socket tmpSocket = user.workSocket;

            // 從客戶端讀取資料
            int bytesRead = user.workSocket.EndReceive(ar);
            if (bytesRead > 0) // 有資料
            {
                UnicodeEncoding unicode = new UnicodeEncoding();
                // 客戶端資料
                String MSG = unicode.GetString(user.buffer);
                listBoxMsg.Items.Add("讀取客戶端資料 : " + MSG);

                // 將資料推播給所有已連線客戶
                BroadCast(MSG);
            }
            // 清除舊資料
            user.buffer = new byte[1024];
            // 等待客戶端再次傳送資料
            user.workSocket.BeginReceive(user.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), user);
        }

        private void BroadCast(string msg)
        {
            // Convert string to byte[]
            UnicodeEncoding aEncoding = new UnicodeEncoding();
            // 須發佈的訊息
            byte[] byteData = aEncoding.GetBytes(msg);
            // 發佈給所有已連線的客戶端
            foreach (var c in ClientLists)
            {
                c.workSocket.Send(byteData);
            }
            listBoxMsg.Items.Add("已回傳給客戶端 : " + msg);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}