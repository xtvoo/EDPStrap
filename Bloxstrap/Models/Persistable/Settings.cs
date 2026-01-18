using System.Collections.ObjectModel;

namespace Bloxstrap.Models.Persistable
{
    public class Settings
    {
        // uh
        public bool AllowCookieAccess { get; set; } = false;

        // bloxstrap configuration
        public BootstrapperStyle BootstrapperStyle { get; set; } = BootstrapperStyle.FluentAeroDialog;
        public BootstrapperIcon BootstrapperIcon { get; set; } = BootstrapperIcon.IconEDPStrap;
        public string BootstrapperTitle { get; set; } = App.ProjectName;
        public string BootstrapperIconCustomLocation { get; set; } = "";
        public string WindowBackgroundImage { get; set; } = "";
        public double WindowBackgroundOpacity { get; set; } = 0.8;
        public bool AutoLogCleanup { get; set; } = false;
        public bool EnableLuaScripting { get; set; } = false;
        public int CpuCoreLimit { get; set; } = 0;
        public Theme Theme { get; set; } = Theme.Default;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool DeveloperMode { get; set; } = false;
        public bool ForceLocalData { get; set; } = false;
        public bool CheckForUpdates { get; set; } = true;
        public bool MultiInstanceLaunching { get; set; } = false;
        public bool ConfirmLaunches { get; set; } = true;
        public string Locale { get; set; } = "nil";
        public bool ForceRobloxLanguage { get; set; } = false;
        public bool UseFastFlagManager { get; set; } = true;
        public bool WPFSoftwareRender { get; set; } = false;
        public bool EnableAnalytics { get; set; } = false;
        public bool UpdateRoblox { get; set; } = true;
        public bool StaticDirectory { get; set; } = false;
        public string Channel { get; set; } = RobloxInterfaces.Deployment.DefaultChannel;
        public ChannelChangeMode ChannelChangeMode { get; set; } = ChannelChangeMode.Automatic;
        public string ChannelHash { get; set; } = "";
        public string DownloadingStringFormat { get; set; } = Strings.Bootstrapper_Status_Downloading + " {0} - {1}MB / {2}MB";
        public string? SelectedCustomTheme { get; set; } = null;
        public bool BackgroundUpdatesEnabled { get; set; } = false;
        public bool DebugDisableVersionPackageCleanup { get; set; } = false;
        public WebEnvironment WebEnvironment { get; set; } = WebEnvironment.Production;

        // integration configuration
        public CleanerOptions CleanerOptions { get; set; } = CleanerOptions.Never;
        public List<string> CleanerDirectories { get; set; } = new List<string>();
        public bool EnableActivityTracking { get; set; } = true;
        public bool UseDiscordRichPresence { get; set; } = true;
        public bool HideRPCButtons { get; set; } = true;
        public bool ShowAccountOnRichPresence { get; set; } = false;
        public bool ShowServerDetails { get; set; } = false;
        public ObservableCollection<CustomIntegration> CustomIntegrations { get; set; } = new();

        // mod preset configuration
        public bool UseDisableAppPatch { get; set; } = false;

        // Advanced Voidstrap Configuration
        public string SkyboxName { get; set; } = "Default";

        public bool Crosshair { get; set; } = false;
        public string CursorType { get; set; } = "Default";
        public string ImageUrl { get; set; } = "";
        public string CursorColorHex { get; set; } = "#FFFFFF";
        public int CursorSize { get; set; } = 32;

        public bool FPSCounter { get; set; } = false;
        public bool ServerPingCounter { get; set; } = false;
        public bool CurrentTimeDisplay { get; set; } = false;

        public bool FakeVerifiedBadge { get; set; } = false;
        public string FakeVerifiedUserId { get; set; } = "";

        public bool HyperCoreThreading { get; set; } = false;
        public bool DisablePostFX { get; set; } = false;
        public bool DisableShadows { get; set; } = false;
        public bool LowQualityTextures { get; set; } = false;
    }
}