<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarTiempoTareas.aspx.cs" Inherits="ProyectosWeb.RegistrarTiempoTareas" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/RegistroActividades/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/RegistroActividades/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="css/RegistroActividades/Default.css" rel="stylesheet" type="text/css" />
    <script src="Resources/Scripts/modernizr-2.8.3.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-primary" id="CollapsiblePanel">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-toggle="collapse" data-parent="#CollapsiblePanel" href="#stepTwo" id="Lnk">Informaci&oacute;n
                    de la tarea seleccionada</a></h4>
        </div>
        <div id="stepTwo" class="panel-collapse collapse in">
            <div class="panel-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table class="table table-bordered table-condensed">
                            <tbody>
                                <tr>
                                    <td class="lead">
                                        Tarea
                                    </td>
                                    <td class="Field450px">
                                        <asp:Label ID="lbl_Nombre" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="success">
                                        ID
                                    </td>
                                    <td class="Field450px fonte">
                                        <asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="success">
                                        Componente
                                    </td>
                                    <td class="Field450px fonte">
                                        <asp:Label ID="lbl_Componente" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="success">
                                        Clave
                                    </td>
                                    <td class="Field450px">
                                        <asp:Label ID="lbl_Clave" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class="table table-bordered table-condensed">
                            <thead>
                                <tr>
                                    <td class=" lead">
                                        Descripci&oacute;n
                                    </td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="Field450px fonte">
                                        <asp:Label ID="lbl_Descripcion" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- se cierra el primer panel y el div stepTwo y el panel-->
            </div>
        </div>
    </div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <h4 class="panel-title">
                Registrar actividad</h4>
        </div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnk_Iniciar" CssClass="btn alert-success btn-lg " runat="server"
                                        ToolTip="Iniciar registro de actividad" OnClick="lnk_Iniciar_Click"><span class="glyphicon glyphicon-play"></span></asp:LinkButton>
                                </td>
                                <td class="Separador_v">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnk_Pausa" CssClass="btn btn-default" runat="server" OnClick="lnk_Pausa_Click"><span class="glyphicon glyphicon-pause"> Pausa</span></asp:LinkButton>
                                </td>
                                <td class="Separador_v">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnk_Finalizar" CssClass="btn btn-default" runat="server" OnClick="lnk_Finalizar_Click"
                                        OnClientClick="return confirm('Una vez finalizada la tarea no podrá continuar registrando actividades sobre la misma, ¿Desea continuar de todas maneras?');"><span class="glyphicon glyphicon-stop"> Finalizar</span></asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- panel body -->
    </div>
    <!-- panel info -->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table class="table table-bordered table-condensed">
                <thead>
                    <tr>
                        <td class=" lead">
                            Agregar Notas
                        </td>
                        <td>
                            <table id="tbl_HoraEvento" runat="server">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="udp_HoraRegistrada" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnl_HoraRegistrada" runat="server" Width="100%" Height="100%">
                                                    <asp:Label ID="lbl_HoraRegistrada" CssClass="lead" runat="server" Text=""></asp:Label>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="Field450px fonte">
                            <asp:TextBox ID="Notas_txt" runat="server" CssClass="textbox Multiline" Text="" placeholder="Agregar notas.."
                                MaxLength="255"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Panel ID="pnl_ProgressBar" runat="server">
                                <asp:Label ID="lbl_EstatusGeneralTarea" CssClass="lead" runat="server" Text="Label"></asp:Label>
                                <br />
                                <progress>
                                </progress>
                            </asp:Panel>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lbl_TiempoTot" CssClass="" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbl_TiempoTotal" CssClass="lead alert-success" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd_Registros" runat="server" CssClass="table table-bordered table-condensed"
                        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grd_Registros_PageIndexChanging"
                        DataKeyNames="Id_TareasRegTiempo" EnablePersistedSelection="True" OnSelectedIndexChanged="grd_Registros_SelectedIndexChanged"
                        PageSize="5">
                        <Columns>
                            <asp:BoundField DataField="Id_TareasRegTiempo" HeaderText="Id Registro" ReadOnly="True"
                                SortExpression="Id_TareasRegTiempo" />
                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de registro" SortExpression="FechaRegistro" />
                            <asp:BoundField DataField="Hora" ControlStyle-CssClass="success" HeaderText="Hora (Inicio)"
                                SortExpression="Hora">
                                <ControlStyle CssClass="success" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Resources/Images/ICO/comment.ico"
                                SelectText="Notas" ShowSelectButton="True" />
                            <asp:BoundField DataField="Estatus" HeaderText="Estatus" SortExpression="Estatus" />
                            <asp:BoundField DataField="HoraFin" ControlStyle-CssClass="success" HeaderText="Pausado"
                                SortExpression="HoraFin">
                                <ControlStyle CssClass="success" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="panel panel-warning">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Notas</h4>
                </div>
                <div class="panel-body">
                    <asp:Label ID="lbl_NotasRegistradas" CssClass="" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="Resources/Scripts/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="Resources/Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="Resources/Scripts/jquery.cookie.js" type="text/javascript"></script>
    </form>
</body>
</html>
