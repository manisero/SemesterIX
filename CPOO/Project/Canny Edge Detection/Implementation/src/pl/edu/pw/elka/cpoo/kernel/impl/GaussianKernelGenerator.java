package pl.edu.pw.elka.cpoo.kernel.impl;

import pl.edu.pw.elka.cpoo.kernel.IKernelGenerator;

import java.awt.image.Kernel;

public class GaussianKernelGenerator implements IKernelGenerator
{
    private final int radius;
    private final boolean horizontal;

    public GaussianKernelGenerator(int radius, boolean horizontal)
    {
        this.radius = radius;
        this.horizontal = horizontal;
    }

    @Override
    public Kernel generateKernel()
    {
        if (radius < 1)
        {
            throw new IllegalArgumentException("Gaussian blur kernel must be positive integer greater or equal to one");
        }

        int kernelSize = 2 * radius + 1;
        float[] data = new float[kernelSize];
        float sigma = radius / 3.0f;
        float twoSigmaSquare = 2 * sigma * sigma;
        float squareRootTwoPiSigmaSquare = (float) Math.sqrt(twoSigmaSquare * Math.PI);
        float total = 0.0f;

        for (int i = -radius; i <= radius; i++)
        {
            int distance = radius * radius;
            float value = (float) Math.exp(-distance / twoSigmaSquare) / squareRootTwoPiSigmaSquare;

            data[i + radius] = value;
            total += value;
        }

        for (int i = 0; i < kernelSize; i++)
        {
            data[i] /= total;
        }

        if (horizontal)
        {
            return new Kernel(kernelSize, 1, data);
        }

        return new Kernel(1, kernelSize, data);
    }
}
