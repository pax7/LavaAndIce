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
    [KnownType(typeof(Pax4ModifierWayPointPath))]
    public class Pax4ModifierWayPointPath : Pax4Modifier
    {
        [DataMember]
        public List<Pax4WayPointPath> _wayPointPath = null;

        public Pax4ModifierWayPointPath(String p_name, PaxState p_parent0,Pax4WayPointPath p_wayPointPath = null)
            : base(p_name, p_parent0)
        {
            AddPath(p_wayPointPath);
        }

        public void AddPath(Pax4WayPointPath p_wayPointPath)
        {
            if (p_wayPointPath == null)
                return;

            if (_wayPointPath == null)
                _wayPointPath = new List<Pax4WayPointPath>();

            _wayPointPath.Add(p_wayPointPath);
        }

        public void AddPath(Pax4WayPointPathList p_wayPointPaths)
        {
            if (p_wayPointPaths == null || p_wayPointPaths._wayPointPath == null)
                return;

            if (_wayPointPath == null)
                _wayPointPath = new List<Pax4WayPointPath>();

            for(int i = 0; i < p_wayPointPaths._wayPointPath.Count; i++)
                _wayPointPath.Add(p_wayPointPaths._wayPointPath[i]);
        }

        public void MergePath()
        {
            if(_wayPointPath == null)
                return;


            for (int i = 0; i < _wayPointPath.Count; i++)
                _wayPointPath[i].SetTransformIdentity();
        }

        public override void Enable()
        {
            if (!_isDisabled)
                return;

            if (Pax4World._current != null)
                Pax4World._current.AddWayPointPathModifier(this);

            base.Enable();
        }

        public override void Disable()
        {
            if (_isDisabled)
                return;

            if (Pax4World._current != null)
                Pax4World._current.AddWayPointPathModifier(this);

            base.Disable();
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4ModifierWayPointPathPosition))]
    public class Pax4ModifierWayPointPathPosition : Pax4ModifierWayPointPath
    {
        [IgnoreDataMember]
        private Vector3 _position0;

        [IgnoreDataMember]
        private Vector3 _position1;

        [IgnoreDataMember]
        private Vector3 _position;

        [IgnoreDataMember]
        private Vector3 _velocity;

        public Pax4ModifierWayPointPathPosition(String p_name, PaxState p_parent0, Pax4WayPointPath p_wayPointPath = null)
            : base(p_name, p_parent0, p_wayPointPath)
        {            
        }

        public override void Update(GameTime gameTime)
        {
            if (_done)
                return;

            base.Update(gameTime);

            if (_timer > _duration)
                return;

            _position = _position0 + _velocity * _dt;

            for (int i = 0; i < _wayPointPath.Count; i++)
                _wayPointPath[i].SetPosition(_position);

            if (_done)
            {
                if (!_continuous)
                {
                    _position = _position1;

                    if (_oscillating)
                    {
                        Ini(_position1, _position0, _duration);
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
            if (base.Trigger())
            {
                for (int i = 0; i < _wayPointPath.Count; i++)
                    _wayPointPath[i].SetPosition(_position0);

                return true;
            }

            return false;
        }

        public void Ini(Vector3 p_position0, Vector3 p_position1, float p_duration, float p_delay = 0.0f)
        {
            base.Ini(p_duration, p_delay);

            _position0 = p_position0;
            _position1 = p_position1;

            _velocity = (_position1 - _position0) / _duration;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4ModifierWayPointPathRotationZ))]
    public class Pax4ModifierWayPointPathRotationZ : Pax4ModifierWayPointPath
    {
        [IgnoreDataMember]
        private float _rotationZ0;

        [IgnoreDataMember]
        private float _rotationZ1;

        [IgnoreDataMember]
        private float _rotationZ;

        [IgnoreDataMember]
        private float _rotationZVelocity;

        public Pax4ModifierWayPointPathRotationZ(String p_name, PaxState p_parent0, Pax4WayPointPath p_wayPointPath = null)
            : base(p_name, p_parent0, p_wayPointPath)
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
            for (int i = 0; i < _wayPointPath.Count; i++)
                _wayPointPath[i].SetRotationZ(_rotationZ);

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
            if (_wayPointPath == null)
                return false;

            if (base.Trigger())
            {
               for (int i = 0; i < _wayPointPath.Count; i++)
                    _wayPointPath[i].SetRotationZ(_rotationZ0);

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
    [KnownType(typeof(Pax4ModifierWayPointPathScale))]
    public class Pax4ModifierWayPointPathScale : Pax4ModifierWayPointPath
    {
        [IgnoreDataMember]
        private float _scale0;

        [IgnoreDataMember]
        private float _scale1;

        [IgnoreDataMember]
        private float _scale;

        [IgnoreDataMember]
        private float _scaleVelocity;

        public Pax4ModifierWayPointPathScale(String p_name, PaxState p_parent0, Pax4WayPointPath p_wayPointPath = null)
            : base(p_name, p_parent0, p_wayPointPath)
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
            for (int i = 0; i < _wayPointPath.Count; i++)
                _wayPointPath[i].SetScale(_scale);

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
            if (base.Trigger())
            {
                for (int i = 0; i < _wayPointPath.Count; i++)
                    _wayPointPath[i].SetScale(_scale0);

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
}