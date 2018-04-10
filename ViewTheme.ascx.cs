/*
' Copyright (c) 2018 Department of Beaches and Harbors
' All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.UI;

using DotNetNuke;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
// using DotNetNuke.UI.Utilities;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Controllers;
using DotNetNuke.UI.Skins.Controls;
using DotNetNuke.Services.Log.EventLog;
using DotNetNuke.UI.Skins;
using DBH.BootstrapTheme.Components;
using System.Web.UI.HtmlControls;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Framework;
using System.Text;

namespace DBH.BootstrapTheme
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from PortalModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class ViewTheme : PortalModuleBase, IActionable
    {

        #region Private Members

        private string EditTheme = "EditTheme";

        #endregion

        #region Event Handlers

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Init runs when the control is initialised
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                //Only required if the HTML content uses jQuery or the Services Framework
                JavaScript.RequestRegistration(CommonJs.jQuery);
                ServicesFramework.Instance.RequestAjaxScriptSupport();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// OnLoad runs when this program start is clicked
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                if (!Page.IsPostBack)
                {
                    // Populate drop down list, radio button list
                    FeatureController.LoadThemes(ModuleId, optTheme);
                    if (Settings.Contains("Theme"))
                        optTheme.SelectedIndex = optTheme.Items.IndexOf(optTheme.Items.FindByText(Settings["Theme"].ToString()));
                }
                LoadTheme();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }

        }

        #endregion

        #region "Privated Interfaces"

        protected void optTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FeatureController.LoadModuleTemplates(ModuleId, cboTemplate, optTheme, lblDescription);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadTheme redirect to add new record dialog when the add new record button is clicked
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected void LoadTheme()
        {
            //Load the HTML file
            DotNetNuke.Entities.Modules.ModuleController modCtrl = new ModuleController();
            var HtmlPathFile = @HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName.Replace("\\", "/") + optTheme.SelectedValue + ".html";


            var BootstrapPath = @HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName.Replace("\\","/") + "/Templates/" + optTheme.SelectedValue + "/index.html";

            var content = Null.NullString;
            if (File.Exists(HtmlPathFile))
            {
                TextReader tr = new StreamReader(HtmlPathFile);
                content = tr.ReadToEnd();
                tr.Close();
            }
            else
            {
                // Create the file.
                using (FileStream fs = File.Create(HtmlPathFile))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(GenerateHTML());
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                TextReader tr = new StreamReader(HtmlPathFile);
                content = tr.ReadToEnd();
                tr.Close();
            }
            ctlContent.Text = content;

        }

        #endregion

        #region html generator


        protected string GenerateHTML()
        {
            // Initialize StringWriter instance.
            StringWriter stringWriter = new StringWriter();
            ModuleController modCtrl = new ModuleController();

            // Put HtmlTextWriter in using block because it needs to call Dispose.
            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                // ref https://www.dotnetperls.com/htmltextwriter
                // Some strings for the attributes.
                string idValue = "content";
                string classValue = "embed-responsive embed-responsive-16by9";
                string classValue_2 = "embed-responsive-item";

                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/") + HttpContext.Current.Request.ApplicationPath;
                // Setup link to open a html file
                string urlValue = strUrl + "/DesktopModules/" + modCtrl.GetModule(ModuleId).DesktopModule.FolderName + "/Templates/" + optTheme.SelectedValue + "/index.html";

                // The important part:
                writer.AddAttribute(HtmlTextWriterAttribute.Id, idValue);
                writer.RenderBeginTag(HtmlTextWriterTag.Div); // Begin #1

                writer.AddAttribute(HtmlTextWriterAttribute.Class, classValue);
                writer.RenderBeginTag(HtmlTextWriterTag.Div); // Begin #2

                writer.AddAttribute(HtmlTextWriterAttribute.Class, classValue_2);
                writer.AddAttribute(HtmlTextWriterAttribute.Src, urlValue);

                writer.RenderBeginTag(HtmlTextWriterTag.Iframe); // Begin #3

                writer.RenderEndTag(); // End #3
                writer.RenderEndTag(); // End #2
                writer.RenderEndTag(); // End #1
            }
            // Return the result.
            return stringWriter.ToString();
        }

        #endregion

        #region "Optional Interfaces"

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   ModuleActions is an interface property that returns the module actions collection for the module
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public ModuleActionCollection ModuleActions
        {
            get
            {
                // add the Edit Text action
                var Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), Localization.GetString("EditTheme", this.LocalResourceFile), EditTheme, "", "", this.EditUrl("EditTheme"), false, SecurityAccessLevel.Edit, true, false);

                return Actions;
            }
        }

        #endregion

    }
}