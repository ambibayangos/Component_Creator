using Altium_Component_Creator.Data;
using Altium_Component_Creator.Models;
using Altium_Component_Creator.UI;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Windows.Forms;

namespace Altium_Component_Creator
{
    public partial class MainForm : Form
    {

        //List<Type> selectedTable = new List<Type>();
        Dictionary<ComponentCategory,Type> databaseTableTypes = new Dictionary<ComponentCategory, Type>();

        #region Constructor
        public MainForm()
        {
            InitializeComponent();


            // COMBO BOX
            set_comboBox_options();
            set_table_types();
            // repopulate component view to selected component category in the UI
            categoryComboxBox.SelectedIndexChanged += populate_datagrid;
            // make resistor default category
            categoryComboxBox.SelectedIndex = (int)ComponentCategory.RESISTOR;


            // Save changes made in cells to dB whenerver editing on a cell is completed (e.g., when enter is prssed)
            databaseDatagridView.CellEndEdit += save_changes_to_cell;
        }
        #endregion



        #region Private Methods
        private void populate_datagrid(object sender, EventArgs e)
        {
            ComponentCategory selectedCategory = (ComponentCategory)categoryComboxBox.SelectedIndex;
            switch (selectedCategory)
            {
                case ComponentCategory.RESISTOR:
                    List<ResistorTable> resistors = new List<ResistorTable>();
                    // get resistor items from dB
                    using (TestAltiumDBContext db = new TestAltiumDBContext())
                    {
                        resistors = db.ResistorTables.ToList();
                    }
                    // display resistor items on UI
                    databaseDatagridView.DataSource = resistors;
                    break;
                case ComponentCategory.CAPACITOR:
                    List<CapacitorTable> capacitors = new List<CapacitorTable>();
                    // get capacitor items from dB
                    using (TestAltiumDBContext db = new TestAltiumDBContext())
                    {
                        capacitors = db.CapacitorTables.ToList();
                    }
                    // display capacitor items on UI
                    databaseDatagridView.DataSource = capacitors;
                    break;
                default:
                    break;
            }
        }

        private void refresh_datagrid()
        {
            populate_datagrid(this, EventArgs.Empty);
        }
        private void set_comboBox_options()
        {
            string[] options = {ComponentCategory.RESISTOR.ToString(),
                                ComponentCategory.CAPACITOR.ToString()};
            categoryComboxBox.Items.AddRange(options);
        }

        private void set_table_types()
        {
            databaseTableTypes.Add(ComponentCategory.RESISTOR, typeof(ResistorTable));
            databaseTableTypes.Add(ComponentCategory.CAPACITOR, typeof(CapacitorTable));
        }


        private void addNewItemButton_Click(object sender, EventArgs e)
        {
            CreateResistorForm createResistorForm = new CreateResistorForm();
            createResistorForm.ShowDialog(); // Open form and block other forms while this form is active.
            refresh_datagrid();
        }

        private void deleteItemButton_Click(object sender, EventArgs e)
        {

            DialogResult confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                    "Confirm Delete!!",
                                    MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;
            }

            DataGridViewSelectedRowCollection selectedRows = databaseDatagridView.SelectedRows;
            foreach(DataGridViewRow row in selectedRows)
            {
                // Extract the value of the current datagrid row at column index '0' which is the partnumber.
                string partNumber = row.Cells[0].Value.ToString();


                ComponentCategory selectedCategory = (ComponentCategory)categoryComboxBox.SelectedIndex;
                switch (selectedCategory)
                {
                    case ComponentCategory.RESISTOR:
                        using (TestAltiumDBContext dB = new TestAltiumDBContext())
                        {
                            dB.ResistorTables.Where(i => i.PartNumber == partNumber).ExecuteDelete();
                        }
                        break;
                    case ComponentCategory.CAPACITOR:
                        using (TestAltiumDBContext dB = new TestAltiumDBContext())
                        {
                            dB.CapacitorTables.Where(i => i.PartNumber == partNumber).ExecuteDelete();
                        }
                        break;
                    default:
                        break;
                }

                refresh_datagrid();
            }
        }


        private void save_changes_to_cell(object sender, EventArgs e)
        {
            DataGridViewCell selectedCell = databaseDatagridView.CurrentCell;

            using (TestAltiumDBContext dB = new TestAltiumDBContext())
            {
                ComponentCategory selectedCategory = (ComponentCategory)categoryComboxBox.SelectedIndex;

                switch (selectedCategory)
                {
                    case ComponentCategory.RESISTOR:

                        // Find which field is being edited
                        PropertyInfo[] resistorProperties = typeof(ResistorTable).GetProperties();
                        foreach (PropertyInfo property in resistorProperties)
                        {
                            if (property.Name == selectedCell.OwningColumn.HeaderText)
                            {
                                // get the part number of the item being edited
                                DataGridViewRow currentRow = databaseDatagridView.CurrentRow;
                                string partNumber = currentRow.Cells[0].Value.ToString();

                                // seach database for item that matches partnumber
                                ResistorTable resistor = dB.ResistorTables.Where(i => i.PartNumber == partNumber).FirstOrDefault();
                                property.SetValue(resistor, selectedCell.Value);
                                //save changes to database
                                dB.SaveChanges();
                                break;
                            }
                        }

                        break;
                    case ComponentCategory.CAPACITOR:

                        // Find which field is being edited
                        PropertyInfo[] capacitorProperties = typeof(CapacitorTable).GetProperties();
                        foreach (PropertyInfo property in capacitorProperties)
                        {
                            if (property.Name == selectedCell.OwningColumn.HeaderText)
                            {
                                // get the part number of the item being edited
                                DataGridViewRow currentRow = databaseDatagridView.CurrentRow;
                                string partNumber = currentRow.Cells[0].Value.ToString();

                                // seach database for item that matches partnumber
                                CapacitorTable resistor = dB.CapacitorTables.Where(i => i.PartNumber == partNumber).FirstOrDefault();
                                property.SetValue(resistor, selectedCell.Value);
                                // save changes to database
                                dB.SaveChanges();
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            refresh_datagrid();
        }


        #endregion


        #region ENUMS
        private enum ComponentCategory
        {
            NA = -1,
            RESISTOR = 0,
            CAPACITOR = 1
        }
        #endregion

    }
}
