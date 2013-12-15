package pl.edu.pw.elka.cpoo.edge;

import java.awt.image.BufferedImage;
import java.awt.image.ConvolveOp;
import java.awt.image.Kernel;

public class SobelOperatorFilteringPerformer
{
    public static BufferedImage filter(BufferedImage inputImage, boolean horizontal)
    {
        Kernel kernel = SobelOperatorKernelGenerator.generateSobelOperatorKernel(horizontal);

        return new ConvolveOp(kernel, ConvolveOp.EDGE_NO_OP, null).filter(inputImage, null);
    }
}
