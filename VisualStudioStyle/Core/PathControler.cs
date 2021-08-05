using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AgentIDE.Core
{
    public class PathControler
    {
        public string PathProject { get; set; }
        private TreeView TreeProject;
        public enum TypeTree { Folder, Archive, Project }
        public PathControler(TreeView TreeProject)
        {
            this.TreeProject = TreeProject;
        }

        public void NewProject()
        {
            System.Windows.Forms.OpenFileDialog folderBrowser = new System.Windows.Forms.OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = System.IO.Path.GetDirectoryName(folderBrowser.FileName);
                PathProject = folderPath;
                AtualizeProject(TreeProject);
            }
        }

        private TreeViewItem GetTreeView(string text, TypeTree type)
        {
            TreeViewItem item = new TreeViewItem();
            item.IsExpanded = false;
            StackPanel stack = new StackPanel();
            stack.Orientation = System.Windows.Controls.Orientation.Horizontal;
            System.Windows.Controls.Label lbl = new System.Windows.Controls.Label();
            lbl.Content = text;
            if (type == TypeTree.Folder)
                stack.Children.Add(new PackIcon { Kind = PackIconKind.Folder });
            else if (type == TypeTree.Archive)
                stack.Children.Add(new PackIcon { Kind = PackIconKind.Archive });
            else if (type == TypeTree.Project)
                stack.Children.Add(new PackIcon { Kind = PackIconKind.ArchiveAlert });
            stack.Children.Add(lbl);
            item.Header = stack;
            return item;
        }
        public void AdditionalTreeProject(string path, System.Windows.Controls.TreeViewItem tree)
        {
            if (path.Length > 0 && Directory.Exists(PathProject))
            {
                string[] directories = Directory.GetDirectories(path);
                string[] archives = Directory.GetFiles(path);
                foreach (var item in directories)
                {
                    TreeViewItem treeItem2 = GetTreeView(Path.GetFileName(item), TypeTree.Folder);
                    tree.Items.Add(treeItem2);
                    AdditionalTreeProject(item, treeItem2);
                }
                foreach (var item in archives)
                {
                    tree.Items.Add(GetTreeView(Path.GetFileName(item), TypeTree.Archive));

                }
            }
        }
        public void AtualizeProject(System.Windows.Controls.TreeView tree)
        {
            tree.Items.Clear();
            if (PathProject.Length > 0 && Directory.Exists(PathProject))
            {
                TreeViewItem treeItem = GetTreeView(Path.GetFileName(PathProject), TypeTree.Project);
                AdditionalTreeProject(PathProject, treeItem);
                tree.Items.Add(treeItem);
            }
        }
    }
}
