using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pax4.ProjectMercury;
using Pax4.ProjectMercury.Emitters;
using Pax4.ProjectMercury.Modifiers;
using Pax4.ProjectMercury.Renderers;
using Pax4.ProjectMercury.Controllers;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ParticleEffect))]
    public class Pax4ParticleEffect
    {
        public static Pax4ParticleEffect _current = null;

        public SpriteBatchRenderer _particleRenderer = null;
        
        public List<ParticleEffect> _update = new List<ParticleEffect>();

        public List<ParticleEffect> _draw = new List<ParticleEffect>();
        public List<ParticleEffect> _drawAura = new List<ParticleEffect>();

        public QuadRenderer _particleRenderer3 = null;
        public List<ParticleEffect> _draw3 = new List<ParticleEffect>();
        public List<ParticleEffect> _drawAura3 = new List<ParticleEffect>();
        
        public bool _disabled = false;

        public ParticleEffect _particleEffectDefault = null;
        public ParticleEffect _particleEffectIceStarTrail = null;
        public ParticleEffect _particleEffectChainLinkBreak = null;

        public Pax4ParticleEffect()
        {
            _current = this;

            if (_particleRenderer == null)
            {
                _particleRenderer = new SpriteBatchRenderer { GraphicsDeviceService = Pax4Game._graphicsDeviceManager };
                _particleRenderer.LoadContent(Pax4Game._current.Content);
                
                _particleRenderer3 = new QuadRenderer(10000) { GraphicsDeviceService = Pax4Game._graphicsDeviceManager };
                _particleRenderer3.LoadContent(Pax4Game._current.Content);
            }

            ParticleEffect particleEffect = null;
            Texture2D particleTexture = null;
            AbstractEmitter emitter = null;

            float scale = 1.0f;

            if (Pax4Camera._backBufferWidth <= 320)
                scale = Pax4Camera._current._scale.X * 0.9f;
            else
                scale = Pax4Camera._current._scale.X;

            int budgetFactor = 3;

            #region Default
            //****************************
            //Default
            //****************************
            budgetFactor = 3;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/FlowerBurst");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/FlowerBurst");
            emitter = new PointEmitter
            {
                Budget = 64 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 5,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 50.0f), // { Value = 25f, Variation = 25f },
                ReleaseColour = new ColourRange
                {
                    Red = new Range(0.0f, 0.8f),
                    Green = new Range(0.0f, 1.0f),
                    Blue = new Range(0.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 48.0f * scale), // {  Value = 32f, Variation = 16f },
                //ReleaseRotation
                //ReleaseImpulse
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityFastFadeModifier 
                                    { 
                                        InitialOpacity = 1.0f 
                                    }
                                },

                BlendMode = EmitterBlendMode.Add,

                Controllers = new ControllerPipeline
                                {
                                    new TriggerOffsetController
                                    {
                                        TriggerOffset = Vector3.Zero
                                    },
                                    //new TriggerRotationController
                                    //{
                                    //    TriggerRotation = Vector3.Zero
                                    //}
                                },

                //TriggerOffset
                //MinimumTriggerPeriod
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            _particleEffectDefault = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //Default

            #region IceStarTrail
            //****************************
            //IceStarTrail
            //****************************
            budgetFactor = 1;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/Star");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Star");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 42 * budgetFactor,
                Term = 0.5f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(32.0f, 160.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 16.0f * scale),
                ReleaseRotation = new RotationRange
                {
                    Yaw = new Range(0.0f, 0.0f),
                    Pitch = new Range(0.0f, 0.0f),
                    Roll = new Range(0.0f, 6.28f),
                },
                //ReleaseImpulse
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator3 
                                    { 
                                        InitialOpacity = 0.0f,
                                        MedianOpacity = 1.0f,
                                        Median = 0.75f,
                                        FinalOpacity = 0.0f
                                    },
                                    new DampingModifier
                                    {
                                        DampingCoefficient = 3.0f
                                    },
                                    new ColourInterpolator2
                                    {
                                        InitialColour = new Vector3(0.7529412f, 0.7529412f, 1.0f),
                                        FinalColour = new Vector3(0.7529412f, 1.0f, 1.0f)
                                    },                                       
                                    new LinearGravityModifier
                                    {
                                        GravityVector = new Vector3(0.0f, 1.0f, 0.0f),
                                        Strength = 150.0f                                        
                                    }
                                },
                
                BlendMode = EmitterBlendMode.Add,

                Controllers = new ControllerPipeline
                                {
                                    new TriggerOffsetController
                                    {
                                        TriggerOffset = Vector3.Zero
                                    },
                                    //new TriggerRotationController
                                    //{
                                    //    TriggerRotation = Vector3.Zero
                                    //}
                                },

                //TriggerOffset
                //MinimumTriggerPeriod
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Star");
            emitter = new PointEmitter//Flakes
            {
                Budget = 42 * budgetFactor,
                Term = 0.5f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 48.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(8.0f * scale, 24.0f * scale),
                ReleaseRotation = new RotationRange
                {
                    Yaw = new Range(0.0f, 0.0f),
                    Pitch = new Range(0.0f, 0.0f),
                    Roll = new Range(0.0f, 3.14f),
                },
                //ReleaseImpulse

                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new ColourInterpolator2
                                    {
                                        InitialColour = new Vector3(0.7529412f, 0.7529412f, 1.0f),
                                        FinalColour = new Vector3(0.7529412f, 1.0f, 1.0f)
                                    },  
                                    new OpacityInterpolator2
                                    { 
                                        InitialOpacity = 1.0f,
                                        FinalOpacity = 0.0f
                                    },                                        
                                },
                
                BlendMode = EmitterBlendMode.Add,

                Controllers = new ControllerPipeline
                                {
                                    new TriggerOffsetController
                                    {
                                        TriggerOffset = Vector3.Zero
                                    },
                                    //new TriggerRotationController
                                    //{
                                    //    TriggerRotation = Vector3.Zero
                                    //}
                                },
                //TriggerOffset
                //MinimumTriggerPeriod
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            _particleEffectIceStarTrail = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);
            #endregion //IceStarTrail
            
            #region ChainLinkBreak

            //****************************
            //ChainLinkBreak
            //****************************
            budgetFactor = 5;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/Particle005");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle005");
            emitter = new PointEmitter//Sparks
            {
                Budget = 20 * budgetFactor,
                Term = 0.75f,
                ReleaseQuantity = 20,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 250.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(0.0f * scale, 8.0f * scale),
                //ReleaseRotation,
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator2
                                    { 
                                        InitialOpacity = 1.0f,
                                        FinalOpacity = 0.0f
                                    },
                                    new DampingModifier
                                    {
                                        DampingCoefficient = 2.0f                                            
                                    },
                                    new LinearGravityModifier
                                    {
                                        GravityVector = new Vector3(0.0f, 1.0f, 0.0f),
                                        Strength = 1000.0f
                                    }
                                },
                BlendMode = EmitterBlendMode.Add,

                Controllers = new ControllerPipeline
                                {
                                    new TriggerOffsetController
                                    {
                                        TriggerOffset = Vector3.Zero
                                    },
                                    //new TriggerRotationController
                                    //{
                                    //    TriggerRotation = Vector3.Zero
                                    //}
                                },

                //TriggerOffset
                //MinimumTriggerPeriod
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle005");
            emitter = new PointEmitter//Flash
            {
                Budget = 1 * budgetFactor,
                Term = 0.1f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(50.0f, 50.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(0.0f, 0.5f),
                ReleaseScale = new Range(100.0f * scale, 100.0f * scale),
                //ReleaseRotation,
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator2
                                    { 
                                        InitialOpacity = 1.0f,
                                        FinalOpacity = 0.0f
                                    }                                       
                                },
                
                BlendMode = EmitterBlendMode.Add,

                Controllers = new ControllerPipeline
                                {
                                    new TriggerOffsetController
                                    {
                                        TriggerOffset = Vector3.Zero
                                    },
                                    //new TriggerRotationController
                                    //{
                                    //    TriggerRotation = Vector3.Zero
                                    //}
                                },

                //TriggerOffset
                //MinimumTriggerPeriod
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            _particleEffectChainLinkBreak = particleEffect;
            //_particleEffectPart.Add(_particleEffectChainLinkBreak);

            #endregion //ChainLinkBreak
        }

        public virtual void Update(GameTime gameTime)
        {
            if (_disabled)
                return;

            for (int i = 0; i < _update.Count; i++)
            {
                if (_update[i].ActiveParticlesCount > 0)
                    _update[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (_disabled)
                return;

            for (int i = 0; i < _draw.Count; i++)
            {
                if (_draw[i].ActiveParticlesCount > 0)
                    Pax4ParticleEffect._current._particleRenderer.RenderEffect(_draw[i], ref Pax4Game._matIdentity, ref Pax4Camera._current._matView, ref Pax4Camera._current._matProjection, ref Pax4Camera._current._position);
            }

            for (int i = 0; i < _drawAura.Count; i++)
            {
                if (_drawAura[i].ActiveParticlesCount > 0)
                    Pax4ParticleEffect._current._particleRenderer.RenderEffect(_drawAura[i], ref Pax4Game._matIdentity, ref Pax4Camera._current._matView, ref Pax4Camera._current._matProjection, ref Pax4Camera._current._position);
            }
        }

        public virtual void Draw3(GameTime gameTime)
        {
            if (_disabled)
                return;

            for (int i = 0; i < _draw3.Count; i++)
            {
                if (_draw3[i].ActiveParticlesCount > 0)
                    Pax4ParticleEffect._current._particleRenderer3.RenderEffect(_draw3[i], ref Pax4Game._matIdentity, ref Pax4Camera._current._matView, ref Pax4Camera._current._matProjection, ref Pax4Camera._current._position);
            }

            for (int i = 0; i < _drawAura3.Count; i++)
            {
                if (_drawAura3[i].ActiveParticlesCount > 0)
                    Pax4ParticleEffect._current._particleRenderer3.RenderEffect(_drawAura3[i], ref Pax4Game._matIdentity, ref Pax4Camera._current._matView, ref Pax4Camera._current._matProjection, ref Pax4Camera._current._position);
            }
        }

        public virtual void Dx()
        {
            //_dx = false;

            Reset();

            _update = null;
            _draw = null;
            _draw3 = null;
            _drawAura = null;
            _drawAura3 = null;
            
            if (this == _current)
                _current = null;
        }

        public virtual void Reset()
        {
            ResetUpdate();
            ResetDraw();
            ResetDraw3();
            ResetDrawAura();
            ResetDrawAura3();
        }

        public virtual void AddUpdate(ParticleEffect p_particleEffect)
        {
            if (_update != null && p_particleEffect != null)
                _update.Add(p_particleEffect);
        }

        public virtual void RemoveUpdate(ParticleEffect p_particleEffect)
        {
            if (_update != null && p_particleEffect != null)
                _update.Remove(p_particleEffect);
        }

        public virtual void ResetUpdate()
        {
            _update.Clear();
        }

        public virtual void AddDraw(ParticleEffect p_particleEffect)
        {
            if (_draw != null && p_particleEffect != null)
                _draw.Add(p_particleEffect);
        }
        public virtual void RemoveDraw(ParticleEffect p_particleEffect)
        {
            if (_draw != null && p_particleEffect != null)
                _draw.Remove(p_particleEffect);
        }
        public virtual void ResetDraw()
        {
            _draw.Clear();
        }

        public virtual void AddDraw3(ParticleEffect p_particleEffect)
        {
            if (_draw3 != null && p_particleEffect != null)
                _draw3.Add(p_particleEffect);
        }
        public virtual void RemoveDraw3(ParticleEffect p_particleEffect)
        {
            if (_draw3 != null && p_particleEffect != null)
                _draw3.Remove(p_particleEffect);
        }
        public virtual void ResetDraw3()
        {
            _draw3.Clear();
        }

        public virtual void AddDrawAura(ParticleEffect p_particleEffect)
        {
            if (_drawAura != null && p_particleEffect != null)
                _drawAura.Add(p_particleEffect);
        }
        public virtual void RemoveDrawPartAura(ParticleEffect p_particleEffect)
        {
            if (_drawAura != null && p_particleEffect != null)
                _drawAura.Remove(p_particleEffect);
        }
        public virtual void ResetDrawAura()
        {
            _drawAura.Clear();
        }

        public virtual void AddDrawAura3(ParticleEffect p_particleEffect)
        {
            if (_drawAura3 != null && p_particleEffect != null)
                _drawAura3.Add(p_particleEffect);
        }
        public virtual void RemoveDrawAura3(ParticleEffect p_particleEffect)
        {
            if (_drawAura3 != null && p_particleEffect != null)
                _drawAura3.Remove(p_particleEffect);
        }
        public virtual void ResetDrawAura3()
        {
            _drawAura3.Clear();
        }

        //public void Trigger(String p_particleEffect, ref Vector3 p_screen)
        //{
        //    ParticleEffect particleEffect = null;

        //    _particleEffect.TryGetValue(p_particleEffect, out particleEffect);

        //    Trigger(particleEffect, ref p_screen);
        //}

        //public void TriggerWorld(String p_particleEffect, Vector3 p_world)
        //{
        //    ParticleEffect particleEffect = null;

        //    _particleEffect.TryGetValue(p_particleEffect, out particleEffect);

        //    p_world = Pax4Tools.WorldToScreen(p_world);

        //    Trigger(particleEffect, ref p_world);
        //}

        //public void Trigger(ParticleEffect p_particleEffect, ref Vector3 p_position)
        //{
        //    p_particleEffect.Trigger(ref p_position);
        //}
        
        //public ParticleEffect Get(String p_particleEffect)
        //{
        //    ParticleEffect result = null;

        //    if (!_particleEffect.ContainsKey(p_particleEffect))
        //        Load(p_particleEffect);

        //    _particleEffect.TryGetValue(p_particleEffect, out result);

        //    return result;
        //}

        public virtual void Enable()
        {
            _disabled = false;
        }

        public virtual void Disable()
        {
            _disabled = true;
        }
    }
}