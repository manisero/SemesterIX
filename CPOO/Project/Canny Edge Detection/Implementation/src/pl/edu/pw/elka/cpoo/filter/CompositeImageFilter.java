package pl.edu.pw.elka.cpoo.filter;

import java.awt.image.BufferedImage;

public class CompositeImageFilter implements IImageFilter
{
    private IImageFilter[] filters;

    public CompositeImageFilter(IImageFilter... filters)
    {
        this.filters = filters;
    }

    @Override
    public BufferedImage filter(BufferedImage input)
    {
        for (IImageFilter filter : filters)
        {
            input = filter.filter(input);
        }

        return input;
    }
}
