using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Pax4.Core
{
    public class Pax4KeyboardState
    {
        public KeyboardState _state;

        public Pax4KeyboardState()
        {
            _state = Keyboard.GetState();
        }
    }

    [DataContract]
    [KnownType(typeof(Pax4Keyboard))]
    public class Pax4Keyboard
    {
        public List<Pax4KeyboardState> _keyboardState;
        public int _historySize = 2;

        public Pax4KeyboardState _currentKeyboardState = null;
        public Pax4KeyboardState _previousKeyboardState = null;

        public Pax4Keyboard(int p_historySize = 2)
        {
            Reset(p_historySize);
        }

        public void Reset(int p_historySize = 2)
        {
            _historySize = p_historySize;
            _keyboardState = new List<Pax4KeyboardState>();
            for (int i = 0; i < _historySize; i++)
                _keyboardState.Add(new Pax4KeyboardState());

            _currentKeyboardState = _keyboardState[_historySize - 1];
            _previousKeyboardState = _keyboardState[_historySize - 2];
        }

        public void Update()
        {
            for (int i = 0; i < _historySize - 1; i++)
                _keyboardState[i]._state = _keyboardState[i + 1]._state;
            _keyboardState[_historySize - 1]._state = Keyboard.GetState();
        }
    }
}
