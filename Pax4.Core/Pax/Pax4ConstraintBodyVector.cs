using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Xna.Framework;
using Pax4.Jitter.Dynamics.Constraints;
using Pax4.Jitter.LinearMath;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ConstraintBodyVector))]
    public class Pax4ConstraintBodyVector : Constraint
    {
        public Pax4ObjectPhysicsPart _physicsPart = null;

        public float _velocityFactor = 0.0f;
        
        public float _bodyForce = 0.0f;
        public float _bodyVelocity = 0.0f;

        public JVector _bodyVector = JVector.Zero;

        public float _worldForceFactor = 1.0f;
        public float _currentDistance = 0.0f;

        public Pax4ConstraintBodyVector(Pax4ObjectPhysicsPart p_physicsPart, float p_velocityFactor, Vector3 p_bodyVector)
            : base(p_physicsPart._body, null)
        {   
            _velocityFactor = p_velocityFactor;

            _bodyVector = Pax4Tools.ToJVector(p_bodyVector);
            _bodyVector.Normalize();

            SetPhysicsPart(p_physicsPart);
        }

        public override void UpdateController(float dt)
        {
            if (dt <= 0.01f)
                dt = 0.01f;

            if (_physicsPart._addBodyForce)
            {
                _bodyForce = ((_velocityFactor - _physicsPart._body.LinearVelocity.Length()) * _physicsPart._mass) / dt;

                _physicsPart._body.AddForce(_bodyForce * _bodyVector);
            }
        }

        public void SetPhysicsPart(Pax4ObjectPhysicsPart p_physicsPart)
        {
            if (p_physicsPart == null)
                return;

            p_physicsPart.DisableConstraint();

            _physicsPart = p_physicsPart;

            _physicsPart.AddConstraint(this);
        }

        public void SetVelocityFactor(float p_velocityFactor)
        {
            _velocityFactor = p_velocityFactor;
        }
    }
}