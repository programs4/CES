<%@ Page EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Tools_SIB_R_Add_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="row">
                <div class="col-md-9">
                    <div class="form-holder-sib">
                        <form class="form-inline">
                            <div class="form-group">
                                <label for="exampleInputName2">Qiymətləndirmə növü</label>
                                <asp:DropDownList ID="DListSIBRTypes" CssClass="form-control" DataValueField="ID" DataTextField="Name" runat="server"></asp:DropDownList>
                            </div>
                            <div class="row">
                                <div class="col-md-10">
                                    <div class="form-group">
                                        <label for="exampleInputName2">Soyadı,adı</label>
                                        <asp:TextBox ID="TxtFullName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="exampleInputName2">Cinsi</label><br>
                                        K<asp:RadioButton ID="RbMale" runat="server" Style="margin-right: 15px; margin-left: 9px;" Checked="true" Enabled="false" />
                                        Q<asp:RadioButton ID="RbFemale" runat="server" Style="margin-left: 9px;" Enabled="false" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputName2">Cavabdeh</label>
                                        <asp:TextBox ID="TxtRespondent" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputName2">Qohumluq əlaqəsi</label>
                                        <asp:TextBox ID="TxtRelationship" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputName2">Yoxlayan</label>
                                <asp:TextBox ID="TxtExaminer" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputName2">Məktəb və ya təşkilat</label>
                                <asp:TextBox ID="TxtSchool" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputName2">Valideyn/Qəyyum</label>
                                <asp:TextBox ID="TxtParent" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputName2">Ünvan</label>
                                <asp:TextBox ID="TxtAdress" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="exampleInputName2">Qeyd</label>
                                <asp:TextBox ID="TxtDescription" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </form>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="calc-age">
                        <label>Yaşın hesablanması</label>
                        <div class="calc-age-main">

                            <table class="tg">
                                <tr>
                                    <th class="tg-iuhm"></th>
                                    <th class="tg-2bfz">İl</th>
                                    <th class="tg-2bfz">Ay</th>
                                    <th class="tg-2bfz">Gün</th>
                                </tr>
                                <tr>
                                    <td class="tg-e2nl">Yoxlama tarixi</td>
                                    <td class="tg-iuhm">
                                        <asp:TextBox ID="TxtTestingDateYear" runat="server" CssClass="selectAll numeric" MaxLength="4" onkeyup="calculationAge()" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="tg-yw4l">
                                        <asp:TextBox ID="TxtTestingDateMonth" runat="server" CssClass="selectAll numeric" MaxLength="2" onkeyup="calculationAge()" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                    <td class="tg-yw4l">
                                        <asp:TextBox ID="TxtTestingDateDay" runat="server" CssClass="selectAll numeric" MaxLength="2" onkeyup="calculationAge()" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tg-e2nl">Doğum tarixi</td>
                                    <td class="tg-iuhm">
                                        <asp:TextBox ID="TxtBirthDateYear" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="tg-yw4l">
                                        <asp:TextBox ID="TxtBirthDateMonth" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="tg-yw4l">
                                        <asp:TextBox ID="TxtBirthDateDay" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tg-e2nl">Fərqi</td>
                                    <td class="tg-iuhm">
                                        <asp:TextBox ID="TxtDifferenceYear" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="tg-yw4l">
                                        <asp:TextBox ID="TxtDifferenceMonth" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="tg-yw4l">
                                        <asp:TextBox ID="TxtDifferenceDay" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tg-e2nl">Yaş</td>
                                    <td class="tg-iuhm">
                                        <asp:TextBox ID="TxtAgeYear" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="HdnAgeYear" runat="server" ClientIDMode="Static" />
                                    </td>
                                    <td class="tg-yw4l">
                                        <asp:TextBox ID="TxtAgeMonth" runat="server" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="HdnAgeMonth" runat="server" ClientIDMode="Static" />
                                    </td>
                                    <td class="tg-yw4l"></td>
                                </tr>
                                <tr>
                                    <td class="tg-iuhm"></td>
                                    <td class="tg-iuhm" colspan="3">(Ayların cəmi)</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>

        <asp:View ID="View2" runat="server">
            <asp:Repeater ID="RptAdaptive" runat="server">
                <ItemTemplate>
                    <div class="row" <%#Container.ItemIndex==0?"":"style=\"margin-top: 40px;\"" %>>
                        <div class="col-md-12">
                            <div class="adaptive-behavior-sib">
                                <p><%# string.Format("{0}. {1}",Eval("Alphabet"),Eval("Name")) %> / Adaptiv Davranış</p>
                                <asp:HiddenField ID="HdnSIBRScoringTypes" runat="server" Value='<%#Eval("ID")%>' />
                                <div class="c-row-holder">
                                    <div class="calculate-b-sib c-b-s-1">
                                        <div class="sib-box">
                                            <div class="blue-box">
                                                <asp:TextBox ID="TxtSumA" runat="server" MaxLength="2" CssClass="inputSumA selectAll numeric" data-key="A" data-rowId='<%# Container.ItemIndex+1 %>' ClientIDMode="Static" TabIndex='<%# Container.ItemIndex * 3 + 1 %>'></asp:TextBox>
                                            </div>
                                            <span>Sum A</span>
                                        </div>
                                        <div class="sib-box">
                                            <div class="blue-box">
                                                <asp:TextBox ID="TxtSumB" runat="server" MaxLength="2" CssClass="inputSumB selectAll numeric" data-key="B" data-rowId='<%# Container.ItemIndex+1 %>' ClientIDMode="Static" TabIndex='<%# Container.ItemIndex * 3 + 1  %>'></asp:TextBox>
                                            </div>
                                            <span>Sum B</span>
                                        </div>
                                        <div class="sib-box">
                                            <div class="blue-box">
                                                <asp:TextBox ID="TxtSumC" runat="server" MaxLength="2" CssClass="inputSumC selectAll numeric" data-key="C" data-rowId='<%# Container.ItemIndex+1 %>' ClientIDMode="Static" TabIndex='<%# Container.ItemIndex * 3 + 1  %>'></asp:TextBox>
                                            </div>
                                            <span>Sum C</span>
                                        </div>
                                    </div>
                                    <div class="calculate-b-sib c-b-s-2">
                                        <div class="sib-box">
                                            <div class="blue-box blue-box-disable">
                                                <asp:Label ID="LblSumA" runat="server" CssClass="outputSumA" ClientIDMode="Static" data-rowId='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </div>
                                            <span>Sum A</span>
                                        </div>
                                        <span class="plus">+</span>
                                        <div class="sib-box">
                                            <div class="blue-box blue-box-disable">
                                                <asp:Label ID="LblSumB" runat="server" CssClass="outputSumB" ClientIDMode="Static" data-rowId='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </div>
                                            <span>Sum Bx2</span>
                                        </div>
                                        <span class="plus">+</span>
                                        <div class="sib-box">
                                            <div class="blue-box blue-box-disable">
                                                <asp:Label ID="LblSumC" runat="server" CssClass="outputSumC" ClientIDMode="Static" data-rowId='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </div>
                                            <span>Sum Cx3</span>
                                        </div>
                                        <span class="plus">=</span>
                                        <div class="sib-box">
                                            <div class="blue-box blue-box-disable">
                                                <asp:Label ID="LblRawScore" runat="server" CssClass="sumRawScore" ClientIDMode="Static" data-rowId='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </div>
                                            <span>Raw Score</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Panel ID="PnlRawScore" runat="server">
                <asp:HiddenField ID="HdnRawScore1" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore2" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore3" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore4" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore5" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore6" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore7" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore8" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore9" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore10" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore11" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore12" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore13" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore14" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="HdnRawScore15" ClientIDMode="Static" runat="server" />

            </asp:Panel>
        </asp:View>

        <asp:View ID="View4" runat="server">

            <div class="row" style="margin-top: 40px">
                <div class="col-md-12">
                    <div class="adaptive-behavior-sib">
                        <p>Page 2 Early Development Form/Adaptive Behavior</p>
                        <div class="c-row-holder">
                            <div class="calculate-b-sib c-b-s-1">
                                <div class="sib-box">
                                    <div class="blue-box">
                                        <asp:TextBox ID="TxtPage2SumA" runat="server" MaxLength="2" CssClass="inputSumA selectAll numeric" data-rowId="1" data-key="A" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <span>Sum A</span>
                                </div>
                                <div class="sib-box">
                                    <div class="blue-box">
                                        <asp:TextBox ID="TxtPage2SumB" runat="server" MaxLength="2" CssClass="inputSumB selectAll numeric" data-rowId="1" data-key="B" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <span>Sum B</span>
                                </div>
                                <div class="sib-box">
                                    <div class="blue-box">
                                        <asp:TextBox ID="TxtPage2SumC" runat="server" MaxLength="2" CssClass="inputSumC selectAll numeric" data-rowId="1" data-key="C" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <span>Sum C</span>
                                </div>
                            </div>
                            <div class="calculate-b-sib c-b-s-2">
                                <div class="sib-box">
                                    <div class="blue-box blue-box-disable">
                                        <asp:Label ID="LblPage2SumA" runat="server" CssClass="outputSumA" data-rowId="1" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <span>Sum A</span>
                                </div>
                                <span class="plus">+</span>
                                <div class="sib-box">
                                    <div class="blue-box blue-box-disable">
                                        <asp:Label ID="LblPage2SumB" runat="server" CssClass="outputSumB" data-rowId="1" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <span>Sum Bx2</span>
                                </div>
                                <span class="plus">+</span>
                                <div class="sib-box">
                                    <div class="blue-box blue-box-disable">
                                        <asp:Label ID="LblPage2SumC" runat="server" CssClass="outputSumC" data-rowId="1" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <span>Sum Cx3</span>
                                </div>
                                <span class="plus">=</span>
                                <div class="sib-box">
                                    <div class="blue-box blue-box-disable">
                                        <asp:Label ID="LblPage2RawScore" runat="server" CssClass="sumRawScore" data-rowId="1" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <span style="display: block">Raw Score</span>
                                    <span>Page 2</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" style="margin-top: 40px">
                <div class="col-md-12">
                    <div class="adaptive-behavior-sib">
                        <p>Page 3 Early Development Form/Adaptive Behavior</p>
                        <div class="c-row-holder">
                            <div class="calculate-b-sib c-b-s-1">
                                <div class="sib-box">
                                    <div class="blue-box">
                                        <asp:TextBox ID="TxtPage3SumA" runat="server" MaxLength="2" CssClass="inputSumA selectAll numeric" data-rowId="2" data-key="A" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <span>Sum A</span>
                                </div>
                                <div class="sib-box">
                                    <div class="blue-box">
                                        <asp:TextBox ID="TxtPage3SumB" runat="server" MaxLength="2" CssClass="inputSumB selectAll numeric" data-rowId="2" data-key="B" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <span>Sum B</span>
                                </div>
                                <div class="sib-box">
                                    <div class="blue-box">
                                        <asp:TextBox ID="TxtPage3SumC" runat="server" MaxLength="2" CssClass="inputSumC selectAll numeric" data-rowId="2" data-key="C" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <span>Sum C</span>
                                </div>
                            </div>
                            <div class="calculate-b-sib c-b-s-2">
                                <div class="sib-box">
                                    <div class="blue-box blue-box-disable">
                                        <asp:Label ID="LblPage3SumA" runat="server" CssClass="outputSumA" data-rowId="2" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <span>Sum A</span>
                                </div>
                                <span class="plus">+</span>
                                <div class="sib-box">
                                    <div class="blue-box blue-box-disable">
                                        <asp:Label ID="LblPage3SumB" runat="server" CssClass="outputSumB" data-rowId="2" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <span>Sum Bx2</span>
                                </div>
                                <span class="plus">+</span>
                                <div class="sib-box">
                                    <div class="blue-box blue-box-disable">
                                        <asp:Label ID="LblPage3SumC" runat="server" CssClass="outputSumC" data-rowId="2" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <span>Sum Cx3</span>
                                </div>
                                <span class="plus">=</span>
                                <div class="sib-box">
                                    <div class="blue-box blue-box-disable">
                                        <asp:Label ID="LblPage3RawScore" runat="server" CssClass="sumRawScore" data-rowId="2" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <span style="display: block">Raw Score</span>
                                    <span>Page 3</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" style="margin-top: 40px">
                <div class="col-md-12">
                    <div class="adaptive-behavior-sib">
                        <p></p>
                        <div class="c-row-holder">
                            <div class="calculate-b-sib">
                                <div class="sib-box">
                                    <div class="blue-box blue-box-disable">
                                        <asp:Label ID="LblPlusPage2RawScore" runat="server" CssClass="sumRawScore" data-rowId="1" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <span style="display: block">Raw Score</span>
                                    <span>Page 2</span>
                                </div>
                                <span class="plus">+</span>
                                <div class="sib-box">
                                    <div class="blue-box blue-box-disable">
                                        <asp:Label ID="LblPlusPage3RawScore" runat="server" CssClass="sumRawScore" data-rowId="2" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <span style="display: block">Raw Score</span>
                                    <span>Page 3</span>
                                </div>
                                <span class="plus">=</span>
                                <div class="sib-box">
                                    <div class="blue-box blue-box-disable">
                                        <asp:Label ID="LblTotalRawScore" runat="server" CssClass="sumTotalRawScore" data-rowId="3" ClientIDMode="Static"></asp:Label>
                                    </div>
                                    <span style="display: block">Raw Score</span>
                                    <span>Total</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </asp:View>

        <asp:View ID="View3" runat="server" OnActivate="View3_Activate">

            <div id="contentLoading" class="loading-overlay loading-theme-light loading-hidden" style="display: none">
                <div class="loading-overlay-content">
                    <img src="/Images/loadingBlue.gif" style="z-index: 9999; width: 80px; margin-left: auto" />
                    Hesablanır...
                </div>
            </div>
            <div class="row" id="contentFinish" style="margin-top: 60px;">
                <div class="col-md-12">
                    <asp:Panel ID="PnlProblemBehavior" runat="server" class="problem-beh-table">
                        <table class="tg">
                            <tr>
                                <th class="tg-031e" style="border: none;"></th>
                                <th class="tg-s6z2" style="border-left: none; border-top: none;"></th>
                                <th class="tg-dgjr" colspan="4" style="padding: 5px 10px;">Addım 2</th>
                            </tr>
                            <tr>
                                <td class="tg-031e" rowspan="2" style="border-left: none; border-top: none;"></td>
                                <td class="tg-2xjm" rowspan="2">Problemli Davranış</td>
                                <td class="tg-dxjn" colspan="4" style="padding: 5px 10px;">Sıxlıq və Ağırlıq dərəcəsinin qiymətini göstərən Xallar Bölməsi</td>
                            </tr>
                            <tr>
                                <td class="tg-cg0l" style="padding: 5px 10px;">Mənimsənilmiş Uyğunlaşmama İndeksi</td>
                                <td class="tg-0ug4" style="padding: 5px 10px;">Asosial Uyğunlaşmama İndeksi</td>
                                <td class="tg-0ug4" style="padding: 5px 10px;">Xarici Uyğunlaşmama İndeksi</td>
                                <td class="tg-0ug4" style="padding: 5px 10px;">Ümumi Uyğunlaşmama İndeksi</td>
                            </tr>
                            <tr>
                                <td class="tg-sn24" rowspan="8" style="color: white; font-size: 11px; vertical-align: middle; text-align: center">A<br />
                                    d<br />
                                    d<br />
                                    ı<br />
                                    m<br />
                                    <br />
                                    1</td>
                                <td class="tg-yw4l">
                                    <div class="first-column">
                                        <div class="row-descs">
                                            <b style="font-size: 13px">1. Özünə ziyan verən davranış</b>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Tezlik dərəcəsinin xalları</p>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Ağırlıq dərəcəsinin xalları</p>
                                        </div>
                                        <div class="row-inputs">
                                            <asp:TextBox ID="Txt1" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="1"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="Txt2" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="2"></asp:TextBox>
                                        </div>
                                        <div class="row-ratings">
                                            <b style="font-size: 13px; display: block; margin-bottom: -11px;">Xallar:</b><br />
                                            PS:<br />
                                            PS:
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-1">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>16</span><br />
                                                <span>16</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>18</span><br />
                                                <span>19</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>20</span><br />
                                                <span>22</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>22</span><br />
                                                <span>25</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>23</span><br />
                                                <span>28</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>25</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-2 deactive-td"></td>
                                <td class="tg-yw4l column-3 deactive-td"></td>
                                <td class="tg-yw4l column-4">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>6</span><br />
                                                <span>6</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>7</span><br />
                                                <span>7</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>7</span><br />
                                                <span>8</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>8</span><br />
                                                <span>10</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>9</span><br />
                                                <span>11</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>10</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-yw4l">
                                    <div class="first-column">
                                        <div class="row-descs">
                                            <b style="font-size: 13px">2. Başkalarına ziyan verən davranış</b>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Tezlik dərəcəsinin xalları</p>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Ağırlıq dərəcəsinin xalları</p>

                                        </div>
                                        <div class="row-inputs">
                                            <asp:TextBox ID="Txt3" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="1"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="Txt4" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="2"></asp:TextBox>
                                        </div>
                                        <div class="row-ratings">
                                            <b style="font-size: 13px; display: block; margin-bottom: -11px;">Xallar:</b><br />
                                            PS:<br />
                                            PS:
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-1 deactive-td"></td>
                                <td class="tg-yw4l column-2 deactive-td"></td>
                                <td class="tg-yw4l column-3">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>15</span><br />
                                                <span>15</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>17</span><br />
                                                <span>18</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>19</span><br />
                                                <span>21</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>22</span><br />
                                                <span>24</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>24</span><br />
                                                <span>27</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>26</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-4">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>6</span><br />
                                                <span>6</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>7</span><br />
                                                <span>7</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>8</span><br />
                                                <span>9</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>10</span><br />
                                                <span>11</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>11</span><br />
                                                <span>13</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>12</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-yw4l">
                                    <div class="first-column">
                                        <div class="row-descs">
                                            <b style="font-size: 13px">3. Avadanlığa ziyan verən davranış</b>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Tezlik dərəcəsinin xalları</p>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Ağırlıq dərəcəsinin xalları</p>
                                        </div>
                                        <div class="row-inputs">
                                            <asp:TextBox ID="Txt5" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="1"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="Txt6" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="2"></asp:TextBox>
                                        </div>
                                        <div class="row-ratings">
                                            <b style="font-size: 13px; display: block; margin-bottom: -11px;">Xallar:</b><br />
                                            PS:<br />
                                            PS:
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-1 deactive-td"></td>
                                <td class="tg-yw4l column-2 deactive-td"></td>
                                <td class="tg-yw4l column-3">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>15</span><br />
                                                <span>15</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>17</span><br />
                                                <span>18</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>20</span><br />
                                                <span>22</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>23</span><br />
                                                <span>25</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>25</span><br />
                                                <span>29</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>28</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-4">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>6</span><br />
                                                <span>6</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>7</span><br />
                                                <span>8</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>9</span><br />
                                                <span>10</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>10</span><br />
                                                <span>12</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>12</span><br />
                                                <span>14</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>13</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-yw4l">
                                    <div class="first-column">
                                        <div class="row-descs">
                                            <b style="font-size: 13px">4. Dağıdıcı davranış</b>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Tezlik dərəcəsinin xalları</p>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Ağırlıq dərəcəsinin xalları</p>
                                        </div>
                                        <div class="row-inputs">
                                            <asp:TextBox ID="Txt7" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="1"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="Txt8" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="2"></asp:TextBox>
                                        </div>
                                        <div class="row-ratings">
                                            <b style="font-size: 13px; display: block; margin-bottom: -11px;">Xallar:</b><br />
                                            PS:<br />
                                            PS:
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-1 deactive-td"></td>
                                <td class="tg-yw4l column-2 deactive-td"></td>
                                <td class="tg-yw4l column-3">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>15</span><br />
                                                <span>15</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>16</span><br />
                                                <span>17</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>18</span><br />
                                                <span>20</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>19</span><br />
                                                <span>22</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>21</span><br />
                                                <span>25</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>22</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-4">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>6</span><br />
                                                <span>6</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>6</span><br />
                                                <span>7</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>7</span><br />
                                                <span>9</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>8</span><br />
                                                <span>10</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>9</span><br />
                                                <span>12</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>10</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-yw4l">
                                    <div class="first-column">
                                        <div class="row-descs">
                                            <b style="font-size: 13px">5. Qeyri-Adi və ya təkrarlanan vərdişlər</b>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Tezlik dərəcəsinin xalları</p>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Ağırlıq dərəcəsinin xalları</p>
                                        </div>
                                        <div class="row-inputs">
                                            <asp:TextBox ID="Txt9" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="1"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="Txt10" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="2"></asp:TextBox>
                                        </div>
                                        <div class="row-ratings">
                                            <b style="font-size: 13px; display: block; margin-bottom: -11px;">Xallar:</b><br />
                                            PS:<br />
                                            PS:
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-1">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>16</span><br />
                                                <span>16</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>17</span><br />
                                                <span>19</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>18</span><br />
                                                <span>21</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>20</span><br />
                                                <span>24</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>21</span><br />
                                                <span>27</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>22</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-2 deactive-td"></td>
                                <td class="tg-yw4l column-3 deactive-td"></td>
                                <td class="tg-yw4l column-4">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>6</span><br />
                                                <span>6</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>6</span><br />
                                                <span>7</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>6</span><br />
                                                <span>7</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>7</span><br />
                                                <span>8</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>7</span><br />
                                                <span>9</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>8</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-yw4l">
                                    <div class="first-column">
                                        <div class="row-descs">
                                            <b style="font-size: 13px">6. Sosial Təhqiredici Davranış</b>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Tezlik dərəcəsinin xalları</p>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Ağırlıq dərəcəsinin xalları</p>
                                        </div>
                                        <div class="row-inputs">
                                            <asp:TextBox ID="Txt11" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="1"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="Txt12" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="2"></asp:TextBox>
                                        </div>
                                        <div class="row-ratings">
                                            <b style="font-size: 13px; display: block; margin-bottom: -11px;">Xallar:</b><br />
                                            PS:<br />
                                            PS:
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-1 deactive-td"></td>
                                <td class="tg-yw4l column-2">
                                    <div class="matrix-column">
                                        <div class="values 0-index">
                                            <b>0</b><br />
                                            <span>23</span><br />
                                            <span>23</span>
                                        </div>
                                        <div class="values 1-index">
                                            <b>1</b><br />
                                            <span>25</span><br />
                                            <span>26</span>
                                        </div>
                                        <div class="values 2-index">
                                            <b>2</b><br />
                                            <span>27</span><br />
                                            <span>30</span>
                                        </div>
                                        <div class="values 3-index">
                                            <b>3</b><br />
                                            <span>30</span><br />
                                            <span>33</span>
                                        </div>
                                        <div class="values 4-index">
                                            <b>4</b><br />
                                            <span>32</span><br />
                                            <span>36</span>
                                        </div>
                                        <div class="values 5-index">
                                            <b>5</b><br />
                                            <span>34</span><br />
                                            <span>-</span>
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-3 deactive-td"></td>
                                <td class="tg-yw4l column-4">
                                    <div class="matrix-column">
                                        <div class="values 0-index">
                                            <b>0</b><br />
                                            <span>6</span><br />
                                            <span>6</span>
                                        </div>
                                        <div class="values 1-index">
                                            <b>1</b><br />
                                            <span>6</span><br />
                                            <span>7</span>
                                        </div>
                                        <div class="values 2-index">
                                            <b>2</b><br />
                                            <span>7</span><br />
                                            <span>8</span>
                                        </div>
                                        <div class="values 3-index">
                                            <b>3</b><br />
                                            <span>8</span><br />
                                            <span>9</span>
                                        </div>
                                        <div class="values 4-index">
                                            <b>4</b><br />
                                            <span>9</span><br />
                                            <span>10</span>
                                        </div>
                                        <div class="values 5-index">
                                            <b>5</b><br />
                                            <span>9</span><br />
                                            <span>-</span>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-yw4l">
                                    <div class="first-column">
                                        <div class="row-descs">
                                            <b style="font-size: 13px">7. Qeriyə çəkilmə yaxud etinasız davranış</b>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Tezlik dərəcəsinin xalları</p>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Ağırlıq dərəcəsinin xalları</p>
                                        </div>
                                        <div class="row-inputs">
                                            <asp:TextBox ID="Txt13" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="1"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="Txt14" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="2"></asp:TextBox>
                                        </div>
                                        <div class="row-ratings">
                                            <b style="font-size: 13px; display: block; margin-bottom: -11px;">Xallar:</b><br />
                                            PS:<br />
                                            PS:
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-1">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>16</span><br />
                                                <span>16</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>18</span><br />
                                                <span>19</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>20</span><br />
                                                <span>22</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>21</span><br />
                                                <span>25</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>23</span><br />
                                                <span>29</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>25</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-2 deactive-td"></td>
                                <td class="tg-yw4l column-3 deactive-td"></td>
                                <td class="tg-yw4l column-4">
                                    <div class="indexes-matrix">
                                        <div class="matrix-column">
                                            <div class="values 0-index">
                                                <b>0</b><br />
                                                <span>6</span><br />
                                                <span>6</span>
                                            </div>
                                            <div class="values 1-index">
                                                <b>1</b><br />
                                                <span>6</span><br />
                                                <span>7</span>
                                            </div>
                                            <div class="values 2-index">
                                                <b>2</b><br />
                                                <span>7</span><br />
                                                <span>8</span>
                                            </div>
                                            <div class="values 3-index">
                                                <b>3</b><br />
                                                <span>7</span><br />
                                                <span>9</span>
                                            </div>
                                            <div class="values 4-index">
                                                <b>4</b><br />
                                                <span>8</span><br />
                                                <span>10</span>
                                            </div>
                                            <div class="values 5-index">
                                                <b>5</b><br />
                                                <span>8</span><br />
                                                <span>-</span>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-yw4l">
                                    <div class="first-column">
                                        <div class="row-descs">
                                            <b style="font-size: 13px">8. Əməkdaşlıq etməyən davranış</b>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Tezlik dərəcəsinin xalları</p>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px;">Ağırlıq dərəcəsinin xalları</p>
                                        </div>
                                        <div class="row-inputs">
                                            <asp:TextBox ID="Txt15" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="1"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="Txt16" runat="server" class="fr-sv-input selectAll numeric" MaxLength="1" data-row="2"></asp:TextBox>
                                        </div>
                                        <div class="row-ratings">
                                            <b style="font-size: 13px; display: block; margin-bottom: -11px;">Xallar:</b><br />
                                            PS:<br />
                                            PS:
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-1 deactive-td"></td>
                                <td class="tg-yw4l column-2">
                                    <div class="matrix-column">
                                        <div class="values 0-index">
                                            <b>0</b><br />
                                            <span>23</span><br />
                                            <span>23</span>
                                        </div>
                                        <div class="values 1-index">
                                            <b>1</b><br />
                                            <span>26</span><br />
                                            <span>27</span>
                                        </div>
                                        <div class="values 2-index">
                                            <b>2</b><br />
                                            <span>28</span><br />
                                            <span>30</span>
                                        </div>
                                        <div class="values 3-index">
                                            <b>3</b><br />
                                            <span>31</span><br />
                                            <span>34</span>
                                        </div>
                                        <div class="values 4-index">
                                            <b>4</b><br />
                                            <span>33</span><br />
                                            <span>37</span>
                                        </div>
                                        <div class="values 5-index">
                                            <b>5</b><br />
                                            <span>35</span><br />
                                            <span>-</span>
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l column-3 deactive-td"></td>
                                <td class="tg-yw4l column-4">
                                    <div class="matrix-column">
                                        <div class="values 0-index">
                                            <b>0</b><br />
                                            <span>6</span><br />
                                            <span>6</span>
                                        </div>
                                        <div class="values 1-index">
                                            <b>1</b><br />
                                            <span>7</span><br />
                                            <span>7</span>
                                        </div>
                                        <div class="values 2-index">
                                            <b>2</b><br />
                                            <span>8</span><br />
                                            <span>8</span>
                                        </div>
                                        <div class="values 3-index">
                                            <b>3</b><br />
                                            <span>8</span><br />
                                            <span>10</span>
                                        </div>
                                        <div class="values 4-index">
                                            <b>4</b><br />
                                            <span>9</span><br />
                                            <span>11</span>
                                        </div>
                                        <div class="values 5-index">
                                            <b>5</b><br />
                                            <span>10</span><br />
                                            <span>-</span>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-58yr" style="color: white; font-size: 11px; vertical-align: middle; text-align: center">A<br />
                                    d<br />
                                    d<br />
                                    ı<br />
                                    m<br />
                                    <br />
                                    3</td>
                                <td class="tg-yw4l">
                                    <div class="first-column">
                                        <div class="row-descs">
                                            <p style="font-size: 12px; margin: 0px;">Yaş üçün Xallar Bölməsi İllə</p>
                                            <p style="font-size: 12px; margin: 0px; padding-left: 30px; margin-top: 35px;">Fərdi yaşı</p>
                                        </div>
                                        <div class="row-inputs" style="padding-top: 52px;">
                                            <asp:TextBox ID="TxtIndividualAge" runat="server" class="fr-sv-input selectAll numeric" MaxLength="2" data-type="age"></asp:TextBox>
                                        </div>
                                        <div class="row-ratings">
                                            Yaş:<br />
                                            PS:<br />
                                            <br />
                                            Yaş:<br />
                                            PS:
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l age-td column-1">
                                    <div class="matrix-column">
                                        <div class="values" data-list="1,2,3,4,5,6,7,8">
                                            <b>1-8</b><br />
                                            <span>0</span><br />
                                        </div>
                                        <div class="values 9-15-index" data-list="9,10,11,12,13,14,15">
                                            <b>9-15</b><br />
                                            <span>1</span><br />
                                        </div>
                                        <div class="values max-index" data-list="16,17">
                                            <b>16+</b><br />
                                            <span>2</span><br />
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l age-td column-2">
                                    <div class="matrix-column">
                                        <div class="values 1-7-index" data-list="1,2,3,4,5,6,7">
                                            <b>1-7</b><br />
                                            <span>0</span><br />
                                        </div>
                                        <div class="values 8-10-index" data-list="8,9,10">
                                            <b>8-10</b><br />
                                            <span>1</span><br />
                                        </div>
                                        <div class="values 11-12-index" data-list="11,12">
                                            <b>11-12</b><br />
                                            <span>2</span><br />
                                        </div>
                                        <div class="values 13-15-index" data-list="13,14,15">
                                            <b>13-15</b><br />
                                            <span>3</span><br />
                                        </div>
                                    </div>
                                    <div class="matrix-column" style="margin-top: 10px;">
                                        <div class="values 16-18-index" data-list="16,17,18">
                                            <b>16-18</b><br />
                                            <span>4</span><br />
                                        </div>
                                        <div class="values 19-21-index" data-list="19,20,21">
                                            <b>19-21</b><br />
                                            <span>5</span><br />
                                        </div>
                                        <div class="values 22+-index max-index" data-list="22,23">
                                            <b>22+</b><br />
                                            <span>6</span><br />
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l age-td column-3">
                                    <div class="matrix-column">
                                        <div class="values 1-6-index" data-list="1,2,3,4,5,6">
                                            <b>1-6</b><br />
                                            <span>0</span><br />
                                        </div>
                                        <div class="values 7-10-index" data-list="7,8,9,10">
                                            <b>7-10</b><br />
                                            <span>1</span><br />
                                        </div>
                                        <div class="values 11-index" data-list="11">
                                            <b>11</b><br />
                                            <span>2</span><br />
                                        </div>
                                        <div class="values 12-13-index" data-list="12,13">
                                            <b>12-13</b><br />
                                            <span>3</span><br />
                                        </div>
                                    </div>
                                    <div class="matrix-column" style="margin-top: 10px;">
                                        <div class="values 14-15-index" data-list="14,15">
                                            <b>14-15</b><br />
                                            <span>4</span><br />
                                        </div>
                                        <div class="values 16-index" data-list="16">
                                            <b>16</b><br />
                                            <span>5</span><br />
                                        </div>
                                        <div class="values 17-18-index" data-list="17,18">
                                            <b>17-18</b><br />
                                            <span>6</span><br />
                                        </div>
                                        <div class="values 19+-index max-index" data-list="19,20">
                                            <b>19+</b><br />
                                            <span>7</span><br />
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l age-td column-4">
                                    <div class="matrix-column">
                                        <div class="values 1-7-index" data-list="1,2,3,4,5,6,7">
                                            <b>1-7</b><br />
                                            <span>0</span><br />
                                        </div>
                                        <div class="values 8-11-index" data-list="8,9,10,11">
                                            <b>8-11</b><br />
                                            <span>1</span><br />
                                        </div>
                                        <div class="values 12-13-index" data-list="12,13">
                                            <b>12-13</b><br />
                                            <span>2</span><br />
                                        </div>
                                        <div class="values 14-index" data-list="14">
                                            <b>14</b><br />
                                            <span>3</span><br />
                                        </div>
                                        <div class="values 15+-index max-index" data-list="15,16">
                                            <b>15+</b><br />
                                            <span>4</span><br />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-llf7" style="color: white; font-size: 11px; vertical-align: middle; text-align: center">A<br />
                                    d<br />
                                    d<br />
                                    ı<br />
                                    m<br />
                                    <br />
                                    4</td>
                                <td class="tg-yw4l" style="vertical-align: middle;">Xallar Bölməsinin Cəmi</td>
                                <td class="tg-yw4l sum-score-td" data-col="1">
                                    <div class="holder">
                                        <p style="margin: 0px;">100</p>
                                        <div class="flex-holder">
                                            <div>-</div>
                                            <div class="sum-span" data-col="1">0</div>
                                            <div>CƏM</div>
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l sum-score-td" data-col="2">
                                    <div class="holder">
                                        <p style="margin: 0px;">100</p>
                                        <div class="flex-holder">
                                            <div>-</div>
                                            <div class="sum-span" data-col="2">0</div>
                                            <div>CƏM</div>
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l sum-score-td" data-col="3">
                                    <div class="holder">
                                        <p style="margin: 0px;">100</p>
                                        <div class="flex-holder">
                                            <div>-</div>
                                            <div class="sum-span" data-col="3">0</div>
                                            <div>CƏM</div>
                                        </div>
                                    </div>
                                </td>
                                <td class="tg-yw4l sum-score-td" data-col="4">
                                    <div class="holder">
                                        <p style="margin: 0px;">100</p>
                                        <div class="flex-holder">
                                            <div>-</div>
                                            <div class="sum-span" data-col="4">0</div>
                                            <div>CƏM</div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tg-z4t5" style="color: white; font-size: 11px; vertical-align: middle; text-align: center">A<br />
                                    d<br />
                                    d<br />
                                    ı<br />
                                    m<br />
                                    <br />
                                    5</td>
                                <td class="tg-yw4l" style="vertical-align: middle;">Uyğunlaşmayan davranış indeksi</td>
                                <td class="tg-yw4l final-result-td">
                                    <div class="final-result-holder" data-column="1">
                                        <div class="final-blue-box final-blue-box-1">
                                            <asp:Literal ID="LtrInternalized" runat="server">100</asp:Literal>
                                        </div>
                                        <asp:HiddenField ID="HdnFinalBox1" ClientIDMode="Static" runat="server" />
                                        <div>+ or -</div>
                                    </div>
                                    <p style="margin: 0px; margin-top: 8px; font-size: 12px;">Uyğunlaşmayan davranış indeksi</p>
                                </td>
                                <td class="tg-yw4l final-result-td">
                                    <div class="final-result-holder" data-column="2">
                                        <div class="final-blue-box final-blue-box-2">
                                            <asp:Literal ID="LtrAsocial" runat="server">100</asp:Literal>
                                        </div>
                                        <asp:HiddenField ID="HdnFinalBox2" ClientIDMode="Static" runat="server" />
                                        <div>+ or -</div>
                                    </div>
                                    <p style="margin: 0px; margin-top: 8px; font-size: 12px;">Asosial uyğunlaşmama indeksi</p>
                                </td>
                                <td class="tg-yw4l final-result-td">
                                    <div class="final-result-holder" data-column="3">
                                        <div class="final-blue-box final-blue-box-3">
                                            <asp:Literal ID="LtrExternalized" runat="server">100</asp:Literal>

                                        </div>
                                        <asp:HiddenField ID="HdnFinalBox3" ClientIDMode="Static" runat="server" />
                                        <div>+ or -</div>
                                    </div>
                                    <p style="margin: 0px; margin-top: 8px; font-size: 12px;">Xarici uyğunlaşmama indeksi</p>
                                </td>
                                <td class="tg-yw4l final-result-td">
                                    <div class="final-result-holder" data-column="4">
                                        <div class="final-blue-box final-blue-box-4">
                                            <asp:Literal ID="LtrGeneral" runat="server">100</asp:Literal>
                                        </div>
                                        <asp:HiddenField ID="HdnFinalBox4" ClientIDMode="Static" runat="server" />
                                        <div>+ or -</div>
                                    </div>
                                    <p style="margin: 0px; margin-top: 8px; font-size: 12px;">Ümumi uyğunlaşmama indeksi</p>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>

        </asp:View>

    </asp:MultiView>

    <div class="row" id="buttonFinish">
        <div class="col-md-7">
            <asp:Button ID="BtnPrevious" runat="server" Text="Əvvəlki" CssClass="btn-default nextButton" OnClick="BtnPrevious_Click" />
            <asp:Button ID="BtnNext" runat="server" Text="Növbəti" CssClass="btn-default nextButton" OnClick="BtnNext_Click" />
        </div>
    </div>

    <script type="text/javascript" lang="js">

        $(document).ready(function () {
            calculationAge();
            calculationAdaptive();
            isNumeric();

            $(".selectAll").focus(function () { $(this).select(); });
            checkAllFields();

        });

        function isNumeric() {

            $(".numeric").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });

            $(".numericNegative").keypress(function (e) {

                //reqemden qabaq menfi yaza bilsin ,45 menfi (-) yazmaq uchundu  
                if ($(this).val().length > 0 && e.which == 45) {
                    return false;
                }

                if (e.which != 45 && e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        }

        //Yashin hesablanmasi
        function calculationAge() {

            var TestYear = parseInt($("#TxtTestingDateYear").val());
            var TestMonth = parseInt($("#TxtTestingDateMonth").val());
            var TestDay = parseInt($("#TxtTestingDateDay").val());

            var BirthYear = parseInt($("#TxtBirthDateYear").val());
            var BirthMonth = parseInt($("#TxtBirthDateMonth").val());
            var BirthDay = parseInt($("#TxtBirthDateDay").val());

            if (TestDay < BirthDay) {
                TestDay = TestDay + 30;
                TestMonth = TestMonth - 1;
            }

            if (TestMonth < BirthMonth) {
                TestMonth = TestMonth + 12;
                TestYear = TestYear - 1;
            }

            var differenceDay = TestDay - BirthDay;
            var differenceMonth = TestMonth - BirthMonth;
            var differenceYear = TestYear - BirthYear;

            if (differenceDay < 0 || isNaN(differenceDay)) {
                differenceDay = 0;
            }

            if (differenceMonth < 0 || isNaN(differenceMonth)) {
                differenceMonth = 0;
            }

            if (differenceYear < 0 || isNaN(differenceYear)) {
                differenceYear = 0;
            }

            $("#TxtDifferenceDay").val(differenceDay);
            $("#TxtDifferenceMonth").val(differenceMonth);
            $("#TxtDifferenceYear").val(differenceYear);

            $("#TxtAgeYear").val($("#TxtDifferenceYear").val());
            $("#TxtAgeMonth").val(parseInt($("#TxtDifferenceMonth").val()) + (parseInt($("#TxtDifferenceDay").val()) >= 15 ? 1 : 0));

            if (parseInt($("#TxtAgeMonth").val()) == 12) {
                $("#TxtAgeYear").val(parseInt($("#TxtDifferenceYear").val()) + 1);
                $("#TxtAgeMonth").val(0);
            }

            $("#HdnAgeYear").val($("#TxtAgeYear").val());
            $("#HdnAgeMonth").val($("#TxtAgeMonth").val());

        }

        //Adaptive Behavior
        $(function () {

            $(".inputSumA").keyup(function () {
                var rowId = $(this).attr('data-rowId');
                $(".outputSumA[data-rowId='" + rowId + "']").html($(this).val());
                calculateRawScore(rowId)
                calculationAdaptive();
            });

            $(".inputSumB").keyup(function () {
                var rowId = $(this).attr('data-rowId');
                $(".outputSumB[data-rowId='" + rowId + "']").html(parseInt($(this).val()) * 2);
                calculateRawScore(rowId)
                calculationAdaptive();
            });

            $(".inputSumC").keyup(function () {
                var rowId = $(this).attr('data-rowId');
                $(".outputSumC[data-rowId='" + rowId + "']").html(parseInt($(this).val()) * 3);
                calculateRawScore(rowId)
                calculationAdaptive();
            });


        });

        function calculateRawScore(rowId) {

            //Full Scale
            var a = parseInt($(".outputSumA[data-rowId='" + rowId + "']").html());
            var b = parseInt($(".outputSumB[data-rowId='" + rowId + "']").html());
            var c = parseInt($(".outputSumC[data-rowId='" + rowId + "']").html());

            //Early Development Form
            var rawSocore1 = parseInt($(".sumRawScore[data-rowId=1]").html());
            var rawSocore2 = parseInt($(".sumRawScore[data-rowId=2]").html());


            if (isNaN(a)) {
                a = 0;
            }
            if (isNaN(b)) {
                b = 0;
            }
            if (isNaN(c)) {
                c = 0;
            }

            var rawScore = (a + b + c);

            //Full Scale
            $(".sumRawScore[data-rowId='" + rowId + "']").html(rawScore);

            //Early Development Form
            $(".sumTotalRawScore[data-rowId=3]").html(rawSocore1 + rawSocore2);

            //Full Scale
            $("#HdnRawScore" + rowId + "").val(rawScore);

            //Early Development Form
            $("#HdnRawScore15").val(rawSocore1 + rawSocore2);
        }

        function calculationAdaptive() {
            for (var i = 1; i <= 14; i++) {
                $(".outputSumA[data-rowId='" + i + "']").html($(".inputSumA[data-rowId='" + i + "']").val());
                $(".outputSumB[data-rowId='" + i + "']").html(parseInt($(".inputSumB[data-rowId='" + i + "']").val()) * 2);
                $(".outputSumC[data-rowId='" + i + "']").html(parseInt($(".inputSumC[data-rowId='" + i + "']").val()) * 3);
                calculateRawScore(i);
            }
        }

        //Problm Behavior
        function isInArray(value, array) {
            return array.indexOf(value) > -1;
        }
        function checkAllFields() {
            $(".fr-sv-input").each(function () {
                if ($(this).val()) {
                    if ($(this).attr('data-type') == "age") {
                        $(this).parent().parent().parent().parent().find(".values span").removeClass("matrix-active-number");
                        var age = $(this).val().toString();

                        var checkArr = [];
                        $(this).parent().parent().parent().parent().find(".age-td").each(function () {
                            var notFinded = true;
                            $(this).find(".values").each(function () {
                                checkArr = [];
                                var list = $(this).attr("data-list");
                                checkArr = list.split(",");
                                if (isInArray(age, checkArr)) {
                                    $(this).find("span").addClass("matrix-active-number");
                                    notFinded = false;
                                }
                            })
                            if (notFinded) {
                                $(this).find(".max-index span").addClass("matrix-active-number");
                            }

                        });

                    } else {
                        var value = $(this).val();
                        var nthType = $(this).attr("data-row");
                        $(this).parent().parent().parent().parent().find("span:nth-of-type(" + nthType + ")").removeClass("matrix-active-number");
                        $(this).parent().parent().parent().parent().find("." + value + "-index span:nth-of-type(" + nthType + ")").addClass("matrix-active-number");
                    }
                }
            });
            calculateFinal();
        }
        $(".fr-sv-input").on("keyup", function () {
            checkAllFields();
            if ($(this).attr('data-type') == "age") {
                $(this).parent().parent().parent().parent().find(".values span").removeClass("matrix-active-number");
                var age = $(this).val().toString();
                var checkArr = [];
                $(this).parent().parent().parent().parent().find(".age-td").each(function () {
                    var notFinded = true;
                    $(this).find(".values").each(function () {
                        checkArr = [];
                        var list = $(this).attr("data-list");
                        checkArr = list.split(",");
                        if (isInArray(age, checkArr)) {
                            $(this).find("span").addClass("matrix-active-number");
                            notFinded = false;
                        }
                    })
                    if (notFinded) {
                        $(this).find(".max-index span").addClass("matrix-active-number");
                    }

                });
            } else {
                var value = $(this).val();
                var nthType = $(this).attr("data-row");
                $(this).parent().parent().parent().parent().find("span:nth-of-type(" + nthType + ")").removeClass("matrix-active-number");
                $(this).parent().parent().parent().parent().find("." + value + "-index span:nth-of-type(" + nthType + ")").addClass("matrix-active-number");
            }
            calculateFinal();
        })

        function calculateFinal() {
            $(".sum-span").each(function () {
                var lastSum = 0;
                var columnId = $(this).attr("data-col");
                $(".column-" + columnId + " .matrix-active-number").each(function () {
                    if ($(this).text() == "-") {
                        lastSum += 0;
                    } else {
                        lastSum += parseInt($(this).text());
                    }

                });
                $(this).text(lastSum);
                var final = 100 - parseInt(lastSum);
                $(".final-blue-box-" + columnId).text(final);
                $("#HdnFinalBox" + columnId).val(final);

            })
        }

    </script>

</asp:Content>

