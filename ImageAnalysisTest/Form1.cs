using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Imaging;
using AForge.Imaging.ColorReduction;
using AForge.Imaging.ComplexFilters;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;

namespace ImageAnalysisTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = (Bitmap)System.Drawing.Image.FromFile(openFileDialog1.FileName);
                 
            }
        }

        private void grayscaleImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            GrayscaleBT709 gray = new GrayscaleBT709();
            pictureBox2.Image = gray.Apply((Bitmap)pictureBox1.Image);
        }

        private void blackAndWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EuclideanColorFiltering filter = new EuclideanColorFiltering();
            filter.CenterColor = new AForge.Imaging.RGB(Color.White); //Pure White
            filter.Radius = 40; //Increase this to allow off-whites
            filter.FillColor = new AForge.Imaging.RGB(Color.Red); //Replacement Colour
            //filter.ApplyInPlace(image);
            // apply the filter
            //filter.ApplyInPlace(image);
            Bitmap newImage = filter.Apply((Bitmap)pictureBox1.Image);
            
            pictureBox2.Image = newImage;
        }

        private void blackBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create filter
            ColorFiltering filter = new ColorFiltering();
            // set color ranges to keep
            filter.Red = new IntRange(100, 255);
            filter.Green = new IntRange(0, 75);
            filter.Blue = new IntRange(0, 75);
            // apply the filter
            // filter.ApplyInPlace((Bitmap)pictureBox2.Image);
            Bitmap newImage = filter.Apply((Bitmap)pictureBox2.Image);
            pictureBox2.Image = newImage;
        }

        private void countCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage((Bitmap)pictureBox2.Image);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            System.Diagnostics.Debug.WriteLine("Objects count: " + blobs.Length);
            //// create Graphics object to draw on the image and a pen
            //Graphics g = Graphics.FromImage(bitmap);
            //Pen bluePen = new Pen(Color.Blue, 2);
            //// check each object and draw circle around objects, which
            //// are recognized as circles
            //for (int i = 0, n = blobs.Length; i < n; i++)
            //{
            //    List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);
            //    List<IntPoint> corners = PointsCloud.FindQuadrilateralCorners(edgePoints);

            //    g.DrawPolygon(bluePen, ToPointsArray(corners));
            //}

            //bluePen.Dispose();
            //g.Dispose();
        }
    }
}
