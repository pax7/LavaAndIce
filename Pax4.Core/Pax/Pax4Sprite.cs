using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pax.Core;

namespace Pax4.Core
{ 
    [DataContract]
    [KnownType(typeof(Pax4Sprite))]
    public class Pax4Sprite : Pax4Object
    {
        #region Class Members
        [DataMember]
        public Vector2 _centerPosition0 = Vector2.Zero;

        [DataMember]
        public Vector2 _centerPosition = Vector2.Zero;

        [DataMember]
        public Rectangle _rectangle0;

        [DataMember]
        public Rectangle _rectangleScaled;

        [DataMember]
        public Rectangle _rectangleDraw;

        [DataMember]
        public Color _color = Color.White;

        [DataMember]
        public float _rotationZ0 = 0.0f;

        [DataMember]
        public float _rotationZ = 0.0f;

        [DataMember]
        public int _leftThreshold = 0;

        [DataMember]
        public int _topThreshold = 0;

        [DataMember]
        public int _rightThreshold = 0;

        [DataMember]
        public int _bottomThreshold = 0;

        [DataMember]
        public int _leftViewingThreshold = 0;

        [DataMember]
        public int _rightViewingThreshold = 0;

        [DataMember]
        public int _topViewingThreshold = 0;

        [DataMember]
        public int _bottomViewingThreshold = 0;

        [IgnoreDataMember]
        public bool _oneTouch = false;

        [IgnoreDataMember]
        public bool _oneTap = false;

        [IgnoreDataMember]
        public bool _oneFlick = false;

        [DataMember]
        public Vector2 _positionScale0 = Vector2.One;

        [DataMember]
        public Vector2 _positionScale = Vector2.One;

        [DataMember]
        public Vector2 _relativePosition = Vector2.Zero;

        [DataMember]
        public Vector2 _originDraw0 = Vector2.Zero;

        [DataMember]
        public Vector2 _originDraw = Vector2.Zero;

        [DataMember]
        public Vector2 _originScaledDraw = Vector2.Zero;

        [DataMember]
        public Vector2 _originRelativeScaledDraw = Vector2.Zero;

        [DataMember]
        public Vector2 _topLeft = Vector2.Zero;

        [DataMember]
        public Vector2 _bottomRight = Vector2.Zero;

        [DataMember]
        public Vector2 _centerPositionDraw = Vector2.Zero;
        
        [DataMember]
        public float   _scaleDraw0 = 1.0f;
        
        [DataMember]
        public float   _scaleDraw = 1.0f;

        [IgnoreDataMember]
        public bool _skipRectangleDraw = false;

        [IgnoreDataMember]
        public bool _skipUpdate = false;

        [IgnoreDataMember]
        public bool _isDesignMode = false;
        #endregion

        public Pax4Sprite(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            if (p_parent0 != null && p_parent0 is Pax4Sprite)
            {
                _centerPosition = ((Pax4Sprite)p_parent0)._centerPosition + _centerPosition0;
                _rightViewingThreshold = ((Pax4Sprite)p_parent0)._rightViewingThreshold;
                _bottomViewingThreshold = ((Pax4Sprite)p_parent0)._bottomViewingThreshold;
                _topViewingThreshold = ((Pax4Sprite)p_parent0)._topViewingThreshold;
                _leftViewingThreshold = ((Pax4Sprite)p_parent0)._leftViewingThreshold;
            }
            else
            {
                if (Pax4Game._graphicsDeviceManager != null)
                {
                    _rightViewingThreshold = Pax4Game._graphicsDeviceManager.PreferredBackBufferWidth;
                    _bottomViewingThreshold = Pax4Game._graphicsDeviceManager.PreferredBackBufferHeight;
                }
                else
                {
                    _rightViewingThreshold = 0;
                    _bottomViewingThreshold = 0;
                }
                 _topViewingThreshold = 0;
                _leftViewingThreshold = 0;
            }
        }

        public override void Update(GameTime gameTime)
        {
            
            if (_parent0 != null)
            {
                if (_skipUpdate && ((Pax4Sprite)_parent0)._skipUpdate)
                    return;

                _scaleDraw = ((Pax4Sprite)_parent0)._scaleDraw * _scaleDraw0;
                _rotationZ = ((Pax4Sprite)_parent0)._rotationZ + _rotationZ0;
                _positionScale = ((Pax4Sprite)_parent0)._positionScale * _positionScale0;

                _centerPosition = ((Pax4Sprite)_parent0)._centerPosition + _centerPosition0 ;
                _centerPositionDraw = _centerPosition;
            }
            else
            {
                if (_skipUpdate)
                    return;

                _centerPositionDraw = _centerPosition;
            }

            UpdateThreshold();

            _skipUpdate = true;
        }

        public override void Draw(GameTime gameTime)
        {
        }

        [Intent(typeof(Pax4Sprite), "SetPosition", typeof(Vector2), "p_position0")]
        public virtual void SetPosition(Vector2 p_position0)
        {
            _relativePosition = p_position0;

            _centerPosition0 = _relativePosition * Pax4Game._current.GetPreferredBackBufferVector();
                        
            _centerPosition = _parent0!=null? ((Pax4Sprite)_parent0)._centerPosition + _centerPosition0 : _centerPosition0;
            UpdateThreshold();
        }

        [Intent(typeof(Pax4Sprite), "SetPositionAbsolute", typeof(Vector2), "p_position0")]
        public virtual void SetPositionAbsolute(Vector2 p_position0)
        {
            _relativePosition = p_position0;

            _centerPosition0 = _relativePosition;

            _centerPosition = _parent0 != null ? ((Pax4Sprite)_parent0)._centerPosition + _centerPosition0 : _centerPosition0;
            UpdateThreshold();
        }

        [Intent(typeof(Pax4Sprite), "SetRotationZ", typeof(float), "p_rotationZ")]
		public void SetRotationZ(float p_rotationZ)
        {
            _rotationZ0 = p_rotationZ;
            _rotationZ = _rotationZ0;

            _skipUpdate = false;
        }

        [Intent(typeof(Pax4Sprite), "SetRectangleWidthHeightVector2", typeof(Vector2), "p_widthHeight")]
        public virtual void SetRectangleWidthHeight(Vector2 p_widthHeight)
        {
            SetRectangleWidthHeight((int)p_widthHeight.X, (int)p_widthHeight.Y);
        }

        [Intent(typeof(Pax4Sprite), "SetRectangleWidthHeightTexture", typeof(Texture2D), "p_texture")]
        public virtual void SetRectangleWidthHeight(Texture2D p_texture)
        {
            SetRectangleWidthHeight(p_texture.Width, p_texture.Height);
        }

        [Intent(typeof(Pax4Sprite), "SetRectangleWidthHeightIntInt", typeof(int), "p_width", typeof(int), "p_height")]
        public virtual void SetRectangleWidthHeight(int p_width, int p_height)
        {
            _rectangle0.Width = p_width;
            _rectangle0.Height = p_height;
            _rectangleDraw = _rectangle0;
            
            _originDraw0.X = _rectangle0.Width / 2.0f;
            _originDraw0.Y = _rectangle0.Height / 2.0f;
            _originDraw = _originDraw0;
            
            _rectangleScaled = _rectangle0;
            _rectangleScaled.Width *= (int)_scaleDraw0;
            _rectangleScaled.Height *= (int)_scaleDraw0;

            _originScaledDraw = _originDraw0;
            _originScaledDraw.X *= _scaleDraw0;
            _originScaledDraw.Y *= _scaleDraw0;

            _originRelativeScaledDraw = _originScaledDraw / Pax4Game._current.GetPreferredBackBufferVector();

            _skipUpdate = false;
        }
		
        public void UpdateThreshold()
        {
            _topLeft = (_centerPosition - _originScaledDraw);
            _bottomRight = (_centerPosition + _originScaledDraw);

            //Set Touch Thresholds Bouund Based on Viewing Threasholds
            _leftThreshold = Pax4Tools.BoundOutput((int)_topLeft.X, Pax4Game._graphicsDeviceManager.PreferredBackBufferWidth, _leftViewingThreshold);
            _rightThreshold = Pax4Tools.BoundOutput((int)_bottomRight.X, _rightViewingThreshold);
            _topThreshold = Pax4Tools.BoundOutput((int)_topLeft.Y, Pax4Game._graphicsDeviceManager.PreferredBackBufferHeight, _topViewingThreshold);
            _bottomThreshold = Pax4Tools.BoundOutput((int)_bottomRight.Y, _bottomViewingThreshold);
            //_leftThreshold = (int)(_centerPosition.X - _originScaledDraw.X);
            //_rightThreshold = (_leftThreshold + _rectangleScaled.Width);
            //_topThreshold = (int)(_centerPosition.Y - _originScaledDraw.Y);
            //_bottomThreshold = (_topThreshold + _rectangleScaled.Height);
            SetRectangleDraw();
        }

        [Intent(typeof(Pax4Sprite), "SetColor", typeof(Color), "p_color")]
        public virtual void SetColor(Color p_color)
        {
            _color = p_color;

            _skipUpdate = false;
        }

        [Intent(typeof(Pax4Sprite), "SetScale", typeof(float), "p_scale")]
        public virtual void SetScale(float p_scale)
        {
            _scaleDraw0 = p_scale;
            _scaleDraw = _scaleDraw0;

            _rectangleScaled = _rectangle0;
            _rectangleScaled.Width *= (int)p_scale;
            _rectangleScaled.Height *= (int)p_scale;

            _originScaledDraw = _originDraw0;
            _originScaledDraw.X *= p_scale;
            _originScaledDraw.Y *= p_scale;

            _skipUpdate = false;
        }

        public bool Touched() //Touched Spirte Object
        {
            return
                 Pax4Touch._current._currentTouchState._xy.X >= _leftThreshold
              && Pax4Touch._current._currentTouchState._xy.X <= _rightThreshold
              && Pax4Touch._current._currentTouchState._xy.Y >= _topThreshold
              && Pax4Touch._current._currentTouchState._xy.Y <= _bottomThreshold;
        } 
        public bool ViewableTouched() //Touched Total Viewing Area
        {
            bool result = Pax4Touch._current._currentTouchState._xy.X >= _leftViewingThreshold
              && Pax4Touch._current._currentTouchState._xy.X <= _rightViewingThreshold
              && Pax4Touch._current._currentTouchState._xy.Y >= _topViewingThreshold
              && Pax4Touch._current._currentTouchState._xy.Y <= _bottomViewingThreshold;

            if (_isDesignMode && result)
            {
                //Send Intent
                PaxIntent intent = new PaxIntent();
                intent.SetPath(GetPath0());
                intent.SetIntent("SyncSerialize");
                intent.SetReturnIntent();
                intent._returnIntent.SetPath("localhost", "_root");
                PaxIntent.Enqueue(intent);
            }

            return result;
        }

        public void SetDisabledInvisible()
        {
            bool isNotOnScreen = GetIsNotOnScreen();

            _isInvisible = isNotOnScreen;
            if (isNotOnScreen) 
            {
                _rectangleDraw = Rectangle.Empty;
            }
            else if (_isDisabled == true) //Triggers only when moving from Disabled to Not Disabled
            { 
                //Reset Draw Rectangle and Recalculate It.
                _rectangleDraw = _rectangle0;
                UpdateThreshold();
            }

            _isDisabled = isNotOnScreen;

            _skipUpdate = false;
        }

        private void SetRectangleDraw()
        {
            if (_skipRectangleDraw || _isInvisible)
                return;

            _topLeft = _centerPosition -_originScaledDraw;
            _bottomRight = _centerPosition +_originScaledDraw;
            bool modifications = false;

            if (_topLeft.X < _leftViewingThreshold) // Decreases Width of retangle and adds to the Rectangles X axis which adds to the Postion within the draw;
            {
                var e = (_leftViewingThreshold - (int)_topLeft.X);
                e *= Math.Sign(e);
                _rectangleDraw.Width = _rectangle0.Width - e;
                _rectangleDraw.X = _rectangle0.X + e;
                modifications = true;
            }
            else if (_bottomRight.X > _rightViewingThreshold) //Between on and the right of the screen decrease the draw rectangle
            {
                _rectangleDraw = _rectangle0;
                _rectangleDraw.Width = _rectangle0.Width + (_rightViewingThreshold - (int)_bottomRight.X);
                modifications = true;
            }

            if (_topLeft.Y < _topViewingThreshold) 
            {
                var e = (_topViewingThreshold - (int)_topLeft.Y);
                e *= Math.Sign(e);
                _rectangleDraw.Height = _rectangle0.Height - e;
                _rectangleDraw.Y = _rectangle0.Y + e;
                modifications = true;
            }
            else if (_bottomRight.Y > _bottomViewingThreshold)
            {
                _rectangleDraw = _rectangle0;
                _rectangleDraw.Height = _rectangle0.Height + (_bottomViewingThreshold - (int)_bottomRight.Y);
                modifications = true;
            }

            if (!modifications) //Normal case;
            {
                _rectangleDraw = _rectangle0;
            }

            _skipUpdate = false;
        }

        public void SetViewingThreshold(int p_left, int p_right,int p_top,int p_bottom)
        {
            _leftViewingThreshold = p_left;
            _rightViewingThreshold = p_right;
            _topViewingThreshold = p_top;
            _bottomViewingThreshold = p_bottom;

            _skipUpdate = false;
        }
        public void SetViewingThreshold(Pax4Sprite p_parent)
        {
            _leftViewingThreshold = p_parent._leftViewingThreshold;
            _rightViewingThreshold = p_parent._rightViewingThreshold;
            _topViewingThreshold = p_parent._topViewingThreshold;
            _bottomViewingThreshold = p_parent._bottomViewingThreshold;

            _skipUpdate = false;
        }

        public bool GetIsNotOnScreen()
        {
            _skipUpdate = false;

            _topLeft = _centerPosition - _originScaledDraw;
            _bottomRight = _centerPosition + _originScaledDraw;

            if (_topLeft.X > _rightViewingThreshold)
            {
                return true;
            }
            if (_topLeft.Y > _bottomViewingThreshold)
            {
                return true;
            }
            if (_bottomRight.X < _leftViewingThreshold)
            {
                return true;
            }
            if (_bottomRight.Y < _topViewingThreshold)
            {
                return true;
            }
            return false;
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