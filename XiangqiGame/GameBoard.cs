using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections;

namespace XiangqiGame
{
    class GameBoard
    {
        //用来储存每个位置的数据,在grid上
        public chesspiece[,] board = new chesspiece[10,9];
       
        //GameBoard board1 = new GameBoard();
        int orginX = -1;
        int orginY = -1;
        int destX = -1;
        int destY = -1;
        public GameState State = GameState.Select;//一开始默认是先选棋子
        public currentPlayer Player = currentPlayer.red;//默认红方开始

        public enum currentPlayer
        {
            black,
            red
        }
        public currentPlayer getPlayer()
        {
            return this.Player;
        }
        public void changePlayer()
        {
            if (Player == currentPlayer.red)
                Player = currentPlayer.black;
            else
                Player = currentPlayer.red;
        }
        public enum GameState 
        {
            Select,
            Move
        }
        public GameState getGameState()
        {
            return this.State;
        }
        public void setGameState(GameState state)
        {
            this.State = state;
        }
        public void changeGameState()
        {
            if (State == GameState.Move)
                State = GameState.Select;
            else
                State = GameState.Move;
        }

        //得到当前棋盘
        public chesspiece[,] getBoard()
        {
            return this.board;
        }
        public void setBoard(chesspiece[,] board)
        {
            this.board = board;
        }

        public bool[,] ValidMove = new bool[10,9];
        //List<Button> boardList = new List<Button>();
        List<Button> buttons = new List<Button>();

        public void StoredButton(Button button)
        {
           // List<Button> buttons = new List<Button>();
           // boardList = buttons;
            buttons.Add(button);
            //for (int i = 0; i <buttons.Count; i++)
            //{
             // MessageBox.Show(" " + button.Name);
            //}
            // MessageBox.Show("添加了一个按钮");

        }
        public List<Button> getButtons()
        {
            //MessageBox.Show("" + buttons[0].Name);
           // MessageBox.Show("" + buttons[1].Name);

            return buttons;
        }

        public GameBoard()//给棋盘所有棋子赋值,初始化游戏棋盘
        {
            for (int i = 0; i < board.GetLength(0); i++)//10 
            {
                for (int j = 0; j < board.GetLength(1); j++)//9 
                {
                     
                    this.board[i, j] = new blank(chesspiece.Player_side.blank, i, j);//棋盘的(x，y)
                }
            }
            board[9, 0] = new chariot(chesspiece.Player_side.red, 9, 0);             //每个棋子的起始位置
            board[9, 1] = new house(chesspiece.Player_side.red, 9, 1);
            board[9, 2] = new elephant(chesspiece.Player_side.red, 9, 2);
            board[9, 3] = new advisor(chesspiece.Player_side.red, 9, 3);
            board[9, 4] = new general(chesspiece.Player_side.red, 9, 4);
            board[9, 5] = new advisor(chesspiece.Player_side.red, 9, 5);
            board[9, 6] = new elephant(chesspiece.Player_side.red, 9, 6);
            board[9, 7] = new house(chesspiece.Player_side.red, 9, 7);
            board[9, 8] = new chariot(chesspiece.Player_side.red, 9, 8);
            board[7, 1] = new cannon(chesspiece.Player_side.red, 7, 1);
            board[7, 7] = new cannon(chesspiece.Player_side.red, 7, 7);
            board[6, 0] = new soldier(chesspiece.Player_side.red, 6, 0);
            board[6, 2] = new soldier(chesspiece.Player_side.red, 6, 2);
            board[6, 4] = new soldier(chesspiece.Player_side.red, 6, 4);
            board[6, 6] = new soldier(chesspiece.Player_side.red, 6, 6);
            board[6, 8] = new soldier(chesspiece.Player_side.red, 6, 8);

            board[0, 0] = new chariot(chesspiece.Player_side.black, 0, 0);
            board[0, 1] = new house(chesspiece.Player_side.black, 0, 1);
            board[0, 2] = new elephant(chesspiece.Player_side.black, 0, 2);
            board[0, 3] = new advisor(chesspiece.Player_side.black, 0, 3);
            board[0, 4] = new general(chesspiece.Player_side.black, 0, 4);
            board[0, 5] = new advisor(chesspiece.Player_side.black, 0, 5);
            board[0, 6] = new elephant(chesspiece.Player_side.black, 0, 6);
            board[0, 7] = new house(chesspiece.Player_side.black, 0, 7);
            board[0, 8] = new chariot(chesspiece.Player_side.black, 0, 8);
            board[2, 1] = new cannon(chesspiece.Player_side.black, 2, 1);
            board[2, 7] = new cannon(chesspiece.Player_side.black, 2, 7);
            board[3, 0] = new soldier(chesspiece.Player_side.black, 3, 0);
            board[3, 2] = new soldier(chesspiece.Player_side.black, 3, 2);
            board[3, 4] = new soldier(chesspiece.Player_side.black, 3, 4);
            board[3, 6] = new soldier(chesspiece.Player_side.black, 3, 6);
            board[3, 8] = new soldier(chesspiece.Player_side.black, 3, 8);
        }

        // 用来选择移动的棋子
        public void Selectedpiece(Button button)
        {
            orginX = (int)button.GetValue(WindowNew.XQRowProperty);
            orginY = (int)button.GetValue(WindowNew.XQColProperty);
            //MessageBox.Show("这里是要移动的棋子！");
        }
        public int getOrginX()
        {
            return this.orginX;
        }

        public int getOrginY()
        {
            return this.orginY;
        }


        public void ChosenDest(Button button)
        {
            destX = (int)button.GetValue(WindowNew.XQRowProperty); 
            destY = (int)button.GetValue(WindowNew.XQColProperty);
            //MessageBox.Show("这里是目的地！");
        }


        public void movePiece()
        {
            chesspiece load = new blank(chesspiece.Player_side.blank, orginX, orginY);
            //Console.WriteLine(load.getType());

            //this.board[destX,destY] = this.board[orginX, orginY];
            //this.board[destX, destY].setX(destX);
            //this.board[destX, destY].setY(destY);
            // this.board[orginX, orginY] = load;

            this.board[orginX, orginY].setX(destX);
            this.board[orginX, orginY].setY(destY);
            this.board[destX, destY] = this.board[orginX, orginY];
            this.board[orginX, orginY] = load;
        }

        public ArrayList getPieceCango()
        {
            ArrayList myList = new ArrayList();
            myList = board[orginX,orginY].PlaceCanGo(this);
            return myList;
        }
    }
}
