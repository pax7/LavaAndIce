using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using CpuSkinningDataTypes;
using Pax.Core;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ModelState))]
    public class Pax4ModelState : PaxState
    {
        [IgnoreDataMember]
        public Model _model = null;

        [IgnoreDataMember]
        public Matrix _matTransform = Matrix.Identity;

        [IgnoreDataMember]        
        public Effect _effect = null;

        public Pax4ModelState(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public void SetEffect(Effect p_effect = null)
        {
            if (p_effect == null)
                p_effect = _effect;

            foreach (ModelMesh mesh in _model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = p_effect;
                }
            }
        }
    }

    [DataContract]
    [KnownType(typeof(Pax4Model))]
    public class Pax4Model : PaxState
    {
        [IgnoreDataMember]
        public static Pax4Model _current = null;
        
        public Pax4Model(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            _current = this;
        }

        public void Load(String p_model)
        {
            if (p_model == null)
                return;

            if (GetChild().ContainsKey(p_model))
                return;

            Pax4ModelState modelState = new Pax4ModelState(p_model, this);
            modelState._model = Pax4Game._current.Content.Load<Model>(p_model);

            Matrix[] matTemp = new Matrix[modelState._model.Bones.Count];
            modelState._model.CopyAbsoluteBoneTransformsTo(matTemp);
            modelState._matTransform = matTemp[0];
        }

        public void Load(List<String> p_model)
        {
            if (p_model == null)
                return;

            Pax4ModelState modelState = null;
            Matrix[] matTemp = null;
            String model = null;

            for (int i = 0; i < p_model.Count; i++)
            {
                model = p_model[i];
                if (GetChild().ContainsKey(model))
                    continue;

                modelState = new Pax4ModelState(model, this);
                modelState._model = Pax4Game._current.Content.Load<Model>(model);

                matTemp = new Matrix[modelState._model.Bones.Count];
                modelState._model.CopyAbsoluteBoneTransformsTo(matTemp);
                modelState._matTransform = matTemp[0];
            }
        }

        public override void Dx()
        {
            if (this == _current)
                _current = null;

            base.Dx();
        }

        public void SetDefaultParameters()
        {
            foreach (Pax4ModelState modelState in GetChild().Values)
            {
                foreach (ModelMesh mesh in modelState._model.Meshes)
                {
                    foreach (Effect effect in mesh.Effects)
                    {
                        if (effect is BasicEffect)
                        {
                            ((BasicEffect)effect).EnableDefaultLighting();
                            ((BasicEffect)effect).PreferPerPixelLighting = true;
                            ((BasicEffect)effect).Projection = Pax4Camera._current._matProjection;
                            //((BasicEffect)effect).SpecularColor = Vector3.Zero;
                            //((BasicEffect)effect).SpecularPower = 0.0f;
                            //((BasicEffect)effect).DirectionalLight0.Direction = Vector3.Forward;
                            //((BasicEffect)effect).DirectionalLight0.Enabled = true;
                            //((BasicEffect)effect).DirectionalLight1.Enabled = false;
                            //((BasicEffect)effect).DirectionalLight2.Enabled = false;
                        }
                        else if (effect is SkinnedEffect)
                        {
                            ((SkinnedEffect)effect).EnableDefaultLighting();
                            ((SkinnedEffect)effect).PreferPerPixelLighting = true;
                            ((SkinnedEffect)effect).Projection = Pax4Camera._current._matProjection;
                        }                        
                    }
                }
            }
        }
    }
}