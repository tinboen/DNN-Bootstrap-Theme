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

using System.IO;
using System.Collections.Generic;
using DotNetNuke.Entities.Modules;
using System;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using DotNetNuke.Services.Search.Entities;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common.Utilities;
using System.Web.UI.HtmlControls;
using DotNetNuke.Entities.Users;
using System.Xml;

namespace DBH.BootstrapTheme.Components
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for BootstrapTheme
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class FeatureController : ModuleSearchBase, IPortable, IUpgradeable
    {
        // feel free to remove any interfaces that you don't wish to use
        // (requires that you also update the .dnn manifest file)

        #region Private Members


        #endregion

        #region Populate Dropdownlist

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Reading the names of folders and Binding the Radio Buttons
        /// Below is the method that reads the names of folders and then binds the same to the ASP.Net Radio Buttons Control
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void LoadThemes(int ModuleId, RadioButtonList optTheme)
        {

            optTheme.Items.Clear();
            DotNetNuke.Entities.Modules.ModuleController modCtrl = new ModuleController();
            string sModulePath = @HttpContext.Current.Server.MapPath("~/DesktopModules/") + modCtrl.GetModule(ModuleId).DesktopModule.FolderName + "\\";
            var moduleTemplatePath = sModulePath + "Templates";
            string[] folderList = Directory.GetDirectories(moduleTemplatePath);
            foreach (string folderPath in folderList)
            {
                optTheme.Items.Add(new ListItem(Path.GetFileName(folderPath)));

                //string result = Path.GetFileName(folderPath);

                //if (Request.QueryString["ctl"] == result)
                //{
                //    // Indicates a successful or positive action
                //    li.Attributes.Add("class", "btn btn-success");
                //    bFound = true;
                //}
                //else
                //{
                //    // Secondary, outline button
                //    li.Attributes.Add("class", "btn btn-secondary");
                //}

            }
            optTheme.SelectedIndex = 0;
        }

        #endregion

        #region Optional Interfaces

        /// <summary>
        /// Gets the modified search documents for the DNN search engine indexer.
        /// </summary>
        /// <param name="moduleInfo">The module information.</param>
        /// <param name="beginDate">The begin date.</param>
        /// <returns></returns>
        public override IList<SearchDocument> GetModifiedSearchDocuments(ModuleInfo moduleInfo, DateTime beginDate)
        {
            var searchDocuments = new List<SearchDocument>();

            return searchDocuments;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="moduleId">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        public string ExportModule(int ModuleID)
        {
            throw new NotImplementedException();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="moduleId">The Id of the module to be imported</param>
        /// <param name="content">The content to be imported</param>
        /// <param name="version">The version of the module to be imported</param>
        /// <param name="userId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            throw new NotImplementedException();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        public string UpgradeModule(string Version)
        {
            try
            {
                switch (Version)
                {
                    case "01.00.00":
                        // run your custom code here
                        return "success";
                    default:
                        return "success";
                }
            }
            catch
            {
                return "failure";
            }
        }


        #endregion

    }
}
