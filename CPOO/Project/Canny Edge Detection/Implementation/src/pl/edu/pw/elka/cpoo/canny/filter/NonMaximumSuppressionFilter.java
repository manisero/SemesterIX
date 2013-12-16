package pl.edu.pw.elka.cpoo.canny.filter;

import pl.edu.pw.elka.cpoo.canny.gradient.Direction;
import pl.edu.pw.elka.cpoo.canny.gradient.DirectionAndMagnitude;
import pl.edu.pw.elka.cpoo.canny.gradient.DirectionAndMagnitudeComputer;
import pl.edu.pw.elka.cpoo.filter.IImageFilter;
import pl.edu.pw.elka.cpoo.utilities.GrayscaleBufferedImage;

import java.awt.image.BufferedImage;

public class NonMaximumSuppressionFilter implements IImageFilter
{
    private final DirectionAndMagnitude directionAndMagnitude;

    public NonMaximumSuppressionFilter(DirectionAndMagnitude directionAndMagnitude)
    {
        this.directionAndMagnitude = directionAndMagnitude;
    }

    @Override
    public BufferedImage filter(BufferedImage input)
    {
        GrayscaleBufferedImage grayscaleBufferedImage = GrayscaleBufferedImage.getGrayscaleImage(input);
        grayscaleBufferedImage = cutOffBorders(grayscaleBufferedImage);
        grayscaleBufferedImage = performNoBorderSuppression(grayscaleBufferedImage);

        return grayscaleBufferedImage;
    }

    private GrayscaleBufferedImage cutOffBorders(GrayscaleBufferedImage input)
    {
        int width = input.getWidth();
        int height = input.getHeight();

        for (int x = 0; x < width; ++x)
        {
            input.setPixelValue(x, 0, 0);
            input.setPixelValue(x, height - 1, 0);
        }

        for (int y = 0; y < height; ++y)
        {
            input.setPixelValue(0, y, 0);
            input.setPixelValue(width - 1, y, 0);
        }

        return input;
    }

    private GrayscaleBufferedImage performNoBorderSuppression(GrayscaleBufferedImage input)
    {
        int width = input.getWidth();
        int height = input.getHeight();

        for (int x = 1; x < width - 1; ++x)
        {
            for (int y = 1; y < height - 1; ++y)
            {
                Direction approximatedDirection = DirectionAndMagnitudeComputer.approximateDirection
                        (directionAndMagnitude, x, y);

                if (approximatedDirection.equals(Direction.DIRECTION_0_DEGREES))
                {
                    if (DirectionAndMagnitudeComputer.isMagnitudeEastWestMaximum(directionAndMagnitude, x, y))
                    {
                        input.setPixelValue(x, y, (int) directionAndMagnitude.getMagnitude(x, y));
                        continue;
                    }
                }
                else if (approximatedDirection.equals(Direction.DIRECTION_45_DEGREES))
                {
                    if (DirectionAndMagnitudeComputer.isMagnitudeNorthEastSouthWestMaximum
                            (directionAndMagnitude, x, y))
                    {
                        input.setPixelValue(x, y, (int) directionAndMagnitude.getMagnitude(x, y));
                        continue;
                    }
                }
                else if (approximatedDirection.equals(Direction.DIRECTION_90_DEGREES))
                {
                    if (DirectionAndMagnitudeComputer.isMagnitudeNorthSouthMaximum(directionAndMagnitude, x, y))
                    {
                        input.setPixelValue(x, y, (int) directionAndMagnitude.getMagnitude(x, y));
                        continue;
                    }
                }
                else if (approximatedDirection.equals(Direction.DIRECTION_135_DEGREES))
                {
                    if (DirectionAndMagnitudeComputer.isMagnitudeNorthWestSouthEastMaximum
                            (directionAndMagnitude, x, y))
                    {
                        input.setPixelValue(x, y, (int) directionAndMagnitude.getMagnitude(x, y));
                        continue;
                    }
                }

                input.setPixelValue(x, y, 0);
            }
        }

        return input;
    }
}
