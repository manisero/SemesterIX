package pl.edu.pw.elka.cpoo.grayscale;

import java.awt.color.ColorSpace;
import java.awt.image.BufferedImage;
import java.awt.image.ColorConvertOp;

public class GrayscaleFilteringPerformer
{
    public static BufferedImage filter(BufferedImage inputImage)
    {
        ColorConvertOp grayscale = new ColorConvertOp(ColorSpace.getInstance(ColorSpace.CS_GRAY), null);
        return grayscale.filter(inputImage, null);
    }
}
