﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDashboard.aspx.cs" Inherits="POSTOFFICE3.EmployeeDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Employee Dashboard</h1>
            <a href="UpdatePackage.aspx">Update a Package Location/Status</a><br />
            <a href="FacilityTraffic.aspx">Facility Report</a><br />
            <a href="Tracking.aspx">Track a package</a>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Logout" OnClick="Button1_Click" style="height: 26px" />
            <br />
        </div>
    </form>
</body>
</html>
