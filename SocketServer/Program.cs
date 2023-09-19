using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TCPServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // B1: Khởi tạo IPEndPoint của server
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, 8888);
                // B2: Khởi tạo Socket ở dạng TCP
                Socket server = new Socket(SocketType.Stream, ProtocolType.Tcp);
                // B3: Liên kết Socket với IPEndPoint
                server.Bind(iep);
                // B4: Tạo bộ lắng nghe
                server.Listen(10);
                Console.WriteLine("Cho ket noi tu Client!");
                // B5: Chấp nhận kết nối đến từ client, tạo phiên làm việc
                Socket client = server.Accept();
                Console.WriteLine("Co ket noi den tu client!");
                // B6: Nhận/gửi gói tin theo kịch bản đã xây dựng
                // B6.1: Nhận gói tin gửi từ Client lên
                byte[] receiveData = new byte[1024];
                int len = client.Receive(receiveData);
                string message = ASCIIEncoding.ASCII.GetString(receiveData, 0, len);
                Console.WriteLine("<Client>: " + message);
                // B6.2: Xử lý dữ liệu và gửi trả về phản hồi
                string send = message.ToUpper();
                byte[] sendData = ASCIIEncoding.ASCII.GetBytes(send);
                client.Send(sendData, sendData.Length, SocketFlags.None);
                // B?: Đóng kết nối
                client.Close();
                server.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Loi: " + e.Message);
            }
            Console.ReadKey();
        }
    }
}
