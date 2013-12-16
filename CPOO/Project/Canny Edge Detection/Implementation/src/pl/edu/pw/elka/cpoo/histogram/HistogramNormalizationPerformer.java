package pl.edu.pw.elka.cpoo.histogram;

import java.awt.image.BufferedImage;
import java.util.Arrays;

public class HistogramNormalizationPerformer
{
    public static BufferedImage filter(BufferedImage image)
    {
        int[] pixelValues = getPixelValues(image);
        int[] cumulativeDistributionFunction = getCumulativeDistributionFunction(pixelValues);
        int[] normalizedCumulativeDistributionFunction = normalizeCumulativeDistributionFunction(
                cumulativeDistributionFunction, image.getWidth(), image.getHeight());

        mapPixelValues(normalizedCumulativeDistributionFunction, image);

        return image;
    }

    private static int getPixelValue(BufferedImage image, int x, int y)
    {
        return image.getRaster().getPixel(x, y, new int[1])[0];
    }

    private static int[] getPixelValues(BufferedImage image)
    {
        int[] pixelValues = new int[255];

        for (int x = 0; x < image.getWidth(); ++x)
        {
            for (int y = 0; y < image.getHeight(); ++y)
            {
                pixelValues[getPixelValue(image, x, y)] += 1;
            }
        }

        return pixelValues;
    }

    private static int[] getCumulativeDistributionFunction(int[] pixelValues)
    {
        int[] cumulativeDistributionFunction = Arrays.copyOf(pixelValues, pixelValues.length);

        for (int i = 1; i < pixelValues.length; ++i)
        {
            cumulativeDistributionFunction[i] += cumulativeDistributionFunction[i - 1];
        }

        return cumulativeDistributionFunction;
    }

    private static int[] normalizeCumulativeDistributionFunction(int[] cumulativeDistributionFunction, float width,
                                                                 float height)
    {
        int[] normalizedCumulativeDistributionFunction = new int[cumulativeDistributionFunction.length];
        float cumulativeDistributionFunctionMinimum = minimum(cumulativeDistributionFunction);

        for (int i = 0; i < cumulativeDistributionFunction.length; ++i)
        {
            float normalizedValue = cumulativeDistributionFunction[i] - cumulativeDistributionFunctionMinimum;
            normalizedValue /= width * height - cumulativeDistributionFunctionMinimum;
            normalizedValue *= 255;

            normalizedCumulativeDistributionFunction[i] = Math.round(normalizedValue);
        }

        return normalizedCumulativeDistributionFunction;
    }

    private static int minimum(int[] array)
    {
        int[] arrayCopy = Arrays.copyOf(array, array.length);
        Arrays.sort(arrayCopy);

        return arrayCopy[0];
    }

    private static void mapPixelValues(int[] pixelValues, BufferedImage image)
    {
        for (int x = 0; x < image.getWidth(); ++x)
        {
            for (int y = 0; y < image.getHeight(); ++y)
            {
                setPixelValue(image, x, y, pixelValues[getPixelValue(image, x, y)]);
            }
        }
    }

    private static void setPixelValue(BufferedImage image, int x, int y, int value)
    {
        image.getRaster().setPixel(x, y, new int[] { value });
    }
}
