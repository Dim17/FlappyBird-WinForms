using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdGame
{
    class SoundsF
    {
        public static void Pipeplay()
        {
            System.Media.SoundPlayer player1 = new System.Media.SoundPlayer();
            player1.SoundLocation = "Sounds\\Flappyp.wav";
            player1.Play();
        }
        static public void Wingsplay()
        {
            System.Media.SoundPlayer player1 = new System.Media.SoundPlayer();
            player1.SoundLocation = "Sounds\\Flappy.wav";
            player1.Play();
        }
        static public void Dieplay ()
        {
            System.Media.SoundPlayer player1 = new System.Media.SoundPlayer();
            player1.SoundLocation = "Sounds\\Flappyd.wav";
            player1.Play();
        }
       
    }
}
