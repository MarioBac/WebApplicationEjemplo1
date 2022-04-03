using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationEjemplo1
{
    public partial class _Default : Page
    {
        List<Alumno> alumnos = new List<Alumno>();
        List<Inscripciones> inscripciones = new List<Inscripciones>();
        protected void Page_Load(object sender, EventArgs e)
        {
            LeerAlumnos();

            DropDownList1.DataValueField = "nombre";
            DropDownList1.DataSource = alumnos;
            DropDownList1.DataBind();
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

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GuardarInscripciones(string fileName, string texto)
        {
            string fileName1  = Server.MapPath("~/Archivos/Inscripciones.txt");

            FileStream stream = new FileStream(fileName1, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(texto);
            writer.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Inscripciones inscripcion = new Inscripciones();

            inscripcion.carne = DropDownList1.SelectedValue;
            inscripcion.grado = Convert.ToInt16(((TextBox)sender).Text);
            inscripcion.fecha = DateTime.Now;

            inscripciones.Add(inscripcion);
        }
    }
}