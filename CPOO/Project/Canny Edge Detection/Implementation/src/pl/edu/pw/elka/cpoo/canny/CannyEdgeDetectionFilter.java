package pl.edu.pw.elka.cpoo.canny;

import pl.edu.pw.elka.cpoo.canny.filter.HysteresisFilter;
import pl.edu.pw.elka.cpoo.canny.filter.NonMaximumSuppressionFilter;
import pl.edu.pw.elka.cpoo.canny.gradient.DirectionAndMagnitude;
import pl.edu.pw.elka.cpoo.canny.gradient.DirectionAndMagnitudeComputer;
import pl.edu.pw.elka.cpoo.canny.gradient.SobelComputer;
import pl.edu.pw.elka.cpoo.filter.CompositeImageFilter;
import pl.edu.pw.elka.cpoo.filter.IImageFilter;
import pl.edu.pw.elka.cpoo.filter.impl.HistogramNormalizationFilter;
import pl.edu.pw.elka.cpoo.filter.impl.KernelFilter;
import pl.edu.pw.elka.cpoo.kernel.impl.GaussianKernelGenerator;
import pl.edu.pw.elka.cpoo.utilities.GrayscaleBufferedImage;

import java.awt.image.BufferedImage;

public class CannyEdgeDetectionFilter implements IImageFilter
{
    private final int radius;
    private final int lowThreshold;
    private final int highThreshold;

    public CannyEdgeDetectionFilter(int radius, int lowThreshold, int highThreshold)
    {
        this.radius = radius;
        this.lowThreshold = lowThreshold;
        this.highThreshold = highThreshold;
    }

    @Override
    public BufferedImage filter(BufferedImage input)
    {
        GrayscaleBufferedImage grayscaleImage = GrayscaleBufferedImage.getGrayscaleImage(input);
        BufferedImage filteredImage = getInitialFilter() .filter(grayscaleImage);

        double[][] sobelGradientX = SobelComputer.computeGradientX(filteredImage);
        double[][] sobelGradientY = SobelComputer.computeGradientY(filteredImage);

        DirectionAndMagnitude directionAndMagnitude = DirectionAndMagnitudeComputer
                .computeGradientDirectionAndMagnitude(sobelGradientX, sobelGradientY);

        BufferedImage edgesImage = getEdgesFilter(directionAndMagnitude).filter(grayscaleImage);

        return edgesImage;
    }

    private IImageFilter getInitialFilter()
    {
        IImageFilter normalizationFilter = new HistogramNormalizationFilter();
        IImageFilter gaussianHorizontalFilter = new KernelFilter(new GaussianKernelGenerator(radius, true));
        IImageFilter gaussianVerticalFilter = new KernelFilter(new GaussianKernelGenerator(radius, false));
        IImageFilter initialFilter = new CompositeImageFilter(normalizationFilter, gaussianHorizontalFilter,
                gaussianVerticalFilter);

        return initialFilter;
    }

    private IImageFilter getEdgesFilter(DirectionAndMagnitude directionAndMagnitude)
    {
        IImageFilter nonMaximumSuppressionFilter = new NonMaximumSuppressionFilter(directionAndMagnitude,
                lowThreshold, highThreshold);
        IImageFilter hysteresisFilter = new HysteresisFilter();
        IImageFilter edgesFilter = new CompositeImageFilter(nonMaximumSuppressionFilter, hysteresisFilter);

        return edgesFilter;
    }
}
