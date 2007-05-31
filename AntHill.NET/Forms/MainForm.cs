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
        public bool done = false;

        private ConfigForm cf = null;
        
        bool scrolling = false;
        Point mousePos;
        Counter counter = new Counter();

        private bool InitGL()
        {
            Gl.glEnable(Gl.GL_TEXTURE_2D);                                      // Enable Texture Mapping
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glShadeModel(Gl.GL_SMOOTH);                                      // Enable Smooth Shading
            Gl.glClearColor(1, 0, 0, 0.5f);                                     // Black Background
            Gl.glClearDepth(1);                                                 // Depth Buffer Setup
            //Gl.glEnable(Gl.GL_DEPTH_TEST);                                      // Enables Depth Testing
            Gl.glDepthFunc(Gl.GL_LEQUAL);                                       // The Type Of Depth Testing To Do                        
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

            ReSizeGLScene(maxMagnitude, maxMagnitude, true);

            return true;
        }

        public MainForm()
        {
            InitializeComponent();

            openGLControl.InitializeContexts();
            InitGL();

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
        float sceneWidth, sceneHeight;
        private void ReSizeGLScene(float width, float height, bool updateViewport)
        {
            if (updateViewport)            
                Gl.glViewport(0, 0, openGLControl.Width, openGLControl.Height);                            
            Gl.glMatrixMode(Gl.GL_PROJECTION);                                  // Select The Projection Matrix
            Gl.glLoadIdentity();                                                // Reset The Projection Matrix
            Gl.glOrtho(-(0.5f * width), (0.5f * width), (0.5f * height), -(0.5f * height), -100, 100);
            sceneWidth = width;
            sceneHeight = height;
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
                    openGLControl.Invalidate();
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
                if (Simulation.simulation.Map.Width > Simulation.simulation.Map.Height)
                    maxMagnitude = Simulation.simulation.Map.Width;
                else
                    maxMagnitude = Simulation.simulation.Map.Height;
                moveX = -(Simulation.simulation.Map.Width >> 1) + 0.5f;
                moveY = -(Simulation.simulation.Map.Height >> 1) + 0.5f;

                vScrollBar1.Minimum = 0;
                vScrollBar1.LargeChange = 1;
                vScrollBar1.Maximum = 10 * Simulation.simulation.Map.Height + vScrollBar1.LargeChange;
                vScrollBar1.Value = 10 * (Simulation.simulation.Map.Height >> 1);
                vScrollBar1.Enabled = true;
                hScrollBar1.Minimum = 0;
                hScrollBar1.LargeChange = 1;
                hScrollBar1.Maximum = 10 * Simulation.simulation.Map.Width + hScrollBar1.LargeChange;
                hScrollBar1.Value = 10 * (Simulation.simulation.Map.Width >> 1);
                hScrollBar1.Enabled = true;
                RecalculateUI(true);
                openGLControl.Invalidate();
            }
        }
        private int maxMagnitude = 0;
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
                openGLControl.Invalidate();
                MessageBox.Show(Properties.Resources.SimulationFinished);
            }

            openGLControl.Invalidate();
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
            counter.FrameTick();
            this.Text = "fps:"+counter.FPS + " rps:" + counter.RPS;
            openGLControl.Invalidate();
        }

        private void buttonShowConfig_Click(object sender, EventArgs e)
        {
            cf.RefreshData();
            cf.Show();
        }

        
        private void UpdateMap(object sender, EventArgs e)
        {
            RecalculateUI(true);            
            openGLControl.Invalidate();
        }

        private void RecalculateUI(bool recalculateViewport)
        {
            float mapWidth = Simulation.simulation.Map.Width,
                mapHeight = Simulation.simulation.Map.Height,
                tileSize = AntHillConfig.tileSize,
                realWidth = mapWidth * tileSize,
                realHeight = mapHeight * tileSize;

            float magnitude = ((float)magnitudeBar.Value) / ((float)magnitudeBar.Maximum);
            float x = 1.0f + (float)(Simulation.simulation.Map.Width - 1) * magnitude;
            float y = 1.0f + (float)(Simulation.simulation.Map.Height - 1) * magnitude;            

            /*xRatio = ((float)drawingRect.Width) / realWidth;
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
            */
            ReSizeGLScene(x, y, recalculateViewport);
        }

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            timer.Interval = 10000 / (speedBar.Value);
        }
        
        private void magnitudeBar_Scroll(object sender, EventArgs e)
        {
            AntHillConfig.curMagnitude = ((float)magnitudeBar.Value) / 1000.0f;
            cf.RefreshData();
            RecalculateUI(false);
            openGLControl.Invalidate();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Simulation.DeInit();
            Simulation.Init(new Map(AntHillConfig.mapColCount, AntHillConfig.mapRowCount, AntHillConfig.tiles));

            startButton.Enabled = true;
            //btnReset.Enabled = true;
            doTurnButton.Enabled = true;
            btnStop.Enabled = false;

            openGLControl.Invalidate();
        }

        

        private void Scrolled(object sender, EventArgs e)
        {
            openGLControl.Invalidate();
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

        private bool ShouldOmitDrawing(int x, int y)
        {
            return ((x + moveX + 1 < -sceneWidth / 2.0f) ||
                       (x + moveX - 1 > sceneWidth / 2.0f) ||
                       (y + moveY + 1 < -sceneHeight / 2.0f) ||
                       (y + moveY - 1 > sceneHeight / 2.0f));
        }

        float moveX = 0, moveY = 0;
        private void openGLControl_Paint(object sender, PaintEventArgs ea)
        {
            counter.RoundTick();
            this.Text = "fps:" + counter.FPS + " rps:" + counter.RPS;
            Gl.glClearColor(0, 0, 0, 0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();

            if (Simulation.simulation == null) return;
            
            Gl.glNormal3f(0, 1, 0);
            Map map = Simulation.simulation.Map;

            int signal;
            float maxSignal = 10.0f;
            int initialAlpha = 2;

            Gl.glColor4f(1, 1, 1, 1);
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (ShouldOmitDrawing(x, y)) continue;
                    DrawElement(x, y, map.GetTile(x, y).GetTexture(), Dir.N, moveX, moveY);
                }
            }

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (ShouldOmitDrawing(x, y)) continue;

                    if ((signal = map.MsgCount[x, y].GetCount(MessageType.FoodLocalization)) > 0)
                    {
                        Gl.glColor4f(1, 1, 1, (float)(signal + initialAlpha) / maxSignal);
                        DrawElement(x, y, (int)AHGraphics.Texture.MessageFoodLocation, Dir.N, moveX, moveY);
                    }
                    if ((signal = map.MsgCount[x, y].GetCount(MessageType.QueenInDanger)) > 0)
                    {
                        Gl.glColor4f(1, 1, 1, (float)(signal + initialAlpha) / maxSignal);
                        DrawElement(x, y, (int)AHGraphics.Texture.MessageQueenInDanger, Dir.N, moveX, moveY);
                    }
                    if ((signal = map.MsgCount[x, y].GetCount(MessageType.QueenIsHungry)) > 0)
                    {
                        Gl.glColor4f(1, 1, 1, (float)(signal + initialAlpha) / maxSignal);
                        DrawElement(x, y, (int)AHGraphics.Texture.MessageQueenIsHungry, Dir.N, moveX, moveY);
                    }
                    if ((signal = map.MsgCount[x, y].GetCount(MessageType.SpiderLocalization)) > 0)
                    {
                        Gl.glColor4f(1, 1, 1, (float)(signal + initialAlpha) / maxSignal);
                        DrawElement(x, y, (int)AHGraphics.Texture.MessageSpiderLocation, Dir.N, moveX, moveY);
                    }
                }
            }

            Gl.glColor4f(1, 1, 1, 1);
            Creature e;
            Food f;
            LIList<Ant>.Enumerator enumerator = Simulation.simulation.ants.GetEnumerator();
            while (enumerator.MoveNext())
            {
                e = enumerator.Current;
                if (ShouldOmitDrawing(e.Position.X, e.Position.Y)) continue;
                DrawElement(e.Position.X, e.Position.Y, e.GetTexture(), e.Direction, moveX, moveY);
            }
            LIList<Spider>.Enumerator enumeratorSpider = Simulation.simulation.spiders.GetEnumerator();
            while (enumeratorSpider.MoveNext())
            {
                e = enumeratorSpider.Current;
                if (ShouldOmitDrawing(e.Position.X, e.Position.Y)) continue;
                DrawElement(e.Position.X, e.Position.Y, e.GetTexture(), e.Direction, moveX, moveY);
            }
            LIList<Food>.Enumerator enumeratorFood = Simulation.simulation.food.GetEnumerator();
            while (enumeratorFood.MoveNext())
            {
                f = enumeratorFood.Current;
                if (ShouldOmitDrawing(f.Position.X, f.Position.Y)) continue;
                DrawElement(f.Position.X, f.Position.Y, f.GetTexture(), Dir.N, moveX, moveY);
            }

            e = Simulation.simulation.queen;
            if (e != null && !ShouldOmitDrawing(e.Position.X, e.Position.Y))
                DrawElement(e.Position.X, e.Position.Y, e.GetTexture(), e.Direction, moveX, moveY);
            
            //deszcz
            Rain rain = Simulation.simulation.rain;
            if (rain != null)
            {
                Gl.glPushMatrix();
                Gl.glTranslatef(rain.Position.X + moveX, rain.Position.Y + moveY, 0);
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, rain.GetTexture());
                Gl.glBegin(Gl.GL_TRIANGLE_FAN);
                Gl.glTexCoord2f(0, 0); Gl.glVertex3f(0.0f, 0.0f, 0.0f);
                Gl.glTexCoord2f(1, 0); Gl.glVertex3f((AntHillConfig.rainWidth),0.0f, 0.0f);
                Gl.glTexCoord2f(1, 1); Gl.glVertex3f(AntHillConfig.rainWidth, AntHillConfig.rainWidth, 0.0f);
                Gl.glTexCoord2f(0, 1); Gl.glVertex3f(0.0f, AntHillConfig.rainWidth, 0.0f);
                Gl.glEnd();
                Gl.glPopMatrix();
            }
        }

        private void DrawElement(int x, int y, int texture, Dir direction, float moveX, float moveY)
        {
            Gl.glPushMatrix();                           
            Gl.glTranslatef(x + moveX, y + moveY, 0);
            Gl.glRotatef(90.0f * (float)direction, 0, 0, 1);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glTexCoord2f(0, 0); Gl.glVertex3f(-0.5f, -0.5f, 0.0f);
            Gl.glTexCoord2f(1, 0); Gl.glVertex3f(0.5f, -0.5f, 0.0f);
            Gl.glTexCoord2f(1, 1); Gl.glVertex3f(0.5f, 0.5f, 0.0f);            
            Gl.glTexCoord2f(0, 1); Gl.glVertex3f(-0.5f, 0.5f, 0.0f);
            Gl.glEnd();
            Gl.glPopMatrix();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            moveX = -((float)hScrollBar1.Value / 10);
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            moveY = -((float)vScrollBar1.Value / 10); 
        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mousePos = e.Location;
                scrolling = true;
            }
        }        

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                scrolling = false;
            }
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (scrolling)
            {
                int hVal = hScrollBar1.Value - (e.X - mousePos.X);
                int vVal = vScrollBar1.Value - (e.Y - mousePos.Y);
                hScrollBar1.Value = Math.Min(hScrollBar1.Maximum - hScrollBar1.LargeChange, Math.Max(0, hVal));
                vScrollBar1.Value = Math.Min(vScrollBar1.Maximum - vScrollBar1.LargeChange, Math.Max(0, vVal));
                moveX = -((float)hScrollBar1.Value / 10);
                moveY = -((float)vScrollBar1.Value / 10);
                openGLControl.Invalidate();
                mousePos = e.Location;
            }
        }
    }
    
}
