using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ObjectPhysicsPartSphere))]
    public class Pax4ObjectPhysicsPartSphere : Pax4ObjectPhysicsPartModel
    {
        public Pax4ObjectPhysicsPartSphere(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public override void SetScale(Vector3 p_scale, bool p_transform = false)
        {
            base.SetScale(p_scale, p_transform);
            _collisionSkin.RemoveAllPrimitives();
            CreateSpherePrimitive(_body.Position, p_scale.X / 2.0f, 1.0f);
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}