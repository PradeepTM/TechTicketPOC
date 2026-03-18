using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechTicketPOC.BLL;
using TechTicketPOC.Common.Extensions;
using TechTicketPOC.Entities;
using static TechTicketPOC.Entities.Constants.FieldDataType;
using static TechTicketPOC.AppCode.Constants.ControlTypes;
using static TechTicketPOC.Common.Constants.SessionKeys;
using static TechTicketPOC.Common.SessionWrapper;

namespace TechTicketPOC.UserControls
{
    public partial class EmailTicket : System.Web.UI.UserControl
    {

        #region Properties

        public int? DivisionId
        {
            get
            {
                var selectedDivision = X.GetCmp<ComboBox>(nameof(cboxDivision)).Value;

                if (selectedDivision != null && int.TryParse(selectedDivision.ToString(), out int divisionId))
                    return divisionId;
                else
                    return null;

            }
        }

        public int? RequestId
        {
            get
            {
                var selectedDivision = X.GetCmp<ComboBox>(nameof(cboxRequest)).Value;

                if (selectedDivision != null && int.TryParse(selectedDivision.ToString(), out int divisionId))
                    return divisionId;
                else
                    return null;

            }

        }

        public string RequestName
        {
            get
            {
                return X.GetCmp<ComboBox>(nameof(cboxRequest)).SelectedItem.Text;
            }

        }

        #endregion

        #region Event handlers

        protected override void OnInit(EventArgs e)
        {
            //if (!IsPostBack && !X.IsAjaxRequest)
            //    GenerateEmailTemplate(33);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack && !X.IsAjaxRequest)
            {
                LoadMasterData();
                //GenerateEmailTemplate(33);
            }

        }

        protected void OnEmail(object sender, DirectEventArgs args)
        {

            if (RequestId.HasValue)
            {
                var requestId = RequestId.Value;
                var emailTemplate = Get<EmailTemplateDTO>(string.Format(EMAIL_TEMPLATE_BY_REQUEST, requestId));
                var fieldValues = GetData(emailTemplate);
                DisplayEmailPreview(fieldValues, emailTemplate);
            }

        }

        protected void OnDivisionSelected(object sender, DirectEventArgs e)
        {

            if (DivisionId.HasValue)
            {
                var requests = new RequestBLL().GetRequests(DivisionId.Value);
                strRequests.DataSource = requests;
                strRequests.DataBind();
            }

        }

        protected void OnRequestSelected(object sender, DirectEventArgs e)
        {
            if (RequestId.HasValue)
            {
                GenerateEmailTemplate(RequestId.Value);
            }

        }

        #endregion

        #region Private helper methods

        private void LoadMasterData()
        {
            strDivsion.DataSource = new DivisionBLL().GetDivisions();
            strDivsion.DataBind();
        }

        private void DisplayEmailPreview(Dictionary<string, string> fieldValues, EmailTemplateDTO emailTemplate)
        {
            if (fieldValues == null || fieldValues.Count == 0)
                return;

            var subject = GetEmailSubject(emailTemplate);
            var previewHtml = new EmailTemplateBLL().BuildEmailPreviewHtml(emailTemplate, subject, fieldValues);

            pnlEmailPreview.Html = previewHtml;
            pnlEmailPreview.Update();
            wndEmailPreview.Show();
        }

        private void GenerateEmailTemplate(int requestId)
        {
            var emailTemplate = new EmailTemplateBLL().GetEmailTemplate(requestId);

            if (emailTemplate != null)
                Set(string.Format(EMAIL_TEMPLATE_BY_REQUEST, requestId), emailTemplate);

            GenerateFormControls(requestId);
            AssignEventHandlers();
        }

        private void AssignEventHandlers()
        {
        }

        private void GenerateFormControls(int requestId)
        {
            ClearControls();

            var emailTemplate = Get<EmailTemplateDTO>(string.Format(EMAIL_TEMPLATE_BY_REQUEST, RequestId));

            if (!(emailTemplate != null && emailTemplate.Fields != null && emailTemplate.Fields.Count > 0))
                return;

            var leftCtrls = emailTemplate.Fields
                                         .Where((field, index) => index % 2 == 0)
                                         .ToList();

            var rightCtrls = emailTemplate.Fields
                                         .Where((field, index) => index % 2 != 0)
                                         .ToList();

            AddControlsToPanel(pnlLeft, leftCtrls);
            AddControlsToPanel(pnlRight, rightCtrls);
            pnlFields.Show();
        }

        private void ClearControls()
        {
            pnlLeft.ContentControls.Clear();
            pnlRight.ContentControls.Clear();
            pnlFields.Hide();
            pnlLeft.UpdateContent();
            pnlRight.UpdateContent();
        }

        private void AddControlsToPanel(Ext.Net.Panel panel, List<EmailTemplateFieldDTO> fields)
        {

            foreach (var field in fields)
            {
                var control = GetControl(field);

                if (control != null)
                    panel.ContentControls.Add(control);

            }

            panel.UpdateContent();
        }

        private Control GetControl(EmailTemplateFieldDTO field)
        {

            switch (field.FieldType)
            {
                case TEXT_BOX:
                    if (field.DataType.Equals(INT, StringComparison.InvariantCultureIgnoreCase))
                    {
                        var textField = new NumberField()
                        {
                            ID = field.FieldName,
                            FieldLabel = field.DisplayName,
                            TabIndex = (short)field.FieldOrder,
                            AllowBlank = field.IsAllowBlank,
                            Text = field.DefaultValue
                        };

                        if (!string.IsNullOrEmpty(field.FormatRegEx))
                            textField.Regex = textField.MaskRe = field.FormatRegEx;

                        return textField;
                    }
                    else
                    {
                        var textField = new TextField()
                        {
                            ID = field.FieldName,
                            FieldLabel = field.DisplayName,
                            TabIndex = (short)field.FieldOrder,
                            AllowBlank = field.IsAllowBlank,
                            Text = field.DefaultValue
                        };

                        if (!string.IsNullOrEmpty(field.FormatRegEx))
                            textField.Regex = textField.MaskRe = field.FormatRegEx;

                        return textField;
                    }

                case DROP_DOWN:
                    var comboBox = new ComboBox()
                    {
                        ID = field.FieldName,
                        FieldLabel = field.DisplayName,
                        Text = $"Select {field.DisplayName}",
                        Editable = false,
                        TabIndex = (short)field.FieldOrder,
                        AllowBlank = field.IsAllowBlank
                    };

                    if (field.FieldOptions != null && field.FieldOptions.Count > 0)
                    {
                        field.FieldOptions.ForEach((fo) => comboBox.Items.Add(new Ext.Net.ListItem() { Text = fo.DisplayName, Value = fo.Value }));
                    }

                    return comboBox;

                case CHECK_BOX:
                    var checkbox = new Checkbox()
                    {
                        ID = field.FieldName,
                        FieldLabel = field.DisplayName,
                        TabIndex = (short)field.FieldOrder,
                    };

                    return checkbox;

                default:
                    return null;

            }

        }

        /// <summary>
        /// Collects user-entered values from the dynamic form controls.
        /// Returns a dictionary keyed by FieldName (matching {{FieldName}} placeholders
        /// in the stored email template body).
        /// </summary>
        private Dictionary<string, string> GetData(EmailTemplateDTO emailTemplate)
        {
            if (emailTemplate.IsNull() || !emailTemplate.Fields.IsCollectionValid())
                return new Dictionary<string, string>();

            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var field in emailTemplate.Fields)
            {
                string value = string.Empty;

                if (field.FieldType == TEXT_BOX)
                {
                    value = X.GetCmp<TextField>(field.FieldName).Text ?? string.Empty;
                }
                else if (field.FieldType == DROP_DOWN)
                {
                    var selected = X.GetCmp<ComboBox>(field.FieldName).SelectedItem.Value;
                    if (!string.IsNullOrEmpty(selected) &&
                        !selected.StartsWith("Select ", StringComparison.InvariantCultureIgnoreCase))
                        value = selected;
                }
                else if (field.FieldType == CHECK_BOX)
                {
                    value = X.GetCmp<Checkbox>(field.FieldName).Checked ? "Yes" : "No";
                }

                data[field.FieldName] = value;
            }

            data["Attachment"] = fuAttachments.HasFile ? fuAttachments.FileName : string.Empty;

            return data;
        }

        private string GetEmailSubject(EmailTemplateDTO emailTemplate)
        {
            var userId = "PTM";

            if (emailTemplate.Fields.IsCollectionValid() &&
                emailTemplate.Fields.Exists(ef => ef.FieldName.Equals("ClaimNumber", StringComparison.InvariantCultureIgnoreCase)))
                return $"{RequestName} {X.GetCmp<TextField>("ClaimNumber").Text} {userId}";
            else
                return $"{RequestName} {userId}";

        }

        #endregion
    }

}