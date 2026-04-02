using Project12.Models;
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
            ConnectAndLoadData();
        }

        private async void ConnectAndLoadData()
        {
            try
            {
                using TcpClient client = new TcpClient();
                await client.ConnectAsync("127.0.0.1", 8080);
                using var stream = client.GetStream();

                string request = "GetStudents";
                byte[] requestData = Encoding.UTF8.GetBytes(request);
                await stream.WriteAsync(requestData, 0, requestData.Length);

                byte[] buffer = new byte[8192];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                List<Student> students = JsonSerializer.Deserialize<List<Student>>(json);
                dgStudents.ItemsSource = students;

                txtStatus.Text = "Server connection successful. Data loaded.";
                txtStatus.Foreground = System.Windows.Media.Brushes.Red;
                txtStatus.Visibility = Visibility.Visible;
            }
            catch
            {
                txtStatus.Text = "Unable to connect to server. Please check server status and try connecting again.";
                txtStatus.Foreground = System.Windows.Media.Brushes.Red;
                txtStatus.Visibility = Visibility.Visible;
            }
        }

        private void Reconnect_Click(object sender, RoutedEventArgs e)
        {
            ConnectAndLoadData();
        }
    }
}