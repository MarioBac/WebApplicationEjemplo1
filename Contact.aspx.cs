﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationEjemplo1
{
    public partial class Contact : Page
    {
        List<Reporte> reportes = new List<Reporte>();
        List<Alumno> alumnos = new List<Alumno>();
        List<Inscripciones> inscripciones = new List<Inscripciones>();
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        private void LeerAlumnos()
        {
            string fileName = Server.MapPath("~/Archivos/Alumnos.txt");
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                Alumno alumno = new Alumno();
                alumno.carne = reader.ReadLine();
                alumno.nombre = reader.ReadLine();

                alumnos.Add(alumno);
            }

            reader.Close();
        }

        private void LeerInscripciones()
        {
            string fileName = Server.MapPath("~/Archivos/Inscrípciones.txt");
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                Inscripciones inscripcion = new Inscripciones();
                inscripcion.carne = reader.ReadLine();
                inscripcion.grado = Convert.ToInt32(reader.ReadLine());
                inscripcion.fecha = Convert.ToDateTime(reader.ReadLine());
            }

            reader.Close();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            LeerAlumnos();
            LeerInscripciones();
            for (int i = 0; i < inscripciones.Count;i++)
            {
                for (int j = 0; j < alumnos.Count;j++)
                {
                    if (inscripciones[i].carne == alumnos[j].carne)
                    {
                        Reporte reporte = new Reporte();
                        reporte.nombre = alumnos[j].nombre;
                        reporte.grado = inscripciones[i].grado;

                        reportes.Add(reporte);
                    }
                }
            }
            reportes = reportes.OrderBy(g => g.grado).ToList();

            GridView1.DataSource = reportes;
            GridView1.DataBind();

            Label1.Text = inscripciones.Count.ToString();
        }
    }
}