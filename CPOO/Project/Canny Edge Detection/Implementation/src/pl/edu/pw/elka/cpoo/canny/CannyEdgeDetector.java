package pl.edu.pw.elka.cpoo.canny;

import pl.edu.pw.elka.cpoo.blur.GaussianBlurFilteringPerformer;
import pl.edu.pw.elka.cpoo.edge.SobelOperatorFilteringPerformer;
import pl.edu.pw.elka.cpoo.grayscale.GrayscaleFilteringPerformer;
import pl.edu.pw.elka.cpoo.histogram.HistogramNormalizationPerformer;

import java.awt.image.BufferedImage;

public class CannyEdgeDetector
{
    private final int radius;
    private final double lowThreshold;
    private final double highThreshold;

    public CannyEdgeDetector(int radius, double lowThreshold, double highThreshold)
    {
        this.radius = radius;
        this.lowThreshold = lowThreshold;
        this.highThreshold = highThreshold;
    }

    public BufferedImage filter(BufferedImage inputImage)
    {
        BufferedImage grayScaleImage = GrayscaleFilteringPerformer.filter(inputImage);
        BufferedImage normalizedImage = HistogramNormalizationPerformer.filter(grayScaleImage);
        BufferedImage blurredImage = GaussianBlurFilteringPerformer.filter(normalizedImage, radius);
        BufferedImage sobelHorizontalImage = SobelOperatorFilteringPerformer.filter(blurredImage, true);
        BufferedImage sobelVerticalImage = SobelOperatorFilteringPerformer.filter(blurredImage, false);

        GradientDirectionMagnitude directionMagnitude = computeDirectionAndMagnitude(sobelHorizontalImage,
                sobelVerticalImage);

        performNonMaximumSuppresion(sobelVerticalImage, directionMagnitude);

        return sobelVerticalImage;
    }

    private GradientDirectionMagnitude computeDirectionAndMagnitude(BufferedImage horizontalGradient,
                                                                    BufferedImage verticalGradient)
    {
        if (horizontalGradient.getWidth() != verticalGradient.getWidth() ||
                horizontalGradient.getHeight() != verticalGradient.getHeight())
        {
            throw new CannyEdgeDetectorException("Horizontal gradient and vertical gradient sizes does not match!");
        }

        int width = horizontalGradient.getWidth();
        int height = horizontalGradient.getHeight();

        GradientDirectionMagnitude result = new GradientDirectionMagnitude(width, height);

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                double horizontalGradientPointValue = getPixelValue(horizontalGradient, x, y);
                double verticalGradientPointValue = getPixelValue(verticalGradient, x, y);

                if (horizontalGradientPointValue != 0)
                {
                    result.setDirection(x, y, Math.atan(verticalGradientPointValue / horizontalGradientPointValue));
                }
                else
                {
                    result.setDirection(x, y, Math.PI / 2.0f);
                }

                result.setMagnitude(x, y, Math.hypot(verticalGradientPointValue, horizontalGradientPointValue));
            }
        }

        return result;
    }

    private int getPixelValue(BufferedImage image, int x, int y)
    {
        return image.getRaster().getPixel(x, y, new int[1])[0];
    }

    private BufferedImage performNonMaximumSuppresion(BufferedImage image,
                                                      GradientDirectionMagnitude directionMagnitude)
    {
        int width = image.getWidth();
        int height = image.getHeight();

        image = removeBorder(image);

        for (int x = 1; x < width - 1; ++x)
        {
            for (int y = 1; y < height - 1; ++y)
            {
                if (directionMagnitude.isDirectionInRange(x, y, Math.toRadians(22.5), Math.toRadians(-22.5), true))
                {
                    if (directionMagnitude.isMagnitudeEastWestMaximum(x, y))
                    {
                        setPixelValueWithThreshold(image, x, y, (int) directionMagnitude.getMagnitude(x, y));
                        continue;
                    }
                }
                else if (directionMagnitude.isDirectionInRange(x, y, Math.toRadians(67.5), Math.toRadians(22.5), true))
                {
                    if (directionMagnitude.isMagnitudeNorthEastSouthWestMaximum(x, y))
                    {
                        setPixelValueWithThreshold(image, x, y, (int) directionMagnitude.getMagnitude(x, y));
                        continue;
                    }
                }
                else if (directionMagnitude.isDirectionInRange(x, y, Math.toRadians(-67.5), Math.toRadians(67.5),
                        false))
                {
                    if (directionMagnitude.isMagnitudeNorthSouthMaximum(x, y))
                    {
                        setPixelValueWithThreshold(image, x, y, (int) directionMagnitude.getMagnitude(x, y));
                        continue;
                    }
                }
                else if (directionMagnitude.isDirectionInRange(x, y, Math.toRadians(-22.5), Math.toRadians(-67.5),
                        true))
                {
                    if (directionMagnitude.isMagnitudeNorthWestSouthEastMaximum(x, y))
                    {
                        setPixelValueWithThreshold(image, x, y, (int) directionMagnitude.getMagnitude(x, y));
                        continue;
                    }
                }

                setPixelValue(image, x, y, 0);
            }
        }

        return image;
    }

    private BufferedImage removeBorder(BufferedImage image)
    {
        int width = image.getWidth();
        int height = image.getHeight();

        for (int x = 0; x < width; ++x)
        {
            setPixelValue(image, x, 0, 0);
            setPixelValue(image, x, height - 1, 0);
        }

        for (int y = 0; y < height; ++y)
        {
            setPixelValue(image, 0, y, 0);
            setPixelValue(image, width - 1, y, 0);
        }

        return image;
    }

    private void setPixelValue(BufferedImage image, int x, int y, int value)
    {
        image.getRaster().setPixel(x, y, new int[] { value });
    }

    private void setPixelValueWithThreshold(BufferedImage image, int x, int y, int value)
    {
        if (value < lowThreshold)
        {
            setPixelValue(image, x, y, 0);
        }
        else if (value < highThreshold)
        {
            setPixelValue(image, x, y, 128);
        }
        else
        {
            setPixelValue(image, x, y, 255);
        }
    }
}
