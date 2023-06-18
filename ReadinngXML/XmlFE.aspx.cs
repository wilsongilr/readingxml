using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;

namespace ReadinngXML
{

    public partial class XmlFE : System.Web.UI.Page
    {
       // string datos="";

        public DataTable dtDetalles = new DataTable();
        public string sDescription = "";
        public string sVrUnitario = "";
        public string sCant = "";
        public string sCodigo = "";
        public int filas = 0;
        

        protected void Page_Load(object sender, EventArgs e)
        {


            String.Format("{0:c2}", txtFactura.Value);

            //   string Ruta = @"H:\ReadinngXML\ReadinngXML\fv08002075900152200007965.xml";
            //   lblRuta.Text = Ruta;

            //lblRuta.Text= Path.Combine(Application StartupPath, "AnalisEstructuraXML.xml");
            //  return;
            //UsingReadXml(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"AnalisEstructuraXML.xml"));



            //   return;

        }
        private static void UsingReadXml(string path)
        {

            //string Archivo = 

            XmlDocument doc = new XmlDocument();

            doc.Load(path);


            foreach (XmlNode n1 in doc.DocumentElement.ChildNodes)
            {
                

                if (n1.HasChildNodes)
                {
                    if (n1.Name == "cbc:ID")
                    {
                        string sNroDcto = n1.InnerText;
                        
                        
                    }
                    if (n1.Name == "cbc:InvoiceTypeCode")
                    {
                        string sTipoDcto = n1.InnerText;
                    }
                    if (n1.Name == "cbc:IssueDate")
                    {
                       string sNroDcto = n1.InnerText;
                        
                    }
                    if (n1.Name == "cac:SenderParty")
                    {
                        foreach (XmlNode n2 in n1.ChildNodes)
                        {
                            if (n2.Name == "cac:PartyTaxScheme")
                            {
                                foreach (XmlNode n3 in n2.ChildNodes)
                                {
                                    if (n3.Name == "cbc:RegistrationName")
                                    {
                                        string sProveedor = n3.InnerText;
                                    }
                                    if (n3.Name == "cbc:CompanyID")
                                    {
                                        string sNit = n3.InnerText;
                                        Console.Write(sNit);
                                    }

                                }
                            }
                        }

                    }

                }


            }

        }

        protected void btnLeer_Click(object sender, EventArgs e)
        {
            

            try
            {
                string filePath = Filexml.PostedFile.FileName;
                if (filePath == "")
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k",
                        "swal.fire('Failed!!','Seleccione Archivo XML','error')", true);
                    return;
                }
                string fileName = Filexml.FileName;// Only file name.
                string Rutaxml = Server.MapPath("XmlLoad/" + fileName);
            //    lblRuta.Text = Rutaxml;

                Filexml.SaveAs(Rutaxml);

                XmlDocument doc = new XmlDocument();
                string Ruta = Rutaxml; //   "D:\Proyectos C#\ReadinngXML\ReadinngXML\fv08002075900152200007965.xml";
                doc.Load(Ruta);

                foreach (XmlNode n1 in doc.DocumentElement.ChildNodes)
                {

                    if (n1.HasChildNodes)
                    {

                        if (n1.Name == "cbc:ID")
                        {
                            string sNroDcto = n1.InnerText;
                            txtFactura.Value = sNroDcto;

                        }
                        if (n1.Name == "cbc:InvoiceTypeCode")
                        {
                            string sTipoDcto = n1.InnerText;
                            //   txtTipoDcto.Value = "TipoDcto: " + sTipoDcto;
                        }
                        if (n1.Name == "cbc:IssueDate")
                        {
                            string sFecha = n1.InnerText;
                            txtFecha.Value = sFecha;

                        }
                        if (n1.Name == "cac:SenderParty")
                        {
                            foreach (XmlNode n2 in n1.ChildNodes)
                            {
                                if (n2.Name == "cac:PartyTaxScheme")
                                {
                                    foreach (XmlNode n3 in n2.ChildNodes)
                                    {
                                        if (n3.Name == "cbc:RegistrationName")
                                        {
                                            string sProveedor = n3.InnerText;
                                            txtRazon.Value = sProveedor;
                                        }
                                        if (n3.Name == "cbc:CompanyID")
                                        {
                                            string sNit = n3.InnerText;
                                            txtNit.Value = sNit;

                                        }

                                    }
                                }
                            }

                        }

                    }
                }
                XElement Xtemp = XElement.Load(Ruta);
                var queryCDATAXML = from element in Xtemp.DescendantNodes()
                                    where element.NodeType == System.Xml.XmlNodeType.CDATA
                                    select element.Parent.Value.Trim();
                string BodyHtml = queryCDATAXML.ToList<string>()[0].ToString();



                XmlDocument docData = new XmlDocument();
                //string Ruta = @"D:\Proyectos C#\ReadinngXML\ReadinngXML\ad08600016150042200256055.xml";
                docData.LoadXml(BodyHtml);

                //dtDetalles.Columns.Add("Item", typeof(string));
                //dtDetalles.Columns.Add("Cantidad", typeof(string));
                //dtDetalles.Columns.Add("VrUnitario", typeof(string));

                dtDetalles.Columns.AddRange(new DataColumn[3] { new DataColumn("Item"), new DataColumn("Cantidad"), new DataColumn("VrUnitario") });

                foreach (XmlNode c1 in docData.DocumentElement.ChildNodes)
                {
                    if (c1.HasChildNodes)
                    {
                        if (c1.Name == "cac:TaxTotal")
                        {
                            foreach (XmlNode c2 in c1.ChildNodes)
                            {
                                if (c2.Name == "cac:TaxSubtotal")
                                {
                                    foreach (XmlNode c3 in c2.ChildNodes)
                                    {
                                        if (c3.Name == "cbc:TaxableAmount")
                                        {
                                            string sVrSubTotal = c3.InnerText;
                                            txtSubTotal.Value = sVrSubTotal;
                                        }
                                        if (c3.Name == "cbc:TaxAmount")
                                        {
                                            Decimal sVrIva = Convert.ToDecimal(c3.InnerText.Replace(".", ","));

                                            txtVrIVA.Value = sVrIva.ToString("C"); ;
                                            //string sVrIva = c3.InnerText;
                                            //txtVrIVA.Value = sVrIva;
                                        }
                                        if (c3.Name == "cac:TaxCategory")
                                        {
                                            foreach (XmlNode c4 in c3.ChildNodes)
                                            {
                                                if (c4.Name == "cbc:Percent")
                                                {
                                                    string sPorIva = c4.InnerText;
                                                    txtIVA.Value = sPorIva;
                                                }
                                            }

                                        }

                                    }
                                }
                            }
                        }
                        if (c1.Name == "cac:WithholdingTaxTotal")
                        {
                            foreach (XmlNode c8 in c1.ChildNodes)
                            {
                                if (c8.Name == "cac:TaxAmount")
                                {
                                    foreach (XmlNode c3 in c8.ChildNodes)
                                    {
                                        if (c3.Name == "cbc:TaxableAmount")
                                        {
                                            string sVrSubTotal = c3.InnerText;
                                            txtSubTotal.Value = sVrSubTotal;
                                        }
                                        if (c3.Name == "cbc:TaxAmount")
                                        {
                                            Decimal sVrIva = Convert.ToDecimal(c3.InnerText.Replace(".", ","));

                                            txtVrIVA.Value = sVrIva.ToString("C"); ;
                                        }
                                        if (c3.Name == "cac:TaxCategory")
                                        {
                                            foreach (XmlNode c4 in c3.ChildNodes)
                                            {
                                                if (c4.Name == "cbc:Percent")
                                                {
                                                    string sPorIva = c4.InnerText;
                                                    txtIVA.Value = sPorIva;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (c1.Name == "cac:LegalMonetaryTotal")
                        {
                            foreach (XmlNode c5 in c1.ChildNodes)
                            {
                                if (c5.Name == "cbc:LineExtensionAmount")
                                {
                                    Decimal sVrSubTotal = Convert.ToDecimal(c5.InnerText.Replace(".", ","));

                                    txtSubTotal.Value = sVrSubTotal.ToString("C");
                                }
                                if (c5.Name == "cbc:TaxInclusiveAmount")
                                {

                                    Decimal Vrtotal = Convert.ToDecimal(c5.InnerText.Replace(".", ","));
                                    // string sVrTotal = c5.InnerText;
                                    txtVrFactura.Value = Vrtotal.ToString("C");
                                }
                            }
                        }
                        if (c1.Name == "cac:InvoiceLine")
                        {
                            foreach (XmlNode n2 in c1.ChildNodes)
                            {
                                if (n2.Name == "cac:Item")
                                {
                                    foreach (XmlNode n3 in n2.ChildNodes)
                                    {
                                        if (n3.Name == "cbc:Description")
                                        {
                                            sDescription = n3.InnerText;
                                        }
                                    }
                                }
                                if (n2.Name == "cac:Price")
                                {
                                    foreach (XmlNode n4 in n2.ChildNodes)
                                    {
                                        if (n4.Name == "cbc:PriceAmount")
                                        {
                                            Decimal nVrUnitario = Convert.ToDecimal(n4.InnerText.Replace(".", ","));
                                            sVrUnitario = nVrUnitario.ToString(); //n4.InnerText.Replace(".", ","); 

                                        }
                                        if (n4.Name == "cbc:BaseQuantity")
                                        {
                                            sCant = n4.InnerText.Replace(".", ",");
                                        }
                                    }
                                }
                            }
                        
                            dtDetalles.Rows.Add(sDescription, sCant, sVrUnitario);
                        }


                    }

                   
                    
                }
                string[,] arrDetalles = new string[30, 3];

                for (int i = 0; i < dtDetalles.Rows.Count; i++)
                {
                    //- Guardar la Columna Nombre el el Arreglo
                    arrDetalles[i, 0] = dtDetalles.Rows[i]["Item"].ToString();
                    arrDetalles[i, 1] = Convert.ToDecimal(dtDetalles.Rows[i]["Cantidad"]).ToString();
                    arrDetalles[i, 2] = Convert.ToDouble(dtDetalles.Rows[i]["VrUnitario"]).ToString();
                    filas ++;
                }


                DeleteTable(txtFactura.Value, txtNit.Value);

                InsertTableSql(txtFactura.Value, txtNit.Value, arrDetalles);

             //   ViewState["dtDetalles"] = dtDetalles;
                this.BindGrid();
            }
            catch (Exception error)
            {
                
                Response.Write("<script language=javascript>alert('" + error.Message + "');</script>");
                //throw;
            }

            
                
        }
        protected void BindGrid()
        {
            string conString = ConfigurationManager.ConnectionStrings["cnCodiesel"].ConnectionString;
            string sSql = "SELECT * FROM DETALLESFETmp";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();
                SqlDataAdapter DaFactura = new SqlDataAdapter(sSql, con);
                DataTable detFac = new DataTable();
                DaFactura.Fill(detFac);
                GVDetalles.DataSource = detFac;
                GVDetalles.DataBind();
                con.Close();
            }
            catch (Exception error)
            {
                Response.Write("<script language=javascript>alert('" + error.Message + "');</script>");
            }
        }



        public void AlertaXML()
        {
            string script = string.Format(@"<script type='text/javascript'> alert('Seleccione Archivo XML Factura Electrónica'); </ script >");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
        }

       
       
        protected void OnUpdate(object sender, EventArgs e)
        { 
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            string CodProducto = (row.Cells[0].Controls[0] as Label).Text;
            




            DataTable dt = ViewState["dt"] as DataTable;
            dt.Rows[row.RowIndex]["lblCodProducto"] = CodProducto;
            ViewState["dt"] = dt;
            GVDetalles.EditIndex = -1;
            this.BindGrid();
        }

      
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList lstProductos = (e.Row.FindControl("lstProductos") as DropDownList);
                lstProductos.DataSource = GetData("SELECT CODIGO, DESCRIPCIO FROM MTMERCIA WHERE HABILITADO=1 AND TIPOINV <>'PT'");
                lstProductos.DataTextField = "DESCRIPCIO";
                lstProductos.DataValueField = "CODIGO";
                lstProductos.DataBind();

                //Add Default Item in the DropDownList
                lstProductos.Items.Insert(0, new ListItem("Seleccione Producto"));

                //Select the Country of Customer in DropDownList
                string producto = (e.Row.FindControl("lblCodProducto") as Label).Text;
              //  lstProductos.Items.FindByValue(producto).Selected = true;
              
            }
        }
        private DataSet GetData(string query)
        {
            string conString = ConfigurationManager.ConnectionStrings["cnCodiesel"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        return ds;
                    }
                }
            }
        }

       
        protected void GVDetalles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVDetalles.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void GVDetalles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string Conex = ConfigurationManager.ConnectionStrings["cnCodiesel"].ConnectionString;
            Label Id = GVDetalles.Rows[e.RowIndex].FindControl("lblId") as Label;
            string cod = ((DropDownList)GVDetalles.Rows[e.RowIndex].FindControl("lstProductos")).SelectedValue;
            try
            {
                using (SqlConnection con = new SqlConnection(Conex))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE DetallesFETmp Set Codigo ='" + cod + "' where ID=" + Convert.ToInt32(Id.Text),con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        GVDetalles.EditIndex = -1;
                        this.BindGrid();
                    }
                }
            }
            catch (Exception error)
            {
                Response.Write("<script language=javascript>alert('" + error.Message + "');</script>");

            }
            //dt.Rows[e.RowIndex]["Codigo"] = cod;
            //ViewState["dt"] = GVDetalles.DataSource as DataTable;
            //((Label)GVDetalles.Rows[e.RowIndex].FindControl("lblCodProducto")).Text = cod;
//              ViewState["dt"] = dt;
            GVDetalles.EditIndex = -1;
            this.BindGrid();
        }

        public void DeleteTable(string Fac, string Nit)
        {
            string Conex = ConfigurationManager.ConnectionStrings["cnCodiesel"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(Conex))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM DetallesFETmp WHERE Nrodcto ='" + Fac + "' And Nit= '" + Nit + "'", con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        
                    }
                }
            }
            catch (Exception error)
            {
                Response.Write("<script language=javascript>alert('" + error.Message + "');</script>");

            }
        }
    

        public void InsertTableSql(string Fac, string Nit, string[,] arraydet)
        {
            int iFil = 0;
            string Conex = ConfigurationManager.ConnectionStrings["cnCodiesel"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(Conex))
                {
                    using (SqlCommand cmd = new SqlCommand("CT_InsertDetallesFEtmp"))
                    {
                        cmd.Parameters.Add("@Nit", SqlDbType.NVarChar, 15);
                        cmd.Parameters.Add("@NroDcto", SqlDbType.NVarChar, 10);
                        cmd.Parameters.Add("@Item", SqlDbType.NVarChar, 100);
                        cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal);
                        cmd.Parameters.Add("@VrUnitario", SqlDbType.Money);
                        cmd.Parameters.Add("@Rpta", SqlDbType.NVarChar, 2000);
                        cmd.Parameters["@Rpta"].Direction = ParameterDirection.Output;
                        while (iFil < filas)
                        {
                            if(arraydet[iFil,0] == null)
                            {
                                return;
                            }
                            if(con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = con;
                            
                            cmd.Parameters["@Nit"].Value= Nit;
                            
                            cmd.Parameters["@NroDcto"].Value= Fac;
                            
                            cmd.Parameters["@Item"].Value = arraydet[iFil, 0];
                            
                            cmd.Parameters["@Cantidad"].Value = arraydet[iFil, 1];
                            
                            cmd.Parameters["@VrUnitario"].Value = arraydet[iFil, 2];
                            
                            cmd.ExecuteNonQuery();
                            iFil++;
                        }                                            
                    }
                }
            }
            catch (Exception error)
            {
                Response.Write("<script language=javascript>alert('" + error.Message + "');</script>");
                
            }
        }

    }
    
}

