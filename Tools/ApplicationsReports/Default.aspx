<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_ApplicationsReports_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.6.0/Chart.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="Filter">
                <div class="row">
                    <asp:Panel ID="PnlOrganizations" runat="server" class="col-md-2">
                        Mərkəzlər:<br />
                        <asp:ListBox ID="DListFilterMultiOrganizations" runat="server" Style="display: none" ClientIDMode="Static" SelectionMode="Multiple" data-placeholder=" " CssClass="multiSelectAll form-control" DataValueField="ID" DataTextField="Name"></asp:ListBox>
                    </asp:Panel>

                    <div class="col-md-2">
                        Növü:
                        <br />
                        <asp:DropDownList ID="DListFilterReportsTypes" DataValueField="ID" DataTextField="Name" AutoPostBack="true" OnSelectedIndexChanged="DListFilterReportsTypes_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>

                    <asp:Panel ID="PnlFilterMultiYears" Visible="false" runat="server" class="col-md-2">
                        İllər:<br />
                        <asp:ListBox ID="DListFilterMultiYears" runat="server" Style="display: none" ClientIDMode="Static" SelectionMode="Multiple" data-placeholder=" " CssClass="multiSelectAll form-control" DataValueField="ID" DataTextField="Name"></asp:ListBox>
                    </asp:Panel>

                    <asp:Panel ID="PnlFilterYears" Visible="false" runat="server" class="col-md-2">
                        İl:
                        <br />
                        <asp:DropDownList ID="DListFilterYears" DataValueField="Name" DataTextField="Name" AutoPostBack="true" OnSelectedIndexChanged="DListFilterYears_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                    </asp:Panel>

                    <asp:Panel ID="PnlFilterMultiMonths" Visible="false" runat="server" class="col-md-2">
                        Aylar:<br />
                        <asp:ListBox ID="DListFilterMultiMonths" runat="server" Style="display: none" ClientIDMode="Static" SelectionMode="Multiple" data-placeholder=" " CssClass="multiSelectAll form-control" DataValueField="ID" DataTextField="Name"></asp:ListBox>
                    </asp:Panel>

                    <asp:Panel ID="PnlFilter" runat="server" class="col-md-2">
                        <br />
                        <asp:Button ID="BtnFilter" CssClass="btn btn-default" Width="100px" Height="40px" runat="server" Text="AXTAR" Font-Bold="False" OnClick="BtnFilter_Click" OnClientClick="this.style.display='none';document.getElementById('loading').style.display=''" />
                        <img id="loading" src="/images/loading.gif" style="display: none" />
                    </asp:Panel>

                </div>
            </div>

            <div class="report-table-tabs">
                <div class="tab-heads-holder">
                    <div class="tab-heads">
                        <div class="tab-head active" data-target="tab-1">Müraciətlər</div>
                        <div class="tab-head" data-target="tab-2">Sosial status</div>
                        <div class="tab-head" data-target="tab-3">Müraciət olunan xidmətlər</div>
                        <div class="tab-head" data-target="tab-4">CASE</div>
                        <div class="tab-head" data-target="tab-5">Ailə səfərləri</div>
                        <div class="tab-head" data-target="tab-6">Təlim və tədbirlər</div>
                        <div class="tab-head" data-target="tab-7">Qrafik təsvir</div>

                    </div>
                    <div class="icons-holder">
                        <img class="toggle-dropdowns" src="/Images/down.png" alt="Alternate Text" />
                        <img class="toggle-tabs" src="/Images/open.png" alt="Alternate Text" />
                        <img src="/images/print.png" alt="Alternate Text" onclick="OpenTabsForPrint();window.print();" />

                    </div>
                </div>

                <div class="tab-content-reports active" id="tab-1">
                    <h3 class="tab-name">Müraciətlər</h3>
                    <table class="report-table">
                        <tr class="tr-month">
                            <th></th>
                            <asp:Literal ID="LtrOrganizationsMonths" runat="server"></asp:Literal>
                            <th>CƏMİ</th>
                        </tr>

                        <asp:Literal ID="LtrReportsOrganizations" runat="server"></asp:Literal>

                    </table>
                </div>

                <div class="tab-content-reports" id="tab-2">
                    <h3 class="tab-name">Sosial status</h3>
                    <table class="report-table">
                        <tr class="tr-month">
                            <th></th>
                            <asp:Literal ID="LtrSosialStatusMonths" runat="server"></asp:Literal>
                            <th>CƏMİ</th>
                        </tr>

                        <asp:Literal ID="LtrReportsSosialStaus" runat="server"></asp:Literal>

                    </table>

                </div>

                <div class="tab-content-reports" id="tab-3">
                    <h3 class="tab-name">Müraciət olunan xidmətlər</h3>
                    <table class="report-table">
                        <tr class="tr-month">
                            <th></th>
                            <asp:Literal ID="LtrServicesMonths" runat="server"></asp:Literal>
                            <th>CƏMİ</th>
                        </tr>

                        <asp:Literal ID="LtrReportsServices" runat="server"></asp:Literal>

                    </table>

                </div>

                <div class="tab-content-reports" id="tab-4">
                    <h3 class="tab-name">CASE</h3>
                    <table class="report-table">
                        <tr class="tr-month">
                            <th></th>
                            <asp:Literal ID="LtrCaseMonths" runat="server"></asp:Literal>
                            <th>CƏMİ</th>
                        </tr>

                        <asp:Literal ID="LtrReportsCase" runat="server"></asp:Literal>

                    </table>

                </div>

                <div class="tab-content-reports" id="tab-5">
                    <h3 class="tab-name">Ailə səfərləri</h3>
                    <table class="report-table">
                        <tr class="tr-month">
                            <th></th>
                            <asp:Literal ID="LtrApplicationFammilyMonths" runat="server"></asp:Literal>
                            <th>CƏMİ</th>
                        </tr>

                        <asp:Literal ID="LtrReportsApplicationsFamily" runat="server"></asp:Literal>

                    </table>
                </div>

                <div class="tab-content-reports" id="tab-6">
                    <h3 class="tab-name">Təlim və tədbirlər</h3>
                    <table class="report-table">
                        <tr class="tr-month">
                            <th></th>
                            <asp:Literal ID="LtrEventsMonths" runat="server"></asp:Literal>
                            <th>CƏMİ</th>
                        </tr>

                        <asp:Literal ID="LtrReportsEvents" runat="server"></asp:Literal>

                    </table>
                </div>

                <div class="tab-content-reports" id="tab-7">
                    <h3 class="tab-name">Qrafik təsvir</h3>
                    <table class="report-table">
                       
                            <%-- <canvas id="myChart"></canvas>--%>
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
                            </div>
                      
                        <%-- <tr class="tr-month">
                            <th></th>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            <th>CƏMİ</th>
                        </tr>

                        <asp:Literal ID="Literal2" runat="server"></asp:Literal>--%>
                    </table>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:HiddenField ID="HdnMotorSkills" ClientIDMode="Static" Value="40,20,10,16,24,38,74,167,508,784" runat="server" />
    <asp:HiddenField ID="HdnSocialInteraction" ClientIDMode="Static" Value="40,20,10,16,24,38,74,167,508,784" runat="server" />
    <asp:HiddenField ID="HdnPersonalLiving" ClientIDMode="Static" Value="40,20,10,16,24,38,74,167,508,784" runat="server" />
    <asp:HiddenField ID="HdnCommunityLiving" ClientIDMode="Static" Value="40,20,10,16,24,38,74,167,508,784" runat="server" />
    <asp:HiddenField ID="HdnBroadIndependence" ClientIDMode="Static" Value="40,20,10,16,24,38,74,167,508,784" runat="server" />

    <asp:HiddenField ID="HdnMonths" ClientIDMode="Static" Value="Yanvar,Fevral,Mart,Aprel,May,İyun,Iyul,Avqust,Sentyabr" runat="server" />
    <asp:HiddenField ID="HdnValues" ClientIDMode="Static" Value="38,17,6,142,2,0" runat="server" />

    <script src="/js/chart.min.js"></script>
    <script>
        var valueMotorSkills = $("#HdnMotorSkills").val().split(',');
        var valueSocialInteraction = $("#HdnSocialInteraction").val().split(',');
        var valuePersonalLiving = $("#HdnPersonalLiving").val().split(',');
        var valueCommunityLiving = $("#HdnCommunityLiving").val().split(',');
        var valueBroadIndependence = $("#HdnBroadIndependence").val().split(',');

        new Chart(document.getElementById("line-chart-motorSkills"), {
            type: 'line',
            data: {
                labels: [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050],
                datasets: [
                    {
                        data: valueMotorSkills,
                        label: "Motor Skills",
                        borderColor: "#8e5ea2",
                        fill: false
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Motor Skills'
                }
            }
        });

        new Chart(document.getElementById("line-chart-socialInteraction"), {
            type: 'line',
            data: {
                labels: [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050],
                datasets: [
                    {
                        data: valueSocialInteraction,
                        label: "Social Interaction & Communication Skills",
                        borderColor: "#3e95cd",
                        fill: false
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Social Interaction'
                }
            }
        });

        new Chart(document.getElementById("line-chart-personalLiving"), {
            type: 'line',
            data: {
                labels: [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050],
                datasets: [
                    {
                        data: valuePersonalLiving,
                        label: "Personal Living",
                        borderColor: "#3cba9f",
                        fill: false
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Personal Living'
                }
            }
        });

        new Chart(document.getElementById("line-chart-communityLiving"), {
            type: 'line',
            data: {
                labels: [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050],
                datasets: [
                    {
                        data: valueCommunityLiving,
                        label: "Community Living",
                        borderColor: "#c45850",
                        fill: false
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Community Living'
                }
            }
        });

        new Chart(document.getElementById("line-chart-broadIndependence"), {
            type: 'line',
            data: {
                labels: [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050],
                datasets: [
                    {
                        data: valueBroadIndependence,
                        label: "Broad Independence",
                        borderColor: "#e8c3b9",
                        fill: false
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Broad Independence'
                }
            }
        });

    </script>


    <script type="text/javascript">

        $(document).ready(function () {
            Report();
        });

        //var month = [];
        //month = $("#HdnMonths").val().split(",");

        //var values = [];
        //values = $("#HdnValues").val().split(",");

        //let myChart = document.getElementById('myChart').getContext('2d');

        //// Global Options
        //Chart.defaults.global.defaultFontFamily = 'Lato';
        //Chart.defaults.global.defaultFontSize = 18;
        //Chart.defaults.global.defaultFontColor = '#777';

        //let massPopChart = new Chart(myChart, {
        //    type: 'bar', // bar, horizontalBar, pie, line, doughnut, radar, polarArea
        //    data: {
        //        labels: month,
        //        datasets: [{
        //            label: 'Say',
        //            data: values,
        //            //backgroundColor:'green',
        //            backgroundColor: [
        //                'rgba(255, 99, 132, 0.6)',
        //                'rgba(54, 162, 235, 0.6)',
        //                'rgba(255, 206, 86, 0.6)',
        //                'rgba(75, 192, 192, 0.6)',
        //                'rgba(153, 102, 255, 0.6)',
        //                'rgba(255, 159, 64, 0.6)',
        //                'rgba(255, 99, 132, 0.6)'
        //            ],
        //            borderWidth: 1,
        //            borderColor: '#777',
        //            hoverBorderWidth: 3,
        //            hoverBorderColor: '#000'
        //        }]
        //    },
        //    options: {
        //        title: {
        //            display: true,
        //            text: 'Largest Cities In Massachusetts',
        //            fontSize: 25
        //        },
        //        legend: {
        //            display: true,
        //            position: 'right',
        //            labels: {
        //                fontColor: '#000'
        //            }
        //        },
        //        layout: {
        //            padding: {
        //                left: 50,
        //                right: 0,
        //                bottom: 0,
        //                top: 0
        //            }
        //        },
        //        tooltips: {
        //            enabled: true
        //        }
        //    }
        //});

        function Report() {
            $(".report-table .inner-table-holder th:first-child").on("click", function () {
                $(this).parent().parent().find("tr:not(.new-table)").toggle();
            });

            $(".report-table-tabs .tab-heads .tab-head").on("click", function () {
                $(".tab-head.active").removeClass("active");
                $(this).addClass("active");
                var target = $(this).attr("data-target");
                $(".report-table-tabs .tab-content-reports").removeClass("active");
                $("#" + target).addClass("active");
            });

            var isDropdownsOpen = false;
            $(".toggle-dropdowns").on("click", function () {
                if (isDropdownsOpen) {
                    $(".tab-content-reports tr:not(.new-table):not(.tr-month)").hide();
                    $(".toggle-dropdowns").attr("src", "/images/down.png");
                    isDropdownsOpen = false;
                } else {
                    $(".tab-content-reports tr:not(.new-table):not(.tr-month)").show();
                    $(".toggle-dropdowns").attr("src", "/images/top.png");
                    isDropdownsOpen = true;
                }
            });

            var isTabsOpen = false;
            $(".toggle-tabs").on("click", function () {
                if (isTabsOpen) {
                    $(".report-table-tabs").removeClass("show-all");
                    $(".toggle-tabs").attr("src", "/Images/open.png");
                    isTabsOpen = false;
                } else {
                    $(".report-table-tabs").addClass("show-all");
                    $(".toggle-tabs").attr("src", "/images/tabclose.png");
                    isTabsOpen = true;
                }

            })
        }

        function OpenTabsForPrint() {

            $(".tab-content-reports tr:not(.new-table):not(.tr-month)").show();
            $(".toggle-dropdowns").attr("src", "/images/top.png");

            $(".report-table-tabs").addClass("show-all");
            $(".toggle-tabs").attr("src", "/images/tabclose.png");
        }
    </script>


</asp:Content>

