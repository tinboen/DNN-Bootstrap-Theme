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
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Web;

using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using DotNetNuke.UI.Skins;
using DotNetNuke.UI.Skins.Controls;
using DBH.BootstrapTheme.Components;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;

namespace DBH.BootstrapTheme
{
    /// -----------------------------------------------------------------------------
    /// <summary>   
    /// The Edit class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from PortalModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class EditTheme : PortalModuleBase
    {
        #region Private Members

        private string strXMLfile = Null.NullString;
        private string _rootPath = Null.NullString;

        #endregion

        #region BoundFields with Paging and Sorting

        protected void gvDepartment_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;

            if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
            {
                //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (gv.ShowFooter == true && gv.Rows.Count > 0)
            {
                //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        #endregion

        #region Gidview Procedures

        protected void BindGrid()
        {
            string[] folderList = Directory.GetDirectories(_rootPath);
            lstExistingTheme.DataSource = folderList;
            lstExistingTheme.DataBind();
        }

        protected void gvZip_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("content-disposition", "attachment; filename=" + e.CommandArgument.ToString());
                Response.TransmitFile(Server.MapPath("~/Zip//" + e.CommandArgument.ToString()));
                Response.End();
            }
        }

        #endregion

        #region "Optional Command Interfaces"

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// cmdSubmit_Click redirect to add new record dialog when the add new record button is clicked
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected void cmdSubmit_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    // Get file name
                    string filename = Path.GetFileName(FileUpload1.FileName);

                    // Get File name only to create folder
                    string filenameWitoutextension = Path.GetFileNameWithoutExtension(FileUpload1.FileName);

                    //// Create folder by file name only
                    //File.Create(_rootPath + "\\" + filenameWitoutextension);

                    // Save file to outside new folder
                    FileUpload1.SaveAs(_rootPath + "\\" + filename);

                    // Extract file to new folder
                    ZipFile.ExtractToDirectory(_rootPath + "\\" + filename, _rootPath);

                    // Delete the zip file
                    System.IO.File.Delete(_rootPath + "\\" + filename);

                    //string destdir = Server.MapPath(".") + @"\Zip\" + filenameWitoutextension + ".Zip";
                    //zip.AddDirectory(Server.MapPath(".") + @"\Upload\");
                    //zip.Save(destdir);
                    //string[] files = System.IO.Directory.GetFiles(Server.MapPath("~/Upload//"));
                    //foreach (string f in files)
                    //{
                    //    System.IO.File.Delete(f);
                    //}
                    BindGrid();
                }
                else
                {
                    // Notify the user that a file was not uploaded.
                    Skin.AddModuleMessage(this, Localization.GetString("FileNotFound.ErrorMessage", LocalResourceFile), ModuleMessage.ModuleMessageType.RedError);
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                Skin.AddModuleMessage(this, Localization.GetString("SavingError.ErrorMessage", LocalResourceFile), ModuleMessage.ModuleMessageType.RedError);

            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// cmdCancel_Click runs when the cancel button is clicked
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        protected void cmdCancel_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                // Reditect to previous page
                Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        #endregion

        #region "Events"

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// OnInit runs when this program start is clicked
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
                //JavaScript.RequestRegistration(CommonJs.jQuery);

                //ClientResourceManager.RegisterScript(Page, "~/desktopmodules/DBH.BootstrapTheme/scripts/jquery.Manager.js");

                //DetailView.ItemDataBound += RepeaterItemDataBound;

                ////Save User Preferences
                //SavePersonalizedSettings();
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

            //If user not in Access Roles or Super User, then we will redirect to Access Denied page
            if (!UserInfo.IsSuperUser)
            {
                Skin.AddModuleMessage(this, Localization.GetString("SuperUser.ErrorMessage", LocalResourceFile), ModuleMessage.ModuleMessageType.YellowWarning);
                //cmdCreate.Visible = false;
                return;
            }

            try
            {
                // setup the strXMLFile for global access
                ModuleController modCtrl = new ModuleController();
                _rootPath = @HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName.Replace("/", "\\") + "\\Templates";

                if (!IsPostBack)
                {
                    //Bind data to the GridView control.
                    BindGrid();
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

    }
}
