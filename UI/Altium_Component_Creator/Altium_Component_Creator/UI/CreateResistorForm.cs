using Altium_Component_Creator.Data;
using Altium_Component_Creator.Models;
using Microsoft.IdentityModel.Tokens;

namespace Altium_Component_Creator.UI
{
    public partial class CreateResistorForm : Form
    {
        public CreateResistorForm()
        {
            InitializeComponent();
            set_tab_order();
        }


        /// <summary>
        /// Sets the order of the selected field upon hitting the tab key.
        /// </summary>
        private void set_tab_order()
        {
            partNumberTextBox.TabIndex = 0;
            manufacturerTextBox.TabIndex = 1;
            valueTextBox.TabIndex = 2;
            toleranceTextBox.TabIndex = 3;
            packageTextBox.TabIndex = 4;
            voltageTextBox.TabIndex = 5;
            powerTextBox.TabIndex = 6;
            datasheetPathTextBox.TabIndex = 7;
            footprintNameTextBox.TabIndex = 8;
            footprintPathTextBox.TabIndex = 9;
            schematicSymbolTextBox.TabIndex = 10;
            schematicPathTextBox.TabIndex = 11;
        }

        private void create_button_clicked(object sender, EventArgs e)
        {
            // validate 
            if (partNumberTextBox.Text.IsNullOrEmpty() || valueTextBox.Text.IsNullOrEmpty() ||
                packageTextBox.Text.IsNullOrEmpty() || manufacturerTextBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Part Number, Value, Package and Manufacturer can't be empty!");
                return;
            }

            //validate
            double power, voltage, tolerance;
            bool validPower = false;
            bool validVoltage = false;
            bool validTolerance = false;
            validPower = double.TryParse(powerTextBox.Text, out power);
            validVoltage = double.TryParse(voltageTextBox.Text, out voltage);
            validTolerance = double.TryParse(toleranceTextBox.Text, out tolerance);
            if (!validPower || !validVoltage || !validTolerance)
            {   
                MessageBox.Show("Invalid power,voltage,or tolerance input. Please enter numeric value only!");
                return;
            }


            ResistorTable newResistor = new ResistorTable()
            {
                PartNumber = partNumberTextBox.Text,
                Value = valueTextBox.Text,
                Package = packageTextBox.Text,
                Power = power,
                Voltage = voltage,
                Tolerance = tolerance,
                Manufacturer = manufacturerTextBox.Text,
                FoorprintRef = footprintNameTextBox.Text,
                FootprintPath = footprintPathTextBox.Text,
                LibraryRef = schematicSymbolTextBox.Text,
                LibraryPath = schematicPathTextBox.Text,
                ComponentLink1Description = "Datasheet",
                ComponentLink1Url = datasheetPathTextBox.Text
            };
            // Create description in following formnat "RES_{VALUE}_{PAKCAGE}_{Power}W_{Tolerance}%_{VOLTAGE}V"
            newResistor.Description = $"RES_{newResistor.Value}_{newResistor.Package}_{newResistor.Power}W_{newResistor.Tolerance}%_{newResistor.Voltage}V";


            using (TestAltiumDBContext db = new TestAltiumDBContext())
            {
                // true if there is a resistor with the partnumber entered in the partnumber textbox, false otherwise.
                bool alreadyExists = db.ResistorTables.Any(t => t.PartNumber == partNumberTextBox.Text);

                // check if component being created already exists.
                // only make it if not.
                if (!alreadyExists)
                {
                   db.ResistorTables.Add(newResistor);
                   db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("This component already exists!");
                    return;
                }

            }

            this.Close(); // close the add resistor form
        }

    }
}
