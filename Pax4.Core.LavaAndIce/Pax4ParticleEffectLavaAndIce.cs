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
    [KnownType(typeof(Pax4ParticleEffectLavaAndIce))]
    public class Pax4ParticleEffectLavaAndIce : Pax4ParticleEffect
    {
        public ParticleEffect _particleEffectScoreShieldUp = null;
        public ParticleEffect _particleEffectScoreHelpingHand = null;
        public ParticleEffect _particleEffectLightSpeed = null;

        public ParticleEffect _particleEffectLavaExplosion = null;
        public ParticleEffect _particleEffectLavaTrail = null;
        public ParticleEffect _particleEffectLavaTrail1 = null;
        public ParticleEffect _particleEffectLavaTrailEnemy = null;
        public ParticleEffect _particleEffectLavaMonsterShield = null;
        public ParticleEffect _particleEffectLavaMonsterExplosion = null;
        public ParticleEffect _particleEffectLavaActive = null;

        public ParticleEffect _particleEffectIceExplosion = null;
        public ParticleEffect _particleEffectIceTrail = null;
        public ParticleEffect _particleEffectIceTrail1 = null;
        public ParticleEffect _particleEffectIceTrailEnemy = null;
        public ParticleEffect _particleEffectIceMonsterShield = null;
        public ParticleEffect _particleEffectIceMonsterExplosion = null;
        public ParticleEffect _particleEffectIceActive = null;

        public ParticleEffect _particleEffectScore100 = null;
        public ParticleEffect _particleEffectScore300 = null;
        public ParticleEffect _particleEffectScore500 = null;
        public ParticleEffect _particleEffectScoreGreat = null;
        public ParticleEffect _particleEffectScorePerfect = null;

        public Pax4ParticleEffectLavaAndIce()
            : base()
        {
            _current = this;

            ParticleEffect particleEffect = null;
            Texture2D particleTexture = null;
            AbstractEmitter emitter = null;

            float scale = 1.0f;

            if (Pax4Camera._backBufferWidth <= 320)
                scale = Pax4Camera._current._scale.X * 0.9f;
            else
                scale = Pax4Camera._current._scale.X;

            float scoreScale = 70.0f;
            float messageScale = 150.0f;
            float helpingHandScale = 130.0f;
            
            int budgetFactor = 3;

            #region LavaExplosion
            //****************************
            //LavaExplosion
            //****************************
            budgetFactor = 10;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/Cloud001");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Cloud001");
            emitter = new CircleEmitter//Smoke Trail
            {
                Budget = 8 * budgetFactor,//16
                Term = 2.5f,
                ReleaseQuantity = 8,//16
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 128.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(0.5019608f, 0.5019608f),
                                    Green = new Range(0.5019608f, 0.5019608f),
                                    Blue = new Range(0.5019608f, 0.5019608f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 16.0f * scale),
                //ReleaseRotation
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator2
                                    { 
                                        InitialOpacity = 0.2f,
                                        FinalOpacity = 0.0f
                                    },
                                    new ScaleInterpolator2
                                    {
                                        InitialScale = 48 * scale,
                                        FinalScale = 255 * scale
                                    },
                                    new DampingModifier
                                    {
                                        DampingCoefficient = 1.0f                                            
                                    },
                                    new RotationModifier
                                    {
                                        RotationRate = new Vector3(0.0f, 0.0f, 1.0f),
                                    }
                                },


                BlendMode = EmitterBlendMode.Alpha,

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

                Radius = 1.0f * scale,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            Pax4Texture2D._current.Load("ParticleEffect/Particle004");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle004");
            emitter = new CircleEmitter//Flames
            {
                Budget = 64 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 64,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 100.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(0.7f, 1.0f),
                                    Green = new Range(0.5019608f, 0.5019608f),
                                    Blue = new Range(0.0f, 0.0f),
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 80.0f * scale),
                ReleaseRotation = new RotationRange
                                    {
                                        Yaw = new Range(0.0f, 0.0f),
                                        Pitch = new Range(0.0f, 0.0f),
                                        Roll = new Range(0.0f, 6.28f),
                                    },
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator2
                                    { 
                                        InitialOpacity = 0.5f,
                                        FinalOpacity = 0.0f
                                    },
                                    new RotationModifier
                                    {
                                        RotationRate = new Vector3(0.0f, 0.0f, 1.0f),
                                    },                                        
                                    new DampingModifier
                                    {
                                        DampingCoefficient = 1.0f                                            
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

                Radius = 1.0f * scale,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            Pax4Texture2D._current.Load("ParticleEffect/Particle005");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle005");
            emitter = new PointEmitter//Sparks
            {
                Budget = 35 * budgetFactor,
                Term = 0.75f,
                ReleaseQuantity = 35,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 250.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(1.0f, 1.0f),
                                    Green = new Range(0.8784314f, 0.8784314f),
                                    Blue = new Range(0.7529412f, 0.7529412f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(3.0f * scale, 7.0f * scale),
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

                ReleaseSpeed = new Range(50.0f, 50.0f), // { Value = 25f, Variation = 25f },
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(1.0f, 1.0f),
                                    Green = new Range(1.0f, 1.0f),
                                    Blue = new Range(1.0f, 1.0f)
                                },
                ReleaseOpacity = new Range(0.0f, 0.5f),
                ReleaseScale = new Range(192.0f * scale, 192.0f * scale), // {  Value = 32f, Variation = 16f },
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

            _particleEffectLavaExplosion = particleEffect;            
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //LavaExplosion

            #region LavaTrail
            //****************************
            //LavaTrail
            //****************************
            budgetFactor = 1;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/LensFlare");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/LensFlare");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 64 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(32.0f, 160.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(1.0f, 1.0f),
                                    Green = new Range(0.5019608f, 0.5019608f),
                                    Blue = new Range(0.0f, 0.0f)
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
                                        InitialColour = new Vector3(1.0f, 0.5019608f, 0.0f),
                                        FinalColour = new Vector3(0.7f, 0.5019608f, 0.0f)
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

            Pax4Texture2D._current.Load("ParticleEffect/Particle001");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle001");
            emitter = new PointEmitter//Flakes
            {
                Budget = 64 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 48.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(1.0f, 1.0f),
                                    Green = new Range(0.5019608f, 0.5019608f),
                                    Blue = new Range(0.0f, 0.0f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 16.0f * scale),
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
                                        InitialColour = new Vector3(1.0f, 0.5019608f, 0.0f),
                                        FinalColour = new Vector3(0.7f, 0.5019608f, 0.0f)
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

            _particleEffectLavaTrail = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //LavaTrail

            #region LavaTrailEnemy
            //****************************
            //LavaTrailEnemy
            //****************************
            budgetFactor = 10;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/LensFlare");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/LensFlare");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 16 * budgetFactor,
                Term = 0.25f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(32.0f, 160.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(0.5019608f, 0.5019608f),
                    Blue = new Range(0.0f, 0.0f)
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
                                        InitialColour = new Vector3(1.0f, 0.5019608f, 0.0f),
                                        FinalColour = new Vector3(0.7f, 0.5019608f, 0.0f)
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

            Pax4Texture2D._current.Load("ParticleEffect/Particle001");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle001");
            emitter = new PointEmitter//Flakes
            {
                Budget = 16 * budgetFactor,
                Term = 0.35f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 48.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(0.5019608f, 0.5019608f),
                    Blue = new Range(0.0f, 0.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 16.0f * scale),
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
                                        InitialColour = new Vector3(1.0f, 0.5019608f, 0.0f),
                                        FinalColour = new Vector3(0.7f, 0.5019608f, 0.0f)
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

            _particleEffectLavaTrailEnemy = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //LavaTrailEnemy

            #region LavaTrail1
            //****************************
            //LavaTrail1
            //****************************
            budgetFactor = 1;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/Particle004");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle004");
            emitter = new CircleEmitter//Flames
            {
                Budget = 64 * budgetFactor,
                Term = 0.75f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(25.0f, 75.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(0.9f, 1.0f),
                                    Green = new Range(0.5019608f, 0.5019608f),
                                    Blue = new Range(0.0f, 0.0f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 80.0f * scale),
                ReleaseRotation = new RotationRange
                {
                    Yaw = new Range(0.0f, 0.0f),
                    Pitch = new Range(0.0f, 0.0f),
                    Roll = new Range(0.0f, 6.28f),
                },
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator2
                                    { 
                                        InitialOpacity = 0.5f,
                                        FinalOpacity = 0.0f
                                    },                                        
                                    new RotationModifier
                                    {
                                        RotationRate = new Vector3(0.0f, 0.0f, 1.0f),
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

                Radius = 3.0f * scale,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            Pax4Texture2D._current.Load("ParticleEffect/Cloud004");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Cloud004");
            emitter = new CircleEmitter//Dying Flames
            {
                Budget = 64 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(25.0f, 75.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(0.9f, 1.0f),
                                    Green = new Range(0.5019608f, 0.5019608f),
                                    Blue = new Range(0.0f, 0.0f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 80.0f * scale),
                ReleaseRotation = new RotationRange
                {
                    Yaw = new Range(0.0f, 0.0f),
                    Pitch = new Range(0.0f, 0.0f),
                    Roll = new Range(0.0f, 6.28f),
                },
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator3
                                    { 
                                        InitialOpacity = 0.0f,
                                        MedianOpacity = 0.15f,
                                        Median = 0.7f,
                                        FinalOpacity = 0.0f
                                    },                                        
                                    new RotationModifier
                                    {
                                        RotationRate = new Vector3(0.0f, 0.0f, 1.0f),
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

                Radius = 3.0f * scale,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            _particleEffectLavaTrail1 = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //LavaTrail1

            #region LavaMonsterShield
            //****************************
            //LavaMonsterShield
            //****************************
            budgetFactor = 1;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/FlowerBurst");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/FlowerBurst");

            emitter = new CircleEmitter//Flames
            {
                Budget = 64 * budgetFactor,//128
                Term = 0.75f,
                ReleaseQuantity = 4,//8
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 0.14f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(0.7019608f, 0.7019608f),
                    Blue = new Range(0.3f, 0.3f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(1.0f, 2.5f),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 6.28f),
                //},
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator2
                                    { 
                                        InitialOpacity = 0.5f,
                                        FinalOpacity = 0.0f
                                    },                                        
                                    new LinearGravityModifier
                                    {
                                        GravityVector = new Vector3(0.0f, 50.0f, 0.0f)
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

                Radius = 3.11f,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            Pax4Texture2D._current.Load("ParticleEffect/LensFlare");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/LensFlare");
            emitter = new CircleEmitter//Dying Flames
            {
                Budget = 128 * budgetFactor,
                Term = 0.5f,
                ReleaseQuantity = 8,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 0.14f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(0.7f, 1.0f),
                    Green = new Range(0.7019608f, 0.7019608f),
                    Blue = new Range(0.3f, 0.3f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(1.5f, 4.0f),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 6.28f),
                //},
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator3
                                    { 
                                        InitialOpacity = 0.0f,
                                        MedianOpacity = 0.25f,
                                        Median = 0.7f,
                                        FinalOpacity = 0.0f
                                    },                                        
                                    new LinearGravityModifier
                                    {
                                        GravityVector = new Vector3(0.0f, 30.0f, 0.0f)
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

                Radius = 3.0f,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);
            
            _particleEffectLavaMonsterShield = particleEffect;
            AddUpdate(particleEffect);
            AddDrawAura3(particleEffect);

            #endregion //LavaMonsterShield

            #region LavaMonsterExplosion
            ////****************************
            ////LavaMonsterExplosion
            ////****************************
            //budgetFactor = 1;
            //particleEffect = new ParticleEffect();
            //_particleEffectLavaMonsterExplosion = particleEffect;
            //AddUpdate(particleEffect);
            //AddDraw(particleEffect);
            #endregion LavaMonste Explosion

            #region LavaActive
            //****************************
            //LavaActive
            //****************************
            budgetFactor = 1;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/LensFlare");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/LensFlare");
            emitter = new BoxEmitter//particles
            {
                Budget = 16 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 25.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(1.0f, 1.0f),
                                    Green = new Range(0.5019608f, 0.5019608f),
                                    Blue = new Range(0.0f, 0.0f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 16.0f * scale),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 6.28f),
                //},
                //ReleaseImpulse = 
                ParticleTexture = particleTexture,

                Width = 100.0f * scale,
                Height = 250.0f * scale,

                Modifiers = new ModifierCollection
                                {
                                    new LinearGravityModifier
                                    {
                                        GravityVector = new Vector3(0.0f, -1.0f, 0.0f),
                                        Strength = 150.0f                                        
                                    },
                                    new OpacityInterpolator3 
                                    { 
                                        InitialOpacity = 0.0f,
                                        MedianOpacity = 1.0f,
                                        Median = 0.50f,
                                        FinalOpacity = 0.0f
                                    },
                                    new ScaleInterpolator3
                                    {
                                        InitialScale = 16 * scale,
                                        MedianScale = 32 * scale,
                                        Median = 0.50f,
                                        FinalScale = 16 * scale
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
                //MinimumTriggerPeriod = 0.1f
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            Pax4Texture2D._current.Load("ParticleEffect/Beam");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Beam");
            emitter = new BoxEmitter//Fast Beams
            {
                Budget = 5 * budgetFactor,
                Term = 0.5f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 48.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(1.0f, 1.0f),
                                    Green = new Range(0.5019608f, 0.5019608f),
                                    Blue = new Range(0.0f, 0.0f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(256.0f * scale, 256.0f * scale),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 3.14f),
                //},
                //ReleaseImpulse

                ParticleTexture = particleTexture,

                Width = 150.0f * scale,
                Height = 100.0f * scale,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator3 
                                    { 
                                        InitialOpacity = 0.0f,
                                        MedianOpacity = 0.1f,
                                        Median = 0.50f,
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

            Pax4Texture2D._current.Load("ParticleEffect/BeamBlurred");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/BeamBlurred");
            emitter = new BoxEmitter//Fast Beams
            {
                Budget = 5 * budgetFactor,
                Term = 0.5f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 48.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(0.9f, 0.9f),
                                    Green = new Range(0.4019608f, 0.4019608f),
                                    Blue = new Range(0.0f, 0.0f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(256.0f * scale, 256.0f * scale),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 3.14f),
                //},
                //ReleaseImpulse

                ParticleTexture = particleTexture,

                Width = 180.0f * scale,
                Height = 100.0f * scale,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator3 
                                    { 
                                        InitialOpacity = 0.0f,
                                        MedianOpacity = 0.3f,
                                        Median = 0.50f,
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

            _particleEffectLavaActive = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //LavaActive

            #region IceExplosion
            //****************************
            //IceExplosion
            //****************************
            budgetFactor = 10;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/Cloud001");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Cloud001");
            emitter = new CircleEmitter//Smoke Trail
            {
                Budget = 8 * budgetFactor,//16
                Term = 2.5f,
                ReleaseQuantity = 8,//16
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 128.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(0.0f, 0.0f),
                                    Green = new Range(1.0f, 1.0f),
                                    Blue = new Range(1.0f, 1.0f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 16.0f * scale),
                //ReleaseRotation
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator2
                                    { 
                                        InitialOpacity = 0.2f,
                                        FinalOpacity = 0.0f
                                    },
                                    new ScaleInterpolator2
                                    {
                                        InitialScale = 48 * scale,
                                        FinalScale = 255 * scale
                                    },
                                    new DampingModifier
                                    {
                                        DampingCoefficient = 1.0f                                            
                                    },
                                    new RotationModifier
                                    {
                                        RotationRate = new Vector3(0.0f, 0.0f, 1.0f),
                                    }
                                },

                BlendMode = EmitterBlendMode.Alpha,

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

                Radius = 1.0f * scale,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            Pax4Texture2D._current.Load("ParticleEffect/Particle004");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle004");
            emitter = new CircleEmitter//Flames
            {
                Budget = 64 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 64,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 100.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(0.7529412f, 0.7529412f),
                                    Green = new Range(1.0f, 1.0f),
                                    Blue = new Range(1.0f, 1.0f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 80.0f * scale),
                ReleaseRotation = new RotationRange
                                    {
                                        Yaw = new Range(0.0f, 0.0f),
                                        Pitch = new Range(0.0f, 0.0f),
                                        Roll = new Range(0.0f, 6.28f),
                                    },
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator2
                                    { 
                                        InitialOpacity = 0.5f,
                                        FinalOpacity = 0.0f
                                    },
                                    new RotationModifier
                                    {
                                        RotationRate = new Vector3(0.0f, 0.0f, 1.0f),
                                    },                                        
                                    new DampingModifier
                                    {
                                        DampingCoefficient = 1.0f                                            
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

                Radius = 1.0f * scale,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            Pax4Texture2D._current.Load("ParticleEffect/Particle005");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle005");
            emitter = new PointEmitter//Sparks
            {
                Budget = 35 * budgetFactor,
                Term = 0.75f,
                ReleaseQuantity = 35,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 250.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(0.7529412f, 0.7529412f),
                                    Green = new Range(1.0f, 1.0f),
                                    Blue = new Range(1.0f, 1.0f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(3.0f * scale, 7.0f * scale),
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

                ReleaseSpeed = new Range(50.0f, 50.0f), // { Value = 25f, Variation = 25f },
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(1.0f, 1.0f),
                                    Green = new Range(1.0f, 1.0f),
                                    Blue = new Range(1.0f, 1.0f)
                                },
                ReleaseOpacity = new Range(0.0f, 0.5f),
                ReleaseScale = new Range(192.0f * scale, 192.0f * scale), // {  Value = 32f, Variation = 16f },
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

            _particleEffectIceExplosion = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //IceExplosion

            #region IceTrail
            //****************************
            //IceTrail
            //****************************
            budgetFactor = 1;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/LensFlare");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/LensFlare");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 64 * budgetFactor,
                Term = 1.0f,
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

            Pax4Texture2D._current.Load("ParticleEffect/Particle001");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle001");
            emitter = new PointEmitter//Flakes
            {
                Budget = 64 * budgetFactor,
                Term = 1.0f,
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
                ReleaseScale = new Range(16.0f * scale, 16.0f * scale),
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

            _particleEffectIceTrail = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //IceTrail

            #region IceTrailEnemy
            //****************************
            //IceTrailEnemy
            //****************************
            budgetFactor = 10;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/LensFlare");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/LensFlare");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 16 * budgetFactor,
                Term = 0.25f,
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

            Pax4Texture2D._current.Load("ParticleEffect/Particle001");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle001");
            emitter = new PointEmitter//Flakes
            {
                Budget = 16 * budgetFactor,
                Term = 0.35f,
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
                ReleaseScale = new Range(16.0f * scale, 16.0f * scale),
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

            _particleEffectIceTrailEnemy = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //IceTrailEnemy

            #region IceTrail1
            //****************************
            //IceTrail1
            //****************************
            budgetFactor = 1;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/Particle004");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Particle004");
            emitter = new CircleEmitter//Flames
            {
                Budget = 64 * budgetFactor,
                Term = 0.75f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(25.0f, 75.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(0.7529412f, 0.7529412f),
                                    Green = new Range(1.0f, 1.0f),
                                    Blue = new Range(1.0f, 1.0f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 80.0f * scale),
                ReleaseRotation = new RotationRange
                {
                    Yaw = new Range(0.0f, 0.0f),
                    Pitch = new Range(0.0f, 0.0f),
                    Roll = new Range(0.0f, 6.28f),
                },
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator2
                                    { 
                                        InitialOpacity = 0.5f,
                                        FinalOpacity = 0.0f
                                    },                                        
                                    new RotationModifier
                                    {
                                        RotationRate = new Vector3(0.0f, 0.0f, 1.0f),
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

                Radius = 3.0f * scale,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            Pax4Texture2D._current.Load("ParticleEffect/Cloud004");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Cloud004");
            emitter = new CircleEmitter//Dying Flames
            {
                Budget = 64 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(25.0f, 75.0f),
                ReleaseColour = new ColourRange
                                {
                                    Red = new Range(0.7529412f, 0.7529412f),
                                    Green = new Range(1.0f, 1.0f),
                                    Blue = new Range(1.0f, 1.0f)
                                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(16.0f * scale, 80.0f * scale),
                ReleaseRotation = new RotationRange
                {
                    Yaw = new Range(0.0f, 0.0f),
                    Pitch = new Range(0.0f, 0.0f),
                    Roll = new Range(0.0f, 6.28f),
                },
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator3
                                    { 
                                        InitialOpacity = 0.0f,
                                        MedianOpacity = 0.15f,
                                        Median = 0.7f,
                                        FinalOpacity = 0.0f
                                    },                                        
                                    new RotationModifier
                                    {
                                        RotationRate = new Vector3(0.0f, 0.0f, 1.0f),
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

                Radius = 3.0f * scale,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            _particleEffectIceTrail1 = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //IceTrail1

            #region IceMonsterShield
            //****************************
            //IceMonsterShield
            //****************************
            budgetFactor = 1;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/FlowerBurst");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/FlowerBurst");
            emitter = new CircleEmitter//Flames
            {
                Budget = 64 * budgetFactor,//128
                Term = 0.75f,
                ReleaseQuantity = 4,//8
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 0.14f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(0.7529412f, 0.7529412f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(1.0f, 2.5f),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 6.28f),
                //},
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator2
                                    { 
                                        InitialOpacity = 0.5f,
                                        FinalOpacity = 0.0f
                                    },                                        
                                    new LinearGravityModifier
                                    {
                                        GravityVector = new Vector3(0.0f, 50.0f, 0.0f)
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

                Radius = 3.11f,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            Pax4Texture2D._current.Load("ParticleEffect/LensFlare");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/LensFlare");
            emitter = new CircleEmitter//Dying Flames
            {
                Budget = 128 * budgetFactor,
                Term = 0.5f,
                ReleaseQuantity = 8,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 0.14f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(0.7529412f, 0.7529412f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(1.5f, 4.0f),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 6.28f),
                //},
                //ReleaseImpulse,
                ParticleTexture = particleTexture,

                Modifiers = new ModifierCollection
                                {
                                    new OpacityInterpolator3
                                    { 
                                        InitialOpacity = 0.0f,
                                        MedianOpacity = 0.25f,
                                        Median = 0.7f,
                                        FinalOpacity = 0.0f
                                    },                                        
                                    new LinearGravityModifier
                                    {
                                        GravityVector = new Vector3(0.0f, 30.0f, 0.0f)
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

                Radius = 3.0f,
                Shell = true,
                Radiate = true,
            };

            emitter.Initialise();
            particleEffect.Emitters.Add(emitter);

            _particleEffectIceMonsterShield = particleEffect;

            AddUpdate(particleEffect);
            AddDrawAura3(particleEffect);

            #endregion //IceMonsterShield

            #region IceMonsterExplosion
            ////****************************
            ////IceMonsterExplosion
            ////****************************
            //budgetFactor = 1;
            //particleEffect = new ParticleEffect();   
            //_particleEffectIceMonsterExplosion = particleEffect;
            //_particleEffect.Add(_particleEffectIceMonsterExplosion);
            #endregion

            #region IceActive
            ////****************************
            ////IceActive
            ////****************************
            //budgetFactor = 10;
            //particleEffect = new ParticleEffect();
            //_particleEffectIceActive = particleEffect;
            //_particleEffect.Add(_particleEffectIceActive);
            #endregion

            #region ScoreHelpingHand
            //****************************
            //ScoreHelpingHand
            //****************************
            budgetFactor = 4;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/ScoreHelpingHand");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/ScoreHelpingHand");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 1 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 50.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(helpingHandScale * scale, helpingHandScale * scale),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 0.0f),
                //},
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

            _particleEffectScoreHelpingHand = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //ScoreHelpingHand

            #region ScoreShieldUp
            //****************************
            //ScoreShieldUp
            //****************************
            budgetFactor = 4;
            particleEffect = new ParticleEffect();
            Pax4Texture2D._current.Load("ParticleEffect/ScoreShieldUp");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/ScoreShieldUp");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 1 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 50.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(messageScale * scale, messageScale * scale),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 0.0f),
                //},
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
            _particleEffectScoreShieldUp = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //ScoreShieldUp

            #region Score100

            //****************************
            //Score100
            //****************************
            budgetFactor = 8;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/Score100");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Score100");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 1 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 50.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(scoreScale * scale, scoreScale * scale),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 0.0f),
                //},
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

            _particleEffectScore100 = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //Score100

            #region Score300
            //****************************
            //Score300
            //****************************
            budgetFactor = 8;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/Score300");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Score300");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 1 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 50.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(scoreScale * scale, scoreScale * scale),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 0.0f),
                //},
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

            _particleEffectScore300 = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //Score300

            #region Score500
            //****************************
            //Score500
            //****************************
            budgetFactor = 8;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/Score500");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/Score500");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 1 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 50.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(scoreScale * scale, scoreScale * scale),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 0.0f),
                //},
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

            _particleEffectScore500 = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //Score500

            #region ScoreGreat
            //****************************
            //ScoreGreat
            //****************************
            budgetFactor = 4;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/ScoreGreat");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/ScoreGreat");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 1 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 50.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(messageScale * scale, messageScale * scale),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 0.0f),
                //},
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

            _particleEffectScoreGreat = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion ScoreGreat

            #region ScorePerfect
            //****************************
            //ScorePerfect
            //****************************
            budgetFactor = 4;
            particleEffect = new ParticleEffect();

            Pax4Texture2D._current.Load("ParticleEffect/ScorePerfect");
            particleTexture = Pax4Texture2D._current.Get("ParticleEffect/ScorePerfect");
            emitter = new PointEmitter//Sparkles
            {
                Budget = 1 * budgetFactor,
                Term = 1.0f,
                ReleaseQuantity = 1,
                Enabled = true,

                ReleaseSpeed = new Range(0.0f, 50.0f),
                ReleaseColour = new ColourRange
                {
                    Red = new Range(1.0f, 1.0f),
                    Green = new Range(1.0f, 1.0f),
                    Blue = new Range(1.0f, 1.0f)
                },
                ReleaseOpacity = new Range(1.0f, 1.0f),
                ReleaseScale = new Range(messageScale * scale, messageScale * scale),
                //ReleaseRotation = new RotationRange
                //{
                //    Yaw = new Range(0.0f, 0.0f),
                //    Pitch = new Range(0.0f, 0.0f),
                //    Roll = new Range(0.0f, 0.0f),
                //},
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

            _particleEffectScorePerfect = particleEffect;
            AddUpdate(particleEffect);
            AddDraw(particleEffect);

            #endregion //ScorePerfect
        }
    }
}