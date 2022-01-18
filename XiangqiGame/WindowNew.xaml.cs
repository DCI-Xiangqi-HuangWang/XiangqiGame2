using System;
using System.Collections;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Media;
using System.Collections.Generic;
using System.Timers;
using System.Threading;
using System.Windows.Threading;
using Timer = System.Timers.Timer;
using System.Diagnostics;

namespace XiangqiGame
{
    /// <summary>
    /// Interaction logic for WindowNew.xaml
    /// </summary>
    ///  
    public partial class WindowNew : Window
    {
        GameBoard board = new GameBoard(); //已经给所有棋子赋值，初始化成功
        chesspiece[,] chesspiece = new chesspiece[10, 9];
        display show = new display();
        int min = 0, sec = 0;

        public WindowNew()
        {
            InitializeComponent();
            CreateGrid();
            RedrawGrid();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            timer.IsEnabled = true;

        }
        //显示游戏进行时间
        public void timer_Tick(object sender, EventArgs e)
        {
            sec++;
            
            if (sec > 60)
            {
                min++;
                sec = 0;
            }
            Timer1.Text = "Game time: "+min + " : " + sec;
        }

        //依赖象棋的行列，此依赖属性用来储存Button的值，依赖宿主的类型是default(int)
        public static readonly DependencyProperty XQRowProperty = DependencyProperty.Register("XQRow",
                typeof(int),
                typeof(Button),
                new PropertyMetadata(default(int)));//定义棋盘行的属性 依赖属性就是可以自己没有值，并能够通过Binding从数据源获取值（依赖在别人身上）的属性
        public static readonly DependencyProperty XQColProperty = DependencyProperty.Register("XQCol",
                typeof(int),
                typeof(Button),
                new PropertyMetadata(default(int)));//定义棋盘列的属性


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //得到所点击button的行
            int btnRow = (int)((Button)sender).GetValue(XQRowProperty);
            int btnCol = (int)((Button)sender).GetValue(XQColProperty);
            //MessageBox.Show("Button is: " + ((Button)sender).Name + "\n - Row = " + btnRow + "\n - Colum  = " + btnCol);

            //HandleClick(btnCol, btnRow);
            HandleButton((Button)sender);

            //加按键声音
            PlayClickSound();
        }

        public static SoundPlayer _sound = new SoundPlayer();

        public static void PlayClickSound()
        {
            _sound.SoundLocation = "Sounds/click.wav";
            _sound.Play();
        }

        Button storedButton = new Button();
        //chesspiece piece;
        int count = 0;
        //int stepcount = -1;
        
        ArrayList list = new ArrayList();
       
        private void HandleButton(Button button)
        {
            chesspiece = board.getBoard();
            if (board.getGameState() == GameBoard.GameState.Select)
            {
                int turn = count % 2;
                bool round = show.currentRound(board, (int)button.GetValue(XQRowProperty), (int)button.GetValue(XQColProperty), turn);
                if (round)
                {
                    board.changeGameState();
                    board.Selectedpiece(button);
                   // MessageBox.Show("" + button.Name);
                    board.StoredButton(button);

                    storedButton = button;
                    //stepcount++;

                    list = MoveHint();
                }
                //选择棋子完后GameState => Move
            }
            else
            {
                // 在移动状态时要做到可以选择目的地
                //MessageBox.Show("当前状态是Move");
                board.ChosenDest(button);
                board.changeGameState();
                //MessageBox.Show("原坐标：" + (int)storedButton.GetValue(XQRowProperty) + "," + (int)storedButton.GetValue(XQColProperty) + "\n" + "目的地" + (int)button.GetValue(XQRowProperty) + "," + (int)button.GetValue(XQColProperty));
                if (chesspiece[(int)storedButton.GetValue(XQRowProperty), (int)storedButton.GetValue(XQColProperty)].ValidMove(board, (int)storedButton.GetValue(XQRowProperty), (int)storedButton.GetValue(XQColProperty), (int)button.GetValue(XQRowProperty), (int)button.GetValue(XQColProperty)))
                {
                    count++;
                    removeHint((int)button.GetValue(XQRowProperty), (int)button.GetValue(XQColProperty));
                    board.changePlayer();
                    board.movePiece();
                   // MessageBox.Show("" + button.Name);
                    board.StoredButton(button);
                    //stepcount++;
                    //MessageBox.Show("" + stepcount);
                    button.Background = storedButton.Background;
                    storedButton.Background = null;
                    show.isWine(board);
                    
                }
                else
                {
                    MessageBox.Show("Please follow rules!");
                    removeHint((int)button.GetValue(XQRowProperty), (int)button.GetValue(XQColProperty));
                }
                //GameState => Select
            }
            state.Text = "Current state: " + board.getGameState().ToString();
            side.Text = "Current side: " + board.getPlayer().ToString();
        }

        public void CreateGrid() //创建一个网格对应棋盘
        {
            //此时已有一个初始化好了的chesspiece[,]棋盘名叫board在GameBoard中
            //我要让名字为board1类型为chesspiece[,]的变量得到存储这初始化棋盘数据的二位数组board
            chesspiece[,] board1 = new chesspiece[10, 9];
            board1 = board.getBoard();

            //display show = new display();
            for (int i = 0; i < 10; i++)
                GameBoardGrid.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < 9; i++)
                //获取控件的ColumnDefinitions列表，并添加新建一个ColunmDefinition到最后
                GameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    //遍历，在每个格上new一个button,名字叫做Button行列
                    Button button = new Button();
                    string name = "Button";
                    name += row.ToString() + col.ToString();
                    //必须要注册后面才能Find
                    RegisterName(name, button);
                    button.Name = "Button" + row.ToString() + col.ToString();
                    //为每个button都添加一个路由事件
                    button.Click += new RoutedEventHandler(this.Button_Click);
                    //将row的值赋给XQRowProperty,设置按钮所在Grid控件的行
                    button.SetValue(XQRowProperty, row);
                    button.SetValue(XQColProperty, col);

                    //设置附加属性row为给定的button
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);

                    //将button添加到网格上
                    GameBoardGrid.Children.Add(button);

                    show.showChesspiece(row, col, button, board);
                }
            }
        }

        public void RedrawGrid()
        {
            ImageBrush bg = new ImageBrush();
            bg.ImageSource = new BitmapImage(new Uri("Picture/ChineseChessGame/chessBoard.png", UriKind.RelativeOrAbsolute));
            this.GameBoardGrid.Background = bg;
        }

        //移动时的高光提示
        public ArrayList MoveHint()
        {
            ArrayList myList = new ArrayList();
            myList = board.getPieceCango();
            foreach (chesspiece piece in myList)
            {
                //MessageBox.Show("" + piece.getX() + piece.getY());
                string name = "Button";
                name += piece.getX();
                name += piece.getY();

                Button button = (Button)FindName(name);
                // Button btn = (Button)this.Controls.Find("Name", false)[0];
                //MessageBox.Show(""+name);
                //MessageBox.Show("" + button.Name);

                if (piece.getType() == XiangqiGame.chesspiece.Piece_type.blank)
                {
                    button.Opacity = 0.1;
                    button.Background = new SolidColorBrush(Colors.Blue);
                }
            }
            return myList;
        }
        //去掉高光提示
        public void removeHint(int destX, int destY)
        {
            string orginName = "Button" + destX + destY;
            ArrayList useList = new ArrayList();
            useList = board.getPieceCango();
            foreach (chesspiece piece in useList)
            {
                string name = "Button";
                name += piece.getX();
                name += piece.getY();

                Button button = (Button)FindName(name);
                if (button.Name.ToString() != orginName && piece.getType() == XiangqiGame.chesspiece.Piece_type.blank)
                {
                    button.Opacity = 0;
                }
                if (button.Name.ToString() == orginName)
                {
                    button.Opacity = 1;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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

        List<Button> buttons = new List<Button>();

        public void comeBackStep()
        {
            buttons = board.getButtons();
            int num = -1;
            foreach(Button button in buttons)
            {
                num++;
            }
           // MessageBox.Show("" + buttons[1].Name);
            //起始位置
           //Button lastbutton = buttons[buttons.Count];
            //MessageBox.Show(lastbutton.Name.ToString());
            int selectedX = (int)buttons[num].GetValue(XQRowProperty);
            int selectedY = (int)buttons[num].GetValue(XQColProperty);
            //MessageBox.Show("最后一个按钮位置为：" + selectedX + " " + selectedY);
            //目的地
           // Button destbutton = buttons[buttons.Count - 1];
            int destX = (int)buttons[num-1].GetValue(XQRowProperty);
            int destY = (int)buttons[num-1].GetValue(XQColProperty);
            //MessageBox.Show("倒数第二个按钮位置为：" + destX + " " + destY);

            chesspiece = board.getBoard();
            chesspiece load = new blank(XiangqiGame.chesspiece.Player_side.blank, selectedX, selectedY);

            chesspiece[selectedX, selectedY].setX(destX);
            chesspiece[selectedX, selectedY].setY(destY);
            chesspiece[destX, destY] = chesspiece[selectedX, selectedY];
            chesspiece[selectedX, selectedY] = load;

            //MessageBox.Show("最后一个按钮种类为：" + board.getBoard()[selectedX, selectedY].getType().ToString());
            //MessageBox.Show("倒数第二个按钮种类为：" + board.getBoard()[destX, destY].getType().ToString());

            buttons[num-1].Background = buttons[num].Background;
            buttons[num].Background = null;
            //board.clean();
            //MessageBox.Show("" + buttons.Count.ToString());
            count--;
            board.changePlayer();
            state.Text = "Current state: " + board.getGameState().ToString();
            side.Text = "Current side: " + board.getPlayer().ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("click2");
            //List<Button> buttons = new List<Button>();
            buttons = board.getButtons();
            //MessageBox.Show(buttons.Count.ToString());
           // if (buttons.Count >= 1 && buttons.Count % 2 != 0)
           // {
                comeBackStep();
            //}
        }
    }

}
