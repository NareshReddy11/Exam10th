using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server1
{
    class ServerProgram
    {
        static Dictionary<string, ClientHandler> dSockets = new Dictionary<string, ClientHandler>();
        static int i = 1;
        static Socket sck;
        static Socket acc;

        static void Connection()
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234));
            sck.Listen(2);
        }
        static void Operation()
        {
            while (true)
            {
                acc = sck.Accept();
                try
                {
                    byte[] Buf = new byte[255];
                    int rec = acc.Receive(Buf, 0, Buf.Length, 0);
                    Array.Resize(ref Buf, rec);
                    string dis = Encoding.Default.GetString(Buf);
                    Console.WriteLine(dis + " Joined.....");
                    byte[] send = Encoding.Default.GetBytes(dis);
                    acc.Send(send, 0, send.Length, 0);
                    byte[] Buf1 = new byte[255];
                    int rec1 = acc.Receive(Buf1, 0, Buf1.Length, 0);
                    Array.Resize(ref Buf1, rec1);
                    string dis1 = Encoding.Default.GetString(Buf1);
                    ClientHandler mtch = new ClientHandler(acc, dis, dis1, dSockets);
                    dSockets.Add(dis, mtch);
                    Thread t = new Thread(mtch.Run);
                    Thread t1 = new Thread(mtch.Send);
                    t.Start();
                    t1.Start();
                    i++;
                }
                catch
                {
                    Console.WriteLine("Hello");
                }
            }
        }

        static void Main(string[] args)
        {

            Console.WriteLine("WelCome To Chat Server");
            Connection();
            Operation();

        }

    }
    class ClientHandler
    {

        private string name;
        Socket s;
        bool isloggedin;
        string[] strr;
        static ClientHandler kk;
        Dictionary<string, ClientHandler> dSockets;


        public ClientHandler(Socket s, string name, string con, Dictionary<string, ClientHandler> ch)
        {

            this.name = name;
            this.s = s;
            this.isloggedin = true;
            dSockets = ch;
            this.strr = con.Split('|');
        }

        public void Run()
        {
            string received;
            while (true)
            {
                try
                {
                    byte[] Buffer = new byte[255];
                    int rec = s.Receive(Buffer, 0, Buffer.Length, 0);
                    Array.Resize(ref Buffer, rec);
                    received = Encoding.Default.GetString(Buffer);

                    Console.WriteLine(received);
                    string[] st = received.Split('#');
                    string recipient = st[0];
                    string MsgToSend = st[1];

                    if (MsgToSend.Equals("logout"))
                    {
                        dSockets.Remove(recipient);
                        this.isloggedin = false;
                        this.s.Close();
                        break;
                    }

                    if (strr[0].Equals(" "))
                    {
                        foreach (KeyValuePair<string, ClientHandler> val in dSockets)
                        {
                            ClientHandler mc = (ClientHandler)val.Value;


                            if (!(mc.name.Equals(recipient)) && mc.isloggedin == true)
                            {
                                byte[] sData = Encoding.Default.GetBytes(this.name + " : " + MsgToSend);
                                mc.s.Send(sData, 0, sData.Length, 0);
                            }

                        }
                    }

                    dSockets.TryGetValue(recipient, out kk);
                    foreach (KeyValuePair<string, ClientHandler> val in dSockets)
                    {
                        ClientHandler mc = (ClientHandler)val.Value;


                        for (int i = 0; i < kk.strr.Length; i++)
                        {

                            if (!(mc.name.Equals(recipient)) && mc.isloggedin == true && mc.name.Equals(kk.strr[i]))
                            {
                                byte[] sData = Encoding.Default.GetBytes(this.name + " : " + MsgToSend);
                                mc.s.Send(sData, 0, sData.Length, 0);
                                break;
                            }
                        }
                    }


                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Send()
        {
            if (s != null)
            {
                while (true)
                {
                    string msg = Console.ReadLine();
                    foreach (KeyValuePair<string, ClientHandler> val in dSockets)
                    {
                        ClientHandler mc = (ClientHandler)val.Value;
                        byte[] sData = Encoding.Default.GetBytes("Server : " + msg);
                        mc.s.Send(sData, 0, sData.Length, 0);
                    }
                }
            }
        }
    }
}