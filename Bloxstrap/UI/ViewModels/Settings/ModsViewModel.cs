using System.Windows;
using System.Windows.Input;

using Microsoft.Win32;

using Windows.Win32;
using Windows.Win32.UI.Shell;
using Windows.Win32.Foundation;

using CommunityToolkit.Mvvm.Input;

using Bloxstrap.Models.SettingTasks;
using Bloxstrap.AppData;

namespace Bloxstrap.UI.ViewModels.Settings
{
    public class ModsViewModel : NotifyPropertyChangedViewModel
    {
        private void OpenModsFolder() => Process.Start("explorer.exe", Paths.Modifications);

        private readonly Dictionary<string, byte[]> FontHeaders = new()
        {
            { "ttf", new byte[4] { 0x00, 0x01, 0x00, 0x00 } },
            { "otf", new byte[4] { 0x4F, 0x54, 0x54, 0x4F } },
            { "ttc", new byte[4] { 0x74, 0x74, 0x63, 0x66 } } 
        };

        private void ManageCustomFont()
        {
            if (!String.IsNullOrEmpty(TextFontTask.NewState))
            {
                TextFontTask.NewState = "";
            }
            else
            {
                var dialog = new OpenFileDialog
                {
                    Filter = $"{Strings.Menu_FontFiles}|*.ttf;*.otf;*.ttc"
                };

                if (dialog.ShowDialog() != true)
                    return;

                string type = dialog.FileName.Substring(dialog.FileName.Length-3, 3).ToLowerInvariant();

                if (!FontHeaders.ContainsKey(type) 
                    || !FontHeaders.Any(x => File.ReadAllBytes(dialog.FileName).Take(4).SequenceEqual(x.Value)))
                {
                    Frontend.ShowMessageBox(Strings.Menu_Mods_Misc_CustomFont_Invalid, MessageBoxImage.Error);
                    return;
                }

                TextFontTask.NewState = dialog.FileName;
            }

            OnPropertyChanged(nameof(ChooseCustomFontVisibility));
            OnPropertyChanged(nameof(DeleteCustomFontVisibility));
        }

        public ICommand OpenModsFolderCommand => new RelayCommand(OpenModsFolder);

        public Visibility ChooseCustomFontVisibility => !String.IsNullOrEmpty(TextFontTask.NewState) ? Visibility.Collapsed : Visibility.Visible;

        public Visibility DeleteCustomFontVisibility => !String.IsNullOrEmpty(TextFontTask.NewState) ? Visibility.Visible : Visibility.Collapsed;

        public ICommand ManageCustomFontCommand => new RelayCommand(ManageCustomFont);

        public ICommand OpenCompatSettingsCommand => new RelayCommand(OpenCompatSettings);

        public ModPresetTask OldAvatarBackgroundTask { get; } = new("OldAvatarBackground", @"ExtraContent\places\Mobile.rbxl", "OldAvatarBackground.rbxl");

        public ModPresetTask OldCharacterSoundsTask { get; } = new("OldCharacterSounds", new()
        {
            { @"content\sounds\action_footsteps_plastic.mp3", "Sounds.OldWalk.mp3"  },
            { @"content\sounds\action_jump.mp3",              "Sounds.OldJump.mp3"  },
            { @"content\sounds\action_get_up.mp3",            "Sounds.OldGetUp.mp3" },
            { @"content\sounds\action_falling.mp3",           "Sounds.Empty.mp3"    },
            { @"content\sounds\action_jump_land.mp3",         "Sounds.Empty.mp3"    },
            { @"content\sounds\action_swim.mp3",              "Sounds.Empty.mp3"    },
            { @"content\sounds\impact_water.mp3",             "Sounds.Empty.mp3"    },
            { @"content\sounds\ouch.ogg",                     "Sounds.OldDeath.ogg" }
        });

        public EmojiModPresetTask EmojiFontTask { get; } = new();

        public EnumModPresetTask<Enums.CursorType> CursorTypeTask { get; } = new("CursorType", new()
        {
            {
                Enums.CursorType.From2006, new()
                {
                    { @"content\textures\Cursors\KeyboardMouse\ArrowCursor.png",    "Cursor.From2006.ArrowCursor.png"    },
                    { @"content\textures\Cursors\KeyboardMouse\ArrowFarCursor.png", "Cursor.From2006.ArrowFarCursor.png" }
                }
            },
            {
                Enums.CursorType.From2013, new()
                {
                    { @"content\textures\Cursors\KeyboardMouse\ArrowCursor.png",    "Cursor.From2013.ArrowCursor.png"    },
                    { @"content\textures\Cursors\KeyboardMouse\ArrowFarCursor.png", "Cursor.From2013.ArrowFarCursor.png" }
                }
            }
        });

        public FontModPresetTask TextFontTask { get; } = new();

        public bool EnableLuaScripting
        {
            get => App.Settings.Prop.EnableLuaScripting;
            set
            {
                App.Settings.Prop.EnableLuaScripting = value;
                OnPropertyChanged(nameof(EnableLuaScripting));
                OnPropertyChanged(nameof(ScriptsFolderVisibility));
            }
        }

        public Visibility ScriptsFolderVisibility => EnableLuaScripting ? Visibility.Visible : Visibility.Collapsed;

        public ICommand OpenScriptsFolderCommand => new RelayCommand(() => Process.Start("explorer.exe", Paths.Base));

        // Advanced Voidstrap Features

        // Crosshair
        public bool Crosshair
        {
            get => App.Settings.Prop.Crosshair;
            set { App.Settings.Prop.Crosshair = value; OnPropertyChanged(nameof(Crosshair)); }
        }

        public string CursorColorHex
        {
            get => App.Settings.Prop.CursorColorHex;
            set { App.Settings.Prop.CursorColorHex = value; OnPropertyChanged(nameof(CursorColorHex)); }
        }

        public int CursorSize
        {
            get => App.Settings.Prop.CursorSize;
            set { App.Settings.Prop.CursorSize = value; OnPropertyChanged(nameof(CursorSize)); }
        }

         public string ImageUrl
        {
            get => App.Settings.Prop.ImageUrl;
            set { App.Settings.Prop.ImageUrl = value; OnPropertyChanged(nameof(ImageUrl)); }
        }


        // Overlays
        public bool FPSCounter
        {
            get => App.Settings.Prop.FPSCounter;
            set { App.Settings.Prop.FPSCounter = value; OnPropertyChanged(nameof(FPSCounter)); }
        }

        public bool ServerPingCounter
        {
            get => App.Settings.Prop.ServerPingCounter;
            set { App.Settings.Prop.ServerPingCounter = value; OnPropertyChanged(nameof(ServerPingCounter)); }
        }

         public bool CurrentTimeDisplay
        {
            get => App.Settings.Prop.CurrentTimeDisplay;
            set { App.Settings.Prop.CurrentTimeDisplay = value; OnPropertyChanged(nameof(CurrentTimeDisplay)); }
        }

        // Skybox
         public string SkyboxName
        {
            get => App.Settings.Prop.SkyboxName;
            set { App.Settings.Prop.SkyboxName = value; OnPropertyChanged(nameof(SkyboxName)); }
        }

        public List<string> SkyboxSelections => new List<string>() { "Default", "Blue Sky", "Galaxy", "Purple", "Night" }; // Todo: Load dynamically or from enum
        
        // Fake Verified Badge
        public bool FakeVerifiedBadge
        {
            get => App.Settings.Prop.FakeVerifiedBadge;
            set
            {
                App.Settings.Prop.FakeVerifiedBadge = value;
                UpdateFakeVerifiedFlag();
                OnPropertyChanged(nameof(FakeVerifiedBadge));
                OnPropertyChanged(nameof(FakeVerifiedUserIdVisibility));
            }
        }

        public string FakeVerifiedUserId
        {
            get => App.Settings.Prop.FakeVerifiedUserId ?? "";
            set
            {
                App.Settings.Prop.FakeVerifiedUserId = value;
                UpdateFakeVerifiedFlag();
                OnPropertyChanged(nameof(FakeVerifiedUserId));
            }
        }

        public Visibility FakeVerifiedUserIdVisibility => FakeVerifiedBadge ? Visibility.Visible : Visibility.Collapsed;

        private async void UpdateFakeVerifiedFlag()
        {
            if (FakeVerifiedBadge && !string.IsNullOrEmpty(FakeVerifiedUserId))
            {
                string input = FakeVerifiedUserId.Trim();
                
                // If input text contains non-digits, assume it's a username and try to resolve it
                if (!long.TryParse(input, out _))
                {
                    try
                    {
                        var response = await App.HttpClient.GetAsync($"https://users.roblox.com/v1/users/search?keyword={input}&limit=10");
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            using var doc = System.Text.Json.JsonDocument.Parse(json);
                            var data = doc.RootElement.GetProperty("data");
                            if (data.GetArrayLength() > 0)
                            {
                                // Find exact match or first
                                string? resolvedId = null;
                                foreach (var user in data.EnumerateArray())
                                {
                                    if (string.Equals(user.GetProperty("name").GetString(), input, StringComparison.OrdinalIgnoreCase))
                                    {
                                        resolvedId = user.GetProperty("id").ToString();
                                        break;
                                    }
                                }
                                
                                if (resolvedId == null)
                                    resolvedId = data[0].GetProperty("id").ToString(); // fallback to first result

                                if (resolvedId != null)
                                {
                                    FakeVerifiedUserId = resolvedId; // Update property (triggering this again, but it will be numeric now)
                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        App.Logger.WriteLine("ModsViewModel::UpdateFakeVerifiedFlag", $"Failed to resolve username: {ex.Message}");
                    }
                }

                App.FastFlags.SetValue("FStringWhitelistVerifiedUserId", input);
            }
            else
            {
                App.FastFlags.SetValue("FStringWhitelistVerifiedUserId", null);
            }
            
            App.FastFlags.Save();
        }
        
        private void OpenCompatSettings()
        {
            string path = new RobloxPlayerData().ExecutablePath;

            if (File.Exists(path))
                PInvoke.SHObjectProperties(HWND.Null, SHOP_TYPE.SHOP_FILEPATH, path, "Compatibility");
            else
                Frontend.ShowMessageBox(Strings.Common_RobloxNotInstalled, MessageBoxImage.Error);
        }
    }
}
