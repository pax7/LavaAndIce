using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ObjectPhysicsPartModel))]
    public class Pax4ObjectPhysicsPartModel : Pax4ObjectPhysicsPart
    {
        public Pax4ModelState _modelState = null;

        public Pax4ObjectPhysicsPartModel(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {
            _boundingSphere.Radius = 1.0f;
        }

        public override void Draw(GameTime gameTime)
        {
            if (_isOutsideView)
                return;

            if (_modelState != null)
            {
                foreach (ModelMesh mesh in _modelState._model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = _matWorld;
                        effect.View = Pax4Camera._current._matView;
                    }

                    mesh.Draw();
                }
            }

            base.Draw(gameTime);
        }

        public override void SetScale(Vector3 p_scale, bool p_transform = false)
        {
            base.SetScale(p_scale, p_transform);
            if(_modelState != null)
                _matScale = _modelState._matTransform * _matScale;
        }

        public void SetModel(String p_modelName = "")
        {
            _modelState = (Pax4ModelState)Pax4Model._current.GetChild(p_modelName);
            _matScale = _modelState._matTransform * _matScale;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}