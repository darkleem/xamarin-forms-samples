using System;
using CustomRenderer;
using CustomRenderer.Tizen;
using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Multimedia = Tizen.Multimedia;

[assembly: ExportRenderer(typeof(CameraPage), typeof(CameraPageRenderer))]
namespace CustomRenderer.Tizen
{
    public class CameraPageRenderer : PageRenderer
    {
        protected Multimedia.MediaView CameraPreview;
        protected Multimedia.Camera Camera;
        protected Multimedia.CameraDevice SelectedCamera = Multimedia.CameraDevice.Rear;
        protected bool FlashEnabled = false;
        protected ElmSharp.GestureLayer GestureLayer;

        ElmSharp.Image flashButton;
        ElmSharp.Image noFlashButton;
        ElmSharp.Image cameraButton;
        ElmSharp.Image takePhotoButton;
        ElmSharp.Label label = null;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (Camera == null)
            {
                var box = new ElmSharp.Box(Forms.Context.MainWindow);
                SetNativeControl(box);

                try
                {
                    CameraPreview = new Multimedia.MediaView(Forms.Context.MainWindow);
                    CreateCamera();
                    box.PackEnd(CameraPreview);
                }
                catch (Exception ex)
                {
                    Element.Title = "Error";
                    label = new ElmSharp.Label(Forms.Context.MainWindow) {
                        Text = "Camera preview is not available.<br><br>" + ex
                    };
                    label.LineWrapType = WrapType.Mixed;
                    box.UnPackAll();
                    Camera?.Dispose();
                    Camera = null;
                    box.PackEnd(label);
                    label.Show();
                }

                cameraButton = AddButtonImage("ToggleCameraButton.png");
                takePhotoButton = AddButtonImage("TakePhotoButton.png");
                noFlashButton = AddButtonImage("NoFlashButton.png");
                flashButton = AddButtonImage("FlashButton.png");
                flashButton.Hide();

                CameraPreview.Show();
                GestureLayer = new ElmSharp.GestureLayer(box);
                GestureLayer.Attach(box);
                GestureLayer.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.End, OnTap);
                box.SetLayoutCallback(OnLayout);
                box.Show();
            }

            if (e.OldElement != null)
                Camera?.StopPreview();

            if (e.NewElement != null)
                Camera?.StartPreview();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Camera != null)
                {
                    Camera.StateChanged -= Camera_CameraStateChanged;
                }
                Camera?.Dispose();
                Camera = null;
            }
            base.Dispose(disposing);
        }

        private void Camera_CameraStateChanged(object sender, Multimedia.CameraStateChangedEventArgs e)
        {
            if (e.Current == Multimedia.CameraState.Created)
            {
                Camera?.Dispose();
                CreateCamera();
                Camera.StartPreview();
            }
        }

        private void CreateCamera()
        {
            Camera = new Multimedia.Camera(SelectedCamera);
            Camera.Display = new Multimedia.Display(CameraPreview);
            Camera.StateChanged += Camera_CameraStateChanged;
        }

        private ElmSharp.Image AddButtonImage(string fileName)
        {
            var image = new ElmSharp.Image(Forms.Context.MainWindow);
            image.Load(ResourcePath.GetPath(fileName));
            image.Show();
            (NativeView as ElmSharp.Box).PackEnd(image);
            return image;
        }

        private void OnTap(GestureLayer.TapData tap)
        {
            if (IsTapInside(ref tap, flashButton))
                ToggleFlash(flashButton, null);
            else if (IsTapInside(ref tap, cameraButton))
                ToggleCamera(cameraButton, null);
            else if (takePhotoButton.IsVisible && IsTapInside(ref tap, takePhotoButton))
                TakePhoto(takePhotoButton, null);
        }

        private bool IsTapInside(ref GestureLayer.TapData tap, ElmSharp.EvasObject target)
        {
            ElmSharp.Rect rectangle = target.Geometry;
            return tap.X >= rectangle.Left && tap.X <= rectangle.Right && tap.Y >= rectangle.Top && tap.Y <= rectangle.Bottom;
        }

        private void TakePhoto(object sender, EventArgs e)
        {
            takePhotoButton.Hide();
            Device.StartTimer(TimeSpan.FromSeconds(0.4), () =>
            {
                takePhotoButton.Show();
                return false;
            });
        }

        private void ToggleCamera(object sender, EventArgs e)
        {
            if (SelectedCamera == Multimedia.CameraDevice.Rear)
            {
                SelectedCamera = Multimedia.CameraDevice.Front;
                flashButton.Hide();
                noFlashButton.Hide();
            }
            else
            {
                SelectedCamera = Multimedia.CameraDevice.Rear;
                if (FlashEnabled)
                    flashButton.Show();
                else
                    noFlashButton.Show();
            }
            Camera.StopPreview();
        }

        private void ToggleFlash(object sender, EventArgs e)
        {
            if (SelectedCamera == Multimedia.CameraDevice.Front)
                return;

            FlashEnabled = !FlashEnabled;
            if (FlashEnabled)
            {
                noFlashButton.Hide();
                flashButton.Show();
            }
            else
            {
                flashButton.Hide();
                noFlashButton.Show();
            }
        }

        private void OnLayout()
        {
            var g = NativeView.Geometry;

            if (label != null)
            {
                label.Geometry = g;
            }
            else
            {
                CameraPreview.Geometry = g;
                flashButton.Geometry = new ElmSharp.Rect(g.Left + 20, g.Top + 20, 108, 114);
                noFlashButton.Geometry = flashButton.Geometry;

                cameraButton.Geometry = new ElmSharp.Rect(g.Right - 20 - 136, g.Top + 20, 136, 104);
                takePhotoButton.Geometry = new ElmSharp.Rect((g.Right - 143) / 2, g.Bottom - 143 - 20, 143, 143);
            }
        }
    }
}