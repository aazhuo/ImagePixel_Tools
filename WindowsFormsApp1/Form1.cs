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

        OpenFileDialog openFileDialog;
        SaveFilePath SaveFilePath;

        public 图片像素批量修改工具()
		{
			InitializeComponent();
            openFileDialog = new OpenFileDialog();
            SaveFilePath = new SaveFilePath();
        }

		private void button1_Click(object sender, EventArgs e)
		{

		
	          
			openFileDialog.Filter = "BMP File(*.bmp)|*.bmp|JPEG File(*.jpeg)|*.jpg|PNG File(*.png)|*.png";
			openFileDialog.Multiselect = true;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{

				
				


			}
		}


        //设置图片像素
        //FileName 设置文件名
       //SaveFileName 设置文件保存名
       //返回值 true 成功 ，false 失败
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
                    newImg.Save(SaveFilePath.GetFilePath() + SaveFileName, SaveFilePath.GetImageFormat(img));
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

        private void button2_Click(object sender, EventArgs e)
        {


            Int32 value = 0;
            progressBar1.Value = 0;
            foreach (string filename in openFileDialog.FileNames)
            {

                if (Image_Change(filename, System.IO.Path.GetFileName(filename)))
                {
                    value++;
                    progressBar1.Value = (int)value * 100 / openFileDialog.FileNames.Length;

                }

            }
            value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text=SaveFilePath.SetGetFilePath = dialog.SelectedPath;
             
            }



        }
    }


    public class SaveFilePath
    {


        private string filepath;

        public string SetGetFilePath {

            get {

                return filepath;

            }
            set
            {

                filepath = value;

            }



        }


        //获取保存路径
        public string GetFilePath()
        {

            return SetGetFilePath + "\\";

        }

        //获取图片格式
        public System.Drawing.Imaging.ImageFormat GetImageFormat(Image image)
        {

            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
            {

                return System.Drawing.Imaging.ImageFormat.Jpeg;
            }
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
            {

                return System.Drawing.Imaging.ImageFormat.Bmp;
            }
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
            {

                return System.Drawing.Imaging.ImageFormat.Png;
            }
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
            {

                return System.Drawing.Imaging.ImageFormat.Gif;

            }

            else
                return null;
        }

    }
}
