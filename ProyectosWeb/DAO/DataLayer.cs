using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;


namespace Datos
{

    /// <summary>
    /// Summary description for DataLayer
    /// </summary>
    public class DataHelper
    {
        String cnn = ConfigurationManager.ConnectionStrings["ProyectosGestionConnectionString"].ToString();
        //SQLServer db = new SQLServer(cnn);


        public DataHelper()
        {

        }

        public Int32 TareaRegistroTiempo(Int32 IDTareas, String Notas, Byte Id_TareasRTEstatus, ref Int32 Id_Registrado, ref String HoraEvento, ref String Show_msg, ref String Dev_msg)
        {
            string query = "setTareaRegistroTiempo_sp";
            SqlParameter parameter;
            CommandType cmdType = CommandType.StoredProcedure;

            ArrayList parameters = new ArrayList();

            //Input parameters

            getParamList(0, "IDTareas", IDTareas, ref parameters, ParameterDirection.Input, SqlDbType.Int);
            getParamList(1, "Notas", Notas, ref parameters, ParameterDirection.Input, SqlDbType.VarChar,255);
            getParamList(2, "Id_TareasRTEstatus", Id_TareasRTEstatus, ref parameters, ParameterDirection.Input, SqlDbType.TinyInt);

            //Output parameters
           
            getParamList(3, "HoraEvento", HoraEvento, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 10);
            getParamList(4, "Show_msg", Show_msg, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 250);
            getParamList(5, "Dev_msg", Dev_msg, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 250);
            getParamList(6, "Id_Registrado", Id_Registrado, ref parameters, ParameterDirection.Output, SqlDbType.Int);

            //Execute SP
            SQLServer db = new SQLServer(cnn);
            Int32 Result = db.ExecuteNonQuery(query, cmdType, ref parameters);

            //Output results
            parameter = (SqlParameter)parameters[3];
            HoraEvento = (String)parameter.Value;

            parameter = (SqlParameter)parameters[4];
            Show_msg = (String)parameter.Value;

            parameter = (SqlParameter)parameters[5];
            Dev_msg = (String)parameter.Value;

            parameter = (SqlParameter)parameters[6];
            Id_Registrado = (Int32)parameter.Value;

            return Result;
        }




        public Int32 CerrarPeriodo(Int32 IDTareas, Int32 IdAbierto, ref String Show_msg, ref String Dev_msg)
        {
            string query = "setCerrarPeriodo_sp";
            SqlParameter parameter;
            CommandType cmdType = CommandType.StoredProcedure;

            ArrayList parameters = new ArrayList();

            //Input parameters

            getParamList(0, "IDTareas", IDTareas, ref parameters, ParameterDirection.Input, SqlDbType.Int);
            getParamList(1, "IdAbierto", IdAbierto, ref parameters, ParameterDirection.Input, SqlDbType.Int);


            //Output parameters

            getParamList(2, "Show_msg", Show_msg, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 250);
            getParamList(3, "Dev_msg", Dev_msg, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 250);


            //Execute SP
            SQLServer db = new SQLServer(cnn);
            Int32 Result = db.ExecuteNonQuery(query, cmdType, ref parameters);

            //Output results

            parameter = (SqlParameter)parameters[2];
            Show_msg = (String)parameter.Value;

            parameter = (SqlParameter)parameters[3];
            Dev_msg = (String)parameter.Value;

            return Result;
        }





        /*
            getTareas - - - - - - - - - - - - - - - - - - -
         */

        public List<Entidad.Tareas> getTareas(Int32 IDTareas, ref String Show_msg, ref String Dev_msg)
        {
            List<Entidad.Tareas> ListaCronometro = new List<Entidad.Tareas>();

            ArrayList parameters = new ArrayList();

            //Input parameters

            getParamList(0, "IDTareas", IDTareas, ref parameters, ParameterDirection.Input, SqlDbType.Int);

            string query = "getTareas_sp";
            CommandType cmdType = CommandType.StoredProcedure;

            SQLServer db = new SQLServer(cnn);
            DataTable Dt_Cronometro = db.ExecuteReader(query, cmdType, ref parameters);

            ListaCronometro = LoadTareasCronometradas(Dt_Cronometro);

            return ListaCronometro;        
        }

        public static List<Entidad.Tareas> LoadTareasCronometradas(DataTable DT)
        {
            List<Entidad.Tareas> Cronometro = new List<Entidad.Tareas>();
            Entidad.Tareas CronoList = new Entidad.Tareas();
            Int32 NoRows = DT.Rows.Count;

            for (Int32 j = 0; j < NoRows; j++)
            {
                CronoList.IDTareas = Convert.ToInt32(DT.Rows[j]["IDTareas"]);
                CronoList.Componente = Convert.ToString(DT.Rows[j]["Componente"]);
                CronoList.ClaveTareas = Convert.ToString(DT.Rows[j]["ClaveTareas"]);
                CronoList.Nombre = Convert.ToString(DT.Rows[j]["Nombre"]);
                CronoList.Descripcion = Convert.ToString(DT.Rows[j]["Descripcion"]);
                CronoList.Cliente = Convert.ToString(DT.Rows[j]["Cliente"]);
                CronoList.FechaRegistro = Convert.ToDateTime(DT.Rows[j]["FechaRegistro"]);
                CronoList.FechaInicio = Convert.ToDateTime(DT.Rows[j]["FechaInicio"]);
                CronoList.FechaFinEstimada = Convert.ToDateTime(DT.Rows[j]["FechaFinEstimada"]);
                CronoList.FechaFinReal = Convert.ToDateTime(DT.Rows[j]["FechaFinReal"]);
                CronoList.HorasEstimadas = (TimeSpan)(DT.Rows[j]["HorasEstimadas"]);
                CronoList.HorasReales = (TimeSpan)(DT.Rows[j]["HorasReales"]);
                CronoList.Tecnologias = Convert.ToString(DT.Rows[j]["Tecnologias"]);
                CronoList.ClaveTareas = Convert.ToString(DT.Rows[j]["Estado"]);

                Cronometro.Add(CronoList);
            }

            return Cronometro;
        }

        public List<Entidad.getRegistroTareas_view> getRegistroTareas(Int32 IDTareas, ref Boolean Nulo, ref String IdAbiertoString, ref String Show_msg)
        {
            List<Entidad.getRegistroTareas_view> ListaRegistroTareas = new List<Entidad.getRegistroTareas_view>();

            string query = "getRegistroTareas_sp";
            CommandType cmdType = CommandType.StoredProcedure;
            SqlParameter parameter;
            ArrayList parameters = new ArrayList();

            //Input parameters

            getParamList(0, "IDTareas", IDTareas, ref parameters, ParameterDirection.Input, SqlDbType.Int);
            
            //Output parameters

            getParamList(1, "Nulo", Nulo, ref parameters, ParameterDirection.Output, SqlDbType.Bit);
            getParamList(2, "IdAbiertoString", IdAbiertoString, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 6);
            getParamList(3, "Show_msg", Show_msg, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 255);

            SQLServer db = new SQLServer(cnn);
            DataTable Dt_RegistroTareas = db.ExecuteReader(query, cmdType, ref parameters);

            //Output results

            parameter = (SqlParameter)parameters[1];
            Nulo = (Boolean)parameter.Value;



            parameter = (SqlParameter)parameters[2];
            IdAbiertoString = (String)parameter.Value;

            parameter = (SqlParameter)parameters[3];
            Show_msg = (String)parameter.Value;

            ListaRegistroTareas = LoadRegistroTareas(Dt_RegistroTareas);

            return ListaRegistroTareas;
        }

        public static List<Entidad.getRegistroTareas_view> LoadRegistroTareas(DataTable DT)
        {
            List<Entidad.getRegistroTareas_view> RegTareaList = new List<Entidad.getRegistroTareas_view>();
           
            Int32 NoRows = DT.Rows.Count;
            Int32 j = 0;

            foreach (DataRow dr in DT.Rows)
            {
                Entidad.getRegistroTareas_view RegTarea = new Entidad.getRegistroTareas_view();

                RegTarea.Id_TareasRegTiempo = Convert.ToInt32(dr["Id_TareasRegTiempo"]);
                RegTarea.FechaRegistro = Convert.ToString(dr["FechaRegistro"]);
                RegTarea.Hora = Convert.ToString(dr["Hora"]);
                RegTarea.HoraFin = Convert.ToString(dr["HoraFin"]);
                RegTarea.Estatus = Convert.ToString(dr["Estatus"]);
                RegTarea.Notas = Convert.ToString(dr["Notas"]);


                RegTareaList.Add(RegTarea);
            }

            //for (Int32 j = 0; j < NoRows; j++)
            //{
            //    RegTarea.Id_TareasRegTiempo = Convert.ToInt32(DT.Rows[j]["Id_TareasRegTiempo"]);
            //    RegTarea.Hora = Convert.ToString(DT.Rows[j]["Hora"]);
            //    RegTarea.FechaRegistro = Convert.ToString(DT.Rows[j]["FechaRegistro"]);
            //    RegTarea.Estatus = Convert.ToString(DT.Rows[j]["Estatus"]);
            //    RegTarea.Notas = Convert.ToString(DT.Rows[j]["Notas"]);


            //    RegTareaList.Add(RegTarea);
            //}

            return RegTareaList;
        }

        public Int32 DetenerActividad(Int32 Id_TareasRegTiempo, Int32 IDTareas, String Notas, ref String HoraEvento, ref String Show_msg, ref String Dev_msg)
        {
            string query = "setActividadDetenida_sp";
            SqlParameter parameter;
            CommandType cmdType = CommandType.StoredProcedure;

            ArrayList parameters = new ArrayList();

            //Input parameters

            getParamList(0, "Id_TareasRegTiempo", Id_TareasRegTiempo, ref parameters, ParameterDirection.Input, SqlDbType.Int);
            getParamList(1, "IDTareas", IDTareas, ref parameters, ParameterDirection.Input, SqlDbType.Int);
            getParamList(2, "Notas", Notas, ref parameters, ParameterDirection.Input, SqlDbType.VarChar,255);

         
            //Output parameters
            getParamList(3, "HoraEvento", HoraEvento, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 10);
            getParamList(4, "Show_msg", Show_msg, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 250);
            getParamList(5, "Dev_msg", Dev_msg, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 250);

            //Execute SP
            SQLServer db = new SQLServer(cnn);
            Int32 Result = db.ExecuteNonQuery(query, cmdType, ref parameters);

            //Output results
            parameter = (SqlParameter)parameters[3];
            HoraEvento = (String)parameter.Value;

            parameter = (SqlParameter)parameters[4];
            Show_msg = (String)parameter.Value;

            parameter = (SqlParameter)parameters[5];
            Dev_msg = (String)parameter.Value;

            return Result;
        }

        public Int32 FinalizarTarea(Int32 IDTareas,  ref String Show_msg, ref String Dev_msg)
        {
            string query = "setFinalizarTarea_sp";
            SqlParameter parameter;
            CommandType cmdType = CommandType.StoredProcedure;

            ArrayList parameters = new ArrayList();

            //Input parameters

            getParamList(0, "IDTareas", IDTareas, ref parameters, ParameterDirection.Input, SqlDbType.Int);

            //Output parameters
            getParamList(1, "Show_msg", Show_msg, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 250);
            getParamList(2, "Dev_msg", Dev_msg, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 250);

            //Execute SP
            SQLServer db = new SQLServer(cnn);
            Int32 Result = db.ExecuteNonQuery(query, cmdType, ref parameters);

            //Output results

            parameter = (SqlParameter)parameters[1];
            Show_msg = (String)parameter.Value;

            parameter = (SqlParameter)parameters[2];
            Dev_msg = (String)parameter.Value;

            return Result;
        }

        public Int32 getTiempoTotalTarea(Int32 IDTareas, ref String TiempoTotal)
        {
            string query = "getTiempoCalculadoTarea_sp";
            SqlParameter parameter;
            CommandType cmdType = CommandType.StoredProcedure;

            ArrayList parameters = new ArrayList();

            //Input parameters

            getParamList(0, "IDTareas", IDTareas, ref parameters, ParameterDirection.Input, SqlDbType.Int);

            //Output parameters
            getParamList(1, "TiempoTotal", TiempoTotal, ref parameters, ParameterDirection.Output, SqlDbType.VarChar, 8);

            //Execute SP
            SQLServer db = new SQLServer(cnn);
            Int32 Result = db.ExecuteNonQuery(query, cmdType, ref parameters);

            //Output results

            parameter = (SqlParameter)parameters[1];
            TiempoTotal = (String)parameter.Value;

            return Result;
        }

        public Int32 getMostrarTareasxUsuario(string Usuario, ref int Visible)
        {
            string query="getMostrarTareas_sp";
            SqlParameter parameter;
            CommandType cmdType = CommandType.StoredProcedure;

            ArrayList parameters = new ArrayList();

            getParamList(0, "Usuario", Usuario, ref parameters, ParameterDirection.Input, SqlDbType.VarChar, 30);
            getParamList(1, "visible", Visible, ref parameters, ParameterDirection.Output, SqlDbType.Int);

            SQLServer db = new SQLServer(cnn);
            Int32 Result = db.ExecuteNonQuery(query, cmdType, ref parameters);

            //Output results

            parameter = (SqlParameter)parameters[1];
            Visible = (Int32)parameter.Value;

            return Result;
        }

        public Int32 getMostrarTareasxPerfil(string Usuario, ref int Visible)
        {
            int Perfil = -1;
            getPerfil(Usuario, ref Perfil);

            string query = "getMostrarTareasPerfil_sp";
            SqlParameter parameter;
            CommandType cmdType = CommandType.StoredProcedure;

            ArrayList parameters = new ArrayList();

            getParamList(0, "Perfil", Perfil, ref parameters, ParameterDirection.Input, SqlDbType.VarChar, 30);
            getParamList(1, "visible", Visible, ref parameters, ParameterDirection.Output, SqlDbType.Int);

            SQLServer db = new SQLServer(cnn);
            Int32 Result = db.ExecuteNonQuery(query, cmdType, ref parameters);

            //Output results

            parameter = (SqlParameter)parameters[1];
            Visible = (Int32)parameter.Value;

            return Result;
        }

        public Int32 getPerfil(String Usuario, ref int Perfil)
        {
            string query = "getPerfil_sp";
            SqlParameter parameter;
            CommandType cmdType = CommandType.StoredProcedure;

            ArrayList parameters = new ArrayList();

            getParamList(0, "Usuario", Usuario, ref parameters, ParameterDirection.Input, SqlDbType.VarChar, 30);
            getParamList(1, "Perfil", Perfil, ref parameters, ParameterDirection.Output, SqlDbType.Int);

            SQLServer db = new SQLServer(cnn);
            Int32 Result = db.ExecuteNonQuery(query, cmdType, ref parameters);

            //Output results

            parameter = (SqlParameter)parameters[1];
            Perfil = (Int32)parameter.Value;

            return Result;
        }

        public Int32 getIDUsuario(String Usuario, ref int IDUsuario)
        {
            string query = "getIDUsuario_sp";
            SqlParameter parameter;
            CommandType cmdType = CommandType.StoredProcedure;

            ArrayList parameters = new ArrayList();

            getParamList(0, "Usuario", Usuario, ref parameters, ParameterDirection.Input, SqlDbType.VarChar, 30);
            getParamList(1, "IDUsuario", IDUsuario, ref parameters, ParameterDirection.Output, SqlDbType.Int);

            SQLServer db = new SQLServer(cnn);
            Int32 Result = db.ExecuteNonQuery(query, cmdType, ref parameters);

            //Output results

            parameter = (SqlParameter)parameters[1];
            IDUsuario = (Int32)parameter.Value;

            return Result;
        }

        private void getParamList(int position, string PName, Object PValue, ref ArrayList Parameters, ParameterDirection PDirection, SqlDbType PType, int PSize = 0)
        {
            SqlParameter parameter;

            Parameters.Add(new SqlParameter("@" + PName, PType, PSize));

            parameter = (SqlParameter)Parameters[position];
            parameter.Direction = PDirection;

            if (PValue.GetType() == typeof(Byte))
            {
                parameter.Value = (Byte)PValue;
            }

            if (PValue.GetType() == typeof(Int16))
            {
                parameter.Value = (Int16)PValue;
            }

            if (PValue.GetType() == typeof(Int32))
            {
                parameter.Value = (Int32)PValue;
            }

            if (PValue.GetType() == typeof(String))
            {
                parameter.Value = (String)PValue;
            }

            if (PValue.GetType() == typeof(Boolean))
            {
                parameter.Value = (Boolean)PValue;
            }

            if (PValue.GetType() == typeof(DateTime))
            {
                parameter.Value = (DateTime)PValue;
            }

            if (PValue.GetType() == typeof(Int64))
            {
                parameter.Value = (Int64)PValue;
            }

        }
    }

}