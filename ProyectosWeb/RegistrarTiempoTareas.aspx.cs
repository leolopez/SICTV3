using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;

namespace ProyectosWeb
{
    public partial class RegistrarTiempoTareas : System.Web.UI.Page
    {
        DataHelper dh = new DataHelper();

        String Mensaje_Usuario = "";
        String Mensaje_desarrollador = "";
        String HoraEvento = "";
        Int32 Id_Registrado = 0;
        String TiempoTotal = "";
        Boolean Nulo = false;
        String IdAbierto = "";

        protected void Page_Init(object sender, EventArgs e)
        {

            pnl_ProgressBar.Visible = false;

            if (!IsPostBack)
            {
                lnk_Iniciar.Visible = true;
                lnk_Finalizar.Visible = false;
                lbl_EstatusGeneralTarea.Text = "En progreso";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String Mensaje_usuario = "";
            String Mensaje_desarrollador = "";

            Session["Id_Tarea"] = Session["Index"];

            List<Entidad.Tareas> ListaTareas = new List<Entidad.Tareas>();

            ListaTareas = dh.getTareas((Int32)Session["Id_Tarea"], ref Mensaje_usuario, ref Mensaje_desarrollador);

            this.lbl_Nombre.Text = ListaTareas[0].Nombre;
            this.lbl_ID.Text = Convert.ToString(ListaTareas[0].IDTareas);
            this.lbl_Descripcion.Text = ListaTareas[0].Descripcion;
            this.lbl_Clave.Text = ListaTareas[0].ClaveTareas;
            this.lbl_Componente.Text = Convert.ToString(ListaTareas[0].Componente);


            List<Entidad.getRegistroTareas_view> TareasRegistradas = new List<Entidad.getRegistroTareas_view>();

            TareasRegistradas = dh.getRegistroTareas((Int32)Session["Id_Tarea"], ref Nulo, ref IdAbierto, ref Mensaje_usuario);

            if (!IsPostBack)
            {

                Session["Nulo"] = Nulo;
                Session["IdAbierto"] = IdAbierto;

                if ((Boolean)Session["Nulo"])
                {

                    if ((String)Session["IdAbierto"] != "0")
                    {
                        Int32 id = Convert.ToInt32(Session["IdAbierto"]);
                        dh.CerrarPeriodo((Int32)Session["Id_Tarea"], id, ref Mensaje_Usuario, ref Mensaje_desarrollador);
                    }


                    grd_Registros.DataSource = dh.getRegistroTareas((Int32)Session["Id_Tarea"], ref Nulo, ref IdAbierto, ref Mensaje_Usuario);
                    grd_Registros.DataBind();

                    lnk_Pausa.Visible = false;
                    lnk_Finalizar.Visible = false;

                }
            }

            grd_Registros.DataSource = TareasRegistradas;
            grd_Registros.DataBind();

            var Lista = (from L in TareasRegistradas
                         select new
                         {
                             Id = L.Id_TareasRegTiempo,
                             Notas = L.Notas
                         }
             );

            foreach (var Cookie in Lista)
            {

                HttpCookie TareasRegistradasCookie = new HttpCookie(Cookie.Id.ToString());

                TareasRegistradasCookie.Value = Cookie.Notas;
                TareasRegistradasCookie.Expires = DateTime.Now.AddHours(1);

                Response.Cookies.Add(TareasRegistradasCookie);

            }



            lbl_HoraRegistrada.Text = HoraEvento;

        }

        protected void lnk_Iniciar_Click(object sender, EventArgs e)
        {
            HoraEvento = "";
            String Notas = this.Notas_txt.Text;

            dh.TareaRegistroTiempo((Int32)Session["Id_Tarea"], Notas, 1, ref Id_Registrado, ref HoraEvento, ref Mensaje_Usuario, ref Mensaje_desarrollador);

            grd_Registros.DataSource = dh.getRegistroTareas((Int32)Session["Id_Tarea"], ref Nulo, ref IdAbierto, ref Mensaje_Usuario);
            grd_Registros.DataBind();

            lbl_HoraRegistrada.Text = HoraEvento;

            Session["Id_Registrado"] = Id_Registrado;

            pnl_HoraRegistrada.CssClass = "alert-success";
            pnl_ProgressBar.Visible = true;

            lnk_Iniciar.Visible = false;
            lnk_Pausa.Visible = true;
            lnk_Finalizar.Visible = false;
        }

        protected void lnk_Pausa_Click(object sender, EventArgs e)
        {
            String Notas = this.Notas_txt.Text;

            dh.DetenerActividad((Int32)Session["Id_Registrado"], (Int32)Session["Id_Tarea"], Notas, ref HoraEvento, ref Mensaje_Usuario, ref Mensaje_desarrollador);
            grd_Registros.DataSource = dh.getRegistroTareas((Int32)Session["Id_Tarea"], ref Nulo, ref IdAbierto, ref Mensaje_Usuario);
            grd_Registros.DataBind();

            dh.getTiempoTotalTarea((Int32)Session["Id_Tarea"], ref TiempoTotal);

            lbl_TiempoTot.Text = "Tiempo total transcurrido: ";
            lbl_TiempoTotal.Text = TiempoTotal;
            lbl_HoraRegistrada.Text = HoraEvento;

            pnl_HoraRegistrada.CssClass = "alert-warning";
            pnl_ProgressBar.Visible = false;

            lnk_Iniciar.Visible = true;
            lnk_Pausa.Visible = false;
            lnk_Finalizar.Visible = true;

            Notas_txt.Text = "";
        }

        protected void lnk_DetenerAbierto_Click(object sender, EventArgs e)
        {
            dh.CerrarPeriodo((Int32)Session["Id_Tarea"], (Int32)Session["IdAbierto"], ref Mensaje_Usuario, ref Mensaje_desarrollador);

            grd_Registros.DataSource = dh.getRegistroTareas((Int32)Session["Id_Tarea"], ref Nulo, ref IdAbierto, ref Mensaje_Usuario);
            grd_Registros.DataBind();

            lnk_Iniciar.Visible = true;
            lnk_Pausa.Visible = false;
            lnk_Finalizar.Visible = true;
        }

        protected void lnk_Finalizar_Click(object sender, EventArgs e)
        {
            HoraEvento = "";

            dh.FinalizarTarea((Int32)Session["Id_Tarea"], ref Mensaje_Usuario, ref Mensaje_desarrollador);

            pnl_HoraRegistrada.CssClass = "";
            pnl_ProgressBar.Visible = false;

            lnk_Iniciar.Visible = false;
            lnk_Pausa.Visible = false;
            lnk_Finalizar.Visible = false;

            ClientScript.RegisterStartupScript(GetType(), "close", "window.close();", true);
        }

        protected void grd_Registros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_Registros.PageIndex = e.NewPageIndex;
            grd_Registros.DataBind();
        }

        protected void grd_Registros_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grd_Registros.SelectedRow;

            Session["Id_Seleccionado"] = Convert.ToInt32(row.Cells[0].Text);

            Int32 Id = (Int32)Session["Id_Seleccionado"];

            String CookieName = Id.ToString();

            if (Request.Cookies[CookieName] != null)
            {
                lbl_NotasRegistradas.Text = Server.HtmlDecode(Request.Cookies[CookieName].Value);

            }

        }


    }
}