package pl.edu.pw.elka.cpoo.canny;

import pl.edu.pw.elka.cpoo.blur.GaussianBlurFilteringPerformer;
import pl.edu.pw.elka.cpoo.edge.SobelOperatorFilteringPerformer;
import pl.edu.pw.elka.cpoo.grayscale.GrayscaleFilteringPerformer;

import java.awt.image.BufferedImage;

public class CannyEdgeDetector
{
    private final int radius;

    public CannyEdgeDetector(int radius)
    {
        this.radius = radius;
    }

    public BufferedImage filter(BufferedImage inputImage)
    {
        BufferedImage grayScaleImage = GrayscaleFilteringPerformer.filter(inputImage);
        BufferedImage blurredImage = GaussianBlurFilteringPerformer.filter(grayScaleImage, radius);
        BufferedImage sobelHorizontalImage = SobelOperatorFilteringPerformer.filter(blurredImage, true);
        BufferedImage sobelVerticalImage = SobelOperatorFilteringPerformer.filter(sobelHorizontalImage, false);

        return sobelVerticalImage;
    }


}
