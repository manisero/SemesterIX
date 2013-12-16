package pl.edu.pw.elka.cpoo.gui;

import javax.imageio.ImageIO;
import javax.jnlp.FileContents;
import javax.jnlp.FileOpenService;
import javax.jnlp.ServiceManager;
import javax.jnlp.UnavailableServiceException;
import javax.swing.*;
import javax.swing.filechooser.FileFilter;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;

public class MainPanel extends JPanel implements ActionListener
{
    private JButton selectFileButton;

    public MainPanel()
    {
        initializeLayout();
        initializeSelectFileButton();
        initializePerformFilteringButton();
    }

    public void initializeLayout()
    {
        setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
    }

    public void initializeSelectFileButton()
    {
        selectFileButton = new JButton("Wybierz plik...");
        selectFileButton.addActionListener(this);
        add(selectFileButton);
    }

    public void initializePerformFilteringButton()
    {
        JButton button = new JButton("Filtruj!");
        add(button);
    }

    @Override
    public void actionPerformed(ActionEvent event)
    {
        if (event.getSource() == selectFileButton)
        {
            openFileDialog();
        }
    }

    private void openFileDialog()
    {
        JFileChooser fileChooser = createGraphicsFileChooser();

        if (fileChooser.showDialog(null, "Wybierz plik") == JFileChooser.APPROVE_OPTION)
        {
            selectFileButton.setText("Plik: " + fileChooser.getSelectedFile().getName() + "...");
        }
    }

    private JFileChooser createGraphicsFileChooser()
    {
        JFileChooser fileChooser = new JFileChooser();
        fileChooser.setDialogTitle("Wybierz plik graficzny...");
        fileChooser.setAcceptAllFileFilterUsed(false);

        String[] suffices = ImageIO.getReaderFileSuffixes();

        for (int i = 0; i < suffices.length; i++)
        {
            FileFilter filter = new FileNameExtensionFilter("Pliki " + suffices[i], suffices[i]);
            fileChooser.addChoosableFileFilter(filter);
        }

        return fileChooser;
    }
}
