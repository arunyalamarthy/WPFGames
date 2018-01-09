using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;

namespace WPFTileGame
{
    class GameButton : Button, INotifyPropertyChanged
    {
        private int position;
        public static int emptysquare = 0;  //0 to 8
        public static int lastemptysquare = 0;  //0 to 8
        private static int diff;
        private int id;
        public int MoveComplete;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Position
        {
            get { return position; }
            set { position = value; }
        }

        public GameButton()
        {
            //Background = Brushes.White;
            position = 0;
        }

        public GameButton(int id, int pos)
        {
            Id = id;
            Position = pos;

        }
        public void Move()
        {
            
            if (Math.Abs(emptysquare - position) == diff| Math.Abs(emptysquare - position) == 1)
            {
                lastemptysquare = emptysquare;
                //valid move //place from position to empty square
                int emptysquarerow = emptysquare / diff;
                int emptysquarecolumn = emptysquare % diff;

                SetValue(Grid.RowProperty, emptysquarerow);
                SetValue(Grid.ColumnProperty, emptysquarecolumn);

                //swap empty square and position
                int temp = emptysquare;
                emptysquare = position; //new emptysquere
                position = temp;
                
                PropertyChanged(this, new PropertyChangedEventArgs("MoveComplete"));
            }

        }


        public void SetInSquare(int n)
        {
            //n is random no b/w 1 to n, but squares are 0 based. so n = n-1
            n = n - 1;
            int emptysquarerow = n / diff;
            int emptysquarecolumn = n % diff;

            SetValue(Grid.RowProperty, emptysquarerow);
            SetValue(Grid.ColumnProperty, emptysquarecolumn);
            Position = n;

        }

        static public void InitializeEmptySquares(int n)
        {
            emptysquare = n;
            lastemptysquare = n;
            diff = (int)Math.Sqrt(emptysquare + 1);
        }

        // Summary:
        //     Occurs when a property value changes.
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
