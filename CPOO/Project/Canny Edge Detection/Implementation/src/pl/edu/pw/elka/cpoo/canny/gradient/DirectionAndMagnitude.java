package pl.edu.pw.elka.cpoo.canny.gradient;

public class DirectionAndMagnitude
{
    private final double[][] direction;
    private final double[][] magnitude;

    public DirectionAndMagnitude(int width, int height)
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

    public int getMagnitudeWidth()
    {
        return magnitude.length;
    }

    public int getMagnitudeHeight()
    {
        return magnitude[0].length;
    }
}
