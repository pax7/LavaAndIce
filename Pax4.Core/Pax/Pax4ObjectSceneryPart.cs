using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Pax.Core;
using Pax4.Jitter.LinearMath;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ObjectSceneryPart))]
    public class Pax4ObjectSceneryPart : Pax4Object
    {
        private JVector _scale = JVector.One;
        private JVector _rotation = JVector.Zero;
        private JVector _position = JVector.Zero;

        public Matrix _matWorld = Matrix.Identity;

        public BoundingSphere _boundingSphere;//this is for drawing purposes only

        public bool _isOutsideView = true;

        public Pax4ObjectSceneryPart(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_dxRequested)
                return;

            _isOutsideView = Pax4Camera._current._boundingFrustum.Contains(_boundingSphere) == ContainmentType.Disjoint;
        }

        public virtual JVector GetScale()
        {
            return _scale;
        }

        public virtual JVector GetRotation()
        {
            return _rotation;
        }

        public virtual JVector GetPosition()
        {
            return _position;
        }

        public virtual Matrix GetWorld()
        {
            return _matWorld;
        }

        public virtual void SetScale(JVector p_scale, bool p_transform = true)
        {
            _scale = p_scale;

            _boundingSphere.Radius = p_scale.Length() / 2.9f;

            if (p_transform)
                _matWorld =   Matrix.CreateScale(p_scale.X, p_scale.Y, p_scale.Z) 
                            * Matrix.CreateFromYawPitchRoll(_rotation.Y, _rotation.X, _rotation.Z) 
                            * Matrix.CreateTranslation(_position.X, _position.Y, _position.Z);
        }

        public virtual void SetRotation(JVector p_rotation, bool p_transform = true)
        {
            _rotation = p_rotation;

            if (p_transform)
                _matWorld =   Matrix.CreateScale(_scale.X, _scale.Y, _scale.Z)
                            * Matrix.CreateFromYawPitchRoll(_rotation.Y, _rotation.X, _rotation.Z)
                            * Matrix.CreateTranslation(_position.X, _position.Y, _position.Z);
        }

        public virtual void SetPosition(JVector p_position, bool p_transform = true)
        {
            _position = p_position;

            _boundingSphere.Center.X = p_position.X;
            _boundingSphere.Center.Y = p_position.Y;
            _boundingSphere.Center.Z = p_position.Z;

            if (p_transform)
                _matWorld =   Matrix.CreateScale(_scale.X, _scale.Y, _scale.Z) 
                            * Matrix.CreateFromYawPitchRoll(_rotation.Y, _rotation.X, _rotation.Z) 
                            * Matrix.CreateTranslation(_position.X, _position.Y, _position.Z);
        }

        public virtual void SetScaleRotationPosition(JVector p_scale, JVector p_rotation, JVector p_position, bool p_transform = true)
        {
            _scale = p_scale;
            _rotation = p_rotation;
            _position = p_position;

            _boundingSphere.Radius = p_scale.Length() / 2.9f;
            
            _boundingSphere.Center.X = p_position.X;
            _boundingSphere.Center.Y = p_position.Y;
            _boundingSphere.Center.Z = p_position.Z;

            if (p_transform)
                _matWorld =   Matrix.CreateScale(_scale.X, _scale.Y, _scale.Z)
                            * Matrix.CreateFromYawPitchRoll(_rotation.Y, _rotation.X, _rotation.Z)
                            * Matrix.CreateTranslation(_position.X, _position.Y, _position.Z);
        }

        public override void Enable()
        {
            if (!_isDisabled)
                return;

            base.Enable();

            Pax4World._current.AddUpdateObject(this);
            Pax4World._current.AddDrawObject(this);
        }

        public override void Disable()
        {
            if (_isDisabled)
                return;

            base.Disable();

            Pax4World._current.RemoveUpdateObject(this);
            Pax4World._current.RemoveDrawObject(this);
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}