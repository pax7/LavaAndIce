using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pax4.JigLibX.Physics;
using Pax4.JigLibX.Collision;
using Pax4.JigLibX.Geometry;
using Pax4.JigLibX.Math;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ActorWorld))]
    public class Pax4ActorWorld : Pax4Actor
    {
        public static Pax4ActorWorld _current = null;

        public static float _fudgeFactor = 0.0f;
        public static float _scaleFactor = 0.0f;
        public static Vector3 _worldPosition = Vector3.Zero;

        public Pax4ActorWorld(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {
            float scaleFactor = 11.5f / Pax4Camera._current._scale.Z;
            SetScale(Vector3.One * scaleFactor);
            _worldPosition = Vector3.Backward * -10.0f;        

            _current = null;
            _actorType = EActorType._WORLD;

            _body.Immovable = true;

            if (Pax4ActorPlayerAmmoLava._scaleFactor > Pax4ActorPlayerAmmoIce._scaleFactor)
                _fudgeFactor = Pax4ActorPlayerAmmoLava._scaleFactor / 1.5f;
            else
                _fudgeFactor = Pax4ActorPlayerAmmoIce._scaleFactor / 1.5f;

            _collisionSkin.RemoveAllPrimitives();

            CreatePlanePrimitive(Vector3.Backward, Vector3.Forward * _fudgeFactor, 1.0f, 0.0f, 0.0f);
            CreatePlanePrimitive(Vector3.Forward, Vector3.Backward * _fudgeFactor, 1.0f, 0.0f, 0.0f);

            _fudgeFactor = 36.0f;
            CreatePlanePrimitive(Vector3.Left, Vector3.Right * _fudgeFactor / (3.7f * Pax4Camera._current._scale.Z), 1.0f, 0.0f, 0.0f);
            CreatePlanePrimitive(Vector3.Right, Vector3.Left * _fudgeFactor / (3.7f * Pax4Camera._current._scale.Z), 1.0f, 0.0f, 0.0f);

            _fudgeFactor = 36.0f;
            CreatePlanePrimitive(Vector3.Down, Vector3.Up * _fudgeFactor / 2.2f, 0.4f, 0.5f, 0.5f);
            //CreatePlanePrimitive(Vector3.Up, Vector3.Down * _fudgeFactor / 1.2f, 0.4f, 0.5f, 0.5f);

            SetMass(_worldMass);
        }

        public override void Enable()
        {
            base.Enable();

            _matWorld *= Matrix.CreateTranslation(_worldPosition);
        }

        public override void Dx()
        {
            base.Dx();

            if (this == _current)
                _current = null;
        }
    }
}