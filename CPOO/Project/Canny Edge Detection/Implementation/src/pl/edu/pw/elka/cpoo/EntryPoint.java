package pl.edu.pw.elka.cpoo;

import pl.edu.pw.elka.cpoo.blur.GaussianBlurFilteringPerformer;
import pl.edu.pw.elka.cpoo.canny.CannyEdgeDetector;
import pl.edu.pw.elka.cpoo.edge.SobelOperatorFilteringPerformer;
import pl.edu.pw.elka.cpoo.grayscale.GrayscaleFilteringPerformer;
import pl.edu.pw.elka.cpoo.reader.ImageReader;

import javax.swing.*;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;

public class EntryPoint
{
    public static void main(String[] args)
    {
        SwingUtilities.invokeLater(new Runnable()
        {
            @Override
            public void run()
            {
                createGUI();
            }
        });
    }

    public static void createGUI()
    {
        JFrame frame = new JFrame("Canny");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.add(createMainPanel());
        frame.pack();
        frame.setVisible(true);
    }

    public static JPanel createMainPanel()
    {
        JPanel panel = new JPanel(new BorderLayout());
        panel.add(createImageLabel());

        return panel;
    }

    public static JLabel createImageLabel()
    {
        JLabel label = new JLabel();
        label.setIcon(new ImageIcon(getFilteredImage()));

        return label;
    }

    public static BufferedImage getFilteredImage()
    {
        File imageFile = new File("lena.bmp");
        BufferedImage image = ImageReader.readImage(imageFile);

        CannyEdgeDetector detector = new CannyEdgeDetector(2, 20, 50);

        return detector.filter(image);
    }
}
