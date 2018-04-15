using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using WallE.Hierarchy;
using WallE.Sintime;
using WallE.Sintime.AST.Statements;
using WallE.Sintime.AST.Statements.Instructions.Commands.Commons;
using WallE.Sintime.AST;
using WallE.Painters;

namespace WallE
{
    public partial class MainForm : Form
    {
        #region Properties

        private int sizeTile;
        private int sizeTileDefault;
        private int speedTimer;
        private Map map;
        private List<Error> errors;
        private IEnumerator<Tuple<InstructionNode, bool>> instructions;
        private Dictionary<Type, Painter> pallete;

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();
            Compiler.Initialize();
            InitializeColorText();
            InitializePallente();
            notepad.NewScript();
            errors = new List<Error>();
            map = new Map();
            sizeTile = sizeTileDefault = 42;
            speedTimer = timer.Interval;
        }

        #endregion

        #region Actions

        private void Build(Func<string, List<Error>,bool> function)
        {
            errors.Clear();
            treeViewWatch.Nodes.Clear();
            richTextBoxMessages.Text = "";
            notepad.SaveAllScript();
            function(notepad.SelectedScript.CurrentFile, errors);
            UpdateErrors();
        }

        private void DebugStart(bool start, Action action)
        {
            errors.Clear();
            treeViewWatch.Nodes.Clear();
            richTextBoxMessages.Text = "";
            notepad.SaveAllScript();
            instructions = Compiler.Execute(notepad.SelectedScript.CurrentFile, errors, map);
            UpdateErrors();
            if (instructions != null)
            {
                timer.Enabled = start;
                action();
            }
        }

        private void ContinuePause(bool start, Action action)
        {
            timer.Enabled = start;
            action();
        }
        
        private void Stop()
        {
            SelectStop();
            notepad.RemoveAllHighlights();
            timer.Enabled = false;
        }

        private void Next(Predicate<Tuple<InstructionNode,bool>> predicate)
        {
            while (instructions != null && instructions.MoveNext())
            {
                UpdateMessages(instructions.Current);
                UpdateWatch(instructions.Current);
                if (predicate(instructions.Current))
                {
                    notepad.SelectLine(instructions.Current.Item1.File, instructions.Current.Item1.Line);
                    pictureBoxDisplay.Invalidate();
                    return;
                }
            }
            Stop();
        }
        
        #endregion

        #region Methods Of Tool

        private void InitializeColorText()
        {
            foreach (var i in Compiler.StatementNodes)
                notepad.AddKeyword(i.Key, (Activator.CreateInstance(i.Value, new ActionNode(null)) as StatementNode).VisualColor);
            foreach (var i in Compiler.Separators)
                notepad.AddKeyword(i, ColorTranslator.ToHtml(Color.Blue));
            notepad.AddKeyword("action", ColorTranslator.ToHtml(Color.DarkBlue));
            notepad.AddKeyword("end", ColorTranslator.ToHtml(Color.DarkBlue));
        }

        private void InitializePallente()
        {
            pallete = new Dictionary<Type, Painter>();
            LinkedList<Type> objects = new LinkedList<Type>();
            LinkedList<Type> painters = new LinkedList<Type>();
            foreach (var assembly in SearchAssemblys(Environment.CurrentDirectory))
            {
                ActionNode action = new ActionNode(null);
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass && !type.IsAbstract)
                    {
                        if (type.IsSubclassOf(typeof(Painter)) && !painters.Contains(type))
                            painters.AddLast(type);
                        if (type.IsSubclassOf(typeof(Hierarchy.Object)) && !objects.Contains(type))
                            objects.AddLast(type);
                    }
                }
            }
            foreach (var item in objects)
            {
                List<Painter> list = new List<Painter>();
                Painter temp = null;
                foreach (var type in painters)
                {
                    if (!type.IsAbstract && type.IsSubclassOf(typeof(Painter)))
                    {
                        var painter = (Painter)Activator.CreateInstance(type);
                        if (painter.TypeHePaints.Equals(item))
                            list.Add(painter);
                    }
                }
                if (list.Count == 0)
                    list.Add(new PainterNull());
                foreach (var painter in list)
                {
                    if (temp == null)
                        temp = painter;
                    else
                    {
                        if (painter.GetType().IsSubclassOf(temp.GetType()))
                            temp = painter;
                        else if (!temp.GetType().IsSubclassOf(painter.GetType()))
                            throw new DuplicateWaitObjectException("Collision of the painters of the objects.");
                    }
                }
                pallete.Add(item, temp);
            }
        }

        private void UpdateWatch(Tuple<InstructionNode, bool> instruction)
        {
            if (instruction.Item1 is SetNode && instruction.Item2)
            {
                treeViewWatch.Sort();
                treeViewWatch.ExpandAll();
                var temp = instruction.Item1 as SetNode;
                foreach (var assign in temp.Assigns)
                {
                    var file = Path.GetFileNameWithoutExtension(assign.File);
                    var name = assign.VariableName;
                    var index = assign.VariableIndex;
                    var value = assign.VariableValue;
                    if (!treeViewWatch.Nodes.ContainsKey(file))
                        treeViewWatch.Nodes.Add(file, file);
                    var node = treeViewWatch.Nodes[treeViewWatch.Nodes.IndexOfKey(file)];
                    if (!node.Nodes.ContainsKey(name))
                        node.Nodes.Add(name, name);
                    node = node.Nodes[node.Nodes.IndexOfKey(name)];
                    if (index.Count == 0)
                        node.Text = name + " = " + (value == null ? "nan" : value.ToString());
                    else
                    {
                        string indexString = "";
                        foreach (var i in index)
                            indexString += "_" + (i == null ? "nan" : i.ToString());
                        indexString = indexString.Substring(1);
                        if (!node.Nodes.ContainsKey(indexString))
                            node.Nodes.Add(indexString, indexString);
                        node = node.Nodes[node.Nodes.IndexOfKey(indexString)];
                        node.Text = indexString + " = " + (value == null ? "nan" : value.ToString());
                    }
                }
                tabControl.SelectTab(2);
            }
        }

        private void UpdateMessages(Tuple<InstructionNode, bool> instruction)
        {
            if (instruction.Item1 is MessageNode && instruction.Item2)
            {
                var temp = instruction.Item1 as MessageNode;
                string message;
                if (temp.MessageType == MessageTypes.String)
                    message = temp.Message;
                else message = temp.MessageExpression.Evaluate().ToString();
                richTextBoxMessages.Text = message + "\n" + richTextBoxMessages.Text;
                tabControl.SelectTab(0);
            }
        }

        private void UpdateErrors()
        {
            listViewErrors.SelectedItems.Clear();
            listViewErrors.Items.Clear();
            errors.Sort((a, b) => a.File.CompareTo(b.File));
            errors.Sort((a, b) => (a.File == b.File) ? a.Line.CompareTo(b.Line) : 0);
            foreach (var error in errors)
                listViewErrors.Items.Add(new ListViewItem(new string[] { error.File, error.Line + "", error.Explication }));
            if (errors.Count != 0)
                tabControl.SelectTab(1);
            UpdateWaveLines();
        }

        private void UpdateWaveLines()
        {
            notepad.RemoveAllWaveLines();
            foreach (var i in errors)
            {
                int index = notepad.ContainScript(i.File);
                if (index == -1)
                {
                    notepad.SetScript(i.File);
                    index = notepad.ContainScript(i.File);
                }
                notepad.AddWaveLines(index, i.Line);
            }
        }

        private void BlockFileMenu(bool opcion)
        {
            newFileToolStripMenuItem.Enabled = opcion;
            openFileToolStripMenuItem.Enabled = opcion;
            openProjectToolStripMenuItem.Enabled = opcion;
            saveFileToolStripMenuItem.Enabled = opcion;
            saveAsToolStripMenuItem.Enabled = opcion;
            saveProjectProyectoToolStripMenuItem.Enabled = opcion;
            closeFileToolStripMenuItem.Enabled = opcion;
            closeProjectToolStripMenuItem.Enabled = opcion;
        }

        private void BlockDebugMenu()
        {
            bool opcion = notepad.SelectedScript != null;
            buildSimuladorToolStripMenuItem.Enabled = opcion;
            buildRobotToolStripMenuItem.Enabled = opcion;
            debugToolStripMenuItem.Enabled = opcion;
            startToolStripMenuItem.Enabled = opcion;
        }

        private void SelectDebugPause()
        {
            BlockFileMenu(false);
            buildSimuladorToolStripMenuItem.Enabled = false;
            buildRobotToolStripMenuItem.Enabled = false;
            debugToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = false;
            continueToolStripMenuItem.Enabled = true;
            pauseToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
            nextInstructionToolStripMenuItem.Enabled = true;
            nextActionToolStripMenuItem.Enabled = true;
        }

        private void SelectStartContinue()
        {
            BlockFileMenu(false);
            buildSimuladorToolStripMenuItem.Enabled = false;
            buildRobotToolStripMenuItem.Enabled = false;
            debugToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = false;
            continueToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = true;
            nextInstructionToolStripMenuItem.Enabled = false;
            nextActionToolStripMenuItem.Enabled = false;
        }
        
        private void SelectStop()
        {
            BlockFileMenu(true);
            buildSimuladorToolStripMenuItem.Enabled = true;
            buildRobotToolStripMenuItem.Enabled = true;
            debugToolStripMenuItem.Enabled = true;
            startToolStripMenuItem.Enabled = true;
            continueToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = false;
            nextInstructionToolStripMenuItem.Enabled = false;
            nextActionToolStripMenuItem.Enabled = false;
        }

        private IEnumerable<Assembly> SearchAssemblys(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
                if (file.EndsWith(".dll") || file.EndsWith(".exe"))
                    yield return Assembly.LoadFile(file);
        }
        
        #endregion

        #region File Menu

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.NewScript();
            BlockDebugMenu();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.OpenScript();
            BlockDebugMenu();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.OpenProject();
            BlockDebugMenu();
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.SaveCurrentScript();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.SaveAsCurrentScript();
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.SaveAllScript();
        }

        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.CloseCurrentScript();
            BlockDebugMenu();
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepad.CloseAllScript();
            BlockDebugMenu();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Debug Menu

        private void buildSimulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Build(Compiler.CompileMap);
        }

        private void buildRobotToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Build(Compiler.CompileRobot);
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugStart(false, SelectDebugPause);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugStart(true, SelectStartContinue);
        }

        private void continueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContinuePause(true, SelectStartContinue);
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContinuePause(false, SelectDebugPause);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void nextInstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Next(a => true);
        }

        private void nextActionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Next(a => a.Item1.InstructionType == InstructionsTypes.Special && a.Item2);
        }

        #endregion

        #region Options Menu

        private void sizeOfTheTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var opcion = new OptionForm(sizeTile * 20 / sizeTileDefault);
            opcion.ShowDialog();
            sizeTile = Math.Max(sizeTileDefault / 2, sizeTileDefault * opcion.Value / 20);
            pictureBoxDisplay.Invalidate();
        }

        private void updatesSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var opcion = new OptionForm(101 - (timer.Interval * 10 / speedTimer));
            opcion.ShowDialog();
            timer.Interval = speedTimer * (101 - opcion.Value) * 5 / 50;
        }

        #endregion

        #region Help Menu

        private void sintimeLanguajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new InfoForm();
            info.ShowDialog();
        }

        #endregion

        #region Events

        private void timer_Tick(object sender, EventArgs e)
        {
            Next(a => true);
        }

        private void listViewErrors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewErrors.SelectedItems.Count > 0)
            {
                string file = listViewErrors.SelectedItems[0].SubItems[0].Text;
                int line = int.Parse(listViewErrors.SelectedItems[0].SubItems[1].Text);
                notepad.SelectLine(file, line);
            }
            else notepad.RemoveAllHighlights();
        }

        private void pictureBoxDisplay_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.DodgerBlue) { Width = 5 };
            var canvas = sender as PictureBox;
            var graphic = e.Graphics;
            canvas.Width = sizeTile * map.Columns;
            canvas.Height = sizeTile * map.Rows;
            for (int i = 0; i < map.Rows; i++)
                for (int j = 0; j < map.Columns; j++)
                    if (map[i, j] != null && pallete.ContainsKey(map[i, j].GetType()))
                        pallete[map[i, j].GetType()].Paint(i, j, sizeTile, graphic, map[i, j]);
            for (int i = 0; i <= canvas.Height; i++)
                graphic.DrawLine(pen, 0, i * sizeTile, canvas.Width, i * sizeTile);
            for (int i = 0; i <= canvas.Width; i++)
                graphic.DrawLine(pen, i * sizeTile, 0, i * sizeTile, canvas.Height);
        }

        #endregion
    }
}
