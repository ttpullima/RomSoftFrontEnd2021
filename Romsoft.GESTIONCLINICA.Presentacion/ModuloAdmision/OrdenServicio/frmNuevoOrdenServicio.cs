using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Romsoft.GESTIONCLINICA.Common;
using Romsoft.GESTIONCLINICA.DTO.TABLAS.ADM_ATENCION;
using Romsoft.GESTIONCLINICA.Presentacion.ApiService;
using Romsoft.GESTIONCLINICA.Presentacion.Core;
using Romsoft.GESTIONCLINICA.Presentacion.Helpers;
using Romsoft.GESTIONCLINICA.Presentacion.ModuloFacturacion.Prefacturacion;
using Romsoft.GESTIONCLINICA.Presentacion.Utilities;

namespace Romsoft.GESTIONCLINICA.Presentacion.ModuloAdmision.OrdenServicio
{
    public partial class frmNuevoOrdenServicio : Form
    {
        public frmNuevoOrdenServicio()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            // Hola    
            frmPrefacturacion frm = new frmPrefacturacion();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                // 0 = Consulta Todos
                //    InitialLoad(0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrdenServicio.frmFiltroProfesional frm = new OrdenServicio.frmFiltroProfesional();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // 0 = Consulta Todos
                //    InitialLoad(0);
            }
        }

        private void BtnProducto_Click(object sender, EventArgs e)
        {
            OrdenServicio.frmFiltroPacienteCuenta frm = new OrdenServicio.frmFiltroPacienteCuenta();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // 0 = Consulta Todos
                //    InitialLoad(0);
            }
        }

        private void BtnOrdenGuia_Click(object sender, EventArgs e)
        {

        }

        //Carga combos
        private void CargarComboTipoPaciente()
        {
            try
            {
                var jsonResponse = new JsonResponse { Success = false };
                jsonResponse = InvokeHelper.MakeRequest(ConstantesWindows.WS_ADM_TIPO_PACIENTE_GetAllActives, new DTO.TABLAS.ADM_TIPO_PACIENTE.ADM_TIPO_PACIENTEDTO { }, ConstantesWindows.METHODPOST);

                if (jsonResponse.Success && !jsonResponse.Warning)
                {
                    var tipopaListDTO = (List<DTO.TABLAS.ADM_TIPO_PACIENTE.ADM_TIPO_PACIENTEDTO>)JsonConvert.DeserializeObject(jsonResponse.Data.ToString(), (new List<DTO.TABLAS.ADM_TIPO_PACIENTE.ADM_TIPO_PACIENTEDTO>()).GetType());
                    CboTipoPaciente.ValueMember = "id_tipo_paciente";
                    CboTipoPaciente.DisplayMember = "t_descripcion";
                    CboTipoPaciente.DataSource = tipopaListDTO;
                    CboTipoPaciente.SelectedIndex = -1;
                }
                else if (jsonResponse.Warning)
                {
                    Cursor.Current = Cursors.Default;
                    Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, jsonResponse.Message);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, ex.Message);
            }
        }

        private void CargarComboTipoAtencion()
        {
            try
            {
                var jsonResponse = new JsonResponse { Success = false };
                jsonResponse = InvokeHelper.MakeRequest(ConstantesWindows.WS_ADM_TIPO_ATENCION_GetAllActives, new DTO.TABLAS.ADM_TIPO_ATENCION.ADM_TIPO_ATENCIONDTO { }, ConstantesWindows.METHODPOST);

                if (jsonResponse.Success && !jsonResponse.Warning)
                {
                    var tipopatListDTO = (List<DTO.TABLAS.ADM_TIPO_ATENCION.ADM_TIPO_ATENCIONDTO>)JsonConvert.DeserializeObject(jsonResponse.Data.ToString(), (new List<DTO.TABLAS.ADM_TIPO_ATENCION.ADM_TIPO_ATENCIONDTO>()).GetType());
                    CboTipoAtencion.ValueMember = "id_tipo_atencion";
                    CboTipoAtencion.DisplayMember = "t_descripcion";
                    CboTipoAtencion.DataSource = tipopatListDTO;
                    CboTipoAtencion.SelectedIndex = -1;
                }
                else if (jsonResponse.Warning)
                {
                    Cursor.Current = Cursors.Default;
                    Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, jsonResponse.Message);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, ex.Message);
            }
        }

        private void CargarComboPlanSeguro()
        {
            try
            {
                var jsonResponse = new JsonResponse { Success = false };
                jsonResponse = InvokeHelper.MakeRequest(ConstantesWindows.WS_CVN_PLAN_SEGURO_GetAllActives, new DTO.TABLAS.CVN_PLAN_SEGURO.CVN_PLAN_SEGURODTO { }, ConstantesWindows.METHODPOST);

                if (jsonResponse.Success && !jsonResponse.Warning)
                {
                    var planseguroListDTO = (List<DTO.TABLAS.CVN_PLAN_SEGURO.CVN_PLAN_SEGURODTO>)JsonConvert.DeserializeObject(jsonResponse.Data.ToString(), (new List<DTO.TABLAS.CVN_PLAN_SEGURO.CVN_PLAN_SEGURODTO>()).GetType());
                    CboPlanSeguro.ValueMember = "id_plan_seguro";
                    CboPlanSeguro.DisplayMember = "t_descripcion";
                    CboPlanSeguro.DataSource = planseguroListDTO;
                    CboPlanSeguro.SelectedIndex = -1;
                }
                else if (jsonResponse.Warning)
                {
                    Cursor.Current = Cursors.Default;
                    Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, jsonResponse.Message);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, ex.Message);
            }
        }

        private void CargarComboBeneficio()
        {
            try
            {
                var jsonResponse = new JsonResponse { Success = false };
                jsonResponse = InvokeHelper.MakeRequest(ConstantesWindows.WS_CVN_BENEFICIO_GetAllActives, new DTO.TABLAS.CVN_BENEFICIO.CVN_BENEFICIODTO { }, ConstantesWindows.METHODPOST);

                if (jsonResponse.Success && !jsonResponse.Warning)
                {
                    var beneficioListDTO = (List<DTO.TABLAS.CVN_BENEFICIO.CVN_BENEFICIODTO>)JsonConvert.DeserializeObject(jsonResponse.Data.ToString(), (new List<DTO.TABLAS.CVN_BENEFICIO.CVN_BENEFICIODTO>()).GetType());
                    CboBeneficio.ValueMember = "id_beneficio";
                    CboBeneficio.DisplayMember = "t_descripcion";
                    CboBeneficio.DataSource = beneficioListDTO;
                    CboBeneficio.SelectedIndex = -1;
                }
                else if (jsonResponse.Warning)
                {
                    Cursor.Current = Cursors.Default;
                    Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, jsonResponse.Message);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, ex.Message);
            }
        }

        private void CargarComboCategoriaPago()
        {
            try
            {
                var jsonResponse = new JsonResponse { Success = false };
                jsonResponse = InvokeHelper.MakeRequest(ConstantesWindows.WS_CVN_CATEGORIA_PAGO_GetAllActives, new DTO.TABLAS.CVN_CATEGORIA_PAGO.CVN_CATEGORIA_PAGODTO { }, ConstantesWindows.METHODPOST);

                if (jsonResponse.Success && !jsonResponse.Warning)
                {
                    var catpagoListDTO = (List<DTO.TABLAS.CVN_CATEGORIA_PAGO.CVN_CATEGORIA_PAGODTO>)JsonConvert.DeserializeObject(jsonResponse.Data.ToString(), (new List<DTO.TABLAS.CVN_CATEGORIA_PAGO.CVN_CATEGORIA_PAGODTO>()).GetType());
                    CboCategoriaPago.ValueMember = "id_categoria_pago";
                    CboCategoriaPago.DisplayMember = "t_descripcion";
                    CboCategoriaPago.DataSource = catpagoListDTO;
                    CboCategoriaPago.SelectedIndex = -1;
                }
                else if (jsonResponse.Warning)
                {
                    Cursor.Current = Cursors.Default;
                    Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, jsonResponse.Message);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, ex.Message);
            }
        }

        private void CargarComboMedicoTratante()
        {
            try
            {
                var jsonResponse = new JsonResponse { Success = false };
                jsonResponse = InvokeHelper.MakeRequest(ConstantesWindows.WS_ADM_PROFESIONAL_GetAllActives, new DTO.TABLAS.ADM_PROFESIONAL.ADM_PROFESIONALDTO { }, ConstantesWindows.METHODPOST);

                if (jsonResponse.Success && !jsonResponse.Warning)
                {
                    var profesionalListDTO = (List<DTO.TABLAS.ADM_PROFESIONAL.ADM_PROFESIONALDTO>)JsonConvert.DeserializeObject(jsonResponse.Data.ToString(), (new List<DTO.TABLAS.ADM_PROFESIONAL.ADM_PROFESIONALDTO>()).GetType());
                    CboMedico.ValueMember = "id_profesional";
                    CboMedico.DisplayMember = "t_medico";
                    CboMedico.DataSource = profesionalListDTO;
                    CboMedico.SelectedIndex = -1;
                }
                else if (jsonResponse.Warning)
                {
                    Cursor.Current = Cursors.Default;
                    Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, jsonResponse.Message);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, ex.Message);
            }
        }

        private void CargarComboEspecialidad(int Id_Profesional)
        {
            try
            {
                var jsonResponse = new JsonResponse { Success = false };
                jsonResponse = InvokeHelper.MakeRequest(ConstantesWindows.WS_ADM_ESPECIALIDAD_GetAllActives, new DTO.TABLAS.ADM_ESPECIALIDAD.ADM_ESPECIALIDADPROFESIONALDTO { id_profesional = Convert.ToInt32(CboMedico.SelectedValue) }, ConstantesWindows.METHODPOST);

                if (jsonResponse.Success && !jsonResponse.Warning)
                {
                    var especialidadlListDTO = (List<DTO.TABLAS.ADM_ESPECIALIDAD.ADM_ESPECIALIDADDTO>)JsonConvert.DeserializeObject(jsonResponse.Data.ToString(), (new List<DTO.TABLAS.ADM_ESPECIALIDAD.ADM_ESPECIALIDADDTO>()).GetType());
                    CboEspecialidad.ValueMember = "id_especialidad";
                    CboEspecialidad.DisplayMember = "t_descripcion";
                    CboEspecialidad.DataSource = especialidadlListDTO;
                    //CboEspecialidad.SelectedIndex = -1;
                }
                else if (jsonResponse.Warning)
                {
                    Cursor.Current = Cursors.Default;
                    Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, jsonResponse.Message);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, ex.Message);
            }
        }

        private void CargarComboMoneda()
        {
            try
            {
                var jsonResponse = new JsonResponse { Success = false };
                jsonResponse = InvokeHelper.MakeRequest(ConstantesWindows.WS_CVN_MONEDA_GetAllActives, new DTO.TABLAS.CVN_MONEDA.CVN_MONEDADTO { }, ConstantesWindows.METHODPOST);

                if (jsonResponse.Success && !jsonResponse.Warning)
                {
                    var motivoaltalListDTO = (List<DTO.TABLAS.CVN_MONEDA.CVN_MONEDADTO>)JsonConvert.DeserializeObject(jsonResponse.Data.ToString(), (new List<DTO.TABLAS.CVN_MONEDA.CVN_MONEDADTO>()).GetType());
                    CboMoneda.ValueMember = "id_moneda";
                    CboMoneda.DisplayMember = "t_descripcion";
                    CboMoneda.DataSource = motivoaltalListDTO;
                    CboMoneda.SelectedIndex = -1;


                }
                else if (jsonResponse.Warning)
                {
                    Cursor.Current = Cursors.Default;
                    Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, jsonResponse.Message);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, ex.Message);
            }
        }


        private void GetDatosAtencion(int idAtencion)
        {


            var jsonResponse = new JsonResponse { Success = false };

            //Consulta Cabecera//
            jsonResponse = InvokeHelper.MakeRequest(ConstantesWindows.WS_ADM_ATENCION_GetById, new DTO.TABLAS.ADM_ATENCION.ADM_ATENCION_RequestDTO { IdAtencion = idAtencion }, ConstantesWindows.METHODPOST);

            if (jsonResponse.Success && !jsonResponse.Warning)
            {
                var atencionListDTO = (List<ADM_ATENCIONDTO>)JsonConvert.DeserializeObject(jsonResponse.Data.ToString(), (new List<ADM_ATENCIONDTO>()).GetType());
                //this.ETUsuariobindingSource.DataSource = usuarioListDTO;
                //--------------------------

                for (int i = 0; i < atencionListDTO.Count; i++)
                {
                    txtNumHistoria.Text = ComunFilter.f_NumHistoriaClinica.ToString();
                    txtCtaCte.Text = idAtencion.ToString();
                    txtNombrePaciente.Text = ComunFilter.f_NombrePaciente;
                    CboTipoPaciente.SelectedValue = Convert.ToInt32(atencionListDTO[i].id_tipo_paciente.ToString());
                    CboTipoAtencion.SelectedValue = Convert.ToInt32(atencionListDTO[i].id_tipo_atencion.ToString());
                    CboPlanSeguro.SelectedValue = Convert.ToInt32(atencionListDTO[i].id_plan_seguro.ToString());
                    CboCategoriaPago.SelectedValue = Convert.ToInt32(atencionListDTO[i].id_categoria_pago.ToString());
                    CboBeneficio.SelectedValue = Convert.ToInt32(atencionListDTO[i].id_beneficio.ToString());
                    CboMoneda.SelectedValue = Convert.ToInt32(atencionListDTO[i].id_moneda.ToString());
                    TxtCopago.Text = (atencionListDTO[i].n_copago_fijo.ToString());
                    TxtCoaseguro.Text = (atencionListDTO[i].n_copago_variable.ToString());
                    TxtCoaseguroFarmacia.Text = (atencionListDTO[i].n_copago_variable_far.ToString());

                    CboMedico.SelectedValue = Convert.ToInt32(atencionListDTO[i].id_profesional.ToString());


                }
            }
            else if (jsonResponse.Warning)
            {
                Cursor.Current = Cursors.Default;
                Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, jsonResponse.Message);
            }

        }


     
        private void frmNuevoOrdenServicio_Load_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;

            CargarComboTipoPaciente();
            CargarComboTipoAtencion();
            CargarComboPlanSeguro();
            CargarComboBeneficio();
            CargarComboCategoriaPago();
            CargarComboMedicoTratante();
            CargarComboEspecialidad(Convert.ToInt32(CboMedico.SelectedValue));
            CargarComboMoneda();
            //
            GetDatosAtencion(ComunFilter.f_idAtencion);

            Cursor.Current = Cursors.Default;

        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            ComunFilter.ft_codigo = "";
            ComunFilter.ft_descripcion = "";
            ComunFilter.ft_clasificacion = "";
            ComunFilter.ft_precio = 0;
            ComunFilter.ft_cantidad = 0;
            ComunFilter.ft_total = 0;

            OrdenServicio.frmFiltroOrdenServicio frm = new OrdenServicio.frmFiltroOrdenServicio();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dgvOrdenServicio.Rows.Add(ComunFilter.ft_clasificacion, ComunFilter.ft_codigo, ComunFilter.ft_descripcion, ComunFilter.ft_precio, ComunFilter.ft_cantidad, ComunFilter.ft_total, ComunFilter.ft_total,Convert.ToDecimal(TxtCoaseguro.Text),9999,DateTime.Now);

            }

            //Totales
            calculaTotales();
        }

        private void calculaTotales()
        {
            try
            {
                decimal suma = 0;
                decimal Paciente = 0;
                decimal coaseguro = 0;

                foreach (DataGridViewRow row in dgvOrdenServicio.Rows)
                {
                    if (row.Cells["Total"].Value != null)
                        suma += (decimal)row.Cells["Total"].Value;
                }
                txtTotal.Text = suma.ToString();

                //paciente
                foreach (DataGridViewRow row in dgvOrdenServicio.Rows)
                {
                    if (row.Cells["Paciente"].Value != null)
                        Paciente += (decimal)row.Cells["Paciente"].Value;
                }
                txtPaciente.Text = Paciente.ToString();

                //paciente
                foreach (DataGridViewRow row in dgvOrdenServicio.Rows)
                {
                    if (row.Cells["Coaseguro"].Value != null)
                        coaseguro += (decimal)row.Cells["Coaseguro"].Value;
                }
                txtSeguro.Text = coaseguro.ToString();
            }
            catch (Exception ex)
            {
                txtTotal.Text = "0.00";
                txtPaciente.Text = "0.00";
                TxtCoaseguro.Text = "0.00";
            }

        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvOrdenServicio.SelectedRows.Count > 0)
            {
                dgvOrdenServicio.Rows.RemoveAt(this.dgvOrdenServicio.SelectedRows[0].Index);

            }
            //Totales
            calculaTotales();
        }
    }
}
