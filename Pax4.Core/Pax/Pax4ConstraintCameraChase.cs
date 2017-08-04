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
    [KnownType(typeof(Pax4ConstraintCameraChase))]
    public class Pax4ConstraintCameraChase : Constraint
    {
        public Pax4ObjectPhysicsPart _physicsPart = null;

        private Vector3 _cameraPosition = Vector3.Zero;

        public Pax4ConstraintCameraChase(Pax4ObjectPhysicsPart p_physicsPart, Vector3 p_cameraPosition)
            : base(p_physicsPart._body, null)
        {
            _cameraPosition = p_cameraPosition;

            SetPhysicsPart(p_physicsPart);
        }

        public override void UpdateController(float dt)
        {
            JVector cameraPosition = _physicsPart._body.Position;
            cameraPosition.X += _cameraPosition.X;
            cameraPosition.Y += _cameraPosition.Y;
            cameraPosition.Z += _cameraPosition.Z; //chase
            
            //cameraPosition.Z -= 10.0f; //chase front
            
            Pax4Camera._current.SetPosition(cameraPosition);
            Pax4Camera._current.SetTarget(_physicsPart._body.Position);
        }

        public void SetPhysicsPart(Pax4ObjectPhysicsPart p_physicsPart)
        {
            if (p_physicsPart == null)
                return;

            _physicsPart = p_physicsPart;

            _physicsPart.AddConstraint(this);
        }
    }
}