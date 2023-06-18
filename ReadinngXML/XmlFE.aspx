<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XmlFE.aspx.cs" Inherits="ReadinngXML.XmlFE" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
  <%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css"/>
  <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.1/dist/jquery.slim.min.js"></script>
  <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>
  --%>  
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="Content/Site.css" rel="stylesheet" />--%>
    <link href="Content/style.css" rel="stylesheet" />
    <link href="Content/sweetalert2.css" rel="stylesheet" />
    <script src="Scripts/sweetalert2.js"></script>

    
    <script>
function toFinalNumberFormat(controlToCheck) {
            var enteredNumber = '' + controlToCheck.value;
            enteredNumber = enteredNumber.replace(/[^0-9\.]+/g, ''); 
            controlToCheck.value = Number(enteredNumber).toLocaleString('en-US', { style: 'currency', currency: 'USD' }); 
        }
    </script>

</head>
<body>
    <form runat="server">
    <div class="container mt-3">
         <h2>CAUSACION FACTURAS ELECTRONICAS</h2> 
        <div class="container p-3 my-2 border">
            <div class="row">
                <div class="col-md-6">
                    <h4 for="leerxml" class="form-control-plaintext">Archivo XML</h4>
                </div>
            </div>
            <div class="row">               
               <div class="col-md-6">
                    <asp:FileUpload ID="Filexml" accept="text/xml"  runat="server" class="form-control" />
               </div>
               <div class="col-md-3">
                    <asp:Button  runat="server" type="button" class="btn btn-primary" ID="btnLeer" Text="Leer XML FEv2" OnClick="btnLeer_Click" />  
               </div>
            </div>
            <div class="row">
               <div class ="col-md-12">
                    <asp:Label class="help-block" ID="lblRuta" runat="server" Text="...."></asp:Label>
               </div>
            </div>
        </div>
         <!--Datos Factura-->
        <div class="container p-3 my-3 border">
        <div class="row">
                <div class="col-md-6">
                    <h4 for="leerxml" class="form-control-plaintext">Datos Factura</h4>
                </div>
            </div>
        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <label for="txtNit">NIT:</label>
                    <input runat="server" disabled="disabled" type="text" class="form-control" id="txtNit"/>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtRazon">RAZÓN SOCIAL:</label>
                    <input runat="server" type="text" disabled="disabled" class="form-control" id="txtRazon"/>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="txtFactura">N° Dcto:</label>
                    <input runat="server" type="text" class="form-control" id="txtFactura"/>
                </div>
            </div>
            <%--<div class="col-md-2">
                <div class="form-group">
                    <label for="txtTipoDcto">Tipo Dcto:</label>
                    <input runat="server" type="text" class="form-control" id="txtTipoDcto"/>
                </div>
            </div>--%>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="txtFecha">FECHA:</label>
                    <input runat="server" type="text" class="form-control" id="txtFecha"/>
                </div>
            </div>
        </div>

        <br />
        
        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <label for="txtSubTotal">SubTotal:</label>
                    <input runat="server" type="text" disabled="disabled" class="form-control" id="txtSubTotal"/>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="txtIVA">IVA:</label>
                    <input runat="server" type="text" disabled="disabled" class="form-control" id="txtIVA"/>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="txtVrIVA">VR. IVA:</label>
                    <input runat="server" type="text" disabled="disabled" class="form-control" id="txtVrIVA"/>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="pwd">TOTAL FACTURA:</label>
                    <input runat="server" type="text" disabled="disabled" class="form-control" id="txtVrFactura"/>
                </div>
            </div>
        </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    <label for="txtVrIVA">RETEFUENTE:</label>
                    <asp:DropDownList class="form-select" ID="LstRetefuente" runat="server" DataSourceID="DSRetenciones" DataTextField="DESCCRIPCIO" DataValueField="CODRETE"></asp:DropDownList>
                    <asp:SqlDataSource ID="DSRetenciones" runat="server" ConnectionString="<%$ ConnectionStrings:cnCoordinadora %>" SelectCommand="SELECT '' AS CODRETE, '' AS DESCCRIPCIO UNION
                                    SELECT CODRETE, DESCRIPCIO FROM MTTOPRTE WHERE CODRETE NOT IN('OD')"></asp:SqlDataSource>
                </div>
                <div class="col-md-4">
                    <label for="txtVrIVA">RETICA:</label>
                    <asp:DropDownList class="form-select" ID="LstReteICA" runat="server" DataSourceID="DSRetica" DataTextField="DESCRIPCION" DataValueField="CODRETICA"></asp:DropDownList>
                    <asp:SqlDataSource ID="DSRetica" runat="server" ConnectionString="<%$ ConnectionStrings:cnCoordinadora %>" SelectCommand="SELECT '' AS CODRETICA, '' AS DESCRIPCION UNION
                                        SELECT CODRETICA, DESCRIPCIO FROM MTRETICA"></asp:SqlDataSource>
                </div>
                <div class="col-md-3">
                    <asp:Button type="button" class="btn btn-danger" runat="server" ID="BtnFacturar" Text="Facturar" />

                </div>
              </div>

            
            
                
            </div>
         
  </div>
        <div class="d-flex justify-content-center">
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="GVDetalles" runat="server" BackColor="White"  AutoGenerateColumns="false" BorderColor="#E7E7FF" BorderStyle="None" 
                        BorderWidth="1px" CellPadding="3" GridLines="Horizontal"    OnRowEditing="GVDetalles_RowEditing" OnRowUpdating="GVDetalles_RowUpdating">
                    <AlternatingRowStyle BackColor="#F7F7F7"  />
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center"/>
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="right" />
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                              <Columns>
                                  <asp:TemplateField>
                                      <ItemTemplate>
                                          <asp:Label runaT="server" ID="lblId" Text='<%#Bind("Id") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  

                                  <asp:TemplateField HeaderText="CODIGO">
                                      <ItemTemplate>
                                          <asp:Label runaT="server" ID="lblCodProducto" Text='<%#Bind("Codigo") %>'></asp:Label>
                                      </ItemTemplate>
                                      <EditItemTemplate>
                                          <asp:DropDownList runat="server" ID="lstProductos" DataSourceID="DsProductos" 
                                              DataTextField="Descripcio" DataValueField="Codigo" SelectedValue='<%# Bind("Codigo") %>'></asp:DropDownList>
                                      </EditItemTemplate>
                                      
                                  </asp:TemplateField>
                                  <asp:TemplateField>
                                      <ItemTemplate>
                                          <asp:Label runat="server" ID="lblDescripcion" Text='<%# Bind("DESCRIPCION") %>' ItemStyle-Width="200"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField>
                                      <ItemTemplate>
                                          <asp:Label runat="server" ID="lblCantidad" Text='<%# Bind("CANTIDAD") %>' ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center"  ></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField><asp:TemplateField>
                                      <ItemTemplate>
                                          <asp:Label runat="server" ID="lblVrUnitario" Text='<%# Bind("VRUNITARIO") %>' ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                   
                                                                                <asp:CommandField ShowEditButton="true" />
                                  <%--<asp:BoundField DataField="ITEM" HeaderText="Descripcion" ItemStyle-Width="200" />
                                  <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" />
                                  <asp:BoundField DataField="VrUnitario" HeaderText="Vr Unitario" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" />--%>
                                 <%-- <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton Text="Edit" runat="server" CommandName="Edit" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton Text="Update" runat="server"/>
                                            <asp:LinkButton Text="Cancel" runat="server"  />
                                        </EditItemTemplate>
                                   </asp:TemplateField>--%>
                              </Columns>
                </asp:GridView> 
            </div>
                 <asp:SqlDataSource ID="DsProductos" runat="server" ConnectionString="<%$ ConnectionStrings:cnCoordinadora %>" 
                     SelectCommand="SELECT '' AS CODIGO, '' AS DESCRIPCIO UNION SELECT CODIGO, DESCRIPCIO FROM MTMERCIA WHERE HABILITADO=1 AND TIPOINV <>'PT'"></asp:SqlDataSource>
        </div>
     </div>

                            
        
    </form>
   
</body>
<script src="js/bootstrap.min.js"></script>
</html>
