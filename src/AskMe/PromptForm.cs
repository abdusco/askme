﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AskMe.Core;

namespace AskMe
{
    internal sealed class PromptLabel : Label
    {
        public PromptQuery Prompt { get; }

        public PromptLabel(PromptQuery prompt)
        {
            Text = prompt.Question;
            Prompt = prompt;
        }
    }

    internal sealed class PromptTextBox : TextBox
    {
        public PromptQuery Prompt { get; }

        public PromptTextBox(PromptQuery prompt)
        {
            Text = prompt.Answer;
            Prompt = prompt;
            AutoCompleteMode = AutoCompleteMode.Suggest;
            AutoCompleteSource = AutoCompleteSource.HistoryList;
        }
    }

    public class PromptForm : Form
    {
        private IContainer components = null;
        private List<(PromptLabel, PromptTextBox)> Prompts { get; }
        private Button Save { get; set; }

        public readonly PromptResponse Response = new PromptResponse();

        public PromptForm(List<PromptQuery> questionPrompts)
        {
            Prompts = questionPrompts.Select((prompt, i) =>
            {
                var label = new PromptLabel(prompt);
                var textbox = new PromptTextBox(prompt);
                return (Label: label, TextBox: textbox);
            }).ToList();
            InitializeComponent();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(0),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            Controls.Add(panel);

            for (var index = 0; index < Prompts.Count; index++)
            {
                var (label, textbox) = Prompts[index];

                label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                label.AutoSize = true;
                label.Margin = new Padding(0, 0, 0, 4);

                textbox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                textbox.AutoSize = true;
                textbox.Margin = new Padding(0, 4, 0, 8);
                var size = TextRenderer.MeasureText(textbox.Text, textbox.Font);
                textbox.MinimumSize = new Size(Math.Min(250, size.Width) + 20, size.Height);

                panel.Controls.Add(label);
                panel.Controls.Add(textbox);
            }

            Save = new Button
            {
                Margin = new Padding(0),
                Text = "Save",
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            Save.Click += HandleClick;
            panel.Controls.Add(Save);

            SuspendLayout();
            Load += HandleOnLoad;
            Padding = new Padding(8);
            AcceptButton = Save;
            AutoSize = true;
            MaximizeBox = false;
            MinimizeBox = false;
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoScaleMode = AutoScaleMode.Dpi;
            Name = "PromptForm";
            Text = string.Empty;

            ResumeLayout(false);
        }

        private void HandleOnLoad(object sender, EventArgs e)
        {
            BringToFront();
            CenterToScreen();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void HandleClick(object sender, EventArgs e)
        {
            foreach (var (label, textBox) in Prompts)
            {
                Response.AddAnswer(label.Prompt, textBox.Text);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}