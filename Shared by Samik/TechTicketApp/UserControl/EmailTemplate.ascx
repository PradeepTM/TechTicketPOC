<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailTemplate.ascx.cs" Inherits="TechTicketApp.UserControl.EmailTemplate" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>


<%@ Assembly Name="TechTicketDB" %>



<script runat="server">

    Ext.Net.Button b;
    Ext.Net.TextField c;
    Ext.Net.ComboBox drp;
    Ext.Net.Checkbox cb;
    TechTicketDB.ViewModelDTO vm;

    protected override void OnInit(EventArgs e)
    {
        List<TechTicketDB.division> divisionList = TechTicketDB.Repository.GetAllDivision();
        this.drpDivisionStore.DataSource = divisionList;
        this.drpDivisionStore.DataBind();




        //c = new Ext.Net.TextField();
        //this.Panel1.Controls.Add(c);
    }


    protected void getValue(object sender, DirectEventArgs e)
    {
        vm = TechTicketDB.Repository.GetAllFieldAndTemplateByRequestId(Convert.ToInt32(this.drpRequest.SelectedItem.Value));

        string msg = "You have Filled Form by : ";

        if (vm.fields != null && vm.fields.Count > 0)
        {
            foreach (var control in vm.fields)
            {
                if (control.field_type == "TEXTBOX")
                {
                    msg = msg + control.display_name + " : " + X.GetCmp<TextField>(control.field_name).Text;
                }
                if (control.field_type == "CHECKBOX")
                {
                    msg = msg + control.display_name + " : " + X.GetCmp<Checkbox>(control.field_name).Checked;
                }
                if (control.field_type == "COMBOBOX")
                {
                    msg = msg + control.display_name + " : " + X.GetCmp<ComboBox>(control.field_name).SelectedItem.Value;
                }
                msg = msg + "||";
            }
        }

        X.Msg.Alert("value", msg).Show();
    }

    protected void drpDivision_Select(object sender, DirectEventArgs e)
    {

        //X.Msg.Alert("drpDivision_Select", "Selected index: " + this.drpDivision.SelectedItem.Value).Show();

        List<TechTicketDB.request> divisionList = TechTicketDB.Repository.GetAllRequestByDivisionId(Convert.ToInt32(this.drpDivision.SelectedItem.Value));
        this.drpRequestStore.DataSource = divisionList;
        this.drpRequestStore.DataBind();
    }
    protected void drpRequest_Select(object sender, DirectEventArgs e)
    {

        //X.Msg.Alert("drpRequest_Select", "Selected index: " + this.drpRequest.SelectedItem.Value).Show();


        //** For Refresh UI Control **//

        //this.Panel1.Controls.Clear();
        //this.Panel1.update();
        //this.Panel1.();
        //this.Panel1.Destroy();
        //this.Panel1.RemoveAll();
        //this.Panel1.Dispose();
        //this.Panel1.Items.Clear();
        //this.Panel1.DestroyContent = true;
        //this.Panel1.LayoutConfig.Clear();


        ////this.Panel1.Controls.Clear()


        //this.Panel1.RemoveAll(true);

        vm = TechTicketDB.Repository.GetAllFieldAndTemplateByRequestId(Convert.ToInt32(this.drpRequest.SelectedItem.Value));
        if (vm.fields != null && vm.fields.Count > 0)
        {
            foreach (var control in vm.fields)
            {
                if (control.field_type == "TEXTBOX")
                {
                    c = new Ext.Net.TextField();
                    c.Text = control.default_value;
                    c.ID = control.field_name;
                    c.AllowBlank = true;

                    c.FieldLabel = control.display_name;
                    this.Panel1.Controls.Add(c);
                }
                if (control.field_type == "CHECKBOX")
                {
                    cb = new Ext.Net.Checkbox();
                    cb.Checked = Convert.ToBoolean(control.default_value);
                    cb.ID = control.field_name;
                    cb.FieldLabel = control.display_name;
                    this.Panel1.Controls.Add(cb);
                }
                if (control.field_type == "COMBOBOX")
                {
                    List<OptionValue> drpOption = new List<OptionValue>();
                    drpOption.Add(new OptionValue { Key = "1", Value = "Option 1" });

                    drpOption.Add(new OptionValue { Key = "2", Value = "Option 2" });

                    drpOption.Add(new OptionValue { Key = "3", Value = "Option 3" });

                    Store storeItem = new Store();
                    storeItem.ID = "drpPIP";
                    storeItem.Fields.Add("Key");
                    storeItem.Fields.Add("Value");

                    storeItem.DataSource = drpOption;
                    storeItem.DataBind();

                    drp = new Ext.Net.ComboBox();
                    drp.ID = control.field_name;
                    drp.FieldLabel = control.display_name;
                    drp.DisplayField = "Value";
                    drp.ValueField = "Key";
                    drp.Store.Add(storeItem);
                    drp.DataBind();


                    this.Panel1.Controls.Add(drp);
                }


            }

            //b = new Ext.Net.Button("Send Mail");
            //b.ID = "btnSendMail";
            //b.DirectEvents.Click.Event += btnClick;
            //this.Panel1.Controls.Add(b);

        }


        this.Panel1.Render();


    }

    public class OptionValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    protected void SaveData(object sender, DirectEventArgs e)
    {
        ///Your server side code for data saving goes here
    }


</script>

<ext:XScript ID="XScript1" runat="server">
<script type="text/javascript">


    var removeTool = function (panel, toolId) {
        alert("ok");



    }
    </script>
</ext:XScript>


<ext:ResourceManager runat="server" IDMode="Explicit" />
<ext:FormPanel
    ID="FormPanel1"
    runat="server"
    Title="Dynamic Email Template"
    MonitorPoll="500"
    MonitorValid="true"
    Padding="5"
    Width="800"
    Height="600"
    ButtonAlign="Right"
    Layout="Column">
    <Items>
        <ext:Panel runat="server" Border="false" IDMode="Explicit" Padding="10" Header="false">

            <Items>

                <ext:ComboBox
                    ID="drpDivision"
                    runat="server"
                    ValueField="Id"
                    DisplayField="DivisionName"
                    FieldLabel="Select Division"
                    LabelWidth="50"
                    Width="250"
                    AutoPostBackEvent="Select">
                    <Store>
                        <ext:Store ID="drpDivisionStore" runat="server">
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="Id" />
                                        <ext:ModelField Name="DivisionName" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <DirectEvents>
                        <Select OnEvent="drpDivision_Select"></Select>
                    </DirectEvents>
                </ext:ComboBox>
            </Items>
        </ext:Panel>
        <ext:Panel runat="server" IDMode="Explicit" Padding="10" Border="false" Header="false">

            <Items>
                <ext:ComboBox
                    ID="drpRequest"
                    runat="server"
                    ValueField="Id"
                    DisplayField="request_name"
                    FieldLabel="Select Request:"
                    LabelWidth="50"
                    Width="250">
                    <Store>
                        <ext:Store ID="drpRequestStore" runat="server">
                            <Model>
                                <ext:Model runat="server">
                                    <Fields>
                                        <ext:ModelField Name="Id" />
                                        <ext:ModelField Name="request_name" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <DirectEvents>
                        <Select OnEvent="drpRequest_Select"></Select>
                    </DirectEvents>
                </ext:ComboBox>
            </Items>
        </ext:Panel>

        <ext:Panel ID="Panel1" IDMode="Static" runat="server" ButtonAlign="Left">
            <%-- <Listeners>
        <BeforeRender Handler="removeTool(#{Panel1}, 'minimize');" />

    </Listeners>--%>
        </ext:Panel>
    </Items>
    <Buttons>
        <ext:Button UI="Primary" ID="btnSentMail" Text="Send Mail" runat="server" Icon="Disk">
           <%-- <DirectEvents>
                <Click OnEvent="getValue"></Click>
            </DirectEvents>--%>
            <Listeners>
                        <Click Handler="if (#{FormPanel1}.getForm().isValid()) {Ext.Msg.alert('Submit', 'Saved!');}else{Ext.Msg.show({icon: Ext.MessageBox.ERROR, msg: 'FormPanel is incorrect', buttons:Ext.Msg.OK});}" />
              </Listeners>
             <DirectEvents>
                        <Click 
                            Before="return #{FormPanel1}.getForm().isValid();" 
                            Success="Ext.Msg.alert('WOW!', 'Saved');"
                            OnEvent="SaveData"/>
             </DirectEvents>
            <%-- <Listeners>
                             <Click Fn="updateUnavailableDates" />
                         </Listeners>--%>
        </ext:Button>
    </Buttons>

</ext:FormPanel>
