using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace WPFTileGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Grid grid1;
        static int nRows, nCols;
        static int nSquares;

        //random number generation
        List<int> randomNumbers;

        //list for undo and redo
        List<GameButtonState> gameReplayList;
        Stack<GameButton> gameUndoStack;
        DispatcherTimer tr;

        int index = 0;
        bool wingame = false;

        public MainWindow()
        {
            InitializeComponent();
            //create 3*3 grid
            grid1 = new Grid();

            //undo and redo
            gameUndoStack = new Stack<GameButton>();
            gameReplayList = new List<GameButtonState>();
            
            Border border = new Border();
            border.BorderThickness = new Thickness(10);
            border.BorderBrush = Brushes.LawnGreen;
            border.Child = grid1;


            Content = border;
            nRows = 3;
            nCols = 3;
            nSquares = nRows * nCols;

            CreateNumberGrid();
            CreateTiles();

            randomNumbers = new List<int>();
            GenerateRandomNumbersList();
            RandomizeTiles();
            AddMenuToGrid();
            
        }


        public void CreateNumberGrid()
        {
            for (int i = 0; i < nRows; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                //rowdef.Height = GridLength.Auto;
                grid1.RowDefinitions.Add(rowdef);
            }

            for (int i = 0; i < nCols; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();
                //coldef.Width = GridLength.Auto;
                grid1.ColumnDefinitions.Add(coldef);
            }

            GameButton.InitializeEmptySquares(nSquares - 1);

            //surround it with border                        
        }


        public void CreateTiles()
        {
            int tileCount = 1;
            for (int r = 0; r < nRows; r++)
            {
                for (int c = 0; c < nCols; c++)
                {
                    if (tileCount == (nSquares)) break;
                    //both id and position are initially same
                    GameButton btn = new GameButton(r*nRows+ c, r*nCols + c);
                    btn.Content = r*nRows + c + 1;
                    btn.Click += new RoutedEventHandler(GameButton_Click);
                    btn.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(btn_PropertyChanged);
                    grid1.Children.Add(btn);                   
                    Grid.SetRow(btn, r);
                    Grid.SetColumn(btn, c);
                    tileCount++;
                }

            }


        }


        public void AddMenuToGrid()
        {
            ContextMenu mnu = new ContextMenu();
            
            MenuItem undoItem = new MenuItem();
            undoItem.Header = "Undo";
            undoItem.Click += new RoutedEventHandler(undoItem_Click);
            mnu.Items.Add(undoItem);

            MenuItem replayItem = new MenuItem();
            replayItem.Header = "Replay";
            replayItem.Click += new RoutedEventHandler(replayItem_Click);
            replayItem.IsEnabled = false;
            mnu.Items.Add(replayItem);

            grid1.ContextMenu = mnu;

        }

        void ResetBoard()
        {
            //reset content on buttons
            int counter = 0;
            foreach (GameButton btn in grid1.Children)
            {
                btn.SetInSquare((btn.Id+1)); //1based index
                counter++;
                if ((nSquares - 1) == counter) break;
            }

            //randomizetiles to intial position
            RandomizeTiles();
            GameButton.InitializeEmptySquares(nSquares - 1);
        }

        void replayItem_Click(object sender, RoutedEventArgs e)
        {
            //ResetBoard
            ResetBoard();

            index = 0;
            
            tr = new DispatcherTimer();
            tr.Interval = TimeSpan.FromMilliseconds(1000);
            tr.Tick += new EventHandler(timer_Tick);
            tr.Start();
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Title = index.ToString();
            if (index < gameReplayList.Count)
            {
                GameButtonState btnState = gameReplayList[index];
                btnState.gameButton.Position = btnState.Oldpos;
                btnState.gameButton.Move();
                index++;
                //MessageBox.Show("1");
                //button10.Text = index.ToString();
            }

            if (index == gameReplayList.Count)
            {
                tr.Stop();
            }
        }

        void undoItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //fromgamestatelist remove lastitem
                GameButton btn = gameUndoStack.Pop();
                btn.Move();
                Title = gameUndoStack.Count.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Game Board Is in Original State!, No further undo possible");
            }
            
        }


        void btn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (true == checkforwinner())
            {
                MessageBox.Show("You have won the game");
                MenuItem item = (MenuItem)grid1.ContextMenu.Items[1];
                item.IsEnabled = true;
                wingame = true;
            }
        }

        

        public void RandomizeTiles()
        {
            
            int counter = 0;
            foreach (GameButton btn in grid1.Children)
            {
                btn.SetInSquare(randomNumbers[counter]);
                counter++;
                if ((nSquares - 1) == counter) break;
            }

        }

        public void GameButton_Click(object sender, RoutedEventArgs e)
        {
            if (true == wingame)
            {
                MessageBox.Show("You have to restart again to play game");
                return;
            }

            GameButton btn = sender as GameButton;
            GameButtonState btnState = new GameButtonState(btn.Id, btn);
            // int digit = Convert.ToInt32(btn.Content.ToString());         
            int row = ((int)btn.GetValue(Grid.RowProperty));
            int col = ((int)btn.GetValue(Grid.ColumnProperty));
            int digit = row * nRows + col;
            btn.Position = digit;
            btnState.Oldpos = btn.Position;
            //record into undo/redo stack before moving.
            btn.Move();
            btnState.Newpos = btn.Position;
            gameUndoStack.Push(btn);
            gameReplayList.Add(btnState);

            Title = gameUndoStack.Count.ToString();
    
        }//end GameButton_Click


        private void GenerateRandomNumbersList()
        {
            Random rnd = new Random(DateTime.Now.Second);
            int number = rnd.Next(1, nSquares) % nSquares;
            randomNumbers.Add(number);

            for (int count = 0; count < nSquares - 2; )
            {
                number = rnd.Next(1, nSquares) % nSquares;
                if (true == randomNumbers.Contains(number)) continue;
                else
                {
                    randomNumbers.Add(number);
                    count++;                    
                }
            }
        }

        
        private bool checkforwinner()
        {
            int counter = 1;
            bool win = false;

            if (GameButton.emptysquare != (nSquares - 1)) return win;

            foreach (GameButton btn in grid1.Children)
            {
                int pos = Int32.Parse(btn.Position.ToString());
                int con = Int32.Parse(btn.Id.ToString());

                if (con != (pos))
                {
                    break;
                }
                else
                    counter++;

                if ((nSquares - 1) == counter)
                {
                    win = true;
                    break;
                }
            }

            return win;
        }


    }
}
