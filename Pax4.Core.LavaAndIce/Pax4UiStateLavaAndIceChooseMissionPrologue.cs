using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiStateLavaAndIceChooseMissionPrologue))]
    public class Pax4UiStateLavaAndIceChooseMissionPrologue : Pax4UiStateLavaAndIceChooseMission
    {
        public Pax4UiStateLavaAndIceChooseMissionPrologue(String p_name, Pax4Ui p_ui)
            : base(p_name, p_ui)
        {
            Vector2 position;
            float duration = 0.5f;
            float delay = 0.0f;

            Pax4SpriteColorModifier colorModifierEnter = new Pax4SpriteColorModifier("", null);
            colorModifierEnter.Ini(Color.Black, Color.White, duration);
            AddStateEnterModifier(colorModifierEnter);
            Pax4SpriteColorModifier colorModifierExit = new Pax4SpriteColorModifier("", null);
            colorModifierExit.Ini(Color.White, Color.Black, duration);
            AddStateExitModifier(colorModifierExit);

            Pax4SpriteAlphaModifier alphaModifierEnter = new Pax4SpriteAlphaModifier("", null);
            alphaModifierEnter.Ini(0.0f, 1.0f, duration);
            AddStateEnterModifier(alphaModifierEnter);
            Pax4SpriteAlphaModifier alphaModifierExit = new Pax4SpriteAlphaModifier("", null);
            alphaModifierExit.Ini(1.0f, 0.0f, duration);
            AddStateExitModifier(alphaModifierExit);

            duration = Pax4Ui._btnDuration;

            Pax4SpritePositionModifier positionModifierEnter = null;
            Pax4SpritePositionModifier positionModifierExit = null;
            Pax4SpriteTextModifier textModifier = null;
            //Pax4UiState state = null;
            Vector2 position0 = Vector2.Zero;
            Vector2 position1 = Vector2.Zero;

            String textureName = null;
            Texture2D texture = null;
            Texture2D textureOver = null;
            //SpriteFont spriteFont = null;
            Pax4Sprite sprite = null;


            textureName = "Sprite/lavaandiceMainBg";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("lavaandiceMainBg", null);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            position.X = 0.5f;
            position.Y = 0.5f;
            sprite.SetPosition(position);
            AddChild(sprite);
            duration = 0.5f;
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);


            delay = 0.3f;
            position.X = .5f;
            position.Y = .35f;

            sprite = new Pax4SpriteText("lavaandicePrologueTitle", sprite);
            AddChild(sprite);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/Livingstone");
            ((Pax4SpriteText)sprite).SetPosition(position);
            textModifier = new Pax4SpriteTextModifier("", null);
            textModifier.AddChild(sprite);
            textModifier.Ini("Prologue", duration, delay);
            AddStateEnterModifier(textModifier);
            duration = 0.5f;
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            position.X = .5f;
            position.Y = .85f;
            //misc
            sprite = new Pax4Button("lavaandiceBackBtn", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceBackBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTexture(texture);
            textureName = "Sprite/lavaandiceBackBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetTextSpriteFont("SpriteFont/Livingstone", position, "Back");
            ((Pax4Button)sprite).SetOnClick(lavaandiceBackBtn_Click);



            

            //**************************************************
            //create
            //**************************************************
            textureName = "Sprite/lavaandiceMissionBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            textureName = "Sprite/lavaandiceMissionBtnOver";
            textureOver = Pax4Texture2D._current.Get(textureName);

            position.X = .5f;
            position.Y = .5f;
            Pax4Slider slider = new Pax4Slider("sldQuest", null);
            slider.SetRectangleWidthHeight(texture.Width,0);
            slider.SetPosition(position);//, texture.Width, 400.0f);
            slider._verticalScroll = true;
            slider.SetViewingThreshold(0, Pax4Game._graphicsDeviceManager.PreferredBackBufferWidth, 400, 900);
            
            slider._verticalSpacing = 20.0f;

            //slider.SetSnapPosition(position);
            AddChild(slider);

            //colorModifier = new Pax4SpriteColorModifier(slider);
            //colorModifier.Ini(Color.Black, Color.White, duration);
            //AddStateEnterModifier(colorModifier);
            //colorModifier = new Pax4SpriteColorModifier(slider);
            //colorModifier.Ini(Color.White, Color.Black, duration);
            //AddStateExitModifier(colorModifier);

            duration = 0.5f;
            delay = 0.0f;
            //position0.X = -1091.0f;
            //position0.Y = position.Y;
            //position0 *= Pax4Camera._current._scale2;
            //position *= Pax4Camera._current._scale2;
            
            //positionModifierEnter = new Pax4SpritePositionModifier(slider);
            //positionModifierEnter.Ini(position0, position, duration, delay);
            //AddStateEnterModifier(positionModifierEnter);
            //positionModifierExit = new Pax4SpritePositionModifier(slider);
            //position0.X = 1091.0f;
            ///positionModifierExit.Ini(position, position0, duration, delay);
            //AddStateExitModifier(positionModifierExit);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission0Btn", slider, "Prologue", 1);
            ((Pax4Button)sprite).SetText("Mission 0",Vector2.Zero);
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            sprite.SetPosition(Vector2.Zero);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission0Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission1Btn", slider, "Prologue", 1);
            ((Pax4Button)sprite).SetText("Mission 1");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission1Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission2Btn", slider, "Prologue", 2);
            ((Pax4Button)sprite).SetText("Mission 2");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission2Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission3Btn", slider, "Prologue", 3);
            ((Pax4Button)sprite).SetText("Mission 3");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission3Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission4Btn", slider, "Prologue", 4);
            ((Pax4Button)sprite).SetText("Mission 4");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission4Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission5Btn", slider, "Prologue", 5);
            ((Pax4Button)sprite).SetText("Mission 5");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission5Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission6Btn", slider, "Prologue", 6);
            ((Pax4Button)sprite).SetText("Mission 6");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission6Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission7Btn", slider, "Prologue", 7);
            ((Pax4Button)sprite).SetText("Mission 7");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission7Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission8Btn", slider, "Prologue", 8);
            ((Pax4Button)sprite).SetText("Mission 8");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission8Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission9Btn", slider, "Prologue", 9);
            ((Pax4Button)sprite).SetText("Mission 9");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission9Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission10Btn", slider, "Prologue", 10);
            ((Pax4Button)sprite).SetText("Mission 10");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission10Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission11Btn", slider, "Prologue", 11);
            ((Pax4Button)sprite).SetText("Mission 11");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission11Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission12Btn", slider, "Prologue", 12);
            ((Pax4Button)sprite).SetText("Mission 12");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission12Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission13Btn", slider, "Prologue", 13);
            ((Pax4Button)sprite).SetText("Mission 13");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission13Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission14Btn", slider, "Prologue", 14);
            ((Pax4Button)sprite).SetText("Mission 14");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission14Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission15Btn", slider, "Prologue", 15);
            ((Pax4Button)sprite).SetText("Mission 15");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission15Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission16Btn", slider, "Prologue", 16);
            ((Pax4Button)sprite).SetText("Mission 16");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission16Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission17Btn", slider, "Prologue", 17);
            ((Pax4Button)sprite).SetText("Mission 17");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission17Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission18Btn", slider, "Prologue", 18);
            ((Pax4Button)sprite).SetText("Mission 18");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission18Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission19Btn", slider, "Prologue", 19);
            ((Pax4Button)sprite).SetText("Mission 19");
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission19Btn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);


            /////////////////////////////

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission20Btn", slider, "Prologue", 20);
            //((Pax4Button)sprite).SetText("Mission 20");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission20Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission21Btn", slider, "Prologue", 21);
            //((Pax4Button)sprite).SetText("Mission 21");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission21Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission22Btn", slider, "Prologue", 22);
            //((Pax4Button)sprite).SetText("Mission 22");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission22Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission23Btn", slider, "Prologue", 23);
            //((Pax4Button)sprite).SetText("Mission 23");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission23Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission24Btn", slider, "Prologue", 24);
            //((Pax4Button)sprite).SetText("Mission 24");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission24Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission25Btn", slider, "Prologue", 25);
            //((Pax4Button)sprite).SetText("Mission 25");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission25Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission26Btn", slider, "Prologue", 26);
            //((Pax4Button)sprite).SetText("Mission 26");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission26Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission27Btn", slider, "Prologue", 27);
            //((Pax4Button)sprite).SetText("Mission 27");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission27Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission28Btn", slider, "Prologue", 28);
            //((Pax4Button)sprite).SetText("Mission 28");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission28Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission29Btn", slider, "Prologue", 29);
            //((Pax4Button)sprite).SetText("Mission 29");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission29Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission30Btn", slider, "Prologue", 30);
            //((Pax4Button)sprite).SetText("Mission 30");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission30Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission31Btn", slider, "Prologue", 31);
            //((Pax4Button)sprite).SetText("Mission 31");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission31Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //sprite = new Pax4ButtonLavaAndIceMission("lavaandicePrologueMission32Btn", slider, "Prologue", 32);
            //((Pax4Button)sprite).SetText("Mission 32");
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceMission)sprite).SetOnClick1(this.lavaandicePrologueMission32Btn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);


            slider.AddChild(null);//no mas buttons, wrap shit up

            position.X = .5f;
            position.Y = .5f;

            sprite.Enable();

            ////duration = 0.5f;
            ////delay = 0.0f;
            ////position0.X = position.X;
            ////position0.Y = 1920.0f;
            ////sprite.SetPosition(position0);
            ////position *= Pax4Camera._current._scale2;
            ////position0 *= Pax4Camera._current._scale2;
            ////positionModifierEnter = new Pax4SpritePositionModifier(sprite);
            ////positionModifierEnter.Ini(position0, position, duration);
            ////AddStateEnterModifier(positionModifierEnter);

            ////positionModifierExit = new Pax4SpritePositionModifier(sprite);
            ////positionModifierExit.Ini(position, position0, duration);
            ////AddStateExitModifier(positionModifierExit);


            //colorModifier = new Pax4SpriteColorModifier(sprite);
            //colorModifier.Ini(Color.Black, Color.White, duration/2.0f);
            //AddStateEnterModifier(colorModifier);

            //colorModifier = new Pax4SpriteColorModifier(sprite);
            //colorModifier.Ini(Color.White, Color.Black, duration / 2.0f);
            //AddStateExitModifier(colorModifier);
        }

        private void lavaandicePrologueMission0Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 0;
        }

        private void lavaandicePrologueMission1Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 1;
        }

        private void lavaandicePrologueMission2Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 2;
        }

        private void lavaandicePrologueMission3Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 3;
        }

        private void lavaandicePrologueMission4Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 4;
        }

        private void lavaandicePrologueMission5Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 5;
        }

        private void lavaandicePrologueMission6Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 6;
        }

        private void lavaandicePrologueMission7Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 7;
        }

        private void lavaandicePrologueMission8Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 8;
        }

        private void lavaandicePrologueMission9Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 9;
        }

        private void lavaandicePrologueMission10Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 10;
        }

        private void lavaandicePrologueMission11Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 11;
        }

        private void lavaandicePrologueMission12Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 12;
        }

        private void lavaandicePrologueMission13Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 13;
        }

        private void lavaandicePrologueMission14Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 14;
        }

        private void lavaandicePrologueMission15Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 15;
        }

        private void lavaandicePrologueMission16Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 16;
        }

        private void lavaandicePrologueMission17Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 17;
        }

        private void lavaandicePrologueMission18Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 18;
        }

        private void lavaandicePrologueMission19Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 19;
        }

        private void lavaandicePrologueMission20Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 20;
        }

        private void lavaandicePrologueMission21Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 21;
        }

        private void lavaandicePrologueMission22Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 22;
        }

        private void lavaandicePrologueMission23Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 23;
        }

        private void lavaandicePrologueMission24Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 24;
        }

        private void lavaandicePrologueMission25Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 25;
        }

        private void lavaandicePrologueMission26Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 26;
        }

        private void lavaandicePrologueMission27Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 27;
        }

        private void lavaandicePrologueMission28Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 28;
        }

        private void lavaandicePrologueMission29Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 29;
        }

        private void lavaandicePrologueMission30Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 30;
        }

        private void lavaandicePrologueMission31Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 31;
        }

        private void lavaandicePrologueMission32Btn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("difficulty");

            Pax4WorldLavaAndIce._missionIndex = 32;
        }


        public override void Enter()
        {
            base.Enter();

            Pax4UiStateLavaAndIceChooseMission._currentMissionState = this;
        }
    }
}