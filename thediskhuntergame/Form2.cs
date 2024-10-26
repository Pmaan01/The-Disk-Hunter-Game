using System.Windows.Forms;

namespace thediskhuntergame
{
    public partial class Form2 : Form
    {
        // Properties to store the selected colors
        public Color BackgroundColor { get; private set; }  // The color for the background
        public Color BoardColor { get; private set; }       // The color for the board
        public Color DiskColor { get; private set; }        // The color for the disk

        // Constructor to initialize the form
        public Form2()
        {
            BackgroundColor = Color.Linen;
            BoardColor = Color.Black;
            DiskColor = Color.White;
            InitializeComponent();  // Initialize the components (UI elements) of the form
        }

        // Event handler for the button that selects the background color
        private void button1_Click(object sender, EventArgs e)
        {
            // Show the color dialog for the background color
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                // If the user selects a color, assign it to the BackgroundColor property
                BackgroundColor = colorDialog1.Color;
            }
        }

        // Event handler for the button that selects the board color
        private void button2_Click(object sender, EventArgs e)
        {
            // Show the color dialog for the board color
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                // If the user selects a color, assign it to the BoardColor property
                BoardColor = colorDialog2.Color;
            }
        }

        // Event handler for the button that selects the disk color
        private void button3_Click(object sender, EventArgs e)
        {
            // Show the color dialog for the disk color
            if (colorDialog3.ShowDialog() == DialogResult.OK)
            {
                // If the user selects a color, assign it to the DiskColor property
                DiskColor = colorDialog3.Color;
            }
        }

        // Event handler for the 'Save' or 'OK' button that confirms the selected colors
        private void button4_Click(object sender, EventArgs e)
        {
            // Set the dialog result to 'OK' to indicate the user has confirmed the changes
            this.DialogResult = DialogResult.OK;

            // Close the settings form after confirmation
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Close the settings form after confirmation
            this.Close();
        }
    }
}
