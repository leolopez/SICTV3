using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidad
{
    /// <summary>
    /// Summary description for Tareas2Entity
    /// </summary>
    public class Tareas
    {
        public Int32 IDTareas { get; set; }
        public String Componente { get; set; }
        public String ClaveTareas { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public String Cliente { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinEstimada { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public TimeSpan? HorasEstimadas { get; set; }
        public TimeSpan? HorasReales { get; set; }
        public String Tecnologias { get; set; }
        public String Estado { get; set; }
    }

    public class TareasRegistroTiempo
    {
        public Int32 Id_TareasRegTiempo { get; set; }
        public Int32 IDTareas { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime ActividadDetenida { get; set; }
        public Byte Id_TareasRTEstatus { get; set; }
    }

    public class getRegistroTareas_view
    {
        public Int32 Id_TareasRegTiempo { get; set; }
        public String FechaRegistro { get; set; }
        public String Hora { get; set; }
        public String HoraFin { get; set; }
        public String Estatus { get; set; }
        public String Notas { get; set; }
    }

    public class TareasRTEstatus
    {
        public Byte Id_TareasRTEstatus { get; set; }
        public String Nombre { get; set; }
    }

    //public class CronometroEntity
    //{
    //    public Int64 Id_Cronometro { get; set; }
    //    public Int64 Id_Tarea { get; set; }
    //    public DateTime FechaInicio { get; set; }
    //    public DateTime HoraInicio { get; set; }
    //    public DateTime FechaFin { get; set; }
    //    public DateTime HoraFin { get; set; }
    //    public Byte Id_Estatus { get; set; }
    //}

}

