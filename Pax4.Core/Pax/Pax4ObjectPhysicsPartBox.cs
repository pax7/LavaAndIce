using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ObjectPhysicsPartBox))]
    public class Pax4ObjectPhysicsPartBox : Pax4ObjectPhysicsPartModel
    {
        public Pax4ObjectPhysicsPartBox(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {               
        }

        public override void SetScale(Vector3 p_scale, bool p_transform = false)
        {
            base.SetScale(p_scale, p_transform);
            _collisionSkin.RemoveAllPrimitives();
            CreateBoxPrimitive(Matrix.Identity, _body.Position, p_scale);
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }    
}