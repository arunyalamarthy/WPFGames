using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFTileGame
{
    class GameButtonState
    {

        public GameButtonState(int id, GameButton btn)
        {
            _id = id;
            _gameButton = btn;
        }

//properties
        GameButton _gameButton;

        internal GameButton gameButton
        {
            get { return _gameButton; }
            set { _gameButton = value; }
        }

        int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
       

//oldpos
        int _oldpos;

        public int Oldpos
        {
            get { return _oldpos; }
            set { _oldpos = value; }
        }

        int _newpos;

        public int Newpos
        {
            get { return _newpos; }
            set { _newpos = value; }
        }
      
    }
}
