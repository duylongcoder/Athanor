using System;
using System.Globalization;
using System.Text;

namespace Athanor.Core
{
    /// <summary>
    /// Player settings: volumes, haptics and language. Unity-free; Game stores the JSON (PlayerPrefs, key athanor.settings.v1).
    /// Public fields so JsonUtility can read it too; ToJson/FromJson are hand-written so Core needs no serializer.
    /// </summary>
    [Serializable]
    public sealed class Settings
    {
        public const int MaxVolume = 10;

        /// <summary>For later migrations.</summary>
        public int version = 1;
        /// <summary>0-10, 0 = off.</summary>
        public int music = 7;
        /// <summary>0-10, 0 = off.</summary>
        public int sfx = 8;
        public bool haptics = true;
        /// <summary>"" (not chosen: use the system language), "en" or "vi".</summary>
        public string language = "";

        /// <summary>Forces every field into range. Returns this.</summary>
        public Settings Clamp()
        {
            music = Math.Max(0, Math.Min(MaxVolume, music));
            sfx = Math.Max(0, Math.Min(MaxVolume, sfx));
            if (language != "en" && language != "vi") language = "";
            return this;
        }

        /// <summary>Flat JSON object, same shape JsonUtility writes.</summary>
        public string ToJson()
        {
            var c = new Settings { version = version, music = music, sfx = sfx, haptics = haptics, language = language }.Clamp();
            var sb = new StringBuilder();
            sb.Append("{\"version\":").Append(c.version.ToString(CultureInfo.InvariantCulture));
            sb.Append(",\"music\":").Append(c.music.ToString(CultureInfo.InvariantCulture));
            sb.Append(",\"sfx\":").Append(c.sfx.ToString(CultureInfo.InvariantCulture));
            sb.Append(",\"haptics\":").Append(c.haptics ? "true" : "false");
            sb.Append(",\"language\":\"").Append(c.language).Append("\"}");
            return sb.ToString();
        }

        /// <summary>Reads settings; null, empty or unreadable text gives defaults. Missing fields keep defaults; values are clamped.</summary>
        public static Settings FromJson(string json)
        {
            var s = new Settings();
            if (string.IsNullOrWhiteSpace(json)) return s;
            // The reader is total: every read is bounds-checked and uses TryParse, so malformed text
            // returns false instead of throwing. No catch is needed (and none may swallow errors).
            var parsed = new Reader(json).ReadObject(s);
            return parsed ? s.Clamp() : new Settings();
        }

        sealed class Reader
        {
            readonly string t; int i;
            public Reader(string text) { t = text; }

            void Ws() { while (i < t.Length && char.IsWhiteSpace(t[i])) i++; }
            bool Eat(char c) { Ws(); if (i < t.Length && t[i] == c) { i++; return true; } return false; }

            public bool ReadObject(Settings s)
            {
                if (!Eat('{')) return false;
                if (Eat('}')) return true;
                while (true)
                {
                    var key = ReadString();
                    if (key == null || !Eat(':')) return false;
                    Ws();
                    if (!ReadValue(key, s)) return false;
                    if (Eat(',')) continue;
                    return Eat('}');
                }
            }

            bool ReadValue(string key, Settings s)
            {
                Ws();
                if (i >= t.Length) return false;
                char c = t[i];
                if (c == '"')
                {
                    var v = ReadString();
                    if (v == null) return false;
                    if (key == "language") s.language = v;
                    else if (key == "version" || key == "music" || key == "sfx" || key == "haptics") return false;
                    return true;
                }
                if (c == 't' || c == 'f')
                {
                    bool b = c == 't';
                    string word = b ? "true" : "false";
                    if (string.CompareOrdinal(t, i, word, 0, word.Length) != 0) return false;
                    i += word.Length;
                    if (key == "haptics") s.haptics = b;
                    else if (key == "version" || key == "music" || key == "sfx" || key == "language") return false;
                    return true;
                }
                int start = i;
                while (i < t.Length && (char.IsDigit(t[i]) || t[i] == '-' || t[i] == '+' || t[i] == '.' || t[i] == 'e' || t[i] == 'E')) i++;
                if (i == start) return false;
                if (!double.TryParse(t.Substring(start, i - start), NumberStyles.Float, CultureInfo.InvariantCulture, out var d)) return false;
                int n = (int)Math.Max(int.MinValue, Math.Min(int.MaxValue, Math.Round(d)));
                switch (key)
                {
                    case "version": s.version = n; break;
                    case "music": s.music = n; break;
                    case "sfx": s.sfx = n; break;
                    case "haptics": case "language": return false;
                }
                return true;
            }

            string ReadString()
            {
                if (!Eat('"')) return null;
                var sb = new StringBuilder();
                while (i < t.Length)
                {
                    char c = t[i++];
                    if (c == '"') return sb.ToString();
                    if (c != '\\') { sb.Append(c); continue; }
                    if (i >= t.Length) return null;
                    char e = t[i++];
                    switch (e)
                    {
                        case 'n': sb.Append('\n'); break;
                        case 't': sb.Append('\t'); break;
                        case 'r': sb.Append('\r'); break;
                        case 'b': sb.Append('\b'); break;
                        case 'f': sb.Append('\f'); break;
                        case 'u':
                            if (i + 4 > t.Length || !int.TryParse(t.Substring(i, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var cp)) return null;
                            sb.Append((char)cp); i += 4; break;
                        default: sb.Append(e); break;
                    }
                }
                return null;
            }
        }
    }
}
