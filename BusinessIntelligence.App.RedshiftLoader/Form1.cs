using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessIntelligence.Data;
namespace BusinessIntelligence.App.RedshiftLoader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sequence.Add(0, "tb1");

            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void btPrevious_Click(object sender, EventArgs e)
        {
            currentStepIndex--;
            if (tabControl1.SelectedTab.Name == "tb2")
            {
                tabControl1.SelectTab("tb1");
            }
            else if (tabControl1.SelectedTab.Name == "tb3")
            {
                tabControl1.SelectTab("tb2");
            }
            if (tabControl1.SelectedTab.Name == "tb4")
            {
                tabControl1.SelectTab("tb3");
            }
            checkState();
        }
        System.Data.Common.DbConnection conn = null;
        char delimiter;
        List<string> lines = new List<string>();
        private void btNext_Click(object sender, EventArgs e)
        {
            currentStepIndex++;
            checkState();
            if (tabControl1.SelectedTab.Name == "tb1")
            {
                if (rdTextFileDelimited.Checked)
                {
                    openFileDialog1.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|All files (*.*)|*.*";
                }
                if (rdTextFileDelimited.Checked || rdExcelFile.Checked)
                {
                    if (rdTextFileDelimited.Checked)
                    {
                        openFileDialog1.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|All files (*.*)|*.*";
                    }
                    string filename;

                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        filename = openFileDialog1.FileName;
                        if (rdTextFileDelimited.Checked)
                        {

                            bool delimiterDefined = false;
                            using (System.IO.StreamReader sr = new System.IO.StreamReader(filename,Encoding.Default))
                            {

                                while (!sr.EndOfStream)
                                {
                                    lines.Add(sr.ReadLine());
                                }
                                sr.Close();
                                sr.Dispose();
                            }
                            if (lines.Count == 0)
                            {
                                MessageBox.Show("Arquivo vazio!");
                                return;
                            }
                            char[] delimitersTest = { ',', ';', '\t', '|' };
                            if (!delimiterDefined)
                            {

                                foreach (char d in delimitersTest)
                                {
                                    if (testDelimiter(lines, d))
                                    {
                                        delimiter = d;
                                        txtDelimiter.Text = delimiter.ToString();
                                        rdMoreColumns.Checked = true;
                                        ToDataGridSplited(lines, d, false);
                                        delimiterDefined = true;
                                        break;
                                    }
                                }

                            }
                            if (!delimiterDefined)
                            {
                                rd1Column.Checked = true;
                                //         ToDataGridSingleColumn(lines, true);

                            }

                        }
                        else if (rdExcelFile.Checked)
                        {

                        }
                    }
                    tabControl1.SelectTab("tb2");
                }
                else if (rdDataSource.Checked)
                {

                }
            }
            else if (tabControl1.SelectedTab.Name == "tb2")
            {
                if (dt != null)
                {
                    conn = Connections.GetNewConnection("REDSHIFT");
                    var qex = new QueryExecutor(conn);
                    var schemas = qex.GetSchemasNames();
                    cbSchemas.DataSource = schemas;

                    tabControl1.SelectTab("tb3");
                }
            }
            else if (tabControl1.SelectedTab.Name == "tb3")
            {

                if (dt != null)
                {
                    if (string.IsNullOrEmpty(txtTableName.Text))
                    {
                        MessageBox.Show("É necessário um nome de tabela para carregar os dados.", "Erro de preenchimento");
                        return;
                    }

                    var l = new BusinessIntelligence.Data.Redshift.RedshiftLoader(conn, cbSchemas.SelectedItem.ToString(), txtTableName.Text);

                    if (l.Load(dt))
                    {
                        MessageBox.Show("Dados carregados com sucesso!", "Carga de dados");
                        Environment.Exit(0);
                    }
                    else
                    {

                        MessageBox.Show("Erro na carga de dados.", "Carga de dados");

                    }
                }
            }
        }

        private bool testDelimiter(List<string> lines, char delimiter)
        {
            int countDelimiter = 1;
            foreach (string line in lines)
            {
                if (line.Split(delimiter).Length == 1)
                {
                    return false;
                }
                else if (countDelimiter == 1)
                {
                    countDelimiter = line.Split(delimiter).Length;
                }
                else if (countDelimiter != line.Split(delimiter).Length)
                {
                    return false;
                }
            }
            return true;

        }
        private void checkState()
        {
            btPrevious.Visible = currentStepIndex > 0;

        }
        Dictionary<int, string> sequence = new Dictionary<int, string>();
        int currentStepIndex = 0;


        private void rdTextFile_CheckedChanged(object sender, EventArgs e)
        {
            btNext.Enabled = true;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|All files (*.*)|*.*";
        }

        private void rdExcelFile_CheckedChanged(object sender, EventArgs e)
        {
            btNext.Enabled = true;
            openFileDialog1.Filter = "xls files (*.xls)|*.xls|xlsx files (*.xlsx)|*.xlsx|xlsm files (*.xlsm)|*.xlsm";
        }

        private void rdDataSource_CheckedChanged(object sender, EventArgs e)
        {
            btNext.Enabled = true;
        }

        private void txtDelimiter_KeyDown(object sender, KeyEventArgs e)
        {
            txtDelimiter.Text = "";
        }

        private void btVerifyDelimiter_Click(object sender, EventArgs e)
        {
            ToDataGridSplited(lines, txtDelimiter.Text.ToCharArray()[0], ckHasColumnsNames.Checked);
        }
        DataTable dt = null;
        private void ToDataGridSplited(List<string> lines, char delimiter, bool hasColumnsNames)
        {
            if (testDelimiter(lines, delimiter))
            {
                dt = new DataTable();
                bool isFirstLine = true;
                foreach (string line in lines)
                {
                    if (isFirstLine && hasColumnsNames)
                    {

                        if (hasColumnsNames)
                        {
                            foreach (string content in line.Split(delimiter))
                            {
                                dt.Columns.Add(content);
                            }
                        }

                    }
                    else
                    {
                        if (isFirstLine)
                        {
                            int x = 1;
                            foreach (string content in line.Split(delimiter))
                            {
                                dt.Columns.Add("Column" + x.ToString());
                                x++;
                            }

                        }

                        var r = dt.NewRow();
                        int i = 0;
                        foreach (string content in line.Split(delimiter))
                        {
                            r[i] = content;
                            i++;
                        }
                        dt.Rows.Add(r);
                    }
                    isFirstLine = false;
                }
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Este não é um delimitador válido para este arquivo.");
            }
        }
        private void ToDataGridSingleColumn(List<string> lines, bool hasColumnsNames)
        {
            dt = new DataTable();
            bool isFirstLine = true;


            foreach (string line in lines)
            {
                if (isFirstLine && hasColumnsNames)
                {
                    dt.Columns.Add(line);
                }
                else
                {
                    if (isFirstLine)
                    {
                        dt.Columns.Add("Column1");
                    }
                    var r = dt.NewRow();
                    r[0] = line;
                    dt.Rows.Add(r);
                }
                isFirstLine = false;
            }

            dataGridView1.DataSource = dt;



        }
        private void txtDelimiter_TextChanged(object sender, EventArgs e)
        {
            if (txtDelimiter.Text != delimiter.ToString())
            {
                pnDelimiterOptions.Enabled = true;
            }
        }

        private void rd1Column_CheckedChanged(object sender, EventArgs e)
        {
            pnDelimiterOptions.Enabled = !rd1Column.Checked;

            if (rd1Column.Checked)
            {
                ToDataGridSingleColumn(lines, ckHasColumnsNames.Checked);

            }
            else
            {
                ToDataGridSplited(lines, delimiter, ckHasColumnsNames.Checked);
            }
        }
        private void ckHasColumnsNames_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1Column.Checked)
            {
                ToDataGridSingleColumn(lines, ckHasColumnsNames.Checked);

            }
            else
            {
                ToDataGridSplited(lines, delimiter, ckHasColumnsNames.Checked);
            }

        }

        private void rdMoreColumns_CheckedChanged(object sender, EventArgs e)
        {
            pnDelimiterOptions.Enabled = rdMoreColumns.Checked;
           
        }
    }
}
