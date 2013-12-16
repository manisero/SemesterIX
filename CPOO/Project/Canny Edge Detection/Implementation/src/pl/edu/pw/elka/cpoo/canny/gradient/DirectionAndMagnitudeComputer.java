package pl.edu.pw.elka.cpoo.canny.gradient;

import pl.edu.pw.elka.cpoo.exception.CannyEdgeDetectorException;
import pl.edu.pw.elka.cpoo.exception.DirectionAndMagnitudeComputerException;
import pl.edu.pw.elka.cpoo.utilities.GrayscaleBufferedImage;

import java.awt.image.BufferedImage;

public class DirectionAndMagnitudeComputer
{

    public static DirectionAndMagnitude computeGradientDirectionAndMagnitude(BufferedImage vertical,
                                                                                     BufferedImage horizontal)
    {
        GrayscaleBufferedImage grayscaleVertical = GrayscaleBufferedImage.getGrayscaleImage(vertical);
        GrayscaleBufferedImage grayscaleHorizontal = GrayscaleBufferedImage.getGrayscaleImage(horizontal);

        return computeGradientDirectionAndMagnitude(grayscaleVertical, grayscaleHorizontal);
    }

    public static DirectionAndMagnitude computeGradientDirectionAndMagnitude(
            GrayscaleBufferedImage vertical, GrayscaleBufferedImage horizontal)
    {
        int width = horizontal.getWidth();
        int height = horizontal.getHeight();

        if (width != vertical.getWidth() || height != vertical.getHeight())
        {
            throw new CannyEdgeDetectorException("Horizontal gradient and vertical gradient sizes does not match!");
        }

        DirectionAndMagnitude directionAndMagnitude = new DirectionAndMagnitude(width, height);

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                double verticalValue = vertical.getPixelValue(x, y);
                double horizontalValue = horizontal.getPixelValue(x, y);

                if (horizontalValue != 0)
                {
                    directionAndMagnitude.setDirection(x, y, Math.atan(verticalValue / horizontalValue));
                }
                else
                {
                    directionAndMagnitude.setDirection(x, y, Math.PI / 2.0d);
                }

                directionAndMagnitude.setMagnitude(x, y, Math.hypot(verticalValue, horizontalValue));
            }
        }

        return directionAndMagnitude;
    }

    public static Direction approximateDirection(DirectionAndMagnitude directionAndMagnitude, int x, int y)
    {
        if (directionInRange(directionAndMagnitude.getDirection(x, y), Math.toRadians(-22.5), Math.toRadians(22.5),
                true))
        {
            return Direction.DIRECTION_0_DEGREES;
        }
        else if (directionInRange(directionAndMagnitude.getDirection(x, y), Math.toRadians(22.5), Math.toRadians(67.5),
                true))
        {
            return Direction.DIRECTION_45_DEGREES;
        }
        else if (directionInRange(directionAndMagnitude.getDirection(x, y), Math.toRadians(67.5),
                Math.toRadians(-67.5), false))
        {
            return Direction.DIRECTION_90_DEGREES;
        }
        else if (directionInRange(directionAndMagnitude.getDirection(x, y), Math.toRadians(-67.5),
                Math.toRadians(22.5), true))
        {
            return Direction.DIRECTION_135_DEGREES;
        }

        throw new CannyEdgeDetectorException("Could not approximate direction exception");
    }

    private static boolean directionInRange(double direction, double higherOrEqualThan, double lowerThan,
                                            boolean conjunction)
    {
        if (conjunction)
        {
            return direction >= higherOrEqualThan && direction < lowerThan;
        }

        return direction >= higherOrEqualThan || direction < lowerThan;
    }

    public static boolean isMagnitudeEastWestMaximum(DirectionAndMagnitude directionAndMagnitude, int x, int y)
    {
        checkIfMagnitudeTestIsValid(directionAndMagnitude, x, y);

        return directionAndMagnitude.getMagnitude(x, y) > directionAndMagnitude.getMagnitude(x - 1, y)
                && directionAndMagnitude.getMagnitude(x, y) > directionAndMagnitude.getMagnitude(x + 1, y);
    }

    private static void checkIfMagnitudeTestIsValid(DirectionAndMagnitude directionAndMagnitude, int x, int y)
    {
        if (x <= 0 || y <= 0 || x >= directionAndMagnitude.getMagnitudeWidth()
                || y >= directionAndMagnitude.getMagnitudeHeight())
        {
            throw new DirectionAndMagnitudeComputerException("Could not test magnitude for: " + x + ", " + y + "!");
        }
    }

    public static boolean isMagnitudeNorthSouthMaximum(DirectionAndMagnitude directionAndMagnitude, int x,
                                                       int y)
    {
        checkIfMagnitudeTestIsValid(directionAndMagnitude, x, y);

        return directionAndMagnitude.getMagnitude(x, y) > directionAndMagnitude.getMagnitude(x, y - 1)
                && directionAndMagnitude.getMagnitude(x, y) > directionAndMagnitude.getMagnitude(x, y + 1);
    }

    public static boolean isMagnitudeNorthEastSouthWestMaximum(DirectionAndMagnitude directionAndMagnitude,
                                                               int x, int y)
    {
        checkIfMagnitudeTestIsValid(directionAndMagnitude, x, y);

        return directionAndMagnitude.getMagnitude(x, y) > directionAndMagnitude.getMagnitude(x - 1, y - 1)
                && directionAndMagnitude.getMagnitude(x, y) > directionAndMagnitude.getMagnitude(x + 1, y + 1);
    }

    public static boolean isMagnitudeNorthWestSouthEastMaximum(DirectionAndMagnitude directionAndMagnitude,
                                                               int x, int y)
    {
        checkIfMagnitudeTestIsValid(directionAndMagnitude, x, y);

        return directionAndMagnitude.getMagnitude(x, y) > directionAndMagnitude.getMagnitude(x - 1, y + 1)
                && directionAndMagnitude.getMagnitude(x, y) > directionAndMagnitude.getMagnitude(x + 1, y - 1);
    }
}
