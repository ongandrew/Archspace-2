using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class PlayerSettings
    {
        public PlayerSettings()
        {
        }

        public int StartingAdmiralCount { get; set; }

        public static PlayerSettings CreateDefault()
        {
            PlayerSettings result = new PlayerSettings();

            result.StartingAdmiralCount = 5;

            return result;
        }
    }
}
