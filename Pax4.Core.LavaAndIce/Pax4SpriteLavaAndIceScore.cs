using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pax.Core;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiLavaAndIceQuestScore))]
    class Pax4UiLavaAndIceQuestScore
    {
        public static Pax4UiLavaAndIceQuestScore _currentScore = null;

        public static Dictionary<String, int> _score = new Dictionary<String, int>();
#if WINDOWS
        public const String _filePath = "lavaandiceScores";
#else
        public const String _filePath = "/sdcard/lavaandiceScores";
#endif

        public Pax4UiLavaAndIceQuestScore()
        {
            _currentScore = this;

            if (!File.Exists(_filePath))
            {
                IniScore();
                Write();
            }
            else
            {
                Read();
            }
        }

        public void Read()
        {
            _score.Clear();

            StreamReader sr = null;
            sr = System.IO.File.OpenText(_filePath);

            String line = null;

            char[] separator = "=".ToCharArray();

            String[] keyValue = null;// p_intentScriptFilePath.Split(sep);

            String key = null;
            int value = 0;
            int maxLineCount = 16*32*5;
            int lineCount = -1;
            for (line = sr.ReadLine(); line != null; line = sr.ReadLine())
            {
                lineCount++;
                if (lineCount >= maxLineCount)
                    break;

                line = line.TrimEnd('\n');                
                PaxTools.Decode64(line, out line);                
                keyValue = line.Split(separator);

                key = keyValue[0];

                try
                {
                    if (!int.TryParse(keyValue[1], out value))
                        value = 0;
                }
                catch(Exception ex)
                {
                    continue;
                }

                _score.Add(key, value);
            }

            sr.Close();
            sr = null;
        }

        public void Write()
        {
            FileStream fs = null;

            String buff = null;
            byte[] bbuff = null;

            try
            {
                fs = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Write);

                foreach (KeyValuePair<String, int> kvp in _score)
                {
                    PaxTools.Encode64(kvp.Key + "=" + kvp.Value, out buff);
                    buff += "\n";
                    bbuff = new System.Text.UTF8Encoding(true).GetBytes(buff);
                    fs.Write(bbuff, 0, bbuff.Length);
                }

                fs.Flush();
                fs.Close();
                fs = null;
            }
            catch (Exception ex)
            {
            }
        }

        public void IniScore()
        {
            List<String> quest = new List<String>();

            quest.Add("Prologue");
            quest.Add("Equilibrium");
            quest.Add("LavaGrail");
            quest.Add("IceGrail");
            quest.Add("Dragons");

            int score = 0;

            foreach (String questName in quest)
            {
                _score.Add(questName + "_NightmareMedalCount", score);
                _score.Add(questName + "_HardMedalCount", score);
                _score.Add(questName + "_NormalMedalCount", score);
                _score.Add(questName + "_EasyMedalCount", score);

                _score.Add(questName + "_TotalScore", score);
                _score.Add(questName + "_LastScore", score);

                _score.Add(questName + "_LavaKills", score);
                _score.Add(questName + "_IceKills", score);
                _score.Add(questName + "_MonsterKills", score);

                for (int j = 0; j < Pax4WorldLavaAndIce._maxMissions; j++)
                {
                    if (j == 0)
                        _score.Add(questName + "_" + (j + 1).ToString() + "_Locked", 0);
                    else
                        _score.Add(questName + "_" + (j + 1).ToString() + "_Locked", 1);

                    _score.Add(questName + "_" + (j + 1).ToString() + "_HighScore", score);
                    _score.Add(questName + "_" + (j + 1).ToString() + "_LastScore", score);

                    _score.Add(questName + "_" + (j + 1).ToString() + "_NightmareMedalCount", score);
                    _score.Add(questName + "_" + (j + 1).ToString() + "_HardMedalCount", score);
                    _score.Add(questName + "_" + (j + 1).ToString() + "_NormalMedalCount", score);
                    _score.Add(questName + "_" + (j + 1).ToString() + "_EasyMedalCount", score);
                }
            }
        }
    }
}