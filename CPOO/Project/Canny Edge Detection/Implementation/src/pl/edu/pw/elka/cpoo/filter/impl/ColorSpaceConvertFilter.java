package pl.edu.pw.elka.cpoo.filter.impl;

import pl.edu.pw.elka.cpoo.filter.CompositeImageFilter;
import pl.edu.pw.elka.cpoo.filter.IImageFilter;

import java.awt.color.ColorSpace;
import java.awt.image.BufferedImage;
import java.awt.image.ColorConvertOp;

public class ColorSpaceConvertFilter extends CompositeImageFilter
{
    private final int colorSpace;

    public ColorSpaceConvertFilter(int colorSpace, IImageFilter... filters)
    {
        super(filters);

        this.colorSpace = colorSpace;
    }

    @Override
    protected BufferedImage performFiltering(BufferedImage input)
    {
        return new ColorConvertOp(ColorSpace.getInstance(colorSpace), null).filter(input, null);
    }
}
