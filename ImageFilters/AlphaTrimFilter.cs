using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class AlphaTrim
    {
        public static Byte[,] ApplyFilter(Byte[,] ImageMatrix, int MaxWindowSize, int UsedAlgorithm, int TrimValue)
        {
            int extra = MaxWindowSize / 2;
            int h = ImageOperations.GetHeight(ImageMatrix);
            int w = ImageOperations.GetWidth(ImageMatrix);
            int avarage = 0;

            byte[,] filterImage = Extend(ImageMatrix, MaxWindowSize);

            for (int i = extra; i < h + extra; i++)
            {
                for (int j = extra; j < w + extra; j++)
                {
                    int sum = 0;
                    byte[] window = GetWindow(filterImage, MaxWindowSize, j, i);

                    if (UsedAlgorithm == 0)
                    {
                        // window sort count;
                        window = SortHelper.CountingSort(window);
                        for (int k = TrimValue; k < window.GetLength(0) - TrimValue; k++)
                        {
                            sum += window[k];
                        }
                        avarage = sum / (window.GetLength(0) - MaxWindowSize * 2);
                    }
                    else if (UsedAlgorithm == 1)
                    {
                        // window select k;

                        avarage = SortHelper.Kth_element(window, TrimValue);
                    }

                    ImageMatrix[i - extra, j - extra] = (byte)avarage;
                }
            }

            return ImageMatrix;
        }

        public static byte[] GetWindow(byte[,] Image, int WindowSize, int x, int y)
        {
            int extra = WindowSize / 2;
            int index = 0;

            byte[] Window = new byte[WindowSize * WindowSize];

            for (int i = y - extra; i <= y + extra; i++)
                for (int j = x - extra; j <= x + extra; j++)
                    Window[index++] = Image[i, j];

            return Window;
        }

        public static byte[,] Extend(byte[,] Image, int WindowSize)
        {
            int h = ImageOperations.GetHeight(Image);
            int w = ImageOperations.GetWidth(Image);

            int extra = WindowSize / 2;

            byte[,] eImage = new Byte[h + extra * 2, w + extra * 2];

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    eImage[i + extra, j + extra] = Image[i, j];
                }
            }

            for (int i = extra; i > 0; i--)
            {
                for (int j = 0; j < w; j++)
                {
                    eImage[i - 1, j + extra] = Image[extra - i, j];
                    eImage[h + extra * 2 - i, j + extra] = Image[h - extra + i - 1, j];
                }
            }

            for (int i = extra; i > 0; i--)
            {
                for (int j = 0; j < h; j++)
                {
                    eImage[j + extra, i - 1] = Image[j, extra - i];
                    eImage[j + extra, w + extra * 2 - i] = Image[j, w - extra + i - 1];
                }
            }

            for (int i = 0; i < extra; i++)
            {
                for (int j = 0; j < extra; j++)
                {
                    eImage[i, j] = Image[extra - i - 1, extra - j - 1];
                    eImage[h + extra * 2 - i - 1, j] = Image[h - extra + i, extra - 1 - j];
                    eImage[i, w + extra * 2 - j - 1] = Image[extra - i - 1, w - extra + j];
                    eImage[h + extra * 2 - i - 1, w + extra * 2 - j - 1] = Image[h - extra + i, w - extra + j];
                }
            }

            return eImage;
        }

    }
}