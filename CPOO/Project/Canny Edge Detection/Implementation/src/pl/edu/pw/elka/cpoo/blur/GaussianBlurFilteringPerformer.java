package pl.edu.pw.elka.cpoo.blur;

import java.awt.image.BufferedImage;
import java.awt.image.ConvolveOp;
import java.awt.image.Kernel;

public class GaussianBlurFilteringPerformer
{
    public static BufferedImage filter(BufferedImage inputImage, int radius)
    {
        Kernel horizontalKernel = GaussianBlurKernelGenerator.generateGaussianBlurKernel(radius, true);
        Kernel verticalKernel = GaussianBlurKernelGenerator.generateGaussianBlurKernel(radius, false);

        ConvolveOp horizontalConvolveOp = new ConvolveOp(horizontalKernel, ConvolveOp.EDGE_NO_OP, null);
        ConvolveOp verticalConvolveOp = new ConvolveOp(verticalKernel, ConvolveOp.EDGE_NO_OP, null);

        BufferedImage result = horizontalConvolveOp.filter(inputImage, null);
        result = verticalConvolveOp.filter(result, null);

        return result;
    }
}
