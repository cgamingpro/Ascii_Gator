using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Reflection.Metadata;


class Program
{
    static void Main()
    {
        string path = "F:\\wg\\c#\\ASCII_GENRATOR\\Chig_Genrator\\demoImage\\s4.png";
        string asciiChars = "@%#*+=-:.   ";
        //string asciiChars = " .:-=+*#%@";

        float[,] sobleX = new float[,]
        {
            {-1,0,1},
            {-2,0,2},
            {-1,0,1}
        };
        float[,] sobelY = new float[,]
        {
            {-1,-2,-1},
            {0,0,0},
            {1,2,1}
        };
        float[,] gausianKernel = new float[,]
        {
            { 1f/16, 2f/16, 1f/16 },
            { 2f/16, 4f/16, 2f/16 },
            { 1f/16, 2f/16, 1f/16 }
        };
        //float[,] gausianKernel = new float[,]
        //{
        //    { 1f/273, 4f/273, 7f/273, 4f/273, 1f/273 },
        //    { 4f/273, 16f/273, 26f/273, 16f/273, 4f/273 },
        //    { 7f/273, 26f/273, 41f/273, 26f/273, 7f/273 },
        //    { 4f/273, 16f/273, 26f/273, 16f/273, 4f/273 },
        //    { 1f/273, 4f/273, 7f/273, 4f/273, 1f/273 }
        //};


        


        int width;
        int height;
        using (Image<Rgba32> originalImage = Image.Load<Rgba32>(path))
        {
            width = originalImage.Width;
            height = originalImage.Height/4;

            originalImage.Mutate(ctx => ctx.Resize(width, height));

            Rgba32[,] originalColoredImage = new Rgba32[height, width];
            float[,] grayMatrix = new float[height, width];
            float[,] bluredGrayMatrix = new float[height, width];
            float[,] edgeAngleMatrix = new float[height, width];
            char[,] asciiMatrix = new char[height, width];
            float[,] edgeMatrix = new float[height, width];
            Image<Rgba32> edgees = new Image<Rgba32>(width, height);


            //gray and ascii
            for (int y = 0; y <height ;  y ++)
            {
                for (int x = 0; x <width ; x ++)
                {
                    Rgba32 pixel = originalImage[x,y];
                    originalColoredImage[y,x] = pixel;
                    float grayValue = (float)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);

                    grayMatrix[y,x] = grayValue;

                    //int index = (int)(grayValue * (asciiChars.Length - 1)) / 255;
                    int index = (int)((grayValue / 255f) * (asciiChars.Length - 1));
                    asciiMatrix[y,x] = asciiChars[index];

                }
            }

            //gausianblur

            for (int y = 1; y < height - 1; y ++)
            {
                for (int x = 1; x < width -1 ; x ++)
                {
                    float sum = 0;
                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            sum += gausianKernel[ky + 1, kx + 1] * grayMatrix[y + ky, x + kx];
                        }
                    }
                    bluredGrayMatrix[y,x] = sum;
                        
                }
            }
            //soble edge adn threshold 

            int count = 0;
            float totalEdge = 0;

            for (int y = 1; y <height -1; y ++)
            {
                for(int x = 1;x <width -1; x ++)
                {
                    float gy = 0, gx = 0;
                    float edgeAngle = 0;

                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for(int kx =  -1; kx <= 1; kx++)
                        {
                            float pixel = bluredGrayMatrix[y + ky,x + kx];
                            gx += sobleX[ky + 1, kx + 1] * pixel;
                            gy += sobelY[ky + 1, kx + 1] * pixel;

                        }
                    }

                    float gradient = MathF.Sqrt(gx  * gx + gy * gy);


                    edgeAngle = MathF.Atan2(gy , gx );
                    float angleDegrees = edgeAngle * (180f / MathF.PI);
                    float absoouteAngle = (angleDegrees + 180) % 180;
                    edgeAngleMatrix[y,x] = (float)absoouteAngle;

                    gradient = Math.Clamp(gradient, 0, 255);
                    totalEdge += gradient;
                    count++;
                    edgeMatrix[y, x] = gradient;
                    byte val = (byte)(gradient);

                    edgees[x,y] = new Rgba32(val,val,val);
                }
            }
            
            float avg = totalEdge/count;
            float threshold = avg * 2.5f;

            //changing the ascii matrix one's with edge
            for(int y = 0; y <height; y ++)
            {
                for (int x = 0; x <width; x ++)
                {
                    if (edgeMatrix[y,x] > threshold)
                    {
                        //asciiMatrix[y, x] = '|';
                        float angle = edgeAngleMatrix[y, x];
                        if((angle >= 0 && angle < 22) || (angle >= 158 && angle < 180))
                        {
                            asciiMatrix[y, x] = '-';
                        }
                        else if(angle >= 22 && angle < 67.5 )
                        {
                            asciiMatrix[y,x] = '\\';
                        }
                        else if(angle >= 67.5 && angle < 112.5)
                        {
                            asciiMatrix[y, x] = '|';
                        }
                        else
                        {
                            asciiMatrix[y, x] = '/';
                        }

                    }
                    else
                    {
                        continue;
                    }
                }
            }
            //edgees.Save("F:\\wg\\c#\\ASCII_GENRATOR\\Chig_Genrator\\demoImage\\line4.jpg");

            Console.WriteLine("height of the image is " + width);
            Console.WriteLine("weidith is " + width);
            Console.WriteLine(grayMatrix);
            PrintMatrix(asciiMatrix,originalColoredImage);
            
        }
    }

    static void PrintMatrix<Matrix>( Matrix [,] matrixx, Rgba32[,] colorMatrixx)
    {
        
        for (int y = 0; y < matrixx.GetLength(0); y++)
        {
           for(int x = 0;x < matrixx.GetLength(1); x++)
           {
                Console.ForegroundColor = GetNearestConsoleColor(colorMatrixx[y,x]);
                Console.Write($"{matrixx[y,x]}",matrixx[y,x]);
                //Console.Write(matrixx[y, x]);
            }
            Console.WriteLine("\n");
        }
        Console.ResetColor();
        
    }
    static ConsoleColor GetNearestConsoleColor(Rgba32 pixel)
    {
        ConsoleColor nearest = ConsoleColor.White;
        double minDistance = double.MaxValue;

        foreach (var (color, rgb) in consoleColors)
        {
            double distance = Math.Pow(pixel.R - rgb.R, 2) +
                              Math.Pow(pixel.G - rgb.G, 2) +
                              Math.Pow(pixel.B - rgb.B, 2);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = color;
            }
        }
        return nearest;
    }


    static readonly (ConsoleColor color, Rgba32 rgb)[] consoleColors = new (ConsoleColor, Rgba32)[]
    {
    (ConsoleColor.Black, new Rgba32(0,0,0)),
    (ConsoleColor.DarkBlue, new Rgba32(0,0,128)),
    (ConsoleColor.DarkGreen, new Rgba32(0,128,0)),
    (ConsoleColor.DarkCyan, new Rgba32(0,128,128)),
    (ConsoleColor.DarkRed, new Rgba32(128,0,0)),
    (ConsoleColor.DarkMagenta, new Rgba32(128,0,128)),
    (ConsoleColor.DarkYellow, new Rgba32(210,180,140)), // Use tan/beige for skin
    (ConsoleColor.Gray, new Rgba32(192,192,192)),
    (ConsoleColor.DarkGray, new Rgba32(128,128,128)),
    (ConsoleColor.Blue, new Rgba32(0,0,255)),
    (ConsoleColor.Green, new Rgba32(0,255,0)),
    (ConsoleColor.Cyan, new Rgba32(0,255,255)),
    (ConsoleColor.Red, new Rgba32(255,200,180)), // softer red/orange
    (ConsoleColor.Magenta, new Rgba32(255,0,255)),
    (ConsoleColor.Yellow, new Rgba32(255,255,180)), // pale yellow for highlights
    (ConsoleColor.White, new Rgba32(255,255,255))
    };

}
