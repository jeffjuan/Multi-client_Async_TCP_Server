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

namespace ChatRoomAsync_Client
{
    public partial class ClientForm : Form
    {
        // ManualResetEvent instances signal
        private ManualResetEvent connectDone = new ManualResetEvent(false);

        private static int serverPort = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["ServerPort"].ToString());

        private Socket clientSocket;

        private string ClientName { get; set; }

        public ClientForm()
        {
            InitializeComponent();
        }

        // 發送訊息
        private void button1_Click(object sender, EventArgs e)
        {
            SendMSG();
        }

        private void textMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                SendMSG();
        }

        private void SendMSG()
        {
            try
            {
                if (textMessage.Text == "")
                {
                    MessageBox.Show("請填寫訊息");
                    return;
                }
                else
                {
                    UnicodeEncoding aEncoding = new UnicodeEncoding();
                    byte[] byteData = aEncoding.GetBytes(this.ClientName + "說：" + textMessage.Text.Trim());

                    clientSocket.Send(byteData);
                    textBoxName.Enabled = false;
                    textMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                // 開始非同步接收主機端資料
                state.workSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = null;
                state = (StateObject)ar.AsyncState;
                Socket server = state.workSocket;

                // 從主機端讀取資料
                int bytesRead = server.EndReceive(ar);
                // 有資料
                if (bytesRead > 0)
                {
                    UnicodeEncoding unicode = new UnicodeEncoding();
                    String MSG = unicode.GetString(state.buffer);
                    listBox.Items.Add(MSG);
                }
                // 繼續等待主機回傳的資料
                state.workSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        // 與主機端連線
        private void buttonConn_Click(object sender, EventArgs e)
        {
            if (textBoxServerIP.Text == "")
            {
                MessageBox.Show("請填寫主機 IP");
            }
            else
            {
                Connection();
            }
        }

        private void Connection()
        {
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(textBoxServerIP.Text), serverPort);
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallBack), clientSocket);
                connectDone.WaitOne();
                Receive(clientSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                // 完成連線
                client.EndConnect(ar);
                labelState.Text = "已連線到主機: " + client.RemoteEndPoint.ToString();
                labelState.ForeColor = Color.Blue;
                MessageBox.Show("連線成功");
                // 狀態設定為未收到訊號
                connectDone.Set();
            }
            catch (Exception e)
            {
                connectDone.Set();
                MessageBox.Show(e.ToString());
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            this.Close();
        }

        // 設定聊天室顯示的姓名
        private void buttonSettingName_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "")
            {
                MessageBox.Show("請填寫姓名");
                return;
            }
            else
            {
                this.ClientName = textBoxName.Text.Trim();
                textBoxName.Enabled = false;
            }
        }
    }
}