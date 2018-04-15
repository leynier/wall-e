using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Edit;
using System.IO;

namespace CodeStrange.Controls
{
    public partial class Notepad : UserControl
    {
        protected OpenFileDialog openFileDialog;
        protected SaveFileDialog saveFileDialog;
        protected FolderBrowserDialog folderBrowserDialog;
        protected Dictionary<string, Tuple<Color, List<string>>> Colors;

        public string ProjectPath { get; protected set; }
        public string Extension { get; set; }
        public string Languaje { get; set; }

        public Notepad()
        {
            InitializeComponent();
            ProjectPath = Environment.CurrentDirectory;
            Languaje = "SINTIME";
            Extension = "stm";
            Colors = new Dictionary<string, Tuple<Color, List<string>>>();
            InitializeOpenFileDialog();
            InitializeSaveFileDialog();
            InitializeFolderBrowserDialog();
        }

        #region Protected Methods

        protected void InitializeOpenFileDialog()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Open file of {0}", Languaje);
            openFileDialog.Filter = string.Format("Extension of {0}|*.{1}", Languaje, Extension);
        }

        protected void InitializeSaveFileDialog()
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = string.Format("Save file of {0}", Languaje);
            saveFileDialog.Filter = string.Format("Extension of {0}|*.{1}|RTF File|*.rtf", Languaje, Extension);
        }

        protected void InitializeFolderBrowserDialog()
        {
            folderBrowserDialog = new FolderBrowserDialog();
        }
        
        protected EditControl CreateEditControl()
        {
            var editControl = new EditControl();

            editControl.AcceptsReturn = true;
            editControl.AcceptsTab = true;
            editControl.AllowDrop = true;
            editControl.Font = new Font("Consolas", 10, FontStyle.Regular, GraphicsUnit.Point, 0);
            editControl.Location = new Point(8, 0);
            editControl.Multiline = true;
            editControl.Name = "tbCode";
            editControl.Size = new Size(568, 352);
            editControl.TabIndex = 0;
            editControl.Text = "";
            editControl.WordWrap = false;
            editControl.StatusBarVisible = true;
            editControl.LineNumberMarginVisible = true;
            editControl.IndicatorMarginVisible = false;
            editControl.OutliningEnabled = true;
            editControl.BraceMatchingEnabled = true;
            editControl.WhiteSpaceVisible = false;
            editControl.KeepTabs = true;
            editControl.ContextPromptEnabled = true;
            editControl.ContextChoiceEnabled = true;
            editControl.SyntaxColoringEnabled = true;
            editControl.GridLinesVisible = false;
            editControl.IndentType = EditIndentType.Smart;
            editControl.ShowSplitterButton = false;
            editControl.FileExtension = Extension;
            editControl.FileNameVisible = true;

            foreach (var i in Colors)
            {
                editControl.AddColorGroup(i.Key, i.Value.Item1, Color.White, true, false, EditColorGroupType.RegularText);
                foreach (string k in i.Value.Item2)
                    editControl.AddKeyword(k, i.Key);
            }
            
            editControl.AddColorGroup("errors", Color.Red, Color.White, true, false, EditColorGroupType.RegularText);

            editControl.AddColorGroup("strings", Color.FromArgb(255, 193, 21, 67), Color.White, true, false, EditColorGroupType.RegularText);
            editControl.AddTag("\"", "\"", "\\", false, "strings");

            editControl.AddColorGroup("comments", Color.DarkGreen, Color.White, true, false, EditColorGroupType.RegularText);
            editControl.AddTag("/*", "*/", "", true, "comments");
            editControl.AddTag("//", "", "", false, "comments");

            editControl.Dock = DockStyle.Fill;

            return editControl;
        }

        protected void AddEditControl(EditControl e)
        {
            TabPage codePage = new TabPage();
            codePage.Controls.Add(e);
            codePage.Text = Path.GetFileName(e.CurrentFile);
            e.FileNameChanged += (o, ea) =>
            {
                codePage.Text = Path.GetFileName(e.CurrentFile);
            };
            e.HasWorkingFileChanged += (o, ea) =>
            {
                if (!e.HasWorkingFile)
                    tabControl.TabPages.Remove(codePage);
            };
            tabControl.TabPages.Add(codePage);
        }

        #endregion

        #region Public Methods

        public EditControl SelectedScript
        {
            get { return tabControl.SelectedTab == null ? null : tabControl.SelectedTab.Controls[0] as EditControl; }
        }

        public List<EditControl> EditControls
        {
            get
            {
                return tabControl.TabPages.OfType<TabPage>().Select(tp => tp.Controls[0] as EditControl).ToList();
            }
        }

        public bool SetScript(string path)
        {
            if (!File.Exists(path))
                return false;
            var e = CreateEditControl();
            e.LoadFile(path);
            AddEditControl(e);
            return true;
        }

        public void NewScript()
        {
            string temp;
            for (int i = 0; true; i++)
            {
                temp = string.Format("{0}\\Untitled{1}.stm", ProjectPath, i == 0 ? "" : i.ToString());
                if (!File.Exists(temp))
                    break;
            }
            var file = File.Create(temp);
            file.Close();
            var e = CreateEditControl();
            e.LoadFile(temp);
            AddEditControl(e);
            SelectScript(temp);
        }

        public bool OpenScript()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return SetScript(openFileDialog.FileName);
            return false;
        }

        public bool OpenProject()
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                CloseAllScript();
                ProjectPath = folderBrowserDialog.SelectedPath;
                Environment.CurrentDirectory = ProjectPath;
                foreach (var path in Directory.GetFiles(ProjectPath, "*.stm"))
                    SetScript(path);
                return true;
            }
            return false;
        }
        
        public void SaveCurrentScript()
        {
            if (SelectedScript != null)
                SelectedScript.Save();
        }

        public bool SaveAsCurrentScript()
        {
            if (SelectedScript != null && saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FilterIndex == 2)
                    SelectedScript.SaveAsRTF(saveFileDialog.FileName);
                else
                    SelectedScript.SaveFile(saveFileDialog.FileName);
                CloseCurrentScript();
                SetScript(saveFileDialog.FileName);
                SelectScript(saveFileDialog.FileName);
                return true;
            }
            return false;
        }

        public bool SaveScript(int index)
        {
            if (index < 0 || index >= EditControls.Count)
                return false;
            EditControls[index].Save();
            return true;
        }

        public bool SaveAsScript(int index)
        {
            if (index < 0 || index >= EditControls.Count)
                return false;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FilterIndex == 2)
                    EditControls[index].SaveAsRTF(saveFileDialog.FileName);
                else
                    EditControls[index].SaveFile(saveFileDialog.FileName);
                CloseCurrentScript();
                SetScript(saveFileDialog.FileName);
                SelectScript(saveFileDialog.FileName);
                return true;
            }
            return false;
        }

        public void SaveAllScript()
        {
            foreach (var ec in EditControls)
                ec.Save();
        }
        
        public void CloseCurrentScript()
        {
            if (SelectedScript == null)
                return;
            SelectedScript.Close();
        }

        public bool CloseScript(int index)
        {
            if (index < 0 || index >= EditControls.Count)
                return false;
            EditControls[index].Close();
            return true;
        }

        public void CloseAllScript()
        {
            foreach (var e in EditControls)
                e.Close();
        }

        public int ContainScript(string path)
        {
            for (int i = 0; i < EditControls.Count; i++)
                if (EditControls[i].CurrentFile == path)
                    return i;
            return -1;
        }

        public bool SelectScript(int index)
        {
            if (index < 0 || index >= tabControl.TabCount)
                return false;
            tabControl.SelectTab(index);
            return true;
        }

        public bool SelectScript(string path)
        {
            int index = ContainScript(path);
            if (index == -1) return false;
            return SelectScript(index);
        }

        public bool SelectOrOpenScript(string path)
        {
            if (SelectScript(path)) return true;
            if (SetScript(path))
                return SelectScript(path);
            return false;
        }

        public bool AddWaveLines(int index, params int[] lines)
        {
            if (index < 0 || index >= tabControl.TabCount)
                return false;
            foreach (var i in lines)
            {
                string s = EditControls[index].GetString(i);
                int start = 0;
                int end = s.Length;
                for (int j = 0; j < s.Length; j++)
                {
                    if (s[j] != ' ')
                    {
                        start = j;
                        break;
                    }
                }
                for (int j = s.Length -1 ; j >= 0; j--)
                {
                    if (s[j] != ' ')
                    {
                        end = j;
                        break;
                    }
                }
                EditControls[index].AddWaveLine(i, start + 1, end + 2, "errors");
            }
            return true;
        }

        public bool RemoveWaveLines(int index, params int[] lines)
        {
            if (index< 0 || index >= tabControl.TabCount)
                return false;
            foreach (var i in lines)
                EditControls[index].RemoveWaveLines(i);
            return true;
        }

        public bool RemoveAllWaveLines(int index)
        {
            if (index < 0 || index >= tabControl.TabCount)
                return false;
            for (int i = 1; i <= EditControls[index].LineCount; i++)
                EditControls[index].RemoveWaveLines(i);
            return true;
        }

        public void RemoveAllWaveLines()
        {
            foreach (var edit in EditControls)
                for (int i = 1; i <= edit.LineCount; i++)
                    edit.RemoveWaveLines(i);
        }

        public bool AddHighlights(int index, params int[] lines)
        {
            if (index < 0 || index >= tabControl.TabCount)
                return false;
            foreach (var i in lines)
                EditControls[index].AddHighlight(i);
            return true;
        }

        public bool RemoveHighlights(int index, params int[] lines)
        {
            if (index < 0 || index >= tabControl.TabCount)
                return false;
            foreach (var i in lines)
                EditControls[index].RemoveHighlight(i);
            return true;
        }

        public bool RemoveAllHighlights(int index)
        {
            if (index < 0 || index >= tabControl.TabCount)
                return false;
            EditControls[index].RemoveAllHighlight();
            return true;
        }

        public void RemoveAllHighlights()
        {
            foreach (var i in EditControls)
                i.RemoveAllHighlight();
        }

        public bool SelectLine(string path, int line)
        {
            if (!SelectOrOpenScript(path))
                return false;
            int index = ContainScript(path);
            if (!RemoveAllHighlights(index))
                return false;
            return AddHighlights(index, line);
        }

        public void AddKeyword(string text, string color)
        {
            Color _color = ColorTranslator.FromHtml(color);
            if (!Colors.ContainsKey(color))
                Colors.Add(color, new Tuple<Color, List<string>>(_color, new List<string>()));
            Colors[color].Item2.Add(text);
        }

        #endregion
    }
}
