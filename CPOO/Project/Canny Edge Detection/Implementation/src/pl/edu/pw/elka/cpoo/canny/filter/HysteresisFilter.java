package pl.edu.pw.elka.cpoo.canny.filter;

import pl.edu.pw.elka.cpoo.filter.IImageFilter;
import pl.edu.pw.elka.cpoo.utilities.GrayscaleBufferedImage;

import java.awt.image.BufferedImage;

public class HysteresisFilter implements IImageFilter
{
    private GrayscaleBufferedImage outputImage;

    @Override
    public BufferedImage filter(BufferedImage input)
    {
        initializeOutputImage(input);

        GrayscaleBufferedImage grayscaleInput = GrayscaleBufferedImage.getGrayscaleImage(input);

        for (int x = 0; x < input.getWidth(); ++x)
        {
            for (int y = 0; y < input.getHeight(); ++y)
            {
                if (outputImage.getPixelValue(x, y) == 0 && isStrongPixel(grayscaleInput, x, y))
                {
                    follow(x, y, grayscaleInput);
                }
            }
        }

        return outputImage;
    }

    private boolean isStrongPixel(GrayscaleBufferedImage input, int x, int y)
    {
        return input.getPixelValue(x, y) == 255;
    }

    private void initializeOutputImage(BufferedImage input)
    {
        BufferedImage emptyImage = new BufferedImage(input.getWidth(), input.getHeight(), BufferedImage.TYPE_BYTE_GRAY);
        outputImage = GrayscaleBufferedImage.getGrayscaleImage(emptyImage);
    }

    private void follow(int x, int y, GrayscaleBufferedImage input)
    {
        int left = x - 1;
        int right = x + 1;
        int top = y - 1;
        int bottom = y + 1;

        if (left < 0)
            left = 0;

        if (right >= input.getWidth())
            right = input.getWidth() - 1;

        if (top < 0)
            top = 0;

        if (bottom >= input.getHeight())
            bottom = input.getHeight() + 1;

        outputImage.setPixelValue(x, y, 255);

        for (int i = left; x <= right; ++x)
        {
            for (int j = top; j <= bottom; ++j)
            {
                if (outputImage.getPixelValue(i, j) == 0 && isWeakPixel(input, i, j))
                {
                    follow(i, j, input);
                    return;
                }
            }
        }
    }

    private boolean isWeakPixel(GrayscaleBufferedImage input, int x, int y)
    {
        return input.getPixelValue(x, y) > 0 && input.getPixelValue(x, y) < 255;
    }
}
