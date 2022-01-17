using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace XiangqiGame
{
    internal class display
    {
        Button button = new Button();
        //GameBoard board = new GameBoard();

        public void showChesspiece(int i, int j, Button button, GameBoard board)
        {
            ImageBrush bg = new ImageBrush();
            chesspiece[,] board1 = new chesspiece[10, 9];
            board1 = board.getBoard();
            button.BorderBrush = new SolidColorBrush(Colors.Transparent);
            if (board.getBoard()[i, j].getPlayer() == chesspiece.Player_side.red)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                switch (board.getBoard()[i, j].getType())
                {
                    case chesspiece.Piece_type.general:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess7.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "帅";

                        break;
                    case chesspiece.Piece_type.advisor:
                        bg.ImageSource =
                             new BitmapImage(new Uri("Picture/ChineseChessGame/chess8.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "仕";
                        break;
                    case chesspiece.Piece_type.elephant:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess9.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "相";
                        break;
                    case chesspiece.Piece_type.house:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess10.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "马";
                        break;
                    case chesspiece.Piece_type.chariot:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess11.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "车";
                        break;
                    case chesspiece.Piece_type.cannon:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess12.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "炮";
                        break;
                    case chesspiece.Piece_type.soldier:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess13.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "兵";
                        break;
                    case chesspiece.Piece_type.blank:
                        //button.Content = "  ";
                        button.Background = null;
                        break;
                    default:
                        //Console.WriteLine("  ");
                        break;
                }
            }
            else
            {
                //Console.ForegroundColor = ConsoleColor.Black;
                switch (board.getBoard()[i, j].getType())
                {
                    case chesspiece.Piece_type.general:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess0.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "将";

                        break;
                    case chesspiece.Piece_type.advisor:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess1.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "士";
                        break;
                    case chesspiece.Piece_type.elephant:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess2.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "象";
                        break;
                    case chesspiece.Piece_type.house:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess3.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "马";
                        break;
                    case chesspiece.Piece_type.chariot:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess4.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "车";
                        break;
                    case chesspiece.Piece_type.cannon:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess5.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "砲";
                        break;
                    case chesspiece.Piece_type.soldier:
                        bg.ImageSource =
                            new BitmapImage(new Uri("Picture/ChineseChessGame/chess6.png", UriKind.RelativeOrAbsolute));
                        button.Background = bg;
                        //button.Content = "卒";
                        break;
                    case chesspiece.Piece_type.blank:
                        //button.Content = "  ";
                        button.Background = null;
                        break;
                    default:
                        //Console.WriteLine("  ");
                        break;
                }
            }
        }

        public bool currentRound(GameBoard board, int x, int y, int turn)
        {
            //其次要得到当前回合
            bool temp = true;
            if (turn == 0)
            {
                if (board.board[x,y].getType()!=chesspiece.Piece_type.blank)
                {
                    if (board.board[x, y].getPlayer() != chesspiece.Player_side.red)
                    {
                        //Console.WriteLine("move red piece！");
                        MessageBox.Show("move red piece！");
                        temp = false;
                    }
                }
                else
                {
                    temp = false;
                    MessageBox.Show("No Piece!");
                }
            }
            else
            {
                if (board.board[x, y].getType() != chesspiece.Piece_type.blank)
                {
                    if (board.board[x, y].getPlayer() != chesspiece.Player_side.black)
                    {
                        //Console.WriteLine("move black piece！");
                        MessageBox.Show("move black piece！");
                        temp = false;
                    }
                }
                else
                {
                    temp = false;
                    MessageBox.Show("No Piece!");
                }
            }

            return temp;
        }
        //判断输赢
        public bool isWine(GameBoard board)
        {
            bool temp = false;
            int a = 0;
            int b = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    if (board.board[i, j].getType() == chesspiece.Piece_type.general && board.board[i, j].getPlayer() == chesspiece.Player_side.red)
                    {
                        a = 1;
                    }
                    else if (board.board[i, j].getType() == chesspiece.Piece_type.general && board.board[i, j].getPlayer() == chesspiece.Player_side.black)
                    {
                        b = 1;
                    }
                }
            }
            if (a == 0)
            {
                //Console.WriteLine("Black wins!");
                MessageBox.Show("Black wins!");
                temp = true;
            }
            else if (b == 0)
            {
                MessageBox.Show("Red wins!");
                //Console.WriteLine("Red wins!");
                temp = true;
            }
            return temp;
        }
    }
}
