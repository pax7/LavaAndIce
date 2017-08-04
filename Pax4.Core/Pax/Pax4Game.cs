using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Pax4.ProjectMercury;
using Pax4.ProjectMercury.Emitters;
using Pax4.ProjectMercury.Modifiers;
using Pax4.ProjectMercury.Renderers;
using Pax4.ProjectMercury.Controllers;
using Pax.Core;
using System.Reflection;
using System.Json;

namespace Pax4.Core
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Pax4Game : Microsoft.Xna.Framework.Game
    {
        public static Pax4Game _current = null;

        //graphics
        public static Matrix _matIdentity = Matrix.Identity;
        public static Vector3 _vecZero = Vector3.Zero;

        public static  GraphicsDeviceManager _graphicsDeviceManager = null;

        public static bool _pause = false;

        public static SpriteBatch _spriteBatch = null;

        public static Pax4ParticleEffectPart _touchParticleEffect = null;

        public bool _onResume = false;

        public Random _random = new Random();

        private Vector2 _preferredBackBufferVector = Vector2.Zero;
        private bool _isPreferredBackBufferVectorSet = false;
        public Vector2 GetPreferredBackBufferVector()
        {
            if (_isPreferredBackBufferVectorSet)
                return _preferredBackBufferVector; 

            _preferredBackBufferVector.X = _graphicsDeviceManager.PreferredBackBufferWidth;
            _preferredBackBufferVector.Y = _graphicsDeviceManager.PreferredBackBufferHeight;

            _isPreferredBackBufferVectorSet = true;

            return _preferredBackBufferVector;
        }
        
        public Pax4Game()
        {   
            _current = this;

            _graphicsDeviceManager = new GraphicsDeviceManager(this);

            this.IsFixedTimeStep = true;
            _graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;

#if !WINDOWS
            _graphicsDeviceManager.ToggleFullScreen();
#else
            _graphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
            _graphicsDeviceManager.PreferMultiSampling = true;
#endif


            //PaxState.IniDefaults(true);
            RegisterIntent();

            //PaxIntent intent0 = new PaxIntent();
            //intent0.SetPath(new String[] { "Foo" });
            //intent0.SetIntent("SyncGetIntent");           
            //intent0.SetReturnIntent();
            //PaxIntent intent0 = new PaxIntent();
            //intent0.SetPath(new String[] { "Foo" });
            //intent0.SetIntent("SyncGetIntent");
            //intent0.SetReturnIntent();

            ////PaxState._root.Sync("Foo", "localhost");
            //PaxState._root.Sync("localhost", "localhost");  
            //PaxState._root.Sync("Foo", "localhost");
            ////PaxState._root.Sync("Foo", "10.8.0.10");

            //PaxState syncClient = null;
            //PaxState._root.TryGetChild("Foo", out syncClient);
            //((PaxSyncClientState)syncClient).SetSyncDelegate(PaxState.SyncDelegateIntent);
            
            //PaxIntent.Enqueue(intent0);
            //PaxState syncClient = null;
            //PaxState._root.TryGetChild("Foo", out syncClient);
            //((PaxSyncClientState)syncClient).SetSyncDelegate(PaxState.SyncDelegateIntent);

            //PaxIntent intentResult = null;
            //PaxIntent.GetIntentResult(intent0, out intentResult);
            //PaxIntent.Enqueue(intent0);

            //PaxIntent intentResult = null;
            //PaxIntent.GetIntentResult(intent0, out intentResult);
        }
        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here            
            Content.RootDirectory = "Content";
            
            Pax4Camera camera = new Pax4Camera("camera", null);

            Pax4Sound sound = new Pax4Sound("sound", null);

            Pax4Touch touch = new Pax4Touch("touch", null);

            Pax4Effect effect = new Pax4Effect("effect", null);

            Pax4Model model = new Pax4Model("model", null);

            Pax4Texture2D texture2D = new Pax4Texture2D("texture2D", null);

            Pax4SpriteFont spriteFont = new Pax4SpriteFont("spriteFont", null);

            //Pax4ParticleEffect particleEffect = new Pax4ParticleEffect();
            //_particleEffect = Pax4ParticleEffect._current._particleEffectIceStarTrail;

            base.Initialize();

            //_graphicsDeviceManager.IsFullScreen = true;
            _graphicsDeviceManager.ApplyChanges();

#if WINDOWS
            this.IsMouseVisible = true;
#endif      
        }

        public virtual void RegisterIntent()
        {
            //PaxState.RegisterIntent();

            PaxState.RegisterIntent<Pax4Object>();
            PaxState.RegisterIntent<Pax4Sprite>();

            PaxState._newFactoryDelegate = NewFactory;
            PaxState._covertPropertyValueDelegate = ConvertPropertyValue;
        }

        public static bool NewFactory(PaxIntent p_intent, out PaxState p_state)
        {
            PaxState parent0 = null;
            if (!PaxState._root.TryFindChildState(p_intent, out parent0))
            {
                p_state = null;
                return false;
            }

            String name = null;
            String type = p_intent._argv["p_type"];

            switch (type)
            {
                case "Pax4Object":
                    name = p_intent._argv["p_name"];
                    p_state = new Pax4Object(name, parent0);
                    return true;

                case "Pax4Sprite":
                    name = p_intent._argv["p_name"];
                    p_state = new Pax4Sprite(name, parent0);
                    return true;

                case "Pax4Slider":
                    name = p_intent._argv["p_name"];
                    p_state = new Pax4Slider(name, parent0);
                    return true;

                default:
                    return PaxState.NewFactory(p_intent, out p_state);
            }
        }

        public static bool ConvertPropertyValue(PaxState p_state, FieldInfo p_fieldInfo, String p_result)
        {
            if (p_state == null || p_result == null)
                return false;

            try
            {
                String propertyInfo = p_fieldInfo.FieldType.ToString();
                switch (propertyInfo)
                {
                    case "Microsoft.Xna.Framework.Vector2":
                        Vector2 v2Result;
                        if (PaxTools.Deserialize<Vector2>(p_result, out v2Result))
                            p_fieldInfo.SetValue(p_state, v2Result);
                        return true;
                    case "Microsoft.Xna.Framework.Rectangle":
                        Rectangle rectResult;
                        if (PaxTools.Deserialize<Rectangle>(p_result, out rectResult))
                            p_fieldInfo.SetValue(p_state, rectResult);
                        return true;
                    case "Microsoft.Xna.Framework.Vector3":
                        Vector3 v3Result;
                        if (PaxTools.Deserialize<Vector3>(p_result, out v3Result))
                            p_fieldInfo.SetValue(p_state, v3Result);
                        return true;
                    case "Microsoft.Xna.Framework.Vector4":
                        Vector4 v4Result;
                        if (PaxTools.Deserialize<Vector4>(p_result, out v4Result))
                            p_fieldInfo.SetValue(p_state, v4Result);
                        return true;
                    case "Microsoft.Xna.Framework.Matrix":
                        Matrix matResult;
                        if (PaxTools.Deserialize<Matrix>(p_result, out matResult))
                            p_fieldInfo.SetValue(p_state, matResult);
                        return true;
                    case "Microsoft.Xna.Framework.Quaternion":
                        Quaternion quatResult;
                        if (PaxTools.Deserialize<Quaternion>(p_result, out quatResult))
                            p_fieldInfo.SetValue(p_state, quatResult);
                        return true;
                    case "Microsoft.Xna.Framework.Color":
                        Color cResult;
                        if (PaxTools.Deserialize<Color>(p_result, out cResult))
                            p_fieldInfo.SetValue(p_state, cResult);
                        return true;
                    default:
                        return PaxState.ConvertPropertyValue(p_state, p_fieldInfo, p_result);
                }
            }
            catch (Exception ex)
            {
#if WINDOWS
                Console.WriteLine(ex.ToString());
#endif
            }
            return false;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {   
            // TODO: use this.Content to load your game content here
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>        
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            
#if WINDOWS
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
#endif
            //update input
            if (Pax4Touch._current != null)
                Pax4Touch._current.Update(gameTime);
#if !WINDOWS
            if (Pax4Touch._current._currentTouchState._oneTouch && _touchParticleEffect != null)
            {
#else
            if (Pax4Touch._current._currentTouchState._leftDown && _touchParticleEffect != null)
            {
#endif
                _touchParticleEffect.Trigger(ref Pax4Touch._current._currentTouchState._xy, true);
            }
            
            if (Pax4Ui._current != null)
                Pax4Ui._current.Update(gameTime);            

            //update current camera
            if (Pax4Camera._current != null)
                Pax4Camera._current.Update(gameTime);

            if (Pax4Sound._current != null)
                Pax4Sound._current.Update(gameTime);
            
            if (_pause)
                return; 

            if (Pax4World._current != null)
                Pax4World._current.Update(gameTime);
            
            if (Pax4ParticleEffect._current != null)
                Pax4ParticleEffect._current.Update(gameTime);

            if (_onResume)
            {
                if (Pax4Model._current != null)
                    Pax4Model._current.SetDefaultParameters();

                _onResume = false;
            }

            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

#if !WINDOWS
            _graphicsDeviceManager.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer | ClearOptions.Stencil, Color.Black, 1.0f, 0);
#else            
            _graphicsDeviceManager.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
#endif
            if (Pax4World._current != null)
                Pax4World._current.Draw(gameTime);

            if (Pax4ParticleEffect._current != null)
                Pax4ParticleEffect._current.Draw3(gameTime);

            Pax4Game._spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, Pax4Camera._rasterizerState);
            
            if (Pax4Ui._current != null)
                Pax4Ui._current.Draw(gameTime);

            Pax4Game._spriteBatch.End();

            if (Pax4ParticleEffect._current != null)
                Pax4ParticleEffect._current.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void SetOnResume()
        {
            _onResume = true;
        }

        public new void Exit()
        {
            base.Exit();
        }
    }
}