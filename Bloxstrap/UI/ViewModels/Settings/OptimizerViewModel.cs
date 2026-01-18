using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Bloxstrap.UI.ViewModels.Settings
{
    public class OptimizerViewModel : NotifyPropertyChangedViewModel
    {
        public int CpuCoreLimit
        {
            get => App.Settings.Prop.CpuCoreLimit;
            set
            {
                App.Settings.Prop.CpuCoreLimit = value;
                OnPropertyChanged(nameof(CpuCoreLimit));
            }
        }

        public bool DisablePostFX
        {
            get => App.Settings.Prop.DisablePostFX;
            set
            {
                App.Settings.Prop.DisablePostFX = value;
                if (value)
                {
                    App.FastFlags.SetValue("FFlagDisablePostFx", "True");
                    App.FastFlags.SetValue("FIntDebugForceMSAASamples", 0);
                }
                else
                {
                    App.FastFlags.SetValue("FFlagDisablePostFx", null);
                    App.FastFlags.SetValue("FIntDebugForceMSAASamples", null);
                }
            }
        }

        public bool DisableShadows
        {
            get => App.Settings.Prop.DisableShadows;
            set
            {
                App.Settings.Prop.DisableShadows = value;
                if (value)
                {
                    App.FastFlags.SetValue("FIntRenderShadowIntensity", 0);
                    App.FastFlags.SetValue("FFlagDebugDisplayUnthemedinstances", "False");
                }
                else
                {
                    App.FastFlags.SetValue("FIntRenderShadowIntensity", null);
                    App.FastFlags.SetValue("FFlagDebugDisplayUnthemedinstances", null);
                }
            }
        }
        
        public bool HyperCoreThreading
        {
            get => App.Settings.Prop.HyperCoreThreading;
            set => App.Settings.Prop.HyperCoreThreading = value;
        }

        // We can add more optimizer settings here later
    }
}
