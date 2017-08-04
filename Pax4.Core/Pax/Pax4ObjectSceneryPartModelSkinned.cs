using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using CpuSkinningDataTypes;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ObjectSceneryPartModelSkinned))]
    public class Pax4ObjectSceneryPartModelSkinned : Pax4ObjectSceneryPartModel
    {
        public AnimationPlayer _currentAnimationPlayer = null;
        
        public AnimationClip _currentAnimationClip = null;

        public List<AnimationClip> _animationClip = new List<AnimationClip>();

        public Pax4ObjectSceneryPartModelSkinned(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_currentAnimationPlayer != null)
                _currentAnimationPlayer.Update(gameTime.ElapsedGameTime, Matrix.Identity);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_isOutsideView)
                return;

            if (_currentAnimationPlayer == null)
                return;

            foreach (ModelMesh mesh in _modelState._model.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.SetBoneTransforms(_currentAnimationPlayer.SkinTransforms);
                    effect.World = _matWorld;
                    effect.View = Pax4Camera._current._matView;
                }

                mesh.Draw();
            }            
        }

        public override void SetModel(String p_modelName = "")
        {
            base.SetModel(p_modelName);
            
            SkinningData skinningData = (SkinningData)_modelState._model.Tag;            
            _currentAnimationPlayer = new AnimationPlayer(skinningData);

            foreach (KeyValuePair<String, AnimationClip> kvp in skinningData.AnimationClips)
                _animationClip.Add(kvp.Value);
            
            _currentAnimationClip = _animationClip[0];
            _currentAnimationClip.Ini(skinningData);            
        }

        public override void Enable()
        {
            base.Enable();

            if (_currentAnimationPlayer == null)
                return;

            _currentAnimationPlayer.EnterClip(ref _currentAnimationClip);
        }

        public virtual void SetAnimationtVelocityFactor(float p_animationVelocityFactor = 1.0f)
        {
            if (_currentAnimationPlayer == null)
                return;

            _currentAnimationClip.SetVelocityFactor(p_animationVelocityFactor);
        }

        public virtual void EnterClip(AnimationClip p_clip, float p_blendDuration = 0.3f)
        {
            if (_currentAnimationPlayer == null)
                return;

            //_currentAnimationPlayer.EnterClip();
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}