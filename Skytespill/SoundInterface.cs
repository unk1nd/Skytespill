using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;

namespace Skytespill
{
    class SoundInterface : Form1
    {
        SoundPlayer bossmusic = new SoundPlayer(global::Skytespill.Properties.Resources.big_boss_2_0);
        public SoundInterface()
        {
            bossmusic.PlayLooping();
        }
    }
}
