<%@ Import Namespace="System.Data.SqlClient" %> 

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Trinity.Master" CodeBehind="authorize.aspx.vb" Inherits="TrinityViewer.WebForm1" %>

<asp:Content runat="server" ContentPlaceHolderID="Main" ID="cntAuthorize" style="font-family:Arial; font-size: smaller">
<script runat="server">
    Public BudgetCTC as decimal
    Public StartDate as Date
    Public EndDate as Date
    Public Product As String
    
    Protected overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ID As String = Request.QueryString("id")
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim rd As SqlDataReader
      
        conn = New SqlConnection("server=STO-APP60;database=extranet;uid=johanh;pwd=turbo")
        conn.Open()
        cmd = New SqlCommand("SELECT * FROM campaign WHERE id='" & ID & "'", conn)
        rd = cmd.ExecuteReader
        If rd.Read() Then
            BudgetCTC = rd!ctc
            StartDate = rd!startdate
            EndDate = rd!enddate
            Product = rd!product
        End If
        
    End Sub
    
    Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Windows.Forms.MessageBox.Show("Yes")
    End Sub
    
    Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Windows.Forms.MessageBox.Show("No")
    End Sub
</script>

    <div>
        By clicking yes you authorize Mediaedge:cia to book bla bla bla, disclaimer text...<br />
        <br />
        Are you sure you want to authorize <b><% Response.Write(Format(BudgetCTC, "C0")) %></b> to be booked for <% response.write(Product) %> in the period <% response.Write(format(startdate,"Short date")) %> - <% Response.Write(Format(EndDate, "Short date"))%>?
        <br />
    </div>
    <div>
        <br />
        <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="btnYes_Click" />
        <asp:Button ID="btnNo" runat="server" Text="No" OnClick="btnNo_Click" />
    </div>
</asp:Content>