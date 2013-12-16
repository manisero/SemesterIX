package pl.edu.pw.elka.cpoo.filter.impl;

import pl.edu.pw.elka.cpoo.filter.IImageFilter;
import pl.edu.pw.elka.cpoo.kernel.IKernelGenerator;

import java.awt.image.BufferedImage;
import java.awt.image.ConvolveOp;
import java.awt.image.Kernel;

public class KernelFilter implements IImageFilter
{
    private final IKernelGenerator kernelGenerator;

    public KernelFilter(IKernelGenerator kernelGenerator)
    {
        this.kernelGenerator = kernelGenerator;
    }

    @Override
    public BufferedImage filter(BufferedImage input)
    {
        Kernel kernel = kernelGenerator.generateKernel();
        return new ConvolveOp(kernel, ConvolveOp.EDGE_NO_OP, null).filter(input, null);
    }
}
