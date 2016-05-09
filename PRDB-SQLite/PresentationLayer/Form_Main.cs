using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PRDB_SQLite.DataAccessLayer;
using PRDB_SQLite.BusinessLogicLayer;
using PRDB_SQLite.PresentationLayer;
using DevExpress.XtraTab;
using System.Timers;

namespace PRDB_SQLite
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }

        #region ** Declare variables and objects

        #region * Forms
        Form_About frm_About;
        Form_Connecting frm_CF;
        Form_DeleteQuery frm_DelQuery;
        Form_DeleteRelation frm_DelRelation;
        Form_DeleteSchema frm_DelSchema;
        Form_DeleteView frm_DelView;
        Form_InputType frm_InpType;
        Form_InputValue frm_InpValue;
        Form_Main frm_Main;
        Form_NewQuery frm_NewQuery;
        Form_NewSchema frm_NewSchema;
        Form_NewRelation frm_NewRelation;
        Form_NewView frm_NewView;
        Form_OpenQuery frm_OpenQuery;
        Form_OpenRelation frm_OpenRelation;
        Form_OpenSchema frm_OpenSchema;
        Form_OpenView frm_OpenView;
        Form_RenameDB frm_RenameDB;
        Form_Saving frm_Saving;
        Form_Loading frm_Loading;
        #endregion

        #region * Objects
        ProbDatabase DB;
        ProbSchema CurrentSchema;
        ProbRelation CurrentRelation;
        ProbQuery CurrentQuery;
        ProbView CurrentView;
        System.Timers.Timer timer;
        #endregion

        #region * TreeView
        TreeNode NodeDB, NodeSchema, NodeRelation, NodeView, NodeQuery, CurrentNode, NewNode;
        #endregion

        #region * Class
        clsProcess clsProcess = new clsProcess();
        #endregion

        #region * Images
        public struct ImageIndex
        {
            public int UnselectedState;
            public int SelectedState;
        }
        #endregion

        #region * Variables
        int CurrentRow, CurrentColumn, CurrentCell;
        ImageIndex DB_ImgIndex, Folder_ImgIndex, Schema_ImgIndex, Relation_ImgIndex, View_ImgIndex, Query_ImgIndex;
        string DBName, ErrorMessage;
        bool validated, flag, SavingNewSchema, SavingNewRelation, SavingNewView, SavingNewQuery;
        bool newDatabase;
        #endregion

        #endregion

        #region ** Menu

        #region * Database

        private void MenuBar_NewDatabase_Click(object sender, EventArgs e)
        {
            SaveFileDialog DialogSave = new SaveFileDialog();                                   // Save dialog
            DialogSave.DefaultExt = "pdb";                                                                 // Default extension
            DialogSave.Filter = "Database file (*.pdb)|*.pdb|All files (*.*)|*.*";            // add extension to dialog
            DialogSave.AddExtension = true;                                                              // enable adding extension
            DialogSave.RestoreDirectory = true;                                                         // Tu dong phuc hoi duong dan cho lan sau
            DialogSave.Title = "Create new database...";
            DialogSave.InitialDirectory = clsProcess.GetRootPath(AppDomain.CurrentDomain.BaseDirectory.ToString());
            DialogSave.SupportMultiDottedExtensions = true;
            try
            {
                if (DialogSave.ShowDialog() == DialogResult.OK)
                {
                    DB = null;
                    TreeView.Nodes.Clear();

                    DB = new ProbDatabase(DialogSave.FileName);
                    PRDB_SQLite.Program.dbName = DB.DBName;
                    PRDB_SQLite.Program.ConnectionString = DB.ConnectionString;

                    if (!clsProcess.CreateNewDatabase(DB))
                        throw new Exception("Cannot create new database");

                    Load_TreeView();
                    clsProcess.EditMode = false;
                    newDatabase = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DialogSave.Dispose();
        }

        private void MenuBar_OpenDatabase_Click(object sender, EventArgs e)
        {
            OpenFileDialog DialogOpen = new OpenFileDialog();
            DialogOpen.DefaultExt = "pdb";
            DialogOpen.Filter = "Database file (*.pdb)|*.pdb";
            DialogOpen.AddExtension = true;
            DialogOpen.RestoreDirectory = true;
            DialogOpen.Title = "Open database...";
            DialogOpen.InitialDirectory = clsProcess.GetRootPath(AppDomain.CurrentDomain.BaseDirectory.ToString());
            DialogOpen.SupportMultiDottedExtensions = true;
            try
            {
                if (DialogOpen.ShowDialog() == DialogResult.OK)
                {
                    TreeView.Nodes.Clear();
                    DB = null;

                    DB = clsProcess.LoadDB(DialogOpen.FileName);
                    PRDB_SQLite.Program.dbName = DB.DBName;
                    PRDB_SQLite.Program.ConnectionString = DB.ConnectionString;

                    Cursor oldCursor = Cursor;
                    Cursor = Cursors.WaitCursor;

                    frm_CF = new Form_Connecting();
                    frm_CF.Show();
                    frm_CF.Refresh();

                    bool success = (clsProcess.Connect() && clsProcess.LoadDatabase(DB));

                    frm_CF.Close();
                    Cursor = oldCursor;

                    if (!success)
                    {
                        clsProcess.Dispose();
                        throw new Exception("Cannot connect to the database") { };
                    }
                    Load_TreeView();
                    clsProcess.EditMode = true;
                    newDatabase = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DialogOpen.Dispose();
        }

        private void MenuBar_SaveDatabase_Click(object sender, EventArgs e)
        {
            // Record to database
            Cursor oldCursor = Cursor;
            Cursor = Cursors.WaitCursor;

            frm_Saving = new Form_Saving();
            frm_Saving.Show();
            frm_Saving.Refresh();

            if (!clsProcess.SaveDatabase(DB))
            {
                lblStatus.Text = "Cannnot save the database";
                timer.Start();
            }
            else
            {
                lblStatus.Text = "The database has been saved";
                timer.Start();
            }

            frm_Saving.Close();
            Cursor = oldCursor;
        }

        private void MenuBar_SaveDatabaseAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog DialogSave = new SaveFileDialog();
            DialogSave.DefaultExt = "pdb";                                                                   // Default extension
            DialogSave.Filter = "Database file (*.pdb)|*.pdb|All files (*.*)|*.*";              // add extension to dialog
            DialogSave.AddExtension = true;                                                                // enable adding extension
            DialogSave.RestoreDirectory = true;                                                           // Tu dong phuc hoi duong dan cho lan sau
            DialogSave.Title = "Save as...";
            DialogSave.InitialDirectory = clsProcess.GetRootPath(AppDomain.CurrentDomain.BaseDirectory.ToString());
            DialogSave.SupportMultiDottedExtensions = true;
            try
            {
                if (DialogSave.ShowDialog() == DialogResult.OK)
                {
                    DB.ChangeDir(DialogSave.FileName);
                    PRDB_SQLite.Program.dbName = DB.DBName;
                    PRDB_SQLite.Program.ConnectionString = DB.ConnectionString;

                    string oldName = DB.DBName;
                    PRDB_SQLite.Program.dbShowName = "DB_" + (oldName.Remove(oldName.Length - 4)).ToUpper();

                    NodeDB.Text = PRDB_SQLite.Program.dbShowName;
                    NodeDB.ToolTipText = "Database " + PRDB_SQLite.Program.dbShowName;

                    Cursor oldCursor = Cursor;
                    Cursor = Cursors.WaitCursor;

                    frm_Saving = new Form_Saving();
                    frm_Saving.Show();
                    frm_Saving.Refresh();

                    if (!clsProcess.SaveDatabase(DB))
                    {
                        lblStatus.Text = "Cannnot save the database";
                        timer.Start();
                    }
                    else
                    {
                        lblStatus.Text = "The database has been saved";
                        timer.Start();
                    }

                    frm_Saving.Close();
                    Cursor = oldCursor;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            DialogSave.Dispose();
        }

        private void MenuBar_CloseDatabase_Click(object sender, EventArgs e)
        {
            if (DB != null && !DB.isNotEdited())
            {
                DialogResult result = MessageBox.Show("Do you want to save this database ?", "Close database " + DB.DBName + "...", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (!clsProcess.SaveDatabase(DB))
                    {
                        lblStatus.Text = "Cannnot save the database";
                        timer.Start();
                    }
                    else
                    {
                        lblStatus.Text = "Database has been saved";
                        timer.Start();
                    }

                    TreeView.Nodes.Clear();
                    DB = null;
                }
                else 
                {
                    TreeView.Nodes.Clear();
                    DB = null;
                }
            }
            else
            {
                TreeView.Nodes.Clear();
                DB = null;
            }

            ReloadMainForm();
        }

        private void MenuBar_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region * Schema

        private void MenuSchema_CreateNew_Click(object sender, EventArgs e)
        {
            try
            {
                //if (DB == null)
                //    throw new Exception("Cannot find the Database");
                //PRDB_SQLite.Program.SchemaName = clsProcess.GetListOfSchema(DB);
                //frm_NewSchema = new Form_NewSchema();
                //frm_NewSchema.Show();
                //frm_NewSchema.Disposed += new EventHandler(frm_NewSchema_Disposed);
                xtraTabDatabase.TabPages[0].Show();
                xtraTabDatabase.TabPages[0].Text = "Create Scheme...";
                GridViewDesign.Rows.Clear();
                if (GridViewDesign.CurrentRow != null)
                    lblDesignRowNumber.Text = (GridViewDesign.CurrentRow.Index + 1).ToString() + " / " + GridViewDesign.Rows.Count.ToString();
                else lblDesignRowNumber.Text = "1 / " + GridViewDesign.Rows.Count.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void frm_NewSchema_Disposed(object sender, EventArgs e)
        {
            try
            {
                string SchemaName = frm_NewSchema.SchemaName;
                if (SchemaName != null)
                {
                    ProbSchema NewSchema = new ProbSchema(SchemaName);
                    DB.Schemas.Add(NewSchema);
                    NewNode = new TreeNode();
                    NewNode.Text = SchemaName;
                    NewNode.ToolTipText = "Schema " + SchemaName;
                    NewNode.ContextMenuStrip = ContextMenu_SchemaNode;
                    NewNode.ImageIndex = Schema_ImgIndex.UnselectedState;
                    NewNode.SelectedImageIndex = Schema_ImgIndex.UnselectedState;
                    NodeSchema.Nodes.Add(NewNode);

                    if (SavingNewSchema)
                    {
                        xtraTabDatabase.TabPages[0].Text = "Create Schema " + SchemaName;

                        int nRow = GridViewDesign.Rows.Count - 1;
                        DataRow NewRow;
                        DataColumn NewColumn;

                        for (int i = 0; i < 5; i++)
                        {
                            NewColumn = new DataColumn();
                            NewColumn.ColumnName = i.ToString();
                            NewColumn.DataType = typeof(string);
                            NewSchema.txtSchema.Columns.Add(NewColumn);
                        }

                        ProbAttribute attribute;
                        for (int i = 0; i < nRow; i++)
                        {
                            attribute = new ProbAttribute();
                            attribute.Name = GridViewDesign.Rows[i].Cells[1].Value.ToString();
                            attribute.PrimaryKey = (GridViewDesign.Rows[i].Cells[0].Value == null ? false : true);
                            attribute.Type.TypeName = GridViewDesign.Rows[i].Cells[2].Value.ToString();
                            attribute.Type._DataType = attribute.Type.GetDataType();
                            attribute.Type.ValueType = GridViewDesign.Rows[i].Cells[3].Value.ToString();
                            attribute.Description = (GridViewDesign.Rows[i].Cells[4].Value == null ? "" : GridViewDesign.Rows[i].Cells[4].Value.ToString());

                            NewSchema.Attributes.Add(attribute);

                            NewRow = NewSchema.txtSchema.Rows.Add();

                            NewRow[0] = (GridViewDesign.Rows[i].Cells[0].Value == null ? "false" : "true");
                            NewRow[1] = GridViewDesign.Rows[i].Cells[1].Value.ToString();
                            NewRow[2] = GridViewDesign.Rows[i].Cells[2].Value.ToString();
                            NewRow[3] = GridViewDesign.Rows[i].Cells[3].Value.ToString();
                            NewRow[4] = (GridViewDesign.Rows[i].Cells[4].Value == null ? "" : GridViewDesign.Rows[i].Cells[4].Value.ToString());
                        }

                        CurrentSchema = NewSchema;
                        SavingNewSchema = false;
                    }

                    NewSchema.NewCreated = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuSchema_SaveSchema_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                if (xtraTabDatabase.TabPages[0].Text.Equals("Create Schema..."))
                {
                    frm_NewSchema = new Form_NewSchema();
                    frm_NewSchema.Show();
                    frm_NewSchema.Disposed += new EventHandler(frm_NewSchema_Disposed);
                    SavingNewSchema = true;
                }
                else
                {
                    string SchemaName = CurrentSchema.SchemaName;
                    xtraTabDatabase.TabPages[0].Text = "Create Schema " + SchemaName;

                    int nRow = GridViewDesign.Rows.Count - 1;
                    DataRow NewRow;
                    DataColumn NewColumn;

                    for (int i = 0; i < 5; i++)
                    {
                        NewColumn = new DataColumn();
                        NewColumn.ColumnName = i.ToString();
                        NewColumn.DataType = typeof(string);
                        CurrentSchema.txtSchema.Columns.Add(NewColumn);
                    }

                    ProbAttribute attribute;
                    for (int i = 0; i < nRow; i++)
                    {
                        attribute = new ProbAttribute();
                        attribute.Name = GridViewDesign.Rows[i].Cells[1].Value.ToString();
                        attribute.PrimaryKey = (GridViewDesign.Rows[i].Cells[0].Value == null ? false : true);
                        attribute.Type.TypeName = GridViewDesign.Rows[i].Cells[2].Value.ToString();
                        attribute.Type._DataType = attribute.Type.GetDataType();
                        attribute.Type.ValueType = GridViewDesign.Rows[i].Cells[3].Value.ToString();
                        attribute.Description = (GridViewDesign.Rows[i].Cells[4].Value == null ? "" : GridViewDesign.Rows[i].Cells[4].Value.ToString());

                        CurrentSchema.Attributes.Add(attribute);

                        NewRow = CurrentSchema.txtSchema.Rows.Add();

                        NewRow[0] = (GridViewDesign.Rows[i].Cells[0].Value == null ? "false" : "true");
                        NewRow[1] = GridViewDesign.Rows[i].Cells[1].Value.ToString();
                        NewRow[2] = GridViewDesign.Rows[i].Cells[2].Value.ToString();
                        NewRow[3] = GridViewDesign.Rows[i].Cells[3].Value.ToString();
                        NewRow[4] = (GridViewDesign.Rows[i].Cells[4].Value == null ? "" : GridViewDesign.Rows[i].Cells[4].Value.ToString());
                    }
                    CurrentSchema.Edited = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuSchema_Open_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                PRDB_SQLite.Program.SchemaName = clsProcess.GetListOfSchema(DB);
                frm_OpenSchema = new Form_OpenSchema();
                frm_OpenSchema.Show();
                frm_OpenSchema.Disposed += new EventHandler(frm_OpenSchema_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void frm_OpenSchema_Disposed(object sender, EventArgs e)
        {
            string SchemaName = frm_OpenSchema.SchemaName;
            if (SchemaName != null)
            {
                CurrentSchema = DB.GetSchema(SchemaName);
                try
                {
                    xtraTabDatabase.Show();
                    xtraTabQuery.Hide();
                    xtraTabDatabase.TabPages[0].Text = "Create Schema " + SchemaName;
                    xtraTabDatabase.TabPages[0].PageVisible = true;  // tab Design is shown
                    xtraTabDatabase.TabPages[0].Show();

                    GridViewDesign.Rows.Clear();
                    lblDesignRowNumber.Text = "1 / 1";

                    int nRow = CurrentSchema.txtSchema.Rows.Count;

                    for (int i = 0; i < nRow; i++)
                    {
                        GridViewDesign.Rows.Add();
                        GridViewDesign.Rows[i].Cells[0].Value = CurrentSchema.txtSchema.Rows[i][0].ToString().Equals("true");
                        GridViewDesign.Rows[i].Cells[1].Value = CurrentSchema.txtSchema.Rows[i][1].ToString();
                        GridViewDesign.Rows[i].Cells[2].Value = CurrentSchema.txtSchema.Rows[i][2].ToString();
                        GridViewDesign.Rows[i].Cells[3].Value = CurrentSchema.txtSchema.Rows[i][3].ToString();
                        GridViewDesign.Rows[i].Cells[4].Value = (CurrentSchema.txtSchema.Rows[i][4] != null ? CurrentRelation.txtSchema.Rows[i][4].ToString() : null);
                    }

                    if (GridViewDesign.CurrentRow != null)
                        lblDesignRowNumber.Text = (GridViewDesign.CurrentRow.Index + 1).ToString() + GridViewDesign.Rows.Count.ToString();
                    else lblDesignRowNumber.Text = "1 / " + GridViewDesign.Rows.Count.ToString();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void MenuSchema_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                PRDB_SQLite.Program.SchemaName = clsProcess.GetListOfSchema(DB);
                frm_DelSchema = new Form_DeleteSchema();
                frm_DelSchema.Show();
                frm_DelSchema.Disposed += new EventHandler(frm_DelSchema_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void frm_DelSchema_Disposed(object sender, EventArgs e)
        {
            try
            {
                string SchemaName = frm_DelSchema.SchemaName;
                if (SchemaName != null)
                {
                    ProbSchema DeleteSchema = DB.GetSchema(SchemaName);

                    if (DeleteSchema.Relations.Count > 0)
                        throw new Exception("Cannot delete this schema because it is inherited");

                    DialogResult result = new DialogResult();
                    result = MessageBox.Show("Are you sure want to delete this schema ?", "Delete schema " + SchemaName, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {

                        xtraTabDatabase.TabPages[0].Text = "Create Schema...";

                        if (DeleteSchema == null)
                            throw new Exception("The schema does not exist in database");

                        DB.Schemas.Remove(DeleteSchema);
                        DeleteSchema = null;
                        TreeNode DeleteNode = NodeSchema.Nodes[SchemaName];
                        DeleteNode.Remove();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        #endregion

        #region * Relation

        private void MenuRelation_New_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Database has not been created");
                if (DB.Schemas.Count == 0)
                    throw new Exception("Cannot find related Scheme");
                PRDB_SQLite.Program.SchemaName = clsProcess.GetListOfSchema(DB);
                PRDB_SQLite.Program.RelationName = clsProcess.GetListOfRelation(DB);
                frm_NewRelation = new Form_NewRelation();
                frm_NewRelation.Show();
                frm_NewRelation.Disposed += new EventHandler(frm_NewRelation_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuRelation_OpenRelation_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                PRDB_SQLite.Program.RelationName = clsProcess.GetListOfRelation(DB);
                frm_OpenRelation = new Form_OpenRelation();
                frm_OpenRelation.Show();
                frm_OpenRelation.Disposed += new EventHandler(frm_OpenRelation_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuRelation_SaveRelation_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                if (xtraTabDatabase.TabPages[0].Text.Equals("Create Relation..."))
                {
                    frm_NewRelation = new Form_NewRelation();
                    frm_NewRelation.Show();
                    frm_NewRelation.Disposed += new EventHandler(frm_NewRelation_Disposed);
                    SavingNewRelation = true;
                }
                else
                {
                    string RelationName = CurrentRelation.RelationName;
                    xtraTabDatabase.TabPages[0].Text = "Create Relation " + RelationName;

                    int nRow, nCol;
                    nRow = GridViewData.Rows.Count - 1;
                    nCol = GridViewData.Columns.Count;
                    ProbTuple tuple;
                    ProbTriple triple;
                    CurrentRelation.Data = new List<ProbTuple>();

                    for (int i = 0; i < nRow; i++)
                    {
                        tuple = new ProbTuple();
                        for (int j = 0; j < nCol; j++)
                        {
                            triple = new ProbTriple(GridViewData.Rows[i].Cells[j].Value.ToString());
                            tuple.Triples.Add(triple);
                        }
                        CurrentRelation.Data.Add(tuple);
                    }

                    CurrentRelation.Edited = true;
                    CurrentRelation.Schema.Edited = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void frm_OpenRelation_Disposed(object sender, EventArgs e)
        {
            try
            {
                string RelationName = frm_OpenRelation.RelationName;
                if (RelationName != null)
                {
                    CurrentRelation = DB.GetRelation(RelationName);
                    xtraTabDatabase.TabPages[1].Text = "Create Relation " + RelationName;
                    xtraTabDatabase.Show();
                    xtraTabDatabase.TabPages[1].PageVisible = true;  // tab Data is shown
                    xtraTabDatabase.TabPages[1].Show();
                    xtraTabQuery.Hide();

                    GridViewData.Rows.Clear();
                    GridViewData.Columns.Clear();

                    int nRow = CurrentRelation.Data.Count;
                    int nCol = CurrentRelation.Attributes.Count;

                    for (int i = 0; i < nCol; i++)      // Make GridViewData Column
                    {
                        GridViewData.Columns.Add("Column " + i.ToString(), CurrentRelation.Attributes[i].Name);
                    }

                    ProbTuple tuple;

                    for (int i = 0; i < nRow; i++)      // Assign data for GridViewData
                    {
                        tuple = CurrentRelation.Data[i];
                        GridViewData.Rows.Add();
                        for (int j = 0; j < nCol; j++)
                            GridViewData.Rows[i].Cells[j].Value = tuple.Triples[j].GetStrValue();
                    }

                    if (GridViewData.CurrentRow != null)
                        lblDataRowNumber.Text = (GridViewData.CurrentRow.Index + 1).ToString() + " / " + GridViewData.Rows.Count.ToString();
                    else lblDataRowNumber.Text = "1 / " + GridViewData.Rows.Count.ToString();

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuRelation_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                PRDB_SQLite.Program.RelationName = clsProcess.GetListOfRelation(DB);
                frm_DelRelation = new Form_DeleteRelation();
                frm_DelRelation.Show();
                frm_DelRelation.Disposed += new EventHandler(frm_DelRelation_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void frm_DelRelation_Disposed(object sender, EventArgs e)
        {
            try
            {
                string RelationName = frm_DelRelation.RelationName;
                if (RelationName != null)
                {
                    DialogResult result = new DialogResult();
                    result = MessageBox.Show("Are you sure want to delete this relation ?", "Delete relation " + RelationName, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {

                        CurrentRelation = DB.GetRelation(RelationName);
                        ProbSchema ParentSchema = CurrentRelation.Schema;
                        ParentSchema.Relations.Remove(CurrentRelation);
                        ParentSchema.Edited = true;
                        DB.Relations.Remove(CurrentRelation);
                        CurrentRelation = null;
                        TreeNode DeleteNode = NodeRelation.Nodes[RelationName];
                        NodeRelation.Nodes.Remove(DeleteNode);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        #endregion

        #region * View

        private void MenuView_NewView_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                PRDB_SQLite.Program.ViewName = clsProcess.GetListOfView(DB);
                frm_NewView = new Form_NewView();
                frm_NewView.Show();
                frm_NewView.Disposed += new EventHandler(frm_NewView_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuView_SaveView_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                if (GridViewResult.Rows.Count == 0)
                    throw new Exception("The query was not executed");
                if (xtraTabDatabase.TabPages[0].Text.Equals("Input Query...") || xtraTabDatabase.TabPages[0].Text.Contains("Create Query "))
                {
                    frm_NewView = new Form_NewView();
                    frm_NewView.Show();
                    frm_NewView.Disposed += new EventHandler(frm_NewView_Disposed);
                    SavingNewView = true;
                }
                else
                {
                    CurrentView.Query = txtQuery.Text;
                    CurrentView.Relation.Attributes = CurrentView.Schema.Attributes = CurrentQuery.Attributes;

                    int nRow = GridViewResult.Rows.Count;
                    int nCol = GridViewResult.Columns.Count;

                    DataRow NewRow;
                    DataColumn NewColumn;

                    for (int i = 0; i < nCol; i++)
                    {
                        NewColumn = new DataColumn();
                        NewColumn.ColumnName = i.ToString();
                        NewColumn.DataType = typeof(string);
                        CurrentView.Relation.txtSchema.Columns.Add(NewColumn);
                    }

                    for (int i = 0; i < nRow; i++)
                    {
                        NewRow = CurrentView.Relation.txtSchema.Rows.Add();

                        for (int j = 0; j < nCol; j++)
                        {
                            NewRow[j] = GridViewResult.Rows[i].Cells[j].Value.ToString();
                        }
                    }

                    ProbTuple tuple;
                    ProbTriple triple;
                    CurrentView.Relation.Data = new List<ProbTuple>();

                    for (int i = 0; i < nRow; i++)
                    {
                        tuple = new ProbTuple();
                        for (int j = 0; j < nCol; j++)
                        {
                            triple = new ProbTriple(GridViewResult.Rows[i].Cells[j].Value.ToString());
                            tuple.Triples.Add(triple);
                        }
                        CurrentView.Relation.Data.Add(tuple);
                    }

                    CurrentView.Edited = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuView_OpenView_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                PRDB_SQLite.Program.ViewName = clsProcess.GetListOfView(DB);
                frm_OpenView = new Form_OpenView();
                frm_OpenView.Show();
                frm_OpenView.Disposed += new EventHandler(frm_OpenView_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void frm_OpenView_Disposed(object sender, EventArgs e)
        {
            try
            {
                string ViewName = frm_OpenView.ViewName;
                if (ViewName != null)
                {
                    xtraTabQuery.TabPages[0].Text = "Create View " + ViewName;

                    CurrentView = DB.GetView(ViewName);
                    xtraTabDatabase.Hide();
                    xtraTabQuery.Show();

                    GridViewResult.Rows.Clear();
                    GridViewResult.Columns.Clear();

                    int nRow = CurrentView.Relation.Data.Count;
                    int nCol = CurrentView.Schema.Attributes.Count;

                    for (int i = 0; i < nCol; i++)      // Make GridViewData Column
                    {
                        GridViewResult.Columns.Add("Column " + i.ToString(), CurrentView.Schema.Attributes[i].Name);
                    }

                    ProbTuple tuple;

                    for (int i = 0; i < nRow; i++)      // Assign data for GridViewResult
                    {
                        tuple = CurrentView.Relation.Data[i];
                        GridViewData.Rows.Add();
                        for (int j = 0; j < nCol; j++)
                            GridViewResult.Rows[i].Cells[j].Value = tuple.Triples[j].GetStrValue();
                    }

                    if (GridViewData.CurrentRow != null)
                        lblDataRowNumber.Text = (GridViewData.CurrentRow.Index + 1).ToString() + " / " + GridViewData.Rows.Count.ToString();
                    else lblDataRowNumber.Text = "1 / " + GridViewData.Rows.Count.ToString();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuView_DeleteView_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                PRDB_SQLite.Program.ViewName = clsProcess.GetListOfView(DB);
                frm_DelView = new Form_DeleteView();
                frm_DelView.Show();
                frm_DelView.Disposed += new EventHandler(frm_DelView_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void frm_DelView_Disposed(object sender, EventArgs e)
        {
            try
            {
                string ViewName = frm_DelView.ViewName;
                if (ViewName != null)
                {
                    DialogResult result = new DialogResult();
                    result = MessageBox.Show("Are you sure want to delete this view ?", "Delete view " + ViewName, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        xtraTabQuery.TabPages[0].Text = "Input Query...";
                        ProbView DeleteView = DB.GetView(ViewName);
                        if (DeleteView == null)
                            throw new Exception("The view does not exist in database");
                        DB.Views.Remove(DeleteView);
                        DeleteView = null;
                        TreeNode DeleteNode = NodeView.Nodes[ViewName];
                        DeleteNode.Remove();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        #endregion

        #region * Query

        private void MenuQuery_New_Click(object sender, EventArgs e)
        {
            try
            {
                //if (DB == null)
                //    throw new Exception("Cannot find the Database");
                //PRDB_SQLite.Program.QueryName = clsProcess.GetListOfQuery(DB);
                //frm_NewQuery = new Form_NewQuery();
                //frm_NewQuery.Show();
                //frm_NewQuery.Disposed += new EventHandler(frm_NewQuery_Disposed);
                xtraTabQuery.Show();
                xtraTabQuery.TabPages[0].Text = "Input Query...";
                xtraTabResult.SelectedTabPage = xtraTabResult.TabPages[0];
                txtQuery.Clear();
                txtMessage.Clear();
                GridViewResult.Rows.Clear();
                GridViewResult.Columns.Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void frm_NewQuery_Disposed(object sender, EventArgs e)
        {
            try
            {
                string QueryName = frm_NewQuery.QueryName;
                if (QueryName != null)
                {
                    ProbQuery NewQuery = new ProbQuery(txtQuery.Text);
                    NewQuery.QueryName = QueryName;
                    DB.Queries.Add(NewQuery);

                    NewNode = new TreeNode();
                    NewNode.Text = QueryName;
                    NewNode.ContextMenuStrip = contextMenu_QueryNode;
                    NewNode.ImageIndex = Query_ImgIndex.UnselectedState;
                    NewNode.SelectedImageIndex = Query_ImgIndex.SelectedState;
                    NodeQuery.Nodes.Add(NewNode);

                    if (SavingNewQuery)
                    {
                        xtraTabQuery.TabPages[0].Text = "Create Query " + QueryName;
                        NewQuery.QueryText = txtQuery.Text;
                        CurrentQuery = NewQuery;
                        SavingNewQuery = false;
                    }

                    NewQuery.NewCreated = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuQuery_Open_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                PRDB_SQLite.Program.QueryName = clsProcess.GetListOfQuery(DB);
                frm_OpenQuery = new Form_OpenQuery();
                frm_OpenQuery.Show();
                frm_OpenQuery.Disposed += new EventHandler(frm_OpenQuery_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void frm_OpenQuery_Disposed(object sender, EventArgs e)
        {
            try
            {
                string QueryName = frm_OpenQuery.QueryName;
                if (QueryName != null)
                {
                    CurrentQuery = DB.GetQuery(QueryName);
                    xtraTabDatabase.Hide();
                    xtraTabQuery.Show();
                    xtraTabQuery.TabPages[0].Text = "Input Query " + QueryName;
                    txtQuery.Text = CurrentQuery.QueryText;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuQuery_SaveQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                if (xtraTabQuery.TabPages[0].Text.Equals("Input Query...") || xtraTabQuery.TabPages[0].Text.Contains("Create View "))
                {
                    frm_NewQuery = new Form_NewQuery();
                    frm_NewQuery.Show();
                    frm_NewQuery.Disposed += new EventHandler(frm_NewQuery_Disposed);
                    SavingNewQuery = true;
                }
                else
                {
                    string QueryName = CurrentQuery.QueryName;
                    xtraTabQuery.TabPages[0].Text = "Create Query " + QueryName;
                    CurrentQuery.QueryText = txtQuery.Text;
                    CurrentQuery.Edited = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuQuery_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                PRDB_SQLite.Program.QueryName = clsProcess.GetListOfQuery(DB);
                frm_DelQuery = new Form_DeleteQuery();
                frm_DelQuery.Show();
                frm_DelQuery.Disposed += new EventHandler(frm_DelQuery_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void frm_DelQuery_Disposed(object sender, EventArgs e)
        {
            try
            {
                string QueryName = frm_DelQuery.QueryName;
                if (QueryName != null)
                {
                    DialogResult result = new DialogResult();
                    result = MessageBox.Show("Are you sure want to delete this query ?", "Delete query " + QueryName, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        xtraTabQuery.TabPages[0].Text = "Input Query...";
                        ProbQuery DeleteQuery = DB.GetQuery(QueryName);
                        if (DeleteQuery == null)
                            throw new Exception("The query does not exist in the database");
                        DB.Queries.Remove(DeleteQuery);
                        DeleteQuery = null;
                        TreeNode DeleteNode = NodeQuery.Nodes[QueryName];
                        DeleteNode.Remove();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void MenuQuery_Execute_Click(object sender, EventArgs e)
        {
            Toolbar_BtnStop.Enabled = true;

            try
            {
                lblStatus.Text = "Executing...";
                GridViewResult.Rows.Clear();
                GridViewResult.Columns.Clear();
                txtMessage.Text = "";

                if (txtQuery.Text == "")
                    throw new Exception("The query is not entered") { };
                string query = txtQuery.SelectedText.Length == 0 ? txtQuery.Text : txtQuery.SelectedText;
                CurrentQuery = new ProbQuery(query.Trim());
                CurrentQuery.Relations = DB.Relations;
                List<ProbTuple> result = new List<ProbTuple>();

                if (CurrentQuery.CheckSyntax())
                    result = CurrentQuery.Execute();

                if (result.Count == 0)
                {
                    txtMessage.Text = "There is no relation satisfy the condition";
                    xtraTabResult.SelectedTabPageIndex = 1;
                }
                else
                {
                    foreach (ProbAttribute attribute in CurrentRelation.Attributes)
                        GridViewResult.Columns.Add(attribute.Name, attribute.Name);

                    for (int i = 0; i < result.Count; i++)
                    {
                        GridViewResult.Rows.Add();
                        for (int j = 0; j < result[i].Triples.Count; j++)
                            GridViewResult.Rows[i].Cells[j].Value = result[i].Triples[j].GetStrValue();
                    }

                    xtraTabResult.SelectedTabPageIndex = 0;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                if (CurrentQuery != null) txtMessage.Text = CurrentQuery.ErrorMessage;
            }
            lblStatus.Text = "Ready";
            Toolbar_BtnStop.Enabled = false;
        }
        #endregion

        #region * About
        private void MenuAbout_Click(object sender, EventArgs e)
        {
            frm_About = new Form_About();
            frm_About.Show();
        }
        #endregion

        #endregion

        #region ** Context Menu

        #region * Database

        private void CTMenuDB_Rename_Click(object sender, EventArgs e)
        {
            frm_RenameDB = new Form_RenameDB();
            frm_RenameDB.Show();
            frm_RenameDB.Disposed += new EventHandler(frm_RenameDB_Disposed);
        }

        void frm_RenameDB_Disposed(object sender, EventArgs e)
        {
            string dbName = frm_RenameDB.dbName;
            if (dbName != null)
            {
                DB.Rename(dbName);

                PRDB_SQLite.Program.dbName = DB.DBName;
                PRDB_SQLite.Program.ConnectionString = DB.ConnectionString;

                PRDB_SQLite.Program.dbShowName = "DB_" + (DB.DBName.Remove(DB.DBName.Length - 4)).ToUpper();

                NodeDB.Text = PRDB_SQLite.Program.dbShowName;
                NodeDB.ToolTipText = "Database " + PRDB_SQLite.Program.dbShowName;
            }
        }

        private void CTMenuDB_CloseDB_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to close this database ?", "Close " + DB.DBName + "...", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                TreeView.Nodes.Clear();
                DB = null;
            }
        }

        #endregion

        #region * Schema
        private void CTMenuSchema_NewSchema_Click(object sender, EventArgs e)
        {
            frm_NewSchema = new Form_NewSchema();
            frm_NewSchema.Show();
            frm_NewSchema.Disposed += new EventHandler(frm_NewSchema_Disposed);
        }

        private void CTMenuSchema_DelSchemas_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsProcess.NotEmptySchemas(DB))
                    throw new Exception("The schemas are not empty");

                DialogResult result = new DialogResult();
                result = MessageBox.Show("Are you sure want to delete all schemas ?", "Delete All Schemas", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    NodeSchema.Nodes.Clear();
                    CurrentSchema = null;
                    DB.Schemas.Clear();
                    if (!xtraTabDatabase.TabPages[0].Text.Equals("Create Schema..."))
                    {
                        GridViewDesign.Rows.Clear();
                        lblDesignRowNumber.Text = "1 / 1";
                        xtraTabDatabase.TabPages[0].Text = "Create Schema...";
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CTMenuSchNode_OpenSchema_Click(object sender, EventArgs e)
        {
            string SchemaName = CurrentNode.Text;
            TreeView.SelectedNode = CurrentNode;
            CurrentSchema = DB.GetSchema(SchemaName);
            try
            {
                if (CurrentSchema == null)
                    throw new Exception("The schema does not exist in the database");

                xtraTabQuery.Hide();
                xtraTabDatabase.Show();
                xtraTabDatabase.SelectedTabPage = xtraTabDatabase.TabPages[0];
                xtraTabDatabase.TabPages[0].Text = "Create Schema " + SchemaName;
                xtraTabDatabase.TabPages[0].PageVisible = true;
                xtraTabDatabase.TabPages[0].Show();

                GridViewDesign.Rows.Clear();
                lblDesignRowNumber.Text = "1 / 1";

                int nRow = CurrentSchema.txtSchema.Rows.Count;
                int nCol = CurrentSchema.txtSchema.Columns.Count;

                for (int i = 0; i < nRow; i++)      // Assign data from DataTable to GridViewDesign
                {
                    GridViewDesign.Rows.Add();
                    GridViewDesign.Rows[i].Cells[0].Value = CurrentSchema.txtSchema.Rows[i][0].ToString().Equals("true");
                    GridViewDesign.Rows[i].Cells[1].Value = CurrentSchema.txtSchema.Rows[i][1].ToString();
                    GridViewDesign.Rows[i].Cells[2].Value = CurrentSchema.txtSchema.Rows[i][2].ToString();
                    GridViewDesign.Rows[i].Cells[3].Value = CurrentSchema.txtSchema.Rows[i][3].ToString();
                    GridViewDesign.Rows[i].Cells[4].Value = (CurrentSchema.txtSchema.Rows[i][4] != null ? CurrentSchema.txtSchema.Rows[i][4].ToString() : null);
                }
                lblDesignRowNumber.Text = "1 / " + GridViewDesign.Rows.Count.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CTMenuSchNode_DeleteSchema_Click(object sender, EventArgs e)
        {
            string SchemaName = CurrentNode.Text;

            try
            {
                ProbSchema DeleteSchema = DB.GetSchema(SchemaName);
                if (DeleteSchema.Relations.Count > 0)
                    throw new Exception("The schema is not empty");

                if (DeleteSchema == null)
                    throw new Exception("The schema does not exist in database");

                DialogResult result = new DialogResult();
                result = MessageBox.Show("Are you sure want to delete this schema ?", "Delete schema " + SchemaName, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    xtraTabDatabase.TabPages[0].Text = "Create Schema...";
                    CurrentNode.Remove();
                    DB.Schemas.Remove(DeleteSchema);
                    DeleteSchema = null;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CTMenuSchNode_NewRelation_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                if (DB.Schemas.Count == 0)
                    throw new Exception("Cannot find related Scheme");
                PRDB_SQLite.Program.SchemaName = clsProcess.GetListOfSchema(DB);
                PRDB_SQLite.Program.RelationName = clsProcess.GetListOfRelation(DB);
                frm_NewRelation = new Form_NewRelation();
                frm_NewRelation.Show();
                frm_NewRelation.Disposed += new EventHandler(frm_NewRelation_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        #endregion

        #region * Relation
        private void CTMenuRelation_NewRelation_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                if (DB.Schemas.Count == 0)
                    throw new Exception("Cannot find related Scheme");
                PRDB_SQLite.Program.SchemaName = clsProcess.GetListOfSchema(DB);
                PRDB_SQLite.Program.RelationName = clsProcess.GetListOfRelation(DB);
                frm_NewRelation = new Form_NewRelation();
                frm_NewRelation.Show();
                frm_NewRelation.Disposed += new EventHandler(frm_NewRelation_Disposed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        void frm_NewRelation_Disposed(object sender, EventArgs e)
        {
            try
            {
                string RelName = frm_NewRelation.RelationName;
                string SchName = frm_NewRelation.SchemaName;

                if (RelName != null)
                {
                    ProbRelation NewRelation = new ProbRelation(RelName);
                    ProbSchema ParentSchema = DB.GetSchema(SchName);
                    NewRelation.Schema = ParentSchema;
                    ParentSchema.Relations.Add(NewRelation);
                    ParentSchema.Edited = true;
                    DB.Relations.Add(NewRelation);

                    NewRelation.Attributes = ParentSchema.Attributes;
                    NewRelation.txtSchema = ParentSchema.txtSchema;

                    NewNode = new TreeNode();
                    NewNode.Text = RelName;
                    NewNode.ToolTipText = "Relation " + RelName;
                    NewNode.ContextMenuStrip = ContextMenu_RelationNode;
                    NewNode.ImageIndex = Relation_ImgIndex.UnselectedState;
                    NewNode.SelectedImageIndex = Relation_ImgIndex.UnselectedState;
                    NodeRelation.Nodes.Add(NewNode);

                    if (SavingNewRelation)
                    {
                        xtraTabDatabase.TabPages[0].Text = "Create Relation " + RelName;

                        int nRow, nCol;
                        nRow = GridViewData.Rows.Count - 1;
                        nCol = GridViewData.Columns.Count;
                        ProbTuple tuple;
                        ProbTriple triple;
                        NewRelation.Data = new List<ProbTuple>();

                        for (int i = 0; i < nRow; i++)
                        {
                            tuple = new ProbTuple();
                            for (int j = 0; j < nCol; j++)
                            {
                                triple = new ProbTriple(GridViewData.Rows[i].Cells[j].Value.ToString());
                                tuple.Triples.Add(triple);
                            }
                            NewRelation.Data.Add(tuple);
                        }

                        CurrentRelation = NewRelation;
                        SavingNewRelation = false;
                    }

                    NewRelation.NewCreated = true;
                    ParentSchema.NewCreated = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CTMenuRelation_DeleteRelations_Click(object sender, EventArgs e)
        {
            DialogResult result = new DialogResult();
            result = MessageBox.Show("Are you sure want to delete all relations ?", "Delete All Relations", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                NodeRelation.Nodes.Clear();

                ProbSchema ParentSchema = CurrentRelation.Schema;
                ParentSchema.Relations.Remove(CurrentRelation);
                CurrentRelation = null;

                foreach (ProbSchema schema in DB.Schemas)       // Clear all relations in schemas
                    schema.Relations.Clear();

                DB.Relations.Clear();
                if (!xtraTabDatabase.TabPages[1].Text.Equals("Create Relation..."))
                {
                    GridViewData.Rows.Clear();
                    GridViewData.Columns.Clear();
                    xtraTabDatabase.TabPages[1].Text = "Create Relation...";
                }
                ParentSchema.Edited = true;
            }
        }

        private void CTMenuRelNode_OpenRelation_Click(object sender, EventArgs e)
        {
            string RelationName = CurrentNode.Text;
            TreeView.SelectedNode = CurrentNode;
            CurrentRelation = DB.GetRelation(RelationName);

            try
            {
                if (CurrentRelation == null)
                    throw new Exception("The relation does not exist in the database");

                xtraTabQuery.Hide();
                xtraTabDatabase.Show();
                xtraTabDatabase.SelectedTabPage = xtraTabDatabase.TabPages[1];
                xtraTabDatabase.TabPages[1].Text = "Create Relation " + RelationName;
                xtraTabDatabase.TabPages[1].PageVisible = true;
                xtraTabDatabase.TabPages[1].Show();

                GridViewData.Rows.Clear();
                GridViewData.Columns.Clear();

                int nRow = CurrentRelation.Data.Count;
                int nCol = CurrentRelation.Attributes.Count;

                for (int i = 0; i < nCol; i++)      // Make GridViewData Column
                {
                    GridViewData.Columns.Add("Column " + i.ToString(), CurrentRelation.Attributes[i].Name);
                }

                ProbTuple tuple;

                for (int i = 0; i < nRow; i++)      // Assign data for GridViewData
                {
                    tuple = CurrentRelation.Data[i];
                    GridViewData.Rows.Add();
                    for (int j = 0; j < nCol; j++)
                        GridViewData.Rows[i].Cells[j].Value = tuple.Triples[j].GetStrValue();
                }

                if (GridViewData.CurrentRow != null)
                    lblDataRowNumber.Text = (GridViewData.CurrentRow.Index + 1).ToString() + " / " + GridViewData.Rows.Count.ToString();
                else lblDataRowNumber.Text = "1 / " + GridViewData.Rows.Count.ToString();

                Connection clsConnection = new Connection();
                if (!clsConnection.Existed("rel_" + CurrentRelation.RelationName)) CurrentRelation.NewCreated = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CTMenuRelNode_DeleteRelation_Click(object sender, EventArgs e)
        {
            string RelationName = CurrentNode.Text;
            ProbRelation DeleteRelation = DB.GetRelation(RelationName);

            try
            {
                if (DeleteRelation == null)
                    throw new Exception("The relation does not exist in the database");

                DialogResult result = new DialogResult();
                result = MessageBox.Show("Are you sure want to delete this relation ?", "Delete relation " + RelationName, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    xtraTabDatabase.TabPages[1].Text = "Create Relation...";
                    CurrentNode.Remove();
                    ProbSchema schema = DeleteRelation.Schema;
                    schema.Relations.Remove(DeleteRelation);
                    DB.Relations.Remove(DeleteRelation);
                    DeleteRelation = null;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CTMenuRelNode_EditSchema_Click(object sender, EventArgs e)
        {
            string RelationName = CurrentNode.Text;
            CurrentRelation = DB.GetRelation(RelationName);
            string SchemaName = CurrentRelation.Schema.SchemaName;
            CurrentSchema = CurrentRelation.Schema;
            TreeView.SelectedNode = CurrentNode;

            try
            {
                if (CurrentSchema == null)
                    throw new Exception("The schema does not exist in the database");

                xtraTabQuery.Hide();
                xtraTabDatabase.Show();
                xtraTabDatabase.SelectedTabPage = xtraTabDatabase.TabPages[0];
                xtraTabDatabase.TabPages[0].Text = "Create Schema " + SchemaName;
                xtraTabDatabase.TabPages[0].PageVisible = true;
                xtraTabDatabase.TabPages[0].Show();

                GridViewDesign.Rows.Clear();
                lblDesignRowNumber.Text = "1 / 1";

                int nRow = CurrentSchema.txtSchema.Rows.Count;
                int nCol = CurrentSchema.txtSchema.Columns.Count;

                for (int i = 0; i < nRow; i++)      // Assign data from DataTable to GridViewDesign
                {
                    GridViewDesign.Rows.Add();
                    GridViewDesign.Rows[i].Cells[0].Value = CurrentSchema.txtSchema.Rows[i][0].ToString().Equals("true");
                    GridViewDesign.Rows[i].Cells[1].Value = CurrentSchema.txtSchema.Rows[i][1].ToString();
                    GridViewDesign.Rows[i].Cells[2].Value = CurrentSchema.txtSchema.Rows[i][2].ToString();
                    GridViewDesign.Rows[i].Cells[3].Value = CurrentSchema.txtSchema.Rows[i][3].ToString();
                    GridViewDesign.Rows[i].Cells[4].Value = (CurrentSchema.txtSchema.Rows[i][4] != null ? CurrentSchema.txtSchema.Rows[i][4].ToString() : null);
                }

                if (GridViewDesign.CurrentRow != null)
                    lblDesignRowNumber.Text = (GridViewDesign.CurrentRow.Index + 1).ToString() + " / " + GridViewDesign.Rows.Count.ToString();
                else lblDesignRowNumber.Text = "1 / " + GridViewDesign.Rows.Count.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        #endregion

        #region * View
        private void CTMenuView_NewView_Click(object sender, EventArgs e)
        {
            frm_NewView = new Form_NewView();
            frm_NewView.Show();
            frm_NewView.Disposed += new EventHandler(frm_NewView_Disposed);
        }

        void frm_NewView_Disposed(object sender, EventArgs e)
        {
            try
            {
                string ViewName = frm_NewView.ViewName;
                if (ViewName != null)
                {
                    ProbView NewView = new ProbView(ViewName);
                    DB.Views.Add(NewView);

                    NewNode = new TreeNode();
                    NewNode.Text = ViewName;
                    NewNode.ToolTipText = "View " + ViewName;
                    NewNode.ContextMenuStrip = ContextMenu_ViewNode;
                    NewNode.ImageIndex = View_ImgIndex.UnselectedState;
                    NewNode.SelectedImageIndex = View_ImgIndex.SelectedState;
                    NodeView.Nodes.Add(NewNode);

                    if (SavingNewView)
                    {
                        xtraTabQuery.TabPages[0].Text = "Create View " + ViewName;

                        NewView.Query = txtQuery.Text;
                        NewView.Relation.Attributes = NewView.Schema.Attributes = CurrentQuery.Attributes;

                        int nRow = GridViewResult.Rows.Count;
                        int nCol = GridViewResult.Columns.Count;

                        DataRow NewRow;
                        DataColumn NewColumn;

                        for (int i = 0; i < nCol; i++)
                        {
                            NewColumn = new DataColumn();
                            NewColumn.ColumnName = i.ToString();
                            NewColumn.DataType = typeof(string);
                            NewView.Relation.txtSchema.Columns.Add(NewColumn);
                        }

                        for (int i = 0; i < nRow; i++)
                        {
                            NewRow = NewView.Relation.txtSchema.Rows.Add();

                            for (int j = 0; j < nCol; j++)
                            {
                                NewRow[j] = GridViewResult.Rows[i].Cells[j].Value.ToString();
                            }
                        }

                        ProbTuple tuple;
                        ProbTriple triple;
                        NewView.Relation.Data = new List<ProbTuple>();

                        for (int i = 0; i < nRow; i++)
                        {
                            tuple = new ProbTuple();
                            for (int j = 0; j < nCol; j++)
                            {
                                triple = new ProbTriple(GridViewResult.Rows[i].Cells[j].Value.ToString());
                                tuple.Triples.Add(triple);
                            }
                            NewView.Relation.Data.Add(tuple);
                        }

                        CurrentView = NewView;
                        SavingNewView = false;
                    }

                    NewView.NewCreated = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CTMenuView_DeleteViews_Click(object sender, EventArgs e)
        {
            DialogResult result = new DialogResult();
            result = MessageBox.Show("Are you sure want to delete all views ?", "Delete All Views", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                NodeView.Nodes.Clear();
                CurrentView = null;
                DB.Views.Clear();
                if (xtraTabQuery.TabPages[0].Text.Contains("Create View"))
                {
                    GridViewResult.Rows.Clear();
                    GridViewResult.Columns.Clear();
                    txtQuery.Clear();
                    xtraTabQuery.TabPages[0].Text = "Input Query...";
                }
            }
        }

        private void CTMenuViewNode_OpenView_Click(object sender, EventArgs e)
        {
            string ViewName = CurrentNode.Text;
            TreeView.SelectedNode = CurrentNode;
            CurrentView = DB.GetView(ViewName);
            try
            {
                if (CurrentView == null)
                    throw new Exception("The view does not exist in the database");

                xtraTabQuery.Show();
                xtraTabDatabase.Hide();
                xtraTabQuery.TabPages[0].Text = "Create View " + ViewName;

                GridViewResult.Rows.Clear();
                GridViewResult.Columns.Clear();

                int nRow = CurrentView.Relation.Data.Count;
                int nCol = CurrentView.Schema.Attributes.Count;

                txtQuery.Text = CurrentView.Query;

                for (int i = 0; i < nCol; i++)      // Make GridViewResult Column
                {
                    GridViewResult.Columns.Add("Column " + i.ToString(), CurrentView.Schema.Attributes[i].Name);
                }

                ProbTuple tuple;

                for (int i = 0; i < nRow; i++)      // Assign data for GridViewResult
                {
                    tuple = CurrentView.Relation.Data[i];
                    GridViewResult.Rows.Add();
                    for (int j = 0; j < nCol; j++)
                        GridViewResult.Rows[i].Cells[j].Value = tuple.Triples[j].GetStrValue();
                }

                Connection clsConnection = new Connection();
                if (!clsConnection.Existed("sch_" + CurrentView.ViewName)) CurrentView.NewCreated = true;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CTMenuViewNode_DeleteView_Click(object sender, EventArgs e)
        {
            string ViewName = CurrentNode.Text;
            ProbView DeleteView = DB.GetView(ViewName);
            xtraTabQuery.TabPages[0].Text = "Input Query...";

            try
            {
                if (DeleteView == null)
                    throw new Exception("The view does not exist in the database");

                DialogResult result = new DialogResult();
                result = MessageBox.Show("Are you sure want to delete this view ?", "Delete view " + ViewName, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    CurrentNode.Remove();
                    DB.Views.Remove(DeleteView);
                    DeleteView = null;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        #endregion

        #region * Query
        private void CTMenuQuery_NewQuery_Click(object sender, EventArgs e)
        {
            frm_NewQuery = new Form_NewQuery();
            frm_NewQuery.Show();
            frm_NewQuery.Disposed += new EventHandler(frm_NewQuery_Disposed);
        }

        private void CTMenuQuery_DeleteQueries_Click(object sender, EventArgs e)
        {
            DialogResult result = new DialogResult();
            result = MessageBox.Show("Are you sure want to delete all queries ?", "Delete All Queries", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                NodeQuery.Nodes.Clear();
                CurrentQuery = null;
                DB.Queries.Clear();
                if (!xtraTabQuery.TabPages[0].Text.Equals("Input Query..."))
                {
                    txtQuery.Clear();
                    xtraTabQuery.TabPages[0].Text = "Input Query...";
                }
            }
        }

        private void CTMenuQueryNode_OpenQuery_Click(object sender, EventArgs e)
        {
            string QueryName = CurrentNode.Text;
            TreeView.SelectedNode = CurrentNode;
            CurrentQuery = DB.GetQuery(QueryName);
            try
            {
                if (CurrentQuery == null)
                    throw new Exception("The query does not exist in the database");

                xtraTabQuery.Show();
                xtraTabDatabase.Hide();
                xtraTabQuery.TabPages[0].Text = "Create Query " + QueryName;

                txtQuery.Text = CurrentQuery.QueryText;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CTMenuQuery_DeleteQuery_Click(object sender, EventArgs e)
        {
            string QueryName = CurrentNode.Text;
            ProbQuery DeleteQuery = DB.GetQuery(QueryName);
            xtraTabQuery.TabPages[0].Text = "Input Query...";

            try
            {
                if (DeleteQuery == null)
                    throw new Exception("The query does not exist in database");

                DialogResult result = new DialogResult();
                result = MessageBox.Show("Are you sure want to delete this query ?", "Delete query " + QueryName, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    CurrentNode.Remove();
                    DB.Queries.Remove(DeleteQuery);
                    DeleteQuery = null;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        #endregion

        #region * Query Editor
        private void CTMenuQueryEditor_Conj_ig_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊗_ig";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊗_ig");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void CTMenuQueryEditor_Conj_in_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊗_in";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊗_in");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void CTMenuQueryEditor_Conj_me_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊗_me";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊗_me");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void CTMenuQueryEditor_Disj_ig_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊕_ig";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊕_ig");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void CTMenuQueryEditor_Disj_in_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊕_in";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊕_in");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void CTMenuQueryEditor_Disj_me_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊕_me";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊕_me");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void CTMenuQueryEditor_Diff_ig_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊖_ig";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊖_ig");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void CTMenuQueryEditor_Diff_in_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊖_in";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊖_in");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void CTMenuQueryEditor_Diff_me_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊖_me";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊖_me");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void CTMenuQueryEditor_Equal_ig_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"EQUAL(ig)";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"EQUAL(ig)");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 9;
        }

        private void CTMenuQueryEditor_Equal_in_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"EQUAL(in)";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"EQUAL(in)");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 9;
        }

        private void CTMenuQueryEditor_Equal_me_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"EQUAL(me)";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"EQUAL(me)");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 9;
        }

        private void CTMenuQueryEditor_Execute_Click(object sender, EventArgs e)
        {
            Toolbar_BtnStop.Enabled = true;

            try
            {
                lblStatus.Text = "Executing...";
                GridViewResult.Rows.Clear();
                GridViewResult.Columns.Clear();
                txtMessage.Text = "";

                if (txtQuery.Text == "")
                    throw new Exception("The query is not entered") { };
                string query = txtQuery.SelectedText.Length == 0 ? txtQuery.Text : txtQuery.SelectedText;
                CurrentQuery = new ProbQuery(query.Trim());
                CurrentQuery.Relations = DB.Relations;
                List<ProbTuple> result = new List<ProbTuple>();

                if (CurrentQuery.CheckSyntax())
                    result = CurrentQuery.Execute();

                if (result.Count == 0)
                {
                    txtMessage.Text = "There is no relation satisfy the condition";
                    xtraTabResult.SelectedTabPageIndex = 1;
                }
                else
                {
                    foreach (ProbAttribute attribute in CurrentRelation.Attributes)
                        GridViewResult.Columns.Add(attribute.Name, attribute.Name);

                    for (int i = 0; i < result.Count; i++)
                    {
                        GridViewResult.Rows.Add();
                        for (int j = 0; j < result[i].Triples.Count; j++)
                            GridViewResult.Rows[i].Cells[j].Value = result[i].Triples[j].GetStrValue();
                    }

                    xtraTabResult.SelectedTabPageIndex = 0;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                if (CurrentQuery != null) txtMessage.Text = CurrentQuery.ErrorMessage;
            }
            lblStatus.Text = "Ready";
            Toolbar_BtnStop.Enabled = false;
        }

        #endregion

        #endregion

        #region ** ToolBar

        #region * System
        private void Toolbar_BtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog DialogSave = new SaveFileDialog();                            // Save dialog
                DialogSave.DefaultExt = "pdb";                                                            // Default extension
                DialogSave.Filter = "Database file (*.pdb)|*.pdb|All files (*.*)|*.*";       // add extension to dialog
                DialogSave.AddExtension = true;                                                      // enable adding extension
                DialogSave.RestoreDirectory = true;                                                 // Tu dong phuc hoi duong dan cho lan sau
                DialogSave.Title = "Create new database...";
                DialogSave.InitialDirectory = clsProcess.GetRootPath(AppDomain.CurrentDomain.BaseDirectory.ToString());
                DialogSave.SupportMultiDottedExtensions = true;
                if (DialogSave.ShowDialog() == DialogResult.OK)
                {
                    DB = null;
                    TreeView.Nodes.Clear();

                    DB = new ProbDatabase(DialogSave.FileName);
                    PRDB_SQLite.Program.dbName = DB.DBName;
                    PRDB_SQLite.Program.ConnectionString = DB.ConnectionString;

                    if (!clsProcess.CreateNewDatabase(DB))
                        throw new Exception("Cannot create new database");

                    Load_TreeView();
                    clsProcess.EditMode = false;
                    newDatabase = true;
                }
                DialogSave.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Toolbar_BtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog DialogOpen = new OpenFileDialog();
            DialogOpen.DefaultExt = "pdb";
            DialogOpen.Filter = "Database file (*.pdb)|*.pdb";
            DialogOpen.AddExtension = true;
            DialogOpen.RestoreDirectory = true;
            DialogOpen.Title = "Open database...";
            DialogOpen.InitialDirectory = clsProcess.GetRootPath(AppDomain.CurrentDomain.BaseDirectory.ToString());
            DialogOpen.SupportMultiDottedExtensions = true;
            try
            {
                if (DialogOpen.ShowDialog() == DialogResult.OK)
                {
                    TreeView.Nodes.Clear();
                    DB = null;

                    DB = clsProcess.LoadDB(DialogOpen.FileName);
                    PRDB_SQLite.Program.dbName = DB.DBName;
                    PRDB_SQLite.Program.ConnectionString = DB.ConnectionString;

                    Cursor oldCursor = Cursor;
                    Cursor = Cursors.WaitCursor;

                    frm_CF = new Form_Connecting();
                    frm_CF.Show();
                    frm_CF.Refresh();

                    bool success = (clsProcess.Connect() && clsProcess.LoadDatabase(DB));

                    frm_CF.Close();
                    Cursor = oldCursor;

                    if (!success)
                    {
                        clsProcess.Dispose();
                        throw new Exception("Cannot connect to the database") { };
                    }
                    Load_TreeView();
                    clsProcess.EditMode = true;
                    newDatabase = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DialogOpen.Dispose();
        }

        private void Toolbar_BtnSave_Click(object sender, EventArgs e)
        {
            // Record to database
            Cursor oldCursor = Cursor;
            Cursor = Cursors.WaitCursor;

            frm_Saving = new Form_Saving();
            frm_Saving.Show();
            frm_Saving.Refresh();

            if (!clsProcess.SaveDatabase(DB))
            {
                MessageBox.Show("Cannnot save the database");
                //lblStatus.Text = "Cannnot save the database";
                timer.Start();
            }
            else
            {
                lblStatus.Text = "The database has been saved";
                timer.Start();
            }

            frm_Saving.Close();
            Cursor = oldCursor;
        }

        #endregion

        #region * Editor
        private void Toolbar_BtnCut_Click(object sender, EventArgs e)
        {
            txtQuery.Cut();
        }

        private void Toolbar_BtnCopy_Click(object sender, EventArgs e)
        {
            txtQuery.Copy();
        }

        private void Toolbar_BtnPaste_Click(object sender, EventArgs e)
        {
            txtQuery.Paste();
        }

        private void Toolbar_BtnUndo_Click(object sender, EventArgs e)
        {
            txtQuery.Undo();
        }

        private void Toolbar_BtnRedo_Click(object sender, EventArgs e)
        {
            txtQuery.Redo();
        }

        #endregion

        #region * Query Processing
        int oldCursorPosition, CursorPosition;

        private void ToolbarMenuItem_Hoi_ignorance_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊗_ig";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊗_ig");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void ToolbarMenuItem_Hoi_independence_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊗_in";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊗_in");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void ToolbarMenuItem_Hoi_mutualexclusion_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊗_me";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊗_me");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void ToolbarMenuItem_Tuyen_ignorance_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊕_ig";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊕_ig");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void ToolbarMenuItem_Tuyen_independence_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊕_in";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊕_in");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void ToolbarMenuItem_Tuyen_mutualexclusion_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊕_me";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊕_me");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void ToolbarMenuItem_Tru_ignorance_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊖_ig";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊖_ig");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void ToolbarMenuItem_Tru_independence_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊖_in";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊖_in");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void ToolbarMenuItem_Tru_mutualexclusion_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"⊖_me";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"⊖_me");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 4;
        }

        private void ToolbarMenuItem_Bang_ignorance_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"EQUAL(ig)";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"EQUAL(ig)");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 9;
        }

        private void ToolbarMenuItem_Bang_independence_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text = @"EQUAL(in)";
            else txtQuery.Text = txtQuery.Text.Insert(CursorPosition, @"EQUAL(in)");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 9;
        }

        private void ToolbarMenuItem_Bang_mutualexclusion_Click(object sender, EventArgs e)
        {
            oldCursorPosition = CursorPosition = txtQuery.SelectionStart;
            if (txtQuery.Text == "") txtQuery.Text += @"EQUAL(me)";
            else txtQuery.Text = txtQuery.Text.Insert(oldCursorPosition, @"EQUAL(me)");
            txtQuery.SelectionStart = CursorPosition = oldCursorPosition + 9;
        }

        string[] ProbCombOper = new string[9] { "⊗_ig ", "⊗_in ", "⊗_me ", "⊕_ig ", "⊕_in ", "⊕_me ", "⊖_ig ", "⊖_in ", "⊖_me " };
        string[] ProbEqualOper = new string[3] { " EQUAL(ig)", " EQUAL(in)", " EQUAL(me)" };
        string[] CompareOper = new string[6] { "<", ">", "=", "!=", "<=", ">=" };
        string[] LogicOper = new string[4] { " and ", " or ", " not(", "(not(" };

        private void txtQuery_TextChanged(object sender, EventArgs e)
        {
            CursorPosition = txtQuery.SelectionStart;
            int start = 0;
            int end = txtQuery.Text.Length + 1;
            txtQuery.ForeColor = Color.Black;

            for (int i = 0; i < 9; i++)  // Colour for probabilistic combination operator
            {
                start = 0;
                do
                {
                    start = txtQuery.Find(ProbCombOper[i], start, end, RichTextBoxFinds.MatchCase);
                    if (start != -1)
                    {
                        txtQuery.SelectionStart = start;
                        txtQuery.SelectionLength = 4;
                        txtQuery.SelectionColor = Color.Red;
                        start += 1;
                    }
                } while (start != -1);
            }

            for (int i = 0; i < 3; i++)  // Colour for probabilistic equal operator
            {
                start = 0;
                do
                {
                    start = txtQuery.Find(ProbEqualOper[i], start, end, RichTextBoxFinds.MatchCase);
                    if (start != -1)
                    {
                        txtQuery.SelectionStart = start + 1;
                        txtQuery.SelectionLength = 9;
                        txtQuery.SelectionColor = Color.Blue;
                        start += 1;
                    }
                } while (start != -1);
            }

            for (int i = 0; i < 6; i++)  // Colour for compare operator
            {
                start = 0;
                do
                {
                    start = txtQuery.Find(CompareOper[i], start, end, RichTextBoxFinds.MatchCase);
                    if (start != -1)
                    {
                        txtQuery.SelectionStart = start;
                        txtQuery.SelectionLength = CompareOper[i].Length;
                        txtQuery.SelectionColor = Color.Violet;
                        start += 1;
                    }
                } while (start != -1);
            }

            for (int i = 0; i < 4; i++)  // Colour for logic operator
            {
                start = 0;
                do
                {
                    start = txtQuery.Find(LogicOper[i], start, end, RichTextBoxFinds.None);
                    if (start != -1)
                    {
                        txtQuery.SelectionStart = start + 1;
                        txtQuery.SelectionLength = LogicOper[i].Length - 2;
                        txtQuery.SelectionColor = Color.DeepSkyBlue;
                        start += 1;
                    }
                } while (start != -1);
            }

            txtQuery.SelectionStart = CursorPosition;
            txtQuery.SelectionLength = 1;
            txtQuery.SelectionColor = Color.Black;
            txtQuery.DeselectAll();
        }

        private void Toolbar_BtnExecute_Click(object sender, EventArgs e)
        {
            Toolbar_BtnStop.Enabled = true;

            try
            {
                lblStatus.Text = "Executing...";
                GridViewResult.Rows.Clear();
                GridViewResult.Columns.Clear();
                txtMessage.Text = "";

                if (txtQuery.Text == "")
                    throw new Exception("The query is not entered") { };
                string query = txtQuery.SelectedText.Length == 0 ? txtQuery.Text : txtQuery.SelectedText;
                CurrentQuery = new ProbQuery(query.Trim());
                CurrentQuery.Relations.Add(CurrentQuery.GetRelations(DB)[0]);

                List<ProbTuple> result = new List<ProbTuple>();

                if (CurrentQuery.CheckSyntax())
                    result = CurrentQuery.Execute();

                if (result.Count == 0)
                {
                    txtMessage.Text = "There is no relation satisfy the condition";
                    xtraTabResult.SelectedTabPageIndex = 1;
                }
                else
                {
                    foreach (ProbAttribute attribute in CurrentQuery.Attributes)
                        GridViewResult.Columns.Add(attribute.Name, attribute.Name);

                    for (int i = 0; i < result.Count; i++)
                    {
                        GridViewResult.Rows.Add();
                        for (int j = 0; j < result[i].Triples.Count; j++)
                            GridViewResult.Rows[i].Cells[j].Value = result[i].Triples[j].GetStrValue();
                    }

                    xtraTabResult.SelectedTabPageIndex = 0;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                if (CurrentQuery != null) txtMessage.Text = CurrentQuery.ErrorMessage;
            }
            lblStatus.Text = "Ready";
            Toolbar_BtnStop.Enabled = false;
        }

        private void Toolbar_BtnStop_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #endregion

        #region ** Object View

        #region * Button

        private void OE_BtnDatabase_Click(object sender, EventArgs e)
        {
            xtraTabDatabase.Show();
            xtraTabQuery.Hide();
        }

        private void OE_BtnQuery_Click(object sender, EventArgs e)
        {
            xtraTabDatabase.Hide();
            xtraTabQuery.Show();
        }

        #endregion

        #region * TreeView
        private void Load_TreeView()
        {
            TreeView.Nodes.Clear();
            PRDB_SQLite.Program.dbShowName = "DB_" + DB.DBName.ToUpper();

            NodeDB = new TreeNode();
            NodeDB.Text = PRDB_SQLite.Program.dbShowName;
            NodeDB.ToolTipText = "Database " + PRDB_SQLite.Program.dbShowName;
            NodeDB.ContextMenuStrip = ContextMenu_Database;
            NodeDB.ImageIndex = DB_ImgIndex.UnselectedState;
            NodeDB.SelectedImageIndex = DB_ImgIndex.SelectedState;
            TreeView.Nodes.Add(NodeDB);

            NodeSchema = new TreeNode();
            NodeSchema.Text = "Schemas";
            NodeSchema.ToolTipText = "Schemas";
            NodeSchema.ContextMenuStrip = ContextMenu_Schema;
            NodeSchema.ImageIndex = Folder_ImgIndex.UnselectedState;
            NodeSchema.SelectedImageIndex = Folder_ImgIndex.UnselectedState;
            NodeDB.Nodes.Add(NodeSchema);

            NodeRelation = new TreeNode();
            NodeRelation.Text = "Relations";
            NodeRelation.ToolTipText = "Relations";
            NodeRelation.ContextMenuStrip = ContextMenu_Relation;
            NodeRelation.ImageIndex = Folder_ImgIndex.UnselectedState;
            NodeRelation.SelectedImageIndex = Folder_ImgIndex.UnselectedState;
            NodeDB.Nodes.Add(NodeRelation);

            NodeView = new TreeNode();
            NodeView.Text = "Views";
            NodeView.ToolTipText = "Views";
            NodeView.ContextMenuStrip = ContextMenu_View;
            NodeView.ImageIndex = Folder_ImgIndex.UnselectedState;
            NodeView.SelectedImageIndex = Folder_ImgIndex.UnselectedState;
            NodeDB.Nodes.Add(NodeView);

            NodeQuery = new TreeNode();
            NodeQuery.Text = "Queries";
            NodeQuery.ToolTipText = "Queries";
            NodeQuery.ContextMenuStrip = ContextMenu_Query;
            NodeQuery.ImageIndex = Folder_ImgIndex.UnselectedState;
            NodeQuery.SelectedImageIndex = Folder_ImgIndex.UnselectedState;
            NodeDB.Nodes.Add(NodeQuery);

            LoadTreeViewNode();
        }

        private void LoadTreeViewNode()
        {
            foreach (ProbSchema schema in DB.Schemas)
            {
                NewNode = new TreeNode();
                NewNode.Text = schema.SchemaName;
                NewNode.ToolTipText = "Schema " + schema.SchemaName;
                NewNode.ContextMenuStrip = ContextMenu_SchemaNode;
                NewNode.ImageIndex = Schema_ImgIndex.UnselectedState;
                NewNode.SelectedImageIndex = Schema_ImgIndex.UnselectedState;
                NodeSchema.Nodes.Add(NewNode);
            }

            foreach (ProbRelation relation in DB.Relations)
            {
                NewNode = new TreeNode();
                NewNode.Text = relation.RelationName;
                NewNode.ToolTipText = "Relation " + relation.RelationName;
                NewNode.ContextMenuStrip = ContextMenu_RelationNode;
                NewNode.ImageIndex = Relation_ImgIndex.UnselectedState;
                NewNode.SelectedImageIndex = Relation_ImgIndex.UnselectedState;
                NodeRelation.Nodes.Add(NewNode);
            }

            foreach (ProbView view in DB.Views)
            {
                NewNode = new TreeNode();
                NewNode.Text = view.ViewName;
                NewNode.ToolTipText = "View " + view.ViewName;
                NewNode.ContextMenuStrip = ContextMenu_ViewNode;
                NewNode.ImageIndex = View_ImgIndex.UnselectedState;
                NewNode.SelectedImageIndex = View_ImgIndex.SelectedState;
                NodeView.Nodes.Add(NewNode);
            }

            foreach (ProbQuery query in DB.Queries)
            {
                NewNode = new TreeNode();
                NewNode.Text = query.QueryName;
                NewNode.ToolTipText = "Query " + query.QueryName;
                NewNode.ContextMenuStrip = contextMenu_QueryNode;
                NewNode.ImageIndex = Query_ImgIndex.UnselectedState;
                NewNode.SelectedImageIndex = Query_ImgIndex.SelectedState;
                NodeQuery.Nodes.Add(NewNode);
            }
        }

        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            CurrentNode = e.Node;

            if (CurrentNode.Parent == NodeDB && !CurrentNode.IsExpanded)
                e.Node.ImageIndex = e.Node.SelectedImageIndex = Folder_ImgIndex.UnselectedState;

            if (e.Button == MouseButtons.Right)
                CurrentNode.ContextMenuStrip.Show();
        }

        private void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node != NodeDB && e.Node.IsExpanded)
                e.Node.ImageIndex = e.Node.SelectedImageIndex = Folder_ImgIndex.SelectedState;
        }

        #endregion

        #endregion

        #region ** Main Windows

        #region * Form Main

        private void LoadImageCollection()
        {
            TreeView.ImageList = ImageList_TreeView;
            DB_ImgIndex.SelectedState = DB_ImgIndex.UnselectedState = 0;
            Folder_ImgIndex.UnselectedState = 1;
            Folder_ImgIndex.SelectedState = 2;
            Schema_ImgIndex.SelectedState = Schema_ImgIndex.UnselectedState = 3;
            Relation_ImgIndex.SelectedState = Relation_ImgIndex.UnselectedState = 3;
            View_ImgIndex.SelectedState = View_ImgIndex.UnselectedState = 3;
            Query_ImgIndex.SelectedState = Query_ImgIndex.UnselectedState = 3;
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            SavingNewSchema = false;
            SavingNewRelation = false;
            SavingNewView = false;
            SavingNewQuery = false;
            CurrentRow = CurrentCell = CurrentColumn = 0;
            validated = flag = true;
            xtraTabDatabase.Show();
            xtraTabDatabase.SelectedTabPageIndex = 0;
            xtraTabQuery.Hide();
            LoadImageCollection();
            Toolbar_BtnStop.Enabled = false;
            BindingNavigatorData.Visible = true;
            BindingNavigatorDesign.Visible = true;
            BindingNavigatorValue.Visible = true;
            ResetInputValue();
            timer = new System.Timers.Timer(2000);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DB != null && !DB.isNotEdited())
            {
                DialogResult result = MessageBox.Show("Do you want to save this database ?", "Close database " + DB.DBName + "...", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    if (!clsProcess.SaveDatabase(DB))
                    {
                        lblStatus.Text = "Cannot save this database";
                        timer.Start();
                        e.Cancel = true;
                    }
                    else
                    {
                        lblStatus.Text = "The database has been saved";
                        timer.Start();
                    }
                }
                else if (result == DialogResult.Cancel) e.Cancel = true;
            }
        }

        void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            lblStatus.Text = "Ready";
            timer.Stop();
        }

        public void ResetSchemaPage()
        {
            xtraTabDatabase.TabPages[0].Text = "Create schema...";
            GridViewDesign.Rows.Clear();
            lblDesignRowNumber.Text = "1 / 1";
        }

        public void ResetRelationPage()
        {
            xtraTabDatabase.TabPages[1].Text = "Create relation...";

            GridViewData.Rows.Clear();
            GridViewData.Columns.Clear();

            lblDataRowNumber.Text = "0 / 0";

            GridViewValue.Rows.Clear();
            GridViewValue.Columns.Clear();

            lblValueRowNumber.Text = "0 / 0";
        }

        public void ResetQueryPage()
        {
            xtraTabQuery.TabPages[0].Name = "Input query...";
            txtQuery.Clear();
            txtMessage.Clear();
            GridViewResult.Rows.Clear();
            GridViewResult.Columns.Clear();
        }

        public void ReloadMainForm()
        {
            SavingNewSchema = false;
            SavingNewRelation = false;
            SavingNewView = false;
            SavingNewQuery = false;
            CurrentRow = CurrentCell = CurrentColumn = 0;
            validated = flag = true;
            xtraTabDatabase.Show();
            xtraTabDatabase.SelectedTabPageIndex = 0;
            xtraTabQuery.Hide();
            Toolbar_BtnStop.Enabled = false;
            BindingNavigatorData.Visible = true;
            BindingNavigatorDesign.Visible = true;
            BindingNavigatorValue.Visible = true;
            ResetInputValue();
            ResetSchemaPage();
            ResetRelationPage();
            ResetQueryPage();
        }

        #endregion

        #region * GridViewDesign

        private void GridViewDesign_SelectionChanged(object sender, EventArgs e)
        {
            if (GridViewDesign.Rows.Count == 0)
            {
                GridViewDesign.Rows.Add();
                if (GridViewDesign.CurrentRow != null)
                    lblDesignRowNumber.Text = (GridViewDesign.CurrentRow.Index + 1).ToString() + " / " + GridViewDesign.Rows.Count.ToString();
                else lblDesignRowNumber.Text = "1 / " + GridViewDesign.Rows.Count.ToString();
            }
            try
            {
                if (GridViewDesign.CurrentRow.Index != CurrentRow && ValidateRow(CurrentRow) == false)
                {
                    if (!flag)  // Đặt biến cờ để tránh sự kiện SelectionChanged lặp lại 2 lần
                    {
                        flag = true;
                        MessageBox.Show(ErrorMessage);
                        GridViewDesign.CurrentCell = GridViewDesign.Rows[CurrentRow].Cells[CurrentCell];
                    }
                }
                else CurrentRow = GridViewDesign.CurrentRow.Index;
                flag = false;
            }
            catch { }
        }

        private bool ValidateRow(int RowIndex)
        {
            try
            {
                if (RowIndex >= 0)
                {
                    bool PrKey = (GridViewDesign.Rows[RowIndex].Cells["PrimaryKey"].Value != null);       // kiểm tra xem field  PrimaryKey đã có giá trị hay chưa
                    bool AttrName = (GridViewDesign.Rows[RowIndex].Cells["ColumnName"].Value != null);    // kiểm tra xem field  AttributeName đã có giá trị hay chưa
                    bool TypeName = (GridViewDesign.Rows[RowIndex].Cells["ColumnType"].Value != null);    // kiểm tra xem field TypeName đã có giá trị hay chưa
                    bool Description = (GridViewDesign.Rows[RowIndex].Cells["ColumnDescription"].Value != null);    // kiểm tra xem field Description đã có giá trị hay chưa
                    // Nếu cả tên thuộc tính và kiểu dữ liệu đã được nhập
                    if (AttrName && TypeName)
                        return true;
                    // Nếu một trong các cột Khóa chính, tên thuộc tính và kiểu dữ liệu đã có giá trị
                    else if (PrKey || AttrName || TypeName || Description)
                    {
                        // Nếu tên thuộc tính và kiểu dữ liệu chưa được nhập
                        if (!AttrName && !TypeName)
                        {
                            ErrorMessage = "The attribute name and data type are not entered";
                            CurrentCell = 1;
                            return false;
                        }
                        // Nếu chỉ có tên thuộc tính chưa được nhập
                        else if (!AttrName)
                        {
                            ErrorMessage = "The attribute name are not entered";
                            CurrentCell = 1;
                            return false;
                        }
                        // Nếu chỉ có kiểu dữ liệu chưa được nhập
                        else
                        {
                            ErrorMessage = "The data type is not selected";
                            CurrentCell = 2;
                            return false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

            return true;
        }

        private void GridViewDesign_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                CurrentCell = e.ColumnIndex;
                frm_InpType = new Form_InputType();
                frm_InpType.Show();
                frm_InpType.Disposed += new EventHandler(frm_InpType_Disposed);
            }
        }

        void frm_InpType_Disposed(object sender, EventArgs e)
        {
            if (frm_InpType.TypeName == "")
                GridViewDesign.Rows[CurrentRow].Cells[CurrentCell].Value = frm_InpType.DataType;
            else GridViewDesign.Rows[CurrentRow].Cells[CurrentCell].Value = frm_InpType.TypeName;

            GridViewDesign.Rows[CurrentRow].Cells[CurrentCell + 1].Value = frm_InpType.ValueType;
        }

        private void GridViewDesign_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (GridViewDesign.CurrentCell.Value != null)
            {
                string ColumnName = GridViewDesign.Columns[e.ColumnIndex].Name;
                if (GridViewData.Rows.Count > 0 && GridViewData.Columns.Contains(ColumnName))
                {
                    MessageBox.Show("Cannot modify attributes having data");
                    GridViewDesign.ClearSelection();
                }
            }
        }

        private void GridViewDesign_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (GridViewDesign.CurrentCell.Value != null)
            {
                if (e.ColumnIndex == 1)
                    for (int i = 0; i < GridViewDesign.Rows.Count - 1; i++)
                        if (GridViewDesign.CurrentCell.Value.ToString().CompareTo(GridViewDesign.Rows[i].Cells[1].Value.ToString()) == 0 && GridViewDesign.CurrentCell.RowIndex != i)
                        {
                            MessageBox.Show("There is already an attribute with the same name");
                            GridViewDesign.ClearSelection();
                            GridViewDesign.CurrentCell.Selected = true;
                            break;
                        }
                string temp = GridViewDesign.CurrentCell.Value.ToString();
                GridViewDesign.CurrentCell.ToolTipText = temp;
            }
        }

        private void xtraTabDatabase_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            //if (e.PrevPage.Name == "tabDesign")
            //{
            //    if (CurrentRow >= 0 && !ValidateRow(CurrentRow))
            //    {
            //        MessageBox.Show(ErrorMessage);
            //        validated = false;
            //    }
            //}
        }

        private void GridViewDesign_MouseClick(object sender, MouseEventArgs e)
        {
            if (GridViewDesign.Rows.Count == 0)
            {
                GridViewDesign.Rows.Add();
                if (GridViewDesign.CurrentRow != null)
                    lblDesignRowNumber.Text = (GridViewDesign.CurrentRow.Index + 1).ToString() + GridViewDesign.Rows.Count.ToString();
                else lblDesignRowNumber.Text = "1 / " + GridViewDesign.Rows.Count.ToString();
            }
        }

        private void btn_Design_Home_Click(object sender, EventArgs e)
        {
            if (GridViewDesign.Rows.Count > 1)
            {
                GridViewDesign.CurrentCell = GridViewDesign.Rows[0].Cells[0];
                lblDesignRowNumber.Text = "1 / " + GridViewDesign.Rows.Count.ToString();
            }
        }

        private void btn_Design_Pre_Click(object sender, EventArgs e)
        {
            if (GridViewDesign.Rows.Count > 1)
            {
                int PreRow = GridViewDesign.CurrentRow.Index - 1;
                PreRow = (PreRow > 0 ? PreRow : 0);
                GridViewDesign.CurrentCell = GridViewDesign.Rows[PreRow].Cells[0];
                lblDesignRowNumber.Text = (PreRow + 1).ToString() + " / " + GridViewDesign.Rows.Count.ToString();
            }
        }

        private void btn_Design_Next_Click(object sender, EventArgs e)
        {
            if (GridViewDesign.Rows.Count > 1)
            {
                int nRow = GridViewDesign.Rows.Count;
                int NextRow = GridViewDesign.CurrentRow.Index + 1;
                NextRow = (NextRow < nRow - 1 ? NextRow : nRow - 1);
                GridViewDesign.CurrentCell = GridViewDesign.Rows[NextRow].Cells[0];
                lblDesignRowNumber.Text = (NextRow + 1).ToString() + " / " + GridViewDesign.Rows.Count.ToString();
            }
        }

        private void btn_Design_End_Click(object sender, EventArgs e)
        {
            if (GridViewDesign.Rows.Count > 1)
            {
                int nRow = GridViewDesign.Rows.Count;
                GridViewDesign.CurrentCell = GridViewDesign.Rows[nRow - 1].Cells[0];
                lblDesignRowNumber.Text = nRow.ToString() + " / " + nRow.ToString();
            }
        }

        private void Btn_Design_DeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewDesign.Rows.Remove(GridViewDesign.CurrentRow);
            }
            catch { }
        }

        private void Btn_Design_ClearData_Click(object sender, EventArgs e)
        {
            DialogResult result = new DialogResult();
            result = MessageBox.Show("Are you sure want to clear all schema data ?", "Clear All Data", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                GridViewDesign.Rows.Clear();
                lblDesignRowNumber.Text = "1 / 1";
            }
        }

        private void Btn_Design_UpdateData_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Cannot find the Database");
                if (CurrentSchema == null)
                    throw new Exception("The schema has not been created yet");

                CurrentSchema.Attributes.Clear();
                CurrentSchema.txtSchema = new DataTable();

                int nRow = GridViewDesign.Rows.Count - 1;
                GridViewDesign.CurrentCell = GridViewDesign.Rows[nRow].Cells[0];

                DataRow NewRow;
                DataColumn NewColumn;

                for (int i = 0; i < 5; i++)
                {
                    NewColumn = new DataColumn();
                    NewColumn.ColumnName = i.ToString();
                    NewColumn.DataType = typeof(string);
                    CurrentSchema.txtSchema.Columns.Add(NewColumn);
                }

                ProbAttribute attribute;
                for (int i = 0; i < nRow; i++)
                {
                    attribute = new ProbAttribute();
                    attribute.Name = GridViewDesign.Rows[i].Cells[1].Value.ToString();
                    attribute.PrimaryKey = (GridViewDesign.Rows[i].Cells[0].Value == null ? false : true);
                    attribute.Type.TypeName = GridViewDesign.Rows[i].Cells[2].Value.ToString();
                    attribute.Type._DataType = attribute.Type.GetDataType();
                    attribute.Type.ValueType = GridViewDesign.Rows[i].Cells[3].Value.ToString();
                    attribute.Description = (GridViewDesign.Rows[i].Cells[4].Value == null ? "" : GridViewDesign.Rows[i].Cells[4].Value.ToString());

                    CurrentSchema.Attributes.Add(attribute);

                    NewRow = CurrentSchema.txtSchema.Rows.Add();

                    NewRow[0] = (GridViewDesign.Rows[i].Cells[0].Value == null ? "false" : "true");
                    NewRow[1] = GridViewDesign.Rows[i].Cells[1].Value.ToString();
                    NewRow[2] = GridViewDesign.Rows[i].Cells[2].Value.ToString();
                    NewRow[3] = GridViewDesign.Rows[i].Cells[3].Value.ToString();
                    NewRow[4] = (GridViewDesign.Rows[i].Cells[4].Value == null ? "" : GridViewDesign.Rows[i].Cells[4].Value.ToString());
                }

                CurrentSchema.Edited = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        #endregion

        #region * GridViewData
        private void xtraTabDatabase_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //if (e.Page.Name == "tabData")
            //{
            //    if (validated)
            //    {
            //        int nDesign = GridViewDesign.Rows.Count - 1;
            //        int nData = GridViewData.Columns.Count;
            //        for (int i = nData; i < nDesign; i++)
            //            GridViewData.Columns.Add("Column" + i.ToString(), GridViewDesign.Rows[i].Cells[1].Value.ToString());

            //        CurrentRelation = new Relation(CurrentSchema.SchemaName);
            //        DB.Relations.Add(CurrentRelation);
            //        int nRow = GridViewDesign.Rows.Count - 1;
            //        int nCol = GridViewDesign.Columns.Count;
            //        ProbAttribute attr;
            //        for (int i = 0; i < nRow; i++)
            //        {
            //            attr = new ProbAttribute();
            //            attr.Name = GridViewDesign.Rows[i].Cells[1].Value.ToString();
            //            attr.Type._DataType = GridViewDesign.Rows[i].Cells[2].Value.ToString();
            //            CurrentRelation.Attributes.Add(attr);
            //        }

            //    }
            //}
            //else if (e.Page.Name == "tabDesign") validated = true;
        }

        private void btn_Data_Home_Click(object sender, EventArgs e)
        {
            if (GridViewData.Rows.Count > 0)
            {
                GridViewData.CurrentCell = GridViewData.Rows[0].Cells[0];
                lblDataRowNumber.Text = "1 / " + GridViewData.Rows.Count.ToString();
            }
        }

        private void btn_Data_Pre_Click(object sender, EventArgs e)
        {
            if (GridViewData.Rows.Count > 0)
            {
                int PreRow = GridViewData.CurrentRow.Index - 1;
                PreRow = (PreRow > 0 ? PreRow : 0);
                GridViewData.CurrentCell = GridViewData.Rows[PreRow].Cells[0];
                lblDataRowNumber.Text = (PreRow + 1).ToString() + " / " + GridViewData.Rows.Count.ToString();
            }
        }

        private void btn_Data_Next_Click(object sender, EventArgs e)
        {
            if (GridViewData.Rows.Count > 0)
            {
                int nRow = GridViewData.Rows.Count;
                int NextRow = GridViewData.CurrentRow.Index + 1;
                NextRow = (NextRow < nRow - 1 ? NextRow : nRow - 1);
                GridViewData.CurrentCell = GridViewData.Rows[NextRow].Cells[0];
                lblDataRowNumber.Text = (NextRow + 1).ToString() + " / " + GridViewData.Rows.Count.ToString();
            }
        }

        private void btn_Data_End_Click(object sender, EventArgs e)
        {
            if (GridViewData.Rows.Count > 0)
            {
                int nRow = GridViewData.Rows.Count;
                GridViewData.CurrentCell = GridViewData.Rows[nRow - 1].Cells[0];
                lblDataRowNumber.Text = nRow.ToString() + " / " + nRow.ToString();
            }
        }

        private void Btn_Data_ClearData_Click(object sender, EventArgs e)
        {
            DialogResult result = new DialogResult();
            result = MessageBox.Show("Are you sure want to clear all relations data ?", "Clear All Data", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                GridViewData.Rows.Clear();
                GridViewData.Columns.Clear();
                lblDataRowNumber.Text = "1 / 1";
            }
        }

        private void Btn_Data_DeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewData.Rows.Remove(GridViewData.CurrentRow);
            }
            catch { }
        }

        private void Btn_UpdateData_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB == null)
                    throw new Exception("Database has not been created yet");
                if (CurrentRelation == null)
                    throw new Exception("The relation has not been created yet");


                int nRow, nCol;
                nRow = GridViewData.Rows.Count - 1;
                nCol = GridViewData.Columns.Count;

                GridViewData.CurrentCell = GridViewData.Rows[nRow].Cells[0];

                ProbTuple tuple;
                ProbTriple triple;
                CurrentRelation.Data = new List<ProbTuple>();

                for (int i = 0; i < nRow; i++)
                {
                    tuple = new ProbTuple();
                    for (int j = 0; j < nCol; j++)
                    {
                        triple = new ProbTriple(GridViewData.Rows[i].Cells[j].Value.ToString());
                        tuple.Triples.Add(triple);
                    }
                    CurrentRelation.Data.Add(tuple);
                }

                CurrentRelation.Edited = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        #endregion

        #region * GridViewValue
        double minprob, maxprob;
        string strvalue;
        int n;
        List<ProbTriple> attribute;

        private void ResetInputValue()
        {
            label1.Enabled = false;
            label2.Enabled = false;
            txtMinProb.Enabled = false;
            txtMaxProb.Enabled = false;
            txtValue.Visible = false;
            GridViewValue.Visible = true;
            btn_Value_Home.Enabled = true;
            btn_Value_Pre.Enabled = true;
            btn_Value_Next.Enabled = true;
            btn_Value_End.Enabled = true;
            btn_Value_DeleteRow.Enabled = true;
            btn_Value_AddNewRow.Enabled = true;
            lblValueRowNumber.Enabled = true;
        }

        private void CheckBox_UD_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_UD.Checked == false)// Mở các control liên quan đến việc nhập từng giá trị rời rạc
            {
                label1.Enabled = false;
                label2.Enabled = false;
                txtMinProb.Enabled = false;
                txtMaxProb.Enabled = false;
                txtValue.Visible = false;
                GridViewValue.Visible = true;
                btn_Value_Home.Enabled = true;
                btn_Value_Pre.Enabled = true;
                btn_Value_Next.Enabled = true;
                btn_Value_End.Enabled = true;
                btn_Value_DeleteRow.Enabled = true;
                btn_Value_AddNewRow.Enabled = true;
                lblValueRowNumber.Enabled = true;
            }
            else                            // Mở các control liên quan đến việc nhập một tập các giá trị
            {
                label1.Enabled = true;
                label2.Enabled = true;
                txtMinProb.Enabled = true;
                txtMaxProb.Enabled = true;
                txtValue.Visible = true;
                GridViewValue.Visible = false;
                btn_Value_Home.Enabled = false;
                btn_Value_Pre.Enabled = false;
                btn_Value_Next.Enabled = false;
                btn_Value_End.Enabled = false;
                btn_Value_DeleteRow.Enabled = false;
                btn_Value_AddNewRow.Enabled = false;
                lblValueRowNumber.Enabled = false;
            }
        }

        private string Stdize(string S)     // Chuẩn hóa chuỗi cắt bỏ các dấu , dư thừa
        {
            string R = "";
            int i = 0;
            while (S[i] == ',') i++;
            int k = S.Length - 1;
            while (S[k] == ',') k--;
            for (int j = i; j <= k; j++)
                if (S[j] != ',') R += S[j];
                else if (S[j - 1] != ',') R += S[j];
            return R;
        }

        private void btn_Value_AddNewRow_Click(object sender, EventArgs e)
        {
            GridViewValue.Rows.Add();
        }

        private void btn_Value_DeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewValue.Rows.Remove(GridViewValue.CurrentRow);
            }
            catch { }
        }

        private void btn_Value_UpdateData_Click(object sender, EventArgs e)
        {
            int UpdateRow, UpdateCell;

            if (CheckBox_UD.Checked == false) // Nhập bộ ba xác suất theo phân bố rời rạc
            {
                try
                {
                    if (GridViewValue.Rows.Count == 0)
                        MessageBox.Show("The value is not entered");
                    //else if (!ValidateRow(CurrentRow))
                    //{
                    //    MessageBox.Show(ErrorMessage);
                    //    GridViewValue.CurrentCell = GridViewValue.Rows[CurrentRow].Cells[CurrentCell];
                    //}
                    else                    // Lấy tập giá trị từ GridViewValue
                    {
                        n = GridViewValue.Rows.Count;
                        GridViewValue.CurrentCell = GridViewValue.CurrentRow.Cells[0];
                        GridViewValue.CurrentCell = GridViewValue.CurrentRow.Cells[1];
                        GridViewValue.CurrentCell = GridViewValue.CurrentRow.Cells[2];
                        ProbTriple triple = new ProbTriple();
                        for (int i = 0; i < n; i++)
                        {
                            triple.Value.Add(GridViewValue.Rows[i].Cells["ColumnValue"].Value.ToString());
                            triple.MinProb.Add(Convert.ToDouble(GridViewValue.Rows[i].Cells["ColumnMinProb"].Value));
                            triple.MaxProb.Add(Convert.ToDouble(GridViewValue.Rows[i].Cells["ColumnMaxProb"].Value));
                        }
                        triple.Distribution = false;
                        UpdateRow = GridViewData.CurrentRow.Index;
                        UpdateCell = GridViewData.CurrentCell.ColumnIndex;
                        if (UpdateRow == GridViewData.Rows.Count - 1)
                        {
                            GridViewData.Rows.Add();
                            if (GridViewData.CurrentRow != null)
                                lblDataRowNumber.Text = (GridViewData.CurrentRow.Index + 1).ToString() + " / " + GridViewData.Rows.Count.ToString();
                            else lblDataRowNumber.Text = "1 / " + GridViewData.Rows.Count.ToString();
                        }
                        GridViewData.CurrentCell = GridViewData.Rows[UpdateRow].Cells[UpdateCell];
                        GridViewData.CurrentCell.Value = triple.GetStrValue();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            else // Nhập bộ ba xác suất theo phân bố đều 
            {
                if (txtMinProb.Text == "" || txtMaxProb.Text == "")
                    MessageBox.Show("The value Sum of MinProb and MaxProb are not entered");
                else if (txtValue.Text == "")
                    MessageBox.Show("The values are not entered");
                else     // Lấy tập giá trị từ TextBox và phân bố xác suất cho từng giá trị
                {
                    try
                    {
                        double minprob, maxprob;
                        string[] value;
                        value = Stdize(txtValue.Text.Replace("\r\n", ",")).Split(',');
                        for (int i = 0; i < value.Length; i++) value[i] = value[i].Trim();

                        minprob = Convert.ToDouble(txtMinProb.Text);
                        maxprob = Convert.ToDouble(txtMaxProb.Text);

                        n = value.Length;
                        ProbTriple triple = new ProbTriple();
                        triple.Distribution = true;
                        for (int i = 0; i < n; i++)
                        {
                            triple.Value.Add(value[i]);
                            triple.MinProb.Add(minprob / n);
                            triple.MaxProb.Add(maxprob / n);
                        }
                        UpdateRow = GridViewData.CurrentRow.Index;
                        UpdateCell = GridViewData.CurrentCell.ColumnIndex;
                        if (UpdateRow == GridViewData.Rows.Count - 1)
                        {
                            GridViewData.Rows.Add();
                            if (GridViewData.CurrentRow != null)
                                lblDataRowNumber.Text = (GridViewData.CurrentRow.Index + 1).ToString() + " / " + GridViewData.Rows.Count.ToString();
                            else lblDataRowNumber.Text = "1 / " + GridViewData.Rows.Count.ToString();
                        }
                        GridViewData.CurrentCell = GridViewData.Rows[UpdateRow].Cells[UpdateCell];
                        GridViewData.CurrentCell.Value = triple.GetStrValue();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        private void GridViewValue_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (GridViewValue.CurrentCell.Value != null)
            {
                if (e.ColumnIndex > 0)  // Giá trị nhập vào các ô MinProb và MaxProb
                {
                    try
                    {
                        double Prob = Convert.ToDouble(GridViewValue.CurrentCell.Value);
                        if (Prob < 0.0 || Prob > 1.0)
                            MessageBox.Show("Probabilistic value must belong to [0,1]");
                    }
                    catch
                    {
                        MessageBox.Show("Probabilistic value must be a double");
                    }
                }
                else          // Giá trị nhập vào ô Value
                {
                    string StrValue = GridViewValue.CurrentCell.Value.ToString();
                }
            }
        }

        private void Btn_Value_ClearData_Click(object sender, EventArgs e)
        {
            label1.Enabled = false;
            label2.Enabled = false;
            txtMinProb.Enabled = false;
            txtMaxProb.Enabled = false;
            txtValue.Visible = false;
            GridViewValue.Visible = true;
            btn_Value_Home.Enabled = true;
            btn_Value_Pre.Enabled = true;
            btn_Value_Next.Enabled = true;
            btn_Value_End.Enabled = true;
            lblValueRowNumber.Enabled = true;
            CheckBox_UD.Checked = false;
            GridViewValue.Rows.Clear();
            txtMaxProb.Text = "";
            txtMinProb.Text = "";
            txtValue.Text = "";
        }

        private void btn_Value_Home_Click(object sender, EventArgs e)
        {
            if (GridViewValue.Rows.Count > 0)
            {
                GridViewValue.CurrentCell = GridViewValue.Rows[0].Cells[0];
                lblValueRowNumber.Text = "1 / " + GridViewValue.Rows.Count.ToString();
            }
        }

        private void btn_Value_Pre_Click(object sender, EventArgs e)
        {
            if (GridViewValue.Rows.Count > 0)
            {
                int PreRow = GridViewValue.CurrentRow.Index - 1;
                PreRow = (PreRow > 0 ? PreRow : 0);
                GridViewValue.CurrentCell = GridViewValue.Rows[PreRow].Cells[0];
                lblValueRowNumber.Text = (PreRow + 1).ToString() + " / " + GridViewValue.Rows.Count.ToString();
            }
        }

        private void btn_Value_Next_Click(object sender, EventArgs e)
        {
            if (GridViewValue.Rows.Count > 0)
            {
                int nRow = GridViewValue.Rows.Count;
                int NextRow = GridViewValue.CurrentRow.Index + 1;
                NextRow = (NextRow < nRow - 1 ? NextRow : nRow - 1);
                GridViewValue.CurrentCell = GridViewValue.Rows[NextRow].Cells[0];
                lblValueRowNumber.Text = (NextRow + 1).ToString() + " / " + GridViewValue.Rows.Count.ToString();
            }
        }

        private void btn_Value_End_Click(object sender, EventArgs e)
        {
            if (GridViewValue.Rows.Count > 0)
            {
                int nRow = GridViewValue.Rows.Count;
                GridViewValue.CurrentCell = GridViewValue.Rows[nRow - 1].Cells[0];
                lblValueRowNumber.Text = nRow.ToString() + " / " + nRow.ToString();
            }
        }

        #endregion

        private void GridViewDesign_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (GridViewDesign.CurrentRow != null)
                lblDesignRowNumber.Text = (GridViewDesign.CurrentRow.Index + 1).ToString() + " / " + GridViewDesign.Rows.Count.ToString();
            else lblDesignRowNumber.Text = "1 / " + GridViewDesign.Rows.Count.ToString();
        }

        #endregion

    }
}
