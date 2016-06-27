using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 写字板
{
    public partial class Form1 : Form
    {
        bool saveFlag = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            打开ToolStripMenuItem.Enabled = true;
            保存ToolStripMenuItem.Enabled = false;
            颜色ToolStripMenuItem.Enabled = false;
            字体ToolStripMenuItem.Enabled = false;
            另存为ToolStripMenuItem.Enabled = true;
            toolStripStatusLabel2.Text = "";
            saveFlag = true;
            toolStripStatusLabel1.Text = "请选择文件";
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().Show();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFlag == false)
            {
                if (MessageBox.Show("你确定要放弃保存并打开新的文件？", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "RTF File(*.rtf)|*.RTF|TXT FILE(*.txt)|*.txt";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                switch (openFileDialog1.FilterIndex)
                {
                    case 1: richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);
                        break;
                    case 2: richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;
                }

                保存ToolStripMenuItem.Enabled = true;
                颜色ToolStripMenuItem.Enabled = true;
                字体ToolStripMenuItem.Enabled = true;
                saveFlag = true;
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName != "")
            {
                richTextBox1.SaveFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);
                toolStripStatusLabel1.Text = "保存完成";
                saveFlag = true;
            }
            //else()
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "RTF File(*.rtf)|*.RTF|TXT FILE(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                switch (openFileDialog1.FilterIndex)
                {
                    case 1: richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText); break;
                    case 2: richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText); break;
                }
                saveFlag = true;
            }
        }

        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = true;
            colorDialog1.AnyColor = true;
            colorDialog1.SolidColorOnly = false;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectedText == "")
                    richTextBox1.SelectAll();
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.AllowVectorFonts = true;
            fontDialog1.AllowVerticalFonts = true;
            fontDialog1.FixedPitchOnly = false;
            fontDialog1.MaxSize = 75;
            fontDialog1.MinSize = 5;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectedText == "")
                    richTextBox1.SelectAll();
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            保存ToolStripMenuItem.Enabled = true;
            颜色ToolStripMenuItem.Enabled = true;
            字体ToolStripMenuItem.Enabled = true;
            
            string line = (1 + richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart)).ToString();
            string column = (1 + richTextBox1.SelectionStart - (richTextBox1.GetFirstCharIndexFromLine(1 + richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) - 1))).ToString();
            toolStripStatusLabel1.Text = "第 " + line + " 行" + ", " + column + " 列";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //string cnt = richTextBox1.SelectionStart.ToString();
            toolStripStatusLabel2.Text = richTextBox1.Text.Length + " 个字符";
            saveFlag = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saveFlag == false)
            {
                if (MessageBox.Show("你确定要放弃保存并关闭？", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void 显示源代码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //richTextBox1.LoadFile(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "source code.doc", RichTextBoxStreamType.RichText);
            try
            {
                string sourceCode = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "source code.rtf";
                richTextBox1.LoadFile(sourceCode, RichTextBoxStreamType.RichText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("请确定 'source code.rtf' 和可执行文件在同一文件目录下", "找不到文件");
            }
            finally
            {
                saveFlag = true;
            }    
        }

        private void 学生信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "计科 15-01 \n\n541507010144 \n王子毓\n\n\n\n\n\n\n\n\n\n\n祝老师，身体健康，工作顺利";
            saveFlag = true;
        }

        private void 访问网站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/wangziyu9/CSharp-practise/blob/master/Notepad.cs");
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void New_Click(object sender, EventArgs e)
        {
            new Form1().Show();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            if (saveFlag == false)
            {
                if (MessageBox.Show("你确定要放弃保存并打开新的文件？", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "RTF File(*.rtf)|*.RTF|TXT FILE(*.txt)|*.txt";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                switch (openFileDialog1.FilterIndex)
                {
                    case 1:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);
                        break;
                    case 2:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;
                }

                保存ToolStripMenuItem.Enabled = true;
                颜色ToolStripMenuItem.Enabled = true;
                字体ToolStripMenuItem.Enabled = true;
                saveFlag = true;
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName != "")
            {
                richTextBox1.SaveFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);
                toolStripStatusLabel1.Text = "保存完成";
                saveFlag = true;
            }
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void About_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/wangziyu9/CSharp-practise/blob/master/Notepad.cs");
        }
    }
}
