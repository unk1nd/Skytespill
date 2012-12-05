using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;

namespace Skytespill
{
    /*
     *     Interface for all musikken i spillet.
     * 
     */
    class SoundInterface
    {
        SoundPlayer bossmusic = new SoundPlayer(global::Skytespill.Properties.Resources.big_boss_2_0);
        SoundPlayer menuTheme = new SoundPlayer(global::Skytespill.Properties.Resources.Bolt___Vodka_Polka);
        SoundPlayer creditsTheme = new SoundPlayer(global::Skytespill.Properties.Resources.Evan_LE_NY___Credits);
        SoundPlayer mainGameTheme = new SoundPlayer(global::Skytespill.Properties.Resources.Bruno_Belotti___Benvenuta_Estate_Mazurka_Short_Loop);
        public SoundInterface()
        {

        }

        public void PlayBossMusic() 
        {
            bossmusic.PlayLooping();
        }

        public void PlayMenuMusic()
        {
            menuTheme.PlayLooping();
        }

        public void PlayMainGameMusic()
        {
            mainGameTheme.PlayLooping();
        }

        public void PlayCreditsMusic()
        {
            creditsTheme.PlayLooping();
        }
    }
}
