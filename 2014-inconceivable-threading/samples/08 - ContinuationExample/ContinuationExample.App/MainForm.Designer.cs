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
            this.SuspendLayout();
            // 
            // btnDoWorkNormal
            // 
            this.btnDoWorkNormal.Location = new System.Drawing.Point(12, 12);
            this.btnDoWorkNormal.Name = "btnDoWorkNormal";
            this.btnDoWorkNormal.Size = new System.Drawing.Size(181, 23);
            this.btnDoWorkNormal.TabIndex = 7;
            this.btnDoWorkNormal.Text = "BLOCKED UI";
            this.btnDoWorkNormal.UseVisualStyleBackColor = true;
            this.btnDoWorkNormal.Click += new System.EventHandler(this.btnDoWorkNormal_Click);
            // 
            // btnDoWorkContinuation
            // 
            this.btnDoWorkContinuation.Location = new System.Drawing.Point(12, 41);
            this.btnDoWorkContinuation.Name = "btnDoWorkContinuation";
            this.btnDoWorkContinuation.Size = new System.Drawing.Size(181, 23);
            this.btnDoWorkContinuation.TabIndex = 12;
            this.btnDoWorkContinuation.Text = "NONBLOCKED UI (manual)";
            this.btnDoWorkContinuation.UseVisualStyleBackColor = true;
            this.btnDoWorkContinuation.Click += new System.EventHandler(this.btnDoWorkContinuation_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 158);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(181, 215);
            this.txtLog.TabIndex = 13;
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Location = new System.Drawing.Point(27, 116);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(60, 13);
            this.lblTotalTime.TabIndex = 10;
            this.lblTotalTime.Text = "Total Time:";
            // 
            // txtTotalTime
            // 
            this.txtTotalTime.Location = new System.Drawing.Point(93, 113);
            this.txtTotalTime.Name = "txtTotalTime";
            this.txtTotalTime.Size = new System.Drawing.Size(100, 20);
            this.txtTotalTime.TabIndex = 11;
            // 
            // btnDoWorkAsync
            // 
            this.btnDoWorkAsync.Location = new System.Drawing.Point(12, 70);
            this.btnDoWorkAsync.Name = "btnDoWorkAsync";
            this.btnDoWorkAsync.Size = new System.Drawing.Size(181, 23);
            this.btnDoWorkAsync.TabIndex = 14;
            this.btnDoWorkAsync.Text = "NONBLOCKED UI (async)";
            this.btnDoWorkAsync.UseVisualStyleBackColor = true;
            this.btnDoWorkAsync.Click += new System.EventHandler(this.btnDoWorkAsync_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 385);
            this.Controls.Add(this.btnDoWorkAsync);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnDoWorkContinuation);
            this.Controls.Add(this.txtTotalTime);
            this.Controls.Add(this.lblTotalTime);
            this.Controls.Add(this.btnDoWorkNormal);
            this.Name = "MainForm";
            this.Text = "Continuation Example";
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
    }
}