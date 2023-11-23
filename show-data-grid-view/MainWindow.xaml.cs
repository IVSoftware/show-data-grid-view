using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace show_data_grid_view
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var frame = new Frame();
            FrameApp.SetCurrentMainFrame(frame);
            FrameApp.SetCurrentTopFrame(frame);
            FrameApp.FrameTop.Navigate(new ChooseDepPage());

            Content = frame; // << Check that content is being set "somewhere"
        }
    }
    class FrameApp
    {
        public static Frame FrameMain { get; set; }
        public static Frame FrameTop { get; set; }
        public static void SetCurrentMainFrame(Frame frame) => FrameMain = frame;
        public static void SetCurrentTopFrame(Frame frame) => FrameTop = frame;
    }
}