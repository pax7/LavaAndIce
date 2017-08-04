using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pax.Core;
using System.IO;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4Slider))]
    public class Pax4Slider : Pax4Sprite
    {
        #region Class Members
        [IgnoreDataMember]
        public List<Pax4Sprite> _childSprite = null;

        [DataMember]
        public float _horizontalSpacing = 50.0f;

        [DataMember]
        public float _verticalSpacing = 10.0f;

        [DataMember]
        public float _nextPosition = 0.0f;

        [DataMember]
        public float _nextPosition0 = 0.0f;

        [DataMember]
        public float _dnextPosition = 0.0f;

        [DataMember]
        public bool _verticalScroll = true;

        [DataMember]
        public bool _alphaEdges = false;
        //private Texture2D _alphaEdgesTexture = null;

        [DataMember]
        public bool _scaleEdges = false;

        [DataMember]
        public float _bgScale = 0.75f;

        [DataMember]
        public int _leftTouchThreshold = 0;

        [DataMember]
        public int _rightTouchThreshold = 0;

        [DataMember]
        public int _topTouchThreshold = 0;

        [DataMember]
        public int _bottomTouchThreshold = 0;

        [IgnoreDataMember]
        private Pax4SpritePositionModifier _positionModifier = null;

        [DataMember]
        public Vector2 _snapPosition = Vector2.Zero;

        [DataMember]
        public Pax4Sprite _currentSnapSprite = null;

        [DataMember]
        public int _snapSpriteIndex = 0;

        [DataMember]
        public Vector2 _snapVector = Vector2.Zero;

        [DataMember]
        public Vector2 _velocity = Vector2.Zero;

        [DataMember]
        public bool _snap = true;

        [DataMember]
        public bool _accelerate = false;

        [IgnoreDataMember]
        private Vector2 _tempPosition = Vector2.Zero;
        #endregion

        public Pax4Slider(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            _positionModifier = new Pax4SpritePositionModifier("_positionModifier", null);
            _positionModifier.AddChild(this);
            _positionModifier.Ini(0.1f, 0.0f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_isDisabled) //Added by Pony
                return;

            bool onetap = false;
            _accelerate = false;

            if (ViewableTouched())
            {
#if WINDOWS
                if (Pax4Touch._current._currentTouchState._clean && Pax4Touch._current._currentTouchState._oneTouch)
#else
				if (Pax4Touch._current._currentTouchState._clean)
#endif
                {
                    _oneTouch = true;

                    _positionModifier.Stop();

                    _tempPosition = _centerPosition;
                    if (_verticalScroll)
                    {
                        _tempPosition.Y += Pax4Touch._current._currentTouchState._dxdy.Y;
                    }
                    else
                    {
                        _tempPosition.X += Pax4Touch._current._currentTouchState._dxdy.X;
                    }

                    SetPositionAbsolute(_tempPosition);

                    _accelerate = true;
                }
                else
                {
                    onetap = true;
                    _oneTap = Pax4Touch._current._currentTouchState._oneTap;
                }
            }
            else
            { // Cause Flick If touch moves outside Viewable Touch Area
                if (Pax4Touch._current._velocityAvgVal > 30 && _oneTouch)
                {
                    _oneFlick = true;
                    _accelerate = true;
                    _oneTap = false;
                    //Pax4Touch._current._currentTouchState._oneTap = false;
                }

                _oneTouch = false;

            }

            if(onetap || _oneFlick)
            {
                if (_oneTouch)
                {
                    if (Pax4Touch._current._currentTouchState._oneFlick)
                        _accelerate = true;

                    _oneTouch = false;
                }

                if (_accelerate)
                {
                    if (_verticalScroll)
                    {
                        _velocity.X = 0.0f;
                        _velocity.Y = Pax4Touch._current._velocityAvg.Y;
                    }
                    else
                    {
                        _velocity.X = Pax4Touch._current._velocityAvg.X;
                        _velocity.Y = 0.0f;
                    }

                    _positionModifier.IniVelocity0Acceleration(_velocity, 0.40f, Vector2.Zero, Pax4Game._current.GetPreferredBackBufferVector(), true);
                    _positionModifier.Trigger();

                    _accelerate = false;

                    onetap = false;
                    _oneFlick = false;
                }

            }

            if (_positionModifier != null && !_positionModifier._done)
            {
                _positionModifier.Update(gameTime);
            }

            //Recover From children being thrown off screen
            if (_childSprite.Count > 0 && !_oneTouch && !_oneTap)
            {
                var lastChildTopLeft = _childSprite[_childSprite.Count - 1]._centerPositionDraw - _childSprite[_childSprite.Count - 1]._originScaledDraw;
                var firstChildBottomRight = _childSprite[0]._centerPositionDraw + _childSprite[0]._originScaledDraw;

                var bounded = false;

                _tempPosition = _centerPosition;

                if (!_verticalScroll)
                {
                    bounded = Pax4Tools.BoundOutput(ref _tempPosition.X,
                        (_centerPositionDraw - firstChildBottomRight).X + _rightViewingThreshold,
                        _leftViewingThreshold - (lastChildTopLeft - _centerPositionDraw).X);
                }
                else
                {
                    bounded = Pax4Tools.BoundOutput(ref _tempPosition.Y,
                        (_centerPositionDraw - firstChildBottomRight).Y + _bottomViewingThreshold,
                        _topViewingThreshold - (lastChildTopLeft - _centerPositionDraw).Y);
                }

                if (bounded)
                {
                    //Recover if PositionModifier is Done moving Or if all Children are not visable.
                    if (_positionModifier != null && (_positionModifier._done || (_childSprite[_childSprite.Count - 1]._isInvisible && _childSprite[0]._isInvisible)))
                    {
                        _positionModifier.Ini(_centerPosition, _tempPosition, .3f);
                        _positionModifier.Trigger();
                    }
                }
            }
            
            if (_childSprite != null)
            {
                for (int i = 0; i < _childSprite.Count; i++)
                {
                    _childSprite[i].Update(gameTime);
                    _childSprite[i].SetDisabledInvisible();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (_childSprite == null)
                return;

            for (int i = 0; i < _childSprite.Count; i++)
                _childSprite[i].Draw(gameTime);
        }

        //[Intent(_type, "AddChild", typeof(Pax4Sprite), "p_child")]
        public override void AddChild(PaxState p_state, bool p_recursive = true)
        {
            if (p_state != null)
            {
                base.AddChild(p_state, p_recursive);

                if (!(p_state is Pax4Sprite))
                    return;

                Pax4Sprite state = (Pax4Sprite)p_state;

                if (_childSprite == null)
                    _childSprite = new List<Pax4Sprite>();

                _childSprite.Add(state);

                if (_childSprite.Count == 1)
                    _currentSnapSprite = state;

                if (_verticalScroll)
                {
                        state.SetPosition(new Vector2(0.0f, _nextPosition + state._originRelativeScaledDraw.Y));
                        _nextPosition += .15f;
                        this.SetRectangleWidthHeight(_rectangle0.Width, _rectangle0.Height + state._rectangle0.Height);
                        //p_child.SetViewingThreshold(_leftViewingThreshold, _rightViewingThreshold,_topViewingThreshold,_bottomViewingThreshold);

                        _dnextPosition = Math.Abs(_nextPosition - _nextPosition0);
                }
                else
                {
                        state.SetPosition(new Vector2(_nextPosition + state._originRelativeScaledDraw.X, 0.0f));
                        _nextPosition += .65f;
                        this.SetRectangleWidthHeight((int)(_rectangle0.Width + state._rectangle0.Width), _rectangle0.Height);
                        // p_child.SetViewingThreshold(_leftViewingThreshold, _rightViewingThreshold, _topViewingThreshold, _bottomViewingThreshold);
                        _dnextPosition = Math.Abs(_nextPosition - _nextPosition0);
                }

                
                p_state._parent0 = this;
            }

            _nextPosition0 = _nextPosition;


        }

        //[Intent(_type, "Enable")]
        public override void Enable()
        {
            if (!_isDisabled)
                return;

            _isDisabled = false;
			if (_childSprite == null)
                return;

            for (int i = 0; i < _childSprite.Count; i++)
                _childSprite[i].Enable();

            base.Enable();
        }

        //[Intent(_type, "Disable")]
        public override void Disable()
        {
            if (_isDisabled)
                return;

            _isDisabled = true;

            _positionModifier.Stop();

            for (int i = 0; i < _childSprite.Count; i++)
                _childSprite[i].Disable();

            base.Disable();
        }

        //public void SetSnapPosition(Vector2 p_snapPosition)
        //{
        //    _snapPosition = p_snapPosition * Pax4Camera._current._scale2;
        //    _snapVector = _snapPosition;

        ////    _breakPositionTopLeft = _snapPosition;
        ////    _breakPositionBottomRight = _snapPosition;
        //}

        //public void GetNextSnapSprite(bool p_rightDown = true)
        //{
        //    if (p_rightDown)
        //        _snapSpriteIndex--;
        //    else
        //        _snapSpriteIndex++;

        //    if (_snapSpriteIndex < 0)
        //    {
        //        _snapSpriteIndex = 0;
        //        return;
        //    }
        //    else if (_snapSpriteIndex >= _childSprite.Count)
        //    {
        //        _snapSpriteIndex = _childSprite.Count - 1;
        //        return;
        //    }

        //    _currentSnapSprite = _childSprite[_snapSpriteIndex];
        //    _snapVector = _centerPosition + (_snapPosition - _currentSnapSprite._centerPosition);

        //    //EnableSnapSprite();
        //}

        //public void GetClosestSnapSprite(bool p_nextClosest = false)
        //{
        //    float currentDistance = 999999.999f;
        //    float smallestDistance = 999999.999f;
        //    int id = _currentSnapSprite._uid;

        //    if (p_nextClosest)
        //    {
        //        if (_childSprite[0]._uid == id)
        //        {
        //            if (_verticalScroll)
        //            {
        //                if (Pax4Touch._current._velocityAvg.Y > 0.0f)
        //                    return;
        //            }
        //            else
        //            {
        //                if (Pax4Touch._current._velocityAvg.X > 0.0f)
        //                    return;
        //            }
        //        }
        //        else if (_childSprite[_childSprite.Count - 1]._uid == id)
        //        {
        //            if (_verticalScroll)
        //            {
        //                if (Pax4Touch._current._velocityAvg.Y < 0.0f)
        //                    return;
        //            }
        //            else
        //            {
        //                if (Pax4Touch._current._velocityAvg.X < 0.0f)
        //                    return;
        //            }
        //        }
        //    }

        //    for (int i = 0; i < _childSprite.Count; i++)
        //    {
        //        if (_childSprite[i]._uid == id)
        //            continue;

        //        currentDistance = (_childSprite[i]._centerPosition - _snapPosition).Length();
        //        if (currentDistance < smallestDistance)
        //        {
        //            smallestDistance = currentDistance;
        //            _currentSnapSprite = _childSprite[i];
        //            _snapSpriteIndex = i;
        //        }
        //    }

        //    _snapVector = _centerPosition + (_snapPosition - _currentSnapSprite._centerPosition);

        //    //EnableSnapSprite();
        //}

        //public void EnableSnapSprite()
        //{
        //    for (int i = 0; i < _child.Count; i++)
        //    {
        //        if(_child[i]._id == _currentSnapSprite._id)                    
        //            _child[i].Enable();
        //        else
        //            _child[i].Disable();
        //    }
        //}

        //[Intent(_type, "SetColor", typeof(Color), "p_color")]
        public override void SetColor(Color p_color)
        {
            base.SetColor(p_color);

            for (int i = 0; i < _childSprite.Count; i++)
                _childSprite[i].SetColor(p_color);
        }

        public override void Exe(PaxIntent p_intent)
        {
            base.Exe(p_intent);
            _skipUpdate = false;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}