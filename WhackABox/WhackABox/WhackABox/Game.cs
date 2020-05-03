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

        protected void CreateSubPlane(PlaneNode planeNode)
        {
            var node = planeNode.CreateChild("subplane");
            node.Position = new Vector3(0, 0.05f, 0);
            var box = node.CreateComponent<Box>();
            box.Color = Color.FromHex("#22ff0000");
        }
        protected void UpdateSubPlane(PlaneNode planeNode, Vector3 position)
        {
            var subPlaneNode = planeNode.GetChild("subplane");
            subPlaneNode.Scale = new Vector3(planeNode.ExtentX, 0.05f,
            planeNode.ExtentZ);
            subPlaneNode.Position = position;
        }

        protected PlaneNode FindNodeByPlaneId(string planeId) =>scene.Children.OfType<PlaneNode>().FirstOrDefault(e => e.PlaneId == planeId);
    }
}