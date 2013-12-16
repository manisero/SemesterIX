package pl.edu.pw.elka.cpoo.gui;

import pl.edu.pw.elka.cpoo.canny.CannyEdgeDetectionFilter;
import pl.edu.pw.elka.cpoo.reader.ImageReader;

import javax.imageio.ImageIO;
import javax.swing.*;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;
import javax.swing.filechooser.FileFilter;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.image.BufferedImage;
import java.io.File;

public class MainPanel extends JPanel implements ActionListener, ChangeListener
{
    private static final int MIN_BLUR_RADIUS = 1;
    private static final int MAX_BLUR_RADIUS = 10;
    private static final int MIN_THRESHOLD = 0;
    private static final int MAX_THRESHOLD = 255;

    private static final String GAUSSIAN_BLUR_LABEL = "Promień rozmycia";
    private static final String LOW_THRESHOLD_LABEL = "Niski próg odcięcia";
    private static final String HIGH_THRESHOLD_LABEL = "Wysoki próg odcięcia";

    private JButton selectFileButton;
    private JButton filterButton;

    private File selectedFile;
    private int blurRadius = 1;
    private int lowThreshold = 60;
    private int highThreshold = 100;

    public MainPanel()
    {
        initializeLayout();
        initializeSelectFileButton();
        initializeRadiusSlider();
        initializeLowThresholdSlider();
        initializeHighThresholdSlider();
        initializePerformFilteringButton();
    }

    private void initializeLayout()
    {
        setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
    }

    private void initializeSelectFileButton()
    {
        selectFileButton = new JButton("Wybierz plik...");
        selectFileButton.addActionListener(this);
        add(selectFileButton);
    }

    private void initializeRadiusSlider()
    {
        JPanel sliderPane = constructSliderPane(GAUSSIAN_BLUR_LABEL, MIN_BLUR_RADIUS, MAX_BLUR_RADIUS, blurRadius, 1,
                3);
        add(sliderPane);
    }

    private JPanel constructSliderPane(String label, int minimum, int maximum, int initial, int minorTick,
                                       int majorTick)
    {
        JPanel pane = new JPanel();
        pane.setLayout(new BoxLayout(pane, BoxLayout.X_AXIS));
        pane.add(constructLabel(label));
        pane.add(constructSlider(minimum, maximum, initial, minorTick, majorTick));
        pane.add(constructValueField(initial));

        return pane;
    }

    private JLabel constructLabel(String text)
    {
        JLabel label = new JLabel(text);
        return label;
    }

    private JSlider constructSlider(int minimum, int maximum, int initial, int minorTick, int majorTick)
    {
        JSlider slider = new JSlider(JSlider.HORIZONTAL, minimum, maximum, initial);
        slider.setMajorTickSpacing(majorTick);
        slider.setMinorTickSpacing(minorTick);
        slider.setSnapToTicks(true);
        slider.setPaintTicks(false);
        slider.setPaintLabels(true);
        slider.addChangeListener(this);

        return slider;
    }

    private void initializeLowThresholdSlider()
    {
        JPanel sliderPane = constructSliderPane(LOW_THRESHOLD_LABEL, MIN_THRESHOLD, MAX_THRESHOLD, lowThreshold, 1, 10);
        add(sliderPane);
    }

    private void initializeHighThresholdSlider()
    {
        JPanel sliderPane = constructSliderPane(HIGH_THRESHOLD_LABEL, MIN_THRESHOLD, MAX_THRESHOLD, highThreshold, 1,
                10);
        add(sliderPane);
    }

    private JTextField constructValueField(int initial)
    {
        JTextField textField = new JTextField();
        textField.setEditable(false);
        textField.setText(String.valueOf(initial));
        textField.setMaximumSize(new Dimension(200, 20));

        return textField;
    }

    private void initializePerformFilteringButton()
    {
        filterButton = new JButton("Filtruj!");
        filterButton.addActionListener(this);
        add(filterButton);
    }

    @Override
    public void actionPerformed(ActionEvent event)
    {
        if (event.getSource() == selectFileButton)
        {
            openFileDialog();
        }
        else if (event.getSource() == filterButton)
        {
            processButtonClicked();
        }
    }

    private void openFileDialog()
    {
        JFileChooser fileChooser = createGraphicsFileChooser();

        if (fileChooser.showDialog(null, "Wybierz plik") == JFileChooser.APPROVE_OPTION)
        {
            selectedFile = fileChooser.getSelectedFile();
            selectFileButton.setText("Plik: " + selectedFile.getName() + "...");
        }
    }

    private JFileChooser createGraphicsFileChooser()
    {
        JFileChooser fileChooser = new JFileChooser();
        fileChooser.setDialogTitle("Wybierz plik graficzny...");
        fileChooser.setAcceptAllFileFilterUsed(false);
        fileChooser.setCurrentDirectory(new File(System.getProperty("user.dir")));

        String[] suffices = ImageIO.getReaderFileSuffixes();

        for (int i = 0; i < suffices.length; i++)
        {
            FileFilter filter = new FileNameExtensionFilter("Pliki " + suffices[i], suffices[i]);
            fileChooser.addChoosableFileFilter(filter);
        }

        fileChooser.setFileFilter(fileChooser.getChoosableFileFilters()[0]);

        return fileChooser;
    }

    private void processButtonClicked()
    {
        try
        {
            testSettingsAreValid();
            showFilteredImage(processImage());
        }
        catch (InvalidSettingsException e)
        {
            JOptionPane.showMessageDialog(this, e.getMessage(), "Błąd walidacji", JOptionPane.ERROR_MESSAGE);
        }
    }

    private BufferedImage processImage()
    {
        BufferedImage image = ImageReader.readImage(selectedFile);
        CannyEdgeDetectionFilter detector = new CannyEdgeDetectionFilter(blurRadius, lowThreshold, highThreshold);

        return detector.filter(image);
    }

    private void showFilteredImage(BufferedImage filteredImage)
    {
        JLabel imageLabel = new JLabel();
        imageLabel.setIcon(new ImageIcon(filteredImage));

        JPanel pane = new JPanel();
        pane.add(imageLabel);

        JFrame frame = new JFrame("Wynik działania dla: " + selectedFile.getName());
        frame.add(pane);
        frame.pack();
        frame.setResizable(false);
        frame.setVisible(true);
    }

    private void testSettingsAreValid()
    {
        if (selectedFile == null)
        {
            throw new InvalidSettingsException("Nie wybrano pliku graficznego!");
        }
        else if (highThreshold <= lowThreshold)
        {
            throw new InvalidSettingsException("Niski próg odcięcia musi mieć mniejszą wartość niż wysoki próg " +
                    "odcięcia!");
        }
    }

    @Override
    public void stateChanged(ChangeEvent e)
    {
        JSlider source = (JSlider) e.getSource();

        if (!source.getValueIsAdjusting())
        {
            JPanel pane = (JPanel) source.getParent();
            JLabel label = (JLabel) pane.getComponent(0);

            if (label.getText().equals(GAUSSIAN_BLUR_LABEL))
            {
                blurRadius = source.getValue();
            }
            else if (label.getText().equals(LOW_THRESHOLD_LABEL))
            {
                lowThreshold = source.getValue();
            }
            else if (label.getText().equals(HIGH_THRESHOLD_LABEL))
            {
                highThreshold = source.getValue();
            }

            JTextField textField = (JTextField) pane.getComponent(2);
            textField.setText("" + source.getValue());
        }
    }
}
