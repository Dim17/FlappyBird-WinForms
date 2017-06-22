using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdGame
{
    class Pipe
    {

        public List<int> pipe1 = new List<int>();
        public List<int> pipe2 = new List<int>();
        int pipeWidth = 55;
        int pipeDifferentY = 140;
        int pipeDifferentX = 230;

        public int PipeWidth
        {
            get { return pipeWidth; }
        }
        public int PipeDifferentY
        {
            get { return pipeDifferentY; }
        }
        public int PipeDifferentX
        {
            get
            {
                return pipeDifferentX;
            }
        }
      
    }
}
