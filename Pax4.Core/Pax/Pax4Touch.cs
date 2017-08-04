using System;
using System.Collections.Generic;
using System.Text;

#if !WINDOWS_MOUSE
using Microsoft.Xna.Framework.Input.Touch;
#else
    using Microsoft.Xna.Framework.Input;
#endif

using Microsoft.Xna.Framework;
using System.Threading;
using Pax.Core;

namespace Pax4.Core
{
    public class Pax4TouchState : PaxState
    {
        public Vector3 _xy = Vector3.Zero;
        public Vector3 _dxdy = Vector3.Zero;
        
        public bool _oneFlick = false;

        public bool _clean = true;//_inWindow;

        public bool _oneTap = false;
        public bool _oneTouch = false;
        public bool _twoTouch = false;
        public bool _twoTap = false;

        public bool _pinch = false;
        public bool _unPinch = false;

#if !WINDOWS_MOUSE
        public TouchCollection _state;       
		public int _twoTouchDistance = -1;

        public int _twoTapDistance = -1;
#else
        public MouseState _state;

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
#endif

        public Pax4TouchState(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
#if !WINDOWS_MOUSE
            _state = TouchPanel.GetState();
#else
            _state = Mouse.GetState();
#endif
        }

        public void Set(Pax4TouchState p_touchState = null)
        {
            if (p_touchState == null)
                return;

            _state = p_touchState._state;

            _xy = p_touchState._xy;
            _dxdy = p_touchState._dxdy;

            _oneTap = p_touchState._oneTap;

            _oneTouch = p_touchState._oneTouch;
            _twoTouch = p_touchState._twoTouch;
			
#if !WINDOWS_MOUSE
            _twoTouchDistance = p_touchState._twoTouchDistance;

            _twoTap = p_touchState._twoTap;
            _twoTapDistance = p_touchState._twoTapDistance;

            _pinch = p_touchState._pinch;
            _unPinch = p_touchState._unPinch;
#else
            _leftDown = p_touchState._leftDown;
            _rightDown = p_touchState._rightDown;
            _middleDown = p_touchState._middleDown;
            _x1Down = p_touchState._x1Down;
            _x2Down = p_touchState._x2Down;

            _leftUp = p_touchState._leftUp;
            _rightUp = p_touchState._rightUp;
            _middleUp = p_touchState._middleUp;
            _x1Up = p_touchState._x1Up;
            _x2Up = p_touchState._x2Up;

            _rightClick = p_touchState._rightClick;
            _middleClick = p_touchState._middleClick;
            _x1Click = p_touchState._x1Click;
            _x2Click = p_touchState._x2Click;

            _leftDoubleClick = p_touchState._leftDoubleClick;
            _rightDoubleClick = p_touchState._rightDoubleClick;
            _middleDoubleClick = p_touchState._middleDoubleClick;
            _x1DoubleClick = p_touchState._x1DoubleClick;
            _x2DoubleClick = p_touchState._x2DoubleClick;

            _wheelUp = p_touchState._wheelUp;
            _wheelDown = p_touchState._wheelDown;
            _wheelValue = p_touchState._wheelValue;
#endif

            _clean = p_touchState._clean;
            _oneFlick = p_touchState._oneFlick;
        }
    }

    public class Pax4Touch : PaxState
    {
        public static Pax4Touch _current = null;

        public const int _historySize = 3;//to detect double clicks

        public Pax4TouchState _currentTouchState = null;
        public Pax4TouchState _previousTouchState = null;
        public Pax4TouchState _previousTouchState0 = null;

#if !WINDOWS_MOUSE
        public TouchPanelCapabilities _touchPanelCapabilities;
#endif
        public const int _velocityHistorySize = 6;
        public List<Vector3> _velocity = new List<Vector3>();
        public Vector3 _velocityAvg = Vector3.Zero;
        public float _velocityAvgVal = 0.0f;
        public bool _resetVelocity = false;
        public float _distance = 0.0f;

        public const float _tapVelocityThreshold = 50.0f;

#if !WINDOWS_MOUSE
        private TouchLocation _tl = new TouchLocation();
#endif

        public float _flickVelocityThreshold = 100.0f;

        public Pax4Touch(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {

#if !WINDOWS_MOUSE
            _touchPanelCapabilities = TouchPanel.GetCapabilities();
#endif
            Reset();

            _flickVelocityThreshold *= Pax4Camera._current._scale2.X;

            _current = this;
        }

        public void Reset()
        {
            _currentTouchState = new Pax4TouchState("",null);
            _previousTouchState = new Pax4TouchState("", null);
            _previousTouchState0 = new Pax4TouchState("", null);
        }

        public void Update(GameTime gameTime)
        {
            _previousTouchState0.Set(_previousTouchState);
            _previousTouchState.Set(_currentTouchState);

#if !WINDOWS_MOUSE
            _currentTouchState._state = TouchPanel.GetState();

            if (_currentTouchState._state.Count == 1)
            {
                _currentTouchState._oneTouch = true;
                _currentTouchState._twoTouch = false;
            }
            else if (_currentTouchState._state.Count >= 2)
            {
                _currentTouchState._twoTouch = true;
                _currentTouchState._oneTouch = false;
            }
            else
            {
                _currentTouchState._oneTouch = false;
                _currentTouchState._twoTouch = false;

                if (_previousTouchState._oneTouch == true && _distance < _tapVelocityThreshold)
                    _currentTouchState._oneTap = true;
                else
                    _currentTouchState._oneTap = false;

                if (_previousTouchState._twoTouch == true && _distance < _tapVelocityThreshold)
                    _currentTouchState._twoTap = true;
                else
                    _currentTouchState._twoTap = false;

                if (_currentTouchState._state.Count <= 0)
                {
                    ResetVelocity();
                    UpdateVelocity(gameTime);
                    return;
                }
            }
#else
            _currentTouchState._state = Mouse.GetState();
            //Console.WriteLine(_currentTouchState._xy.ToString());//!*
            if (_currentTouchState._xy.X > Pax4Camera._backBufferWidth
                || _currentTouchState._xy.X < 0
                || _currentTouchState._xy.Y > Pax4Camera._backBufferHeight
                || _currentTouchState._xy.Y < 0)
            {

                _currentTouchState._clean = false;                
                Thread.Sleep(0);
            }
            else
            {
                _currentTouchState._clean = true;
            }
//!*move this to android as well
            if (_currentTouchState._state.LeftButton.Equals(ButtonState.Pressed))
                UpdateVelocity(gameTime);
            else
                ResetVelocity();
#endif            

#if !WINDOWS_MOUSE
            TouchLocation tl0;
            TouchLocation tl1 = _tl;

            int twoTouchDd = 0;
            _currentTouchState._clean = false;

            if (_currentTouchState._state.Count > 0 && _previousTouchState._state.Count <= 0)
                ResetVelocity();

            for (int i = 0; i < _currentTouchState._state.Count; i++)
            {
                tl0 = tl1;
                tl1 = _currentTouchState._state[i];

                if (   i == 0 
                    && (tl1.State == TouchLocationState.Pressed || tl1.State == TouchLocationState.Moved))
                {
                    _currentTouchState._xy.X = tl1.Position.X;
                    _currentTouchState._xy.Y = tl1.Position.Y;

                    if (_previousTouchState._oneTouch || _previousTouchState._twoTouch)
                    {
                        _currentTouchState._dxdy = _currentTouchState._xy - _previousTouchState._xy;
                    }
                    else
                    {
                        _currentTouchState._dxdy.X = 0.0f;
                        _currentTouchState._dxdy.Y = 0.0f;
                        _currentTouchState._dxdy.Z = 0.0f;
                    }

                    _currentTouchState._clean = true;
                    UpdateVelocity(gameTime);
                }

                if (i == 1)
                {
                    _currentTouchState._twoTouchDistance = (int)(tl1.Position - tl0.Position).Length();
                    twoTouchDd = _currentTouchState._twoTouchDistance - _previousTouchState._twoTouchDistance;
                    
                    _currentTouchState._oneTouch = false;

                    break;
                }
            }
            
            if (twoTouchDd > 0)
            {
                _currentTouchState._pinch = true;
                _currentTouchState._unPinch = false;
            }
            else if (twoTouchDd < 0)
            {
                _currentTouchState._pinch = false;
                _currentTouchState._unPinch = true;
            }
            else
            {
                _currentTouchState._pinch = false;
                _currentTouchState._unPinch = false;
            }
#else
            _currentTouchState._xy.X = _currentTouchState._state.X;
            _currentTouchState._xy.Y = _currentTouchState._state.Y;

            _currentTouchState._dxdy = _currentTouchState._xy - _previousTouchState._xy;

            _currentTouchState._wheelValue = _currentTouchState._state.ScrollWheelValue;

            if (_currentTouchState._wheelValue == 0)
            {
                _currentTouchState._wheelUp = false;
                _currentTouchState._wheelDown = false;
            }
            else if (_currentTouchState._wheelValue > 0)
            {
                _currentTouchState._wheelUp = false;
                _currentTouchState._wheelDown = true;
            }
            else
            {
                _currentTouchState._wheelUp = true;
                _currentTouchState._wheelDown = false;
            }

            //left
            if (_currentTouchState._state.LeftButton.Equals(ButtonState.Pressed) && _currentTouchState._clean)
            {
                _currentTouchState._leftDown = true;
                _currentTouchState._leftUp = false;
				_currentTouchState._oneTouch = true;
            }
            else
            {
                _currentTouchState._leftDown = false;
                _currentTouchState._leftUp = true;
				_currentTouchState._oneTouch = false;

                if (   _previousTouchState._leftDown 
					&& _velocityAvgVal < 30 
					&& _velocity.Count > 5 
					&& _currentTouchState._clean)
				{
                    _currentTouchState._oneTap = true;
				}
                else
                    _currentTouchState._oneTap = false;
            }
            if (_previousTouchState._oneTap && _previousTouchState._oneTap)
                _currentTouchState._leftDoubleClick = true;
            else
                _currentTouchState._leftDoubleClick = false;

            //right
            if (_currentTouchState._state.RightButton.Equals(ButtonState.Pressed))
            {
                _currentTouchState._rightDown = true;
                _currentTouchState._rightUp = false;
            }
            else
            {
                _currentTouchState._rightDown = false;
                _currentTouchState._rightUp = true;

                if (_currentTouchState._rightUp && _previousTouchState._rightDown)
                    _currentTouchState._rightClick = true;
                else
                    _currentTouchState._rightClick = false;
            }
            if (_previousTouchState._rightClick && _previousTouchState0._rightClick)
                _currentTouchState._rightDoubleClick = true;
            else
                _currentTouchState._rightDoubleClick = false;

            //middle
            if (_currentTouchState._state.MiddleButton.Equals(ButtonState.Pressed))
            {
                _currentTouchState._middleDown = true;
                _currentTouchState._middleUp = false;
            }
            else
            {
                _currentTouchState._middleDown = false;
                _currentTouchState._middleUp = true;

                if (_currentTouchState._middleUp && _previousTouchState._middleDown)
                    _currentTouchState._middleClick = true;
                else
                    _currentTouchState._middleClick = false;
            }
            if (_previousTouchState._middleClick && _previousTouchState0._middleClick)
                _currentTouchState._middleDoubleClick = true;
            else
                _currentTouchState._middleDoubleClick = false;

            //x1
            if (_currentTouchState._state.XButton1.Equals(ButtonState.Pressed))
            {
                _currentTouchState._x1Down = true;
                _currentTouchState._x1Up = false;
            }
            else
            {
                _currentTouchState._x1Down = false;
                _currentTouchState._x1Up = true;

                if (_currentTouchState._x1Up && _previousTouchState._x1Down)
                    _currentTouchState._x1Click = true;
                else
                    _currentTouchState._x1Click = false;
            }
            if (_previousTouchState._x1Click && _previousTouchState0._x1Click)
                _currentTouchState._x1DoubleClick = true;
            else
                _currentTouchState._x1DoubleClick = false;

            //x2
            if (_currentTouchState._state.XButton2.Equals(ButtonState.Pressed))
            {
                _currentTouchState._x2Down = true;
                _currentTouchState._x2Up = false;
            }
            else
            {
                _currentTouchState._x2Down = false;
                _currentTouchState._x2Up = true;

                if (_currentTouchState._x2Up && _previousTouchState._x2Down)
                    _currentTouchState._x2Click = true;
                else
                    _currentTouchState._x2Click = false;
            }
            if (_previousTouchState._x2Click && _previousTouchState0._x2Click)
                _currentTouchState._x2DoubleClick = true;
            else
                _currentTouchState._x2DoubleClick = false;
#endif
        }

        private void UpdateVelocity(GameTime gameTime)
        {
            if (_resetVelocity)
                _velocity.Clear();

            if (_velocity.Count >= _velocityHistorySize)
                _velocity.RemoveAt(0);

            if (_currentTouchState._clean && !_resetVelocity)
            {
                _velocityAvg = _currentTouchState._dxdy / (float)gameTime.ElapsedGameTime.TotalSeconds;
                _distance += _currentTouchState._dxdy.Length();
            }
            else
            {
                _velocityAvg = Vector3.Zero;
                _resetVelocity = false;
            }

            _velocity.Add(_velocityAvg);

            for (int i = 0; i < _velocity.Count - 1; i++)
                _velocityAvg += _velocity[i];
            _velocityAvg /= _velocity.Count;

            _velocityAvgVal = _velocityAvg.Length();
            if (_velocityAvgVal > _flickVelocityThreshold)
                _currentTouchState._oneFlick = true;
            else
                _currentTouchState._oneFlick = false;
        }

        private void ResetVelocity()
        {
            _resetVelocity = true;
            _distance = 0.0f;
        }
    }
}