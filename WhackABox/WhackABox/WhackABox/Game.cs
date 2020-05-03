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
        private static Random random = new Random();
        private float newBoxTtl;
        private readonly float newBoxIntervalInSeconds = 2;

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

        private void AddBox(PlaneNode planeNode)
        {
            var subPlaneNode = planeNode.GetChild("subplane");
            var boxNode = planeNode.CreateChild("Box");
            boxNode.SetScale(0.1f);
            var x = planeNode.ExtentX * (float)(random.NextDouble() -0.5f);
            var z = planeNode.ExtentZ * (float)(random.NextDouble() -0.5f);
            boxNode.Position = new Vector3(x, 0.1f, z) + subPlaneNode.Position;
            var box = boxNode.CreateComponent<Box>();
            box.Color = Color.Blue;

            var rotationSpeed = new Vector3(10.0f, 20.0f, 30.0f);
            var rotator = new Rotator() { RotationSpeed = rotationSpeed };
            boxNode.AddComponent(rotator);
        }

        protected override void OnUpdate(float timeStep)
        {
            base.OnUpdate(timeStep);
            newBoxTtl -= timeStep;
            if (newBoxTtl < 0)
            {
                foreach (var node in scene.Children.OfType<PlaneNode>())
                {
                    AddBox(node);
                }
                newBoxTtl += newBoxIntervalInSeconds;
            }
        }

        protected PlaneNode FindNodeByPlaneId(string planeId) =>scene.Children.OfType<PlaneNode>().FirstOrDefault(e => e.PlaneId == planeId);
    }
}