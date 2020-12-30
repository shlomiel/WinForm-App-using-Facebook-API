using System.Drawing;

namespace DP_20C_OmriShlomi_Logic
{
    internal class SecondFeature
    {
        public Image TurnImageBlackAndWhite(Image i_OriginalImage)
        {
            var newBlackAndWhitePhoto = new Bitmap(i_OriginalImage);
            for (int row = 0; row < newBlackAndWhitePhoto.Width; row++)
            {
                for (int column = 0; column < newBlackAndWhitePhoto.Height; column++)
                {
                    var colorValue = newBlackAndWhitePhoto.GetPixel(row, column);
                    var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 3; // get the average for black and white
                    newBlackAndWhitePhoto.SetPixel(row, column, Color.FromArgb(averageValue, averageValue, averageValue));
                }
            }

            return newBlackAndWhitePhoto;
        }

        public Image RotateImage(Bitmap i_OriginalImage)
        {
            i_OriginalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            return i_OriginalImage as Image;
        }
    }
}