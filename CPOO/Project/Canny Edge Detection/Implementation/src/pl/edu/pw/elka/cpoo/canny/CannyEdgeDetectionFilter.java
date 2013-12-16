package pl.edu.pw.elka.cpoo.canny;

import pl.edu.pw.elka.cpoo.canny.filter.HysteresisFilter;
import pl.edu.pw.elka.cpoo.canny.filter.NonMaximumSuppressionFilter;
import pl.edu.pw.elka.cpoo.canny.gradient.DirectionAndMagnitude;
import pl.edu.pw.elka.cpoo.canny.gradient.DirectionAndMagnitudeComputer;
import pl.edu.pw.elka.cpoo.filter.CompositeImageFilter;
import pl.edu.pw.elka.cpoo.filter.IImageFilter;
import pl.edu.pw.elka.cpoo.filter.impl.HistogramNormalizationFilter;
import pl.edu.pw.elka.cpoo.filter.impl.KernelFilter;
import pl.edu.pw.elka.cpoo.kernel.impl.GaussianKernelGenerator;
import pl.edu.pw.elka.cpoo.kernel.impl.SobelKernelGenerator;
import pl.edu.pw.elka.cpoo.utilities.GrayscaleBufferedImage;

import java.awt.image.BufferedImage;

public class CannyEdgeDetectionFilter extends CompositeImageFilter
{
    private final int radius;
    private final double lowThreshold;
    private final double highThreshold;

    public CannyEdgeDetectionFilter(int radius, double lowThreshold, double highThreshold, IImageFilter... filters)
    {
        super(filters);

        this.radius = radius;
        this.lowThreshold = lowThreshold;
        this.highThreshold = highThreshold;
    }

    @Override
    protected BufferedImage performFiltering(BufferedImage input)
    {
        GrayscaleBufferedImage grayscaleImage = GrayscaleBufferedImage.getGrayscaleImage(input);

        BufferedImage sobelHorizontalImage = getSobelFilter(true).filter(grayscaleImage);
        BufferedImage sobelVerticalImage = getSobelFilter(false).filter(grayscaleImage);

        DirectionAndMagnitude directionAndMagnitude = DirectionAndMagnitudeComputer
                .computeGradientDirectionAndMagnitude(sobelVerticalImage, sobelHorizontalImage);

        BufferedImage edgesImage = getHysteresisFilter(directionAndMagnitude).filter(grayscaleImage);

        return edgesImage;
    }

    private IImageFilter getSobelFilter(boolean horizontal)
    {
        IImageFilter normalizationFilter = new HistogramNormalizationFilter();
        IImageFilter gaussianHorizontalFilter = new KernelFilter(new GaussianKernelGenerator(radius, true));
        IImageFilter gaussianVerticalFilter = new KernelFilter(new GaussianKernelGenerator(radius, false));
        IImageFilter sobelFilter = new KernelFilter(new SobelKernelGenerator(horizontal), gaussianVerticalFilter,
                gaussianHorizontalFilter, normalizationFilter);

        return sobelFilter;
    }

    private IImageFilter getHysteresisFilter(DirectionAndMagnitude directionAndMagnitude)
    {
        IImageFilter nonMaximumSuppresionFilter = new NonMaximumSuppressionFilter(directionAndMagnitude);
        IImageFilter hysteresisFilter = new HysteresisFilter(lowThreshold, highThreshold, nonMaximumSuppresionFilter);

        return hysteresisFilter;
    }
}
