using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ObjectSceneryPartModel))]
    public class Pax4ObjectSceneryPartModel : Pax4ObjectSceneryPart
    {
        public Pax4ModelState _modelState = null;

        public Pax4ObjectSceneryPartModel(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {
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
        }

        public virtual void SetModel(String p_modelName = "")
        {
            _modelState = (Pax4ModelState)Pax4Model._current.GetChild(p_modelName);
            _matWorld = _modelState._matTransform * _matWorld;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}