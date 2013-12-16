package pl.edu.pw.elka.cpoo;

import pl.edu.pw.elka.cpoo.canny.CannyEdgeDetectionFilter;
import pl.edu.pw.elka.cpoo.gui.MainPanel;
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
        frame.add(new MainPanel());
        frame.pack();
        frame.setVisible(true);
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

        CannyEdgeDetectionFilter detector = new CannyEdgeDetectionFilter(2, 20, 50);
        return detector.filter(image);
    }
}
