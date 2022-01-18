using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;

namespace XiangqiGame
{ 
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public void newWindow()
        {
            ImageBrush bg = new ImageBrush();
            bg.ImageSource = new BitmapImage(new Uri("Picture/ChineseChessGame/mainbackground.png", UriKind.RelativeOrAbsolute));
            Background = bg;
        }
        public MainWindow()
        {
            //加背景音乐
            BackgroundMusic();
            InitializeComponent();
            newWindow();
        }
        private void ButtonNewGame(object sender, RoutedEventArgs e)
        {
            //生成新的主窗体
            WindowNew Window_New = new WindowNew();

            //设置系统主窗体
            App.Current.MainWindow = Window_New;

            //关闭原先的窗口
            this.Close();

            //关闭音乐
            _sound.Stop();

            //显示新的主窗体
            Window_New.Show();
        }

        private void Close_Click(object sender, RoutedEventArgs e)

        {
            MessageBoxResult result = MessageBox.Show("Are you sure to quit the game?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.MainWindow.Close();
            }
            else
            {
                return;
            }
        }

        public static SoundPlayer _sound = new SoundPlayer();

        public static void BackgroundMusic()
        {
            _sound.SoundLocation = "Sounds/happy1.wav";
            _sound.Play();
        }
    }
}
