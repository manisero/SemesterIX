package pl.edu.pw.elka.cpoo.reader;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

public class ImageReader
{
    public static BufferedImage readImage(File source)
    {
        try
        {
            return ImageIO.read(source);
        }
        catch (IOException e)
        {
            throw new ImageReaderException(e);
        }
    }
}
