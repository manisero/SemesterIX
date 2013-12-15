package pl.edu.pw.elka.cpoo.edge;

import com.google.common.primitives.Floats;

import java.awt.image.Kernel;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

public class SobelOperatorKernelGenerator
{
    public static final float SOBEL_0_DEGREES = 0.0f;
    public static final float SOBEL_45_DEGREES = 45.0f;
    public static final float SOBEL_90_DEGREES = 90.0f;
    public static final float SOBEL_135_DEGREES = 135.0f;

    private static final float[] SOBEL_0_DEGREES_MASK = new float[]
    {
        -1.0f, 0.0f, +1.0f,
        -2.0f, 0.0f, +2.0f,
        -1.0f, 0.0f, +1.0f
    };

    private static final float[] SOBEL_45_DEGREES_MASK = new float[]
    {
         0.0f, +1.0f, +2.0f,
        -1.0f,  0.0f, +1.0f,
        -2.0f, -1.0f,  0.0f
    };

    private static final float[] SOBEL_90_DEGREES_MASK = new float[]
    {
        +1.0f, +2.0f, +1.0f,
         0.0f,  0.0f,  0.0f,
        -1.0f, -2.0f, -1.0f
    };

    private static final float[] SOBEL_135_DEGREES_MASK = new float[]
    {
        +2.0f, +1.0f,  0.0f,
        +1.0f,  0.0f, -1.0f,
         0.0f, -1.0f, -2.0f
    };

    private static final Map<Float, List<Float>> masks = new LinkedHashMap<Float, List<Float>>();

    static
    {
        masks.put(SOBEL_0_DEGREES, Floats.asList(SOBEL_0_DEGREES_MASK));
        masks.put(SOBEL_45_DEGREES, Floats.asList(SOBEL_45_DEGREES_MASK));
        masks.put(SOBEL_90_DEGREES, Floats.asList(SOBEL_90_DEGREES_MASK));
        masks.put(SOBEL_135_DEGREES, Floats.asList(SOBEL_135_DEGREES_MASK));
    }

    public static Kernel generateSobelOperatorKernel(float degrees)
    {
        if (!masks.containsKey(degrees))
        {
            throw new SobelOperatorKernelGeneratorException("No such Sobel mask: " + degrees);
        }

        return new Kernel(3, 3, Floats.toArray(masks.get(degrees)));
    }
}
