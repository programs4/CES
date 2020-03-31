<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderTab.ascx.cs" Inherits="Tools_UserControls_HeaderTab" %>
<ul class="myHeaderTab">

    <li>
        <asp:HyperLink ID="HrpTab1" runat="server" NavigateUrl="/tools/sib-r/?i={0}">
            SIB-R <span class="badge">{0}</span>
        </asp:HyperLink>
    </li>

    <li>
        <asp:HyperLink ID="HrpTab2" runat="server" NavigateUrl="/tools/evaluations/?i={0}">
            Daxili qiymətləndirmə <span class="badge">{0}</span>
        </asp:HyperLink>
    </li>

    <li>
        <asp:HyperLink ID="HrpTab3" runat="server" NavigateUrl="/tools/evaluationsskill/?i={0}">
            Daxili qiymətləndirmə 2 <span class="badge">{0}</span>
        </asp:HyperLink>
    </li>

</ul>
