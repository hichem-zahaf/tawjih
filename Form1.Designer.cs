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
            this.textBoxTcpPrefix = new System.Windows.Forms.TextBox();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button14 = new System.Windows.Forms.Button();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.textBoxRMUrl = new System.Windows.Forms.TextBox();
            this.button11 = new System.Windows.Forms.Button();
            this.sshportTextBox = new System.Windows.Forms.TextBox();
            this.button12 = new System.Windows.Forms.Button();
            this.startsshbtn = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
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
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.closeApp = new System.Windows.Forms.Button();
            this.minApp = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // textBoxTcpPrefix
            // 
            this.textBoxTcpPrefix.BackColor = System.Drawing.Color.White;
            this.textBoxTcpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTcpPrefix.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxTcpPrefix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.textBoxTcpPrefix.Location = new System.Drawing.Point(15, 270);
            this.textBoxTcpPrefix.Name = "textBoxTcpPrefix";
            this.textBoxTcpPrefix.Size = new System.Drawing.Size(575, 25);
            this.textBoxTcpPrefix.TabIndex = 10;
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxDebug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.checkBoxDebug.Location = new System.Drawing.Point(15, 295);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(95, 19);
            this.checkBoxDebug.TabIndex = 31;
            this.checkBoxDebug.Text = "Debug Mode";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            this.checkBoxDebug.CheckedChanged += new System.EventHandler(this.checkBoxDebug_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.label6.Location = new System.Drawing.Point(15, 320);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Debug URL";
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.White;
            this.button14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button14.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(36)))), ((int)(((byte)(76)))));
            this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button14.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(36)))), ((int)(((byte)(76)))));
            this.button14.Location = new System.Drawing.Point(699, 333);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(85, 28);
            this.button14.TabIndex = 30;
            this.button14.Text = "Register URL";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.BackColor = System.Drawing.Color.White;
            this.textBoxPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPrefix.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxPrefix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.textBoxPrefix.Location = new System.Drawing.Point(15, 237);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(575, 25);
            this.textBoxPrefix.TabIndex = 19;
            // 
            // textBoxRMUrl
            // 
            this.textBoxRMUrl.BackColor = System.Drawing.Color.White;
            this.textBoxRMUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRMUrl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxRMUrl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.textBoxRMUrl.Location = new System.Drawing.Point(15, 338);
            this.textBoxRMUrl.Name = "textBoxRMUrl";
            this.textBoxRMUrl.Size = new System.Drawing.Size(681, 23);
            this.textBoxRMUrl.TabIndex = 29;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.White;
            this.button11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button11.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button11.Location = new System.Drawing.Point(596, 237);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(100, 28);
            this.button11.TabIndex = 20;
            this.button11.Text = "Start Listener";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // sshportTextBox
            // 
            this.sshportTextBox.BackColor = System.Drawing.Color.White;
            this.sshportTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sshportTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.sshportTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.sshportTextBox.Location = new System.Drawing.Point(596, 300);
            this.sshportTextBox.Name = "sshportTextBox";
            this.sshportTextBox.Size = new System.Drawing.Size(100, 23);
            this.sshportTextBox.TabIndex = 28;
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.White;
            this.button12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button12.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button12.Location = new System.Drawing.Point(596, 267);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(100, 28);
            this.button12.TabIndex = 21;
            this.button12.Text = "Start TCP Listener";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // startsshbtn
            // 
            this.startsshbtn.BackColor = System.Drawing.Color.White;
            this.startsshbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startsshbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.startsshbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startsshbtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.startsshbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.startsshbtn.Location = new System.Drawing.Point(699, 300);
            this.startsshbtn.Name = "startsshbtn";
            this.startsshbtn.Size = new System.Drawing.Size(85, 28);
            this.startsshbtn.TabIndex = 27;
            this.startsshbtn.Text = "Start SSH";
            this.startsshbtn.UseVisualStyleBackColor = false;
            this.startsshbtn.Click += new System.EventHandler(this.startsshbtn_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(36)))), ((int)(((byte)(76)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(36)))), ((int)(((byte)(76)))));
            this.button5.Location = new System.Drawing.Point(702, 237);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(82, 28);
            this.button5.TabIndex = 23;
            this.button5.Text = "Stop Listener";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.White;
            this.button8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button8.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(36)))), ((int)(((byte)(76)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(36)))), ((int)(((byte)(76)))));
            this.button8.Location = new System.Drawing.Point(699, 267);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(85, 28);
            this.button8.TabIndex = 24;
            this.button8.Text = "Stop TCP";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            //
            // button13
            //
            this.button13.BackColor = System.Drawing.Color.White;
            this.button13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button13.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button13.FlatAppearance.BorderSize = 1;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button13.Location = new System.Drawing.Point(198, 127);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(91, 36);
            this.button13.TabIndex = 26;
            this.button13.Text = "Close Window";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            //
            // button7
            //
            this.button7.BackColor = System.Drawing.Color.White;
            this.button7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button7.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button7.FlatAppearance.BorderSize = 1;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button7.Location = new System.Drawing.Point(96, 127);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(96, 36);
            this.button7.TabIndex = 16;
            this.button7.Text = "Move Mouse";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            //
            // button10
            //
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(36)))), ((int)(((byte)(76)))));
            this.button10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(36)))), ((int)(((byte)(76)))));
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(30)))), ((int)(((byte)(65)))));
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Location = new System.Drawing.Point(15, 198);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(108, 36);
            this.button10.TabIndex = 14;
            this.button10.Text = "Start Actions";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            //
            // button6
            //
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button6.FlatAppearance.BorderSize = 1;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button6.Location = new System.Drawing.Point(516, 127);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(95, 36);
            this.button6.TabIndex = 9;
            this.button6.Text = "Set Text";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.label3.Location = new System.Drawing.Point(760, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "s";
            //
            // button4
            //
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button4.FlatAppearance.BorderSize = 1;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button4.Location = new System.Drawing.Point(617, 127);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(108, 36);
            this.button4.TabIndex = 6;
            this.button4.Text = "Wait";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            //
            // button3
            //
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button3.FlatAppearance.BorderSize = 1;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button3.Location = new System.Drawing.Point(295, 127);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 36);
            this.button3.TabIndex = 5;
            this.button3.Text = "Get value";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            //
            // textBoxFieldValue
            //
            this.textBoxFieldValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.textBoxFieldValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFieldValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxFieldValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.textBoxFieldValue.Location = new System.Drawing.Point(457, 168);
            this.textBoxFieldValue.Name = "textBoxFieldValue";
            this.textBoxFieldValue.ReadOnly = true;
            this.textBoxFieldValue.Size = new System.Drawing.Size(322, 26);
            this.textBoxFieldValue.TabIndex = 4;
            //
            // textBoxTimeout
            //
            this.textBoxTimeout.BackColor = System.Drawing.Color.White;
            this.textBoxTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTimeout.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxTimeout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.textBoxTimeout.Location = new System.Drawing.Point(731, 137);
            this.textBoxTimeout.Name = "textBoxTimeout";
            this.textBoxTimeout.Size = new System.Drawing.Size(27, 26);
            this.textBoxTimeout.TabIndex = 4;
            this.textBoxTimeout.Text = "3";
            this.textBoxTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // textBoxNewValue
            //
            this.textBoxNewValue.BackColor = System.Drawing.Color.White;
            this.textBoxNewValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxNewValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxNewValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.textBoxNewValue.Location = new System.Drawing.Point(15, 168);
            this.textBoxNewValue.Name = "textBoxNewValue";
            this.textBoxNewValue.Size = new System.Drawing.Size(436, 26);
            this.textBoxNewValue.TabIndex = 4;
            //
            // button2
            //
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button2.FlatAppearance.BorderSize = 1;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button2.Location = new System.Drawing.Point(409, 127);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 36);
            this.button2.TabIndex = 3;
            this.button2.Text = "Set Value";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            //
            // button1
            //
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button1.FlatAppearance.BorderSize = 1;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.button1.Location = new System.Drawing.Point(15, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "Click";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            // txtElementXPath
            //
            this.txtElementXPath.BackColor = System.Drawing.Color.White;
            this.txtElementXPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtElementXPath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtElementXPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txtElementXPath.Location = new System.Drawing.Point(15, 100);
            this.txtElementXPath.Name = "txtElementXPath";
            this.txtElementXPath.Size = new System.Drawing.Size(764, 26);
            this.txtElementXPath.TabIndex = 1;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.label2.Location = new System.Drawing.Point(12, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Element Xpath";
            //
            // txtWindowXPath
            //
            this.txtWindowXPath.BackColor = System.Drawing.Color.White;
            this.txtWindowXPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWindowXPath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtWindowXPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txtWindowXPath.Location = new System.Drawing.Point(15, 56);
            this.txtWindowXPath.Name = "txtWindowXPath";
            this.txtWindowXPath.Size = new System.Drawing.Size(764, 26);
            this.txtWindowXPath.TabIndex = 1;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
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
            this.menuStrip2.Size = new System.Drawing.Size(795, 24);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // closeApp
            // 
            this.closeApp.BackColor = System.Drawing.Color.Transparent;
            this.closeApp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closeApp.BackgroundImage")));
            this.closeApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.closeApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.closeApp.Location = new System.Drawing.Point(764, -1);
            this.closeApp.Name = "closeApp";
            this.closeApp.Size = new System.Drawing.Size(25, 25);
            this.closeApp.TabIndex = 4;
            this.closeApp.UseVisualStyleBackColor = false;
            this.closeApp.Click += new System.EventHandler(this.closeApp_Click);
            // 
            // minApp
            // 
            this.minApp.BackColor = System.Drawing.Color.Transparent;
            this.minApp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("minApp.BackgroundImage")));
            this.minApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.minApp.ForeColor = System.Drawing.Color.Transparent;
            this.minApp.Location = new System.Drawing.Point(733, 0);
            this.minApp.Name = "minApp";
            this.minApp.Size = new System.Drawing.Size(25, 25);
            this.minApp.TabIndex = 5;
            this.minApp.UseVisualStyleBackColor = false;
            this.minApp.Click += new System.EventHandler(this.minApp_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(795, 374);
            this.Controls.Add(this.minApp);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.closeApp);
            this.Controls.Add(this.textBoxTcpPrefix);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.checkBoxDebug);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.textBoxFieldValue);
            this.Controls.Add(this.textBoxPrefix);
            this.Controls.Add(this.textBoxTimeout);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.textBoxNewValue);
            this.Controls.Add(this.textBoxRMUrl);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.txtElementXPath);
            this.Controls.Add(this.startsshbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sshportTextBox);
            this.Controls.Add(this.txtWindowXPath);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Tawjih";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.TextBox sshportTextBox;
        private System.Windows.Forms.Button startsshbtn;
        private System.Windows.Forms.TextBox textBoxRMUrl;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.Button closeApp;
        private System.Windows.Forms.Button minApp;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

