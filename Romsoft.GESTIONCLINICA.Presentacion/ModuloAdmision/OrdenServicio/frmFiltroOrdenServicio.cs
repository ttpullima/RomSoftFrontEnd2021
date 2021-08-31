using Newtonsoft.Json;
using Romsoft.GESTIONCLINICA.Common;
using Romsoft.GESTIONCLINICA.DTO.TABLAS.CVN_TARIFARIO_SEGUS;
using Romsoft.GESTIONCLINICA.Presentacion.ApiService;
using Romsoft.GESTIONCLINICA.Presentacion.Core;
using Romsoft.GESTIONCLINICA.Presentacion.Helpers;
using Romsoft.GESTIONCLINICA.Presentacion.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Romsoft.GESTIONCLINICA.Presentacion.ModuloAdmision.OrdenServicio
{
    public partial class frmFiltroOrdenServicio : Form
    {
        public frmFiltroOrdenServicio()
        {
            InitializeComponent();
        }


        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(this.txtValor.Text))
                {
                    this.errValidator.SetError(this.txtValor, "Ingresar Valor para Búsqueda.");

                }
                else
                {

                    //Muestra Todo por filtro

                    var jsonResponse = new JsonResponse { Success = false };

                    jsonResponse = InvokeHelper.MakeRequest(ConstantesWindows.WS_CVN_TARIFARIO_SEGUS_GetPrice, GetTarifaFitro(), ConstantesWindows.METHODPOST);

                    if (jsonResponse.Success && !jsonResponse.Warning)
                    {
                        var tarifaListDTO = (List<CVN_TARIFARIO_SEGUS_PRICEDTO>)JsonConvert.DeserializeObject(jsonResponse.Data.ToString(), (new List<CVN_TARIFARIO_SEGUS_PRICEDTO>()).GetType());
                        this.ETTarifario.DataSource = tarifaListDTO;
                        //this.usuarioBindingNavigator.BindingSource = this.usuarioBindingSource;
                    }
                    else if (jsonResponse.Warning)
                    {
                        Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, jsonResponse.Message);
                    }

                }


            }
            catch (Exception ex)
            {
                Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, ex.Message);
            }
        }

        //Asigna Datos para el Filtro
        private CVN_TARIFARIO_SEGUS_PRICEReqDTO GetTarifaFitro()
        {
            return new CVN_TARIFARIO_SEGUS_PRICEReqDTO
            {
                valor = txtValor.Text.Trim(),
                c_idioma="E"
            };
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                //this.estadoActual = EstadoActual.Normal;

                string message = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["Valida"].Value);
                    if (isSelected)
                    {
                        message ="Seleccion";
                        //message += row.Cells["Name"].Value.ToString();

                        ComunFilter.ft_codigo = row.Cells[1].Value.ToString();
                        ComunFilter.ft_descripcion = row.Cells[2].Value.ToString();
                        ComunFilter.ft_clasificacion = row.Cells[3].Value.ToString();
                        ComunFilter.ft_precio = Convert.ToDecimal(row.Cells[4].Value.ToString());
                        ComunFilter.ft_cantidad = Convert.ToInt32(row.Cells[5].Value.ToString());
                        ComunFilter.ft_total = (ComunFilter.ft_precio * ComunFilter.ft_cantidad);

                    }

                }

                if(message!="")
                {
                    DialogResult = DialogResult.OK;

                    this.Close();
                }
                else
                {
                    Mensaje.ShowMessageAlert(this.ParentForm, "OrdenServicio", "Tiene qu marcar un registro");
                }

                

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (dataGridView1.Rows.Count > 0)
            //    {
            //        //ComunFilter.ft_codigo = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //        //ComunFilter.ft_descripcion = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //        //ComunFilter.ft_clasificacion = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //        //ComunFilter.ft_precio = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value.ToString());
            //        //ComunFilter.ft_cantidad = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            //        //ComunFilter.ft_total = (ComunFilter.ft_precio * ComunFilter.ft_cantidad);

            //    }
            //}

            //catch (Exception ex)
            //{
            //    Mensaje.ShowMessageAlert(this.ParentForm, ConstantesWindows.TituloMensaje, ex.Message);
            //}
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnBuscar.PerformClick();
            }
        }
    }
}
