using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Xna.Framework;
using Pax.Core;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ModifierCamera))]
    public class Pax4ModifierCamera : Pax4Modifier
    {
        [DataMember]
        public bool _hasTarget0;

        public Pax4ModifierCamera(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            _hasTarget0 = Pax4Camera._current._hasTarget;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4CameraPositionModifier))]
    public class Pax4CameraPositionModifier : Pax4ModifierCamera
    {
        [IgnoreDataMember]
        private Vector3 _position0;

        [IgnoreDataMember]
        private Vector3 _position1;

        [IgnoreDataMember]
        private Vector3 _position2;

        [IgnoreDataMember]
        private Vector3 _position;

        [IgnoreDataMember]
        private Vector3 _breakPositionTopLeft;

        [IgnoreDataMember]
        private Vector3 _breakPositionBottomRight;

        [IgnoreDataMember]
        private Vector3 _breakVelocityThreshold;

        [IgnoreDataMember]
        private Vector3 _velocity0;

        [IgnoreDataMember]
        private Vector3 _velocity1;

        [IgnoreDataMember]
        public Vector3 _velocity;

        [IgnoreDataMember]
        private Vector3 _acceleration0;

        [IgnoreDataMember]
        private bool _break = false;

        public Pax4CameraPositionModifier(String p_name, PaxState p_parent0)
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

            if (_setState1 && _done)
            {
                Pax4Camera._current._position = _position1;

                if (_oscillating)
                {
                    Ini(_position1, _position0, _duration);
                    Trigger();
                }

                return;
            }

            _velocity = _velocity0 + _acceleration0 * _dt;
            _position = _position0 + _velocity * _dt;

            if (_break)
            {
                _done = true;

                if (Math.Abs(_velocity.X) <= _breakVelocityThreshold.X)
                    _velocity.X = 0.0f;
                else
                    _done = false;

                if (Math.Abs(_velocity.Y) <= _breakVelocityThreshold.Y)
                    _velocity.Y = 0.0f;
                else
                    _done = false;

                if (_done)
                    _break = false;
                else
                {
                    _done = true;

                    _position2 = _position;

                    if (_position.X <= _breakPositionTopLeft.X)
                        _position.X = _breakPositionTopLeft.X;
                    else if (_position.X >= _breakPositionBottomRight.X)
                        _position.X = _breakPositionBottomRight.X;
                    else
                        _done = false;

                    if (_position.Y <= _breakPositionTopLeft.Y)
                        _position.Y = _breakPositionTopLeft.Y;
                    else if (_position.Y >= _breakPositionBottomRight.Y)
                        _position.Y = _breakPositionBottomRight.Y;
                    else
                        _done = false;

                    if (_done)
                    {
                        _break = false;
                        Ini(_position2, _position, 0.3f);
                        Trigger();
                        return;
                    }
                }
            }

            Pax4Camera._current._position = _position;
        }

        public override bool Trigger()
        {
            if (base.Trigger())
            {
                Pax4Camera._current._position = _position0;
                Pax4Camera._current._updateView = true;
                Pax4Camera._current._hasTarget = true;

                return true;
            }

            return false;
        }

        public void IniVelocity0Acceleration(Vector3 p_velocity0, float p_accelerationFactor, Vector3 p_breakPositionTopLeft, Vector3 p_breakPositionBottomRight, bool p_break = false)
        {
            _position0 = Pax4Camera._current._position;
            _velocity0 = p_velocity0;

            if (p_break)
            {
                _break = p_break;

                _acceleration0 = -p_accelerationFactor * _velocity0;
                _velocity1 = Vector3.Zero;

                _breakVelocityThreshold = p_velocity0 / 2.0f;
                _breakVelocityThreshold.X = Math.Abs(_breakVelocityThreshold.X);
                _breakVelocityThreshold.Y = Math.Abs(_breakVelocityThreshold.Y);

                _breakPositionTopLeft = p_breakPositionTopLeft;
                _breakPositionBottomRight = p_breakPositionBottomRight;
            }
            else
            {
                _acceleration0 = p_accelerationFactor * _velocity0;
                _velocity1 = _velocity0;
            }

            _duration = 999.0f;

            _setState1 = false;

        }

        public void IniPosition1(Vector3 p_position1)
        {
            _position0 = Pax4Camera._current._position;
            _position1 = p_position1;

            _velocity0 = (_position1 - _position0) / _duration;
            _velocity1 = _velocity0;

            _setState1 = true;

            _acceleration0 = Vector3.Zero;
        }

        public void Ini(Vector3 p_position0, Vector3 p_position1, float p_duration, float p_delay = 0.0f)
        {
            base.Ini(p_duration, p_delay);

            _position0 = p_position0;
            _position1 = p_position1;

            _velocity0 = (_position1 - _position0) / _duration;
            _velocity1 = _velocity0;

            _setState1 = true;

            _acceleration0 = Vector3.Zero;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4CameraTargetModifier))]
    public class Pax4CameraTargetModifier : Pax4ModifierCamera
    {
        [IgnoreDataMember]
        private Vector3 _target0;

        [IgnoreDataMember]
        private Vector3 _target1;

        [IgnoreDataMember]
        private Vector3 _target;

        [IgnoreDataMember]
        private Vector3 _velocity0;

        [IgnoreDataMember]
        private Vector3 _velocity1;

        [IgnoreDataMember]
        public Vector3 _velocity;

        public Pax4CameraTargetModifier(String p_name, PaxState p_parent0)
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

            if (_setState1 && _done)
            {
                Pax4Camera._current._target = _target1;
                Pax4Camera._current._updateView = true;

                if (_oscillating)
                {
                    Ini(_target1, _target0, _duration);
                    Trigger();

                    if (_roundTrip)
                    {
                        _roundTrip = false;
                        _oscillating = false;
                    }
                }

                return;
            }

            _target = _target0 + _velocity0 * _dt;

            Pax4Camera._current._target = _target;
        }

        public override bool Trigger()
        {
            if (base.Trigger())
            {
                Pax4Camera._current._target = _target0;
                Pax4Camera._current._updateView = true;
                Pax4Camera._current._hasTarget = true;

                return true;
            }

            return false;
        }

        public void IniTarget1(Vector3 p_target1)
        {
            _target0 = Pax4Camera._current._target;
            _target1 = p_target1;

            _velocity0 = (_target1 - _target0) / _duration;
            _velocity1 = _velocity0;

            _setState1 = true;
        }

        public void IniTarget1(List<Vector3> p_target1)
        {
        }

        public void Ini(Vector3 p_target0, Vector3 p_target1, float p_duration, float p_delay = 0.0f)
        {
            base.Ini(p_duration, p_delay);

            _target0 = p_target0;
            _target1 = p_target1;

            _velocity0 = (_target1 - _target0) / _duration;
            _velocity1 = _velocity0;

            _setState1 = true;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}