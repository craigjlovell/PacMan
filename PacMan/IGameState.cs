using System;
using System.Collections.Generic;
using System.Text;

namespace PacMan
{
    class IGameState
    {
        protected Program program;

        public IGameState(Program program)
        {
            this.program = program;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }

    }
}
