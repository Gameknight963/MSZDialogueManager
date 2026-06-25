using System.ComponentModel;
using System.Diagnostics;

namespace MSZDialougeManager.Styling
{
    public class ThemeableForm : Form
    {
        protected readonly ControlStyle _headerStyle = new();

        private bool _useTextRenderer = true;
        private static bool IsDesignTime => LicenseManager.UsageMode == LicenseUsageMode.Designtime || Process.GetCurrentProcess().ProcessName 
            is "DesignToolsServer" or "devenv";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ThemeManager.Theme ActiveTheme { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ThemeManager.Theme ResolvedTheme => ThemeManager.ResolveTheme(ActiveTheme);

        private readonly HashSet<Control> _themedControls = new();

        public ThemeableForm()
        {
            if (IsDesignTime) return;
            ActiveTheme = ThemeManager.ActiveTheme;
            Load += (sender, e) => ApplyTheme(ThemeManager.ActiveTheme, ThemeManager.ActiveTextRenderMode);
        }

        protected virtual void OnThemeWasApplied(ThemeManager.Theme resolvedTheme) { }

        private void ApplyControlTheme(Control c, ThemeManager.Theme theme)
        {
            ThemeManager.Theme resolvedTheme = ThemeManager.ResolveTheme(theme);
            if (IsDesignTime) return;
            if (c is ListView lv)
            {
                if (_themedControls.Add(lv))
                {
                    lv.DrawColumnHeader += Lv_DrawColumnHeader;
                    lv.DrawItem += Lv_DrawItem;
                    lv.DrawSubItem += Lv_DrawSubItem;
                }

                lv.OwnerDraw = (resolvedTheme != ThemeManager.Theme.Light);
            }

            if (c is TabControl tc)
            {
                if (_themedControls.Add(tc))
                {
                    tc.DrawItem += Tc_DrawItem;
                }

                if (resolvedTheme == ThemeManager.Theme.Light)
                {
                    tc.DrawMode = TabDrawMode.Normal;
                    return;
                }

                tc.DrawMode = TabDrawMode.OwnerDrawFixed;
            }
            if (c is DataGridView dgv)
            {
                if (_themedControls.Add(dgv))
                {
                    dgv.CellPainting += Dgv_CellPainting;
                    dgv.EnableHeadersVisualStyles = false;
                }

                dgv.BackgroundColor = _headerStyle.BackColor!.Value;
                dgv.GridColor = Color.FromArgb(60, 60, 60);

                dgv.ColumnHeadersDefaultCellStyle.BackColor = _headerStyle.BackColor!.Value;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = _headerStyle.ForeColor!.Value;

                dgv.DefaultCellStyle.BackColor = _headerStyle.BackColor!.Value;
                dgv.DefaultCellStyle.ForeColor = _headerStyle.ForeColor!.Value;

                dgv.DefaultCellStyle.SelectionBackColor = Color.DodgerBlue;
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;

                dgv.RowHeadersDefaultCellStyle.BackColor = _headerStyle.BackColor!.Value;
                dgv.RowHeadersDefaultCellStyle.ForeColor = _headerStyle.ForeColor!.Value;
            }

            foreach (Control child in c.Controls)
                ApplyControlTheme(child, theme);
        }

        private void Dgv_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (ResolvedTheme == ThemeManager.Theme.Light) return;
            if (e.RowIndex >= 0 &&
                ((DataGridView)sender!).Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            {
                return;
            }

            if (e.RowIndex < 0 && e.ColumnIndex < 0)
            {
                return;
            }

            e.PaintBackground(e.CellBounds, true);

            string text = Convert.ToString(e.FormattedValue) ?? string.Empty;

            if (_useTextRenderer)
            {
                TextRenderer.DrawText(
                    e.Graphics!,
                    text,
                    e.CellStyle!.Font!,
                    e.CellBounds,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter
                );
            }
            else
            {
                DrawShadowText(
                    e.Graphics!,
                    text,
                    e.CellStyle!.Font!,
                    e.CellBounds,
                    e.CellStyle.ForeColor
                );
            }

            e.Handled = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (Control c in _themedControls)
                {
                    if (c is ListView lv)
                    {
                        lv.DrawColumnHeader -= Lv_DrawColumnHeader;
                        lv.DrawItem -= Lv_DrawItem;
                        lv.DrawSubItem -= Lv_DrawSubItem;
                    }
                    else if (c is TabControl tc)
                    {
                        tc.DrawItem -= Tc_DrawItem;
                    }
                }

                _themedControls.Clear();
            }

            base.Dispose(disposing);
        }

        public void SetTextRenderMode(ThemeManager.Theme resolvedTheme, ThemeManager.TextRenderMode mode)
        {
            if (IsDesignTime) return;
            switch (mode)
            {
                case ThemeManager.TextRenderMode.Auto:
                    _useTextRenderer = !(resolvedTheme == ThemeManager.Theme.Acrylic || resolvedTheme == ThemeManager.Theme.Blur);
                    break;

                case ThemeManager.TextRenderMode.AutoStrict:
                    _useTextRenderer = resolvedTheme == ThemeManager.Theme.Light || resolvedTheme == ThemeManager.Theme.Dark;
                    break;

                case ThemeManager.TextRenderMode.TextRenderer:
                    _useTextRenderer = true;
                    break;

                case ThemeManager.TextRenderMode.ShadowText:
                    _useTextRenderer = false;
                    break;
            }
            Refresh();
        }

        public IEnumerable<ToolStripItem> GetAllToolStripItems()
        {
            IEnumerable<ToolStrip> toolStrips = Controls.OfType<ToolStrip>();

            foreach (ToolStrip ts in toolStrips)
            {
                foreach (ToolStripItem item in ts.Items)
                {
                    foreach (ToolStripItem child in GetItemAndChildren(item))
                    {
                        yield return child;
                    }
                }
            }
        }

        private static IEnumerable<ToolStripItem> GetItemAndChildren(ToolStripItem item)
        {
            yield return item;

            if (item is ToolStripDropDownItem dropDown)
            {
                foreach (ToolStripItem subItem in dropDown.DropDownItems)
                {
                    foreach (var child in GetItemAndChildren(subItem))
                    {
                        yield return child;
                    }
                }
            }
        }

        public void ApplyTheme(ThemeManager.Theme theme, ThemeManager.TextRenderMode? textMode = null)
        {
            if (IsDesignTime) return;

            ThemeManager.Theme resolvedTheme = ThemeManager.ResolveTheme(theme);

            switch (resolvedTheme)
            {
                case ThemeManager.Theme.Light:
                    _headerStyle.ForeColor = SystemColors.WindowText;
                    _headerStyle.BackColor = SystemColors.Window;
                    break;

                case ThemeManager.Theme.Dark:
                    _headerStyle.ForeColor = Color.White;
                    _headerStyle.BackColor = ThemeManager.DarkMainColor;
                    break;

                case ThemeManager.Theme.ExtendFrame:
                case ThemeManager.Theme.ExtendFrameDark:
                    _headerStyle.ForeColor = Color.White;
                    _headerStyle.BackColor = Color.Black;
                    break;

                case ThemeManager.Theme.Blur:
                case ThemeManager.Theme.Acrylic:
                    _headerStyle.ForeColor = Color.White;
                    _headerStyle.BackColor = ThemeManager.AcrylicMainColor;
                    break;
            }

            ActiveTheme = theme;
            if (textMode.HasValue) SetTextRenderMode(resolvedTheme, textMode.Value);

            ApplyControlTheme(this, resolvedTheme);
            ThemeManager.ApplyThemeToForm(this, resolvedTheme);
            OnThemeWasApplied(resolvedTheme);
        }

        private void DrawShadowText(Graphics g, string text, Font font, Rectangle bounds, Color textColor)
        {
            Color shadowColor = Color.FromArgb(120, 0, 0, 0);

            TextRenderer.DrawText(
                g,
                text,
                font,
                new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width, bounds.Height),
                shadowColor,
                TextFormatFlags.Left
            );

            TextRenderer.DrawText(
                g,
                text,
                font,
                bounds,
                textColor,
                TextFormatFlags.Left
            );
        }

        private void Lv_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
        {
            Color back = e.Item!.Selected ? SystemColors.Highlight : e.Item.BackColor;
            Color fore = e.Item.Selected
                ? SystemColors.HighlightText
                : e.Item.ForeColor != e.Item.ListView!.ForeColor
                    ? e.Item.ForeColor
                    : _headerStyle.ForeColor!.Value;

            using (Brush backBrush = new SolidBrush(back))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            if (_useTextRenderer)
            {
                using Brush foreBrush = new SolidBrush(fore);
                e.Graphics.DrawString(e.SubItem!.Text, e.SubItem.Font, foreBrush, e.Bounds);
            }
            else
            {
                DrawShadowText(
                    e.Graphics,
                    e.SubItem!.Text,
                    e.SubItem.Font,
                    e.Bounds,
                    fore
                );
            }
        }

        private void Lv_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (SolidBrush backBrush = new(_headerStyle.BackColor!.Value))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            if (_useTextRenderer)
            {
                using Brush foreBrush = new SolidBrush(_headerStyle.ForeColor!.Value);
                e.Graphics.DrawString(e.Header!.Text, e.Font!, foreBrush, e.Bounds);
            }
            else
            {
                DrawShadowText(
                    e.Graphics,
                    e.Header!.Text,
                    e.Font!,
                    e.Bounds,
                    _headerStyle.ForeColor!.Value
                );
            }
        }

        private void Lv_DrawItem(object? sender, DrawListViewItemEventArgs e) => e.DrawDefault = false;

        private void Tc_DrawItem(object? sender, DrawItemEventArgs e)
        {
            TabControl tc = (TabControl)sender!;
            TabPage tab = tc.TabPages[e.Index];

            Rectangle bounds = tc.GetTabRect(e.Index);

            using SolidBrush backBrush = new(_headerStyle.BackColor!.Value);

            e.Graphics.FillRectangle(backBrush, bounds);

            TextRenderer.DrawText(
                e.Graphics,
                tab.Text,
                tc.Font,
                bounds,
                _headerStyle.ForeColor!.Value,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );

            if (tc.TabPages.Count > 0)
            {
                Rectangle lastTabRect = tc.GetTabRect(tc.TabPages.Count - 1);

                Rectangle background = new Rectangle(
                    lastTabRect.Right,
                    0,
                    tc.Right - lastTabRect.Right,
                    lastTabRect.Height + 1
                );

                e.Graphics.FillRectangle(backBrush, background);
            }
        }
    }
}
