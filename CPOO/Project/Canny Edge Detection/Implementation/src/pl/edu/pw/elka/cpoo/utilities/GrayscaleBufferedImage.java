package pl.edu.pw.elka.cpoo.utilities;

import pl.edu.pw.elka.cpoo.filter.impl.ColorSpaceConvertFilter;

import java.awt.*;
import java.awt.color.ColorSpace;
import java.awt.image.BufferedImage;

public class GrayscaleBufferedImage extends BufferedImage
{
    private GrayscaleBufferedImage(BufferedImage image)
    {
        super(image.getWidth(), image.getHeight(), image.getType());
        cloneGraphics(image);
    }

    private void cloneGraphics(BufferedImage image)
    {
        Graphics graphics = getGraphics();
        graphics.drawImage(image, 0, 0, null);
        graphics.dispose();
    }

    public static GrayscaleBufferedImage getGrayscaleImage(BufferedImage image)
    {
        BufferedImage grayscaleImage = image;

        if (grayscaleImage.getType() != BufferedImage.TYPE_BYTE_GRAY)
        {
            grayscaleImage = new ColorSpaceConvertFilter(ColorSpace.CS_GRAY).filter(image);
        }

        return new GrayscaleBufferedImage(grayscaleImage);
    }

    public int getPixelValue(int x, int y)
    {
        return getRaster().getPixel(x, y, new int[1])[0];
    }

    public void setPixelValue(int x, int y, int value)
    {
        getRaster().setPixel(x, y, new int[] { value });
    }
}
