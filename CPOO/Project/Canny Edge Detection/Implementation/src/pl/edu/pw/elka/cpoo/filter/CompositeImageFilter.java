package pl.edu.pw.elka.cpoo.filter;

import java.awt.image.BufferedImage;

public abstract class CompositeImageFilter implements IImageFilter
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

        return performFiltering(input);
    }

    protected abstract BufferedImage performFiltering(BufferedImage input);
}
