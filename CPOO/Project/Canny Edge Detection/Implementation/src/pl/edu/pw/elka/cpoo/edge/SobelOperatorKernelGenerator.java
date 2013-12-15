package pl.edu.pw.elka.cpoo.edge;

import java.awt.image.Kernel;

public class SobelOperatorKernelGenerator
{
    private static final float[] SOBEL_HORIZONTAL_MASK = new float[]
    {
        +1.0f, 0.0f, -1.0f,
        +2.0f, 0.0f, -2.0f,
        +1.0f, 0.0f, -1.0f
    };

    private static final float[] SOBEL_VERTICAL_MASK = new float[]
    {
        +1.0f, +2.0f, +1.0f,
         0.0f,  0.0f,  0.0f,
        -1.0f, -2.0f, -1.0f
    };

    public static Kernel generateSobelOperatorKernel(boolean horizontal)
    {
        if (horizontal)
        {
             return new Kernel(3, 3, SOBEL_HORIZONTAL_MASK);
        }

        return new Kernel(3, 3, SOBEL_VERTICAL_MASK);
    }
}
