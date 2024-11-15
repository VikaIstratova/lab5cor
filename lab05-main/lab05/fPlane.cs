using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лаб_6___2__
{
    public partial class fPlane : Form
    {
        public Plane ThePlane;

        public fPlane(Plane t)
        {
            ThePlane = t;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ThePlane.Name = tbName.Text.Trim();
            ThePlane.Country = tbCountry.Text.Trim();
            ThePlane.Producer = tbProducer.Text.Trim();
            ThePlane.Seats = int.Parse(tbSeats.Text.Trim());
            ThePlane.Max_speed = double.Parse(tbMaxSpeed.Text.Trim());
            ThePlane.Time = double.Parse(tbTime.Text.Trim());
            ThePlane.Toilet = chbToilet.Checked;
            ThePlane.Duty_free = chbDuty_free.Checked;
            DialogResult = DialogResult.OK;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void fPlane_Load(object sender, EventArgs e)
        {
            if (ThePlane != null)
            {
                tbName.Text = ThePlane.Name;
                tbCountry.Text = ThePlane.Country;
                tbProducer.Text = ThePlane.Producer;
                tbSeats.Text = ThePlane.Seats.ToString();
                tbMaxSpeed.Text = ThePlane.Max_speed.ToString();
                tbTime.Text = ThePlane.Time.ToString();
                chbToilet.Checked = ThePlane.Toilet;
                chbDuty_free.Checked = ThePlane.Duty_free;

            }
        }
    }
}
