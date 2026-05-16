using System.Runtime.InteropServices;

namespace MSZDialougeManager
{
    public static class ListViewGroupHelpers
    {
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, ref LVHITTESTINFO lParam);

        private const uint LVM_SUBITEMHITTEST = 0x1000 + 57;
        private const uint LVM_HITTEST = 0x1000 + 18;
        private const uint LVHT_EX_GROUP_HEADER = 0x10000000;

        [StructLayout(LayoutKind.Sequential)]
        private struct LVHITTESTINFO
        {
            public int x;
            public int y;
            public uint flags;
            public int iItem;
            public int iSubItem;
            public int iGroup;
        }

        private static int GetGroupId(ListViewGroup group)
        {
            var prop = typeof(ListViewGroup).GetProperty("ID",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);
            return prop != null ? (int)prop.GetValue(group)! : -1;
        }

        public static ListViewGroup? GetGroupAt(ListView lv, Point pt)
        {
            LVHITTESTINFO info = new() { x = pt.X, y = pt.Y };

            uint msg = lv.View == View.Details ? LVM_SUBITEMHITTEST : LVM_HITTEST;
            int returnedId = SendMessage(lv.Handle, msg, -1, ref info);

            if ((info.flags & LVHT_EX_GROUP_HEADER) != 0 && returnedId != -1)
            {
                foreach (ListViewGroup group in lv.Groups)
                {
                    if (GetGroupId(group) == returnedId)
                        return group;
                }
            }

            return null;
        }
    }
}
