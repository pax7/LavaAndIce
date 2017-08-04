//using System;
//using System.Collections.Generic;
//using System.IO;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

//using Pax4.JigLibX.Physics;
//
//using Pax4.JigLibX.Utils;

//using Pax4.ProjectMercury;
//using Pax4.ProjectMercury.Emitters;
//using Pax4.ProjectMercury.Modifiers;
//using Pax4.ProjectMercury.Renderers;
//using Pax4.ProjectMercury.Controllers;
//using Pax4.ProjectMercury.Proxies;

//namespace Pax4.Core
//{
//    /// <summary>
//    /// This is the main type for your game
//    /// </summary>
//    public class Pax4GameRnd : Pax4Game
//    {
//        private ParticleEffectProxy _proxy1 = null;
//        private ParticleEffectProxy _proxy2 = null;
//        private ParticleEffect _particleEffect = null;

//        public Pax4GameRnd()
//            : base()
//        {

//        }
        
//        /// <summary>
//        /// Allows the game to perform any initialization it needs to before starting to run.
//        /// This is where it can query for any required services and load any non-graphic
//        /// related content.  Calling base.Initialize will enumerate through any components
//        /// and initialize them as well.
//        /// </summary>
//        protected override void Initialize()
//        {
//            base.Initialize();

//            //for (int i = 0; i < this.ParticleEffect.Emitters.Count; i++)
//            //    this.ParticleEffect.Emitters[i].Initialise();

//            Pax4ParticleEffect particleEffect = new Pax4ParticleEffect();
//            _particleEffect = Pax4ParticleEffect._current._particleEffectIceStarTrail;

//            _particleEffect = Pax4ParticleEffect._current._particleEffectIceStarTrail;//Game.Content.Load<ParticleEffect>("Demo1");

//            //for (int i = 0; i < this.ParticleEffect.Emitters.Count; i++)
//            //    this.ParticleEffect.Emitters[i].ParticleTexture = Game.Content.Load<Texture2D>("Star");

//            _proxy1 = new ParticleEffectProxy(_particleEffect) { World = Matrix.CreateScale(.5f) * Matrix.CreateTranslation(800, 0, 0) };
//            _proxy2 = new ParticleEffectProxy(_particleEffect) { World = Matrix.CreateScale(.1f) * Matrix.CreateTranslation(-900, 0, 0) };
//        }

//        /// <summary>
//        /// LoadContent will be called once per game and is the place to load
//        /// all of your content.
//        /// </summary>
//        protected override void LoadContent()
//        {
//            base.LoadContent();            
//        }

//        /// <summary>
//        /// UnloadContent will be called once per game and is the place to unload
//        /// all content.
//        /// </summary>
//        protected override void UnloadContent()
//        {
//            base.UnloadContent();
//        }

//        /// <summary>
//        /// Allows the game to run logic such as updating the world,
//        /// checking for collisions, gathering input, and playing audio.
//        /// </summary>
//        /// <param name="gameTime">Provides a snapshot of timing values.</param>        
//        protected override void Update(GameTime gameTime)
//        {
//            base.Update(gameTime);

//            (_particleEffect.Emitters[0].Controllers[0] as TriggerOffsetController).TriggerOffset = new Vector3
//            {
//                X = (float)Math.Cos(gameTime.TotalGameTime.TotalSeconds * 1.15f) * 275f,
//                Y = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 1.15f) * 250f,
//                Z = (float)Math.Cos(gameTime.TotalGameTime.TotalSeconds * 0.75) * 290f,
//            };

//            (_particleEffect.Emitters[1].Controllers[0] as TriggerOffsetController).TriggerOffset = new Vector3
//            {
//                X = (float)Math.Cos(gameTime.TotalGameTime.TotalSeconds * 1f) * -275f,
//                Y = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 1.15f) * -250f,
//                Z = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 0.75) * -290f,
//            };

//            (_particleEffect.Emitters[2].Controllers[0] as TriggerOffsetController).TriggerOffset = new Vector3
//            {
//                X = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 0.75f) * 275f,
//                Y = (float)Math.Cos(gameTime.TotalGameTime.TotalSeconds * 1f) * -250f,
//                Z = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 1.15f) * -290f,
//            };

//            Vector3 triggerPosition = Vector3.Zero;

//            //var frustum = new BoundingFrustum(((TestApp)Game).View * ((TestApp)Game).Projection);
//            _particleEffect.Trigger(ref triggerPosition);
//            _proxy1.Trigger();
//            _proxy2.Trigger();
//            _particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

//            Matrix rotateInstances1;
//            Matrix.CreateRotationY(.01f, out rotateInstances1);
//            Matrix.Multiply(ref _proxy2.World, ref rotateInstances1, out _proxy2.World);

//            Matrix rotateInstance2;
//            Matrix.CreateRotationX(.1f, out rotateInstance2);
//            Matrix.Multiply(ref _proxy1.World, ref rotateInstance2, out _proxy1.World);

//            base.Update(gameTime);
//        }
        
//        /// <summary>
//        /// This is called when the game should draw itself.
//        /// </summary>
//        /// <param name="gameTime">Provides a snapshot of timing values.</param>
//        protected override void Draw(GameTime gameTime)
//        {
//            base.Draw(gameTime);
//        }
               
        
//    }
//}