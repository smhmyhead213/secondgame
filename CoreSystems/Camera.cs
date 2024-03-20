using Microsoft.Xna.Framework.Graphics;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.CoreSystems
{
    public class Camera
    {
        public Viewport Viewport;

        public Matrix4x4 Matrix;

        private Matrix4x4 RotationMatrix; // to be added (what rotation matrix would be used, considering that the origin is the top left)

        private Matrix4x4 ScreenMatrix; // scale matrix that adjusts for screen size

        private Matrix4x4 TranslationMatrix; // translation matrix that allows for camera panning

        private Matrix4x4 ZoomMatrix; // scale matrix that allows for zooming in and out
        public Camera()
        {
            Viewport = new(0, 0, Width, Height);
        }

        public void UpdateMatrices()
        {
            Matrix = TranslationMatrix * ZoomMatrix * ScreenMatrix;
        }

        public void TranslateCamera(Vector2 offset)
        {
            TranslationMatrix = Matrix4x4.CreateTranslation(new Vector3(offset, 0));
        }

        public void ResetTranslation()
        {
            TranslationMatrix = Matrix4x4.Identity;
        }
    }
}
