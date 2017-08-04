using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Pax4.Core
{
    public class Pax4ConstraintWayPointAirplane : Pax4ConstraintWayPoint
    {
        public float _worldDistance = 0.0f;

        public Vector3 _worldAngularVelocityFactor = Vector3.Zero;
        public Vector3 _worldAngularVelocity = Vector3.Zero;

        public Vector3 _worldTorque = Vector3.Zero;

        public Pax4ConstraintWayPointAirplane(Pax4ObjectPhysicsPart p_physicsPart, float p_velocityFactor, Vector3 p_angularVelocityFactor, Pax4WayPointPath p_wayPointPath = null, int p_wayPointIndex = 0)
            : base(p_physicsPart, p_velocityFactor, p_wayPointPath, p_wayPointIndex)
        {
            _worldAngularVelocityFactor = p_angularVelocityFactor;
        }

        public override void UpdateController(float dt)
        {
            if (dt <= 0.1f)
                dt = 0.1f;

            _worldHeading = _wayPointPath._wayPoint[_wayPointIndex] - _physicsPart._body.Position;
            _currentDistance = _worldHeading.Length();

            if (_worldHeading != Vector3.Zero)
                _worldHeading.Normalize();

            float cosTheta = Vector3.Dot(_physicsPart._body.Orientation.Backward, _worldHeading);

            _worldTorque = Vector3.Cross(_physicsPart._body.Orientation.Backward, _worldHeading);

            if (cosTheta < -1 + 0.01f)
            {
                _worldTorque = Vector3.Cross(Vector3.UnitZ, _physicsPart._body.Orientation.Backward);
                if (_worldTorque.Length() < 0.01f)
                {
                    _worldTorque = Vector3.Cross(Vector3.UnitX, _physicsPart._body.Orientation.Backward);
                }
                _worldTorque.Normalize();
            }

            _worldTorque.X = Pax4Tools.FooAggregate(_worldTorque.X, _physicsPart._body.TransformRate.AngularVelocity.X);
            _worldTorque.Y = Pax4Tools.FooAggregate(_worldTorque.Y, _physicsPart._body.TransformRate.AngularVelocity.Y);
            _worldTorque.Z = Pax4Tools.FooAggregate(_worldTorque.Z, _physicsPart._body.TransformRate.AngularVelocity.Z);

            _worldTorque = Vector3.Multiply(_worldTorque, cosTheta / dt);
            _worldTorque = Vector3.Transform(_worldTorque, _physicsPart._body.BodyInertia);

            _physicsPart._body.AddWorldTorque(_worldTorque);

            if (_currentDistance <= _wayPointDistanceThreshold)
            {
                if (_wayPointPath._wayPoint.Length > 1)
                {
                    _wayPointIndex++;
                    if (_wayPointIndex == _wayPointPath._wayPoint.Length)
                        _wayPointIndex = 0;

                    _worldHeading = _wayPointPath._wayPoint[_wayPointIndex] - _physicsPart._body.Position;
                    _currentDistance = _worldHeading.Length();
                    _worldDistance = _currentDistance;
                }

                if (_currentDistance == 0.0f)
                    return;
            }

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
    }
}