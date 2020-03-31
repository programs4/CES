<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_EvaluationsSkill_Add_Default" %>

<%@ Register Src="~/Tools/UserControls/HeaderInfo.ascx" TagPrefix="uc1" TagName="HeaderInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/evaluationsskilltable/perfect-scrollbar.css" />
    <link rel="stylesheet" type="text/css" href="/css/evaluationsskilltable/main.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <uc1:HeaderInfo runat="server" ID="HeaderInfo" />
    <div>
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <asp:Repeater ID="RptTab" runat="server">
                <ItemTemplate>
                    <li role="presentation" class='<%# Container.ItemIndex==0?"active":"" %>'><a href='#<%#Eval("ID") %>' aria-controls='<%#Eval("ID") %>' role="tab" data-toggle="tab"><%#Eval("Name")%></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>

        <!-- Tab panes -->
        <div class="tab-content">
            <asp:Repeater ID="RptContent" runat="server" OnItemDataBound="RptContent_ItemDataBound">
                <ItemTemplate>
                    <asp:HiddenField ID="HdnEvaluationsSkillGroupsID" Value='<%#Eval("ID") %>' runat="server" />
                    <div role="tabpanel" class='tab-pane <%# Container.ItemIndex==0?"active":"" %>' id='<%#Eval("ID")%>'>
                        <br />
                        <br />
                        <div class="wrap-table100">
                            <div class="table100 ver1">
                                <div class="table100-firstcol">
                                    <table>
                                        <thead>
                                            <tr class="row100 head">
                                                <th class="cell100 column1">Suallar (<%#Eval("Name")%>)</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="RptQuestions" runat="server">
                                                <ItemTemplate>
                                                    <tr class="row100 body">
                                                        <td class="cell100 column1"><%#Eval("EvaluationsSkillQuestionsName")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </tbody>
                                    </table>
                                </div>


                                <div class="wrap-table100-nextcols js-pscroll">
                                    <div class="table100-nextcols">
                                        <table>
                                            <thead>
                                                <tr class="row100 head">
                                                    <asp:Repeater ID="RptHeader" runat="server">
                                                        <ItemTemplate>
                                                            <th class="cell100 column2">
                                                                <asp:Literal ID="LtrDate" Text='<%#Eval("DisplayName")%>' runat="server"></asp:Literal>
                                                            </th>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RptBody" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="row100 body">
                                                            <%#Eval("Body")%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <%--   <div role="tabpanel" class="tab-pane active" id="home">
                <br />
                <br />
                <div class="wrap-table100">
                    <div class="table100 ver1">

                        <div class="table100-firstcol">
                            <table>
                                <thead>
                                    <tr class="row100 head">
                                        <th class="cell100 column1">Suallar</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <asp:Repeater ID="RptQuestions" runat="server">
                                        <ItemTemplate>
                                            <tr class="row100 body">
                                                <td class="cell100 column1"><%#Eval("Name")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                            </table>
                        </div>

                        <div class="wrap-table100-nextcols js-pscroll">
                            <div class="table100-nextcols">
                                <table>
                                    <thead>
                                        <tr class="row100 head">
                                            <asp:Repeater ID="RptHeader" runat="server">
                                                <ItemTemplate>
                                                    <th class="cell100 column2">
                                                        <asp:Literal ID="LtrDate" Text='<%#Eval("Date")%>' runat="server"></asp:Literal>
                                                    </th>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RptBody" runat="server">
                                            <ItemTemplate>
                                                <tr class="row100 body">
                                                    <%#Eval("Body")%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                    </div>
                </div>

            </div>--%>
        </div>

    </div>

    <script src="/js/evaluationsskilltable/perfect-scrollbar.min.js"></script>
    <script>
        function selectValue(evaluationsSkillID, evaluationsSkillQuestionsID, evaluationsSkillPointsID) {

            $.ajax({

                type: 'POST',
                url: 'Default.aspx/UpdateEvaluationsSkillValues',
                data: '{ "evaluationsSkillID": ' + evaluationsSkillID + ',"evaluationsSkillQuestionsID": ' + evaluationsSkillQuestionsID + ',"evaluationsSkillPointsID": ' + evaluationsSkillPointsID + ' }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',

                success: function (result) {
                    //alert(result.d);                   
                },

                error: function () {
                    alert('Sistemdə yüklənmə var. Daha sonra cəhd edin.');
                }

            });

            //for (var i = 1; i <= 4; i++) {
            //    var img = $(".imgPoints[rowIndex='" + evaluationsSkillQuestionsID + "_" + i + "']");
            //    img.attr("src", img.attr("src").replace("_on", "_off"));
            //}
            //var curr_img = $(event.target);
            //curr_img.attr("src", curr_img.attr("src").replace("_off", "_on"));

            //var hdnPoints = document.getElementById("ContentPlaceHolderBody_RptBody_HdnPoints_" + index + "");
            //hdnPoints.value = points;
            // alert(document.getElementById("ContentPlaceHolderBody_RptBody_HdnPoints_" + 0 + "").value);
        }

        $('i').on('click', function () {
            $(this).addClass('active').siblings().removeClass('active');
        });


        $('.js-pscroll').each(function () {
            var ps = new PerfectScrollbar(this);

            $(window).on('resize', function () {
                ps.update();
            })

            $(this).on('ps-x-reach-start', function () {
                $(this).parent().find('.table100-firstcol').removeClass('shadow-table100-firstcol');
            });

            $(this).on('ps-scroll-x', function () {
                $(this).parent().find('.table100-firstcol').addClass('shadow-table100-firstcol');
            });

        });


    </script>
    <script src="/js/main.js"></script>
</asp:Content>


