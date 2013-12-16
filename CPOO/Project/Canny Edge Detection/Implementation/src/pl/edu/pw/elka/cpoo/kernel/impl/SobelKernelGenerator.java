package pl.edu.pw.elka.cpoo.kernel.impl;

import pl.edu.pw.elka.cpoo.kernel.IKernelGenerator;

import java.awt.image.Kernel;

public class SobelKernelGenerator implements IKernelGenerator
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

    private final boolean horizontal;

    public SobelKernelGenerator(boolean horizontal)
    {
        this.horizontal = horizontal;
    }

    @Override
    public Kernel generateKernel()
    {
        float[] mask = getMask();
        return new Kernel(3, 3, mask);
    }

    private float[] getMask()
    {
        return horizontal ? SOBEL_HORIZONTAL_MASK : SOBEL_VERTICAL_MASK;
    }
}
