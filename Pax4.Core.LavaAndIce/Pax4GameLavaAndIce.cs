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
using System.Runtime.Serialization;

namespace Pax4.Core
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    [DataContract]
    [KnownType(typeof(Pax4GameLavaAndIce))]
    public class Pax4GameLavaAndIce : Pax4Game
    {
        public Pax4GameLavaAndIce()
            : base()
        {
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
            base.Initialize();

#if WINDOWS
            _graphicsDeviceManager.PreferredBackBufferWidth = 512;
            _graphicsDeviceManager.PreferredBackBufferHeight = 910;
            Pax4Camera._scale0 = new Vector3(_graphicsDeviceManager.PreferredBackBufferWidth, 
                                             _graphicsDeviceManager.PreferredBackBufferHeight, 
                                             0.0f);
#endif
            Pax4Camera camera = new Pax4Camera("camera", null);
            camera.SetOrientation();
            camera.SetCull(CullMode.CullCounterClockwiseFace);
            camera.SetPosition(new Vector3(0.0f, 0.0f, 40.0f));
            camera.Ini();

            Pax4Camera._current.SetImmovable();

            Pax4ParticleEffectLavaAndIce particleEffect = new Pax4ParticleEffectLavaAndIce();

            if (Pax4ParticleEffect._current != null)
            {
                _touchParticleEffect = new Pax4ParticleEffectPart("_touchParticleEffect", null);
                _touchParticleEffect.Ini(Pax4ParticleEffect._current._particleEffectIceStarTrail);
                _touchParticleEffect.Enable();
            }

            List<String> list = new List<String>();

            //******************
            //sprite************
            //******************

            list.Add("Sprite/lavaandiceBackBtn");
            list.Add("Sprite/lavaandiceBackBtnOver");
            list.Add("Sprite/lavaandiceContinueBtn");
            list.Add("Sprite/lavaandiceContinueBtnOver");
            list.Add("Sprite/lavaandiceEasyBtn");
            list.Add("Sprite/lavaandiceEasyBtnOver");
            list.Add("Sprite/lavaandiceEasyOff");
            list.Add("Sprite/lavaandiceEasyOn");
            list.Add("Sprite/lavaandiceExitBtn");
            list.Add("Sprite/lavaandiceExitBtnOver");
            list.Add("Sprite/lavaandiceHardBtn");
            list.Add("Sprite/lavaandiceHardBtnOver");
            list.Add("Sprite/lavaandiceHardOff");
            list.Add("Sprite/lavaandiceHardOn");

            list.Add("Sprite/lavaandiceIceHealth0");
            list.Add("Sprite/lavaandiceIceHealth1");
            list.Add("Sprite/lavaandiceIceHealth2");
            list.Add("Sprite/lavaandiceIceHealth3");

            list.Add("Sprite/lavaandiceInGameFgBottom");
            
            list.Add("Sprite/lavaandiceInGameFgIceLauncher");
            list.Add("Sprite/lavaandiceInGameFgIceLauncherOver");
            list.Add("Sprite/lavaandiceInGameFgLavaLauncher");
            list.Add("Sprite/lavaandiceInGameFgLavaLauncherOver");            
            list.Add("Sprite/lavaandiceInGameFgScore");            
            list.Add("Sprite/lavaandiceInGameFgTimer");
            list.Add("Sprite/lavaandiceInGameFgTimerRed");
            list.Add("Sprite/lavaandiceInGameFgTimerYellow");
            list.Add("Sprite/lavaandiceInGameMenuBg");
            list.Add("Sprite/lavaandiceInGameMenuBtn");
            list.Add("Sprite/lavaandiceInGameMenuBtnOver");
            list.Add("Sprite/lavaandiceInGameMenuTextDefeat");
            list.Add("Sprite/lavaandiceInGameMenuTextDifficulty");
            list.Add("Sprite/lavaandiceInGameMenuTextMenu");
            list.Add("Sprite/lavaandiceInGamePauseBtn");
            list.Add("Sprite/lavaandiceInGamePauseBtnOver");
            list.Add("Sprite/lavaandiceInGameVictoryBg");
            list.Add("Sprite/lavaandiceInstructions");

            list.Add("Sprite/lavaandiceLavaHealth0");
            list.Add("Sprite/lavaandiceLavaHealth1");
            list.Add("Sprite/lavaandiceLavaHealth2");
            list.Add("Sprite/lavaandiceLavaHealth3");

            list.Add("Sprite/lavaandiceMainBg");
            //list.Add("Sprite/lavaandiceMainBgTop");
            //list.Add("Sprite/lavaandiceMainBgMiddle");
            //list.Add("Sprite/lavaandiceMainBgBottom");

            list.Add("Sprite/lavaandiceMissionBtn");
            list.Add("Sprite/lavaandiceMissionBtnOver");
            list.Add("Sprite/lavaandiceMissionLockedBtn");
            list.Add("Sprite/lavaandiceNightmareBtn");
            list.Add("Sprite/lavaandiceNightmareBtnOver");
            list.Add("Sprite/lavaandiceNightmareOff");
            list.Add("Sprite/lavaandiceNightmareOn");
            list.Add("Sprite/lavaandiceNormalBtn");
            list.Add("Sprite/lavaandiceNormalBtnOver");
            list.Add("Sprite/lavaandiceNormalOff");
            list.Add("Sprite/lavaandiceNormalOn");
            list.Add("Sprite/lavaandiceQuestBtn");
            list.Add("Sprite/lavaandiceQuestBtnOver");
            list.Add("Sprite/lavaandiceQuestComingSoonBtn");
            list.Add("Sprite/lavaandiceResumeBtn");
            list.Add("Sprite/lavaandiceResumeBtnOver");
            list.Add("Sprite/lavaandiceRetryBtn");
            list.Add("Sprite/lavaandiceRetryBtnOver");
            list.Add("Sprite/lavaandiceSettingsBtn");
            list.Add("Sprite/lavaandiceSettingsBtnOver");

            list.Add("Sprite/lavaandiceTemperature0.10");
            list.Add("Sprite/lavaandiceTemperature0.20");
            list.Add("Sprite/lavaandiceTemperature0.30");
            list.Add("Sprite/lavaandiceTemperature0.40");
            list.Add("Sprite/lavaandiceTemperature0.45");
            list.Add("Sprite/lavaandiceTemperature0.50");
            list.Add("Sprite/lavaandiceTemperature0.55");
            list.Add("Sprite/lavaandiceTemperature0.60");
            list.Add("Sprite/lavaandiceTemperature0.70");
            list.Add("Sprite/lavaandiceTemperature0.80");
            list.Add("Sprite/lavaandiceTemperature0.90");

            Pax4Texture2D._current.Load(list);
            list.Clear();

            //******************
            //spritefont********
            //******************
            list.Add("SpriteFont/ArialBold");
            list.Add("SpriteFont/Livingstone");
            Pax4SpriteFont._current.Load(list);
            list.Clear();

            list = null;

            //******************
            //sound*************
            //******************
            Pax4SoundLavaAndIce sound = new Pax4SoundLavaAndIce("",null);

            //******************
            //ui****************
            //******************
            Pax4UiLavaAndIce ui = new Pax4UiLavaAndIce("Pax4UiLavaAndIce", Pax.Core.PaxState._root);// Pax4UiLavaAndIce();//new Pax4Ui();
            ui.Create();

            Pax4UiLavaAndIce uii = null;
            //Pax4UiLavaAndIce.Deserialize(ui.Serialize());
        }
        
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            Pax4UiLavaAndIceQuestScore._currentScore.Write();
        }
    }
}