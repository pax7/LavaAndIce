using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Pax4.Jitter.Dynamics.Constraints;

namespace Pax4.Core
{
    public class Pax4ConstraintWayPoint : Constraint
    {
        public Pax4ObjectPhysicsPart _physicsPart = null;

        public Pax4WayPointPath _wayPointPath = null;
        public float _velocityFactor = 0.0f;
        public int _wayPointIndex = 0;
        public static float _wayPointDistanceThreshold = 0.3f;
        public Vector3 _worldHeading = Vector3.Zero;
        public Vector3 _worldForce = Vector3.Zero;
        public Vector3 _worldVelocity = Vector3.Zero;

        public float _worldForceFactor = 1.0f;
        public float _currentDistance = 0.0f;

        public Pax4ConstraintWayPoint(Pax4ObjectPhysicsPart p_physicsPart, float p_velocityFactor, Pax4WayPointPath p_wayPointPath = null, int p_wayPointIndex = 0)
            : base()
        {   
            _velocityFactor = p_velocityFactor;
            _wayPointPath = p_wayPointPath;
            _wayPointIndex = p_wayPointIndex;

            SetPhysicsPart(p_physicsPart, p_wayPointIndex);
        }

        public override void UpdateController(float dt)
        {
            if (dt <= 0.01f)
                dt = 0.01f;

            _worldHeading = _wayPointPath._wayPoint[_wayPointIndex] - _physicsPart._body.Position;
            _currentDistance = _worldHeading.Length();

            if (_currentDistance <= _wayPointDistanceThreshold)
            {
                if (_wayPointPath._wayPoint.Length > 1)
                {
                    _wayPointIndex++;
                    if (_wayPointIndex == _wayPointPath._wayPoint.Length)
                        _wayPointIndex = 0;

                    _worldHeading = _wayPointPath._wayPoint[_wayPointIndex] - _physicsPart._body.Position;
                    _currentDistance = _worldHeading.Length();
                }

                if (_currentDistance == 0.0f)
                    return;
            }

            _worldHeading.Normalize();

            if (_wayPointPath._wayPoint.Length == 1)
            {
                if (_currentDistance <= _wayPointDistanceThreshold)
                    _worldVelocity = _currentDistance * _velocityFactor * _worldHeading;
                else
                    _worldVelocity = _velocityFactor * _worldHeading;
            }
            else
            {
                _worldVelocity = _velocityFactor * _worldHeading;
            }

            _worldForce = ((_worldVelocity - _physicsPart._body.Velocity) * _physicsPart._mass) / dt;
            _physicsPart._body.AddWorldForce(_worldForce);

            if (!_physicsPart._body.IsActive)
                _physicsPart._body.SetActive();
        }

        public override void EnableController()
        {
            if (_wayPointPath != null)
                _wayPointPath._residentCount++;

            base.EnableController();
        }
       
        public override void DisableController()
        {
            if(_wayPointPath != null)
                _wayPointPath._residentCount--;
            
            base.DisableController();
        }

        public void SetPhysicsPart(Pax4ObjectPhysicsPart p_physicsPart, int p_wayPointIndex = 0)
        {
            if (p_physicsPart == null)
                return;

            p_physicsPart.DisableConstraint();

            _physicsPart = p_physicsPart;

            _physicsPart.AddConstraint(this);

            if (_wayPointPath == null)
                return;

            _wayPointIndex = p_wayPointIndex;
            _worldHeading = _wayPointPath._wayPoint[_wayPointIndex] - _physicsPart._body.Position;
            _currentDistance = _worldHeading.Length();
        }
    }
}