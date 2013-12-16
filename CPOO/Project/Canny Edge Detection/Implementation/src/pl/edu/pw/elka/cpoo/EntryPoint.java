package pl.edu.pw.elka.cpoo;

import pl.edu.pw.elka.cpoo.gui.MainPanel;

import javax.swing.*;

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
        frame.setResizable(false);
        frame.setSize(800, 300);
        frame.setVisible(true);
    }
}
