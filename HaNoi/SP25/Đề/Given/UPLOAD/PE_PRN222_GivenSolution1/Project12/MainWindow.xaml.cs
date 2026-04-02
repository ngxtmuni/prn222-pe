using Project12.Entities;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace Project12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectAndLoadData()
        {
            try
            {
                using TcpClient client = new();
                client.Connect("127.0.0.1", 8080);
                using NetworkStream stream = client.GetStream();
                byte[] requests = Encoding.UTF8.GetBytes("GetStudents");
                stream.Write(requests, 0, requests.Length);
                using StreamReader reader = new(stream, Encoding.UTF8, leaveOpen: true);
                string response = reader.ReadToEnd();
                var students = JsonSerializer.Deserialize<List<Student>>(response) ?? new List<Student>();

                dgStudents.ItemsSource = students;
                txtStaus.Text = "Server connection successful. Data loaded";
                txtStaus.Foreground = System.Windows.Media.Brushes.Red;
                txtStaus.Visibility = Visibility.Visible;
            } catch (SocketException)
            {
                txtStaus.Text = "Unable to connect to the server. Please check the server status or try reconnecting again.";
                txtStaus.Foreground = System.Windows.Media.Brushes.Red;
                txtStaus.Visibility = Visibility.Visible;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectAndLoadData();
        }
    }
}