<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Operations.ascx.cs" Inherits="Tools_Controls_Operations" %>

<div class="row operations">
    <div class="col-md-4 text-center">
        <asp:HyperLink ID="HypEvaluations" runat="server">
            <img src="/images/evaluations.png" />
            <br /> <br />
            <span>Qiymətləndirmə</span>
        </asp:HyperLink>
    </div>
    <div class="col-md-4 text-center">
        <asp:HyperLink ID="HypCase" runat="server">
            <img src="/images/case_on.png" />       
             <br /> <br />
            <span>CASE</span>
        </asp:HyperLink>
    </div>
    <div class="col-md-4 text-center">
        <asp:HyperLink ID="HypServices" runat="server">
            <img src="/images/services.png" />
            <br /> <br />
            <span>Xidmət</span>
        </asp:HyperLink>
    </div>
</div>
