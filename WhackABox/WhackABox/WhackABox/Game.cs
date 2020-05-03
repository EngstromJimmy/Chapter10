using System;
using System.Linq;
using Urho;
using Urho.Shapes;
namespace WhackABox
{
    public abstract class Game : Application
    {
        private Scene scene;
        public Game(ApplicationOptions options) : base(options)
        {
        }

        protected abstract void InitializeAR();
    }
}