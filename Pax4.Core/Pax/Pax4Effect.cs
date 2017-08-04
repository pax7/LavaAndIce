using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using CpuSkinningDataTypes;
using System.IO;
using Pax.Core;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4EffectState))]
    public class Pax4EffectState : PaxState
    {
        [IgnoreDataMember]
        public Effect _effect = null;

        public Pax4EffectState(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }

    [DataContract]
    [KnownType(typeof(Pax4Effect))]
    public class Pax4Effect : PaxState
    {
        [IgnoreDataMember]
        public static Pax4Effect _current = null;        

        public Pax4Effect(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            _current = this;
        }

        public void Load(String p_effect)
        {
            if (GetChild().ContainsKey(p_effect))
                return;

            BinaryReader reader = null;

            Pax4EffectState effectState = new Pax4EffectState(p_effect, this);            

            reader = new BinaryReader(File.Open(p_effect, FileMode.Open));
            effectState._effect = new Effect(Pax4Game._graphicsDeviceManager.GraphicsDevice, reader.ReadBytes((int)reader.BaseStream.Length));
        }

        public void Load(List<String> p_effect)
        {
            if (p_effect == null)
                return;

            BinaryReader reader = null;
            String effectName = null;
            Pax4EffectState effectState = null;

            for (int i = 0; i < p_effect.Count; i++)
            {
                effectName = p_effect[i];
                if (GetChild().ContainsKey(effectName))
                    continue;

                effectState = new Pax4EffectState(effectName, this);

                reader = new BinaryReader(File.Open("Content/" + effectName, FileMode.Open));
                effectState._effect = new Effect(Pax4Game._graphicsDeviceManager.GraphicsDevice, reader.ReadBytes((int)reader.BaseStream.Length));
            }
        }
       
        public override void Dx()
        {
            if (this == _current)
                _current = null;

            base.Dx();
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}