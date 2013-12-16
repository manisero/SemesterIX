package pl.edu.pw.elka.cpoo.filter.impl;

import pl.edu.pw.elka.cpoo.filter.IImageFilter;

import java.awt.color.ColorSpace;
import java.awt.image.BufferedImage;
import java.awt.image.ColorConvertOp;

public class ColorSpaceConvertFilter implements IImageFilter
{
    private final int colorSpace;

    public ColorSpaceConvertFilter(int colorSpace)
    {
        this.colorSpace = colorSpace;
    }

    @Override
    public BufferedImage filter(BufferedImage input)
    {
        return new ColorConvertOp(ColorSpace.getInstance(colorSpace), null).filter(input, null);
    }
}
