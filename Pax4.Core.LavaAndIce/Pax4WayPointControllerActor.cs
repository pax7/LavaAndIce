using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Pax4.JigLibX.Physics;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4WayPointControllerActor))]
    public class Pax4WayPointControllerActor : Pax4ConstraintWayPoint
    {
        public static Vector3 _minAngularVelocity = new Vector3(0.0f, 1.5f, 0.5f);

        public Pax4WayPointControllerActor(Pax4ObjectPhysicsPart p_physicsPart, float p_velocityFactor, Pax4WayPointPath p_wayPointPath = null, int p_wayPointIndex = 0)
            : base(p_physicsPart, p_velocityFactor, p_wayPointPath, p_wayPointIndex)            
        {
        }

        public override void UpdateController(float dt)
        {
            base.UpdateController(dt);

            if (_physicsPart._body.AngularVelocity.X <= _minAngularVelocity.X
                && _physicsPart._body.AngularVelocity.Y <= _minAngularVelocity.Y
                && _physicsPart._body.AngularVelocity.Z <= _minAngularVelocity.Z)
            {
                _physicsPart._body.AngularVelocity = _minAngularVelocity;
            }
        }
    }
}