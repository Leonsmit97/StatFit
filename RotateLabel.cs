using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace workoutTracker
{
    public class RotatedLabel : UserControl
    {
        #region Members

        private int _angle;
        private double _radians;
        private ContentAlignment _alignment = ContentAlignment.TopLeft;
        private int _quadrant = 1;

        #endregion //Members

        #region Properties

        [Category("Appearance")]
        [Description("Indicates the angle to which the text will be displayed.")]
        public int Angle
        {
            get
            {
                return _angle;
            }
            set
            {
                _angle = ((value % 360) + 360) % 360; // range must be in 0-360 degrees.

                _radians = Math.PI * _angle / 180.0;
                CalculateQuadrant();

                Refresh();
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                Refresh();
            }
        }

        [Category("Appearance")]
        [Description("Indicates how the text should be aligned.")]
        public ContentAlignment TextAlign
        {
            get
            {
                return _alignment;
            }
            set
            {
                _alignment = value;
                Refresh();
            }
        }

        #endregion //Properties

        #region Events

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            // Calculate the text size.
            SizeF textSize = paintEventArgs.Graphics.MeasureString(Text, Font, Parent.Width);

            int x = Math.Abs((int)Math.Ceiling(textSize.Height * Math.Sin(_radians)));
            int y = Math.Abs((int)Math.Ceiling(textSize.Height * Math.Cos(_radians)));
            Point rotatedHeight = new Point(x, y);

            x = Math.Abs((int)Math.Ceiling(textSize.Width * Math.Cos(_radians)));
            y = Math.Abs((int)Math.Ceiling(textSize.Width * Math.Sin(_radians)));
            Point rotatedWidth = new Point(x, y);

            Size textBoundingBox = new Size(rotatedWidth.X + rotatedHeight.X, rotatedWidth.Y + rotatedHeight.Y);
            SetControlSize(textBoundingBox);

            Point rotationOffset = CalculateOffsetForRotation(ref rotatedHeight, ref rotatedWidth, ref textBoundingBox);
            Point alignmentOffset = CalculateOffsetForAlignment(ref textBoundingBox);

            // Apply the transformation and rotation to the graphics.
            paintEventArgs.Graphics.TranslateTransform(rotationOffset.X + alignmentOffset.X, rotationOffset.Y + alignmentOffset.Y);
            paintEventArgs.Graphics.RotateTransform(_angle);

            // Draw the text and let the base class do its painting.
            paintEventArgs.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), 0f, 0f);
            base.OnPaint(paintEventArgs);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Refresh();
        }

        #endregion // Events

        #region Helper Methods

        private Point CalculateOffsetForRotation(ref Point pRotatedHeight, ref Point pRotatedWidth, ref Size pTextBoundingBox)
        {
            Point offset = new Point(0, 0);

            switch (_quadrant)
            {
                case 1:
                    offset.X = pRotatedHeight.X;
                    break;
                case 2:
                    offset.X = pTextBoundingBox.Width;
                    offset.Y = pRotatedHeight.Y;
                    break;
                case 3:
                    offset.X = pRotatedWidth.X;
                    offset.Y = pTextBoundingBox.Height;
                    break;
                case 4:
                    offset.Y = pRotatedWidth.Y;
                    break;
            }

            return offset;
        }

        private Point CalculateOffsetForAlignment(ref Size pTextBoundingBox)
        {
            Point offset = new Point(0, 0);

            switch (_alignment)
            {
                case ContentAlignment.TopLeft:
                    //nothing to do
                    break;
                case ContentAlignment.TopCenter:
                    offset.X = (int)(0.5 * Width - 0.5 * pTextBoundingBox.Width);
                    break;
                case ContentAlignment.TopRight:
                    offset.X = (Width - pTextBoundingBox.Width);
                    break;
                case ContentAlignment.MiddleLeft:
                    offset.Y = (int)(0.5 * Height - 0.5 * pTextBoundingBox.Height);
                    break;
                case ContentAlignment.MiddleCenter:
                    offset.X = (int)(0.5 * Width - 0.5 * pTextBoundingBox.Width);
                    offset.Y = (int)(0.5 * Height - 0.5 * pTextBoundingBox.Height);
                    break;
                case ContentAlignment.MiddleRight:
                    offset.X = (Width - pTextBoundingBox.Width);
                    offset.Y = (int)(0.5 * Height - 0.5 * pTextBoundingBox.Height);
                    break;
                case ContentAlignment.BottomLeft:
                    offset.Y = (Height - pTextBoundingBox.Height);
                    break;
                case ContentAlignment.BottomCenter:
                    offset.X = (int)(0.5 * Width - 0.5 * pTextBoundingBox.Width);
                    offset.Y = (Height - pTextBoundingBox.Height);
                    break;
                case ContentAlignment.BottomRight:
                    offset.X = (Width - pTextBoundingBox.Width);
                    offset.Y = (Height - pTextBoundingBox.Height);
                    break;
            }

            return offset;
        }

        private void SetControlSize(Size pTextBoundingBox)
        {
            if (DesignMode) return;
            if (!AutoSize) return;

            Width = pTextBoundingBox.Width;
            Height = pTextBoundingBox.Height;
        }

        private void CalculateQuadrant()
        {
            _quadrant = (_angle >= 0 && _angle < 90) ? 1 :
                        (_angle >= 90 && _angle < 180) ? 2 :
                        (_angle >= 180 && _angle < 270) ? 3 :
                        (_angle >= 270 && _angle < 360) ? 4 : 0;
        }

        #endregion //Helper Methods

    }
}
