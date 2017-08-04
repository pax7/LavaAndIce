using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Pax4.Core
{
    public class PaxMouseState
    {
        public MouseState _state;

        public bool _inWindow = false;

        public int _x = -1;
        public int _y = -1;

        public int _dx = 0;
        public int _dy = 0;

        public bool _leftDown = false;
        public bool _rightDown = false;
        public bool _middleDown = false;
        public bool _x1Down = false;
        public bool _x2Down = false;

        public bool _leftUp = false;
        public bool _rightUp = false;
        public bool _middleUp = false;
        public bool _x1Up = false;
        public bool _x2Up = false;

        public bool _leftClick = false;
        public bool _rightClick = false;
        public bool _middleClick = false;
        public bool _x1Click = false;
        public bool _x2Click = false;

        public bool _leftDoubleClick = false;
        public bool _rightDoubleClick = false;
        public bool _middleDoubleClick = false;
        public bool _x1DoubleClick = false;
        public bool _x2DoubleClick = false;

        public bool _wheelUp = false;
        public bool _wheelDown = false;
        public int _wheelValue = 0;

        public PaxMouseState()
        {
            _state = Mouse.GetState();
        }

        public void Set(PaxMouseState p_mouseState = null)
        {
            if (p_mouseState == null)
                return;

            _state = p_mouseState._state;

            _inWindow = p_mouseState._inWindow;

            _x = p_mouseState._x;
            _y = p_mouseState._y;

            _dx = p_mouseState._dx;
            _dy = p_mouseState._dy;

            _leftDown = p_mouseState._leftDown;
            _rightDown = p_mouseState._rightDown;
            _middleDown = p_mouseState._middleDown;
            _x1Down = p_mouseState._x1Down;
            _x2Down = p_mouseState._x2Down;

            _leftUp = p_mouseState._leftUp;
            _rightUp = p_mouseState._rightUp;
            _middleUp = p_mouseState._middleUp;
            _x1Up = p_mouseState._x1Up;
            _x2Up = p_mouseState._x2Up;

            _leftClick = p_mouseState._leftClick;
            _rightClick = p_mouseState._rightClick;
            _middleClick = p_mouseState._middleClick;
            _x1Click = p_mouseState._x1Click;
            _x2Click = p_mouseState._x2Click;

            _leftDoubleClick = p_mouseState._leftDoubleClick;
            _rightDoubleClick = p_mouseState._rightDoubleClick;
            _middleDoubleClick = p_mouseState._middleDoubleClick;
            _x1DoubleClick = p_mouseState._x1DoubleClick;
            _x2DoubleClick = p_mouseState._x2DoubleClick;

            _wheelUp = p_mouseState._wheelUp;
            _wheelDown = p_mouseState._wheelDown;
            _wheelValue = p_mouseState._wheelValue;
        }
    }

    [DataContract]
    [KnownType(typeof(Pax4Mouse))]
    public class Pax4Mouse
    {
        public const int _historySize = 3;//to detect double clicks
        public List<PaxMouseState> _mouseState;

        public PaxMouseState _currentMouseState = null;
        public PaxMouseState _previousMouseState = null;
        public PaxMouseState _previousMouseState0 = null;

        public Pax4Mouse()
        {
            Reset();
        }

        public PaxMouseState GetMouseState(int p_mouseStateIndex = _historySize - 1)
        {
            return _mouseState[p_mouseStateIndex];
        }

        public void Reset()
        {
            _mouseState = new List<PaxMouseState>();
            PaxMouseState ms;
            for (int i = 0; i < _historySize; i++)
            {
                ms = new PaxMouseState();
                ms._state = Mouse.GetState();
                _mouseState.Add(ms);
            }

            _currentMouseState = _mouseState[_historySize - 1];
            _previousMouseState = _mouseState[_historySize - 2];
            _previousMouseState0 = _mouseState[_historySize - 3];
        }

        public void Update()
        {
            for (int i = 0; i < _historySize - 1; i++)
                _mouseState[i].Set(_mouseState[i + 1]);
                //_mouseState[i]._state = _mouseState[i + 1]._state;
            _mouseState[_historySize - 1]._state = Mouse.GetState();

            _currentMouseState._state = Mouse.GetState();           

            _currentMouseState._x = _currentMouseState._state.X;
            _currentMouseState._y = _currentMouseState._state.Y;

            if (_currentMouseState._x > Pax4Camera._backBufferWidth || _currentMouseState._x < 0 || _currentMouseState._y > Pax4Camera._backBufferHeight || _currentMouseState._y < 0)
                _currentMouseState._inWindow = false;
            else
                _currentMouseState._inWindow = true;
            
            _currentMouseState._dx = _currentMouseState._state.X - _previousMouseState._state.X;
            _currentMouseState._dy = _currentMouseState._state.Y - _previousMouseState._state.Y;

            _currentMouseState._wheelValue = _currentMouseState._state.ScrollWheelValue;

            if (_currentMouseState._wheelValue == 0)
            {
                _currentMouseState._wheelUp = false;
                _currentMouseState._wheelDown = false;
            }
            else if (_currentMouseState._wheelValue > 0)
            {
                _currentMouseState._wheelUp = false;
                _currentMouseState._wheelDown = true;
            }
            else
            {
                _currentMouseState._wheelUp = true;
                _currentMouseState._wheelDown = false;
            }

            //left
            if (_currentMouseState._state.LeftButton.Equals(ButtonState.Pressed))
            {
                _currentMouseState._leftDown = true;
                _currentMouseState._leftUp = false;
            }
            else
            {
                _currentMouseState._leftDown = false;
                _currentMouseState._leftUp = true;

                if (_previousMouseState._leftDown)
                    _currentMouseState._leftClick = true;
                else
                    _currentMouseState._leftClick = false;
            }
            if (_previousMouseState._leftClick && _previousMouseState0._leftClick)
                _currentMouseState._leftDoubleClick = true;
            else
                _currentMouseState._leftDoubleClick = false;

            //right
            if (_currentMouseState._state.RightButton.Equals(ButtonState.Pressed))
            {
                _currentMouseState._rightDown = true;
                _currentMouseState._rightUp = false;
            }
            else
            {
                _currentMouseState._rightDown = false;
                _currentMouseState._rightUp = true;

                if (_currentMouseState._rightUp && _previousMouseState._rightDown)
                    _currentMouseState._rightClick = true;
                else
                    _currentMouseState._rightClick = false;
            }
            if (_previousMouseState._rightClick && _previousMouseState0._rightClick)
                _currentMouseState._rightDoubleClick = true;
            else
                _currentMouseState._rightDoubleClick = false;

            //middle
            if (_currentMouseState._state.MiddleButton.Equals(ButtonState.Pressed))
            {
                _currentMouseState._middleDown = true;
                _currentMouseState._middleUp = false;
            }
            else
            {
                _currentMouseState._middleDown = false;
                _currentMouseState._middleUp = true;

                if (_currentMouseState._middleUp && _previousMouseState._middleDown)
                    _currentMouseState._middleClick = true;
                else
                    _currentMouseState._middleClick = false;
            }
            if (_previousMouseState._middleClick && _previousMouseState0._middleClick)
                _currentMouseState._middleDoubleClick = true;
            else
                _currentMouseState._middleDoubleClick = false;

            //x1
            if (_currentMouseState._state.XButton1.Equals(ButtonState.Pressed))
            {
                _currentMouseState._x1Down = true;
                _currentMouseState._x1Up = false;
            }
            else
            {
                _currentMouseState._x1Down = false;
                _currentMouseState._x1Up = true;

                if (_currentMouseState._x1Up && _previousMouseState._x1Down)
                    _currentMouseState._x1Click = true;
                else
                    _currentMouseState._x1Click = false;
            }
            if (_previousMouseState._x1Click && _previousMouseState0._x1Click)
                _currentMouseState._x1DoubleClick = true;
            else
                _currentMouseState._x1DoubleClick = false;

            //x2
            if (_currentMouseState._state.XButton2.Equals(ButtonState.Pressed))
            {
                _currentMouseState._x2Down = true;
                _currentMouseState._x2Up = false;
            }
            else
            {
                _currentMouseState._x2Down = false;
                _currentMouseState._x2Up = true;

                if (_currentMouseState._x2Up && _previousMouseState._x2Down)
                    _currentMouseState._x2Click = true;
                else
                    _currentMouseState._x2Click = false;
            }
            if (_previousMouseState._x2Click && _previousMouseState0._x2Click)
                _currentMouseState._x2DoubleClick = true;
            else
                _currentMouseState._x2DoubleClick = false;
        }
    }
}
