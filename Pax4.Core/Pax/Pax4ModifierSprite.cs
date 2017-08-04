using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pax.Core;
using System.Runtime.Serialization;
using System.IO;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ModifierSprite))]
    public class Pax4ModifierSprite : Pax4Modifier
    {
        [DataMember]
        public Pax4Sprite _currentSprite = null;

        //public Pax4ModifierSprite(String p_name, Pax4Object p_parent0)
        //    : base(p_name, p_parent0)
        //{
        //    AddSprite(p_sprite);
        //    _currentSprite = p_sprite;
        //}

        public Pax4ModifierSprite(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {            
        }

        public override void AddChild(PaxState p_state, bool p_recursive = true)
        {
            if(_currentSprite == null)
                _currentSprite = (Pax4Sprite)p_state;

            base.AddChild(p_state, p_recursive);
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4SpriteColorModifier))]
    public class Pax4SpriteColorModifier : Pax4ModifierSprite
    {
        [IgnoreDataMember]
        private Vector3 _color0;

        [IgnoreDataMember]
        private Vector3 _color1;

        [IgnoreDataMember]
        private Vector3 _color;

        [IgnoreDataMember]
        private Color _colour;

        [IgnoreDataMember]
        private Vector3 _colorVelocity;

        public Pax4SpriteColorModifier(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (_done)
                return;

            base.Update(gameTime);

            if (_timer > _duration)
                return;

            _color = _color0 + _colorVelocity * _dt;           
            
            if (_done)
            {
                _color = _color1;

                if (_oscillating)
                {
                    Ini(_color1, _color0, _duration);
                    Trigger();
                }
            }
            
            _colour.R = (byte)(255.0f * _color.X);
            _colour.G = (byte)(255.0f * _color.Y);
            _colour.B = (byte)(255.0f * _color.Z);

            Pax4Sprite sprite = null;

            foreach (KeyValuePair<String, PaxState> kvp in GetChild())
            {
                if (kvp.Value is Pax4Sprite)
                {   
                    sprite = (Pax4Sprite)kvp.Value;
                    sprite._color.R = _colour.R;
                    sprite._color.G = _colour.G;
                    sprite._color.B = _colour.B;
                }
            }
        }

        public override bool Trigger()
        {
            if (GetChild() != null && base.Trigger())
            {
                Pax4Sprite sprite = null;
                foreach (KeyValuePair<String, PaxState> kvp in GetChild())
                {
                    if (kvp.Value is Pax4Sprite)
                    {
                        sprite = (Pax4Sprite)kvp.Value;
                        sprite._color.R = (byte)(255.0f * _color0.X);
                        sprite._color.G = (byte)(255.0f * _color0.Y);
                        sprite._color.B = (byte)(255.0f * _color0.Z);
                    }
                }

                return true;
            }

            return false;
        }

        public void Ini(Color p_color0, Color p_color1, float p_duration, float p_delay = 0.0f)
        {
            Ini(p_color0.ToVector3(), p_color1.ToVector3(), p_duration, p_delay);
        }

        public void Ini(Vector3 p_color0, Vector3 p_color1, float p_duration, float p_delay = 0.0f)
        {
            base.Ini(p_duration, p_delay);

            _color0 = p_color0;
            _color1 = p_color1;

            _colorVelocity = (_color1 - _color0) / _duration;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4SpriteAlphaModifier))]
    public class Pax4SpriteAlphaModifier : Pax4ModifierSprite
    {
        [IgnoreDataMember]
        private float _alpha0 = 0.0f;

        [IgnoreDataMember]
        private float _alpha1 = 1.0f;

        [IgnoreDataMember]
        private float _alpha;

        [IgnoreDataMember]
        private float _alphaVelocity = 0.0f;

        public Pax4SpriteAlphaModifier(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (_done)
                return;

            base.Update(gameTime);

            if (_timer > _duration)
                return;

            _alpha = _alpha0 + _alphaVelocity * _dt;

            if (_done)
            {
                _alpha = _alpha1;

                if (_oscillating)
                {
                    Ini(_alpha1, _alpha0, _duration);
                    Trigger();
                }
            }

            _alpha = MathHelper.Clamp(255.0f * _alpha, 0.0f, 255.0f);
            
            Pax4Sprite sprite = null;
            foreach (KeyValuePair<String, PaxState> kvp in GetChild())
            {
                if (kvp.Value is Pax4Sprite)
                {
                    sprite = (Pax4Sprite)kvp.Value;
                    sprite._color.A = (byte)_alpha;
                }
            }
        }

        public override bool Trigger()
        {
            if (GetChild() != null && base.Trigger())
            {
                _alpha = MathHelper.Clamp(255.0f * _alpha0, 0.0f, 255.0f);

                Pax4Sprite sprite = null;
                foreach (KeyValuePair<String, PaxState> kvp in GetChild())
                {
                    if (kvp.Value is Pax4Sprite)
                    {
                        sprite = (Pax4Sprite)kvp.Value;
                        sprite._color.A = (byte)_alpha;
                    }
                }

                return true;
            }

            return false;
        }

        public void Ini(float p_alpha0, float p_alpha1, float p_duration, float p_delay = 0.0f)
        {
            base.Ini(p_duration, p_delay);

            _alpha0 = p_alpha0;
            _alpha1 = p_alpha1;

            _alphaVelocity = (_alpha1 - _alpha0) / _duration;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4SpritePositionModifier))]
    public class Pax4SpritePositionModifier : Pax4ModifierSprite
    {
        [IgnoreDataMember]
        private Vector2 _position0;

        [IgnoreDataMember]
        private Vector2 _position1;

        [IgnoreDataMember]
        private Vector2 _position2;

        [IgnoreDataMember]
        private Vector2 _position;

        [IgnoreDataMember]
        private Vector2 _breakPositionTopLeft;

        [IgnoreDataMember]
        private Vector2 _breakPositionBottomRight;

        [IgnoreDataMember]
        private Vector2 _breakVelocityThreshold;

        [IgnoreDataMember]
        private Vector2 _velocity0 = Vector2.Zero;

        [IgnoreDataMember]
        private Vector2 _velocity1;

        [IgnoreDataMember]
        public Vector2 _velocity;

        [IgnoreDataMember]
        private Vector2 _acceleration0;

        [IgnoreDataMember]
        private bool _break = false;

        public Pax4SpritePositionModifier(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }
        
        public override void Update(GameTime gameTime)
        {
            if (_done)
                return;

            base.Update(gameTime);

            if (_timer > _duration)
                return;

            Pax4Sprite sprite = null;
            
            if (_setState1 && _done)
            {
                foreach (KeyValuePair<String, PaxState> kvp in GetChild())
                {
                    if (kvp.Value is Pax4Sprite)
                    {
                        sprite = (Pax4Sprite)kvp.Value;
                        sprite.SetPositionAbsolute(_position);
                    }
                }
                return;
            }

            _position1 = _position;
            _velocity = (_velocity0 + _acceleration0 * _dt);
            _position = _position0 + _velocity * _dt;

            if (_break)
            {
                //if the position change is no longer alligned with the orginal velocity stop.
                if (Vector2.Dot(_position - _position1, _velocity0) < 0) 
                {
                    _break = false;
                    _done = true;
                }
            }
                        
            foreach (KeyValuePair<String, PaxState> kvp in GetChild())
            {
                if (kvp.Value is Pax4Sprite)
                {
                    sprite = (Pax4Sprite)kvp.Value;
                    sprite.SetPositionAbsolute(_position);
                }
            }
        }

        public override bool Trigger()
        {
            if (base.Trigger())
            {
                //for (int i = 0; i < _sprite.Count; i++)
                //{
                //    _sprite[i].SetPositionAbsolute(_position);
                //}
                return true;
            }
            return false;
        }

        public void IniVelocity0Acceleration(Vector2 p_velocity0, float p_accelerationFactor, Vector2 p_breakPositionTopLeft, Vector2 p_breakPositionBottomRight, bool p_break = false)
        {
            _position0 = _currentSprite._centerPosition;
            _velocity0 = p_velocity0;

            //_velocity0.X = Pax4Tools.BoundOutput(_velocity0.X, 200f, -200f);

            if (p_break)
            {
                _break = p_break;

                _acceleration0 = -p_accelerationFactor * _velocity0;
                _velocity1 = Vector2.Zero;

                _breakVelocityThreshold = p_velocity0 / 2.0f;
                _breakVelocityThreshold.X = Math.Abs(_breakVelocityThreshold.X);
                _breakVelocityThreshold.Y = Math.Abs(_breakVelocityThreshold.Y);

                _breakPositionTopLeft = p_breakPositionTopLeft;
                _breakPositionBottomRight = p_breakPositionBottomRight;


                _duration = 999.0f;
            }
            else
            {
                _duration = 1.0f;
                _acceleration0 = p_accelerationFactor * _velocity0;
                _velocity1 = _velocity0;
            }


            _setState1 = false;
        }

        public void IniPosition1(Vector2 p_position1)
        {
            _position0 = _currentSprite._centerPosition;
            _position1 = p_position1;

            _velocity0 = (_position1 - _position0) / _duration;
            _velocity1 = _velocity0;

            _setState1 = true;
            _acceleration0 = Vector2.Zero;
        }

        public void Ini(Vector2 p_position0, Vector2 p_position1, float p_duration, float p_delay = 0.0f)
        {

            base.Ini(p_duration, p_delay);
            _position0 = p_position0;
            _position2 = p_position1;

            _velocity0 = (_position2 - _position0) / _duration;
            _velocity1 = _velocity0;

            _setState1 = true;

            _acceleration0 = Vector2.Zero;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4SpriteRotationModifier))]
	public class Pax4SpriteRotationModifier : Pax4ModifierSprite
    {
        [IgnoreDataMember]
        private float _rotationZ0;

        [IgnoreDataMember]
        private float _rotationZ1;

        [IgnoreDataMember]
        private float _rotationZ;

        [IgnoreDataMember]
        private float _rotationZVelocity;

        public Pax4SpriteRotationModifier(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (_done)
                return;

            base.Update(gameTime);

            if (_timer > _duration)
                return;

            _rotationZ = _rotationZ0 + _rotationZVelocity * _dt;

            Pax4Sprite sprite = null;

            foreach (KeyValuePair<String, PaxState> kvp in GetChild())
            {
                if (kvp.Value is Pax4Sprite)
                {
                    sprite = (Pax4Sprite)kvp.Value;                    
                    sprite._rotationZ0 = _rotationZ;
                }
            }

            if (_done)
            {
                if (!_continuous)
                {
                    _rotationZ = _rotationZ1;

                    if (_oscillating)
                    {
                        Ini(_rotationZ1, _rotationZ0, _duration);
                        Trigger();
                    }
                }
                else
                {
                    _done = false;
                }
            }
        }

        public override bool Trigger()
        {
            if (GetChild() != null && base.Trigger())
            {
                Pax4Sprite sprite = null;
                foreach (KeyValuePair<String, PaxState> kvp in GetChild())
                {
                    if (kvp.Value is Pax4Sprite)
                    {
                        sprite = (Pax4Sprite)kvp.Value;                        
                        sprite._rotationZ0 = _rotationZ0;
                    }
                }

                return true;
            }

            return false;
        }

        public void Ini(float p_rotationZ0, float p_rotationZ1, float p_duration, float p_delay = 0.0f)
        {
            base.Ini(p_duration, p_delay);

            _rotationZ0 = p_rotationZ0;
            _rotationZ1 = p_rotationZ1;

            _rotationZVelocity = (_rotationZ1 - _rotationZ0) / _duration;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4SpriteScaleModifier))]
    public class Pax4SpriteScaleModifier : Pax4ModifierSprite
    {
        [IgnoreDataMember]
        private float _scale0;

        [IgnoreDataMember]
        private float _scale1;

        [IgnoreDataMember]
        private float _scale;

        [IgnoreDataMember]
        private float _scaleVelocity;

        public Pax4SpriteScaleModifier(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (_done)
                return;

            base.Update(gameTime);

            if (_timer > _duration)
                return;

            _scale = _scale0 + _scaleVelocity * _dt;

            Pax4Sprite sprite = null;

            foreach (KeyValuePair<String, PaxState> kvp in GetChild())
            {
                if (kvp.Value is Pax4Sprite)
                {
                    sprite = (Pax4Sprite)kvp.Value;
                    sprite.SetScale(_scale);
                }
            }

            if (_done)
            {
                if (!_continuous)
                {
                    _scale = _scale1;

                    if (_oscillating)
                    {
                        Ini(_scale1, _scale0, _duration);
                        Trigger();
                    }
                }
                else
                {
                    _done = false;
                }
            }
        }

        public override bool Trigger()
        {
            if (GetChild() != null && base.Trigger())
            {
                Pax4Sprite sprite = null;
                foreach (KeyValuePair<String, PaxState> kvp in GetChild())
                {
                    if (kvp.Value is Pax4Sprite)
                    {
                        sprite = (Pax4Sprite)kvp.Value;                        
                        sprite.SetScale(_scale0);
                    }
                }

                return true;
            }

            return false;
        }

        public void Ini(float p_scale0, float p_scale1, float p_duration, float p_delay = 0.0f)
        {
            base.Ini(p_duration, p_delay);

            _scale0 = p_scale0;
            _scale1 = p_scale1;

            _scaleVelocity = (_scale1 - _scale0) / _duration;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4SpriteTextModifier))]
    public class Pax4SpriteTextModifier : Pax4ModifierSprite
    {
        [IgnoreDataMember]
        private String _text = null;

        public Pax4SpriteTextModifier(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (_done)
                return;

            base.Update(gameTime);

            if (_timer > _duration)
                return;

            if (_done)
                ((Pax4SpriteText)_currentSprite).SetText(_text);                
        }

        public override bool Trigger()
        {
            if (base.Trigger())
            {
                ((Pax4SpriteText)_currentSprite).SetText(_text);
                return true;
            }

            return false;
        }

        public void Ini(String p_text, float p_duration, float p_delay = 0.0f)
        {
            base.Ini(p_duration, p_delay);
            _text = p_text;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4SpriteTextNumberModifier))]
    public class Pax4SpriteTextNumberModifier : Pax4ModifierSprite
    {
        [IgnoreDataMember]
        private float _number0 = 0.0f;

        [IgnoreDataMember]
        private float _number1 = 0.0f;

        [IgnoreDataMember]
        private float _numberVelocity = 0.0f;

        [IgnoreDataMember]
        private bool _commaFormat = true;

        public Pax4SpriteTextNumberModifier(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (_done)
                return;

            base.Update(gameTime);

            if (_timer > _duration)
                return;

            float number = _number0 + _numberVelocity * _dt;

            if (_done)
            {
                number = _number1;
                
                if (_oscillating)
                {
                    Ini(_number1, _number0, _commaFormat, _duration);
                    Trigger();
                }
            }

            if (_commaFormat)
                ((Pax4SpriteText)_currentSprite).SetText(String.Format("{0:n0}", (int)number));
            else
                ((Pax4SpriteText)_currentSprite).SetText(((int)number).ToString());
        }

        public override bool Trigger()
        {
            if (base.Trigger())
            {
                if (_commaFormat)
                    ((Pax4SpriteText)_currentSprite).SetText(String.Format("{0:n0}", (int)_number0));
                else
                    ((Pax4SpriteText)_currentSprite).SetText(((int)_number0).ToString());

                return true;
            }

            return false;
        }

        public void Ini(float p_number0, float p_number1, bool p_commaFormat, float p_duration, float p_delay = 0.0f)
        {
            base.Ini(p_duration, p_delay);

            _number0 = p_number0;
            _number1 = p_number1;
            _commaFormat = p_commaFormat;

            _numberVelocity = (p_number1 - _number0) / _duration;
        }

        //public String CommaFormat(float p_number)
        //{
        //    return String.Format("{0:n}", (int)p_number);
        //    //string.Format("{0:n0}", 9876); // no decimals.
        //}

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}