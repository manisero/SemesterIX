package pl.edu.pw.elka.cpoo.edge;

import java.awt.image.BufferedImage;
import java.awt.image.ConvolveOp;
import java.awt.image.Kernel;

public class SobelOperatorFilteringPerformer
{
    public static BufferedImage filter(BufferedImage inputImage)
    {
        Kernel kernel0Degrees = SobelOperatorKernelGenerator.generateSobelOperatorKernel(
                SobelOperatorKernelGenerator.SOBEL_0_DEGREES);
        Kernel kernel45Degrees = SobelOperatorKernelGenerator.generateSobelOperatorKernel(
                SobelOperatorKernelGenerator.SOBEL_45_DEGREES);
        Kernel kernel90Degrees = SobelOperatorKernelGenerator.generateSobelOperatorKernel(
                SobelOperatorKernelGenerator.SOBEL_90_DEGREES);
        Kernel kernel135Degrees = SobelOperatorKernelGenerator.generateSobelOperatorKernel(
                SobelOperatorKernelGenerator.SOBEL_135_DEGREES);

        BufferedImage result = new ConvolveOp(kernel0Degrees, ConvolveOp.EDGE_NO_OP, null).filter(inputImage, null);
        result = new ConvolveOp(kernel45Degrees, ConvolveOp.EDGE_NO_OP, null).filter(result, null);
        result = new ConvolveOp(kernel90Degrees, ConvolveOp.EDGE_NO_OP, null).filter(result, null);
        result = new ConvolveOp(kernel135Degrees, ConvolveOp.EDGE_NO_OP, null).filter(result, null);

        return result;
    }
}
