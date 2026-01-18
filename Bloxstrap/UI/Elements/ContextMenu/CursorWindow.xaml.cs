using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using Bloxstrap.Models.Persistable;

namespace Bloxstrap.UI.Elements.ContextMenu
{
    public partial class CursorWindow : Window
    {
        private readonly DispatcherTimer _timer;

        public CursorWindow()
        {
            InitializeComponent();
            
            // Center on screen initially
            this.Left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2;
            this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50); // Fast update for position tracking if needed
            _timer.Tick += Timer_Tick;
            _timer.Start();

            DrawCrosshair();

            // Click-through
            this.Loaded += (s, e) => 
            {
               var hwnd = new WindowInteropHelper(this).Handle;
               int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
               SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW);
            };
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
             // Logic to update position to center of active window or screen
             // For now, assume center of screen or static
             if (App.Settings.Prop.Crosshair)
             {
                 if (this.Visibility != Visibility.Visible) this.Visibility = Visibility.Visible;
                 DrawCrosshair(); // re-draw if settings changed
             }
             else
             {
                 if (this.Visibility != Visibility.Collapsed) this.Visibility = Visibility.Collapsed;
             }
        }

        private void DrawCrosshair()
        {
            CrosshairCanvas.Children.Clear();
            
            // Use settings to draw
            // This is a simplified version. The full version would reuse the logic from ModsViewModel or vice-versa
            // For now, implementing basic cross drawing directly here to ensure it works.
            
            if (App.Settings.Prop.CursorType == "Default" || App.Settings.Prop.CursorType == "Cross")
            {
                double size = App.Settings.Prop.CursorSize;
                double thickness = 2; // Fixed for now or add setting
                var color = (Color)ColorConverter.ConvertFromString(App.Settings.Prop.CursorColorHex);
                
                var horz = new Rectangle { Width = size, Height = thickness, Fill = new SolidColorBrush(color) };
                var vert = new Rectangle { Width = thickness, Height = size, Fill = new SolidColorBrush(color) };
                
                Canvas.SetLeft(horz, -size/2);
                Canvas.SetTop(horz, -thickness/2);
                Canvas.SetLeft(vert, -thickness/2);
                Canvas.SetTop(vert, -size/2);
                
                CrosshairCanvas.Children.Add(horz);
                CrosshairCanvas.Children.Add(vert);
            }
        }

        #region P/Invoke
        const int GWL_EXSTYLE = -20;
        const int WS_EX_TRANSPARENT = 0x00000020;
        const int WS_EX_TOOLWINDOW = 0x00000080;

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
        #endregion
    }
}
