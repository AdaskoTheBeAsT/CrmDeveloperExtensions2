﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio;

namespace CrmDeveloperExtensions.Core
{
    public static class ProjectWorker
    {
        public static void ExcludeFolder(Project project, string folderName)
        {
            for (int i = 1; i <= project.ProjectItems.Count; i++)
            {
                Guid itemType = new Guid(project.ProjectItems.Item(i).Kind);
                if (itemType != VSConstants.GUID_ItemType_PhysicalFolder)
                    continue;

                if (String.Equals(project.ProjectItems.Item(i).Name, folderName, StringComparison.CurrentCultureIgnoreCase))
                    project.ProjectItems.Item(i).Remove();
            }
        }

        public static string GetFolderProjectFileName(string projectFullName)
        {
            string path = Path.GetDirectoryName(projectFullName);
            if (path != null)
            {
                var dirName = new DirectoryInfo(path).Name;
                var fileName = new FileInfo(projectFullName).Name;
                string folderProjectFileName = dirName + "\\" + fileName;

                return folderProjectFileName;
            }

            return null;
        }
    }
}
