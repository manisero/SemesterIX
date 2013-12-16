package pl.edu.pw.elka.cpoo.filter.impl;

import pl.edu.pw.elka.cpoo.filter.CompositeImageFilter;
import pl.edu.pw.elka.cpoo.utilities.GrayscaleBufferedImage;

import java.awt.image.BufferedImage;
import java.util.Arrays;

public class HistogramNormalizationFilter extends CompositeImageFilter
{
    @Override
    protected BufferedImage performFiltering(BufferedImage input)
    {
        GrayscaleBufferedImage grayscaleImage = GrayscaleBufferedImage.getGrayscaleImage(input);

        int[] pixelValues = getPixelValues(grayscaleImage);
        int[] cumulativeDistributionFunction = getCumulativeDistributionFunction(pixelValues);
        int[] normalizedCumulativeDistributionFunction = normalizeCumulativeDistributionFunction(
                cumulativeDistributionFunction, grayscaleImage.getWidth(), grayscaleImage.getHeight());

        mapPixelValues(normalizedCumulativeDistributionFunction, grayscaleImage);

        return grayscaleImage;
    }

    private static int[] getPixelValues(GrayscaleBufferedImage image)
    {
        int[] pixelValues = new int[255];

        for (int x = 0; x < image.getWidth(); ++x)
        {
            for (int y = 0; y < image.getHeight(); ++y)
            {
                pixelValues[image.getPixelValue(x, y)] += 1;
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

    private static void mapPixelValues(int[] pixelValues, GrayscaleBufferedImage image)
    {
        for (int x = 0; x < image.getWidth(); ++x)
        {
            for (int y = 0; y < image.getHeight(); ++y)
            {
                image.setPixelValue(x, y, pixelValues[image.getPixelValue(x, y)]);
            }
        }
    }
}
