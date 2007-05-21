using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AntHill.NET.Forms;
using Tao.OpenGl;
using Tao.Platform.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace AntHill.NET
{
    public partial class MainForm : Form
    {
        //OpenGl
        public IntPtr hDC;                                              // Private GDI Device Context
        private IntPtr hRC;
        public bool done = false;
        //End of OpenGL

        private ConfigForm cf = null;
        
        bool scrolling = false;
        Point mousePos;
        Rectangle drawingRect;

        #region bool CreateGLWindow(string title, int width, int height, int bits, bool fullscreenflag)
        /// <summary>
        ///     Creates our OpenGL Window.
        /// </summary>
        /// <param name="title">
        ///     The title to appear at the top of the window.
        /// </param>
        /// <param name="width">
        ///     The width of the GL window or fullscreen mode.
        /// </param>
        /// <param name="height">
        ///     The height of the GL window or fullscreen mode.
        /// </param>
        /// <param name="bits">
        ///     The number of bits to use for color (8/16/24/32).
        /// </param>
        /// <param name="fullscreenflag">
        ///     Use fullscreen mode (<c>true</c>) or windowed mode (<c>false</c>).
        /// </param>
        /// <returns>
        ///     <c>true</c> on successful window creation, otherwise <c>false</c>.
        /// </returns>
        private bool CreateGLWindow()
        {
            int pixelFormat;                                                    // Holds The Results After Searching For A Match                      
            GC.Collect();                                                       // Request A Collection
            // This Forces A Swap
            
            Kernel.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
                                                                                                 
            this.FormBorderStyle = FormBorderStyle.Sizable;                 // Sizable
                        
            Gdi.PIXELFORMATDESCRIPTOR pfd = new Gdi.PIXELFORMATDESCRIPTOR();    // pfd Tells Windows How We Want Things To Be
            pfd.nSize = (short)Marshal.SizeOf(pfd);                            // Size Of This Pixel Format Descriptor
            pfd.nVersion = 1;                                                   // Version Number
            pfd.dwFlags = Gdi.PFD_DRAW_TO_WINDOW |                              // Format Must Support Window
                Gdi.PFD_SUPPORT_OPENGL |                                        // Format Must Support OpenGL
                Gdi.PFD_DOUBLEBUFFER;                                           // Format Must Support Double Buffering
            pfd.iPixelType = (byte)Gdi.PFD_TYPE_RGBA;                          // Request An RGBA Format
            pfd.cColorBits = 32;                                       // Select Our Color Depth
            pfd.cRedBits = 0;                                                   // Color Bits Ignored
            pfd.cRedShift = 0;
            pfd.cGreenBits = 0;
            pfd.cGreenShift = 0;
            pfd.cBlueBits = 0;
            pfd.cBlueShift = 0;
            pfd.cAlphaBits = 0;                                                 // No Alpha Buffer
            pfd.cAlphaShift = 0;                                                // Shift Bit Ignored
            pfd.cAccumBits = 0;                                                 // No Accumulation Buffer
            pfd.cAccumRedBits = 0;                                              // Accumulation Bits Ignored
            pfd.cAccumGreenBits = 0;
            pfd.cAccumBlueBits = 0;
            pfd.cAccumAlphaBits = 0;
            pfd.cDepthBits = 16;                                                // 16Bit Z-Buffer (Depth Buffer)
            pfd.cStencilBits = 0;                                               // No Stencil Buffer
            pfd.cAuxBuffers = 0;                                                // No Auxiliary Buffer
            pfd.iLayerType = (byte)Gdi.PFD_MAIN_PLANE;                         // Main Drawing Layer
            pfd.bReserved = 0;                                                  // Reserved
            pfd.dwLayerMask = 0;                                                // Layer Masks Ignored
            pfd.dwVisibleMask = 0;
            pfd.dwDamageMask = 0;

            hDC = User.GetDC(this.Handle);                                      // Attempt To Get A Device Context
            if (hDC == IntPtr.Zero)
            {                                            // Did We Get A Device Context?
                KillGLWindow();                                                 // Reset The Display
                MessageBox.Show("Can't Create A GL Device Context.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            pixelFormat = Gdi.ChoosePixelFormat(hDC, ref pfd);                  // Attempt To Find An Appropriate Pixel Format
            if (pixelFormat == 0)
            {                                              // Did Windows Find A Matching Pixel Format?
                KillGLWindow();                                                 // Reset The Display
                MessageBox.Show("Can't Find A Suitable PixelFormat.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Gdi.SetPixelFormat(hDC, pixelFormat, ref pfd))
            {                // Are We Able To Set The Pixel Format?
                KillGLWindow();                                                 // Reset The Display
                MessageBox.Show("Can't Set The PixelFormat.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            hRC = Wgl.wglCreateContext(hDC);                                    // Attempt To Get The Rendering Context
            if (hRC == IntPtr.Zero)
            {                                            // Are We Able To Get A Rendering Context?
                KillGLWindow();                                                 // Reset The Display
                MessageBox.Show("Can't Create A GL Rendering Context.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Wgl.wglMakeCurrent(hDC, hRC))
            {                                 // Try To Activate The Rendering Context
                KillGLWindow();                                                 // Reset The Display
                MessageBox.Show("Can't Activate The GL Rendering Context.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }                     
            
            ReSizeGLScene(this.ClientSize.Width, this.ClientSize.Height);                                       // Set Up Our Perspective GL Screen

            if (!InitGL())
            {                                                     // Initialize Our Newly Created GL Window
                KillGLWindow();                                                 // Reset The Display
                MessageBox.Show("Initialization Failed.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;                                                        // Success
        }
        #endregion bool CreateGLWindow(string title, int width, int height, int bits, bool fullscreenflag)
        private bool InitGL()
        {
            if (!LoadGLTextures())
            {                                             // Jump To Texture Loading Routine
                return false;                                                   // If Texture Didn't Load Return False
            }

            Gl.glEnable(Gl.GL_TEXTURE_2D);                                      // Enable Texture Mapping
            Gl.glShadeModel(Gl.GL_SMOOTH);                                      // Enable Smooth Shading
            Gl.glClearColor(1, 0, 0, 0.5f);                                     // Black Background
            Gl.glClearDepth(1);                                                 // Depth Buffer Setup
            //Gl.glEnable(Gl.GL_DEPTH_TEST);                                      // Enables Depth Testing
            Gl.glDepthFunc(Gl.GL_LEQUAL);                                       // The Type Of Depth Testing To Do                        

            return true;
        }
        #region KillGLWindow()
        /// <summary>
        ///     Properly kill the window.
        /// </summary>
        private void KillGLWindow()
        {            
            if (hRC != IntPtr.Zero)
            {                                            // Do We Have A Rendering Context?
                if (!Wgl.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero))
                {             // Are We Able To Release The DC and RC Contexts?
                    MessageBox.Show("Release Of DC And RC Failed.", "SHUTDOWN ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!Wgl.wglDeleteContext(hRC))
                {                                // Are We Able To Delete The RC?
                    MessageBox.Show("Release Rendering Context Failed.", "SHUTDOWN ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                hRC = IntPtr.Zero;                                              // Set RC To Null
            }

            if (hDC != IntPtr.Zero)
            {                                            // Do We Have A Device Context?
                if (!this.IsDisposed)
                {                          // Do We Have A Window?
                    if (this.Handle != IntPtr.Zero)
                    {                            // Do We Have A Window Handle?
                        if (!User.ReleaseDC(this.Handle, hDC))
                        {                 // Are We Able To Release The DC?
                            MessageBox.Show("Release Device Context Failed.", "SHUTDOWN ERROR",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                hDC = IntPtr.Zero;                                              // Set DC To Null
            }            
        }
        #endregion KillGLWindow()

        #region bool LoadGLTextures()
        /// <summary>
        ///     Load bitmaps and convert to textures.
        /// </summary>
        /// <returns>
        ///     <c>true</c> on success, otherwise <c>false</c>.
        /// </returns>
        private bool LoadGLTextures()
        {
            LoadTexture(AHGraphics.bmpQueen, AHGraphics.textureQueen);
            LoadTexture(AHGraphics.bmpWorker, AHGraphics.textureWorker);
            LoadTexture(AHGraphics.bmpWarrior, AHGraphics.textureWarrior);
            LoadTexture(AHGraphics.bmpSpider, AHGraphics.textureSpider);
            LoadTexture(AHGraphics.GetFoodBitmap(), AHGraphics.textureFood);
            LoadTexture(AHGraphics.GetTile(TileType.Indoor), AHGraphics.textureIndoor);
            LoadTexture(AHGraphics.GetTile(TileType.Outdoor), AHGraphics.textureOutdoor);
            LoadTexture(AHGraphics.GetTile(TileType.Wall), AHGraphics.textureWall);
            LoadTexture(AHGraphics.GetRainBitmap(), AHGraphics.textureRain);
            LoadTexture(AHGraphics.GetMessagesBitmap(), AHGraphics.textureIndoor);
            return true;                                                      // Return The Status
        }
        private void LoadTexture(Bitmap bitmap, int textureNumber)
        {
            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureNumber);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA8, bitmap.Width, bitmap.Height, 0, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);
            bitmap.UnlockBits(bitmapData);                     // Unlock The Pixel Data From Memory
            bitmap.Dispose();                                  // Dispose The Bitmap                            
        }
        #endregion bool LoadGLTextures()

        public MainForm()
        {
            InitializeComponent();
            this.CreateParams.ClassStyle = this.CreateParams.ClassStyle |       // Redraw On Size, And Own DC For Window.
                User.CS_HREDRAW | User.CS_VREDRAW | User.CS_OWNDC;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);            // No Need To Erase Form Background
            this.SetStyle(ControlStyles.DoubleBuffer, true);                    // Buffer Control
            this.SetStyle(ControlStyles.Opaque, true);                          // No Need To Draw Form Background
            this.SetStyle(ControlStyles.ResizeRedraw, true);                    // Redraw On Resize
            this.SetStyle(ControlStyles.UserPaint, true);                       // We'll Handle Painting Ourselves
            try
            {
                AHGraphics.Init();
            }
            catch
            {
                MessageBox.Show(Properties.Resources.errorGraphics);
                throw new Exception();
            }

            cf = new ConfigForm();
            drawingRect = new Rectangle();
            if (!CreateGLWindow())
            {
                KillGLWindow();
                this.Dispose();
            }
            this.MouseWheel += new MouseEventHandler(MainForm_MouseWheel);
        }

        void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (rightPanel.Enabled == false) return;

            int zoom = magnitudeBar.Value + (e.Delta * (magnitudeBar.Maximum - magnitudeBar.Minimum) / 10 / 120);
            if (zoom > magnitudeBar.Maximum) zoom = magnitudeBar.Maximum;
            if (zoom < magnitudeBar.Minimum) zoom = magnitudeBar.Minimum;
            magnitudeBar.Value = zoom;

            magnitudeBar_Scroll(null, null);
        }
        public bool DrawGLScene()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);        // Clear The Screen And The Depth Buffer
            Gl.glLoadIdentity();                                                // Reset The View            
            
            //Gl.glBindTexture(Gl.GL_TEXTURE_2D, AHGraphics.textureWall);

            Gl.glBegin(Gl.GL_QUADS);
            // Front Face
            Gl.glNormal3f(0, 0, 1);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(-1, -1, 1);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(1, -1, 1);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(1, 1, 1);
            Gl.glTexCoord2f(0, 1); Gl.glVertex3f(-1, 1, 1);
            // Back Face
            Gl.glNormal3f(0, 0, -1);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(-1, -1, -1);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(-1, 1, -1);
            Gl.glTexCoord2f(0, 1); Gl.glVertex3f(1, 1, -1);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(1, -1, -1);
            // Top Face
            Gl.glNormal3f(0, 1, 0);
            Gl.glTexCoord2f(0, 1); Gl.glVertex3f(-1, 1, -1);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(-1, 1, 1);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(1, 1, 1);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(1, 1, -1);
            // Bottom Face
            Gl.glNormal3f(0, -1, 0);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(-1, -1, -1);
            Gl.glTexCoord2f(0, 1); Gl.glVertex3f(1, -1, -1);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(1, -1, 1);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(-1, -1, 1);
            // Right face
            Gl.glNormal3f(1, 0, 0);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(1, -1, -1);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(1, 1, -1);
            Gl.glTexCoord2f(0, 1); Gl.glVertex3f(1, 1, 1);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(1, -1, 1);
            // Left Face
            Gl.glNormal3f(-1, 0, 0);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(-1, -1, -1);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(-1, -1, 1);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(-1, 1, 1);
            Gl.glTexCoord2f(0, 1); Gl.glVertex3f(-1, 1, -1);
            Gl.glEnd();            

            return true;                                                        // Keep Going
        }

        private void ReSizeGLScene(int width, int height)
        {
            if (height == 0)
            {                                                   // Prevent A Divide By Zero...
                height = 1;                                                     // By Making Height Equal To One
            }

            Gl.glViewport(0, 0, width, height);                                 // Reset The Current Viewport
            Gl.glMatrixMode(Gl.GL_PROJECTION);                                  // Select The Projection Matrix
            Gl.glLoadIdentity();                                                // Reset The Projection Matrix
            Gl.glOrtho(-100, 100, -100, 100, -100, 100);            
            Gl.glMatrixMode(Gl.GL_MODELVIEW);                                   // Select The Modelview Matrix
            Gl.glLoadIdentity();                                                // Reset The Modelview Matrix
        }

        private void loadData(object sender, EventArgs e)
        {
            pauseButton_Click(this, null);
            this.Resize -= new EventHandler(UpdateMap);

            string name;
            if (simulationXMLopenFileDialog.ShowDialog() == DialogResult.OK)
            {                
                name = simulationXMLopenFileDialog.FileName;
                XmlReaderWriter reader = new XmlReaderWriter();
                try
                {
                    reader.ReadMe(name);
                    Simulation.DeInit();
                    Simulation.Init(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));

                }
                catch
                {
                    Invalidate();
                    rightPanel.Enabled = false;

                    MessageBox.Show(Properties.Resources.errorInitialization);
                    return;
                }

                rightPanel.Enabled = true;
                /*
                doTurnButton.Enabled = true;
                startButton.Enabled = true;
                btnStop.Enabled = false;
                btnReset.Enabled = true;
                */
                
                timer.Interval = speedBar.Maximum - speedBar.Value + speedBar.Minimum;

                this.Resize += new EventHandler(UpdateMap);
                RecalculateUI();
                Invalidate();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            doTurnButton.Enabled = false;
            startButton.Enabled = false;
            btnStop.Enabled = true;
            btnReset.Enabled = true;

            timer.Start();
            //((ISimulationUser)Simulation.simulation).Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            doTurnButton.Enabled = true;
            startButton.Enabled = true;
            btnStop.Enabled = false;
            btnReset.Enabled = true;                        
        }

        private void doTurnButton_Click(object sender, EventArgs e)
        {
            //doTurnButton.Enabled = true;
            //startButton.Enabled = true;
            btnReset.Enabled = true;
            //btnStop.Enabled = false;

            if (((ISimulationUser)Simulation.simulation).DoTurn() == false)
            {
                Invalidate();
                MessageBox.Show(Properties.Resources.SimulationFinished);
            }
            
            Invalidate();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            timer.Stop();

            startButton.Enabled = true;
            btnReset.Enabled = true;
            doTurnButton.Enabled = true;
            btnStop.Enabled = false;

            //((ISimulationUser)Simulation.simulation).Pause();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (((ISimulationUser)Simulation.simulation).DoTurn() == false)
            {
                timer.Stop();
                MessageBox.Show(Properties.Resources.SimulationFinished);
            }
            Invalidate();
        }

        private void buttonShowConfig_Click(object sender, EventArgs e)
        {
            cf.RefreshData();
            cf.Show();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            DrawGLScene();
            Gdi.SwapBuffers(hDC);                                 
            return;

            Graphics g = e.Graphics;
            //Faster drawing
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.Clear(Color.Black);

            //Check whether we really need to draw the map
            if ((Simulation.simulation == null) ||
                !cbVisualize.Checked)
                return;

            Matrix mT = new Matrix();
            mT.Translate(0, menuStrip1.Height);
            g.Transform = mT;

            //Init variables
            float tileSize = AntHillConfig.tileSize,
                realWidth = Simulation.simulation.Map.Width * tileSize,
                realHeight = Simulation.simulation.Map.Height * tileSize,
                magnitude = AntHillConfig.curMagnitude,
                realTileSize = tileSize * magnitude,
                offX = hScrollBar1.Value, offY = vScrollBar1.Value;

            Simulation.simulation.Map.DrawMap(g, drawingRect,
                offX, offY, magnitude);

            g.SetClip(drawingRect);

            foreach (Food f in Simulation.simulation.food)
                AHGraphics.DrawElement(g, f, realTileSize, offX, offY);                

            foreach (Ant ant in Simulation.simulation.ants)
                AHGraphics.DrawElement(g, ant, realTileSize, offX, offY);

            foreach (Spider spider in Simulation.simulation.spiders)
                AHGraphics.DrawElement(g, spider, realTileSize, offX, offY);

            if (Simulation.simulation.queen != null)
                AHGraphics.DrawElement(g, Simulation.simulation.queen, realTileSize, offX, offY);

            if (Simulation.simulation.rain != null)
                AHGraphics.DrawElement(g, Simulation.simulation.rain, realTileSize, offX, offY);
        }        
        
        private void UpdateMap(object sender, EventArgs e)
        {
            RecalculateUI();
            Invalidate();
        }

        private void RecalculateUI()
        {
            float mapWidth = Simulation.simulation.Map.Width,
                mapHeight = Simulation.simulation.Map.Height,
                tileSize = AntHillConfig.tileSize,
                realWidth = mapWidth * tileSize,
                realHeight = mapHeight * tileSize,
                xRatio, yRatio, magnitude;

            // drawingRect is not the exact position - just width & height
            drawingRect = new Rectangle(0, 0,
                                    rightPanel.Location.X - 1 - vScrollBar1.Width,
                                    ClientSize.Height - hScrollBar1.Height - menuStrip1.Height);

            xRatio = ((float)drawingRect.Width) / realWidth;
            yRatio = ((float)drawingRect.Height) / realHeight;
            magnitudeBar.Minimum = (int)(1000.0f * Math.Min(xRatio, yRatio));
            magnitudeBar.Maximum = (int)(Math.Max(magnitudeBar.Minimum * 2, 2000));
            if (AntHillConfig.curMagnitude < ((float)magnitudeBar.Minimum) / 1000.0f)
                AntHillConfig.curMagnitude = ((float)magnitudeBar.Minimum) / 1000.0f;
            else
                if (AntHillConfig.curMagnitude > ((float)magnitudeBar.Maximum) / 1000.0f)
                    AntHillConfig.curMagnitude = ((float)magnitudeBar.Maximum) / 1000.0f;
            magnitude = AntHillConfig.curMagnitude;

            hScrollBar1.Enabled = (realWidth * magnitude > drawingRect.Width);
            vScrollBar1.Enabled = (realHeight * magnitude > drawingRect.Height);

            hScrollBar1.Maximum = (int)Math.Max(realWidth * magnitude - drawingRect.Width, 0);
            vScrollBar1.Maximum = (int)Math.Max(realHeight * magnitude - drawingRect.Height, 0);
            vScrollBar1.LargeChange = vScrollBar1.Maximum / 5;
            hScrollBar1.LargeChange = hScrollBar1.Maximum / 5;

            ReSizeGLScene(drawingRect.Width, drawingRect.Height);
        }

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            timer.Interval = speedBar.Maximum - speedBar.Value + speedBar.Minimum;
        }

        private void magnitudeBar_Scroll(object sender, EventArgs e)
        {
            AntHillConfig.curMagnitude = ((float)magnitudeBar.Value) / 1000.0f;
            cf.RefreshData();
            RecalculateUI();
            Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Rectangle r = drawingRect;
            r.Height += menuStrip1.Height;
            Invalidate(r);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Simulation.DeInit();
            Simulation.Init(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));

            startButton.Enabled = true;
            //btnReset.Enabled = true;
            doTurnButton.Enabled = true;
            btnStop.Enabled = false;

            Invalidate();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mousePos = e.Location;
                scrolling = true;                
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            scrolling = false;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (scrolling)
            {
                int hVal = hScrollBar1.Value - (e.X - mousePos.X),
                    vVal = vScrollBar1.Value - (e.Y - mousePos.Y);
                hScrollBar1.Value = Math.Min(hScrollBar1.Maximum, Math.Max(0, hVal));
                vScrollBar1.Value = Math.Min(vScrollBar1.Maximum, Math.Max(0, vVal));
                mousePos = e.Location;
            }
        }

        private void Scrolled(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            done = true;
        }
    }
}
