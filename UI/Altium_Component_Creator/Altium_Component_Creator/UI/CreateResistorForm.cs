using Altium_Component_Creator.Data;
using Altium_Component_Creator.Models;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Linq;

namespace Altium_Component_Creator.UI
{
    public partial class CreateResistorForm : Form
    {
        public CreateResistorForm()
        {
            InitializeComponent();
            set_tab_order();
            initilize_comboxes();

            toleranceCheckBox.CheckedChanged += check_NA_fields;
            powerCheckBox.CheckedChanged += check_NA_fields;
            voltageCheckBox.CheckedChanged += check_NA_fields;
            datasheetCheckBox.CheckedChanged += check_NA_fields;
        }


        /// <summary>
        /// Sets the order of the selected field upon hitting the tab key.
        /// </summary>
        private void set_tab_order()
        {
            partNumberTextBox.TabIndex = 0;
            manufacturerTextBox.TabIndex = 1;
            valueTextBox.TabIndex = 2;
            unitCombox.TabIndex = 3;
            toleranceTextBox.TabIndex = 4;
            packageComboBox.TabIndex = 6;
            voltageTextBox.TabIndex = 7;
            powerTextBox.TabIndex = 8;
            datasheetPathTextBox.TabIndex = 9;
            footprintNameTextBox.TabIndex = 10;
            footprintPathTextBox.TabIndex = 11;
            schematicSymbolTextBox.TabIndex = 12;
            schematicPathTextBox.TabIndex = 13;
        }

        private void create_button_clicked(object sender, EventArgs e)
        {
            // validate 
            if (partNumberTextBox.Text.IsNullOrEmpty() || valueTextBox.Text.IsNullOrEmpty() ||
                manufacturerTextBox.Text.IsNullOrEmpty())
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
            if ((!validPower        && !powerCheckBox.Checked)    || 
                (!validVoltage      && !voltageCheckBox.Checked)   ||  
                (!validTolerance    && !toleranceCheckBox.Checked))
            {   
                MessageBox.Show("Invalid power,voltage,or tolerance input. Please enter numeric value only!");
                return;
            }


            ResistorTable newResistor = new ResistorTable()
            {
                PartNumber = partNumberTextBox.Text,
                Value = valueTextBox.Text + unitCombox.SelectedItem.ToString(),
                Package = packageComboBox.SelectedItem.ToString(),
                Power = powerCheckBox.Checked ? null : power,
                Voltage = voltageCheckBox.Checked ? null : voltage,
                Tolerance = toleranceCheckBox.Checked ? null : tolerance,
                Manufacturer = manufacturerTextBox.Text,
                FoorprintRef = footprintNameTextBox.Text,
                FootprintPath = footprintPathTextBox.Text,
                LibraryRef = schematicSymbolTextBox.Text,
                LibraryPath = schematicPathTextBox.Text,
                ComponentLink1Description = "Datasheet",
                ComponentLink1Url = datasheetCheckBox.Checked ? null : datasheetPathTextBox.Text
            };


            // Create description
            string description = "RES";
            string[] desriptionElements = { newResistor.Value , newResistor.Power.ToString() , newResistor.Tolerance.ToString(), newResistor.Voltage.ToString()};
            for(int i = 0; i < desriptionElements.Length; i++)
            {
                if (!desriptionElements[i].IsNullOrEmpty())
                {
                    if (i == 0)
                    {
                        description += $"_{desriptionElements[i]}";
                    }

                    if (i == 1)
                    {
                        description += $"_{desriptionElements[i]}W";
                    }

                    if (i == 2)
                    {
                        description += $"_{desriptionElements[i]}%";
                    }

                    if (i == 3)
                    {
                        description += $"_{desriptionElements[i]}V";
                    }
                }
            }
            newResistor.Description = description;


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


        private void initilize_comboxes()
        {   
            string[] unitOptions = {"R","K","M"};
            unitCombox.Items.AddRange(unitOptions);
            // set the default unit to "R" which is index "0"
            unitCombox.SelectedIndex = 0;

            string[] packageOptions = {"0201", "0402", "0603", "0805", "1210","TH"};
            packageComboBox.Items.AddRange(packageOptions);
            // set the default unit to "0603" which is index "0"
            packageComboBox.SelectedIndex = 2;
        }


        private void check_NA_fields(object sender, EventArgs e)
        {
            if (toleranceCheckBox.Checked)
            {
                toleranceTextBox.Enabled = false;
            }
            else
            {
                toleranceTextBox.Enabled = true;
            }


            if (powerCheckBox.Checked)
            {
                powerTextBox.Enabled = false;
            }
            else
            {
                powerTextBox.Enabled = true;
            }


            if (voltageCheckBox.Checked)
            {
                voltageTextBox.Enabled = false;
            }
            else
            {
                voltageTextBox.Enabled = true;
            }

            if (datasheetCheckBox.Checked)
            {
                datasheetPathTextBox.Enabled = false;
            }
            else
            {
                datasheetPathTextBox.Enabled = true;
            }
        }
    }
}
