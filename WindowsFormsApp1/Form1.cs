using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class 图片像素批量修改工具 : Form
	{
		public 图片像素批量修改工具()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{

			Int32 value =0;
            progressBar1.Value = 0;
			OpenFileDialog openFileDialog = new OpenFileDialog();
	          
			openFileDialog.Filter = "BMP File(*.bmp)|*.bmp|JPEG File(*.jpeg)|*.jpg|PNG File(*.png)|*.png";
			openFileDialog.Multiselect = true;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{

				foreach (string filename in openFileDialog.FileNames)
				{

				    if( Image_Change(filename, System.IO.Path.GetFileName(filename)))
					{
						value++;
						progressBar1.Value = (int)value * 100 / openFileDialog.FileNames.Length;

					}

				}
				value =0;
				


			}
		}


		// 
		public bool  Image_Change(string FileName,string SaveFileName)
		{


			try
			{

				if ((Convert.ToInt32(horizontal.Text)!=0)&&( Convert.ToInt32(vertical.Text)!=0))

				{
					Image img = Image.FromFile(FileName);
					Image newImg = new Bitmap(Convert.ToInt32(horizontal.Text), Convert.ToInt32(vertical.Text));
					Graphics g = Graphics.FromImage(newImg);
					g.DrawImage(img, 0, 0, Convert.ToInt32(horizontal.Text), Convert.ToInt32(vertical.Text));
					newImg.Save(SaveFileName);
					img.Dispose();//释放资源
					newImg.Dispose();//释放资源
					return true;
				}
				else
				{

			
					return false;
				}

			}

			catch (Exception)
			{
				
				return false;
	
			}


		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{

			if (this.WindowState == FormWindowState.Minimized)
			{
				this.ShowInTaskbar = true; //显示在系统任务栏
				this.WindowState = FormWindowState.Normal; //还原窗体
				notifyIcon1.Visible = false; //托盘图标隐藏
			}
		}

		private void 图片像素批量修改工具_FormClosing(object sender, FormClosingEventArgs e)
		{
			

				System.Environment.Exit(System.Environment.ExitCode);
				this.Dispose();
				e.Cancel = false;


	
		}
	}
}
