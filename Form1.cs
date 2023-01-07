using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberCodeFunction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            Clear();

            if (txtItem1.Text.Length > 0) Exec(txtItem1.Text);
            if (txtItem2.Text.Length > 0) Exec(txtItem2.Text);
            if (txtItem3.Text.Length > 0) Exec(txtItem3.Text);

            double ds1 = Convert.ToDouble(lbl08.Text) + Convert.ToDouble(lbl18.Text);
            double ds2 = Convert.ToDouble(lbl28.Text);
            double d1 = ds1 / Convert.ToDouble(txt19.Text);
            double d2 = ds2 / Convert.ToDouble(txt29.Text);
            (tableLayoutPanel2.Controls.Find("lblS1", true).FirstOrDefault() as Label).Text = d1.ToString();
            (tableLayoutPanel2.Controls.Find("lblS2", true).FirstOrDefault() as Label).Text = d2.ToString();
            (tableLayoutPanel2.Controls.Find("lblInfo", true).FirstOrDefault() as Label).Text =
                string.Format("灰白:{0}, 綠:{1}", ds1, ds2);

            lblInfo_DoubleClick(sender, e);
        }
        
        void Exec(string sData)
        {
            if (sData.Length > 0)
            {
                string sItem = 
                    sData.IndexOf("廢棄") != -1 ? "0" :
                    sData.IndexOf("普通") != -1 ? "1" :
                    sData.IndexOf("高級") != -1 ? "2" : "";

                sData = sData.Replace("快取記憶體\r\n\r\n", "快取記憶體").Replace("x ", " ");
                sData = sData + "\r\n 總數 快取記憶體 0";
                string[] aryData = sData.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string s in aryData)
                {
                    string[] aryItem = s.Split(' ');
                    string sIdx = "0";
                    
                    switch(aryItem[1])
                    {
                        case "上身裝甲": sIdx = "0"; break;
                        case "下身裝甲": sIdx = "1"; break;
                        case "升級模組": sIdx = "2"; break;
                        case "主武器": sIdx = "3"; break;
                        case "特殊武器": sIdx = "4"; break;
                        case "破壞性武器": sIdx = "5"; break;
                        case "靴子": sIdx = "6"; break;
                        case "頭盔": sIdx = "7"; break;
                        case "總數": sIdx = "8"; break;
                    }
                    Label l = tableLayoutPanel2.Controls.Find("lbl" + sItem + sIdx, true).FirstOrDefault() as Label;
                    if (l != null)
                        l.Text = aryItem[3];
                }
                Exec1("lbl" + sItem);
            }
        }

        void Clear()
        {
            foreach(var l in tableLayoutPanel2.Controls.OfType<Label>()
                .Where(c => c.Name.StartsWith("lbl")))
                l.Text= "0";
        }

        void Exec1(string sObj)
        {
            double dCnt = tableLayoutPanel2.Controls.OfType<Label>()
                .Where(c => c.Name.IndexOf(sObj) != -1)
                .Sum(s => Convert.ToDouble(s.Text))
                ;
            Label l = Controls.Find(sObj + "8", true).FirstOrDefault() as Label;
            if (l != null)
                l.Text = dCnt.ToString();
        }

        private void lblInfo_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(lblInfo.Text);
            MessageBox.Show("已複製");
        }
        
        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
                btnExec_Click(sender, e);
        }

    }
}
