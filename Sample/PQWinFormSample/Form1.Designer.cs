namespace PQWinFormSample
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Test_MenuItemA = new System.Windows.Forms.ToolStripMenuItem();
            this.Test_MenuItemB = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.GetTools = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonPost = new System.Windows.Forms.Button();
            this.buttonGeneratePath = new System.Windows.Forms.Button();
            this.buttonCompile = new System.Windows.Forms.Button();
            this.CloseFile = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSim = new System.Windows.Forms.Button();
            this.AlignPart = new System.Windows.Forms.Button();
            this.ArcBox = new System.Windows.Forms.Button();
            this.Measure = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.buttonCollide = new System.Windows.Forms.Button();
            this.ChangeColor = new System.Windows.Forms.Button();
            this.buttonImportPoints = new System.Windows.Forms.Button();
            this.panelWorkTree = new System.Windows.Forms.Panel();
            this.panelDebugTree = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonAddCustomEvent = new System.Windows.Forms.Button();
            this.buttonGetPointInfo = new System.Windows.Forms.Button();
            this.buttonGetPointID = new System.Windows.Forms.Button();
            this.buttonDeleteGroup = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbBoxPosType = new System.Windows.Forms.ComboBox();
            this.textBoxC = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxA = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonIK = new System.Windows.Forms.Button();
            this.buttonModifyPoint = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.ContextMenuStrip = this.contextMenuStrip1;
            this.panel1.Location = new System.Drawing.Point(548, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1110, 769);
            this.panel1.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Test_MenuItemA,
            this.Test_MenuItemB});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // Test_MenuItemA
            // 
            this.Test_MenuItemA.Name = "Test_MenuItemA";
            this.Test_MenuItemA.Size = new System.Drawing.Size(152, 22);
            this.Test_MenuItemA.Text = "测试菜单项A";
            this.Test_MenuItemA.Click += new System.EventHandler(this.Test_MenuItemA_Click);
            // 
            // Test_MenuItemB
            // 
            this.Test_MenuItemB.Name = "Test_MenuItemB";
            this.Test_MenuItemB.Size = new System.Drawing.Size(152, 22);
            this.Test_MenuItemB.Text = "测试菜单项B";
            this.Test_MenuItemB.Click += new System.EventHandler(this.Test_MenuItemB_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.GetTools);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button13);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button9);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Location = new System.Drawing.Point(117, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(106, 411);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据交互";
            // 
            // GetTools
            // 
            this.GetTools.Image = ((System.Drawing.Image)(resources.GetObject("GetTools.Image")));
            this.GetTools.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GetTools.Location = new System.Drawing.Point(4, 50);
            this.GetTools.Name = "GetTools";
            this.GetTools.Size = new System.Drawing.Size(98, 24);
            this.GetTools.TabIndex = 14;
            this.GetTools.Text = "   GetTools";
            this.GetTools.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GetTools.UseVisualStyleBackColor = true;
            this.GetTools.Click += new System.EventHandler(this.GetTools_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(4, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "   GetRobots";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.GetRobots_Click);
            // 
            // button13
            // 
            this.button13.Image = ((System.Drawing.Image)(resources.GetObject("button13.Image")));
            this.button13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button13.Location = new System.Drawing.Point(4, 258);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(98, 24);
            this.button13.TabIndex = 13;
            this.button13.Text = "   GetPosture";
            this.button13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.GetPosture_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(4, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 24);
            this.button2.TabIndex = 5;
            this.button2.Text = "   GetJoints";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.GetJoints_Click);
            // 
            // button9
            // 
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.Location = new System.Drawing.Point(6, 228);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(98, 24);
            this.button9.TabIndex = 9;
            this.button9.Text = "   GetName";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.GetName_Click);
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(4, 140);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 24);
            this.button3.TabIndex = 5;
            this.button3.Text = "   GetVelocity";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.GetVelocity_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(4, 198);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(98, 24);
            this.button8.TabIndex = 8;
            this.button8.Text = "   RobotEndPos";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.GetRobotEndPos_Click);
            // 
            // button5
            // 
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(4, 110);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(98, 24);
            this.button5.TabIndex = 6;
            this.button5.Text = "   GetLinks";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.GetLinks_Click);
            // 
            // button7
            // 
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(4, 168);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(98, 24);
            this.button7.TabIndex = 7;
            this.button7.Text = "   GetTCP";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.GetTCP_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonPost);
            this.groupBox3.Controls.Add(this.buttonGeneratePath);
            this.groupBox3.Controls.Add(this.buttonCompile);
            this.groupBox3.Controls.Add(this.CloseFile);
            this.groupBox3.Controls.Add(this.buttonSave);
            this.groupBox3.Controls.Add(this.buttonSim);
            this.groupBox3.Controls.Add(this.AlignPart);
            this.groupBox3.Controls.Add(this.ArcBox);
            this.groupBox3.Controls.Add(this.Measure);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.button12);
            this.groupBox3.Location = new System.Drawing.Point(8, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(106, 411);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "模块调用";
            // 
            // buttonPost
            // 
            this.buttonPost.Image = global::PQWinFormSample.Properties.Resources.未停靠;
            this.buttonPost.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.buttonPost.Location = new System.Drawing.Point(5, 290);
            this.buttonPost.Name = "buttonPost";
            this.buttonPost.Size = new System.Drawing.Size(96, 23);
            this.buttonPost.TabIndex = 23;
            this.buttonPost.Text = "   后置";
            this.buttonPost.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.buttonPost.UseVisualStyleBackColor = true;
            this.buttonPost.Click += new System.EventHandler(this.buttonPost_Click);
            // 
            // buttonGeneratePath
            // 
            this.buttonGeneratePath.Image = ((System.Drawing.Image)(resources.GetObject("buttonGeneratePath.Image")));
            this.buttonGeneratePath.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGeneratePath.Location = new System.Drawing.Point(5, 200);
            this.buttonGeneratePath.Name = "buttonGeneratePath";
            this.buttonGeneratePath.Size = new System.Drawing.Size(98, 24);
            this.buttonGeneratePath.TabIndex = 22;
            this.buttonGeneratePath.Text = "生成轨迹";
            this.buttonGeneratePath.UseVisualStyleBackColor = true;
            this.buttonGeneratePath.Click += new System.EventHandler(this.buttonGeneratePath_Click);
            // 
            // buttonCompile
            // 
            this.buttonCompile.Image = global::PQWinFormSample.Properties.Resources.编译构建;
            this.buttonCompile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCompile.Location = new System.Drawing.Point(5, 230);
            this.buttonCompile.Name = "buttonCompile";
            this.buttonCompile.Size = new System.Drawing.Size(98, 24);
            this.buttonCompile.TabIndex = 21;
            this.buttonCompile.Text = "   编译";
            this.buttonCompile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCompile.UseVisualStyleBackColor = true;
            this.buttonCompile.Click += new System.EventHandler(this.buttonCompile_Click);
            // 
            // CloseFile
            // 
            this.CloseFile.Image = global::PQWinFormSample.Properties.Resources.close_square;
            this.CloseFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CloseFile.Location = new System.Drawing.Point(5, 80);
            this.CloseFile.Name = "CloseFile";
            this.CloseFile.Size = new System.Drawing.Size(98, 24);
            this.CloseFile.TabIndex = 17;
            this.CloseFile.Text = "关闭文件";
            this.CloseFile.UseVisualStyleBackColor = true;
            this.CloseFile.Click += new System.EventHandler(this.CloseFile_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Image = global::PQWinFormSample.Properties.Resources.save;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(5, 50);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(98, 24);
            this.buttonSave.TabIndex = 19;
            this.buttonSave.Text = "保存文件";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSim
            // 
            this.buttonSim.Image = global::PQWinFormSample.Properties.Resources.协议仿真;
            this.buttonSim.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSim.Location = new System.Drawing.Point(5, 260);
            this.buttonSim.Name = "buttonSim";
            this.buttonSim.Size = new System.Drawing.Size(98, 24);
            this.buttonSim.TabIndex = 18;
            this.buttonSim.Text = "   仿真";
            this.buttonSim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSim.UseVisualStyleBackColor = true;
            this.buttonSim.Click += new System.EventHandler(this.buttonSim_Click);
            // 
            // AlignPart
            // 
            this.AlignPart.Image = global::PQWinFormSample.Properties.Resources.校准;
            this.AlignPart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AlignPart.Location = new System.Drawing.Point(5, 170);
            this.AlignPart.Name = "AlignPart";
            this.AlignPart.Size = new System.Drawing.Size(98, 24);
            this.AlignPart.TabIndex = 15;
            this.AlignPart.Text = "三点校准";
            this.AlignPart.UseVisualStyleBackColor = true;
            this.AlignPart.Click += new System.EventHandler(this.AlignPart_Click);
            // 
            // ArcBox
            // 
            this.ArcBox.Image = global::PQWinFormSample.Properties.Resources._3D球_P3;
            this.ArcBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ArcBox.Location = new System.Drawing.Point(5, 319);
            this.ArcBox.Name = "ArcBox";
            this.ArcBox.Size = new System.Drawing.Size(98, 24);
            this.ArcBox.TabIndex = 14;
            this.ArcBox.Text = "   三维球";
            this.ArcBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ArcBox.UseVisualStyleBackColor = true;
            this.ArcBox.Click += new System.EventHandler(this.ArcBox_Click);
            // 
            // Measure
            // 
            this.Measure.Image = global::PQWinFormSample.Properties.Resources.测量工具;
            this.Measure.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Measure.Location = new System.Drawing.Point(5, 140);
            this.Measure.Name = "Measure";
            this.Measure.Size = new System.Drawing.Size(98, 24);
            this.Measure.TabIndex = 13;
            this.Measure.Text = "   测量";
            this.Measure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Measure.UseVisualStyleBackColor = true;
            this.Measure.Click += new System.EventHandler(this.Measure_Click);
            // 
            // button6
            // 
            this.button6.Image = global::PQWinFormSample.Properties.Resources.folder_open_fill;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(5, 20);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(98, 24);
            this.button6.TabIndex = 3;
            this.button6.Text = "打开文件";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // button12
            // 
            this.button12.Image = ((System.Drawing.Image)(resources.GetObject("button12.Image")));
            this.button12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button12.Location = new System.Drawing.Point(5, 110);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(98, 24);
            this.button12.TabIndex = 12;
            this.button12.Text = "  导入3D文件";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.Import3DObj_Click);
            // 
            // buttonCollide
            // 
            this.buttonCollide.Image = global::PQWinFormSample.Properties.Resources.校准;
            this.buttonCollide.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCollide.Location = new System.Drawing.Point(6, 179);
            this.buttonCollide.Name = "buttonCollide";
            this.buttonCollide.Size = new System.Drawing.Size(98, 24);
            this.buttonCollide.TabIndex = 24;
            this.buttonCollide.Text = "碰撞检测";
            this.buttonCollide.UseVisualStyleBackColor = true;
            this.buttonCollide.Click += new System.EventHandler(this.buttonCollide_Click);
            // 
            // ChangeColor
            // 
            this.ChangeColor.Image = global::PQWinFormSample.Properties.Resources.画板;
            this.ChangeColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ChangeColor.Location = new System.Drawing.Point(5, 149);
            this.ChangeColor.Name = "ChangeColor";
            this.ChangeColor.Size = new System.Drawing.Size(98, 24);
            this.ChangeColor.TabIndex = 16;
            this.ChangeColor.Text = "改变颜色";
            this.ChangeColor.UseVisualStyleBackColor = true;
            this.ChangeColor.Click += new System.EventHandler(this.ChangeColor_Click);
            // 
            // buttonImportPoints
            // 
            this.buttonImportPoints.Image = ((System.Drawing.Image)(resources.GetObject("buttonImportPoints.Image")));
            this.buttonImportPoints.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonImportPoints.Location = new System.Drawing.Point(7, 230);
            this.buttonImportPoints.Name = "buttonImportPoints";
            this.buttonImportPoints.Size = new System.Drawing.Size(98, 24);
            this.buttonImportPoints.TabIndex = 20;
            this.buttonImportPoints.Text = "  导入轨迹点";
            this.buttonImportPoints.UseVisualStyleBackColor = true;
            this.buttonImportPoints.Click += new System.EventHandler(this.buttonImportPoints_Click);
            // 
            // panelWorkTree
            // 
            this.panelWorkTree.BackColor = System.Drawing.Color.PeachPuff;
            this.panelWorkTree.Location = new System.Drawing.Point(227, 2);
            this.panelWorkTree.Name = "panelWorkTree";
            this.panelWorkTree.Size = new System.Drawing.Size(320, 378);
            this.panelWorkTree.TabIndex = 17;
            // 
            // panelDebugTree
            // 
            this.panelDebugTree.BackColor = System.Drawing.Color.PeachPuff;
            this.panelDebugTree.Location = new System.Drawing.Point(227, 386);
            this.panelDebugTree.Name = "panelDebugTree";
            this.panelDebugTree.Size = new System.Drawing.Size(320, 385);
            this.panelDebugTree.TabIndex = 18;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonCollide);
            this.groupBox4.Controls.Add(this.buttonAddCustomEvent);
            this.groupBox4.Controls.Add(this.buttonGetPointInfo);
            this.groupBox4.Controls.Add(this.buttonGetPointID);
            this.groupBox4.Controls.Add(this.buttonDeleteGroup);
            this.groupBox4.Controls.Add(this.ChangeColor);
            this.groupBox4.Location = new System.Drawing.Point(7, 428);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(106, 343);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "命令接口";
            // 
            // buttonAddCustomEvent
            // 
            this.buttonAddCustomEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonAddCustomEvent.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAddCustomEvent.Image = global::PQWinFormSample.Properties.Resources._event;
            this.buttonAddCustomEvent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddCustomEvent.Location = new System.Drawing.Point(5, 107);
            this.buttonAddCustomEvent.Name = "buttonAddCustomEvent";
            this.buttonAddCustomEvent.Size = new System.Drawing.Size(98, 36);
            this.buttonAddCustomEvent.TabIndex = 17;
            this.buttonAddCustomEvent.Text = "   添加自定义     事件";
            this.buttonAddCustomEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddCustomEvent.UseVisualStyleBackColor = false;
            this.buttonAddCustomEvent.Click += new System.EventHandler(this.buttonAddCustomEvent_Click);
            // 
            // buttonGetPointInfo
            // 
            this.buttonGetPointInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonGetPointInfo.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonGetPointInfo.Image = ((System.Drawing.Image)(resources.GetObject("buttonGetPointInfo.Image")));
            this.buttonGetPointInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGetPointInfo.Location = new System.Drawing.Point(5, 78);
            this.buttonGetPointInfo.Name = "buttonGetPointInfo";
            this.buttonGetPointInfo.Size = new System.Drawing.Size(98, 24);
            this.buttonGetPointInfo.TabIndex = 17;
            this.buttonGetPointInfo.Text = "   获取点信息";
            this.buttonGetPointInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGetPointInfo.UseVisualStyleBackColor = false;
            this.buttonGetPointInfo.Click += new System.EventHandler(this.buttonGetPointInfo_Click);
            // 
            // buttonGetPointID
            // 
            this.buttonGetPointID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonGetPointID.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonGetPointID.Image = ((System.Drawing.Image)(resources.GetObject("buttonGetPointID.Image")));
            this.buttonGetPointID.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGetPointID.Location = new System.Drawing.Point(5, 49);
            this.buttonGetPointID.Name = "buttonGetPointID";
            this.buttonGetPointID.Size = new System.Drawing.Size(98, 24);
            this.buttonGetPointID.TabIndex = 19;
            this.buttonGetPointID.Text = "   获取点ID";
            this.buttonGetPointID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGetPointID.UseVisualStyleBackColor = false;
            this.buttonGetPointID.Click += new System.EventHandler(this.buttonGetPointID_Click);
            // 
            // buttonDeleteGroup
            // 
            this.buttonDeleteGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonDeleteGroup.Image = global::PQWinFormSample.Properties.Resources.删除;
            this.buttonDeleteGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteGroup.Location = new System.Drawing.Point(5, 20);
            this.buttonDeleteGroup.Name = "buttonDeleteGroup";
            this.buttonDeleteGroup.Size = new System.Drawing.Size(98, 24);
            this.buttonDeleteGroup.TabIndex = 3;
            this.buttonDeleteGroup.Text = "   删除轨迹组";
            this.buttonDeleteGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteGroup.UseVisualStyleBackColor = false;
            this.buttonDeleteGroup.Click += new System.EventHandler(this.buttonDeleteGroup_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.cbBoxPosType);
            this.groupBox5.Controls.Add(this.textBoxC);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.textBoxB);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.textBoxA);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.textBoxZ);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.textBoxY);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.textBoxX);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.buttonIK);
            this.groupBox5.Controls.Add(this.buttonModifyPoint);
            this.groupBox5.Controls.Add(this.buttonImportPoints);
            this.groupBox5.Location = new System.Drawing.Point(116, 428);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(106, 343);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "轨迹点";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "欧拉角旋转方式";
            // 
            // cbBoxPosType
            // 
            this.cbBoxPosType.FormattingEnabled = true;
            this.cbBoxPosType.Location = new System.Drawing.Point(9, 204);
            this.cbBoxPosType.Name = "cbBoxPosType";
            this.cbBoxPosType.Size = new System.Drawing.Size(90, 20);
            this.cbBoxPosType.TabIndex = 23;
            // 
            // textBoxC
            // 
            this.textBoxC.Location = new System.Drawing.Point(30, 157);
            this.textBoxC.Name = "textBoxC";
            this.textBoxC.Size = new System.Drawing.Size(69, 21);
            this.textBoxC.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "C:";
            // 
            // textBoxB
            // 
            this.textBoxB.Location = new System.Drawing.Point(30, 130);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(69, 21);
            this.textBoxB.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "B:";
            // 
            // textBoxA
            // 
            this.textBoxA.Location = new System.Drawing.Point(30, 103);
            this.textBoxA.Name = "textBoxA";
            this.textBoxA.Size = new System.Drawing.Size(69, 21);
            this.textBoxA.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "A:";
            // 
            // textBoxZ
            // 
            this.textBoxZ.Location = new System.Drawing.Point(30, 76);
            this.textBoxZ.Name = "textBoxZ";
            this.textBoxZ.Size = new System.Drawing.Size(69, 21);
            this.textBoxZ.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "Z:";
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(30, 49);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(69, 21);
            this.textBoxY.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "Y:";
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(30, 22);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(69, 21);
            this.textBoxX.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "X:";
            // 
            // buttonIK
            // 
            this.buttonIK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonIK.Image = global::PQWinFormSample.Properties.Resources.校准;
            this.buttonIK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonIK.Location = new System.Drawing.Point(6, 290);
            this.buttonIK.Name = "buttonIK";
            this.buttonIK.Size = new System.Drawing.Size(98, 24);
            this.buttonIK.TabIndex = 3;
            this.buttonIK.Text = "   运动学算法";
            this.buttonIK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonIK.UseVisualStyleBackColor = false;
            this.buttonIK.Click += new System.EventHandler(this.buttonIK_Click);
            // 
            // buttonModifyPoint
            // 
            this.buttonModifyPoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonModifyPoint.Image = global::PQWinFormSample.Properties.Resources.测量工具;
            this.buttonModifyPoint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonModifyPoint.Location = new System.Drawing.Point(7, 260);
            this.buttonModifyPoint.Name = "buttonModifyPoint";
            this.buttonModifyPoint.Size = new System.Drawing.Size(98, 24);
            this.buttonModifyPoint.TabIndex = 3;
            this.buttonModifyPoint.Text = "   编辑轨迹点";
            this.buttonModifyPoint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonModifyPoint.UseVisualStyleBackColor = false;
            this.buttonModifyPoint.Click += new System.EventHandler(this.buttonModifyPoint_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1664, 779);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panelDebugTree);
            this.Controls.Add(this.panelWorkTree);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PQ Platform Sample";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button AlignPart;
        private System.Windows.Forms.Button ArcBox;
        private System.Windows.Forms.Button Measure;
        private System.Windows.Forms.Button ChangeColor;
        private System.Windows.Forms.Button CloseFile;
        private System.Windows.Forms.Button GetTools;
        private System.Windows.Forms.Panel panelWorkTree;
        private System.Windows.Forms.Panel panelDebugTree;
        private System.Windows.Forms.Button buttonSim;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonImportPoints;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonGetPointInfo;
        private System.Windows.Forms.Button buttonGetPointID;
        private System.Windows.Forms.Button buttonDeleteGroup;
        private System.Windows.Forms.Button buttonAddCustomEvent;
        private System.Windows.Forms.Button buttonCompile;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonModifyPoint;
        private System.Windows.Forms.TextBox textBoxC;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonIK;
        private System.Windows.Forms.Button buttonGeneratePath;
        private System.Windows.Forms.Button buttonPost;
        private System.Windows.Forms.ComboBox cbBoxPosType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonCollide;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Test_MenuItemA;
        private System.Windows.Forms.ToolStripMenuItem Test_MenuItemB;
    }
}

