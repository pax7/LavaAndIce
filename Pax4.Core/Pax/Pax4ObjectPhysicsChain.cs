using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pax4.JigLibX.Physics;
using Pax4.JigLibX.Collision;
using Pax4.JigLibX.Geometry;
using Pax4.JigLibX.Math;

namespace Pax4.Core
{
    class Pax4Chain
    {
        private List<Pax4ChainLink> _chainLink = null;

        public Pax4Chain(int p_linkCount, String p_modelName, Vector3 p_startPosition, Vector3 p_direction, float p_scale = 1.0f, bool p_startPositionFixed = true, bool p_endPositionFixed = false)
        {
            _chainLink = new List<Pax4ChainLink>();

            Pax4ChainLink chainLink = null;
            for (int i = 0; i < p_linkCount; i++)
            {
                chainLink = new Pax4ChainLink(p_startPosition + p_direction * (float)i * p_scale, Vector3.Zero, Vector3.One * p_scale, true);
                chainLink.LoadModel(p_modelName);

                if (i == 0)
                    chainLink._body.Immovable = p_startPositionFixed;
                if (i == p_linkCount - 1)
                    chainLink._body.Immovable = p_endPositionFixed;

                _chainLink.Add(chainLink);

                if (i > 0)
                {
                    _chainLink[i - 1].CreateHingeJoint(_chainLink[i],
                                      Vector3.Backward,
                                      p_direction * p_scale / 2.0f,//new Vector3(0.0f, -boxObject._scale.Y/2.0f, 0.0f),
                                      0.5f,
                                      90.0f,
                                      90.0f,
                                      0.0f,
                                      1.0f,
                                      true);

                    _chainLink[i]._parentChainLink = _chainLink[i - 1];
                }
            }
        }

        public void EnableComponent(bool p_enableComponent = true)
        {
            for (int i = 0; i < _chainLink.Count; i++)
            {
                if (p_enableComponent)
                    _chainLink[i].EnableComponent();
                else
                    _chainLink[i].DisableComponent();
            }
        }
    }
}
