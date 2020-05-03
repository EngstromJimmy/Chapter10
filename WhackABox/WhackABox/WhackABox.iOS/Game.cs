using System.Linq;
using ARKit;
using Urho;
using Urho.iOS;
namespace WhackABox.iOS
{
    public class Game : WhackABox.Game
    {
        protected ARKitComponent arkitComponent;
        public Game(ApplicationOptions options) : base(options)
        { }
    }
}