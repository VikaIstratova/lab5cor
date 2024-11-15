using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Лаб_6___2__
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int buttonsSize = 9 * btnAdd.Width + 3 * tsSeparator1.Width + 30;
            btnExit.Margin = new Padding(Width - buttonsSize, 0, 0, 0);
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            gvPlanes.AutoGenerateColumns = false;

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Назва літака";
            gvPlanes.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Country";
            column.Name = "Країна";
            gvPlanes.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Producer";
            column.Name = "Виробник";
            gvPlanes.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Seats";
            column.Name = "Кількість місць";
            gvPlanes.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Max_Speed";
            column.Name = "Максимальна швидкість";
            gvPlanes.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Time";
            column.Name = "Час польоту";
            column.Width = 80;
            gvPlanes.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "Toilet";
            column.Name = "Туалет";
            column.Width = 60;
            gvPlanes.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "Duty_free";
            column.Name = "Дьюті-фрі";
            column.Width = 60;
            gvPlanes.Columns.Add(column);

            bindSrcPlanes.Add(new Plane("Мрія", "Україна", "Анна", 300, 500, 3600, true, false));
            EventArgs args = new EventArgs(); OnResize(args);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Plane plane = new Plane();

            fPlane ft = new fPlane( plane);
            if (ft.ShowDialog() == DialogResult.OK)
            {
                bindSrcPlanes.Add(plane); 
            }


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Plane plane = (Plane)bindSrcPlanes.List[bindSrcPlanes.Position];

            fPlane ft = new fPlane( plane);
            if (ft.ShowDialog() == DialogResult.OK)
            {
                bindSrcPlanes.List[bindSrcPlanes.Position] = plane;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Видалити поточний запис" , "Видалення запису" , MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                bindSrcPlanes.RemoveCurrent();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Очистити таблюцю ? \n \nВсі данні будуть втрачені ", "Очищення данних ", MessageBoxButtons.OKCancel , MessageBoxIcon.Question) == DialogResult.OK)
            {
                bindSrcPlanes.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрити застосунок" , "Вихід з програми" , MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnSaveAsText_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти дані у текстову форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;

            StreamWriter sw;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);
                try
                {
                    foreach (Plane plane in bindSrcPlanes.List)
                    {
                        sw.Write(plane.Name + "\t" + plane.Country + "\t" + plane.Producer + "\t" + plane.Seats + "\t" + plane.Max_speed + "\t" + plane.Time + "\t" + plane.Toilet + "\t" + plane.Duty_free + "\t\n");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        private void btnSaveAsBinary_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Файли даних (*.plane)|*.plane|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти дані у бінарному форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;

            BinaryWriter bw;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bw = new BinaryWriter(saveFileDialog.OpenFile());
                try
                {
                    foreach (Plane plane in bindSrcPlanes.List)
                    {
                        bw.Write(plane.Name);
                        bw.Write(plane.Country);
                        bw.Write(plane.Producer);
                        bw.Write(plane.Seats);
                        bw.Write(plane.Max_speed);
                        bw.Write(plane.Time);
                        bw.Write(plane.Toilet);
                        bw.Write(plane.Duty_free);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    bw.Close();
                }
            }
        }

        private void btnOpenFromText_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Прочитати дані у текстовому форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;

            StreamReader sr;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcPlanes.Clear();
                sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8);
                string s;
                try
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] split = s.Split('\t');
                        Plane plane = new Plane(split[0], split[1], split[2], int.Parse(split[3]),
                            double.Parse(split[4]), double.Parse(split[5]), bool.Parse(split[6]) , bool.Parse(split[7]));
                        bindSrcPlanes.Add(plane);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка: \n{0}",
                        ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sr.Close();
                }
            }
        }

        private void btnOpenFromBinary_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Файли даних (*.plane)|*.plane|All files(*.*)|*.*";
            openFileDialog.Title = "Прочитати дані у бінарному форматі ";
            openFileDialog.InitialDirectory = Application.StartupPath;

            BinaryReader br;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcPlanes.Clear();
                br = new BinaryReader(openFileDialog.OpenFile());
                try
                {
                    Plane plane;
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        plane = new Plane();
                        for (int i = 0; i <= 9; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    plane.Name = br.ReadString();
                                    break;
                                case 1:
                                    plane.Country = br.ReadString();
                                    break;
                                case 2:
                                    plane.Producer = br.ReadString();
                                    break;
                                case 3:
                                    plane.Seats = br.ReadInt32();
                                    break;
                                case 4:
                                    plane.Max_speed = br.ReadDouble();
                                    break;
                                case 5:
                                    plane.Time = br.ReadDouble();
                                    break;
                                case 6:
                                    plane.Toilet = br.ReadBoolean();
                                    break;
                                case 7:
                                    plane.Duty_free = br.ReadBoolean();
                                    break;
                            }
                        }
                        bindSrcPlanes.Add(plane);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка: \n{0}",
                        ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    br.Close();
                }
            }
        }
    }
}
