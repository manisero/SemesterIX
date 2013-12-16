package pl.edu.pw.elka.cpoo.canny.gradient;

import pl.edu.pw.elka.cpoo.utilities.GrayscaleBufferedImage;

import java.awt.image.BufferedImage;
import java.util.Arrays;

public class SobelComputer
{
    public static double[][] computeGradientX(BufferedImage input)
    {
        return computeGradientX(GrayscaleBufferedImage.getGrayscaleImage(input));
    }

    public static double[][] computeGradientX(GrayscaleBufferedImage input)
    {
        int width = input.getWidth();
        int height = input.getHeight();

        double[][] gradientX = new double[width][height];

        for (int x = 1; x < width - 1; ++x)
        {
            for (int y = 1; y < height - 1; ++y)
            {
                int cellValue = 0;

                cellValue += input.getPixelValue(x - 1, y - 1);
                cellValue += 2 * input.getPixelValue(x - 1, y);
                cellValue += input.getPixelValue(x - 1, y + 1);
                cellValue -= input.getPixelValue(x + 1, y - 1);
                cellValue -= 2 * input.getPixelValue(x + 1, y);
                cellValue -= input.getPixelValue(x + 1, y + 1);

                gradientX[x][y] = cellValue;
            }
        }

        return gradientX;
    }

    public static double[][] computeGradientY(BufferedImage input)
    {
        return computeGradientY(GrayscaleBufferedImage.getGrayscaleImage(input));
    }

    public static double[][] computeGradientY(GrayscaleBufferedImage input)
    {
        int width = input.getWidth();
        int height = input.getHeight();

        double[][] gradientY = new double[width][height];

        for (int x = 1; x < width - 1; ++x)
        {
            for (int y = 1; y < height - 1; ++y)
            {
                int cellValue = 0;

                cellValue += input.getPixelValue(x - 1, y - 1);
                cellValue += 2 * input.getPixelValue(x, y - 1);
                cellValue += input.getPixelValue(x + 1, y - 1);
                cellValue -= input.getPixelValue(x - 1, y + 1);
                cellValue -= 2 * input.getPixelValue(x, y + 1);
                cellValue -= input.getPixelValue(x + 1, y + 1);

                gradientY[x][y] = cellValue;
            }
        }

        return gradientY;
    }
}
