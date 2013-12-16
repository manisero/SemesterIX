package pl.edu.pw.elka.cpoo.canny;

public class GradientDirectionMagnitude
{
    private final double[][] direction;
    private final double[][] magnitude;

    public GradientDirectionMagnitude(int width, int height)
    {
        direction = new double[width][height];
        magnitude = new double[width][height];
    }

    public double getDirection(int x, int y)
    {
        return direction[x][y];
    }

    public void setDirection(int x, int y, double value)
    {
        direction[x][y] = value;
    }

    public double getMagnitude(int x, int y)
    {
        return magnitude[x][y];
    }

    public void setMagnitude(int x, int y, double value)
    {
        magnitude[x][y] = value;
    }

    public boolean isDirectionInRange(int x, int y, double lowerThan, double higherOrEqualThan, boolean conjunction)
    {
        if (conjunction)
        {
            return getDirection(x, y) < lowerThan && getDirection(x, y) >= higherOrEqualThan;
        }

        return getDirection(x, y) < lowerThan || getDirection(x, y) >= higherOrEqualThan;
    }

    public boolean isMagnitudeEastWestMaximum(int x, int y)
    {
        return getMagnitude(x, y) > getMagnitude(x - 1, y) && getMagnitude(x, y) > getMagnitude(x + 1, y);
    }

    public boolean isMagnitudeNorthSouthMaximum(int x, int y)
    {
        return getMagnitude(x, y) > getMagnitude(x, y - 1) && getMagnitude(x, y) > getMagnitude(x, y + 1);
    }

    public boolean isMagnitudeNorthEastSouthWestMaximum(int x, int y)
    {
        return getMagnitude(x, y) > getMagnitude(x - 1, y - 1) && getMagnitude(x, y) > getMagnitude(x + 1, y + 1);
    }

    public boolean isMagnitudeNorthWestSouthEastMaximum(int x, int y)
    {
        return getMagnitude(x, y) > getMagnitude(x - 1, y + 1) && getMagnitude(x, y) > getMagnitude(x + 1, y - 1);
    }
}
