<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_SIB_R_Result_Default" %>

<%@ Register Src="~/Tools/UserControls/HeaderInfo.ascx" TagPrefix="uc1" TagName="HeaderInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">

    <%--    <script src="/js/canvasjs.min.js"></script>

    <script type="text/javascript">
        window.onload = function () {

            var chartMS = new CanvasJS.Chart("chartContainerMS", {
                title: {
                    text: "Motor Skills",
                    fontSize: 30
                },
                animationEnabled: true,
                axisX: {
                    gridColor: "Silver",
                    tickColor: "silver",
                    valueFormatString: "DD/MMM"
                },
                toolTip: {
                    shared: true
                },
                theme: "theme2",
                axisY: {
                    gridColor: "Silver",
                    tickColor: "silver"
                },
                legend: {
                    verticalAlign: "center",
                    horizontalAlign: "right"
                },

                data: [
                    {
                        type: "line",
                        showInLegend: true,
                        lineThickness: 2,
                        name: "Motor Skills",
                        color: "#F08080",
                        dataPoints: [
                            { x: new Date(2010, 1, 3), y: -47 },
                            { x: new Date(2010, 4, 4), y: -30 },
                            { x: new Date(2010, 7, 7), y: -6 },
                            { x: new Date(2010, 10, 10), y: 6 }
                        ]
                    }
                ],
                legend: {
                    cursor: "pointer",
                    itemclick: function (e) {
                        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                            e.dataSeries.visible = false;
                        }
                        else {
                            e.dataSeries.visible = true;
                        }
                        chartMS.render();
                    }
                }
            });
            var chartSC = new CanvasJS.Chart("chartContainerSC", {
                title: {
                    text: "Social Interaction & Communication Skills",
                    fontSize: 30
                },
                animationEnabled: true,
                axisX: {
                    gridColor: "Silver",
                    tickColor: "silver",
                    valueFormatString: "DD/MMM"
                },
                toolTip: {
                    shared: true
                },
                theme: "theme2",
                axisY: {
                    gridColor: "Silver",
                    tickColor: "silver"
                },
                legend: {
                    verticalAlign: "center",
                    horizontalAlign: "right"
                },

                data: [


                    {
                        type: "line",
                        showInLegend: true,
                        name: "Social Interaction & Communication",
                        color: "#20B2AA",
                        lineThickness: 2,

                        dataPoints: [
                            { x: new Date(2010, 1, 1), y: -30 },
                            { x: new Date(2010, 4, 4), y: -20 },
                            { x: new Date(2010, 7, 7), y: -5 },
                            { x: new Date(2010, 10, 10), y: 8 }
                        ]
                    }


                ],
                legend: {
                    cursor: "pointer",
                    itemclick: function (e) {
                        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                            e.dataSeries.visible = false;
                        }
                        else {
                            e.dataSeries.visible = true;
                        }
                        chartSC.render();
                    }
                }
            });
            var chartPL = new CanvasJS.Chart("chartContainerPL", {
                title: {
                    text: "Personal Living Skills",
                    fontSize: 30
                },
                animationEnabled: true,
                axisX: {
                    gridColor: "Silver",
                    tickColor: "silver",
                    valueFormatString: "DD/MMM"
                },
                toolTip: {
                    shared: true
                },
                theme: "theme2",
                axisY: {
                    gridColor: "Silver",
                    tickColor: "silver"
                },
                legend: {
                    verticalAlign: "center",
                    horizontalAlign: "right"
                },

                data: [

                    {
                        type: "line",
                        showInLegend: true,
                        name: "Personal Living",
                        color: "#52CC9F",
                        lineThickness: 2,

                        dataPoints: [
                            { x: new Date(2010, 1, 1), y: -40 },
                            { x: new Date(2010, 4, 4), y: -35 },
                            { x: new Date(2010, 7, 7), y: -18 },
                            { x: new Date(2010, 10, 10), y: 0 }
                        ]
                    }


                ],
                legend: {
                    cursor: "pointer",
                    itemclick: function (e) {
                        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                            e.dataSeries.visible = false;
                        }
                        else {
                            e.dataSeries.visible = true;
                        }
                        chartPL.render();
                    }
                }
            });
            var chartCL = new CanvasJS.Chart("chartContainerCL", {
                title: {
                    text: "Community Living Skills",
                    fontSize: 30
                },
                animationEnabled: true,
                axisX: {
                    gridColor: "Silver",
                    tickColor: "silver",
                    valueFormatString: "DD/MMM"
                },
                toolTip: {
                    shared: true
                },
                theme: "theme2",
                axisY: {
                    gridColor: "Silver",
                    tickColor: "silver"
                },
                legend: {
                    verticalAlign: "center",
                    horizontalAlign: "right"
                },

                data: [

                    {
                        type: "line",
                        showInLegend: true,
                        name: "Community Living",
                        color: "#6D77AB",
                        lineThickness: 2,

                        dataPoints: [
                            { x: new Date(2010, 1, 1), y: -25 },
                            { x: new Date(2010, 4, 4), y: -10 },
                            { x: new Date(2010, 7, 7), y: 0 },
                            { x: new Date(2010, 10, 10), y: 12 }
                        ]
                    }

                ],
                legend: {
                    cursor: "pointer",
                    itemclick: function (e) {
                        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                            e.dataSeries.visible = false;
                        }
                        else {
                            e.dataSeries.visible = true;
                        }
                        chartCL.render();
                    }
                }
            });
            var chartBI = new CanvasJS.Chart("chartContainerBI", {
                title: {
                    text: "Broad Independence (Full Scale)",
                    fontSize: 30
                },
                animationEnabled: true,
                axisX: {
                    gridColor: "Silver",
                    tickColor: "silver",
                    valueFormatString: "DD/MMM"
                },
                toolTip: {
                    shared: true
                },
                theme: "theme2",
                axisY: {
                    gridColor: "Silver",
                    tickColor: "silver"
                },
                legend: {
                    verticalAlign: "center",
                    horizontalAlign: "right"
                },

                data: [

                    {
                        type: "line",
                        showInLegend: true,
                        name: "Broad Independence",
                        color: "#C8D35C",
                        lineThickness: 2,

                        dataPoints: [
                            { x: new Date(2010, 1, 1), y: -15 },
                            { x: new Date(2010, 4, 4), y: -3 },
                            { x: new Date(2010, 7, 7), y: 2 },
                            { x: new Date(2010, 10, 10), y: 14 }
                        ]
                    }

                ],
                legend: {
                    cursor: "pointer",
                    itemclick: function (e) {
                        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                            e.dataSeries.visible = false;
                        }
                        else {
                            e.dataSeries.visible = true;
                        }
                        chartBI.render();
                    }
                }
            });
            var chart = new CanvasJS.Chart("chartContainer", {
                title: {
                    text: "FULL",
                    fontSize: 30
                },
                animationEnabled: true,
                axisX: {
                    gridColor: "Silver",
                    tickColor: "silver",
                    valueFormatString: "DD/MMM"
                },
                toolTip: {
                    shared: true
                },
                theme: "theme2",
                axisY: {
                    gridColor: "Silver",
                    tickColor: "silver"
                },
                legend: {
                    verticalAlign: "center",
                    horizontalAlign: "right"
                },
                data: [
                    {
                        type: "line",
                        showInLegend: true,
                        lineThickness: 2,
                        name: "Motor Skills",
                        color: "#F08080",
                        dataPoints: [
                            { x: new Date(2010, 1, 3), y: -47 },
                            { x: new Date(2010, 4, 4), y: -30 },
                            { x: new Date(2010, 7, 7), y: -6 },
                            { x: new Date(2010, 10, 10), y: 6 }
                        ]
                    },

                    {
                        type: "line",
                        showInLegend: true,
                        name: "Social Interaction & Communication",
                        color: "#20B2AA",
                        lineThickness: 2,

                        dataPoints: [
                            { x: new Date(2010, 1, 1), y: -30 },
                            { x: new Date(2010, 4, 4), y: -20 },
                            { x: new Date(2010, 7, 7), y: -5 },
                            { x: new Date(2010, 10, 10), y: 8 }
                        ]
                    },

                    {
                        type: "line",
                        showInLegend: true,
                        name: "Personal Living",
                        color: "#52CC9F",
                        lineThickness: 2,

                        dataPoints: [
                            { x: new Date(2010, 1, 1), y: -40 },
                            { x: new Date(2010, 4, 4), y: -35 },
                            { x: new Date(2010, 7, 7), y: -18 },
                            { x: new Date(2010, 10, 10), y: 0 }
                        ]
                    },

                    {
                        type: "line",
                        showInLegend: true,
                        name: "Community Living",
                        color: "#6D77AB",
                        lineThickness: 2,

                        dataPoints: [
                            { x: new Date(2010, 1, 1), y: -25 },
                            { x: new Date(2010, 4, 4), y: -10 },
                            { x: new Date(2010, 7, 7), y: 0 },
                            { x: new Date(2010, 10, 10), y: 12 }
                        ]
                    },

                    {
                        type: "line",
                        showInLegend: true,
                        name: "Broad Independence",
                        color: "#C8D35C",
                        lineThickness: 2,

                        dataPoints: [
                            { x: new Date(2010, 1, 1), y: -15 },
                            { x: new Date(2010, 4, 4), y: -3 },
                            { x: new Date(2010, 7, 7), y: 2 },
                            { x: new Date(2010, 10, 10), y: 14 }
                        ]
                    }

                ],
                legend: {
                    cursor: "pointer",
                    itemclick: function (e) {
                        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                            e.dataSeries.visible = false;
                        }
                        else {
                            e.dataSeries.visible = true;
                        }
                        chart.render();
                    }
                }
            });

            chartMS.render();
            chartSC.render();
            chartPL.render();
            chartCL.render();
            chartBI.render();
            chart.render();
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <uc1:HeaderInfo runat="server" ID="HeaderInfo" />

    <ul id="myTab" class="nav nav-pills adaptive" role="tablist" style="background-color: #F9F9F9; padding: 5px; border: 1px solid #E4E4E4;">
        <li role="presentation" class="active">
            <a href="#adaptive" id="adaptive-tab" role="tab" data-toggle="tab" aria-controls="adaptive" aria-expanded="false">
                <span class="glyphicon glyphicon-folder-open" runat="server"></span>
                &nbsp;&nbsp;Adaptiv davranış
            </a>
        </li>

        <li role="presentation" class="">
            <a href="#adaptivesubscales" role="tab" id="adaptivesubscales-tab" data-toggle="tab" aria-controls="adaptivesubscales" aria-expanded="true">
                <span class="glyphicon glyphicon-folder-open"></span>&nbsp;&nbsp;Adaptiv alt göstəricilər
            </a>
        </li>

        <li role="presentation" class="">
            <a href="#adaptiveclusters" role="tab" id="adaptiveclusters-tab" data-toggle="tab" aria-controls="adaptiveclusters" aria-expanded="true">
                <span class="glyphicon glyphicon-folder-open"></span>&nbsp;&nbsp; Adaptiv davranış qruplar
            </a>
        </li>

        <li role="presentation" class="">
            <a href="#maladaptive" role="tab" id="maladaptive-tab" data-toggle="tab" aria-controls="maladaptive" aria-expanded="true">
                <span class="glyphicon glyphicon-folder-open"></span>&nbsp;&nbsp; Qeyri-adaptiv davranış
            </a>
        </li>

        <li role="presentation" class="">
            <a href="#chart" role="tab" id="chart-tab" data-toggle="tab" aria-controls="chart" aria-expanded="true">
                <span class="glyphicon glyphicon-folder-open"></span>&nbsp;&nbsp;  Qrafik təsvir
            </a>
        </li>
    </ul>

    <div id="myTabContent" class="tab-content" style="padding: 10px 0px 10px 3px; margin-top: 5px">

        <div role="tabpanel" class="tab-pane fade active in" id="adaptive" aria-labelledby="adaptive-tab">
            <asp:Panel ID="PnlCluster" runat="server" class="row">
                <div class="col-md-12">
                    <div class="b-head-table" style="width: 312px">Klaster W (Q) Xallarının Hesablanması </div>
                    <div class="skills-table">
                        <div class="grid-table-head">
                            <asp:Repeater ID="RptSIBRScoringTypesGroups" runat="server">
                                <ItemTemplate>
                                    <div class="head-item">
                                        <%# Eval("Name")._ToString().Split(' ')[0] %><br />
                                        <%# Eval("Name")._ToString().Split(' ')[1] %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="clearFix"></div>
                        </div>

                        <asp:Repeater ID="RptSIBRAdaptiveScores" runat="server" OnItemDataBound="RptSIBRAdaptiveScores_ItemDataBound">
                            <ItemTemplate>
                                <asp:Literal ID="LtrStart" runat="server" Visible="false"><div class="grid-table-boxes"></asp:Literal>
                                <div class="grid-box-item">
                                    <div class="sib-box">
                                        <asp:HiddenField ID="HdnSIBRScoringTypesID" runat="server" Value='<%#Eval("SIBRScoringTypesID") %>' />
                                        <asp:HiddenField ID="HdnW" runat="server" Value='<%#Eval("W") %>' />
                                        <span class="top-value">W</span>
                                        <span class="left-value"><%# Eval("Alphabet") %></span>
                                        <div class="blue-box"><%# Eval("W") %></div>
                                    </div>
                                </div>
                                <asp:Literal ID="LtrEnd" runat="server" Visible="false"></div></asp:Literal>
                            </ItemTemplate>
                        </asp:Repeater>


                        <div class="grid-table-boxes">
                            <div class="grid-box-item">
                                <div class="sib-box">
                                    <span class="top-value">W</span>
                                    <span class="left-value">MS</span>
                                    <div class="blue-box">
                                        <asp:Literal ID="LtrBIMS" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="grid-box-item">
                                <div class="sib-box">
                                    <span class="top-value">W</span>
                                    <span class="left-value">SC</span>
                                    <div class="blue-box">
                                        <asp:Literal ID="LtrBISC" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="grid-box-item">
                                <div class="sib-box">
                                    <span class="top-value">W</span>
                                    <span class="left-value">PL</span>
                                    <div class="blue-box">
                                        <asp:Literal ID="LtrBIPL" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="grid-box-item">
                                <div class="sib-box">
                                    <span class="top-value">W</span>
                                    <span class="left-value">CL</span>
                                    <div class="blue-box">
                                        <asp:Literal ID="LtrBICL" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <table>
                            <tr>
                                <td colspan="5">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="sib-box">
                                        <span class="top-value">MS W</span>
                                        <div class="blue-box">
                                            <asp:Literal ID="LtrMS" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="sib-box">
                                        <span class="top-value">SC W</span>
                                        <div class="blue-box">
                                            <asp:Literal ID="LtrSC" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="sib-box">
                                        <span class="top-value">PL W</span>
                                        <div class="blue-box">
                                            <asp:Literal ID="LtrPL" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="sib-box">
                                        <span class="top-value">CL W</span>
                                        <div class="blue-box">
                                            <asp:Literal ID="LtrCL" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="sib-box">
                                        <span class="top-value">BI W</span>
                                        <div class="blue-box">
                                            <asp:Literal ID="LtrBI" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>
            </asp:Panel>

            <div class="row">
                <div class="col-md-12">
                    <div class="b-head-table" style="width: 475px">Klaster/Qrup RMI-lərin, SSl-lərin, və PR-lərin Hesablanması</div>
                    <div class="skills-table-detailed">
                        <table>
                            <asp:Repeater ID="RptSIBRAdaptiveResult" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="headWidth">
                                            <%# Eval("Name") %>
                                        </td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value"><%#Eval("ShortName") %></span>
                                                <div class="blue-box"><%#Eval("SumW") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value">REF W</span>
                                                <div class="blue-box"><%#Eval("REFW") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="sib-box type-1">
                                                <span class="top-value">SEM(SS)</span>
                                                <div class="value-box"><%#Eval("SEMSS") %></div>
                                                <div class="schema-hr"></div>
                                                <span class="bottom-value">F</span>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="sib-box type-2">
                                                <span class="top-value">Column</span>
                                                <div class="value-box left-box">
                                                    <span class="small-head">+ DIFF</span>
                                                    <%#Eval("COLUMNS_DIFF_P") %>
                                                </div>
                                                <div class="value-box right-box">
                                                    <span class="small-head">- DIFF</span>
                                                    <%#Eval("COLUMNS_DIFF_N") %>
                                                </div>
                                                <div class="clearFix"></div>
                                                <div class="schema-hr"></div>
                                                <span class="bottom-value">F</span>
                                            </div>
                                        </td>
                                        <td class="small-td">=</td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value">DIFF</span>
                                                <span class="bottom-value">+ OR -</span>
                                                <div class="blue-box"><%#Eval("DIFF") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value">RMI</span>
                                                <span class="bottom-value">G</span>
                                                <div class="blue-box"><%#Eval("RMI") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value">SS</span>
                                                <span class="bottom-value">G</span>
                                                <div class="blue-box"><%#Eval("SS")._ToInt32() %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="sib-box type-1">
                                                <span class="top-value">-1 SEM</span>
                                                <div class="value-box"><%#Eval("SS_SEM_N") %></div>
                                                <div class="schema-hr"></div>
                                            </div>
                                        </td>
                                        <td class="small-td">to</td>
                                        <td>
                                            <div class="sib-box type-1">
                                                <span class="top-value">+1 SEM</span>
                                                <div class="value-box"><%#Eval("SS_SEM_P") %></div>
                                                <div class="schema-hr"></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value">PR</span>
                                                <span class="bottom-value">G</span>
                                                <div class="blue-box"><%#Eval("PR") %></div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="sib-box type-1 type-1-long">
                                                <span class="top-value" style="width: 80px; text-transform: none">Skill Level</span>
                                                <div class="value-box" style="height: auto"><%# Eval("SkillWithAgeLevelTasks") %></div>
                                                <div class="schema-hr"></div>
                                                <span class="bottom-value">Appendix C</span>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div role="tabpanel" class="tab-pane fade" id="adaptivesubscales" aria-labelledby="adaptivesubscales-tab">
            <div class="row">
                <div class="col-md-12">
                    <div class="b-head-table" style="width: 520px">Adaptiv davranış bacarığı səviyyələrinin alt hesablarının hesablanması</div>
                    <div class="subscales-table">
                        <table>
                            <tr>
                                <th>Alt göstəricilər</th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th>Yaş səviyyəsində tapşırıqlara olan bacarıq</th>
                                <th>Yaş səviyyəsində tapşırıqlar olacaq</th>
                            </tr>
                            <asp:Repeater ID="RptSIBRAdaptiveSubscalesScores" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("SIBRScoringTypesName")%></td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value">W</span>
                                                <span class="bottom-value"><%#Eval("Alphabet")%></span>
                                                <div class="blue-box">
                                                    <span><%#Eval("W")%></span>
                                                </div>
                                            </div>
                                        </td>
                                        <td class="small-td"><span>-</span></td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value">REF W</span>
                                                <span class="bottom-value">J</span>
                                                <div class="blue-box">
                                                    <%#Eval("REFW")%>
                                                </div>
                                            </div>
                                        </td>
                                        <td class="small-td"><span>=</span></td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value">DIFF</span>
                                                <span class="bottom-value">+ OR -</span>
                                                <div class="blue-box">
                                                    <%#Eval("DIFF")%>
                                                </div>
                                            </div>
                                        </td>
                                        <td class="underline">
                                            <span><%#Eval("SkillWithAgeLevelTasks") %></span>
                                        </td>
                                        <td class="underline">
                                            <span><%#Eval("AgeLevelTasksWillBe") %></span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div role="tabpanel" class="tab-pane fade" id="adaptiveclusters" aria-labelledby="adaptiveclusters-tab">
            <div class="row">
                <div class="col-md-12">
                    <div class="b-head-table" style="width: 512px">Adaptiv davranış bacarığı səviyyələrinin klasterlərinin hesablanması</div>
                    <div class="subscales-table">
                        <table>
                            <tr>
                                <th>Klasterlər</th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th>Yaş səviyyəsində tapşırıqlara olan bacarıq</th>
                                <th>Yaş səviyyəsində tapşırıqlar olacaq</th>
                            </tr>
                            <asp:Repeater ID="RptSIBRAdaptiveClustersScores" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Name")%></td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value"><%#Eval("ShortName")%> W</span>
                                                <div class="blue-box">
                                                    <span><%#Eval("SumW")%></span>
                                                </div>
                                            </div>
                                        </td>
                                        <td class="small-td"><span>-</span></td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value">REF W</span>
                                                <span class="bottom-value">F</span>
                                                <div class="blue-box">
                                                    <%#Eval("REFW")%>
                                                </div>
                                            </div>
                                        </td>
                                        <td class="small-td"><span>=</span></td>
                                        <td>
                                            <div class="sib-box">
                                                <span class="top-value">DIFF</span>
                                                <span class="bottom-value">+ OR -</span>
                                                <div class="blue-box">
                                                    <%#Eval("DIFF")%>
                                                    <asp:HiddenField ID="HdnDIFF" runat="server" Value='<%#Eval("DIFF")%>' />
                                                </div>
                                            </div>
                                        </td>
                                        <td class="underline">
                                            <span><%#Eval("SkillWithAgeLevelTasks")%></span>
                                        </td>
                                        <td class="underline">
                                            <span><%#Eval("AgeLevelTasksWillBe")%></span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div role="tabpanel" class="tab-pane fade" id="maladaptive" aria-labelledby="maladaptive-tab">

            <div class="row" style="padding: 0px 20px">
                <div class="col-md-6 col-xs-6">
                    <div class="b-head-table" style="width: 310px">Qeyri-adaptiv Davranış İndeksi Profili</div>
                    <div class="instructions">
                        <b>Instructions</b>
                        <ol>
                            <li>Sütün a-da 20-21-ci səhifələrdən əldə edilmiş Qeyri-adaptiv Davranış İndekslərinin hər biri üçün xalları qeyd edin. Müvafiq qaydada “+” və ya “-“ işarələrini qeyd edin.</li>
                            <li>Sütun b-dəki SEM-i sütün a-dakı hər bir xaldan çıxın və
                                <br />
                                fərqi sütün c də qeyd edin.</li>
                            <li>Sütun b-dəki SEM miqdarını sütun a-dakı hər bir xala əlavə 
                                <br />
                                edin və alınmış cəmi sütun d-də qeyd edin.</li>
                            <li>Hər bir indeks üçün -1SEM və (c)-dən +1SEM (d) dəyərinədək 
                                <br />
                                olan sahənin aşağısında zolaq çəkin.</li>
                            <li>Sütun a-dakı GMI xalına uyğun gələn nöqtədə profil
                                <br />
                                boyunca şaquli xətt çətin.</li>
                        </ol>
                    </div>
                </div>
                <div class="col-md-6 col-xs-6">
                    <div class="indexes-table">
                        <table>
                            <tr>
                                <th></th>
                                <th></th>
                                <th>(a)<br />
                                    <br />
                                    <span style="font-size: 11px; font-weight: 100;">INDEX</span></th>
                                <th>(b)<br />
                                    <br />
                                    <span style="font-size: 11px; font-weight: 100;">SEM</span></th>
                                <th>a-b=(c)<br />
                                    <br />
                                    <span style="font-size: 11px; font-weight: 100;">INDEX -1 SEM</span></th>
                                <th></th>
                                <th>a+b=(d)<br />
                                    <br />
                                    <span style="font-size: 11px; font-weight: 100;">INDEX+1 SEM</span>

                                </th>
                            </tr>
                            <tr>
                                <td>Daxili</td>
                                <td>(IMI)</td>
                                <td class="underline">
                                    <asp:Label ID="LblIMI" runat="server"></asp:Label>
                                </td>
                                <td>3</td>
                                <td class="underline">
                                    <asp:Label ID="LblIMI_N" runat="server"></asp:Label>
                                </td>
                                <td>to</td>
                                <td class="underline">
                                    <asp:Label ID="LblIMI_P" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Asosial</td>
                                <td>(AMI)</td>
                                <td class="underline">
                                    <asp:Label ID="LblAMI" runat="server"></asp:Label>
                                </td>
                                <td>4</td>
                                <td class="underline">
                                    <asp:Label ID="LblAMI_N" runat="server"></asp:Label>
                                </td>
                                <td>to</td>
                                <td class="underline">
                                    <asp:Label ID="LblAMI_P" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Xarici</td>
                                <td>(EMI)</td>
                                <td class="underline">
                                    <asp:Label ID="LblEMI" runat="server"></asp:Label>
                                </td>
                                <td>3</td>
                                <td class="underline">
                                    <asp:Label ID="LblEMI_N" runat="server"></asp:Label>
                                </td>
                                <td>to</td>
                                <td class="underline">
                                    <asp:Label ID="LblEMI_P" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Ümumi</td>
                                <td>(GMI)</td>
                                <td class="underline">
                                    <asp:Label ID="LblGMI" runat="server"></asp:Label>
                                </td>
                                <td>2</td>
                                <td class="underline">
                                    <asp:Label ID="LblGMI_N" runat="server"></asp:Label>
                                </td>
                                <td>to</td>
                                <td class="underline">
                                    <asp:Label ID="LblGMI_P" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 40px; padding: 0px 20px;">
                <div class="col-md-12">
                    <div class="indexes-full-table">
                        <table class="tg">
                            <tr>
                                <th class="tg-031e"></th>
                                <th class="tg-031e"></th>
                                <th class="tg-0klj" colspan="6">Çox<br />
                                    ciddi<br />
                                    (-41 and below)</th>
                                <th class="tg-z1yq" colspan="2">Ciddi<br />
                                    <br />
                                    (-40 to -31)</th>
                                <th class="tg-z1yq" colspan="2">Orta ciddi<br />
                                    Serious<br />
                                    (-30 to -21)</th>
                                <th class="tg-z1yq" colspan="2">Cüzi ciddi<br />
                                    Serious<br />
                                    (-20 to -11)</th>
                                <th class="tg-z1yq" colspan="5">Normal<br />
                                    <br />
                                    (-10 and above)</th>
                                <th class="tg-yw4l"></th>
                            </tr>
                            <tr>
                                <td class="tg-nrw8" style="text-align: left;">Daxili</td>
                                <td class="tg-nrw8" style="text-align: left;">(IMI)</td>
                                <td class="tg-n4t1">-70</td>
                                <td class="tg-zfjm">-65</td>
                                <td class="tg-zfjm">-60</td>
                                <td class="tg-zfjm">-55</td>
                                <td class="tg-zfjm">-50</td>
                                <td class="tg-zfjm">-45</td>
                                <td class="tg-lxkg">-40</td>
                                <td class="tg-lxkg">-35</td>
                                <td class="tg-jp84">-30</td>
                                <td class="tg-jp84">-25</td>
                                <td class="tg-z4c0">-20</td>
                                <td class="tg-z4c0">-15</td>
                                <td class="tg-ecrz">-10</td>
                                <td class="tg-ecrz">-5</td>
                                <td class="tg-ecrz">0</td>
                                <td class="tg-ecrz">+5</td>
                                <td class="tg-ecrz">+10</td>
                                <td class="tg-ecrz" style="text-align: left;">(IMI)</td>
                            </tr>
                            <tr>
                                <td class="tg-nrw8" style="text-align: left;">Asosial</td>
                                <td class="tg-nrw8" style="text-align: left;">(AMI)</td>
                                <td class="tg-n4t1">-70</td>
                                <td class="tg-zfjm">-65</td>
                                <td class="tg-zfjm">-60</td>
                                <td class="tg-zfjm">-55</td>
                                <td class="tg-zfjm">-50</td>
                                <td class="tg-zfjm">-45</td>
                                <td class="tg-lxkg">-40</td>
                                <td class="tg-lxkg">-35</td>
                                <td class="tg-jp84">-30</td>
                                <td class="tg-jp84">-25</td>
                                <td class="tg-z4c0">-20</td>
                                <td class="tg-z4c0">-15</td>
                                <td class="tg-ecrz">-10</td>
                                <td class="tg-ecrz">-5</td>
                                <td class="tg-ecrz">0</td>
                                <td class="tg-ecrz">+5</td>
                                <td class="tg-ecrz">+10</td>
                                <td class="tg-ecrz" style="text-align: left;">(AMI)</td>
                            </tr>
                            <tr>
                                <td class="tg-ecrz" style="text-align: left;">Xarici</td>
                                <td class="tg-ecrz" style="text-align: left;">(EMI)</td>
                                <td class="tg-n4t1">-70</td>
                                <td class="tg-zfjm">-65</td>
                                <td class="tg-zfjm">-60</td>
                                <td class="tg-zfjm">-55</td>
                                <td class="tg-zfjm">-50</td>
                                <td class="tg-zfjm">-45</td>
                                <td class="tg-lxkg">-40</td>
                                <td class="tg-lxkg">-35</td>
                                <td class="tg-jp84">-30</td>
                                <td class="tg-jp84">-25</td>
                                <td class="tg-z4c0">-20</td>
                                <td class="tg-z4c0">-15</td>
                                <td class="tg-ecrz">-10</td>
                                <td class="tg-ecrz">-5</td>
                                <td class="tg-ecrz">0</td>
                                <td class="tg-ecrz">+5</td>
                                <td class="tg-ecrz">+10</td>
                                <td class="tg-ecrz" style="text-align: left;">(EMI)</td>
                            </tr>
                            <tr>
                                <td class="tg-ecrz" style="text-align: left;">Ümumi</td>
                                <td class="tg-ecrz" style="text-align: left;">(GMI)</td>
                                <td class="tg-n4t1">-70</td>
                                <td class="tg-zfjm">-65</td>
                                <td class="tg-zfjm">-60</td>
                                <td class="tg-zfjm">-55</td>
                                <td class="tg-zfjm">-50</td>
                                <td class="tg-zfjm">-45</td>
                                <td class="tg-lxkg">-40</td>
                                <td class="tg-lxkg">-35</td>
                                <td class="tg-jp84">-30</td>
                                <td class="tg-jp84">-25</td>
                                <td class="tg-z4c0">-20</td>
                                <td class="tg-z4c0">-15</td>
                                <td class="tg-ecrz">-10</td>
                                <td class="tg-ecrz">-5</td>
                                <td class="tg-ecrz">0</td>
                                <td class="tg-ecrz">+5</td>
                                <td class="tg-ecrz">+10</td>
                                <td class="tg-ecrz" style="text-align: left;">(GMI)</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 60px; padding: 0px 20px;">
                <div class="b-head-table" style="width: 150px">Dəstək Xalı</div>
                <div class="col-md-6 col-xs-6">
                    <div class="instructions">
                        <b>Instructions</b>
                        <ol>
                            <li>Geniş Müstəqillik W Xalını burada qeyd edin:
                                    <asp:Label ID="lblSupporScoreBI_W" runat="server" CssClass="underline"></asp:Label></li>
                            <li>Ümumi Qeyri-adaptiv İndeksini (21-ci səhifə) burada qeyd edin:
                                    <asp:Label ID="lblSupporScoreGMI" runat="server" CssClass="underline"></asp:Label></li>
                            <li>Bu iki rəqəmdən istifadə etməklə Cədvəl 1-dən uyğun gələn Dəstək Xalını əldə et və onu burada qeyd et:
                                    <asp:Label ID="lblSupporScore" runat="server" CssClass="underline"></asp:Label></li>
                            <li>Sağdakı cədvəldən istifadə etməklə bu fərdin Dəstək Xalının yerini tap və burada qeyd et:
                                    <asp:Label ID="lblSupportLevel" runat="server" CssClass="underline"></asp:Label></li>
                        </ol>
                    </div>
                </div>
                <div class="col-md-6 col-xs-6">
                    <div class="level-index-table level-index-table-2">
                        <table>
                            <tr>
                                <th>Dəstək Xalı</th>
                                <th>Dəstək Səviyyəsi</th>
                            </tr>
                            <tr>
                                <td>1-24</td>
                                <td>Geniş yayılmış</td>
                            </tr>
                            <tr>
                                <td>25-39</td>
                                <td>Ekstensiv</td>
                            </tr>
                            <tr>
                                <td>40-54</td>
                                <td>Tez-tez</td>
                            </tr>
                            <tr>
                                <td>55-69</td>
                                <td>Məhdud</td>
                            </tr>
                            <tr>
                                <td>70-84</td>
                                <td>Qeyri-müntəzəm</td>
                            </tr>
                            <tr>
                                <td>85-100</td>
                                <td>Nadir və ya heç</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 60px; padding: 0px 20px;">
                <div class="col-md-12">
                    <div class="indexes-questions">
                        <div class="question-1 question-holder">
                            <div class="question">
                                SDÖ-Y nəticələri bu fərdin hazırki fəaliyyəti ilə bağlı doğru təsvirlə təmin edirmi?
                            </div>
                            <div class="answers-holder">
                                <div class="answer">
                                    Bəli
                                    <input class="answer-input" type="text" />
                                </div>
                                <div class="answer">Xeyir <span class="answer-span"></span></div>
                            </div>
                            <div class="clearFix"></div>
                        </div>
                        <div class="question-1 question-holder question-full-answer">
                            <div class="question">
                                Cavab xeyrdirsə, nəticələrin sual altında qoyulma səbəbini bildirin?
                            </div>
                            <div class="answers-holder">
                                <div class="answer">
                                    <input class="answer-input" type="text" />
                                </div>
                                <%-- <div class="answer"><span class="answer-span"></span></div>--%>
                            </div>
                            <div class="clearFix"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div role="tabpanel" class="tab-pane fade" id="chart" aria-labelledby="chart-tab">

            <%--  <div class="row">
                <div class="col-md-6">
                    <div id="chartContainerMS" class="chartContainer">
                    </div>
                    <br />
                    <br />
                </div>

                <div class="col-md-6">
                    <div id="chartContainerSC" class="chartContainer">
                    </div>
                    <br />
                    <br />
                </div>
                <div class="col-md-6">
                    <div id="chartContainerPL" class="chartContainer">
                    </div>
                    <br />
                    <br />
                </div>
                <div class="col-md-6">
                    <div id="chartContainerCL" class="chartContainer">
                    </div>
                    <br />
                    <br />
                </div>
                <div class="col-md-6">
                    <div id="chartContainerBI" class="chartContainer">
                    </div>
                </div>
                <div class="col-md-6">
                    <div id="chartContainer" class="chartContainer">
                    </div>
                </div>
            </div>--%>

            <div class="row">
                <div class="col-md-4">
                    <canvas id="line-chart-motorSkills" width="800" height="450"></canvas>
                </div>
                <div class="col-md-4">
                    <canvas id="line-chart-socialInteraction" width="800" height="450"></canvas>
                </div>
                <div class="col-md-4">
                    <canvas id="line-chart-personalLiving" width="800" height="450"></canvas>
                </div>
            </div>
            <br />
            <br />
            <br />

            <div class="row">
                <div class="col-md-4">
                    <canvas id="line-chart-communityLiving" width="800" height="450"></canvas>
                </div>

                <div class="col-md-4">
                    <canvas id="line-chart-broadIndependence" width="800" height="450"></canvas>
                </div>

                <div class="col-md-4">
                    <canvas id="line-chart" width="800" height="450"></canvas>
                </div>

            </div>

        </div>

        <asp:HiddenField ID="HdnMotorSkillsSumW" ClientIDMode="Static" Value="" runat="server" />
        <asp:HiddenField ID="HdnMotorSkillsREFW" ClientIDMode="Static" Value="" runat="server" />
        <asp:HiddenField ID="HdnMotorSkillsDate" ClientIDMode="Static" Value="" runat="server" />

        <asp:HiddenField ID="HdnSocialInteractionSumW" ClientIDMode="Static" Value="" runat="server" />
        <asp:HiddenField ID="HdnSocialInteractionREFW" ClientIDMode="Static" Value="" runat="server" />
        <asp:HiddenField ID="HdnSocialInteractionDate" ClientIDMode="Static" Value="" runat="server" />

        <asp:HiddenField ID="HdnPersonalLivingSumW" ClientIDMode="Static" Value="" runat="server" />
        <asp:HiddenField ID="HdnPersonalLivingREFW" ClientIDMode="Static" Value="" runat="server" />
        <asp:HiddenField ID="HdnPersonalLivingDate" ClientIDMode="Static" Value="" runat="server" />

        <asp:HiddenField ID="HdnCommunityLivingSumW" ClientIDMode="Static" Value="" runat="server" />
        <asp:HiddenField ID="HdnCommunityLivingREFW" ClientIDMode="Static" Value="" runat="server" />
        <asp:HiddenField ID="HdnCommunityLivingDate" ClientIDMode="Static" Value="" runat="server" />

        <asp:HiddenField ID="HdnBroadIndependenceSumW" ClientIDMode="Static" Value="" runat="server" />
        <asp:HiddenField ID="HdnBroadIndependenceREFW" ClientIDMode="Static" Value="" runat="server" />
        <asp:HiddenField ID="HdnBroadIndependenceDate" ClientIDMode="Static" Value="" runat="server" />


    </div>
    <%-- http://tobiasahlin.com/blog/chartjs-charts-to-get-you-started/#7-horizontal-bar-chart--%>
    <script src="/js/chart.min.js"></script>
    <script>

        var valueMotorSkillsSumW = $("#HdnMotorSkillsSumW").val().split(',');
        var valueMotorSkillsREFW = $("#HdnMotorSkillsREFW").val().split(',');
        var valueMotorSkillsDate = $("#HdnMotorSkillsDate").val().split(',');

        var valueSocialInteractionSumW = $("#HdnSocialInteractionSumW").val().split(',');
        var valueSocialInteractionREFW = $("#HdnSocialInteractionREFW").val().split(',');
        var valueSocialInteractionDate = $("#HdnSocialInteractionDate").val().split(',');

        var valuePersonalLivingSumW = $("#HdnPersonalLivingSumW").val().split(',');
        var valuePersonalLivingREFW = $("#HdnPersonalLivingREFW").val().split(',');
        var valuePersonalLivingDate = $("#HdnPersonalLivingDate").val().split(',');

        var valueCommunityLivingSumW = $("#HdnCommunityLivingSumW").val().split(',');
        var valueCommunityLivingREFW = $("#HdnCommunityLivingREFW").val().split(',');
        var valueCommunityLivingDate = $("#HdnCommunityLivingDate").val().split(',');

        var valueBroadIndependenceSumW = $("#HdnBroadIndependenceSumW").val().split(',');
        var valueBroadIndependenceREFW = $("#HdnBroadIndependenceREFW").val().split(',');
        var valueBroadIndependenceDate = $("#HdnBroadIndependenceDate").val().split(',');


        new Chart(document.getElementById("line-chart-motorSkills"), {
            type: 'line',
            data: {
                labels: valueMotorSkillsDate,
                datasets: [

                    {
                        data: valueMotorSkillsSumW,
                        label: "Hərəki Bacarıqlar",
                        borderColor: "#8e5ea2",
                        fill: false,
                    }
                    , {

                        data: valueMotorSkillsREFW,
                        label: "Norma",
                        borderColor: "#3cba9f",
                        fill: false
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Hərəki Bacarıqlar'
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            suggestedMin: 100,
                            suggestedMax: 700,
                            stepSize: 75
                        }
                    }]
                }
            }
        });

        new Chart(document.getElementById("line-chart-socialInteraction"), {
            type: 'line',
            data: {
                labels: valueSocialInteractionDate,
                datasets: [
                    {
                        data: valueSocialInteractionSumW,
                        label: "Sosial Əlaqə və Ünsiyyət Bacarıqları",
                        borderColor: "#3e95cd",
                        fill: false
                    }, {

                        data: valueSocialInteractionREFW,
                        label: "Norma",
                        borderColor: "#3cba9f",
                        fill: false
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Sosial Əlaqə və Ünsiyyət Bacarıqları'
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            suggestedMin: 100,
                            suggestedMax: 700,
                            stepSize: 75
                        }
                    }]
                }
            }
        });

        new Chart(document.getElementById("line-chart-personalLiving"), {
            type: 'line',
            data: {
                labels: valuePersonalLivingDate,
                datasets: [
                    {
                        data: valuePersonalLivingSumW,
                        label: "Şəxsi Həyat Bacarıqları",
                        borderColor: "#51829e",
                        fill: false
                    }, {

                        data: valuePersonalLivingREFW,
                        label: "Norma",
                        borderColor: "#3cba9f",
                        fill: false
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Şəxsi Həyat Bacarıqları'
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            suggestedMin: 100,
                            suggestedMax: 700,
                            stepSize: 75
                        }
                    }]
                }
            }
        });

        new Chart(document.getElementById("line-chart-communityLiving"), {
            type: 'line',
            data: {
                labels: valueCommunityLivingDate,
                datasets: [
                    {
                        data: valueCommunityLivingSumW,
                        label: "İcma Həyatı Bacarıqları",
                        borderColor: "#c45850",
                        fill: false
                    }, {

                        data: valueCommunityLivingREFW,
                        label: "Norma",
                        borderColor: "#3cba9f",
                        fill: false
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'İcma Həyatı Bacarıqları'
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            suggestedMin: 100,
                            suggestedMax: 700,
                            stepSize: 75
                        }
                    }]
                }
            }
        });

        new Chart(document.getElementById("line-chart-broadIndependence"), {
            type: 'line',
            data: {
                labels: valueBroadIndependenceDate,
                datasets: [
                    {
                        data: valueBroadIndependenceSumW,
                        label: "Geniş Müstəqillik (Tam Ölçü)",
                        borderColor: "#e8c3b9",
                        fill: false
                    }
                    , {
                        //valueBroadIndependenceREFW                     
                        data: [450,480,580,600,620],
                        label: "Norma",
                        borderColor: "#3cba9f",
                        fill: false
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Geniş Müstəqillik (Tam Ölçü)'
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            suggestedMin: 100,
                            suggestedMax: 700,
                            stepSize: 80
                        }
                    }]
                }
            }
        });

    </script>

</asp:Content>

