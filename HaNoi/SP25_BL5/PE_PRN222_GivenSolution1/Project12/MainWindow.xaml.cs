using Project11.Entities;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectAndLoadData();
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
                txtStatus.Text = "Server connection successful. Data loaded";
                txtStatus.Foreground = System.Windows.Media.Brushes.Red;
                txtStatus.Visibility = Visibility.Visible;
            }
            catch (SocketException)
            {
                txtStatus.Text = "Unable to connect to the server. Please check the server status or try reconnecting again.";
                txtStatus.Foreground = System.Windows.Media.Brushes.Red;
                txtStatus.Visibility = Visibility.Visible;
            }
        }
    }
}