namespace ContinuationExample {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnDoWorkNormal = new System.Windows.Forms.Button();
            this.btnDoWorkContinuation = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.txtTotalTime = new System.Windows.Forms.TextBox();
            this.btnDoWorkAsync = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnNonBlockedUIManual = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDoWorkNormal
            // 
            this.btnDoWorkNormal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoWorkNormal.Location = new System.Drawing.Point(24, 23);
            this.btnDoWorkNormal.Margin = new System.Windows.Forms.Padding(6);
            this.btnDoWorkNormal.Name = "btnDoWorkNormal";
            this.btnDoWorkNormal.Size = new System.Drawing.Size(827, 44);
            this.btnDoWorkNormal.TabIndex = 7;
            this.btnDoWorkNormal.Text = "BLOCKED UI";
            this.btnDoWorkNormal.UseVisualStyleBackColor = true;
            this.btnDoWorkNormal.Click += new System.EventHandler(this.btnDoWorkNormal_Click);
            // 
            // btnDoWorkContinuation
            // 
            this.btnDoWorkContinuation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoWorkContinuation.Location = new System.Drawing.Point(24, 135);
            this.btnDoWorkContinuation.Margin = new System.Windows.Forms.Padding(6);
            this.btnDoWorkContinuation.Name = "btnDoWorkContinuation";
            this.btnDoWorkContinuation.Size = new System.Drawing.Size(827, 44);
            this.btnDoWorkContinuation.TabIndex = 12;
            this.btnDoWorkContinuation.Text = "NONBLOCKED UI (intermediate)";
            this.btnDoWorkContinuation.UseVisualStyleBackColor = true;
            this.btnDoWorkContinuation.Click += new System.EventHandler(this.btnDoWorkContinuation_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(24, 326);
            this.txtLog.Margin = new System.Windows.Forms.Padding(6);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(827, 558);
            this.txtLog.TabIndex = 13;
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Location = new System.Drawing.Point(54, 265);
            this.lblTotalTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(119, 25);
            this.lblTotalTime.TabIndex = 10;
            this.lblTotalTime.Text = "Total Time:";
            // 
            // txtTotalTime
            // 
            this.txtTotalTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalTime.Location = new System.Drawing.Point(186, 259);
            this.txtTotalTime.Margin = new System.Windows.Forms.Padding(6);
            this.txtTotalTime.Name = "txtTotalTime";
            this.txtTotalTime.Size = new System.Drawing.Size(665, 31);
            this.txtTotalTime.TabIndex = 11;
            // 
            // btnDoWorkAsync
            // 
            this.btnDoWorkAsync.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoWorkAsync.Location = new System.Drawing.Point(24, 191);
            this.btnDoWorkAsync.Margin = new System.Windows.Forms.Padding(6);
            this.btnDoWorkAsync.Name = "btnDoWorkAsync";
            this.btnDoWorkAsync.Size = new System.Drawing.Size(827, 44);
            this.btnDoWorkAsync.TabIndex = 14;
            this.btnDoWorkAsync.Text = "NONBLOCKED UI (async)";
            this.btnDoWorkAsync.UseVisualStyleBackColor = true;
            this.btnDoWorkAsync.Click += new System.EventHandler(this.btnDoWorkAsync_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 924);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(866, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip";
            // 
            // toolStripLabel
            // 
            this.toolStripLabel.Name = "toolStripLabel";
            this.toolStripLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // btnNonBlockedUIManual
            // 
            this.btnNonBlockedUIManual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNonBlockedUIManual.Location = new System.Drawing.Point(24, 79);
            this.btnNonBlockedUIManual.Margin = new System.Windows.Forms.Padding(6);
            this.btnNonBlockedUIManual.Name = "btnNonBlockedUIManual";
            this.btnNonBlockedUIManual.Size = new System.Drawing.Size(827, 44);
            this.btnNonBlockedUIManual.TabIndex = 16;
            this.btnNonBlockedUIManual.Text = "NONBLOCKED UI (manual)";
            this.btnNonBlockedUIManual.UseVisualStyleBackColor = true;
            this.btnNonBlockedUIManual.Click += new System.EventHandler(this.btnNonBlockedUIManual_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 946);
            this.Controls.Add(this.btnNonBlockedUIManual);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnDoWorkAsync);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnDoWorkContinuation);
            this.Controls.Add(this.txtTotalTime);
            this.Controls.Add(this.lblTotalTime);
            this.Controls.Add(this.btnDoWorkNormal);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainForm";
            this.Text = "Continuation Example";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDoWorkNormal;
        private System.Windows.Forms.Button btnDoWorkContinuation;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.TextBox txtTotalTime;
        private System.Windows.Forms.Button btnDoWorkAsync;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Button btnNonBlockedUIManual;
    }
}