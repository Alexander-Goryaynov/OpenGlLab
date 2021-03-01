﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;

namespace OpenGlApp
{
    public partial class FormGL : Form
    {
        private const int gridLinesCount = 14;
        OpenGL gl;
        public FormGL()
        {
            InitializeComponent();
            gl = openGLControl.OpenGL;
        }

        private void openGLControl_Load(object sender, EventArgs e)
        {
            openGLControl.Width = Width - 30;
            openGLControl.Height = Height - 100;
            // задаём проекционную матрицу
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // загружаем матрицу в стек
            gl.LoadIdentity();
            // матрица проекции (угол обзора, соотношение сторон экрана,
            // расстояние до ближней плоскости, расстояние до дальней плоскости)
            gl.Perspective(80, 4 / 4, .1, 200);
            // матрица вида (положение камеры x0, y0, z0; в какую точку x1, y1, z1 она смотрит;
            // величины поворота "головы" камеры x2, y2, z2)
            gl.LookAt(6, 6, 6, 0, 0, 0, 0, 0, 1);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            // цвет фона
            gl.ClearColor(.1f, .1f, .3f, 1);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            // координатная сетка
            gl.LineWidth(3.0f);
            gl.Begin(OpenGL.GL_LINES);
            for (int i = 0; i <= gridLinesCount; i++)
            {
                if (i == 0 || i == gridLinesCount / 2 || i == gridLinesCount)
                {
                    // белые линии сетки
                    gl.Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else
                {
                    // серые линии сетки
                    gl.Color(0.5f, 0.5f, 0.5f, 1.0f);
                }
                // рисуем линии
                // линии параллельно Ox
                gl.Vertex(-14.0f + 2 * i, -14.0f, 0.0f);
                gl.Vertex(-14.0f + 2 * i, 14.0f, 0.0f);
                // линии параллельно Oy
                gl.Vertex(-14.0f, -14.0f + 2 * i, 0.0f);
                gl.Vertex(14.0f, -14.0f + 2 * i, 0.0f);
            }
            // ось Ox КРАСНАЯ
            gl.Color(1.0f, 0.0f, 0.0f, 1.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(10.0f, 0.0f, 0.0f);
            // ось Oy ЗЕЛЁНАЯ
            gl.Color(0.0f, 1.0f, 0.0f, 1.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 10.0f, 0.0f);
            // ось Oz СИНЯЯ
            gl.Color(0.0f, 0.0f, 1.0f, 1.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 10.0f);
            gl.End();
            gl.LineWidth(1.0f);
        }
    }
}
