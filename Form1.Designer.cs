namespace UIAutomation
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.button14 = new System.Windows.Forms.Button();
            this.textBoxRMUrl = new System.Windows.Forms.TextBox();
            this.sshportTextBox = new System.Windows.Forms.TextBox();
            this.startsshbtn = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBoxRM = new System.Windows.Forms.TextBox();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.textBoxTcpPrefix = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBoxFieldValue = new System.Windows.Forms.TextBox();
            this.textBoxTimeout = new System.Windows.Forms.TextBox();
            this.textBoxNewValue = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtElementXPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWindowXPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxDebug);
            this.panel1.Controls.Add(this.button14);
            this.panel1.Controls.Add(this.textBoxRMUrl);
            this.panel1.Controls.Add(this.sshportTextBox);
            this.panel1.Controls.Add(this.startsshbtn);
            this.panel1.Controls.Add(this.button13);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.textBoxRM);
            this.panel1.Controls.Add(this.button12);
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.textBoxPrefix);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.textBoxTcpPrefix);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.textBoxFieldValue);
            this.panel1.Controls.Add(this.textBoxTimeout);
            this.panel1.Controls.Add(this.textBoxNewValue);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtElementXPath);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtWindowXPath);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 451);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(9, 365);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(88, 17);
            this.checkBoxDebug.TabIndex = 31;
            this.checkBoxDebug.Text = "Debug Mode";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            this.checkBoxDebug.CheckedChanged += new System.EventHandler(this.checkBoxDebug_CheckedChanged);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(514, 398);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(80, 23);
            this.button14.TabIndex = 30;
            this.button14.Text = "Register URL";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // textBoxRMUrl
            // 
            this.textBoxRMUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRMUrl.Location = new System.Drawing.Point(6, 401);
            this.textBoxRMUrl.Name = "textBoxRMUrl";
            this.textBoxRMUrl.Size = new System.Drawing.Size(502, 20);
            this.textBoxRMUrl.TabIndex = 29;
            this.textBoxRMUrl.Text = "Robomotion Debug Endpoint ";
            // 
            // sshportTextBox
            // 
            this.sshportTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sshportTextBox.Location = new System.Drawing.Point(6, 339);
            this.sshportTextBox.Name = "sshportTextBox";
            this.sshportTextBox.Size = new System.Drawing.Size(79, 20);
            this.sshportTextBox.TabIndex = 28;
            this.sshportTextBox.Text = "3000";
            // 
            // startsshbtn
            // 
            this.startsshbtn.Location = new System.Drawing.Point(91, 336);
            this.startsshbtn.Name = "startsshbtn";
            this.startsshbtn.Size = new System.Drawing.Size(97, 23);
            this.startsshbtn.TabIndex = 27;
            this.startsshbtn.Text = "Start SSH ";
            this.startsshbtn.UseVisualStyleBackColor = true;
            this.startsshbtn.Click += new System.EventHandler(this.startsshbtn_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(222, 213);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(91, 33);
            this.button13.TabIndex = 26;
            this.button13.Text = "Close Window";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(514, 310);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(80, 23);
            this.button9.TabIndex = 25;
            this.button9.Text = "Test TCP";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click_1);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(514, 284);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(80, 23);
            this.button8.TabIndex = 24;
            this.button8.Text = "Stop TCP";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(514, 256);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(81, 23);
            this.button5.TabIndex = 23;
            this.button5.Text = "Stop listner";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // textBoxRM
            // 
            this.textBoxRM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRM.Location = new System.Drawing.Point(6, 313);
            this.textBoxRM.Name = "textBoxRM";
            this.textBoxRM.ReadOnly = true;
            this.textBoxRM.Size = new System.Drawing.Size(502, 20);
            this.textBoxRM.TabIndex = 22;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(423, 285);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(85, 23);
            this.button12.TabIndex = 21;
            this.button12.Text = "Start tcp listner";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(423, 256);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(85, 23);
            this.button11.TabIndex = 20;
            this.button11.Text = "Start Listner";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPrefix.Location = new System.Drawing.Point(6, 259);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(411, 20);
            this.textBoxPrefix.TabIndex = 19;
            this.textBoxPrefix.Text = "http://172.18.1.81:3000/rpa/";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(120, 213);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(96, 33);
            this.button7.TabIndex = 16;
            this.button7.Text = "Move Mouse";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button10.Location = new System.Drawing.Point(6, 213);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(108, 33);
            this.button10.TabIndex = 14;
            this.button10.Text = "Start Actions";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // textBoxTcpPrefix
            // 
            this.textBoxTcpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTcpPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBoxTcpPrefix.Location = new System.Drawing.Point(6, 285);
            this.textBoxTcpPrefix.Name = "textBoxTcpPrefix";
            this.textBoxTcpPrefix.Size = new System.Drawing.Size(411, 23);
            this.textBoxTcpPrefix.TabIndex = 10;
            this.textBoxTcpPrefix.Text = "172.18.1.81:3000";
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button6.Location = new System.Drawing.Point(500, 103);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(95, 30);
            this.button6.TabIndex = 9;
            this.button6.Text = "Set Text";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(153, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Seconds";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button4.Location = new System.Drawing.Point(6, 178);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(108, 29);
            this.button4.TabIndex = 6;
            this.button4.Text = "Wait";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button3.Location = new System.Drawing.Point(6, 141);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 33);
            this.button3.TabIndex = 5;
            this.button3.Text = "Get value";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBoxFieldValue
            // 
            this.textBoxFieldValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFieldValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBoxFieldValue.Location = new System.Drawing.Point(120, 147);
            this.textBoxFieldValue.Name = "textBoxFieldValue";
            this.textBoxFieldValue.ReadOnly = true;
            this.textBoxFieldValue.Size = new System.Drawing.Size(474, 23);
            this.textBoxFieldValue.TabIndex = 4;
            // 
            // textBoxTimeout
            // 
            this.textBoxTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBoxTimeout.Location = new System.Drawing.Point(120, 182);
            this.textBoxTimeout.Name = "textBoxTimeout";
            this.textBoxTimeout.Size = new System.Drawing.Size(27, 23);
            this.textBoxTimeout.TabIndex = 4;
            this.textBoxTimeout.Text = "3";
            this.textBoxTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxNewValue
            // 
            this.textBoxNewValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxNewValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBoxNewValue.Location = new System.Drawing.Point(187, 108);
            this.textBoxNewValue.Name = "textBoxNewValue";
            this.textBoxNewValue.Size = new System.Drawing.Size(210, 23);
            this.textBoxNewValue.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button2.Location = new System.Drawing.Point(84, 102);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "Set Value";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(6, 102);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 2;
            this.button1.Text = "Click";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtElementXPath
            // 
            this.txtElementXPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtElementXPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtElementXPath.Location = new System.Drawing.Point(6, 73);
            this.txtElementXPath.Name = "txtElementXPath";
            this.txtElementXPath.Size = new System.Drawing.Size(589, 23);
            this.txtElementXPath.TabIndex = 1;
            this.txtElementXPath.Text = "//Table[@Name=\'DataGridView\']";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Element Xpath";
            // 
            // txtWindowXPath
            // 
            this.txtWindowXPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWindowXPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtWindowXPath.Location = new System.Drawing.Point(6, 29);
            this.txtWindowXPath.Name = "txtWindowXPath";
            this.txtWindowXPath.Size = new System.Drawing.Size(589, 23);
            this.txtWindowXPath.TabIndex = 1;
            this.txtWindowXPath.Text = "//Window[@Name=\'SelfishNet v0.1 Beta\']";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 385);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Debug URL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Windows Xpath";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Notification Icon";
            this.notifyIcon1.Visible = true;
            // 
            // menuStrip2
            // 
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(786, 24);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(786, 565);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Oakland RPA v2.0.3 [PHME]";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtElementXPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWindowXPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNewValue;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxFieldValue;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBoxTimeout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBoxTcpPrefix;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.TextBox textBoxRM;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.TextBox sshportTextBox;
        private System.Windows.Forms.Button startsshbtn;
        private System.Windows.Forms.TextBox textBoxRMUrl;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MenuStrip menuStrip2;
    }
}

