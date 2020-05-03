using System;
using System.Linq;
using Urho;
using Urho.Shapes;
namespace WhackABox
{
    public abstract class Game : Application
    {
        protected Scene scene;
        private Camera camera;
        private Viewport viewport;


        public Game(ApplicationOptions options) : base(options)
        {
        }

        protected abstract void InitializeAR();

        private void InitializeRenderer()
        {
            viewport = new Viewport(Context, scene, camera, null);
            Renderer.SetViewport(0, viewport);
        }

        private void InitializeCamera()
        {
            var cameraNode = scene.CreateChild("Camera");
            camera = cameraNode.CreateComponent<Camera>();
        }

        private void InitializeLights()
        {
            var lightNode = camera.Node.CreateChild();
            lightNode.SetDirection(new Vector3(1f, -1.0f, 1f));
            var light = lightNode.CreateComponent<Light>();
            light.Range = 10;
            light.LightType = LightType.Directional;
            light.CastShadows = true;
            Renderer.ShadowMapSize *= 4;
        }

        protected override void Start()
        {
            scene = new Scene(Context);
            var octree = scene.CreateComponent<Octree>();
            InitializeCamera();
            InitializeLights();
            InitializeRenderer();
            InitializeAR();
        }

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